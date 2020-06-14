using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetIT.Utility.Classes
{
    public static class Enumerators
    {
        public enum ProductCategories
        {
            Components,
            Accessories,
            Builds
        };

        public enum ProductSubCategory
        {
            Monitors,
            Motherboards,
            Processors,
            RAM,
            Storage,
            Keyboard,
            Mouse
        };
    }
}
