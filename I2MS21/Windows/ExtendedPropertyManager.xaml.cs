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
using WebApi.Models;
using I2MS2.Models;
using I2MS2.Library;
using WebApiClient;
using System.Globalization;
using System.ComponentModel;

namespace I2MS2.Windows
{
    /// <summary>
    /// ManufactureManager.xaml에 대한 상호 작용 논리
    /// </summary>

    public class ExtPropertyVM : INotifyPropertyChanged
    {
        #region ExtPropertyVM 정의
        public int ext_id { get; set; }
        public string ext_name { get; set; }
        public int ext_length { get; set; }
        public string ext_type { get; set; }
        public int user_id { get; set; }
        public string remarks { get; set; }

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

    public class ExtPropertyAnsVM : INotifyPropertyChanged
    {
        #region ExtPropertyAnsVM 정의
        public int ext_property_ans_id { get; set; }
        public int ext_id { get; set; }
        public int ans_no { get; set; }
        public string ans_name { get; set; }
        public int user_id { get; set; }
        public string remarks { get; set; }

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

    public partial class ExtendedPropertyManager : Window
    {
        #region RouteCommand 버튼 관련 정의
        public static RoutedCommand NewCommand = new RoutedCommand();
        public static RoutedCommand DeleteCommand = new RoutedCommand();
        public static RoutedCommand EditCommand = new RoutedCommand();
        public static RoutedCommand SaveCommand = new RoutedCommand();
        public static RoutedCommand CancelCommand = new RoutedCommand();

        public static RoutedCommand New2Command = new RoutedCommand();
        public static RoutedCommand Delete2Command = new RoutedCommand();
        public static RoutedCommand Save2Command = new RoutedCommand();

        private bool _new = true;
        private bool _delete = false;
        private bool _edit = false;
        private bool _save = false;
        private bool _cancel = false;

        private bool _new2 = false;
        private bool _delete2 = false;
        private bool _save2 = false;

        private bool _new_flag = true;
        private bool _new2_flag = true;
        #endregion

        List<ExtPropertyVM> _left_list = null;
        ExtPropertyVM _left_item = null;

        List<ExtPropertyAnsVM> _right_list = null;
        ExtPropertyAnsVM _right_item = null;

        public ExtendedPropertyManager()
        {
            InitializeComponent();
            initLeft();         
            _lvLeft.SelectedItem = 0;
        }

        #region 신규,삭제 등 버튼 처리 로직
        private void _cmdNew_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _new;
        }

        private void _cmdNew_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _new_flag = true;
            _right_list = new List<ExtPropertyAnsVM>();
            clearControlLeft();
            enableControlLeft(true);
            clearControlRight();
            enableControlRight(true);

            _new = false;
            _delete = false;
            _edit = false;
            _save = true;
            _cancel = true;
            _new2 = true;
            _save2 = false;

            txtExtName.Focus();
        }

        private void _cmdDelete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _delete;
        }

        private async void _cmdDelete_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(g.tr_get("C_Delete_Item"), g.tr_get("C_Confirm"), MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            int idx = _lvLeft.SelectedIndex;

            if (await deleteLeft())
            {
                if (idx > 0)
                {
                    refreshLeft(idx);
                    return;
                }
            }
        }

        private void _cmdEdit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _edit;
        }

        private void _cmdEdit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            enableControlLeft(true);
            enableControlRight(true);
            _new = false;
            _delete = false;
            _edit = false;
            _save = true;
            _cancel = true;
            _new2 = true;
            _save2 = true;
            _new_flag = false;

            clearControlRight();

            txtExtName.Focus();
        }

        private void _cmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtExtName.Text) ||
                (cboType.SelectedIndex == 0))
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = _save;
        }

        private async void _cmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!await saveLeft())
                return;

            if (_new_flag)
            {
                // 화면을 갱신하기 위해 다시 데이터를 리스트에 로드하고 선택한다.
                refreshLeft(-1);
            }
            else
            {
                _left_item.force_changed = true;
            }

            clearControlRight();
            enableControlLeft(false);
            enableControlRight(false);

            _new = true;
            _delete = true;
            _edit = true;
            _save = false;
            _cancel = false;
            _new2 = false;
            _delete2 = false;
            _save2 = false;

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }

        private void _cmdCancel_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _cancel;
        }

        private void _cmdCancel_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            clearControlLeft();
            clearControlRight();
            enableControlLeft(false);
            enableControlRight(false);

            _new = true;
            _delete = false;
            _edit = false;
            _save = false;
            _cancel = false;
            _new2 = false;
            _delete2 = false;
            _save2 = false;
        }

        private void _cmdNew2_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _new2;
        }

        private void _cmdNew2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _new2_flag = true;
            clearControlRight();
            enableControlRight(true);

            _save2 = true;

            txtAnsName.Focus();
        }

        private void _cmdDelete2_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _delete2;
        }

        private void _cmdDelete2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (MessageBox.Show(g.tr_get("C_Delete_Item"), g.tr_get("C_Confirm"), MessageBoxButton.YesNo) == MessageBoxResult.No)
                return;

            int idx = _lvRight.SelectedIndex;

            if (deleteRight())
            {
                if (idx >= 0)
                {
                    refreshRight(idx);
                }
            }
            else
                _delete2 = false;
        }           

        private void _cmdSave2_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAnsName.Text))
            {
                e.CanExecute = false;
                return;
            }
            e.CanExecute = _save2;
        }

        private void _cmdSave2_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (!saveRight())
                return;

            if (_new2_flag)
            {
                // 화면을 갱신하기 위해 다시 데이터를 리스트에 로드하고 선택한다.
                refreshRight(-1);
            }
            else
            {
                _right_item.force_changed = true;
            }

            // Command를 무조건 갱신하게 만듦.
            CommandManager.InvalidateRequerySuggested();
        }
        #endregion

        #region 각종 이벤트 핸들러 처리
        private void _lvLeft_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _new = true;
            _delete = true;
            _edit = true;
            _save = false;
            _cancel = false;

            _left_item = (ExtPropertyVM)_lvLeft.SelectedItem;

            if (_left_item == null)
                return;

            dispLeft();
            enableControlLeft(false);
            enableControlRight(false);
            clearControlRight();

            _new2 = false;
            _delete2 = false;
            _save2 = false;
            _new2_flag = true;
        }

        private void _lvRight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _right_item = (ExtPropertyAnsVM)_lvRight.SelectedItem;

            if (_right_item == null)
                return;

            dispRight();
            _new2_flag = false;
            if (_save)
            {
                _new2 = true;
                _delete2 = true;
                _save2 = true;
            }
        }
          
        private void cboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem cbo_item = cboType.SelectedValue as ComboBoxItem;
            string name = cbo_item.Content as string;

            string type = ExtType.get_ext_type(name);

            switch(type)
            {
                    // date
                case "D":
                    txtLength.Text = "8";
                    txtLength.IsEnabled = false;
                    break;
                    // RadioButton
                case "R":
                    txtLength.Text = "4";
                    txtLength.IsEnabled = false;
                    break;
                    // List(ComboBox)
                case "L":
                    txtLength.Text = "4";
                    txtLength.IsEnabled = false;
                    break;
                    // CheckBox
                case "C":
                    txtLength.Text = "10";
                    txtLength.IsEnabled = false;
                    break;
                default :
                    txtLength.IsEnabled = _save;
                    break;
            }
            enableControlRight(true);
        }

        private void _btnOk_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #endregion

        #region init 로직
        // 좌측 창에 내용을 출력한다.
        private void initLeft()
        {
            _left_list = new List<ExtPropertyVM>();
            foreach (var node in g.ext_property_list)
            {
                ExtPropertyVM item = new ExtPropertyVM();
                convert2VM(item, node);
                _left_list.Add(item);
            };
            _lvLeft.ItemsSource = _left_list;
        }

        private void initRight(int id)
        {
            var node_list = g.ext_property_ans_list.Where(p => p.ext_id == id);
            _right_list = new List<ExtPropertyAnsVM>();
            foreach (var node in node_list)
            {
                var item = new ExtPropertyAnsVM();
                convert2VM(item, node);
                _right_list.Add(item);
            }

            _lvRight.ItemsSource = null;
            _lvRight.ItemsSource = _right_list;
        }
        #endregion

        #region refresh 로직
        // 삭제 후 리프레쉬는 인덱스값을 지정하고, 추가 후 리프레쉬는 인덱스 값을 -1을 부여한다.
        private void refreshLeft(int idx)
        {
            // 삭제 시
            if (idx > 0)
            {
                _lvLeft.ItemsSource = null;
                _lvLeft.ItemsSource = _left_list;
                _lvLeft.SelectedIndex = idx - 1;
            }

            // 추가 또는 수정 시
            if (idx == -1)
            {
                int id = _left_item.ext_id;
                _lvLeft.ItemsSource = null;
                _lvLeft.ItemsSource = _left_list;
                _lvLeft.SelectedValue = id;
            }

            var node = _lvLeft.SelectedItem;
            if (node != null)
                _lvLeft.ScrollIntoView(node);
            dispLeft();
            return;
        }

        // 삭제 후 리프레쉬는 인덱스값을 지정하고, 추가 후 리프레쉬는 인덱스 값을 -1을 부여한다.
        private void refreshRight(int idx)
        {
            // 삭제 시
            if (idx >= 0)
            {
                _lvRight.ItemsSource = null;
                _lvRight.ItemsSource = _right_list;
                _lvRight.SelectedIndex = idx - 1;
            }

            // 추가 시
            if (idx == -1)
            {
                _lvRight.ItemsSource = null;
                _lvRight.ItemsSource = _right_list;
                _lvRight.SelectedIndex = _lvRight.Items.Count - 1;
            }

            // 수정 시
            if (idx == -2)
            {
                int idx2 = _lvRight.SelectedIndex;
                _lvRight.ItemsSource = null;
                _lvRight.ItemsSource = _right_list;
                _lvRight.SelectedIndex = idx2;
            }

            var node = _lvRight.SelectedItem;
            if (node != null)
                _lvRight.ScrollIntoView(node);
            dispRight();
            return;
        }
        #endregion

        #region diplay 로직
        private void dispLeft()
        {
            var item = _left_item;
            if (item == null)
                return;

            txtExtId.Text = item.ext_id.ToString();
            txtExtName.Text = item.ext_name;
            txtLength.Text = item.ext_length.ToString();
            cboType.SelectedIndex = ExtType.get_ext_type_index(item.ext_type);
            txtRemarks.Text = item.remarks;

            initRight(item.ext_id);
        }

        private void dispRight()
        {
            var item = _right_item;
            if (item == null)
                return;

            txtAnsName.Text = item.ans_name;
            txtAnsRemarks.Text = item.remarks;
        }
        #endregion

        #region clear control 로직
        private void clearControlLeft()
        {
            txtExtId.Clear();
            txtExtName.Clear();
            txtLength.Clear();
            cboType.SelectedIndex = 0;
            txtRemarks.Clear();

            clearControlRight();

            _lvRight.ItemsSource = null;
            _lvLeft.SelectedIndex = -1;
        }

        private void clearControlRight()
        {
            txtAnsName.Clear();
            txtAnsRemarks.Clear();
        }
        #endregion

        #region enable control 로직
        private void enableControlLeft(bool flag)
        {
            txtExtName.IsEnabled = flag;
            cboType.IsEnabled = flag;
            bool length_flag = false;
            if (flag)
            {
                int idx = cboType.SelectedIndex;
                if ((idx == 1) || (idx == 2))
                    length_flag = true;        
            }
            txtLength.IsEnabled = length_flag;
            txtRemarks.IsEnabled = flag;
        }

        private void enableControlRight(bool flag)
        {
            if (cboType.SelectedIndex == 1 || cboType.SelectedIndex == 2 || cboType.SelectedIndex == 3)
            {
                txtAnsName.IsEnabled = false;
                txtAnsRemarks.IsEnabled = false;
                return; 
            } 
            txtAnsName.IsEnabled = flag;
            txtAnsRemarks.IsEnabled = flag;
        }
        #endregion

        #region update로직
        // DB 업데이트가 아니라 임시 저장 장소에 화면 내용을 옮김
        private bool updateLeft(bool new_flag)
        {
            var item = _left_item;
            if (item == null)
                return false;
            item.ext_name = txtExtName.Text.Trim();
            int length;
            int.TryParse(txtLength.Text, out length);
            item.ext_length = length;
            item.ext_type = ExtType.get_ext_type(cboType.Text.Trim());
            item.user_id = g.login_user_id;
            item.remarks = txtRemarks.Text.Trim();
            return true;
        }

        private bool updateRight(bool new_flag)
        {
            ExtPropertyAnsVM item = _right_item;
            if (item == null)
                return false;
            item.ans_name = txtAnsName.Text.Trim();
            item.user_id = g.login_user_id;
            item.remarks = txtAnsRemarks.Text.Trim();
            return true;
        }
        #endregion

        #region 컨버트 로직(Db <- Screen, Screen <- Db)
        // Left : node <- item
        private void convert2DB(ext_property node, ExtPropertyVM item)
        {
            node.ext_id = item.ext_id;
            node.ext_name = item.ext_name;
            node.ext_type = item.ext_type;
            node.ext_length = item.ext_length;
            node.user_id = item.user_id;
            node.remarks = item.remarks;
        }

        // Left : vm_node <- db_node
        private void convert2VM(ExtPropertyVM item, ext_property node)
        {
            item.ext_id = node.ext_id;
            item.ext_name = node.ext_name;
            item.ext_type = node.ext_type;
            item.ext_length = node.ext_length;
            item.user_id = node.user_id ?? 0;
            item.remarks = node.remarks;
        }

        // Right : node <- item
        private void convert2DB(ext_property_ans node, ExtPropertyAnsVM item)
        {
            node.ext_property_ans_id = item.ext_property_ans_id;
            node.ext_id = item.ext_id;
            node.ans_no = item.ans_no;
            node.ans_name = item.ans_name;
            node.user_id = item.user_id;
            node.remarks = item.remarks;
        }

        // Right : vm_node <- db_node
        private void convert2VM(ExtPropertyAnsVM item, ext_property_ans node)
        {
            item.ext_property_ans_id = node.ext_property_ans_id;
            item.ext_id = node.ext_id;
            item.ans_no = node.ans_no;
            item.ans_name = node.ans_name;
            item.user_id = node.user_id ?? 0;
            item.remarks = node.remarks;
        }
        #endregion

        #region save 로직
        private async Task<bool> saveLeft()
        {
            string name = txtExtName.Text.Trim();

            if (_left_list == null)
                _left_list = new List<ExtPropertyVM>();

            // 신규 상태에서 저장 버튼을 누른 경우
            if (_new_flag)
            {
                var temp = _left_list.Find(p => p.ext_name == name);
                if (temp != null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Duplicated_ExtProperty"));
                    return false;
                }

                _left_item = new ExtPropertyVM() { ext_id = 0 };
                if (!updateLeft(true))
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }

                var node = new ext_property();
                convert2DB(node, _left_item);

                var out_node = (ext_property)await g.webapi.post("ext_property", node, typeof(ext_property));
                if (out_node == null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                g.ext_property_list.Add(out_node);
                _left_item.ext_id = out_node.ext_id;
                _left_list.Add(_left_item);

                await saveLeftSub();
            }
            else
            {
                if (!updateLeft(false))
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
                var node = new ext_property();
                convert2DB(node, _left_item);
                int r = await g.webapi.put("ext_property", _left_item.ext_id, node, typeof(ext_property));
                if (r != 0)
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }

                await saveLeftSub();
            }
            return true;
        }

        private async Task<bool> saveLeftSub()
        {
            int ext_id = _left_item.ext_id;

            // 삭제 항목 부터 처리
            var node_list = g.ext_property_ans_list.Where(p => p.ext_id == _left_item.ext_id).ToList();
            foreach (var node in node_list)
            {
                var item = _right_list.Find(p => p.ext_property_ans_id == node.ext_property_ans_id);
                if (item == null)
                {
                    // 화면에 없으면 DB에서 지워야 함.
                    int rr1 = await g.webapi.delete("ext_property_ans", node.ext_property_ans_id);
                    if (rr1 != 0)
                    {
                        MessageBox.Show(g.tr_get("C_Error_Server"));
                        return false;
                    }
                    g.ext_property_ans_list.Remove(node);
                }
            }

            // 추가할 내용이 없으면 복귀
            if (_right_list == null)
                return true;

            // 추가 항목 및 변경 항목 처리
            foreach (var item in _right_list)
            {
                //var temp = _right_list.Find(p => p.ext_property_ans_id == item.ext_property_ans_id);
                var find_node = node_list.Find(p => p.ext_property_ans_id == item.ext_property_ans_id);
                if (find_node == null)
                {
                    // DB에 없으면 추가 하여야 함.
                    var node = new ext_property_ans();
                    convert2DB(node, item);
                    node.ext_id = ext_id;

                    var out_node = (ext_property_ans)await g.webapi.post("ext_property_ans", node, typeof(ext_property_ans));
                    if (out_node == null)
                    {
                        MessageBox.Show(g.tr_get("C_Error_Server"));
                        return false;
                    }
                    g.ext_property_ans_list.Add(out_node);
                }
                else
                {
                    // 변경 처리
                    convert2DB(find_node, item);
                }
            }

            // 변경 처리 된 내용을 저장하며 이 때 답변 순서도 정해준다. 
            int cnt = 0;
            foreach (var node in node_list)
            {
                node.ans_no = ++cnt;
                await g.webapi.put("ext_property_ans", node.ext_property_ans_id, node, typeof(ext_property_ans));
            }

            cnt = 0;
            foreach (var item in _right_list)
            {
                item.ans_no = ++cnt;
            }

            return true;
        }

        private bool saveRight()
        {
            string name = txtAnsName.Text.Trim();

            // 신규 상태에서 저장 버튼을 누른 경우
            if (_new2_flag)
            {
                if (_right_list == null)
                    _right_list = new List<ExtPropertyAnsVM>();

                var temp = _right_list.Find(p => p.ans_name == name);
                if (temp != null)
                {
                    MessageBox.Show(g.tr_get("C_Error_Duplicated_Answer"));
                    return false;
                }

                _right_item = new ExtPropertyAnsVM()
                {  
                    ext_property_ans_id = 0
                };

                if (!updateRight(true))
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }

                _right_list.Add(_right_item);
            }
            else
            {
                if (!updateRight(false))
                {
                    MessageBox.Show(g.tr_get("C_Error_Server"));
                    return false;
                }
            }
            return true;
        }
        #endregion

        #region delete 로직
        private async Task<bool> deleteLeft()
        {

            int delete_id = _left_item.ext_id;

            int rr1 = await g.webapi.delete("ext_property", delete_id);
            if (rr1 != 0)
            {
                MessageBox.Show(g.tr_get("C_Error_Server"));
                return false;
            }

            var node = g.ext_property_list.Find(p => p.ext_id == delete_id);
            if (node == null)
                return false;

            g.ext_property_list.Remove(node);

            var item = _left_list.Find(p => p.ext_id == delete_id);
            if (item == null)
                return false;

            _left_list.Remove(item);

            return true;
        }

        private bool deleteRight()
        {
            string name = _right_item.ans_name;

            var item = _right_list.Find(p => p.ans_name == name);
            if (item == null)
                return false;

            _right_list.Remove(item);

            return true;
        }
        #endregion
    }

    public static class ExtType
    {
        #region 내부 전용 스태틱 라이브러러
        public static string get_ext_type_name(string type)
        {
            switch (type)
            {
                case "T":
                    return I2MSR.Properties.Resources.M9_ExtendedProperty_Type_1;
                case "N":
                    return I2MSR.Properties.Resources.M9_ExtendedProperty_Type_2;
                case "D":
                    return I2MSR.Properties.Resources.M9_ExtendedProperty_Type_3;
                case "R":
                    return I2MSR.Properties.Resources.M9_ExtendedProperty_Type_4;
                case "L":
                    return I2MSR.Properties.Resources.M9_ExtendedProperty_Type_5;
                case "C":
                    return I2MSR.Properties.Resources.M9_ExtendedProperty_Type_6;
                default:
                    return "";
            };
        }

        public static string get_ext_type(string name)
        {
            if (name == I2MSR.Properties.Resources.M9_ExtendedProperty_Type_1)
                return "T";
            if (name == I2MSR.Properties.Resources.M9_ExtendedProperty_Type_2)
                return "N";
            if (name == I2MSR.Properties.Resources.M9_ExtendedProperty_Type_3)
                return "D";
            if (name == I2MSR.Properties.Resources.M9_ExtendedProperty_Type_4)
                return "R";
            if (name == I2MSR.Properties.Resources.M9_ExtendedProperty_Type_5)
                return "L";
            if (name == I2MSR.Properties.Resources.M9_ExtendedProperty_Type_6)
                return "C";
            return "-";
        }

        public static int get_ext_type_index(string type)
        {
            switch (type)
            {
                case "T":
                    return 1;
                case "N":
                    return 2;
                case "D":
                    return 3;
                case "R":
                    return 4;
                case "L":
                    return 5;
                case "C":
                    return 6;
                default:
                    return 0;
            };
        }

        #endregion
    }
                
    public class ExtTypeConverter : IValueConverter
    {
        #region 내부 전용 컨버터
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";
            string type = (string) value;

            return ExtType.get_ext_type_name(type);
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            if (value == null)
                return "";
            string name = (string)value;

            return ExtType.get_ext_type(name);
        }
        #endregion
    }
}
