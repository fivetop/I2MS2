using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace I2MS2.Library
{
    // 레지스트리 처리 
    public class Reg
    {

        #region 레지스트리 기본 메서드
        public static string get_value(RegistryKey key, string value)
        {
            try
            {
                Object val = key.GetValue(value);
                if (val == null)
                    return "";
                string r = (string)val;
                return r;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static bool set_value(RegistryKey reg, string key, string value)
        {
            try
            {
                reg.SetValue(key, value);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public static RegistryKey open_key(RegistryKey key, string sub_key)
        {
            RegistryKey rr = null;
            try
            {
                rr = key.OpenSubKey(sub_key, true);
            }
            catch (Exception)
            {
                return null;
            }
            return rr;
        }

        public static RegistryKey create_key(RegistryKey key, string sub_key)
        {
            RegistryKey rr = null;
            try
            {
                rr = key.CreateSubKey(sub_key);
            }
            catch (Exception)
            {
                return null;
            }
            return rr;
        }
        #endregion

        #region 로긴 화면에서 호출하는 메서드
        public static string get_saved_user_id()
        {
            try
            {
                RegistryKey pRegKey = Registry.LocalMachine;
                pRegKey = pRegKey.OpenSubKey("Software\\LSCable\\SimpleWin 2.0");
                Object val = pRegKey.GetValue("userid");
                if (val == null)
                    return "";
                return (string)val;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string get_saved_password()
        {
            try
            {
                RegistryKey pRegKey = Registry.LocalMachine;
                pRegKey = pRegKey.OpenSubKey("Software\\LSCable\\SimpleWin 2.0");
                Object val = pRegKey.GetValue("password");
                if (val == null)
                    return "";
                return (string)val;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static bool save_user_id_and_password(string user_id, string password)
        {
            RegistryKey pLocalMachine = Registry.LocalMachine;
            RegistryKey pSoftware;
            RegistryKey pLSCable;
            RegistryKey pSimpleWin;

            pSoftware = open_key(pLocalMachine, "SOFTWARE\\");
            if (pSoftware == null)
            {
                //MessageBox.Show("Can't access registry key. please check users access right");
                return false;
            }

            pLSCable = open_key(pSoftware, "LSCable");
            if (pLSCable == null)
            {
                pLSCable = create_key(pSoftware, "LSCable");
                if (pLSCable == null)
                    return false;
            }

            pSimpleWin = open_key(pLSCable, "SimpleWin 2.0");
            if (pSimpleWin == null)
            {
                pSimpleWin = create_key(pLSCable, "SimpleWin 2.0");
                if (pSimpleWin == null)
                    return false;
            }


            set_value(pSimpleWin, "userid", user_id);
            set_value(pSimpleWin, "password", password);

            return true;
        }
        #endregion

        #region 좌측 트리메뉴 위치
        // 트리의 펼친 위치를 기억: key=asset_tree, favorite_tree, simplewin_tree 3종류

        // <갯수>, <asset_id>,<location_id>,<port_no>, ...
        public static bool save_tree(string key, List<int[]> list)
        {
            RegistryKey pLocalMachine = Registry.LocalMachine;
            RegistryKey pSimpleWin;

            pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
            string data = string.Format("{0}, ", list.Count);
            foreach(var node in list)
            {
                data = data + string.Format("{0},{1},{2}, ", node[0], node[1], node[2]);
            }
            set_value(pSimpleWin, key, data);

            return true;
        }

        public static List<int[]> get_asset_tree(string key)
        {
            List<int[]> list = new List<int[]>();
            string data = "";

            try
            {
                RegistryKey pLocalMachine = Registry.LocalMachine;
                RegistryKey pSimpleWin;
                pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
                Object val = pSimpleWin.GetValue(key);
                if (val == null)
                    return list;
                data = (string) val;
            }
            catch (Exception)
            {
                return list;
            }

            try
            {
                int pos1 = data.IndexOf(",");
                if (pos1 == 0)
                    return list;
                int cnt = Etc.get_int(data.Substring(0, pos1));
                if (cnt == 0)
                    return list;

                int i = 0;
                int pos2 = 0;
                int pos3 = 0;
                int pos0 = pos1 + 1;
                for (i = 0; i < cnt; i++)
                {
                    int[] value = new int[3];
                    pos1 = data.Substring(pos0).IndexOf(",");
                    if (pos1 == -1) 
                        return list;
                    value[0] = Etc.get_int(data.Substring(pos0, pos1));
                    pos1 = pos0 + pos1 + 1;

                    pos2 = data.Substring(pos1).IndexOf(",");
                    if (pos2 == -1)
                        return list;
                    value[1] = Etc.get_int(data.Substring(pos1, pos2));
                    pos2 = pos1 + pos2 + 1;

                    pos3 = data.Substring(pos2).IndexOf(",");
                    if (pos3 == -1)
                        return list;
                    value[2] = Etc.get_int(data.Substring(pos2, pos3));
                    pos0 = pos2 + pos3 + 1;
                    list.Add(value);
                }
            }
            catch(Exception)
            {
                return list;
            }

            return list;
        }
        #endregion

        #region 자산이 3D에서 표시되는 사이즈
        public static bool save_view_option(int ratio)
        {
            RegistryKey pLocalMachine = Registry.LocalMachine;
            RegistryKey pSimpleWin;

            pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
            string data = string.Format("{0}, ", ratio);
            
            set_value(pSimpleWin, "view_option", data);
            return true;
        }

        public static int get_view_option()
        {
            String data;
            try
            {
                RegistryKey pLocalMachine = Registry.LocalMachine;
                RegistryKey pSimpleWin;
                pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
                Object val = pSimpleWin.GetValue("view_option");
                if (val == null)
                    return -1;
                data = (String)val;
                
            }
            catch (Exception){return -1;}

            try
            {
                int pos1 = data.IndexOf(",");
                if (pos1 == 0) return -1;
                
                int cnt = Etc.get_int(data.Substring(0, pos1));
                return cnt;

            }
            catch (Exception)
            {
                return -1;
            }

            return -1;

        }


        #endregion


        #region 배선 관리
        // 배선 관리에서 현재 편집중인 자산을 기억
        // <갯수>, <ipp_asset_id>, ...
        public static bool save_link_diagram(List<int> list)
        {
            RegistryKey pLocalMachine = Registry.LocalMachine;
            RegistryKey pSimpleWin;

            pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
            string data = string.Format("{0}, ", list.Count);
            foreach (int node in list)
            {
                data = data + string.Format("{0}, ", node);
            }
            set_value(pSimpleWin, "link_diagram", data);

            return true;
        }

        public static List<int> get_link_diagram()
        {
            List<int> list = new List<int>();
            string data = "";

            try
            {
                RegistryKey pLocalMachine = Registry.LocalMachine;
                RegistryKey pSimpleWin;
                pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
                Object val = pSimpleWin.GetValue("link_diagram");
                if (val == null)
                    return null;
                data = (string)val;
            }
            catch (Exception)
            {
                return null;
            }

            try
            {
                int pos1 = data.IndexOf(",");
                if (pos1 == 0)
                    return list;
                int cnt = Etc.get_int(data.Substring(0, pos1));
                if (cnt == 0)
                    return list;

                int i = 0;
                int pos0 = pos1 + 1;
                for (i = 0; i < cnt; i++)
                {
                    int value = 0;
                    pos1 = data.Substring(pos0).IndexOf(",");
                    if (pos1 == -1)
                        return list;
                    value = Etc.get_int(data.Substring(pos0, pos1));
                    pos0 = pos0 + pos1 + 1;

                    list.Add(value);
                }
            }
            catch (Exception)
            {
                return list;
            }

            return list;
        }
        #endregion

        #region 댓쉬보도
        // 배선 관리에서 현재 편집중인 자산을 기억
        // <갯수>, <ipp_asset_id>, ...
        public static bool save_dashboard(List<int> list, string key)
        {
            RegistryKey pLocalMachine = Registry.LocalMachine;
            RegistryKey pSimpleWin;

            pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
            string data = string.Format("{0}, ", list.Count);
            foreach (int node in list)
            {
                data = data + string.Format("{0}, ", node);
            }
            set_value(pSimpleWin, key, data);

            return true;
        }

        public static List<int> get_dashboard(string key)
        {
            List<int> list = new List<int>();
            string data = "";

            try
            {
                RegistryKey pLocalMachine = Registry.LocalMachine;
                RegistryKey pSimpleWin;
                pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
                Object val = pSimpleWin.GetValue(key);
                if (val == null)
                    return null;
                data = (string)val;
            }
            catch (Exception)
            {
                return null;
            }

            try
            {
                int pos1 = data.IndexOf(",");
                if (pos1 == 0)
                    return list;
                int cnt = Etc.get_int(data.Substring(0, pos1));
                if (cnt == 0)
                    return list;

                int i = 0;
                int pos0 = pos1 + 1;
                for (i = 0; i < cnt; i++)
                {
                    int value = 0;
                    pos1 = data.Substring(pos0).IndexOf(",");
                    if (pos1 == -1)
                        return list;
                    value = Etc.get_int(data.Substring(pos0, pos1));
                    pos0 = pos0 + pos1 + 1;

                    list.Add(value);
                }
            }
            catch (Exception)
            {
                return list;
            }

            return list;
        }
        #endregion

        #region 언어코드(lang_id)
        public static bool save_lang_id(int lang_id)
        {
            RegistryKey pLocalMachine = Registry.LocalMachine;
            RegistryKey pSimpleWin;

            pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
            string data = string.Format("{0}", lang_id);
            set_value(pSimpleWin, "lang_id", data);
            return true;
        }

        public static int get_lang_id()
        {
            List<int> list = new List<int>();
            string data = "";

            try
            {
                RegistryKey pLocalMachine = Registry.LocalMachine;
                RegistryKey pSimpleWin;
                pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
                Object val = pSimpleWin.GetValue("lang_id");
                if (val == null)
                    return 0;
                data = (string)val;
            }
            catch (Exception)
            {
                return 0;
            }

            int lang_id = Etc.get_int(data);
            return lang_id;
        }
        #endregion

        #region DCIM / CMS
        public static bool save_dcim(int dcim)
        {
            RegistryKey pLocalMachine = Registry.LocalMachine;
            RegistryKey pSimpleWin;

            pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
            string data = string.Format("{0}", dcim);
            set_value(pSimpleWin, "dcim", data);
            return true;
        }

        public static int get_dcim()
        {
            List<int> list = new List<int>();
            string data = "";

            try
            {
                RegistryKey pLocalMachine = Registry.LocalMachine;
                RegistryKey pSimpleWin;
                pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
                Object val = pSimpleWin.GetValue("dcim");
                if (val == null)
                    return 0;
                data = (string)val;
            }
            catch (Exception)
            {
                return 0;
            }

            int dcim = Etc.get_int(data);
            return dcim;
        }
        #endregion

        #region 서버 도메인명(or IP)
        public static string get_saved_server_domain()
        {
            try
            {
                RegistryKey pRegKey = Registry.LocalMachine;
                pRegKey = pRegKey.OpenSubKey("Software\\LSCable\\SimpleWin 2.0");
                Object val = pRegKey.GetValue("server_domain");
                if (val == null)
                    return "";
                return (string)val;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static bool save_server_domain(string server_domain)
        {
            RegistryKey pLocalMachine = Registry.LocalMachine;
            RegistryKey pSoftware;
            RegistryKey pLSCable;
            RegistryKey pSimpleWin;

            pSoftware = open_key(pLocalMachine, "SOFTWARE\\");
            if (pSoftware == null)
            {
                //MessageBox.Show("Can't access registry key. please check users access right");
                return false;
            }

            pLSCable = open_key(pSoftware, "LSCable");
            if (pLSCable == null)
            {
                pLSCable = create_key(pSoftware, "LSCable");
                if (pLSCable == null)
                    return false;
            }

            pSimpleWin = open_key(pLSCable, "SimpleWin 2.0");
            if (pSimpleWin == null)
            {
                pSimpleWin = create_key(pLSCable, "SimpleWin 2.0");
                if (pSimpleWin == null)
                    return false;
            }


            set_value(pSimpleWin, "server_domain", server_domain);

            return true;
        }
        #endregion

        #region 이메일 , SMS 처리 
        public static int get_int(string key)
        {
            String data;
            try
            {
                RegistryKey pLocalMachine = Registry.LocalMachine;
                RegistryKey pSimpleWin;
                pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
                Object val = pSimpleWin.GetValue(key);
                if (val == null)
                    return -1;
                data = (String)val;

            }
            catch (Exception) { return -1; }
            int cnt = Etc.get_int(data);
            return cnt;
        }

        public static string get_string(string key)
        {
            try
            {
                RegistryKey pRegKey = Registry.LocalMachine;
                pRegKey = pRegKey.OpenSubKey("Software\\LSCable\\SimpleWin 2.0");
                Object val = pRegKey.GetValue(key);
                if (val == null)
                    return "";
                return (string)val;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static bool set_int(string key, int v1)
        {
            RegistryKey pLocalMachine = Registry.LocalMachine;
            RegistryKey pSimpleWin;

            pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
            string data = string.Format("{0}", v1);
            set_value(pSimpleWin, key, data);
            return true;
        }

        public static bool set_string(string key, string v1)
        {
            RegistryKey pLocalMachine = Registry.LocalMachine;
            RegistryKey pSimpleWin;

            pSimpleWin = open_key(pLocalMachine, "SOFTWARE\\LSCable\\SimpleWin 2.0");
            string data = v1;
            set_value(pSimpleWin, key, data);
            return true;
        }
        #endregion
    }



/*
 *  레지스트리 에러시 교체 하기 
    
        static Microsoft.Win32.RegistryKey pSimpleWin;

        public static int get_int(string key)
        {
            String data;
            try
            {
                pSimpleWin = Registry.LocalMachine.OpenSubKey("SOFTWARE\\LSCable\\SimpleWin 2.0", RegistryKeyPermissionCheck.ReadWriteSubTree);
                Object val = pSimpleWin.GetValue(key);
                if (val == null)
                {
                    pSimpleWin.Close();
                    return -1;
                }
                data = (String)val;
                pSimpleWin.Close();
            }
            catch (Exception) 
            {
                pSimpleWin.Close();
                return -1; 
            }
            int value = 0;
            try
            {
                int.TryParse(data, out value);
            }
            catch (Exception) { }
            return value;
        }

        public static string get_string(string key)
        {
            try
            {
                pSimpleWin = Registry.LocalMachine.OpenSubKey("SOFTWARE\\LSCable\\SimpleWin 2.0", RegistryKeyPermissionCheck.ReadWriteSubTree);
                Object val = pSimpleWin.GetValue(key);
                if (val == null)
                {
                    pSimpleWin.Close();
                    return "";
                }
                return (string)val;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static bool set_int(string key, object v1)
        {
            try
            {
                pSimpleWin = Registry.LocalMachine.OpenSubKey("SOFTWARE\\LSCable\\SimpleWin 2.0", RegistryKeyPermissionCheck.ReadWriteSubTree);
                if (pSimpleWin == null)
                    pSimpleWin = Registry.LocalMachine.CreateSubKey("SOFTWARE\\LSCable\\SimpleWin 2.0", RegistryKeyPermissionCheck.ReadWriteSubTree);
                string data = string.Format("{0}", v1.ToString());
                pSimpleWin.SetValue(@key, @data);
                pSimpleWin.Close();
            }
            catch (Exception e1)
            {
                Console.WriteLine("{0}{1}", e1.Message, e1.Data);
                pSimpleWin.Close();
                return true;
            }
            return true;
        }

*/
}
