using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;
using System.ComponentModel;
using WebApi.Models;

namespace I2MS2.Models
{
    public class WorkCell : INotifyPropertyChanged
    {
        // 셀 초기화시 만들어져서 변동이 필요없는 정보들
        public int idx { get; set; }
        public int row_no { get; set; }                     // 0부터 셀의 y축을 표현  
        public int col_no { get; set; }                     // 0부터 셀의 x축을 표현  

        // asset + catalog + location에 대한 기본 정보
        public string template_type { get; set; }
        public int asset_port_link_id { get; set; }
        public int asset_id { get; set; }
        public int port_no { get; set; }                    // 포트번호(Based 1 이상)
        public string asset_name { get; set; }
        public string ca_disp_name { get; set; }            // 케이블표시명
        public int catalog_id { get; set; }
        public int catalog_group_id { get; set; }
        public string catalog_name { get; set; }
        public int location_id { get; set; }
        public string location_name { get; set; }
        public int link_80_image_id { get; set; }
        public string link_80_image_name { get; set; }      // 링크 다이어그램 이미지 화일 명: ex) l2_sw_64.png
        public Color ca_disp_color_rgb { get; set; }        // 케이블을 표시할 때 사용할 컬러로 DB에서 읽어온다.

        // asset_port_link 에서 읽어온 연결 정보
        public int front_asset_id { get; set; }             // front와 연결된 자산 ID
        public int front_port_no { get; set; }              // front와 연결된 자산의 포트 번호
        public string front_plug_side { get; set; }         // front와 연결된 자산의 방향(Front or Rear)
        public int front_cable_catalog_id { get; set; }     // front와 연결된 자산과의 사이에 사용하는 케이블 ID
        // public bool is_front_plug { get; set; }

        public int rear_asset_id { get; set; }
        public int rear_port_no { get; set; }
        public string rear_plug_side { get; set; }
        public int rear_cable_catalog_id { get; set; }
        // public bool is_rear_plug { get; set; }

        // asset_ipp_port_link에서 읽어온 패치 상태 정보
        public int remote_ic_asset_id { get; set; }
        public int remote_pp_asset_id { get; set; }
        public int remote_port_no { get; set; }
        public ePortStatus plug_status { get; set; }        // 패치 코드 연결 상태: 1=plugged, 2=linked, 0=unplugged
        public ePortStatus left_plug_status { get; set; }   // 케이블셀에서만 사용하는 정보로 plug_status와 동일
        public ePortStatus right_plug_status { get; set; }  // 케이블셀에서만 사용하는 정보로 plug_status와 동일
        public ePortStatus front_plug_status { get; set; }  
        public ePortStatus rear_plug_status { get; set; }   

        public eAlarmStatus alarm_status { get; set; }      // 1=PluggedAlarm, 2=UnpluggedAlarm, 0=none
        public eWorkStatus wo_status { get; set; }          // 1=PluggedWork, 2=UnpluggedWork, 0=None
        public eTraceStatus trace_status { get; set; }      // 1=Enabled, 0=Disabled
        public Visibility left_alarm_visible { get; set; }
        public Visibility right_alarm_visible { get; set; }
        public Visibility left_wo_visible { get; set; }
        public Visibility right_wo_visible { get; set; }

        public bool is_left_front { get; set; }
        //public bool is_right_front { get; set; }

        // 케이블 드로잉에 사용하려고 구성된 정보
        public Color left_ca_disp_color_rgb { get; set; }       // 표시될 경우 DB에서 지정한 케이블 색상
        public Color right_ca_disp_color_rgb { get; set; }      // 표시될 경우 DB에서 지정한 케이블 색상
        public Color front_ca_disp_color_rgb { get; set; }      // 케이블을 표시할 때 사용할 컬러로 DB에서 읽어온다.
        public Color rear_ca_disp_color_rgb { get; set; }       // 케이블을 표시할 때 사용할 컬러로 DB에서 읽어온다.

        public bool is_ins_mark { get; set; }                   // 편집시 추가된 자산 또는 케이블의 경우 true (저장 시 이 마크가 있는 것만 저장)
        public bool is_del_mark { get; set; }                   // 편집시 삭제된 자산 또는 케이블의 경우 true (저장 시 이 마크가 있는 것만 저장)
        public bool is_wo_mark { get; set; }                    // 편집시 작업지시 요청한 케이블의 경우 true (작업 지시 시 이 마크가 있는 것만 처리)

        public bool selected_cell { get; set; }                 // Copy를 하기 위해서 사용
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
