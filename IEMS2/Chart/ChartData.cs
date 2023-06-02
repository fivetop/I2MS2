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
        public double ivalue { get; set; }         // 현재
        public int imax { get; set; }           // 상한    
        public int imin { get; set; }           // 하한 
        public int icmax { get; set; }          // 컬러 상한     
        public int icmin { get; set; }          // 컬러 하한 
    }

    // 전력, 전압1, 전류2, 역률3, 온도4 , 습도5, 도어6, 스모크7, 전력량 8, 
    // 월간 누적 사용량9, 년간 누적 사용량 10, 월간 목표량 11, 년간 목표량 12
    // 현재, 최대, 최소 처리 
    // soluwin
    public class EnvData2
    {
        public string category { get; set; }
        public string category1 { get; set; }
        public string category2 { get; set; }
        public string category3 { get; set; }
        public string category4 { get; set; }
        public string category5 { get; set; }
        public string category6 { get; set; }
        public string category7 { get; set; }
        public string category8 { get; set; }
        public string category9 { get; set; }
        public string category10 { get; set; }
        public string category11 { get; set; }
        public string category12 { get; set; }
        public string category13 { get; set; }
        public string category14 { get; set; }
        public string category15 { get; set; }
        public string category16 { get; set; }
        public string category17 { get; set; }
        public string category18 { get; set; }
        public string category19 { get; set; }
        public string category20 { get; set; }
        public double ivalue { get; set; }         // 현재
        public double ivalue1 { get; set; }         // 현재
        public double ivalue2 { get; set; }         // 현재
        public double ivalue3 { get; set; }         // 현재
        public double ivalue4 { get; set; }         // 현재
        public double ivalue5 { get; set; }         // 현재
        public double ivalue6 { get; set; }         // 현재
        public double ivalue7 { get; set; }         // 현재
        public double ivalue8 { get; set; }         // 현재
        public double ivalue9 { get; set; }         // 현재
        public double ivalue10 { get; set; }         // 현재
        public double ivalue11 { get; set; }         // 현재
        public double ivalue12 { get; set; }         // 현재
        public double ivalue13 { get; set; }         // 현재
        public double ivalue14 { get; set; }         // 현재
        public double ivalue15 { get; set; }         // 현재
        public double ivalue16 { get; set; }         // 현재
        public double ivalue17 { get; set; }         // 현재
        public double ivalue18 { get; set; }         // 현재
        public double ivalue19 { get; set; }         // 현재
        public double ivalue20 { get; set; }         // 현재
        public int imax { get; set; }           // 상한    
        public int imax1 { get; set; }           // 상한    
        public int imax2 { get; set; }           // 상한    
        public int imax3 { get; set; }           // 상한    
        public int imax4 { get; set; }           // 상한    
        public int imax5 { get; set; }           // 상한    
        public int imax6 { get; set; }           // 상한    
        public int imax7 { get; set; }           // 상한    
        public int imax8 { get; set; }           // 상한    
        public int imax9 { get; set; }           // 상한    
        public int imax10 { get; set; }           // 상한    
        public int imin { get; set; }           // 하한 
        public int imin1 { get; set; }           // 하한 
        public int imin2 { get; set; }           // 하한 
        public int imin3 { get; set; }           // 하한 
        public int imin4 { get; set; }           // 하한 
        public int imin5 { get; set; }           // 하한 
        public int imin6 { get; set; }           // 하한 
        public int imin7 { get; set; }           // 하한 
        public int imin8 { get; set; }           // 하한 
        public int imin9 { get; set; }           // 하한 
        public int imin10 { get; set; }           // 하한 
    }

    // 전력1, 전압2, 전류3, 역률4, 온도5 , 습도6, 도어7, 스모크8, 전력량 9 
    public class StateEnv2
    {
        public string category1 { get; set; }
        public string category2 { get; set; }
        public string category3 { get; set; }
        public string category4 { get; set; }
        public string category5 { get; set; }
        public string category6 { get; set; }
        public string category7 { get; set; }
        public string category8 { get; set; }
        public string category9 { get; set; }
        public double value1 { get; set; }          
        public double value2 { get; set; }         
        public double value3 { get; set; }         
        public double value4 { get; set; }         
        public double value5 { get; set; }         
        public double value6 { get; set; }         
        public double value7 { get; set; }         
        public double value8 { get; set; }         
        public double value9 { get; set; }         
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
