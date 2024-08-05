using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Masking.Domain
{
    public class MaskingOptions
    {
        public List<string> MaskingPropNames {  get; set; } = new List<string>();
        public int MaskingCharCount { get; set; } = 1;
        public int MaxDepth { get; set; }
    }
}
