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
using System.IO;


namespace I2MS2.Library
{
    public class ExportStatistics
    {
        public List<ChartData> out_list1;
        public List<ChartData> out_list2;
        public List<ChartData> out_list3;
        public List<ChartData> out_list4;
        public List<ChartData> out_list5;
        public List<ChartData> out_list6;

        public string title;
        public string subtitle;
        public string subtitle1;
        public string subtitle2;
        public string subtitle3;
        public string _csv_name = "";

        // 엑셀 출력 모듈 
        public void save_2_excel(string templete_filename, int case1)
        {
            Excel.Application oXL;
            
            object missingType = Type.Missing;

            string time = get_now_string();
            string data = templete_filename + "_" + time + ".xls";

            string filename = Get_File(data);

            if (filename == null) return;
            _csv_name = filename;
            string lang_code = g.lang_id == 1080001 ? "ko" : "en";    // 1080001=한글, 영문=1080002
            string file = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).ToString() + "\\LSCable\\SimpleWIN\\ExcelTemplates\\" + templete_filename + "_" + lang_code;
            export_csv();  // romee 2018-03-13 마곡 출력 문제 

            try
            {
                oXL = new Excel.Application();
            }
            catch
            {
                //MessageBox.Show("Check the Microsoft Excel program.");
                return;
            }
            Excel._Workbook oWB = oXL.Workbooks.Add(true);

            try
            {
                oWB = oXL.Workbooks.Open(file, 0, true, 5, "", "", true, 2, "\t", false, false, 0, 1, 0);
                Excel._Worksheet oSheet = oWB.Worksheets.get_Item(1) as Excel._Worksheet;

                int line = 5;

                oSheet.Cells[4, 3] = subtitle;

                switch (case1)
                {
                    case 1:
                        // 내용 출력 
                        for (int i = 0; i < out_list1.Count; i++)
                        {
                            oSheet.Cells.Font.Bold = false;
                            oSheet.Cells[5, i+3] = out_list1[i].category;
                            oSheet.Cells[6, i+3] = out_list1[i].value;
                            line++;
                        }
                        break; 
                }
                // Save Excel File
                //oSheet.Columns.AutoFit();
                oWB.SaveAs(filename, Excel.XlFileFormat.xlWorkbookNormal, missingType, missingType,
                missingType, missingType, Excel.XlSaveAsAccessMode.xlNoChange, missingType, missingType, missingType, missingType, missingType);

                oWB.Close(false, missingType, missingType);
            }
            catch (Exception e)
            {
                MessageBox.Show(string.Format(g.tr_get("C_Error_Save_Excel") + ", {0}", e.Message));
            }
        }

        private void export_csv()
        {
            FileStream fs = new FileStream(_csv_name + "1.csv", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);

            sw.WriteLine("");
            sw.WriteLine(title);
            sw.WriteLine("");
            sw.WriteLine(subtitle1+";"+subtitle);
            sw.WriteLine("");

            string text1 = subtitle2;
            string text2 = subtitle3;
            for (int i = 0; i < out_list1.Count; i++)
            {
                text1 = text1 + ";" + out_list1[i].category;
                text2 = text2 + ";" + out_list1[i].value;
            }
            sw.WriteLine(text1);
            sw.WriteLine(text2);
            sw.Close();
            fs.Close();
        }

        private string get_now_string()
        {
            int T_Year = DateTime.Now.Year;
            int T_Month = DateTime.Now.Month;
            int T_Day = DateTime.Now.Day;
            int T_Hour = DateTime.Now.Hour;
            int T_Minute = DateTime.Now.Minute;
            int T_Second = DateTime.Now.Second;

            DateTime dTime = new DateTime(T_Year, T_Month, T_Day, T_Hour, T_Minute, T_Second);
            return dTime.ToString("yyMMdd_HHmmss");
        }

        private string Get_File(string data)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog1 = new Microsoft.Win32.SaveFileDialog();

            saveFileDialog1.AddExtension = true;
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;
            saveFileDialog1.CreatePrompt = false;
            saveFileDialog1.OverwritePrompt = false;
            saveFileDialog1.FileName = data;
            saveFileDialog1.DefaultExt = "xls";
            saveFileDialog1.Filter = "Excel files (*.xls)|*.xls";
            saveFileDialog1.InitialDirectory = "";
            var f = saveFileDialog1.ShowDialog();

            if (f != true)
            {
                return null;
            }
            return saveFileDialog1.FileName;
        }


    }
}
