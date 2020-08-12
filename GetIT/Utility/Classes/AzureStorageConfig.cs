using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetIT.Utility.Classes
{
    public class AzureStorageConfig
    {
        public string AccountName { get; set; }
        public string AccountKey { get; set; }
        public string ImageContainer { get; set; }
        public string ThumbnailContainer { get; set; }
    }
}
