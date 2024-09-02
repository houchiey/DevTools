using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCellFinder.Core.Result.Interface
{
    public interface IFoundCell
    {
        string SheetName { get; protected set; }
        int RowNumber { get; protected set; }
        int ColumnNumber { get; protected set; }
        string Column { get; protected set; }

        public string ToString();
    }
}
