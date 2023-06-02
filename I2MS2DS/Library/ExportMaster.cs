using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using tool = Microsoft.Windows.Controls;
using System.ComponentModel;
using System.Windows;
using I2MS2.Models;
using WebApi.Models;
using I2MS2.Windows;
using System.Threading;
using System.Threading.Tasks;

namespace I2MS2.Library
{
    // 익스포트 
    public class ExportData1
    {
        public int sn;
        public int building_id;
        public string building_name;
        public int floor_id;
        public string floor_name;
        public int room_id;
        public string room_name;
        public int rack_id;
        public string rack_name;
        public int location_id;
        public int asset_id;        // 실제로 엑셀쉬트에서 읽어올때에는 asset_id가 없고, DB테이블에 추가한 후에 얻어온다. 이 정보는 카드형 스위치 등록시 필요하다.
        public string asset_name;
        public Point asset_pos;
        public int slot_no;
        public int catalog_id;
        public string catalog_name;
        public string serial_no;
        public string ip_addr;
        public string sw_vlan;
        public int ic_con_id;
        public int pp_id;
        public List<Point> user_port_pos;
        public string user_port_string;
        public string snmp_get_community;
        public string snmp_set_community;
        public string snmp_version;
        public string snmp_user_id;
        public string snmp_password;
        public string snmp_trap_svr_ip;
        public string remarks;
    }

    public class ExportData2
    {
        public int sn;
        public int building_id;
        public string building_name;
        public int asset_id;
        public string asset_name;
        public int port_no;

        public int front_asset_id;
        public string front_asset_name;
        public int front_port_no;
        public string front_plug_side;                 // 연결된 자산의 방향 Front, Rear
        public int front_cable_catalog_id;
        public string front_cable_catalog_name;

        public int rear_asset_id;
        public string rear_asset_name;
        public int rear_port_no;
        public string rear_plug_side;                 // 연결된 자산의 방향 Front, Rear
        public int rear_cable_catalog_id;
        public string rear_cable_catalog_name;
    }

    public class ExportData3
    {
        public int sn;
        public int catalog_group1_id;
        public string catalog_group1_name;
        public int catalog_group2_id;
        public string catalog_group2_name;
        public int catalog_id;
        public string catalog_name;
    }

    public class ExportMaster
    {

        public string _site_name = "";
        public List<ExportData1> _data1 = new List<ExportData1>();
        public List<ExportData2> _data2 = new List<ExportData2>();
        public List<ExportData3> _data3 = new List<ExportData3>();
        ProgressBarDialog2 _progress_window;
        ProgressBarDialog3 _progress_window2;

        public void export_2_excel()
        {
            make_sheet1();
            make_sheet2();
            make_sheet3();
            bool b = export_2_excel2();
            if (b)
                MessageBox.Show(g.tr_get("C_Completed_Export_Excel"));
            if (_progress_window != null)
                _progress_window.Close();
        }

        public async Task<bool> import_from_excel()
        {
            bool b = await import_from_excel2();
            if (b)
                MessageBox.Show(g.tr_get("C_Completed_Import_Excel"));
            if (_progress_window2 != null)
                _progress_window2.Close();
            return b;
        }

        public async Task<bool> import_from_excel2()
        {
            bool b1 = MessageBox.Show(g.tr_get("M9_Import_Data_1"), g.tr_get("M9_Import_Data_2"),
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (!b1)
                return false;

            bool b2 = import_from_excel3();
            if (!b2)
                return false;

            bool b3 = check1();
            if (!b3)
                return false;

            var req = new request();
            req.DeleteAllAsset(g.selected_site_id);
            var r = (request) await g.webapi.post("request", req, typeof(request));
            if (r == null)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            var b4 = await g.left_tree_handler.delete_all_asset(g.selected_site_id);
            if (!b4)
                return false;

            bool b5 = await update_db1();
            if (!b5)
                return false;

            bool b6 = check2();
            if (!b6)
                return false;

            bool b7 = await update_db2();

            return b7;
        }

        public void make_sheet1()
        {
            string building_name = "";
            string floor_name = "";
            string room_name = "";
            string rack_name = "";

            int site_id = g.selected_site_id;
            var s = g.site_list.Find(p => p.site_id == site_id);
            if (s == null)
                return;
            _site_name = s.site_name;

            var building_list2 = from aa in g.building_list
                                 join bb in g.location_list on aa.building_id equals bb.building_id
                                 where (bb.location_level == 4) && (bb.site_id == site_id)
                                 orderby bb.disp_order
                                 select new { aa.building_id, aa.building_name, aa.building_image_id, aa.remarks };

            int sn = 1;   
            foreach (var b in building_list2)
            {
                int building_id = b.building_id;
                //ed1 = new ExportData1();
                //ed1.building_name = b.building_name;
                //ed1.remarks = b.remarks;
                //_data1.Add(ed1);

                var floor_list2 = from aa in g.floor_list
                                  join bb in g.location_list on aa.floor_id equals bb.floor_id
                                  where (bb.location_level == 5) && (bb.building_id == building_id)
                                  orderby bb.disp_order
                                  select new { aa.floor_id, aa.floor_name, aa.floor_no, aa.drawing_3d_id, aa.remarks };

                foreach (var f in floor_list2)
                {
                    int floor_id = f.floor_id;
                    //ed1 = new ExportData1();
                    //ed1.building_name = b.building_name;
                    //ed1.floor_name = f.floor_name;
                    //ed1.remarks = f.remarks;
                    //_data1.Add(ed1);

                    var room_list2 = from aa in g.room_list
                                     join bb in g.location_list on aa.room_id equals bb.room_id
                                     where (bb.location_level == 6) && (bb.floor_id == floor_id)
                                     orderby bb.disp_order
                                     select new { aa.room_id, aa.room_name, aa.square_x1, aa.square_x2, aa.square_y1, aa.square_y2, aa.remarks, bb.location_id };

                    foreach (var r in room_list2)
                    {
                        int room_id = r.room_id;
                        //ed1 = new ExportData1();
                        building_name = b.building_name;
                        floor_name = f.floor_name;
                        room_name = r.room_name;
                        rack_name = "";
                        //_data1.Add(ed1);
                        int location_id = r.location_id;

                        // 여기서 룸에 속한 자산 출력....
                        add_asset(ref sn, location_id, building_name, floor_name, room_name, rack_name);

                        var rack_list2 = from aa in g.rack_list
                                         join bb in g.location_list on aa.rack_id equals bb.rack_id
                                         where (bb.location_level == 7) && (bb.room_id == room_id)
                                         orderby bb.disp_order
                                         select new { aa.rack_id, aa.rack_name, aa.pos_x, aa.pos_y, aa.rack_catalog_id, aa.remarks, bb.location_id };

                        foreach (var ra in rack_list2)
                        {
                            int rack_id = ra.rack_id;
                            //ed1 = new ExportData1();
                            //building_name = b.building_name;
                            //floor_name = f.floor_name;
                            //room_name = r.room_name;
                            rack_name = ra.rack_name;
                            //var c = g.catalog_list.Find(p => p.catalog_id == ra.rack_catalog_id);
                            //string rack_catalog_name = c != null ? c.catalog_name : "";
                            //ed1.remarks = ra.remarks;
                            //_data1.Add(ed1);
                            location_id = ra.location_id;

                            // 여기서 랙에 속한 자산 출력....
                            add_asset(ref sn, location_id, building_name, floor_name, room_name, rack_name);

                        }
                    }
                }
            }
        }

        public void make_sheet2()
        {
            int site_id = g.selected_site_id;
            var acl = from aa in g.asset_port_link_list
                      join bb in g.asset_list on aa.asset_id equals bb.asset_id
                      join cc in g.location_list on bb.location_id equals cc.location_id
                      where (cc.site_id == site_id) && !CatalogType.is_pc(bb.catalog_id)
                      orderby bb.asset_name, aa.port_no, cc.building_id
                      select new
                      {
                          cc.building_id,
                          aa.asset_id,
                          bb.asset_name,
                          aa.port_no,
                          aa.front_asset_id,
                          aa.front_port_no,
                          aa.front_plug_side,
                          aa.front_cable_catalog_id,
                          aa.rear_asset_id,
                          aa.rear_port_no,
                          aa.rear_plug_side,
                          aa.rear_cable_catalog_id
                      };

            int sn = 1;
            foreach(var node in acl)
            {
                ExportData2 ed2 = new ExportData2();
                ed2.sn = sn++;
                ed2.building_name = Etc.get_building_name(node.building_id ?? 0);
                ed2.asset_id = node.asset_id;
                ed2.asset_name = node.asset_name;
                ed2.port_no = node.port_no;

                ed2.front_asset_id = node.front_asset_id ?? 0;
                ed2.front_asset_name = Etc.get_asset_name(ed2.front_asset_id);
                ed2.front_port_no = node.front_port_no ?? 0;
                ed2.front_cable_catalog_id = node.front_cable_catalog_id ?? 0;
                string front_cable_catalog_name = Etc.get_catalog_name(ed2.front_cable_catalog_id);
                ed2.front_cable_catalog_name = front_cable_catalog_name;
                ed2.front_plug_side = node.front_plug_side;

                ed2.rear_asset_id = node.rear_asset_id ?? 0;
                ed2.rear_asset_name = Etc.get_asset_name(ed2.rear_asset_id);
                ed2.rear_port_no = node.rear_port_no ?? 0;
                ed2.rear_cable_catalog_id = node.rear_cable_catalog_id ?? 0;
                string rear_cable_catalog_name = Etc.get_catalog_name(ed2.rear_cable_catalog_id);
                ed2.rear_cable_catalog_name = rear_cable_catalog_name;
                ed2.rear_plug_side = node.rear_plug_side;
                _data2.Add(ed2);
            }
        }

        public void make_sheet3()
        {
            var catalog_group_list2 = from aa in g.catalog_group_list
                                      orderby aa.disp_order
                                      select new catalog_group() { 
                                       catalog_level = aa.catalog_level, 
                                       catalog_group_id = aa.catalog_group_id, 
                                       catalog_group_name = aa.catalog_group_name
                                      };

            int catalog_group1_id = 0;
            string catalog_group1_name = "";
            int catalog_group2_id = 0;
            string catalog_group2_name = "";
            int catalog_group2_cnt = 0;
            int sn = 1;
            foreach (var cg in catalog_group_list2)
            {
                if (cg.catalog_level == 1)
                {
                    catalog_group1_id = cg.catalog_group_id;
                    catalog_group1_name = cg.catalog_group_name;
                    catalog_group2_cnt = 0;
                }
                else
                {
                    catalog_group2_cnt++;
                    catalog_group2_id = cg.catalog_group_id;
                    catalog_group2_name = cg.catalog_group_name;

                    int catalog_cnt = 0;
                    var catalog_list2 = from aa in g.catalog_list
                                        orderby aa.catalog_name
                                        where aa.catalog_group_id == cg.catalog_group_id
                                        select new catalog()
                                        {
                                            catalog_id = aa.catalog_id,
                                            catalog_name = aa.catalog_name
                                        };
                    foreach (var c in catalog_list2)
                    {
                        ExportData3 ed3 = new ExportData3();
                        catalog_cnt++;
                        if ((catalog_group2_cnt == 1) && (catalog_cnt == 1))
                        {
                            ed3.catalog_group1_id = catalog_group1_id;
                            ed3.catalog_group1_name = catalog_group1_name;
                            ed3.catalog_group2_id = catalog_group2_id;
                            ed3.catalog_group2_name = catalog_group2_name;
                        }
                        else if (catalog_cnt == 1)
                        {
                            ed3.catalog_group2_id = catalog_group2_id;
                            ed3.catalog_group2_name = catalog_group2_name;
                        }
                        ed3.sn = sn++;
                        ed3.catalog_id = c.catalog_id;
                        ed3.catalog_name = c.catalog_name;
                        _data3.Add(ed3);
                    }
                }
            }
        }

        public class temp
        {
            public int asset_id;
            public string asset_name;
            public int catalog_id;
            public string ipv4;
            public int? pos_x;
            public int? pos_y;
            public string serial_no;
            public string remarks;
            public int location_id;
            public string snmp_get_community;
            public string snmp_set_community;
            public string snmp_version;
            public string snmp_v3_user;
            public string snmp_v3_password;
            public string snmp_trap_svr_ip;
            public int? ic_con_id;
            public string sw_vlan;
        }

        private void add_asset(ref int sn, int location_id, string building_name, string floor_name, string room_name, string rack_name)
        {
            var asset_list2 = from aa in g.asset_list.Where(p => (p.location_id == location_id) && !CatalogType.is_pc(p.catalog_id))
                            join bb in g.asset_aux_list on aa.asset_id equals bb.asset_id
                            join cc in g.asset_tree_list on aa.asset_id equals cc.asset_id
                            orderby cc.disp_order
                            select new temp
                            {
                                asset_id = aa.asset_id,
                                asset_name = aa.asset_name,
                                catalog_id = aa.catalog_id,
                                ipv4 = aa.ipv4,
                                pos_x = aa.pos_x,
                                pos_y = aa.pos_y,
                                serial_no = aa.serial_no,
                                remarks = aa.remarks,
                                location_id = aa.location_id,
                                snmp_get_community = bb.snmp_get_community,
                                snmp_set_community = bb.snmp_set_community,
                                snmp_version = bb.snmp_version,
                                snmp_v3_user = bb.snmp_v3_user,
                                snmp_v3_password = bb.snmp_v3_password,
                                snmp_trap_svr_ip = bb.snmp_trap_svr_ip,
                                ic_con_id = bb.ic_con_id,
                                sw_vlan = bb.sw_vlan
                                //sw_max_slots = bb.sw_max_slots
                            };

            foreach (var a in asset_list2)
            {
                int asset_id = a.asset_id;
                var ed1 = new ExportData1();
                ed1.sn = sn++;
                ed1.building_name = building_name;
                ed1.floor_name = floor_name;
                ed1.room_name = room_name;
                ed1.rack_name = rack_name;
                ed1.asset_name = a.asset_name;
                ed1.sw_vlan = a.sw_vlan;
                int catalog_id = a.catalog_id;
                var c2 = g.catalog_list.Find(p => p.catalog_id == catalog_id);
                ed1.catalog_name = c2 != null ? c2.catalog_name : "";
                ed1.ic_con_id = a.ic_con_id ?? 0;
                ed1.location_id = a.location_id;
                if (CatalogType.is_ipp(catalog_id))
                {
                    ed1.ic_con_id = Etc.get_sys_id_by_ipp_asset_id(asset_id);
                    ed1.pp_id = Etc.get_pp_id_by_asset_id(asset_id);
                }
                else if (CatalogType.is_mb(catalog_id) || CatalogType.is_fp(catalog_id))
                {
                    int num_of_ports = c2.num_of_ports;
                    int i = 0;
                    string pos_str = "";
                    ed1.user_port_pos = new List<Point>();
                    for (i = 0; i < num_of_ports; i++)
                    {
                        int port_no = i + 1;
                        Point pos = new Point();
                        var upl = g.user_port_layout_list.Find(p => (p.asset_id == asset_id) && (p.port_no == port_no));
                        if (upl != null)
                        {
                            pos.X = upl.pos_x ?? 0;
                            pos.Y = upl.pos_y ?? 0;
                        }
                        ed1.user_port_pos.Add(pos);
                        if ((i + 1) == num_of_ports)
                            pos_str = pos_str + string.Format("{0},{1}", pos.X, pos.Y);
                        else
                            pos_str = pos_str + string.Format("{0},{1}, ", pos.X, pos.Y);
                    }
                    ed1.user_port_string = pos_str;
                }
                Point pos2 = new Point();
                pos2.X = a.pos_x ?? 0;
                pos2.Y = a.pos_y ?? 0;
                ed1.asset_pos = pos2;

                ed1.slot_no = Etc.get_slot_no_by_asset_id(asset_id);
                ed1.serial_no = a.serial_no;
                ed1.ip_addr = a.ipv4;
                ed1.snmp_get_community = a.snmp_get_community;
                ed1.snmp_set_community = a.snmp_set_community;
                ed1.snmp_version = a.snmp_version;
                ed1.snmp_user_id = a.snmp_v3_user;
                ed1.snmp_password = a.snmp_v3_password;
                ed1.snmp_trap_svr_ip = a.snmp_trap_svr_ip;
                ed1.remarks = a.remarks;
                _data1.Add(ed1);
            }
        }

        
        

        // Excel로 데이터 Export 관련된 함수....
        private bool export_2_excel2()
        {
            Excel.Application oXL;
            try
            {
                oXL = new Excel.Application();
            }
            catch 
            {
                MessageBox.Show("Check the Microsoft Excel program.");
                return false;
            }
            oXL.DisplayAlerts = false;
            Excel._Workbook oWB = oXL.Workbooks.Add(true);

            object missingType = Type.Missing;

            int T_Year = DateTime.Now.Year;
            int T_Month = DateTime.Now.Month;
            int T_Day = DateTime.Now.Day;
            int T_Hour = DateTime.Now.Hour;
            int T_Minute = DateTime.Now.Minute;
            int T_Second = DateTime.Now.Second;

            DateTime dTime = new DateTime(T_Year, T_Month, T_Day, T_Hour, T_Minute, T_Second);
            string time = dTime.ToString("yyMMdd_HHmmss");
            string data = "Export_" + time + ".xls";

            Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();

            saveFileDialog1.AddExtension = true;
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.CreatePrompt = false;
            saveFileDialog1.OverwritePrompt = false;
            saveFileDialog1.FileName = data;
            saveFileDialog1.DefaultExt = "xls";
            saveFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            //saveFileDialog1.InitialDirectory = "";
            saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
            var f = saveFileDialog1.ShowDialog();
            if (f != true)
                return false;

            try
            {               
                string lang_code = g.lang_id == 1080001 ? "ko" : "en";    // 1080001=한글, 영문=1080002

                string file = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + "\\LSCable\\SimpleWIN\\ExcelTemplates\\export_template_" + lang_code;

                oWB = oXL.Workbooks.Open(file, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, 1, 0);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(g.tr_get("C_Error_Save_Excel") + ", {0}", e.Message));
                return false;
            }

            _progress_window = new ProgressBarDialog2();
            _progress_window.Owner = App.Current.MainWindow;
            _progress_window.Show();

            export_sheet1(oWB);
            export_sheet2(oWB);
            export_sheet3(oWB);

            _progress_window.Close();

            bool success_flag = false;
            try
            {
                // Save Excel File
                oWB.SaveAs(saveFileDialog1.FileName, Excel.XlFileFormat.xlWorkbookNormal, missingType, missingType,
                missingType, missingType, Excel.XlSaveAsAccessMode.xlNoChange,
                missingType, missingType, missingType, missingType, missingType);
                success_flag = true;
            }
            catch (Exception e)
            {
            }
            finally
            {
                oWB.Close(false, missingType, missingType);
                oXL.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oXL);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(oXL);
                oWB = null;
                oXL = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            return success_flag;
        }

        private void export_sheet1(Excel._Workbook oWB)
        {
            Excel._Worksheet oSheet1 = oWB.Worksheets.get_Item(1) as Excel._Worksheet;
            oSheet1.Cells[4, 2] = _site_name;    // 사이트명 대입

            int idx = 0;
            Excel.Range R1 = (Excel.Range)oSheet1.get_Range("A8:U8");
            R1.Copy(Type.Missing);
            int tot_cnt = _data1.Count;
            string ra = string.Format("A{0}:U{1}", 8, tot_cnt + 7);
            Excel.Range R2 = oSheet1.get_Range(ra);
            R2.PasteSpecial(Excel.XlPasteType.xlPasteFormats, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
            double percentage = 0;
            foreach (var node in _data1)
            {
                percentage = (idx + 1) * 100 / tot_cnt;
                _progress_window.set_progress_bar1(percentage);
                oSheet1.Cells[8 + idx, 1] = node.sn;
                oSheet1.Cells[8 + idx, 2] = node.building_name;
                oSheet1.Cells[8 + idx, 3] = node.floor_name;
                oSheet1.Cells[8 + idx, 4] = node.room_name;
                oSheet1.Cells[8 + idx, 5] = node.rack_name;
                oSheet1.Cells[8 + idx, 6] = node.asset_name;
                oSheet1.Cells[8 + idx, 7] = string.Format("{0},{1}", node.asset_pos.X, node.asset_pos.Y);
                oSheet1.Cells[8 + idx, 8] = node.slot_no > 0 ? node.slot_no.ToString() : "";
                oSheet1.Cells[8 + idx, 9] = node.catalog_name;
                oSheet1.Cells[8 + idx, 10] = node.serial_no;
                oSheet1.Cells[8 + idx, 11] = node.ip_addr;
                oSheet1.Cells[8 + idx, 12] = node.sw_vlan;
                oSheet1.Cells[8 + idx, 13] = node.ic_con_id > 0 ? node.ic_con_id.ToString() : "";
                oSheet1.Cells[8 + idx, 14] = node.pp_id > 0 ? node.pp_id.ToString() : "";
                oSheet1.Cells[8 + idx, 15] = node.user_port_string;
                oSheet1.Cells[8 + idx, 16] = node.snmp_version;
                oSheet1.Cells[8 + idx, 17] = node.snmp_get_community;
                oSheet1.Cells[8 + idx, 18] = node.snmp_set_community;
                oSheet1.Cells[8 + idx, 19] = node.snmp_user_id;
                oSheet1.Cells[8 + idx, 20] = node.snmp_password;
                //oSheet1.Cells[8 + idx, 18] = node.snmp_trap_svr_ip;
                oSheet1.Cells[8 + idx, 21] = node.remarks;
                idx++;
            }
            oSheet1 = null;
        }


        private void export_sheet2(Excel._Workbook oWB)
        {
            Excel._Worksheet oSheet2 = oWB.Worksheets.get_Item(2) as Excel._Worksheet;
            oSheet2.Cells[4, 2] = _site_name;    // 사이트명 대입

            int idx = 0;
            // 복사
            Excel.Range R1 = (Excel.Range)oSheet2.get_Range("A8:L8");
            R1.Copy(Type.Missing);
            int tot_cnt = _data2.Count;
            string ra = string.Format("A{0}:L{1}", 8, tot_cnt + 7);
            Excel.Range R2 = oSheet2.get_Range(ra);
            R2.PasteSpecial(Excel.XlPasteType.xlPasteFormats, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
            double percentage = 0;
            foreach (var node in _data2)
            {
                percentage = (idx + 1) * 100 / tot_cnt;
                _progress_window.set_progress_bar2(percentage);
                oSheet2.Cells[8 + idx, 1] = node.sn;
                oSheet2.Cells[8 + idx, 2] = node.building_name;
                oSheet2.Cells[8 + idx, 3] = node.asset_name;
                oSheet2.Cells[8 + idx, 4] = node.port_no;
                oSheet2.Cells[8 + idx, 5] = node.front_asset_name;
                oSheet2.Cells[8 + idx, 6] = node.front_port_no > 0 ? node.front_port_no.ToString() : "";
                oSheet2.Cells[8 + idx, 7] = node.front_cable_catalog_name;
                oSheet2.Cells[8 + idx, 8] = node.front_plug_side;
                oSheet2.Cells[8 + idx, 9] = node.rear_asset_name;
                oSheet2.Cells[8 + idx, 10] = node.rear_port_no > 0 ? node.rear_port_no.ToString() : "";
                oSheet2.Cells[8 + idx, 11] = node.rear_cable_catalog_name;
                oSheet2.Cells[8 + idx, 12] = node.rear_plug_side;
                idx++;
            }
            oSheet2 = null;
        }


        private void export_sheet3(Excel._Workbook oWB)
        {
            Excel._Worksheet oSheet3 = oWB.Worksheets.get_Item(3) as Excel._Worksheet;
            int idx = 0;
            // 복사
            Excel.Range R1 = (Excel.Range)oSheet3.get_Range("A5:D5");
            R1.Copy(Type.Missing);
            int tot_cnt = _data3.Count;
            string ra = string.Format("A{0}:D{1}", 5, tot_cnt + 4);
            Excel.Range R2 = oSheet3.get_Range(ra);
            R2.PasteSpecial(Excel.XlPasteType.xlPasteFormats, Excel.XlPasteSpecialOperation.xlPasteSpecialOperationNone, false, false);
            foreach (var node in _data3)
            {
                oSheet3.Cells[5 + idx, 1] = node.sn;
                oSheet3.Cells[5 + idx, 2] = node.catalog_group1_name;
                oSheet3.Cells[5 + idx, 3] = node.catalog_group2_name;
                oSheet3.Cells[5 + idx, 4] = node.catalog_name;
                idx++;
            }
            oSheet3 = null;
        }

        // Excel로 데이터 Import 관련된 함수....
        private bool import_from_excel3()
        {
            Excel.Application oXL;
            try
            {
                oXL = new Excel.Application();
            }
            catch
            {
                MessageBox.Show("Check the Microsoft Excel program.");
                return false;
            }
            oXL.DisplayAlerts = false;
            Excel._Workbook oWB = oXL.Workbooks.Add(true);

            object missingType = Type.Missing;

            int T_Year = DateTime.Now.Year;
            int T_Month = DateTime.Now.Month;
            int T_Day = DateTime.Now.Day;
            int T_Hour = DateTime.Now.Hour;
            int T_Minute = DateTime.Now.Minute;
            int T_Second = DateTime.Now.Second;

            DateTime dTime = new DateTime(T_Year, T_Month, T_Day, T_Hour, T_Minute, T_Second);
            string time = dTime.ToString("yyMMdd_HHmmss");
            string data = "Export_" + time + ".xls";

            Microsoft.Win32.OpenFileDialog openFileDialog1 = new Microsoft.Win32.OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments).ToString();
            openFileDialog1.AddExtension = true;
            openFileDialog1.DefaultExt = "xls";
            openFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            var f = openFileDialog1.ShowDialog();
            if (f != true)
                return false;

            string file = openFileDialog1.FileName;
            if (file == String.Empty)
                return false;

            try
            {
                oWB = oXL.Workbooks.Open(file, 0, true, 5, "", "", true, Excel.XlPlatform.xlWindows, "\t", false, false, 0, 1, 0);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(g.tr_get("C_Error_Load_Excel") + ", {0}", e.Message));
                return false;
            }

            _progress_window2 = new ProgressBarDialog3();
            _progress_window2.Owner = App.Current.MainWindow;
            _progress_window2.Show();

            _data1.Clear();
            _data2.Clear();

            bool r1 = import_sheet1(oWB);
            if (!r1)
            {
                _progress_window2.Close();
                return false;
            }

            bool r2 = import_sheet2(oWB);
            if (!r2)
                return false;

            try
            {
                oWB.Close(false, missingType, missingType);
                oXL.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(oXL);
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(oXL);
                oWB = null;
                oXL = null;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private bool import_sheet1(Excel._Workbook oWB)
        {
            Excel._Worksheet oSheet1 = oWB.Worksheets.get_Item(1) as Excel._Worksheet;
            string site_name = get_text_cell(oSheet1.Cells[4, 2]);

            int site_id = g.selected_site_id;
            var s = g.site_list.Find(p => p.site_id == site_id);
            if (s == null)
                return false;
            if (site_name != s.site_name)
            {
                MessageBox.Show(g.tr_get("M9_Import_Data_3"));
                return false;
            }

            // 데이터 유무 체크 및 전체 갯수 파악
            int idx = 0;
            double percentage = 0;
            while (true)
            {
                string building_name = get_text_cell(oSheet1.Cells[8 + idx, 2]);
                if (building_name == string.Empty)
                    break;
                idx++;
            }
            int tot_cnt = idx;
            if (tot_cnt == 0)
            {
                MessageBox.Show(g.tr_get("M9_Import_Data_4"));
                return false;
            }

            string end_range = string.Format("V{0}", tot_cnt + 7);
            Microsoft.Office.Interop.Excel.Range range = oSheet1.get_Range("A8", end_range);
            object[,] values = (object[,])range.Value2;

            int sn = 1;
            for (idx = 1; idx <= tot_cnt; idx++)
            {
                // 진행 상황 표시
                percentage = idx * 100 / tot_cnt;
                _progress_window2.set_progress_bar1(percentage);
                if (percentage == 100)
                { }

                ExportData1 ed1 = new ExportData1();
                ed1.sn = sn;

                ed1.building_name = get_text_cell2(values[idx, 2]);
                ed1.floor_name = get_text_cell2(values[idx, 3]);
                ed1.room_name = get_text_cell2(values[idx, 4]);
                ed1.rack_name = get_text_cell2(values[idx, 5]);
                ed1.asset_name = get_text_cell2(values[idx, 6]);
                string pos = get_text_cell2(values[idx, 7]);
                ed1.asset_pos = get_point(pos);
                ed1.slot_no = get_int_cell2(values[idx, 8]);
                ed1.catalog_name = get_text_cell2(values[idx, 9]);
                ed1.serial_no = get_text_cell2(values[idx, 10]);
                ed1.ip_addr = get_text_cell2(values[idx, 11]);
                ed1.sw_vlan = get_text_cell2(values[idx, 12]);
                ed1.ic_con_id = get_int_cell2(values[idx, 13]);
                ed1.pp_id = get_int_cell2(values[idx, 14]);
                string user_port_str = get_text_cell2(values[idx, 15]);
                ed1.user_port_pos = get_user_port_pos(user_port_str);
                ed1.snmp_version = get_text_cell2(values[idx, 16]);
                ed1.snmp_get_community = get_text_cell2(values[idx, 17]);
                ed1.snmp_set_community = get_text_cell2(values[idx, 18]);
                ed1.snmp_user_id = get_text_cell2(values[idx, 19]);
                ed1.snmp_password = get_text_cell2(values[idx, 20]);
                ed1.remarks = get_text_cell2(values[idx, 21]);

                var find = _data1.Find(p => p.asset_name == ed1.asset_name);
                if (find != null)
                {
                    MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_5"), 8 + idx, ed1.asset_name));
                    return false;
                }

                // 데이터가 존재하면 skip
                int asset_id = Etc.get_asset_id_in_building(ed1.asset_name, ed1.building_id);
                if (asset_id == 0)
                {
                    _data1.Add(ed1);
                    sn++;
                }
            }
            percentage = 100;
            _progress_window2.set_progress_bar1(percentage);
            return true;
        }


        private bool check1()
        {            
            int tot_cnt = _data1.Count;
            int idx = 0;
            int site_id = g.selected_site_id;

            for (idx = 0; idx < tot_cnt; idx++)
            {
                ExportData1 ed1 = _data1[idx];
         
                ed1.building_id = Etc.get_building_id_in_site(ed1.building_name, site_id);
                if (ed1.building_id == 0)
                {
                    MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_6"), 8 + idx, ed1.building_name));
                    return false;
                }
                ed1.floor_id = Etc.get_floor_id_in_building(ed1.floor_name, ed1.building_id);
                if (ed1.floor_id == 0)
                {
                    MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_7"), 8 + idx, ed1.floor_name));
                    return false;
                }
                ed1.room_id = Etc.get_room_id_in_floor(ed1.room_name, ed1.floor_id);
                if (ed1.room_id == 0)
                {
                    MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_8"), 8 + idx, ed1.room_name));
                    return false;
                }
                if (ed1.rack_name != string.Empty)
                {
                    ed1.rack_id = Etc.get_rack_id_in_room(ed1.rack_name, ed1.room_id);
                    if (ed1.rack_id == 0)
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_9"), 8 + idx, ed1.rack_name));
                        return false;
                    }
                    var l = g.location_list.Find(p => (p.rack_id == ed1.rack_id) && (p.location_level == 7));
                    if (l == null)
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_10"), 8 + idx, ed1.rack_name));
                        return false;
                    }
                    ed1.location_id = l.location_id;                    
                }
                else
                {
                    var l = g.location_list.Find(p => (p.room_id == ed1.room_id) && (p.location_level == 6));
                    if (l == null)
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_11"), 8 + idx, ed1.room_name));
                        return false;
                    }
                    ed1.location_id = l.location_id;                    
                }
                if (ed1.catalog_name != string.Empty)
                {
                    ed1.catalog_id = Etc.get_catalog_id(ed1.catalog_name);
                    if (ed1.catalog_id == 0)
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_12"), 8 + idx, ed1.catalog_name));
                        return false;
                    }
                }
                int max_slot = Etc.get_rack_max_slot(ed1.rack_id);
                if (ed1.slot_no > max_slot)
                {
                    MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_13"), max_slot, 8 + idx, ed1.slot_no));
                    return false;
                }

                // IC인경우 컨트롤러 번호체크
                if (CatalogType.is_ic(ed1.catalog_id))
                {
                    if ((ed1.ic_con_id < 1) || (ed1.ic_con_id > 999))
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_14"), 8 + idx, ed1.ic_con_id));
                        return false;
                    }
                    var dup_check = _data1.Find(p => CatalogType.is_ic(Etc.get_catalog_id(p.catalog_name)) && (p.ic_con_id == ed1.ic_con_id) && (p.sn != ed1.sn));
                    if (dup_check != null)
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_15"), 8 + idx, ed1.ic_con_id));
                        return false;
                    }

                    var dup_check2 = from aa in g.asset_list
                             join bb in g.asset_aux_list.Where(p => p.ic_con_id == ed1.ic_con_id) on aa.asset_id equals bb.asset_id
                             join cc in g.location_list on aa.location_id equals cc.location_id
                             where cc.site_id != site_id
                             select new { aa.asset_id, cc.location_name };

                    if (dup_check2.Count() > 0)
                    {
                        string other_location_name = dup_check2.ElementAt(0).location_name;
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_16"), 8 + idx, ed1.ic_con_id, other_location_name));
                        return false;
                    }
                }

                if (CatalogType.is_ipp(ed1.catalog_id))
                {
                    var dup_check = _data1.Find(p => (p.ic_con_id == ed1.ic_con_id) && (p.pp_id == ed1.pp_id) && (p.sn != ed1.sn) && (ed1.ic_con_id > 0) && (ed1.pp_id > 0));
                    if (dup_check != null)
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_17"), 8 + idx, ed1.ic_con_id, ed1.pp_id));
                        return false;
                    }
                    // _data1은 쉬트를 읽어드렸을때 컨버전체크를 필요로하는 ID들은 입력이 안되었기 때문에 이름으로 검색...
                    if (ed1.ic_con_id > 0)
                    {
                        var connect = _data1.Find(p => CatalogType.is_ic(Etc.get_catalog_id(p.catalog_name)) && (p.ic_con_id == ed1.ic_con_id));
                        if (connect == null)
                        {
                            MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_18"), 8 + idx, ed1.ic_con_id, ed1.pp_id));
                            return false;
                        }
                    }
                }

                if ((ed1.rack_id > 0) && !CatalogType.is_rack_mountable(ed1.catalog_id))
                {
                    if (!CatalogType.is_sw_card(ed1.catalog_id))
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_19"), 8 + idx, ed1.asset_name));
                        return false;
                    }
                }

            }
            return true;
        }

        private List<Point> get_user_port_pos(string data)
        {
            
            List<Point> pos_list = new List<Point>();
            if (data == string.Empty)
                return pos_list;

            int pos_x = 0;
            int pos_y = 0;
            int pos = 0;
            try
            {
                while (true)
                {
                    Point p = new Point();

                    pos = data.Substring(pos_x).IndexOf(",") + 1;
                    if (pos == 0)
                        break;
                    pos_y = pos_x + pos;
                    p.X = Etc.get_int(data.Substring(pos_x, pos_y - pos_x - 1));
                    pos = data.Substring(pos_y).IndexOf(",") + 1;
                    // 마지막이면...
                    if (pos == 0)
                    {
                        p.Y = Etc.get_int(data.Substring(pos_y));
                        pos_list.Add(p);
                        break;
                    }
                    else
                    {
                        pos_x = pos_y + pos;
                        p.Y = Etc.get_int(data.Substring(pos_y, pos_x - pos_y - 1));
                        pos_list.Add(p);
                    }
                }
            }
            catch (Exception) { }

            return pos_list;
        }

        private string get_text_cell2(object cell)
        {
            string data = "";
            try
            {
                data = Convert.ToString(cell);
                data = data.Trim();
            }
            catch (Exception) { }
            return data;
        }

        private int get_int_cell2(object cell)
        {
            string data = "";
            try
            {
                data = Convert.ToString(cell); 
                return Etc.get_int(data);
            }
            catch (Exception) { }
            return 0;
        }

        private string get_text_cell(Excel.Range cell)
        {
            string data = "";
            try
            {
                data = cell.Value.ToString();
                data = data.Trim();
            }
            catch (Exception) { }
            return data;
        }

        private int get_int_cell(Excel.Range cell)
        {
            string data = "";
            try
            {
                data = cell.Value.ToString();
                return Etc.get_int(data);
            }
            catch (Exception) { }
            return 0;
        }


        private Point get_point(string data)
        {
            Point p = new Point();
            try
            {
                int pos = data.IndexOf(",");
                if (pos < 1)
                    return p;

                string x = data.Substring(0, pos);
                string y = data.Substring(pos + 1);
                p.X = Etc.get_int(x);
                p.Y = Etc.get_int(y);
            }
            catch(Exception) {}
            return p;            
        }

        
        private bool import_sheet2(Excel._Workbook oWB)
        {
            Excel._Worksheet oSheet2 = oWB.Worksheets.get_Item(2) as Excel._Worksheet;

            int site_id = g.selected_site_id;

            // 데이터 유무 체크 및 전체 갯수 파악
            int idx = 0;
            double percentage = 0;
            while (true)
            {
                string name = get_text_cell(oSheet2.Cells[8 + idx, 3]);
                if (name == string.Empty)
                    break;
                idx++;
            }
            // 선번장 데이터가 없는 경우 그냥 빠져나감.
            int tot_cnt = idx;
            if (tot_cnt == 0)
                return true;

            string end_range = string.Format("L{0}", tot_cnt + 7);
            Microsoft.Office.Interop.Excel.Range range = oSheet2.get_Range("A8", end_range);
            object[,] values = (object[,])range.Value2;

            for (idx = 1; idx <= tot_cnt; idx++)
            {
                // 진행 상황 표시
                percentage = idx * 100 / tot_cnt;
                _progress_window2.set_progress_bar3(percentage);

                ExportData2 ed2 = new ExportData2();
                ed2.sn = idx;
                ed2.building_name = get_text_cell2(values[idx, 2]);
                ed2.asset_name = get_text_cell2(values[idx, 3]);
                ed2.port_no = get_int_cell2(values[idx, 4]);
                ed2.front_asset_name = get_text_cell2(values[idx, 5]);
                ed2.front_port_no = get_int_cell2(values[idx, 6]);
                ed2.front_cable_catalog_name = get_text_cell2(values[idx, 7]);
                ed2.front_plug_side = get_text_cell2(values[idx, 8]);
                ed2.rear_asset_name = get_text_cell2(values[idx, 9]);
                ed2.rear_port_no = get_int_cell2(values[idx, 10]);
                ed2.rear_cable_catalog_name = get_text_cell2(values[idx, 11]);
                ed2.rear_plug_side = get_text_cell2(values[idx, 12]);
                _data2.Add(ed2);
            }
            percentage = 100;
            _progress_window2.set_progress_bar3(percentage);
            return true;
        }

        private bool check2()
        {
            int site_id = g.selected_site_id;

            // 데이터 유무 체크 및 전체 갯수 파악
            int idx = 0;
            int tot_cnt = _data2.Count;
            if (tot_cnt == 0)
                return true;

            int building_id = 0;
            string building_name = "";
            int asset_id = 0;
            string asset_name = "";
            int front_asset_id = 0;
            string front_asset_name = "";
            int front_cable_catalog_id = 0;
            string front_cable_catalog_name = "";
            int rear_asset_id = 0;
            string rear_asset_name = "";
            int rear_cable_catalog_id = 0;
            string rear_cable_catalog_name = "";
            int num_of_ports = 0;
            int front_num_of_ports = 0;
            int rear_num_of_ports = 0;
            int percentage = 0;
            for (idx = 0; idx < tot_cnt; idx++)
            {
                // 진행 상황 표시
                percentage = (idx + 1) * 100 / tot_cnt;
                _progress_window2.set_progress_bar3(percentage);

                ExportData2 ed2 = _data2[idx];

                if (building_name != ed2.building_name)
                {
                    building_name = ed2.building_name;
                    ed2.building_id = Etc.get_building_id_in_site(ed2.building_name, site_id);
                    if (ed2.building_id == 0)
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_6"), 8 + idx, ed2.building_name));
                        return false;
                    }
                    building_id = ed2.building_id;
                }
                else
                    ed2.building_id = building_id;

                if (asset_name != ed2.asset_name)
                {
                    asset_name = ed2.asset_name;
                    ed2.asset_id = Etc.get_asset_id_in_building(ed2.asset_name, ed2.building_id);
                    if (ed2.asset_id == 0)
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_20"), 8 + idx, ed2.asset_name));
                        return false;
                    }
                    asset_id = ed2.asset_id;
                    num_of_ports = Etc.get_num_of_ports_by_asset_id(ed2.asset_id);
                }
                else
                    ed2.asset_id = asset_id;

                if ((ed2.port_no < 1) || (ed2.port_no > num_of_ports))
                {
                    MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_21"), num_of_ports, 8 + idx, ed2.port_no));
                    return false;
                }

                // 전면 연결 체크 파트

                if ((ed2.front_asset_name != string.Empty) && (ed2.front_asset_name != "HUB"))
                {
                    if (front_asset_name != ed2.front_asset_name)
                    {
                        front_asset_name = ed2.front_asset_name;
                        ed2.front_asset_id = Etc.get_asset_id_in_building(ed2.front_asset_name, ed2.building_id);
                        if (ed2.front_asset_id == 0)
                        {
                            MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_22"), 8 + idx, ed2.front_asset_name));
                            return false;
                        }
                        front_asset_id = ed2.front_asset_id;
                        front_num_of_ports = Etc.get_num_of_ports_by_asset_id(ed2.front_asset_id);
                    }
                    else
                        ed2.front_asset_id = front_asset_id;

                    if ((ed2.front_port_no < 1) || (ed2.front_port_no > front_num_of_ports))
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_23"), front_num_of_ports, 8 + idx, ed2.front_port_no));
                        return false;
                    }
                    if (front_cable_catalog_name != ed2.front_cable_catalog_name)
                    {
                        front_cable_catalog_name = ed2.front_cable_catalog_name;
                        ed2.front_cable_catalog_id = Etc.get_catalog_id(ed2.front_cable_catalog_name);
                        if (ed2.front_cable_catalog_id == 0)
                        {
                            MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_24"), 8 + idx, ed2.front_cable_catalog_name));
                            return false;
                        }
                        front_cable_catalog_id = ed2.front_cable_catalog_id;
                    }
                    else
                        ed2.front_cable_catalog_id = front_cable_catalog_id;
                    if ((ed2.front_plug_side != "F") && (ed2.front_plug_side != "R"))
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_25"), 8 + idx, ed2.front_plug_side));
                        return false;
                    }
                }

                // 후면 연결 체크 파트

                if ((ed2.rear_asset_name != string.Empty) && (ed2.rear_asset_name != "HUB"))
                {
                    if (rear_asset_name != ed2.rear_asset_name)
                    {
                        rear_asset_name = ed2.rear_asset_name;
                        ed2.rear_asset_id = Etc.get_asset_id_in_building(ed2.rear_asset_name, ed2.building_id);
                        if (ed2.rear_asset_id == 0)
                        {
                            MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_26"), 8 + idx, ed2.rear_asset_name));
                            return false;
                        }
                        rear_asset_id = ed2.rear_asset_id;
                        rear_num_of_ports = Etc.get_num_of_ports_by_asset_id(ed2.rear_asset_id);
                    }
                    else
                        ed2.rear_asset_id = rear_asset_id;

                    if ((ed2.rear_port_no < 1) || (ed2.rear_port_no > rear_num_of_ports))
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_27"), rear_num_of_ports, 8 + idx, ed2.rear_port_no));
                        return false;
                    }
                    if (rear_cable_catalog_name != ed2.rear_cable_catalog_name)
                    {
                        rear_cable_catalog_name = ed2.rear_cable_catalog_name;
                        ed2.rear_cable_catalog_id = Etc.get_catalog_id(ed2.rear_cable_catalog_name);
                        if (ed2.rear_cable_catalog_id == 0)
                        {
                            MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_28"), 8 + idx, ed2.rear_cable_catalog_name));
                            return false;
                        }
                        rear_cable_catalog_id = ed2.rear_cable_catalog_id;
                    }
                    else
                        ed2.rear_cable_catalog_id = rear_cable_catalog_id;
                    if ((ed2.rear_plug_side != "F") && (ed2.rear_plug_side != "R"))
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_29"), 8 + idx, ed2.rear_plug_side));
                        return false;
                    }
                }

                var acl = g.asset_port_link_list.Find(p => (p.asset_id == ed2.asset_id) && (p.port_no == ed2.port_no));
                if (acl == null)
                {
                    MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_30"), 8 + idx, ed2.asset_name, ed2.port_no));
                    return false;
                }
            }
            return true;
        }

        private async Task<bool> update_db1()
        {
            int tot_cnt = _data1.Count;
            int idx = 1;

            // 1순위 항목 추가.       (컨트롤러, 새시형스위치)
            foreach (var node in _data1)
            {
                if (CatalogType.is_ic(node.catalog_id) || CatalogType.is_sw_slot(node.catalog_id))
                {
                    bool b = await add_asset2(node, idx, 0);
                    if (!b)
                        return false;
                    idx++;
                }
            }

            // 2순위 항목 추가.       (1, 2단계에서 추가하지 않은 항목들....)
            int sw_asset_id = 0;
            foreach (var node in _data1)
            {
                if (CatalogType.is_sw_slot(node.catalog_id))
                    sw_asset_id = node.asset_id;
                if (!CatalogType.is_ic(node.catalog_id) && !CatalogType.is_sw_slot(node.catalog_id))
                {
                    // 샤시형 스위치
                    bool b = await add_asset2(node, idx, sw_asset_id);
                    if (!b)
                        return false;
                    idx++;
                }
            }

            return true;
        }

        // sw_asset_id : 새시형 스위치 자산 ID <-- 카드형 스위치 자산을 등록해야 할 때 필요.
        private async Task<bool> add_asset2(ExportData1 node, int idx, int sw_asset_id)
        {
            int tot_cnt = _data1.Count;
            int percentage = 0;
            {
                // 진행 상황 표시
                percentage = idx * 100 / tot_cnt;
                _progress_window2.set_progress_bar2(percentage);
                asset a = new asset();
                a.asset_name = node.asset_name;
                a.catalog_id = node.catalog_id;
                a.last_updated = DateTime.Now;
                a.user_id = g.login_user_id;
                a.ipv4 = node.ip_addr;
                int pos_x = node.asset_pos != null ? (int)node.asset_pos.X : 0;
                int pos_y = node.asset_pos != null ? (int)node.asset_pos.Y : 0;
                bool is_layout = (pos_x > 0) || (pos_y > 0);
                a.is_layout = is_layout ? "Y" : "N";
                a.pos_x = is_layout ? (int?)pos_x : null;
                a.pos_y = is_layout ? (int?)pos_y : null;
                a.serial_no = node.serial_no;
                a.install_user_name = null;
                a.install_date = null;
                a.location_id = node.location_id;
                a.install_date = DateTime.Now;

                // Step 1. 각종 DB 생성(asset, location, asset_tree, .....

                var add = (asset)await g.webapi.post("asset", a, typeof(asset));
                if (add == null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                int new_asset_id = add.asset_id;
                node.asset_id = new_asset_id;

                // Step 2. 메모리에 추가
                g.asset_list.Add(add);

                var aa = (asset_aux)await g.webapi.get("asset_aux", new_asset_id, typeof(asset_aux));
                if (aa == null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }

                aa.sw_vlan = node.sw_vlan;
                aa.snmp_version = node.snmp_version;
                aa.snmp_get_community = node.snmp_get_community;
                aa.snmp_set_community = node.snmp_set_community;
                aa.snmp_v3_user = node.snmp_user_id;
                aa.snmp_v3_password = node.snmp_password;
                //aa.snmp_trap_svr_ip = node.snmp_trap_svr_ip;
                if (CatalogType.is_ic(node.catalog_id))
                    aa.ic_con_id = node.ic_con_id;

                var r1 = await g.webapi.put("asset_aux", new_asset_id, aa, typeof(asset_aux));
                if (r1 != 0)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }

                // 메모리에 추가
                g.asset_aux_list.Add(aa);

                // asset_ext도 추가해야 함.
                string filter = string.Format("?asset_id={0}", new_asset_id);
                var list = (List<asset_ext>)await g.webapi.getList("asset_ext", typeof(List<asset_ext>), filter);
                if (list != null)
                {
                    foreach (var ae in list)
                    {
                        g.asset_ext_list.Add(ae);
                    }
                }

                // ic_ipp_config 구성....
                if (CatalogType.is_ipp(node.catalog_id))
                {
                    if (node.ic_con_id > 0)
                    {
                        int ic_asset_id = Etc.get_ic_asset_id(node.ic_con_id);
                        if (ic_asset_id == 0)
                        {
                            MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_31"), 7 + node.sn, node.asset_name, node.ic_con_id));
                            return false;
                        }
                        var iic = g.ic_ipp_config_list.Find(p => (p.ic_asset_id == ic_asset_id) && (p.ipp_connect_no == node.pp_id));
                        if (iic == null)
                        {
                            MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_32"), 7 + node.sn, node.asset_name, node.ic_con_id, node.pp_id));
                            return false;
                        }
                        iic.ipp_asset_id = new_asset_id;
                        var r2 = await g.webapi.put("ic_ipp_config", iic.ic_ipp_config_id, iic, typeof(ic_ipp_config));
                        if (r2 != 0)
                        {
                            MessageBox.Show(g.tr_get("C_Error_Server"));
                            return false;
                        }
                    }
                }

                // sw_card_config 구성....
                if (CatalogType.is_sw_card(node.catalog_id) && (sw_asset_id > 0))
                {
                    int next_slot_no = Etc.get_sw_next_slot(sw_asset_id);
                    var scc = g.sw_card_config_list.Find(p => (p.sw_asset_id == sw_asset_id) && (p.slot_no == next_slot_no));
                    if (scc == null)
                    {
                        MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_33"), 7 + node.sn, node.asset_name, sw_asset_id, next_slot_no));
                        return false;
                    }
                    scc.sw_card_asset_id = new_asset_id;
                    var r3 = await g.webapi.put("sw_card_config", scc.sw_card_config_id, scc, typeof(sw_card_config));
                    if (r3 != 0)
                    {
                        MessageBox.Show(g.tr_get("C_Error_Server"));
                        return false;
                    }
                }

                // 사용자포트 레이아웃 구성
                if (CatalogType.is_fp(node.catalog_id) || CatalogType.is_mb(node.catalog_id))
                {
                    // 메모리로 로드
                    int num_of_ports = Etc.get_num_of_ports_by_catalog_id(node.catalog_id);
                    await g.left_tree_handler.add_user_port_layout(new_asset_id, node.catalog_id, num_of_ports);
                    int i = 0;
                    for (i = 0; i < num_of_ports; i++)
                    {
                        int port_no = i + 1;
                        var upl = g.user_port_layout_list.Find(p => (p.asset_id == new_asset_id) && (p.port_no == port_no));
                        if (upl == null)
                        {
                            MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_34"), 7 + node.sn, node.asset_name, port_no));
                            return false;
                        }
                        int x = node.user_port_pos.Count > i ? (int) node.user_port_pos[i].X : 0;
                        int y = node.user_port_pos.Count > i ? (int) node.user_port_pos[i].Y : 0;
                        bool is_layout2 = (x > 0) || (y > 0);
                        upl.is_layout = is_layout2 ? "Y" : "N";
                        upl.pos_x = is_layout2 ? (int?) x : null;
                        upl.pos_y = is_layout2 ? (int?) y : null;
                        var r3 = await g.webapi.put("user_port_layout", upl.user_port_layout_id, upl, typeof(user_port_layout));
                        if (r3 != 0)
                        {
                            MessageBox.Show(g.tr_get("C_Error_Server"));
                            return false;
                        }
                    }
                }

                // 즐겨찾기트리와 위치트리 메모리에 추가하고 화면 갱신
                if (!CatalogType.is_sw_card(node.catalog_id))
                {
                    bool b = await g.left_tree_handler.addAsset(new_asset_id);
                    if (!b)
                        return false;
                    if (node.rack_id > 0)
                    {
                        string rack_mount_type = CatalogType.is_eb(node.catalog_id) && (node.slot_no < 1) ? "L" : "S";            // 아직 파워스트립은 하나만....
                        bool b4 = await g.left_tree_handler.add_to_rack_config(node.rack_id, rack_mount_type, node.slot_no, new_asset_id, node.catalog_id);
                    }
                }
            }

            await g.left_tree_handler.reload_asset_port_link();
            return true;
        }


        private async Task<bool> update_db2()
        {
            int tot_cnt = _data2.Count;
            int idx = 1;
            int percentage = 0;
            foreach(var node in _data2)
            {
                // 진행 상황 표시
                percentage = idx * 100 / tot_cnt;
                _progress_window2.set_progress_bar4(percentage);
                var apl = g.asset_port_link_list.Find(p => (p.asset_id == node.asset_id) && (p.port_no == node.port_no));
                if (apl != null)
                {
                    bool change_front = ((apl.front_asset_id ?? 0) != node.front_asset_id) ||
                                        ((apl.front_port_no ?? 0) != node.front_port_no) ||
                                        ((apl.front_cable_catalog_id ?? 0) != node.front_cable_catalog_id) ||
                                        ((apl.front_plug_side ?? "") != node.front_plug_side);
                    bool change_rear = ((apl.rear_asset_id ?? 0) != node.rear_asset_id) ||
                                        ((apl.rear_port_no ?? 0) != node.rear_port_no) ||
                                        ((apl.rear_cable_catalog_id ?? 0) != node.rear_cable_catalog_id) ||
                                        ((apl.rear_plug_side ?? "") != node.rear_plug_side);
                    if (change_front || change_rear)
                    {
                        // DB 변경처리...

                        if (node.front_asset_id > 0)
                        {
                            apl.front_asset_id = Etc.get_null_int(node.front_asset_id);
                            apl.front_port_no = Etc.get_null_int(node.front_port_no);
                            apl.front_cable_catalog_id = Etc.get_null_int(node.front_cable_catalog_id);
                            apl.front_plug_side = Etc.get_null_string(node.front_plug_side);
                        }
                        else
                        {
                            apl.front_asset_id = null;
                            apl.front_port_no = null;
                            apl.front_cable_catalog_id = null;
                            apl.front_plug_side = null;
                        }

                        if (node.rear_asset_id > 0)
                        {
                            apl.rear_asset_id = Etc.get_null_int(node.rear_asset_id);
                            apl.rear_port_no = Etc.get_null_int(node.rear_port_no);
                            apl.rear_cable_catalog_id = Etc.get_null_int(node.rear_cable_catalog_id);
                            apl.rear_plug_side = Etc.get_null_string(node.rear_plug_side);
                        }
                        else
                        {
                            apl.rear_asset_id = null;
                            apl.rear_port_no = null;
                            apl.rear_cable_catalog_id = null;
                            apl.rear_plug_side = null;
                        }

                        var r = await g.webapi.put("asset_port_link", apl.asset_port_link_id, apl, typeof(asset_port_link));
                        if (r != 0)
                        {
                            MessageBox.Show(string.Format(g.tr_get("C_Error_Import_Data_35"), apl.asset_id, apl.port_no));
                            return false;
                        }

                        ePortStatus status1 = node.front_asset_id > 0 ? ePortStatus.Linked : ePortStatus.Unplugged;
                        ePortStatus status2 = node.rear_asset_id > 0 ? ePortStatus.Linked : ePortStatus.Unplugged;

                        // 화면...
                        g.left_tree_handler.update_link_info_screen(node.asset_id, node.port_no, node.front_asset_id, node.front_port_no, status1);
                        g.left_tree_handler.update_link_info_screen(node.asset_id, node.port_no, node.rear_asset_id, node.rear_port_no, status2);
                    }
                }
                idx++;
            }
            percentage = 100;
            _progress_window2.set_progress_bar4(percentage);

            return true;
        }
    }

}
