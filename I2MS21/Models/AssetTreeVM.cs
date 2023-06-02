using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I2MS2.Library;
using System.Windows;


namespace I2MS2.Models
{
    public class AssetTreeVM : INotifyPropertyChanged
    {
        public AssetTreeVM()
        {
            child_list = new List<AssetTreeVM>();
        }

        public AssetTreeVM _parant_ast_vm;

        public Nullable<int> asset_id { get; set; }
        public int location_id { get; set; }
        public int asset_tree_id { get; set; }
        public string disp_name { get; set; }
        public int disp_level { get; set; }
        public int disp_order { get; set; }
        public string image_file_path { get; set; }
        public Visibility is_expander_visible { get; set; }
        public AssetTreeType type { get; set; }                 // 카타로그 타입과 동일 , 로케이션인 경우 카다로그 없음 
        public int type_id { get; set; }                        // 해당 아이디 
        public int port_no { get; set; }
        public Visibility check_view { get; set; }              // 특별한 경우 룸, 랙 레이아웃에서 사용 
        public int child_alarm_cnt { get; set; }                // 숫자 써주기 
        public Visibility is_child_alarm_visible { get; set; }  // 알람카운트 보여줄지 말지 결정 
        public Boolean on_alarm { get; set; }                   // 알람이 된경우 아이콘 변경 
        public Boolean is_expanded { get; set; }                // 확장이 되있는가??
        public Visibility select_view { get; set; }             // 환경에서 사용 , 온도, 습도, 전력에 대한 랙색상 변경     
        public Nullable<int> favorite_tree_id { get; set; }     // 즐겨찾기 아이디 
        public DateTime? last_activated { get; set; }           // 장기 미접속 단말리스트에서 표시

        public List<AssetTreeVM> child_list { get; set; }       //    

        public AssetTreeVM parant_ast_vm {
            get { return _parant_ast_vm; }
            set { _parant_ast_vm = value; }
        }

        //public List<int> getNode()
        //{
        //    AssetTreeVM p_ast_vm = _parant_ast_vm;

        //    List<int> node_list = new List<int>();
        //    node_list.Add(asset_tree_id);

        //    if (p_ast_vm == null)
        //        return null;


        //    node_list.Add(p_ast_vm.asset_tree_id);
        //    while (true)
        //    {
        //        p_ast_vm = p_ast_vm.parant_ast_vm;
        //        if (p_ast_vm == null)
        //            break;

        //        node_list.Add(p_ast_vm.asset_tree_id);
        //    }

        //    return node_list;
        //}

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
    }

}
