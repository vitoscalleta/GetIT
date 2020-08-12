using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetIT.DatabaseLayer.Dto
{
    public class ProductImages
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ImageFileName { get; set; }

        public DateTime UploadTimeStamp { get; set; }

    }
}
