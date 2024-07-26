using ClosedXML.Excel;

namespace ExcelCellFinder.Core
{
    public class FoundCell(IXLCell cell)
    {
        private readonly IXLCell cell = cell;

        public string SheetName { get { return this.cell.Worksheet.Name; } }
        public int RowNumber { get { return this.cell.Address.RowNumber; } }
        public int ColumnNumber { get { return this.cell.Address.ColumnNumber; } }

        public override string ToString()
        {
            return $"{this.SheetName}!{this.cell.Address.ColumnLetter}{this.RowNumber}";
        }
    }
}
