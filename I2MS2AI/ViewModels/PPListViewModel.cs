using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.ComponentModel;
using System.Windows.Data;
using I2MS2.Models;
using System.Windows;
using System;

// 사용안함 
//namespace I2MS2.ViewModels
//{
//    public class PPListView
//    {
//        public PPListViewModel DataContext { get; set; }

//        public PPListView()
//        {
//            DataContext = new PPListViewModel();
//        }
//    }

//    public class PPListViewModel : INotifyPropertyChanged
//    {

//        Color color_red = (Color)Application.Current.Resources["_colorRed"];

//        private ICollectionView _pp_list_view;

//        public ICollectionView pp_list_view
//        {
//            get { return _pp_list_view; }
//        }

//        public PPListViewModel()
//        {
//            IList<ipp_list> pp_lists = getPPLists();
//            _pp_list_view = CollectionViewSource.GetDefaultView(pp_lists);
//        }

//        public IList<ipp_list> getPPLists(List<ipp_list> ipps)
//        {
//            return ipps.ToList();
//        }

//        public event PropertyChangedEventHandler PropertyChanged;

//        public void NotifyPropertyChanged(String info)
//        {
//            if (PropertyChanged != null)
//            {
//                PropertyChanged(this, new PropertyChangedEventArgs(info));
//            }
//        }
//    }

//}