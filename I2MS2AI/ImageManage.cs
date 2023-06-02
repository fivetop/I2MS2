using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using WebApiClient;
using I2MS2.Models;
using I2MS2.Windows;

namespace I2MS2.Library
{
    class ImageManage
    {
        string host_string = g.host_string;

        WebApiClientClass webapi;
        ProgressBarDialog progressbar_dialog;

       
        public ImageManage()
        {
            string web_api_uri_string = "http://" + host_string + ":5000/";
            webapi = new WebApiClientClass(web_api_uri_string);
        }



        public async Task<Boolean> DelImageToServer( string sub_dir, string file_name)
        {
            Task<int> t1 = webapi.deleteFile(sub_dir, file_name);
            int ret = await t1;
            if (ret == 0)
            {
                string file_path = string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH, sub_dir, file_name);
                if (File.Exists(file_path) == true)
                {
                    try
                    {
                        File.Delete(file_path);
                        return true;
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("{0}:{1}", ex.HResult, ex.Message);
                        return false;
                    }
                    
                }
                return false;
            }
            else
                return false;
            
        }

        public async Task<string> addImageToServer(string file_path, string sub_dir, string file_name)
        {
            //서버에 이미지를 등록해준다
            int UPLOAD_BUFF_SIZE = 40960;
            progressbar_dialog = new ProgressBarDialog(webapi);

            Task<TransferResult> t1 = webapi.uploadFile(sub_dir, file_path, UPLOAD_BUFF_SIZE);
            progressbar_dialog.ShowDialog();

            TransferResult ret = await t1;
            //Console.WriteLine("result_code = {0}:{1}", r.result_code,r.server_file_name);

            
            if (ret.result_code == 0)
            {
                File.Copy(file_path, string.Format("{0}{1}/{2}", g.CLIENT_IMAGE_PATH,sub_dir, ret.server_file_name));
                return ret.server_file_name;
            }
            else
                return null;
        }




        public async Task<int> addImageToDb(string server_file_name, string sub_dir, string owner_name)
        {
            int image_type_id;
            switch(sub_dir)
            {
                case "map":
                    image_type_id = 1160001;
                    break;
                case "site":
                    image_type_id = 1160002;
                    break;
                case "building":
                    image_type_id = 1160003;
                    break;
                case "drawing":
                    image_type_id = 1160004;
                    break;
                case "catalog":
                    image_type_id = 1160005;
                    break;
                case "rack_222":
                    image_type_id = 1160006;
                    break;
                case "rack_444":
                    image_type_id = 1160007;
                    break;
                case "rack_888":
                    image_type_id = 1160008;
                    break;
                case "link":
                    image_type_id = 1160009;
                    break;
                case "etc":
                    image_type_id = 1160010;
                    break;
                case "icon_16":
                    image_type_id = 1160011;
                    break;
                case "icon_32":
                    image_type_id = 1160012;
                    break;
                case "icon_64":
                    image_type_id = 1160013;
                    break;
                case "icon_128":
                    image_type_id = 1160014;
                    break;
                default:
                    image_type_id = 1160001;
                    break;

            }


            // 선택된 이미지정보 만들기
            image tmp_img = new image();
            tmp_img.file_name = server_file_name;
            tmp_img.image_name = string.Format("{0} 1 ", owner_name);
            tmp_img.image_type_id = 1160001;
            tmp_img.size_x = 100;
            tmp_img.size_y = 100;
            tmp_img.deletable = "Y";

            // 선택된 이미지정보를 iamage DB에 등록
            image ret_img = (image)await webapi.post("image", tmp_img, typeof(image));

            g.image_list.Add(ret_img);

            //전역변수 sp_image_list에 추가하기위한 sp_list_image_Result 정보 만들기
            sp_list_image_Result tmp_spimage = new sp_list_image_Result();
            tmp_spimage.file_name = server_file_name;
            tmp_spimage.folder_name = sub_dir;
            tmp_spimage.image_id = ret_img.image_id;
            tmp_spimage.image_type_id = image_type_id;
            tmp_spimage.image_type_name = sub_dir;
            tmp_spimage.size_x = 100;
            tmp_spimage.size_y = 100;

            //전역변수 sp_image_list에 추가된 이미지 정보를 삽입
            g.sp_image_list.Add(tmp_spimage);

            return ret_img.image_id;
        }

        public async Task<int> modifyImageToDb(int image_id, string server_file_name)
        {
            //해당 region의 이미지 정보 찾기
            sp_list_image_Result t_sp_image = (sp_list_image_Result)g.sp_image_list.First(at => at.image_id == image_id);
#if false
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
#else
            image tmp_img = (image)g.image_list.First(at => at.image_id == image_id);
            tmp_img.file_name = server_file_name;
#endif

            //DB에 이미지 정보 수정   
            int ret = await webapi.put("image", tmp_img.image_id, tmp_img, typeof(image));

            //전역 이미지 리스트 정보 수정
            t_sp_image.file_name = server_file_name;

            return 1;
        }


        public async void delImageToDb(int image_id, string sub_dir, string selected_file_name)
        {
            //DB 에서 해당 이미지 데이터를 삭제 한다
            int ret = await webapi.delete("image", image_id);

            //전역 이미지리스트에서 이미지 데이터를 삭제 한다

#if true
            sp_list_image_Result tmp_sp_img = (sp_list_image_Result)g.sp_image_list.First(at => at.image_id == image_id);
            string image_file_name = tmp_sp_img.file_name;
            g.sp_image_list.Remove(tmp_sp_img);

            image tmp_img = (image)g.image_list.First(at => at.image_id == image_id);
            g.image_list.Remove(tmp_img);
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
        }


    }
}
