using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTools.Masking.Domain.Masker
{
    internal interface IMasker
    {
        string Mask(string original);
    }
}
