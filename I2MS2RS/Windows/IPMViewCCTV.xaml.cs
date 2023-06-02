using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls.Primitives;
using System.Collections;
using System.IO;
using MahApps.Metro.Controls;

using I2MS2.Models;
using I2MS2.Library;
using WebApi.Models;
using I2MS2.UserControls;
using System.Reflection;
using System.Runtime.InteropServices;

#pragma warning disable 4014

namespace I2MS2.Windows
{
    /// <summary>
    /// IPMView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class IPMViewCCTV : MetroWindow
    {
        int _location_id = 0;

        public IPMViewCCTV(int location_id)
        {
            _location_id = location_id;
            InitializeComponent();


            asset ast = g.asset_list.Find(at => at.asset_id == _location_id);
            if (ast == null)
                return;
            try
            { 
                wbSample.Navigate(new Uri("http://" + ast.ipv4 + "/index.php?menu=cctv&action=view")); // + "/index.php?menu=cctv&action=view"); // "http://www.wpf-tutorial.com");
            }
            catch(Exception e1)
            {
                Console.WriteLine(e1.ToString());
            }
        }

        private void wbSample_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            Console.WriteLine(e.Uri.OriginalString);
            HideScriptErrors(wbSample, true);
        }


        public void HideScriptErrors(WebBrowser wb, bool Hide)
        {

            FieldInfo fiComWebBrowser = typeof(WebBrowser).GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fiComWebBrowser == null) return;
            object objComWebBrowser = fiComWebBrowser.GetValue(wb);

            if (objComWebBrowser == null) return;

            objComWebBrowser.GetType().InvokeMember(
            "Silent", BindingFlags.SetProperty, null, objComWebBrowser, new object[] { Hide });

        }


        public static void SetSilent(WebBrowser browser, bool silent)
        {
            if (browser == null)
                throw new ArgumentNullException("browser");

            // get an IWebBrowser2 from the document
            IOleServiceProvider sp = browser.Document as IOleServiceProvider;
            if (sp != null)
            {
                Guid IID_IWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
                Guid IID_IWebBrowser2 = new Guid("D30C1661-CDAF-11d0-8A3E-00C04FC9E26E");

                object webBrowser;
                sp.QueryService(ref IID_IWebBrowserApp, ref IID_IWebBrowser2, out webBrowser);
                if (webBrowser != null)
                {
                    webBrowser.GetType().InvokeMember("Silent", BindingFlags.Instance | BindingFlags.Public | BindingFlags.PutDispProperty, null, webBrowser, new object[] { silent });
                }
            }
        }


        [ComImport, Guid("6D5140C1-7436-11CE-8034-00AA006009FA"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        private interface IOleServiceProvider
        {
            [PreserveSig]
            int QueryService([In] ref Guid guidService, [In] ref Guid riid, [MarshalAs(UnmanagedType.IDispatch)] out object ppvObject);
        }

        private void wbSample_Navigated(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {
            SetSilent(wbSample, true); // make it silent
        }
    }


}
