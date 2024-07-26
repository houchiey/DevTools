using ClosedXML.Excel;

namespace ExcelCellFinder.Core
{
    public class FoundCell(string wbPath, IXLCell cell, Exception? e)
    {
        public string WorkbookPath { get; } = wbPath;

        public string SheetName { get; } = cell.Worksheet.Name;

        public int RowNumber { get; } = cell.Address.RowNumber;

        public int ColumnNumber { get; } = cell.Address.ColumnNumber;

        public string Column { get; } = cell.Address.ColumnLetter;

        public string? ErrorMessage { get; } = e?.Message;

        public override string ToString()
        {
            return $"{SheetName}!{Column}{RowNumber}";
        }
    }
}
