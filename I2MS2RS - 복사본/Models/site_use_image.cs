using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I2MS2.Models
{
    
    public partial class site_use_image
    {
        public int number { get; set; }
        public int site_id { get; set; }
        public int region2_id { get; set; }
        public string site_name { get; set; }
        public Nullable<int> site_image_id { get; set; }
        public int user_id { get; set; }
        public byte[] last_updated { get; set; }
        public string remarks { get; set; }

        public string site_image_file_name { get; set; }
        public string site_image_file_path { get; set; }
    }
}
