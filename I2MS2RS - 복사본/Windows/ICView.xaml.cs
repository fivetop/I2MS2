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
using I2MS2.Models;
using System.ComponentModel;
using System.Diagnostics;
using I2MS2.Library;
using WebApi.Models;
using I2MS2.UserControls;
using System.Windows.Controls.Primitives;
using System.Collections;

namespace I2MS2.Windows
{
    /// <summary>
    /// RackView.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public class ICViewVM : INotifyPropertyChanged
    {
        #region ICViewVM 정의
        public int asset_id { get; set; }
        public string asset_name { get; set; }
        public int pp_no { get; set; }
        public string figure_type { get; set; }
        public string figure_type_string { get; set; }
        public string media_type { get; set; }
        public string media_type_string { get; set; }
        public string shield_type { get; set; }
        public string shield_type_string { get; set; }
        public string config_type { get; set; }
        public string config_type_string { get; set; }
        public string connect_status { get; set; }
        public string connect_status_string { get; set; }
        public int catalog_id { get; set; }

        public bool force_changed
        {
            get { return true; }
            set { NotifyPropertyChanged(""); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }

    public partial class ICView : Window
    {
        List<ICViewVM> _list_ic_vm = new List<ICViewVM>();

        int _ic_asset_id = 0;

        public ICView(int ic_asset_id)
        {
            _ic_asset_id = ic_asset_id;
            InitializeComponent();
            dispFirst();
            initData();
        }

        private void initData()
        {   
            //var list = from pp in g.ic_ipp_config_list.Where(p => p.ic_asset_id == _ic_asset_id) 
            //         join pp2 in g.ipp_connect_status_list.Where(p => p.ic_asset_id == _ic_asset_id) on pp.ipp_asset_id equals pp2.ipp_asset_id
            //         join a in g.asset_list on pp.ipp_asset_id equals a.asset_id
            //         join c in g.catalog_list on a.catalog_id equals c.catalog_id
            //         select new ICViewVM{
            //               asset_id = pp.ipp_asset_id ?? 0,
            //               asset_name  = a.asset_name,
            //               pp_no = pp.ipp_connect_no,
            //               media_type = c.pp_media_type,
            //               media_type_string = CatalogType.getMediaTypeName(c.pp_media_type),
            //               figure_type = c.pp_figure_type,
            //               figure_type_string = CatalogType.getFigureTypeName(c.pp_figure_type),
            //               shield_type = c.pp_utp_shield_type,
            //               shield_type_string = CatalogType.getShieldTypeName(c.pp_figure_type),
            //               config_type = c.pp_config_type,
            //               config_type_string = CatalogType.getConfigTypeNameLong(c.pp_config_type),
            //               connect_status = pp2.connect_status,
            //               connect_status_string = CatalogType.getConnectStatusName(pp2.connect_status),
            //               catalog_id = c.catalog_id,
            //         };

            var list = from pp in g.ic_ipp_config_list
                       join pp2 in g.ipp_connect_status_list on pp.ipp_asset_id equals pp2.ipp_asset_id
                       join a in g.asset_list on pp.ipp_asset_id equals a.asset_id
                       join c in g.catalog_list on a.catalog_id equals c.catalog_id
                       where (pp.ic_asset_id == _ic_asset_id) 
                       select new ICViewVM
                       {
                           asset_id = pp.ipp_asset_id ?? 0,
                           asset_name = a.asset_name,
                           pp_no = pp.ipp_connect_no,
                           media_type = c.pp_media_type,
                           media_type_string = CatalogType.getMediaTypeName(c.pp_media_type),
                           figure_type = c.pp_figure_type,
                           figure_type_string = CatalogType.getFigureTypeName(c.pp_figure_type),
                           shield_type = c.pp_utp_shield_type,
                           shield_type_string = CatalogType.getShieldTypeName(c.pp_utp_shield_type),
                           config_type = c.pp_config_type,
                           config_type_string = CatalogType.getConfigTypeNameLong(c.pp_config_type),
                           connect_status = pp2.connect_status == "-" ? ((pp.ipp_asset_id ?? 0) > 0 ? "A" : "-") : pp2.connect_status,
                           connect_status_string = CatalogType.getConnectStatusName(pp2.connect_status),
                           catalog_id = c.catalog_id,
                       };


            _list_ic_vm = list.ToList();
            int cnt = _list_ic_vm.Count();
            if (cnt < 0)
                return;

            // pp 최대 수를 알아온다.
            int max = 1;
            var aa = g.asset_list.Find(p => p.asset_id == _ic_asset_id);
            if (aa != null)
            {
                var c = g.catalog_list.Find(p => p.catalog_id == aa.catalog_id);
                max = c != null ? (c.ic_num_of_max_pp ?? 1) : 1;
            }

            int pp_no = 1;
            int idx = 0;
            ICViewVM vm2 = null;

            while (pp_no <= max)
            {
                // 리스트에 레코드가 없는 경우 추가.
                if (idx >= cnt)
                {
                    vm2 = newVM(pp_no++);
                    _list_ic_vm.Add(vm2);
                    cnt++;
                }
                else
                {
                    vm2 = _list_ic_vm[idx];
                    if (vm2.pp_no == pp_no)
                    {
                        pp_no++;
                    }
                    else
                    {
                        vm2 = newVM(pp_no++);
                        _list_ic_vm.Insert(idx, vm2);
                        cnt++;
                    }
                }
                idx++;
            }

            _lvBrief.ItemsSource = _list_ic_vm;
            _lvDetail.ItemsSource = _list_ic_vm;
        }

        private void dispFirst()
        {
            var a = g.asset_list.Find(p => p.asset_id == _ic_asset_id);
            if (a == null)
                return;

            var c = g.catalog_list.Find(p => p.catalog_id == a.catalog_id);
            if (c == null)
                return;

            _txtAssetName.Text = a.asset_name;

            _txtCatalogName.Text = c.catalog_name;
        }

        private ICViewVM newVM(int pp_no)
        {
            ICViewVM vm = new ICViewVM();

            vm.pp_no = pp_no;
            return vm;
        }
    }
}
                                                      