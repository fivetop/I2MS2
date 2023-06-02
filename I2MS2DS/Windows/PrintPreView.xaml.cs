using I2MS2.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Printing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps;

namespace I2MS2.Windows
{
    using Excel = Microsoft.Office.Interop.Excel;
    using System.IO.Packaging;
    using System.Windows.Xps.Packaging;
    using System.IO;
    using I2MS2.Models;
    using System.Threading;
    using System.Windows.Threading;
    using System.Runtime.InteropServices;
    using System.Diagnostics;
    using System.ComponentModel;

    // 리스트 뷰에서 동적 헤더를 생성하기 위한 클래스 
    class listHeader
    {
        public double h_width;          // 길이 
        public string h_title;          // 헤더 이름  
        public string h_bind;           // 바인딩  
    }

    // 리스트뷰에서 컬럼이 이동 되었을경우 원래 위치와 비교를 위한 클래스 
    class lvCompare
    {
        public int orgid;               // 초기 위치 
        public int newid;               // 변경 위치 
        public double orggvcWidth;      // 컬럼, 해당 width 확인용  
        public TextBlock newtb;         // 헤더 텍스트 블럭, 해당 헤더 텍스트 확인용  
        public GridViewColumn newgvc;   // 컬럼, 해당 width 확인용 
    }

    public class iPrint
    {
        public bool _landscape = false; // port 0, land 1

        public int p_cnt;                // 갯수 
        public string[] p_hdn;            // 헤더 이름 
        public double[] p_hdw;            // 헤더 길이 
        public double t_hdw;            // 헤더 길이 

        public string p_name;            // 출력물 파일이름  
        public string p_title1;            // 헤더 이름 
        public string p_title2;            // 헤더 이름 

        public Table oTable;
        public TableRow r1;

        public iPrint()
        {
            //throw new System.NotImplementedException();
        }

        ~iPrint()
        {
            //throw new System.NotImplementedException();
        }

        public void RemoveAll()
        {
            if (oTable != null)
            {
                try
                {
                    Console.WriteLine("Remove Print memory...");

                    for (int i = 0; 1 <= oTable.RowGroups[0].Rows.Count; i++)
                    {
                        oTable.RowGroups[0].Rows.RemoveAt(0);
                    }
                    oTable.RowGroups.RemoveAt(0);
                    for (int i = 0; 1 <= oTable.Columns.Count; i++)
                    {
                        oTable.Columns.RemoveAt(0);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}", e.Message.ToString());
                }
                oTable.Columns.Clear();
                oTable = null;
            }
        }

        internal int anaylize(ListView l1)
        {
            GridView v1 = (GridView)l1.View;
            GridViewColumn b1 = (GridViewColumn)v1.Columns[0];
            if (b1 == null) return 1;
            TextBlock s2 = (TextBlock)((Border)v1.Columns[0].Header).Child;
            if (s2 == null) return 2;

            p_cnt = v1.Columns.Count;

            if (oTable != null)
            {
                p_hdn = null;
                p_hdw = null;
            }
            p_hdn = new string[p_cnt];
            p_hdw = new double[p_cnt];
            t_hdw = 0;

            int cnt = 0;

            for (int i = 1; i < p_cnt; i++)
            {
                b1 = (GridViewColumn)v1.Columns[i];
                s2 = (TextBlock)((Border)v1.Columns[i].Header).Child;
            
                if (b1.ActualWidth != 0)
                {
                    p_hdn[cnt] = s2.Text;
                    p_hdw[cnt] = b1.ActualWidth; //b1.Width;
                    cnt++;
                }
                t_hdw = t_hdw + b1.ActualWidth;
            }
            p_cnt = cnt;
            // 출력용 테이블 

            if (oTable != null)
            {
                try
                {
                    for (int i = 0; 1 <= oTable.RowGroups[0].Rows.Count; i++)
                    {
                        oTable.RowGroups[0].Rows.RemoveAt(0);
                    }
                    oTable.RowGroups.RemoveAt(0);
                    for (int i = 0; 1 <= oTable.Columns.Count; i++)
                    {
                        oTable.Columns.RemoveAt(0);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("{0}", e.Message.ToString());
                }
                oTable.Columns.Clear();
                oTable = null;
            }

            oTable = new Table();
            oTable.Name = "_oTable";

            // 컬럼 만들기 사이즈 와 함께
            for (int x = 0; x < p_cnt; x++)
            {

                oTable.Columns.Add(new TableColumn());
                oTable.Columns[x].Width = new GridLength(p_hdw[x]);
            }
            // 로우그룹 생성 
            oTable.RowGroups.Add(new TableRowGroup());

            // 헤더용 로우 생성 
            oTable.RowGroups[0].Rows.Add(new TableRow());
            r1 = oTable.RowGroups[0].Rows[0];
            r1.Background = System.Windows.Media.Brushes.Navy;
            r1.Foreground = System.Windows.Media.Brushes.White;
            r1.FontSize = 11;
            // 테이블 헤더 적용 
            for (int x = 0; x < p_cnt; x++)
            {
                r1.Cells.Add(new TableCell(new Paragraph(new Run(p_hdn[x]))));
            }

            return 0;
        }

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DateTime currentDateTime = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            currentDateTime = currentDateTime.AddSeconds((uint)value);
            return currentDateTime.ToString();
        }

        internal static double get_timestamp(byte[] bytes)
        {
            double result = 0;
            double inter = 0;
            for (int i = 0; i < bytes.Length; i++)
            {

                inter = System.Convert.ToDouble(bytes[i]);
                inter = inter * Math.Pow(2, ((7 - i) * 8));
                result += inter;
            }

            var value = BitConverter.ToUInt64(bytes.Reverse().ToArray(), 0);
            result = value;
            return result;
        }

        internal static DateTime ConvertFromTimestamp(double timestamp)
        {
            DateTime origin = new DateTime(1900, 1, 1, 9, 0, 0, 0).ToLocalTime () ;
            return origin.AddDays(timestamp);
        }


        internal static double ConvertToTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan diff = date - origin;
            return Math.Floor(diff.TotalSeconds);
        }

    }

    /// <summary>
    /// ManufacturePrint.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PrintPreView : Window
    {
        DateTime time1 = DateTime.Now;
        bool isPrinting = false;
        public iPrint ip;

        public PrintPreView(iPrint ip1)
        {
            ip = ip1;
            InitializeComponent();
            InitializeTable();
        }

        private void InitializeTable()
        {
            // 제목 설정 
            ((Run)_Title1.Inlines.FirstInline).Text = ip.p_title1;
            //((Run)_Title2.Inlines.FirstInline).Text = ip.p_title2; 

            flowDocument.Blocks.Add(ip.oTable);
            if (ip.t_hdw > 790)
            {
                flowDocument.PageHeight = 790;
                flowDocument.PageWidth = ip.t_hdw + 50;
            }
            flowDocument.ColumnWidth = 9999;

            //documentReader.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            //var desiredSize = documentReader.DesiredSize;

            Flow2Fixed();
            documentViewer.t_hdw = ip.t_hdw;
            documentViewer.p_name = ip.p_name;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //
        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            if (isPrinting) return;
            isPrinting = true;
            try
            {
                save_2_excel();
            }
            finally
            { 
                if(oXL != null)
                    TryKillProcessByMainWindowHwnd(oXL.Application.Hwnd);
            }
            isPrinting = false;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                if (ip.t_hdw > 1120)
                {
                    //flowDocument.PageHeight = 790;
                    //flowDocument.PageWidth = ip.t_hdw + 50;

                    // 가로 출력에 한계가 있음 , 가로 출력후 남는 부분이 출력이 안되는 문제 있음 -> flowDocument => fixedDocument 변경이 필요한지 검토 처리 아니면 페이지네이터를 ???
                    var paginator = ((IDocumentPaginatorSource)flowDocument).DocumentPaginator;
                    paginator.PageSize = new Size(ip.t_hdw + 50, 790);  //new Size(1120, 790);
                    printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
                }

                printDialog.PrintDocument(((IDocumentPaginatorSource)flowDocument).DocumentPaginator, "Flow Document Print Job");
            }

        }

/*
 * // 프린터 속도 보완용 시험 코드 // 사용 안함 romeeo 
        private void save_2_excel2()
        {
            ExportToExcel<AssetPrintList, AssetPrintLists> s = new ExportToExcel<AssetPrintList, AssetPrintLists>();
            ICollectionView view = CollectionViewSource.GetDefaultView(ip.lstview1.ItemsSource);
            AssetPrintLists _pl = new AssetPrintLists();
            var t1 = ip.lstview1.ItemsSource;
            foreach (AssetPrintList p1 in t1)
            {
                _pl.Add(p1);
            }
            s.dataToPrint = _pl;
            s.GenerateReport();
        }
*/

        Excel.Application oXL;
        Excel._Workbook oWB;

        // 엑셀 출력 모듈 
        private bool save_2_excel()
        {
            try
            {
                oXL = new Excel.Application();
            }
            catch
            {
                MessageBox.Show("Check the Microsoft Excel program.");
                return false;
            }
            oWB = oXL.Workbooks.Add(true);

            object missingType = Type.Missing;

            int T_Year = DateTime.Now.Year;
            int T_Month = DateTime.Now.Month;
            int T_Day = DateTime.Now.Day;
            int T_Hour = DateTime.Now.Hour;
            int T_Minute = DateTime.Now.Minute;
            int T_Second = DateTime.Now.Second;

            DateTime dTime = new DateTime(T_Year, T_Month, T_Day, T_Hour, T_Minute, T_Second);
            string time = dTime.ToString("yyMMdd_HHmmss");
            string data = ip.p_title1 + time + ".xls";
            string templete_filename = ip.p_title2;

            Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();

            saveFileDialog1.AddExtension = true;
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.CreatePrompt = false;
            saveFileDialog1.OverwritePrompt = false;
            saveFileDialog1.FileName = data;
            saveFileDialog1.DefaultExt = "xls";
            saveFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog1.InitialDirectory = "";
            var f = saveFileDialog1.ShowDialog();

            if (f != true)
            {
                return false;
            }

            try
            {
                string lang_code = g.lang_id == 1080001 ? "ko" : "en";    // 1080001=한글, 영문=1080002

                string file = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + "\\LSCable\\SimpleWIN\\ExcelTemplates\\" + templete_filename + "_" + lang_code;

                oWB = oXL.Workbooks.Open(file, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, 1, 0);
                Excel._Worksheet oSheet = oWB.Worksheets.get_Item(1) as Excel._Worksheet;

                int line = 5;
                int idx;

                oSheet.Cells[2, 2] = ip.p_title1;

                TableRowGroup l1 = ip.oTable.RowGroups[0];

                // 제목 출력 
                TableRow r1 = l1.Rows[0];
                for (int c = 0; c < r1.Cells.Count; c++)
                {
                    Paragraph p = r1.Cells[c].Blocks.FirstBlock as Paragraph;
                    Run run = p.Inlines.FirstInline as Run;
                    string text = run.Text;
                    oSheet.Cells.Font.Bold = true;
                    oSheet.Cells[4, c + 2] = text;
                }

                // 내용 출력 
                for (idx = 1; idx < l1.Rows.Count; idx++)
                {
                    this.Cursor = Cursors.Wait;
                    TableRow r2 = l1.Rows[idx];
                    string s = string.Format("{0}/{1}", (idx+1).ToString(), l1.Rows.Count.ToString());
                    _p1.Text = "Excel Print Status : " + s;
                    _p1.Refresh_Controls();

//                    Thread.Sleep(10);
                    for (int c = 0; c < r2.Cells.Count; c++)
                    {
                        string text = ((Run)((Paragraph)r2.Cells[c].Blocks.FirstBlock).Inlines.FirstInline).Text;
                        //oSheet.Cells.Font.Bold = false;
                        oSheet.Cells[line, c + 2] = text;
                    }
                    line++;
                }
                this.Cursor = null;

                // oSheet.Columns.AutoFit();
                // Save Excel File
                oWB.SaveAs(saveFileDialog1.FileName, Excel.XlFileFormat.xlWorkbookNormal, missingType, missingType,
                missingType, missingType, Excel.XlSaveAsAccessMode.xlNoChange,
                missingType, missingType, missingType, missingType, missingType);

            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(g.tr_get("C_Error_Save_Excel") + " {0}", e.Message));
            }
            _p1.Text = "Excel Print Status : Done";
            _p1.Refresh_Controls();
            return true;
        }

        XpsDocument xps;
        Package package;
        Uri packUri;

        private void Flow2Fixed()
        {
            var paginator = ((IDocumentPaginatorSource)flowDocument).DocumentPaginator;
            package = Package.Open(new MemoryStream(), FileMode.Create, FileAccess.ReadWrite);
            packUri = new Uri("pack://temp.xps");
            PackageStore.RemovePackage(packUri);
            PackageStore.AddPackage(packUri, package);
            xps = new XpsDocument(package, CompressionOption.NotCompressed, packUri.ToString());
            XpsDocument.CreateXpsDocumentWriter(xps).Write(paginator);

            documentViewer.Document = xps.GetFixedDocumentSequence(); // (IDocumentPaginatorSource)paginator;
            //PackageStore.RemovePackage(packUri);
            //documentViewer.Document = (IDocumentPaginatorSource)flowDocument;
            //PackageStore.RemovePackage(packUri);
            //package.Close();
        }

        private void CommandBinding_CanExecutePrint(object sender, CanExecuteRoutedEventArgs e)
        {
//
            e.Handled = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try {
                flowDocument.Blocks.Remove(ip.oTable);
                ip.RemoveAll();
                xps.Close();
                PackageStore.RemovePackage(packUri);
                package.Close();
                documentReader.Document = null;
                documentViewer.Document = null;
                System.GC.Collect();
            }
            catch(Exception e1)
            {
                Console.WriteLine("Remove Print memory...{0}", e1.Message.ToString());

            }
        }

        // kill processor 추가 romee 2/16
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        public static bool TryKillProcessByMainWindowHwnd(int hWnd)
        {
            uint processID;
            GetWindowThreadProcessId((IntPtr)hWnd, out processID);
            if (processID == 0) return false;
            try
            {
                Process.GetProcessById((int)processID).Kill();
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (Win32Exception)
            {
                return false;
            }
            catch (NotSupportedException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            return true;
        }

        public static void KillProcessByMainWindowHwnd(int hWnd)
        {
            uint processID;
            GetWindowThreadProcessId((IntPtr)hWnd, out processID);
            if (processID == 0)
                throw new ArgumentException("Process has not been found by the given main window handle.", "hWnd");
            Process.GetProcessById((int)processID).Kill();
        }
    
    }


    //
    // DocumentViewer 클래스 출력
    //
    public class PrintDocumentViewer : DocumentViewer
    {
        public double t_hdw;            // 헤더 길이 
        public string p_name;            // 출력물 파일이름  

        PageOrientation _pageOrientation = PageOrientation.Portrait;
        public PageOrientation PageOrientation
        {
            get { return _pageOrientation; }
            set { _pageOrientation = value; }
        }

        Visibility _findControlVisibility = Visibility.Collapsed;
        public Visibility FindControlVisibility
        {
            get
            {
                return _findControlVisibility;
            }
            set
            {
                _findControlVisibility = value;
                UpdateFindControlVisibility();
            }
        }

        private void UpdateFindControlVisibility()
        {
            object toolbar = this.Template.FindName("PART_FindToolBarHost", this);
            ContentControl cc = toolbar as ContentControl;
            if (cc != null)
            {
                HeaderedItemsControl itemsControl = cc.Content as HeaderedItemsControl;
                if (itemsControl != null)
                    itemsControl.Visibility = FindControlVisibility;
            }
        }

        public PrintDocumentViewer()
        {
            Loaded += new RoutedEventHandler(PrintDocumentViewer_Loaded);
        }

        void PrintDocumentViewer_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateFindControlVisibility();
        }

        protected override void OnPrintCommand()
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();
            printDialog.PrintTicket = printDialog.PrintQueue.DefaultPrintTicket;

            printDialog.PrintTicket.PageOrientation = PageOrientation;

            if (printDialog.ShowDialog() == true)
            {
                // Code assumes this.Document will either by a FixedDocument or a FixedDocumentSequence
                FixedDocument fixedDocument = this.Document as FixedDocument;
                FixedDocumentSequence fixedDocumentSequence = this.Document as FixedDocumentSequence;

                if (fixedDocument != null)
                    fixedDocument.PrintTicket = printDialog.PrintTicket;

                if (fixedDocumentSequence != null)
                    fixedDocumentSequence.PrintTicket = printDialog.PrintTicket;

                if (t_hdw > 790)
                {
                    // 가로 출력에 한계가 있음 , 가로 출력후 남는 부분이 출력이 안되는 문제 있음 -> flowDocument => fixedDocument 변경이 필요한지 검토 처리 아니면 페이지네이터를 ???
                    printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
                }
                printDialog.PrintQueue.CurrentJobSettings.Description = p_name;
                XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(printDialog.PrintQueue);

                if (fixedDocument != null)
                    writer.WriteAsync(fixedDocument, printDialog.PrintTicket);

                if (fixedDocumentSequence != null)
                    writer.WriteAsync(fixedDocumentSequence, printDialog.PrintTicket);
            }
        }

    }
}
