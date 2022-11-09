using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public sealed class Image
    {
        public int ImageID { get; set; } 
        public string ImageTitle { get; set; }
        public byte[] ImageData { get; set; }
    }
}
