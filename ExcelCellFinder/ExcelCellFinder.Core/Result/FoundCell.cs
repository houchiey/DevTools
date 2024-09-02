using ClosedXML.Excel;
using ExcelCellFinder.Core.Result.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCellFinder.Core.Result
{
    internal class FoundCell : IFoundCell
    {
        internal FoundCell(IXLCell cell) 
        {
            SheetName = cell.Worksheet.Name;
            RowNumber = cell.Address.RowNumber;
            ColumnNumber = cell.Address.ColumnNumber;
            Column = cell.Address.ColumnLetter;
        }
        
        public string SheetName { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public string Column { get; set; }

        string IFoundCell.ToString()
        {
            return $"{SheetName}!{Column}{RowNumber}";
        }
    }
}
