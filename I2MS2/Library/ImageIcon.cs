using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I2MS2.Models;

namespace I2MS2.Library
{
    static public class ImageIcon
    {

        static private Dictionary<int, int> _link80_image_dic = new Dictionary<int, int>() { 
        {3110, 2120001}, // ic
        {3130, 2120002}, // i-patch panel
        {3140, 2120003}, // i-fdf
        {3310, 2120004}, // backbone switch & router
        {3320, 2120005}, // l2 switch 
        {3330, 2120006}, // l3 switch
        {3340, 2120007}, // pc
        {3350, 2120009}, // server
        {3360, 2120010}, // workstation
        {3370, 2120011}, // storage
        {3410, 2120014}, // cp
        {3420, 2120015}, // face plate
        {3430, 2120016}, // mutoa box
        {3440, 2120017}, // pp
        {3450, 2120018}, // fdf
        {3380, 2120012}, // phone
        {3390, 2120013}, // printer
//        {3500, 2120099}, // etc            GS_DEL
        };

        // key=catalog_group_id, value=image_id
        static private Dictionary<int, int> _image_dic = new Dictionary<int, int>() { 
        {3210, 2110005}, // rack device
        {3120, 2110022}, // energy box
        {3110, 2110006}, // ic
        {3130, 2110007}, // i-patch panel
        {3140, 2110007}, // i-fdf
        {3310, 2110021}, // backbone switch & router
        {3320, 2110008}, // l2 switch 
        {3330, 2110020}, // l3 switch
        {3340, 2110010}, // pc
        {3350, 2110012}, // server
        {3360, 2110013}, // workstation
        {3370, 2110014}, // storage
        {3410, 2110015}, // cp
        {3420, 2110016}, // face plate
        {3430, 2110017}, // mutoa box
        {3440, 2110018}, // pp
        {3450, 2110018}, // fdf
        {3380, 2110023}, // phone
        {3390, 2110024}, // printer
//        {3500, 2110099}, // etc
        };

        static public int get_link80_id_by_catalog_group_id(int catalog_group_id)
        {
            int result = 2120099;
            int id = catalog_group_id / 10 * 10;
            if (_link80_image_dic.TryGetValue(id, out result))
                return result;

            var cg = g.catalog_group_list.Find(p => p.catalog_group_id == catalog_group_id);
            if (cg == null)
                return 0;

            id = cg.level2_catalog_group_id ?? 0;
            if (_link80_image_dic.TryGetValue(id, out result))
                return result;

            id = cg.level1_catalog_group_id ?? 0;
            _link80_image_dic.TryGetValue(id, out result);

            return result;
        }
        static public int get_icon_id_by_catalog_group_id(int catalog_group_id)
        {
            int result = 2110099;
            int id = catalog_group_id / 10 * 10;
            if (_image_dic.TryGetValue(id, out result))
                return result;

            var cg = g.catalog_group_list.Find(p => p.catalog_group_id == catalog_group_id);
            if (cg == null)
                return 0;

            id = cg.level2_catalog_group_id ?? 0;
            if (_image_dic.TryGetValue(id, out result))
                return result;

            id = cg.level1_catalog_group_id ?? 0;
            _image_dic.TryGetValue(id, out result);

            return result;
        }

        static public int get_hub_icon_id()
        {
            return 2110019;
        }

        static public int get_etc_icon_id()
        {
            return 2110099;
        }

        static public string get_icon_file_path(int image_id)
        {
            string file_name = "etc_16.png";
            var im = g.image_list.Find(e => e.image_id == image_id);
            if (im != null)
                file_name = im.file_name;

            return string.Format("/I2MS2;component/Icons/{0}", file_name);
        }

    }
}
