using ClosedXML.Excel;

namespace ExcelCellFinder.Core
{
    public class Excel(string path)
    {
        public string Path { get; } = path;

        // 赤字または取り消し線のセルを検索
        public IEnumerable<FoundCell> FindRedColorAndStrikeThroughCells()
        {
            // Excelファイルを開く
            using var workbook = new XLWorkbook(this.Path);

            var cells = new List<IXLCell>();

            // シートをループ
            foreach (var sheet in workbook.Worksheets)
            {
                cells.AddRange(GetCellsWithRedFontOrStrikethrough(sheet));
            }

            return cells.Select(cell => new FoundCell(cell)).ToArray();
        }

        public static IEnumerable<IXLCell> GetCellsWithRedFontOrStrikethrough(IXLWorksheet worksheet)
        {
            var cells = new List<IXLCell>();

            foreach (var cell in worksheet.CellsUsed())
            {
                var richText = cell.GetRichText();
                foreach (var rt in richText)
                {
                    if (rt.FontColor == XLColor.Red || rt.Strikethrough)
                    {
                        cells.Add(cell);
                        break;
                    }
                }
            }

            return cells;
        }
    }
}
