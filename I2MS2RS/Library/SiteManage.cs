using I2MS2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Models;
using WebApiClient;

namespace I2MS2.Library
{
    class SiteManage
    {
        string host_string = g.host_string;

        WebApiClientClass webapi;


        public SiteManage()
        {
            //string web_api_uri_string = "http://" + host_string + ":5000/";
            webapi = new WebApiClientClass(g.web_api_uri_string);
        }


        public async Task<site> addSiteToDb(string site_name,string remarks, int img_id, int region2_id)
        {
            // region1 정보를 DB에 추가한다
            site st = new site();
            //tmp_region1.region1_id = 0;
            st.user_id = 201406;
            st.site_name = site_name;
            st.remarks = remarks;
            st.region2_id = region2_id;
            //이미지 정보를 등록하고 받은 ID를 region 데이터의 image_id 에 저장
            st.site_image_id = img_id;


            //region 데이터를 DB에 추가
            site ret_st = (site)await webapi.post("site", st, typeof(site));

            //전역변수 regionDataList에 해당 데이터를 추가한다
            g.site_list.Add(ret_st);
            await reload_site_environment_list();
            return ret_st;
        }

        public async Task<bool> reload_site_environment_list()
        {
            var se_list = (List<site_environment>)await g.webapi.getList("site_environment", typeof(List<site_environment>));
            if (se_list != null)
                g.site_environment_list = se_list;
            return se_list != null;
        }

        public async Task<bool> delSiteToDb(int site_id)
        {
            site st = (site)g.site_list.First(at => at.site_id == site_id);

            //DB  region1 을 삭제한다
            int ret2 = await webapi.delete("site", site_id);

            //전역 region1리스트 에서 정보를 삭제 한다
            g.site_list.Remove(st);
            await reload_site_environment_list();

            return true;
        }

        public async Task<site> modifySiteToDb(int site_id, string site_name, string remarks,
                                                   Boolean changed_region_name)
        {
            //Console.WriteLine("==== changed_region_POSITION ====");
            site st = (site)g.site_list.First(at => at.site_id == site_id);
            st.site_name = site_name;
            st.remarks = remarks;

            // region1 정보를 수정하여 DB에 저장한다
            int ret = await webapi.put("site", st.site_id, st, typeof(site));

            if (changed_region_name == true)
            {
                //Console.WriteLine("==== changed_region_NAME ====");

                //이름이 수정된경우는 DB에서 image쪽도 수정해 줘야한다
                //해당 region의 이미지 정보 찾기
                sp_list_image_Result t_sp_image = null;
                try
                {
                    t_sp_image = (sp_list_image_Result)g.sp_image_list.First(at => at.image_id == st.site_image_id);
                }
                catch { }
                if (t_sp_image == null) return st; // 이미지 없음 처리하고 리턴 

                image tmp_image = new image();
                tmp_image.image_id = t_sp_image.image_id;
                tmp_image.image_type_id = t_sp_image.image_type_id;
                tmp_image.remarks = t_sp_image.remarks;
                tmp_image.size_x = t_sp_image.size_x;
                tmp_image.size_y = t_sp_image.size_y;
                tmp_image.drawing_3d_id = t_sp_image.drawing_3d_id;
                tmp_image.deletable = "Y"; //  t_sp_image.deletable;  romee 2/2 error 
                tmp_image.file_name = t_sp_image.file_name;

                tmp_image.image_name = string.Format("{0} 1 ", site_name);


                //DB에 이미지 정보 수정   
                int ret2 = await webapi.put("image", tmp_image.image_id, tmp_image, typeof(image));

                //전역 이미지 리스트 수정
                t_sp_image.image_name = string.Format("{0} 1 ", site_name);

            }
            return st;

        }

    }
}
