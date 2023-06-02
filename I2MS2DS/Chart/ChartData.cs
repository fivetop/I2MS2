using I2MS2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;


/// <summary>
/// 통계 처리용
/// 공통으로 사용되는 데이터 클래스 기술
/// 주사용 통계 처리와 댓쉬보드
/// </summary>
namespace I2MS2
{
    // 통계 기간 입력 처리용 
    public class DurationDate : PropertyData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DurationDate"/> class.
        /// </summary>
        public DurationDate()
        {
            if (date == 0)
                sdate = "--All Field (default)--";
            else
                sdate = date.ToString();
        }
        public int date { get; set; }
        public string sdate { get; set; }
    }


    /// <summary>
    /// 장치의 종류와 갯수, 이미지 가겨오기 
    /// 이미지 리스트 처리용     
    /// </summary>
    public class SysData
    {
        public SysData() { }

        public SysData(BitmapImage b, string s, int i)
        {
            dataImages = b;
            dataStrings = s;
            datavalue = i;
        }
        public BitmapImage dataImages { get; set; }
        public string dataStrings { get; set; }
        public int datavalue { get; set; }
    }

    // 전체 시스템 현황 통계용
    // 통계 6 시스템 현황 처리에서 사용  
    public class stat_catalog_group_id : INotifyPropertyChanged
    {
        public stat_catalog_group_id()
        {
            this.node_list = new List<stat_catalog_group_id>();
        }
        public List<stat_catalog_group_id> node_list { get; set; }

        public int catalog_group_id { get; set; }
        public string catalog_group_name { get; set; }
        public int DeviceCount { get; set; }
        public int DeviceUsage { get; set; }
        public int DeviceFree { get; set; }        
        public string img_string { get; set; }
        public Nullable<int> link_80_image_id { get; set; }
        public string file_name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }

    /// <summary>
    /// 차트용
    /// </summary>
    public class ChartData
    {
        public string category { get; set; }
        public int value { get; set; }
    }

    /// <summary>
    /// 차트 더블 용 
    /// </summary>
    public class ChartDataDouble
    {
        public string category { get; set; }
        public double value { get; set; }
    }

    // 전력, 온도 , 습도  현재, 최대, 최소 처리 
    public class EnvData
    {
        public string category { get; set; }
        public int ivalue { get; set; }         // 현재
        public int imax { get; set; }           // 상한    
        public int imin { get; set; }           // 하한 
        public int icmax { get; set; }          // 컬러 상한     
        public int icmin { get; set; }          // 컬러 하한 
    }
    // IPP 카운트 출력용, SW 카운트 출력용 , Rack 카운트 출력용 , 터미날 카운트 출력용 
    public class SystemData
    {
        public string Sys1 { get; set; }
        public string Sys2 { get; set; }
        public string Sys3 { get; set; }
        public string Sys4 { get; set; }
    }

    // 데이터 처리 용 
    public class SeriesData
    {
        public string SeriesDisplayName { get; set; }
        public string SeriesDescription { get; set; }
        public ObservableCollection<ChartData> Items { get; set; }
    }

}
