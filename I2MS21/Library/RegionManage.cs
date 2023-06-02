
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Effects;

using I2MS2.UserControls;
using I2MS2.Models;
using WebApi.Models;
using System.Windows.Controls;
using WebApiClient;
using System.IO;
using System.Windows.Input;

namespace I2MS2.Library
{
    // 리전 관리 
    class RegionManage
    {
        string host_string = g.host_string;
    
        WebApiClientClass webapi;
       // ProgressBarDialog progressbar_dialog;


        public Size map_size;
        public Size parent_map_size;

        public RegionManage(Size _parent_map_size)
        {
            string web_api_uri_string = "http://" + host_string + ":5000/";
            webapi = new WebApiClientClass(web_api_uri_string);

            parent_map_size = _parent_map_size;
        }

        public void setMapSize(double width, double height)
        {
            map_size = new Size(width, height);
        }

        public regionFlagControl makeRegion1Flag(string name, Point ct)
        {
            regionFlagControl region_flag = new regionFlagControl();
            region_flag.Width = 50;
            region_flag.Height = 50;
            region_flag.HorizontalAlignment = HorizontalAlignment.Left;
            region_flag.VerticalAlignment = VerticalAlignment.Top;
            region_flag.Name = name;
            Thickness tmp_margin = new Thickness(ct.X, ct.Y, 0, 0);
            region_flag.Margin = tmp_margin;

            DropShadowEffect tmp_eff = new DropShadowEffect();
            region_flag.Effect = tmp_eff;

            //Mouse Enter, Leave시에 이벤트를 설정한다
            //region1Flag.MouseEnter += Region1Flag_MouseEnterEvent;
            //region1Flag.MouseLeave += Region1Flag_MouseLeaveEvent;
            return region_flag;
        }

        public string getImageFileName(int region_image_id)
        {
            foreach (var at in g.sp_image_list)
            {
                if (at.image_id == region_image_id)
                {
                    return at.file_name;

                }
            }
            return null;
        }


        public Boolean checkPointInMap(Point p)
        {
            if (((p.X > 0) && (p.Y > 0))
                    && ((p.X < map_size.Width) && (p.Y < map_size.Height)))
                return true;
            else
                return false;
        }


        public Point changePosition_SmalMapToDefaultPoint(double in_x, double in_y)
        {
            double out_x = in_x * (g.default_map_size.Width / map_size.Width);
            double out_y = in_y * (g.default_map_size.Height / map_size.Height);

            return new Point(out_x, out_y);
        }

        public Point changePosition_DefaultPointToSmalMap(double in_x, double in_y)
        {
            double out_x = in_x * (map_size.Width / g.default_map_size.Width);
            double out_y = in_y * (map_size.Height / g.default_map_size.Height);

            return new Point(out_x, out_y);
        }


#if false

        //서버와 로컬에서 파일을 삭제 한다
        public async Task<Boolean> DelImageToServer(string file_name)
        {
            //  

            Task<int> t1 = webapi.deleteFile("map", file_name);
            int ret = await t1;
            if (ret == 0)
            {
                File.Delete(string.Format("{0}{1}", g.CLIENT_IMAGE_PATH, file_name));
                return true;
            }
            else
                return false;

        }

        public async Task<string> addImageToServer(string file_path, string file_name)
        {
            //서버에 이미지를 등록해준다
            int UPLOAD_BUFF_SIZE = 40960;
            progressbar_dialog = new ProgressBarDialog(webapi);

            Task<TransferResult> t1 = webapi.uploadFile("map", file_path, UPLOAD_BUFF_SIZE);
            progressbar_dialog.ShowDialog();

            TransferResult ret = await t1;
            //Console.WriteLine("result_code = {0}:{1}", r.result_code,r.server_file_name);


            if (ret.result_code == 0)
            {
                File.Copy(file_path, string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, "map", ret.server_file_name));
                return ret.server_file_name;
            }
            else
                return null;
        }


        public async Task<int> addImageToDb(string server_file_name, string region_name)
        {
            // 선택된 이미지정보 만들기
            image tmp_img = new image();
            tmp_img.file_name = server_file_name;
            tmp_img.image_name = string.Format("{0} 1 ", region_name);
            tmp_img.image_type_id = 1160001;
            tmp_img.size_x = 100;
            tmp_img.size_y = 100;
            tmp_img.deletable = "Y";

            // 선택된 이미지정보를 iamage DB에 등록
            image ret_img = (image)await webapi.post("image", tmp_img, typeof(image));

            //전역변수 sp_image_list에 추가하기위한 sp_list_image_Result 정보 만들기
            sp_list_image_Result tmp_spimage = new sp_list_image_Result();
            tmp_spimage.file_name = server_file_name;
            tmp_spimage.folder_name = "map";
            tmp_spimage.image_id = ret_img.image_id;
            tmp_spimage.image_type_id = 1160001;
            tmp_spimage.image_type_name = "map";
            tmp_spimage.size_x = 100;
            tmp_spimage.size_y = 100;

            //전역변수 sp_image_list에 추가된 이미지 정보를 삽입
            g.sp_image_list.Add(tmp_spimage);

            return ret_img.image_id;
        }

        public async Task<int> modifyImageToDb(int region_image_id, string server_file_name)
        {
            //해당 region의 이미지 정보 찾기
            sp_list_image_Result t_sp_image = (sp_list_image_Result)g.sp_image_list.First(at => at.image_id == region_image_id);
            image tmp_img = new image();
            tmp_img.file_name = server_file_name;
            tmp_img.image_id = t_sp_image.image_id;
            tmp_img.image_name = t_sp_image.image_name;
            tmp_img.image_type_id = t_sp_image.image_type_id;
            tmp_img.remarks = t_sp_image.remarks;
            tmp_img.size_x = t_sp_image.size_x;
            tmp_img.size_y = t_sp_image.size_y;
            tmp_img.drawing_3d_id = t_sp_image.drawing_3d_id;
            tmp_img.deletable = t_sp_image.deletable;


            //DB에 이미지 정보 수정   
            int ret = await webapi.put("image", tmp_img.image_id, tmp_img, typeof(image));

            //전역 이미지 리스트 정보 수정
            t_sp_image.file_name = server_file_name;

            return 1;
        }
#if false

        public async void delRegion1ImageToDb(int region_id, string selected_file_name)
        {
            //DB 에서 해당 이미지 데이터를 삭제 한다
            region1 r1 = (region1)g.region1_list.First(at => at.region1_id == region_id);

            // DBAcess Da = new DBAcess();
            // var t1 = Da.DelImageDataToDB((int)r1.region1_image_id);
            int ret = await webapi.delete("image", (int)r1.region1_image_id);

            //전역 이미지리스트에서 이미지 데이터를 삭제 한다

#if true
            sp_list_image_Result tmp_sp_img = (sp_list_image_Result)g.sp_image_list.First(at => at.image_id == r1.region1_image_id);
            string image_file_name = tmp_sp_img.file_name;
            g.sp_image_list.Remove(tmp_sp_img);
#else
            foreach (var at in g.sp_image_list)
            {
                if (at.image_id == r1.region1_image_id)
                {
                    string image_file_name = at.file_name;
                    g.sp_image_list.Remove(at);
                }
            }
                            
#endif
            //- 로컬에서 파일을 삭제한다
            try
            {
                File.Delete(string.Format("{0}map/{1}", g.CLIENT_IMAGE_PATH, selected_file_name));
            }
            catch (Exception e)
            {
                Console.WriteLine("FileDelete Err:{0}", e.Message);
            }
        }
        
#endif

        public async void delImageToDb(int image_id, string selected_file_name)
        {
            //DB 에서 해당 이미지 데이터를 삭제 한다
            int ret = await webapi.delete("image", image_id);

            //전역 이미지리스트에서 이미지 데이터를 삭제 한다

#if true
            sp_list_image_Result tmp_sp_img = (sp_list_image_Result)g.sp_image_list.First(at => at.image_id == image_id);
            string image_file_name = tmp_sp_img.file_name;
            g.sp_image_list.Remove(tmp_sp_img);
#else
            foreach (var at in g.sp_image_list)
            {
                if (at.image_id == r1.region1_image_id)
                {
                    string image_file_name = at.file_name;
                    g.sp_image_list.Remove(at);
                }
            }
                            
#endif
            //- 로컬에서 파일을 삭제한다
            try
            {
                File.Delete(string.Format("{0}map/{1}", g.CLIENT_IMAGE_PATH, selected_file_name));
            }
            catch (Exception e)
            {
                Console.WriteLine("FileDelete Err:{0}", e.Message);
            }
        }
        
#endif

        public async Task<region1> addRegion1ToDb(string region_name, int img_id, Point selected_point)
        {
            // region1 정보를 DB에 추가한다
            region1 tmp_region1 = new region1();
            //tmp_region1.region1_id = 0;
            tmp_region1.user_id = 201406;
            tmp_region1.region1_name = region_name;

            //이미지 정보를 등록하고 받은 ID를 region 데이터의 image_id 에 저장
            tmp_region1.region1_image_id = img_id;

            //  region   
            //Point regionP = changeEllipsePosition_ChildToParent(selected_point.X, selected_point.Y);
            Point regionP = changePosition_SmalMapToDefaultPoint(selected_point.X, selected_point.Y);
            tmp_region1.pos_x = (int)regionP.X;
            tmp_region1.pos_y = (int)regionP.Y;


            //region 데이터를 DB에 추가
            region1 ret_r = (region1)await webapi.post("region1", tmp_region1, typeof(region1));
            if (ret_r == null)
                return null;

            //전역변수 regionDataList에 해당 데이터를 추가한다
            g.region1_list.Add(ret_r);

            int region1_id = ret_r.region1_id;
            int location_id = Etc.get_location_id_by_region1_id(region1_id);
            g.signalr.send_location_to_signalr(location_id, eAction.eAdd);

            return ret_r;

        }
        public async void delRegion1DataToDb(int region_id)
        {
            region1 r1 = (region1)g.region1_list.First(at => at.region1_id == region_id);

            //DB  region1 을 삭제한다
            // Task.Run(() => Da.DelRegion1DataToDB(selected_region_id)).Wait();
            int ret2 = await webapi.delete("region1", region_id);

            //전역 region1리스트 에서 정보를 삭제 한다
            g.region1_list.Remove(r1);

            int location_id = Etc.get_location_id_by_region1_id(region_id);
            g.signalr.send_location_to_signalr(location_id, eAction.eRemove);
        }

        public async Task<region1> modifyRegion1ToDb(int region_id, string selected_name,Point selected_point, Boolean changed_region_name)
        {
            //Console.WriteLine("==== changed_region_POSITION ====");
            region1 r1 = (region1)g.region1_list.First(at => at.region1_id == region_id);
            r1.region1_name = selected_name;
            Point r_point = changePosition_SmalMapToDefaultPoint(selected_point.X, selected_point.Y);
            r1.pos_x = (int)r_point.X;
            r1.pos_y = (int)r_point.Y;
                        
                        
            // region1 정보를 수정하여 DB에 저장한다
            int ret = await webapi.put("region1", r1.region1_id, r1, typeof(region1));

            if (changed_region_name == true)
            {
                //Console.WriteLine("==== changed_region_NAME ====");

                //이름이 수정된경우는 DB에서 image쪽도 수정해 줘야한다
                //해당 region의 이미지 정보 찾기
                sp_list_image_Result t_sp_image = (sp_list_image_Result)g.sp_image_list.First(at => at.image_id == r1.region1_image_id);
                image tmp_image = new image();
                tmp_image.image_id = t_sp_image.image_id;
                tmp_image.image_type_id = t_sp_image.image_type_id;
                tmp_image.remarks = t_sp_image.remarks;
                tmp_image.size_x = t_sp_image.size_x;
                tmp_image.size_y = t_sp_image.size_y;
                tmp_image.drawing_3d_id = t_sp_image.drawing_3d_id;
                tmp_image.deletable = t_sp_image.deletable;
                tmp_image.file_name = t_sp_image.file_name;

                tmp_image.image_name = string.Format("{0} 1 ", selected_name);


                //DB에 이미지 정보 수정   
                int ret2 = await webapi.put("image", tmp_image.image_id, tmp_image, typeof(image));

                //전역 이미지 리스트 수정
                t_sp_image.image_name = string.Format("{0} 1 ", selected_name);

                int region1_id = r1.region1_id;
                int location_id = Etc.get_location_id_by_region1_id(region_id);
                g.signalr.send_location_to_signalr(location_id, eAction.eModify);
            }
            return r1;
            
        }


        public async Task<region2> addRegion2ToDb(int region1_id, string region_name,  Point selected_point)
        {
            // region1 정보를 DB에 추가한다
            region2 r = new region2();
            //tmp_region1.region1_id = 0;
            r.user_id = 201406;
            r.region1_id = region1_id;
            r.region2_name = region_name;

            //  region   
            Point regionP = changePosition_SmalMapToDefaultPoint(selected_point.X, selected_point.Y);
            r.pos_x = (int)regionP.X;
            r.pos_y = (int)regionP.Y;


            //region 데이터를 DB에 추가
            region2 ret_r = (region2)await webapi.post("region2", r, typeof(region2));
            if (ret_r == null)
                return null;

            //전역변수 regionDataList에 해당 데이터를 추가한다
            g.region2_list.Add(ret_r);

            int region2_id = ret_r.region2_id;
            int location_id = Etc.get_location_id_by_region2_id(region2_id);
            g.signalr.send_location_to_signalr(location_id, eAction.eAdd);

            return ret_r;

        }

        public async void delRegion2DataToDb(int region2_id)
        {
            region2 r = (region2)g.region2_list.First(at => at.region2_id == region2_id);

            //DB  region1 을 삭제한다
            int ret2 = await webapi.delete("region2", region2_id);
            if (ret2 != 0)
                return;

            //전역 region1리스트 에서 정보를 삭제 한다
            g.region2_list.Remove(r);

            int location_id = Etc.get_location_id_by_region2_id(region2_id);
            g.signalr.send_location_to_signalr(location_id, eAction.eRemove);
        }

        public async Task<region2> modifyRegion2ToDb(int region_id, string selected_name, Point selected_point)
        {
            //Console.WriteLine("==== changed_region_POSITION ====");
            region2 r = (region2)g.region2_list.First(at => at.region2_id == region_id);
            r.region2_name = selected_name;
            Point r_point = changePosition_SmalMapToDefaultPoint(selected_point.X, selected_point.Y);
            r.pos_x = (int)r_point.X;
            r.pos_y = (int)r_point.Y;


            // region1 정보를 수정하여 DB에 저장한다
            int ret = await webapi.put("region2", r.region2_id, r, typeof(region2));
            if (ret != 0)
                return null;

            int region2_id = r.region2_id;
            int location_id = Etc.get_location_id_by_region2_id(region2_id);
            g.signalr.send_location_to_signalr(location_id, eAction.eModify);

            return r;

        }


    }
}
