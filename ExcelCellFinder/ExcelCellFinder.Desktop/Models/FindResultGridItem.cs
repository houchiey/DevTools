using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCellFinder.Desktop.Models
{
    public class FindResultGridItem
    {
        public string FoundFilePath { get; set; } = "";
        public string FoundSheet { get; set; } = "";
        public string FoundCell { get; set; } = "";
    }
}
