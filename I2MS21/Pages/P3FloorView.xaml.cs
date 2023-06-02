using I2MS2.Library.Drawing;
using I2MS2.Models;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebApi.Models;
using I2MS2.UserControls.Drawing;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace I2MS2.Pages
{
    public partial class FloorLevelVM
    {
        public FloorLevelVM()
        {
            wall_list = new List<WallDraw>();
            wallc_list = new List<WallCornerDraw>();
            rk_vm_list = new List<RackDrawVM>();
        }

        public int floor_id { get; set; }
        public int building_id { get; set; }
        public int floor_no { get; set; }
        public Nullable<int> drawing_3d_id { get; set; }
        public int user_id { get; set; }
        public byte[] last_updated { get; set; }
        public string remarks { get; set; }
        public string floor_name { get; set; }
        public string drawing_3d_filepath { get; set; }

        public List<RackDrawVM> rk_vm_list { get; set; }
        public List<WallDraw> wall_list { get; set; }
        public List<WallCornerDraw> wallc_list { get; set; }
        
    }

    /// <summary>
    /// P3FloorView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class P3FloorView : Page
    {
        building use_building;
        Dictionary<int, FloorLevelVM> fl_lvm_dic = new Dictionary<int, FloorLevelVM>(); // 읽어온 층 관리를 위함 
        DrawingDataManager drawDataMgr;
        Drawing3D drawer3d;     // 3D 처리용
        Animation3D anim3d;     // 3D 애니메이션

        int selected_fl_no = -200;  // 이전 선택된 층 => 해당층 좌측 트리 색상 변경 -> 우측 도면 변경 처리  
        int focused_fl_no = -200;   // 포커스 되어진 층 -> 마우스 움직임에 따른 층 애니메이션 처리용 

        Boolean Z_on_moving;    // 해당층을 그리고 있음 플래그

        public P3FloorView()
        {
            InitializeComponent();

            drawDataMgr = new DrawingDataManager();
            drawer3d = new Drawing3D(_vpFloorList, _cavasFloorList);
            anim3d = new Animation3D();

            _ctlDrawingView3DMulti.camMoveEndEventToFloorView += new DrawingMuiltFloorView3D.camMoveEndHandler(camMoveZEndEvent);

            
        }
        // 로드 후 처리 없음 
        private void P3FloorView_Loaded(object sender, RoutedEventArgs e)
        {
        //    setBuilding(89101114);
        }
        // 빌딩 선택후 처리 -> 메인이나 자산뷰에서 호출됨 
        public void setBuilding(int building_id)
        {
            if (use_building != null) // 이전 데이터가 있는가?
            {
                //이전에 선택된 건물을 모두 지워준다
                if (selected_fl_no != -200)
                    _ctlDrawingView3DMulti.removeFloor(selected_fl_no);
                drawer3d.removeFloorCubeAll();
                fl_lvm_dic.Clear();

            }
            try
            {

                use_building = g.building_list.Find(at => at.building_id == building_id);
                List<floor> fl_list = g.floor_list.FindAll(at => at.building_id == building_id);
                foreach (var fl in fl_list)
                {
                    FloorLevelVM fl_lvm = makeFloorLevelVM(fl); // 도면 파일 위치 가져오기 
                    //_ctlDrawingView3DMulti.openDrawingFileMuiltFloor(fl_lvm.drawing_3d_filepath, fl_lvm.floor_no);

                    if (fl_lvm_dic.ContainsKey(fl_lvm.floor_no)) // 이미 열린 도면이 아니면  
                    {
                        continue; // 다음 도면으로 
                    }
                    // 층그리기 
                    //fl_lvm.wall_list = _ctlDrawingView3DMulti.openDrawingFile(fl_lvm.drawing_3d_filepath);
                    if (File.Exists(fl_lvm.drawing_3d_filepath)) // 파일이 존재하면 
                    {
                        List<WallDraw> wall_list = new List<WallDraw>();   //??
                        BinaryFormatter openformat = new BinaryFormatter();
                        FileStream openStream = new FileStream(fl_lvm.drawing_3d_filepath, FileMode.Open, FileAccess.Read);
                        List<WallDraw>[] open_w_list = new List<WallDraw>[4];

                        //2nd draw all walls
                        open_w_list = (List<WallDraw>[])openformat.Deserialize(openStream);
                        for (int i = 0; i < 4; i++)
                        {
                            foreach (var w in open_w_list[i])
                            {
                                WallDraw w_3d = drawDataMgr.makeWallVMDataFor3d(w);
                                fl_lvm.wall_list.Add(w_3d);
                            }

                        }

                        try
                        {
                            // 벽 코너 그리기 
                            List<WallCornerDraw>[] open_wc_list = (List<WallCornerDraw>[])openformat.Deserialize(openStream);
                            for (int i = 0; i < 4; i++)
                            {
                                foreach (var wc in open_wc_list[i])
                                {
                                    WallCornerDraw wc_3d = drawDataMgr.convertWallCornerDBto3D(wc);
                                    fl_lvm.wallc_list.Add(wc_3d);
                                }
                            }

                        }
                        catch (Exception ex) { }
                        openStream.Close();
                    }
                    fl_lvm_dic.Add(fl_lvm.floor_no, fl_lvm);
                    // 룸그리기 처리 
                    List<room> rm_list = g.room_list.FindAll(at => at.floor_id == fl_lvm.floor_id); // 랙이 없으면 안그려지는가????
                    if (rm_list.Count > 0) // 룸이 있으면
                    {
                        foreach (var rm in rm_list)
                        {
                            List<rack> rk_list = g.rack_list.FindAll(at => at.room_id == rm.room_id);
                            if (rk_list.Count == 0) continue;

                            foreach (var rk in rk_list) // 랙이 있으면 
                            {
                                RackDrawVM rk_vm = new RackDrawVM()
                                {
                                    point = drawDataMgr.get3DPoint_FromVMPoint(new Point(rk.pos_x ?? 0, rk.pos_y ?? 0)),
                                    size = drawDataMgr.get3DSize_FromVMSize(new Size(g.RACK_SIZE_WIDTH, g.RACK_SIZE_HEIGHT)),
                                    height = drawDataMgr.get3DValue_FromVMValue(g.RACK_HEIGHT),
                                    color = Colors.SkyBlue,
                                    alpha = 1,
                                    Z = 0
                                };
                                fl_lvm.rk_vm_list.Add(rk_vm);
                            }
                        }
                    }
                    // 층 
                    drawFloorList(fl_lvm);
                }

            }
            catch { }
        }
        // 좌측 층 리스트 그리기 
        public void drawFloorList(FloorLevelVM fl_lvm)
        {
            drawer3d.AddFloorCube(fl_lvm.floor_no);
        }
        // 층 그리기
        public void drawFloor(FloorLevelVM fl_lvm, Double z)
        {
            _ctlDrawingView3DMulti.drawFloor(fl_lvm.wall_list, fl_lvm.wallc_list,fl_lvm.rk_vm_list, z, fl_lvm.floor_no);
        }
        // 층그리기 종료
        public void camMoveZEndEvent(object obj)
        {
            Z_on_moving = false; // 층그리기 종료
        }
        // 선택된 층 보이기 
        private void selectFloor(ModelVisual3D fl_cube, int fl_no)
        {
            if ( (fl_cube != null)&& (fl_no != selected_fl_no)) // 모델이 있고 층이 다르면 변환 처리 
            {
                if (Z_on_moving == false) // 수행중에 처리 안됨
                {
                    Z_on_moving = true; // 층그리기 시작
                    //cube의 색깔 변경
                    drawer3d.changeColorFloorCube(fl_cube, new SolidColorBrush(Color.FromArgb(0xFF, 0x12, 0xD3, 0xF2)));

                    //Cube 확대
                    Point3D p = new Point3D(0, 0, fl_no * 200);
                    anim3d.model3d_ScaleAnimXY(fl_cube, 1, 1.05, p);

                    //해당 층의 도면 표시
                    //Double z = mainCamera.Position.Z + mainCamera.LookDirection.Z - 3000;

                    Double move_z = 1000;
                    if (selected_fl_no > fl_no)
                        move_z = -1000;
                    Double z = _ctlDrawingView3DMulti.mainCamera.Position.Z + _ctlDrawingView3DMulti.mainCamera.LookDirection.Z;
                    drawFloor(fl_lvm_dic[fl_no], z + move_z);


                    //중앙화면 카메라 이동을 통해 나타나는 효과 표현
                    //_ctlDrawingView3DMulti.camToUpFloor(1000);
                    _ctlDrawingView3DMulti.changeFloor(move_z, selected_fl_no);


                    //이전층 삭제
                    //_ctlDrawingView3DMulti.removeFloor(selected_fl_no);

                    //선택된 fl_no를 기억
                    selected_fl_no = fl_no;

                    //Point3D p = new Point3D(0, 0, fl_no * 200);
                    //Point3D to_p = new Point3D(0, 500, fl_no * 200);
                    //anim3d.model3d_MoveAnimXY(fl_cube, p, to_p);
                    //anim3d.model3d_MoveXY(fl_cube, p, to_p);
                }
            }
        }
        // 기존 선택된 층을 회색으로 처리 
        private void UnSelectFloor(ModelVisual3D fl_cube, int fl_no)
        {
            if (fl_cube != null)
            {
                //_ctlDrawingView3DMulti.removeFloor(fl_no);
                drawer3d.changeColorFloorCube(fl_cube, Brushes.DarkGray);
                Point3D p = new Point3D(0, 0, fl_no * 200);
                anim3d.model3d_ScaleAnimXY(fl_cube, 1.05, 1, p);
                //Point3D p = new Point3D(0, 500, fl_no * 200);
                //Point3D to_p = new Point3D(0, 0, fl_no * 200);
                //anim3d.model3d_MoveAnimXY(fl_cube, p, to_p);
                //anim3d.model3d_MoveXY(fl_cube, p, to_p);
            }
        }
        // 마우스가 있으므로 포커스 유지 
        private void focusingFloor(ModelVisual3D fl_cube,int fl_no)
        {
            if (fl_cube != null)
            {
                Point3D p = new Point3D(0,0,fl_no*200 );
                anim3d.model3d_ScaleAnimXY(fl_cube, 1, 1.05, p);
            }
        }
        // 마우스가 빠졌으므로 포거스를 하지 않는다. 
        private void unFocusingFloor(ModelVisual3D fl_cube,int fl_no)
        {
            if (fl_cube != null)
            {
                Point3D p = new Point3D(0, 0, fl_no * 200);
                anim3d.model3d_ScaleAnimXY(fl_cube, 1.05, 1,p);
            }
        }
        // 층 데이터 만들기 - 도면파일 위치 가져오기 
        private FloorLevelVM makeFloorLevelVM(floor fl)
        {
            FloorLevelVM fl_lvm = new FloorLevelVM()
            {
                floor_id = fl.floor_id,
                building_id = fl.building_id,
                floor_no = fl.floor_no,
                floor_name = fl.floor_name,
                drawing_3d_id = fl.drawing_3d_id,
                user_id = fl.user_id,
                last_updated = fl.last_updated,
                remarks = fl.remarks
            };
            // 3D 도면파일 가져오기
            drawing_3d dr3d = g.drawing_3d_list.Find(at => at.drawing_3d_id == fl_lvm.drawing_3d_id);
            if(dr3d != null)
                fl_lvm.drawing_3d_filepath = string.Format("{0}drawing_3d/{1}", g.CLIENT_IMAGE_PATH, dr3d.file_name);
            return fl_lvm;
        }
        // 좌측 층 선택 처리 
        private void _vpFloorList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Z_on_moving == true) return; // 층그리기 하고 있으면 리턴 

            Point location = e.GetPosition(_vpFloorList);

            ModelVisual3D m3d;
            HitTestResult ret = VisualTreeHelper.HitTest(_vpFloorList, location);
            if (ret == null) return;
            
            if (ret.VisualHit is ModelVisual3D)
            {
                m3d = (ModelVisual3D)ret.VisualHit;
                if (m3d != null)
                {
                    int fl_no = drawer3d.getFloorCubeNo(m3d);
                    //Console.WriteLine("FIND!!!-{0}", fl_no);

                    if (fl_no == selected_fl_no) return;

                    if (selected_fl_no != -200) // 기존 선택된 층이 있으면 선택 해제
                        UnSelectFloor(drawer3d.getFloorCube(selected_fl_no), selected_fl_no);
                    selectFloor(m3d, fl_no);
                    // Console.WriteLine("selected={0}", fl_no);
                   return;
                }
            }
        }
        // 좌측 리스트 마우스 무브 처리 
        private void _vpFloorList_MouseMove(object sender, MouseEventArgs e)
        {
            Point location = e.GetPosition(_vpFloorList);
            ModelVisual3D result = GetHitTestResult(location);
            if (result == null)
            {
                return;
            }

            if (result is ModelVisual3D)
            {
                ModelVisual3D fl_m3d = (ModelVisual3D)result;
                int fl_no = drawer3d.getFloorCubeNo(fl_m3d); // 좌측 리스트에서 마우스 위에 있는 층번호 가져오기 
                //Console.WriteLine("FIND!!!-{0}", fl_no);
                if (fl_no == focused_fl_no) return;

                //Console.WriteLine("f{0}=s{1}", focused_fl_no, selected_fl_no);
                if ((focused_fl_no != -200) && (focused_fl_no != selected_fl_no))
                    unFocusingFloor(drawer3d.getFloorCube(focused_fl_no), focused_fl_no);

                if (fl_no != selected_fl_no)
                    focusingFloor(fl_m3d, fl_no);
                  //  Console.WriteLine("focused={0}", fl_no);
                focused_fl_no = fl_no;
                return;
            }
        }
        // 좌측 층 리스트 영역에서 마우스가 빠진 경우   
        private void _vpFloorList_MouseLeave(object sender, MouseEventArgs e)
        {
            if ((focused_fl_no != -200) && (focused_fl_no != selected_fl_no))
            {
                unFocusingFloor(drawer3d.getFloorCube(focused_fl_no), focused_fl_no);
            }
        }
        // 좌측 층 리스트 영역으로 들어온 경우  
        ModelVisual3D GetHitTestResult(Point location)
        {
            HitTestResult result = VisualTreeHelper.HitTest(_vpFloorList, location);
            if (result != null && result.VisualHit is ModelVisual3D)
            {
                ModelVisual3D visual = (ModelVisual3D)result.VisualHit;
                return visual;
            }
            return null;
        }
        #region // 사용하지 않음
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (g.selected_building_id != -1)
            {
                setBuilding(g.selected_building_id);
            }
        }
        #endregion
    }
}
