using I2MS2.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace I2MS2
{
    // 시스템 설정 처리 
    public class ConfigData
    {
        public int lang_id;
        public string host_string;
        public int _dashboard_time = 60;       // 해당 판넬의 위치 처리 1,2,....
        public string _gs_log = "";
#if GS_DEL
        public int _dcim = 0;       // 해당 판넬의 위치 처리 1,2,....
        public List<int[]> _asset_tree;
        public int _ratio = 4;       // 해당 판넬의 위치 처리 1,2,....
        public List<int> _dashboard_view;           // 해당 판넬의 보이기 처리 1/0
        public List<int> _dashboard_position;       // 해당 판넬의 위치 처리 1,2,....
#endif
    }

    // XNL data 를 읽어와서 메모리 저장 
    // 파일이 없을 경우 생성 처리 
    public class Config
    {
        private static List<ConfigData> _config;
        private const string _config_file_name = "i2ms2_config.xml";

        private static readonly XmlSerializer serializer = new XmlSerializer(typeof(List<ConfigData>), new XmlRootAttribute("Config"));

        public Config()
        {
            _config = new List<ConfigData>();

            //serializer = new XmlSerializer(_config.GetType(), new XmlRootAttribute("Config"));

            if (File.Exists(_config_file_name))
            {
                StreamReader r = new StreamReader(_config_file_name);
                _config = serializer.Deserialize(r) as List<ConfigData>;
                r.Close();
            }
            else
            {
                _config.Add(new ConfigData
                {
                    lang_id = g.lang_id,
                    host_string = g.host_string,
                    _dashboard_time = g._dashboard_time,
                    _gs_log = g._gs_log,
#if GS_DEL
                    _dcim = g._dcim,
                    _asset_tree = g._asset_tree,
                    _ratio = g._ratio,
                    _dashboard_view = g._dashboard_view,
                    _dashboard_position = g._dashboard_position,
#endif
                });

                using (StreamWriter w = new StreamWriter(_config_file_name))
                {
                    serializer.Serialize(w, _config);
                    w.Close();
                }
            }
        }

        // 읽어온 config.xml을 전역 변수에 저장 
        public bool start()
        {
            if (_config == null)
                return false;
            if (_config.Count == 0)
                return false;
            var n = _config[0];
            g.lang_id = 1080001; //  n.lang_id; // GS_DEL
            g.host_string = n.host_string;
            g._dashboard_time = n._dashboard_time;
            g._gs_log = n._gs_log;

#if GS_DEL
            g._dcim = n._dcim;
            g._asset_tree = n._asset_tree;
            g._ratio = n._ratio;
            g._dashboard_view = n._dashboard_view;
            g._dashboard_position = n._dashboard_position;
#endif

            return true;
        }

        public bool end()
        {
            if (_config == null)
                return false;
            if (_config.Count == 0)
                return false;
            var n = _config[0];
            // n.lang_id = 1080001; // g.lang_id;  // GS_DEL
            n.host_string = g.host_string;
            n._dashboard_time = g._dashboard_time;
            n._gs_log = g._gs_log;
#if GS_DEL
            n._dcim = g._dcim;
            n._ratio = g._ratio;
            n._dashboard_view = g._dashboard_view;
            n._dashboard_position = g._dashboard_position;
#endif
            using (StreamWriter w = new StreamWriter(_config_file_name))
            {
                serializer.Serialize(w, _config);
                w.Close();
            }

            return true;
        }
    }
}
