using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;
using MahApps.Metro.Controls.Dialogs;

namespace I2MS2
{
    public class MainWindowViewModel2 : INotifyPropertyChanged, IDataErrorInfo
    {
        private readonly IDialogCoordinator _dialogCoordinator;

        public MainWindowViewModel2(IDialogCoordinator dialogCoordinator)
        {
            _dialogCoordinator = dialogCoordinator;
            BrushResources = FindBrushResources();
            CultureInfos = CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures).ToList();
        }

        public string Title { get; set; }
        public List<CultureInfo> CultureInfos { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event if needed.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string this[string columnName]
        {
            get
            {
                return null;
            }
        }

        public string Error { get { return string.Empty; } }
        public IEnumerable<string> BrushResources { get; private set; }
        private IEnumerable<string> FindBrushResources()
        {
            var rd = new ResourceDictionary
                {
                    Source = new Uri(@"/MahApps.Metro;component/Styles/Colors.xaml", UriKind.RelativeOrAbsolute)
                };

            var resources = rd.Keys.Cast<object>()
                    .Where(key => rd[key] is Brush)
                    .Select(key => key.ToString())
                    .OrderBy(s => s)
                    .ToList();

            return resources;
        }

    }
}