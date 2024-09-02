using ClosedXML.Excel;
using ExcelCellFinder.Core.Result.Interface;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCellFinder.Core.Mocks.Result
{
    internal class FoundCellMock : IFoundCell
    {
        internal FoundCellMock(string sheetName, int rowNumber, string columnLetter) 
        {
            SheetName = sheetName;
            RowNumber = rowNumber;
            ColumnNumber = ColumnLetterToNumber(columnLetter);
            Column = columnLetter;
        }
        
        public string SheetName { get; set; }
        public int RowNumber { get; set; }
        public int ColumnNumber { get; set; }
        public string Column { get; set; }

        string IFoundCell.ToString()
        {
            return $"{SheetName}!{Column}{RowNumber}";
        }


        private static int ColumnLetterToNumber(string column)
        {
            if (string.IsNullOrEmpty(column)) return 0;

            column = column.ToUpperInvariant();
            int sum = 0;

            for (int i = 0; i < column.Length; i++)
            {
                sum *= 26;
                sum += (column[i] - 'A' + 1);
            }

            return sum;
        }
    }
}
