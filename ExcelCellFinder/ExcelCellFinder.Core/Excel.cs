using ClosedXML.Excel;

namespace ExcelCellFinder.Core
{
    public class Excel(string path, Logger logger)
    {
        private readonly string path = path;
        private readonly Logger logger = logger;

        // 赤字または取り消し線のセルを検索
        public IEnumerable<FoundCell> FindRedColorAndStrikeThroughCells()
        {
            // Excelファイルを開く
            FileStream fs = new FileStream(this.path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var workbook = new XLWorkbook(fs);

            this.logger.LogDebug($"Opened {Path.GetFileName(this.path)}");

            var cells = new List<(IXLCell, Exception?)>();

            // シートをループ
            foreach (var sheet in workbook.Worksheets)
            {
                cells.AddRange(GetCellsWithRedFontOrStrikethrough(sheet));
            }

            return cells.Select(cell => new FoundCell(this.path, cell.Item1, cell.Item2)).ToArray();
        }

        public static IEnumerable<(IXLCell, Exception?)> GetCellsWithRedFontOrStrikethrough(IXLWorksheet worksheet)
        {
            var cells = new List<(IXLCell, Exception?)>();

            foreach (var cell in worksheet.CellsUsed())
            {
                try
                {
                    var richText = cell.GetRichText();
                    foreach (var rt in richText)
                    {
                        if (rt.FontColor == XLColor.Red || rt.Strikethrough)
                        {
                            cells.Add((cell, null));
                            break;
                        }
                    }
                }
                catch (Exception e)
                {
                    cells.Add((cell, e));
                }
            }

            return cells;
        }

        public void CreateNewFile(string[]? sheetNames = null)
        {
            if (File.Exists(this.path))
            {
                File.Delete(this.path);
            }

            using var workbook = new XLWorkbook();

            sheetNames ??= ["Sheet1"];
            foreach (var sheetName in sheetNames)
            {
                workbook.Worksheets.Add(sheetName);
            }

            workbook.SaveAs(this.path);

            this.logger.LogDebug($"Created {Path.GetFileName(this.path)}");
        }

        public int WriteFoundCells(string sheetName, IEnumerable<FoundCell> foundCells, int startRow = 1)
        {
            using var workbook = new XLWorkbook(this.path);
            var worksheet = workbook.Worksheet(sheetName);

            var workRow = startRow;
            foreach (var foundCell in foundCells)
            {
                worksheet.Cell(workRow, 1).FormulaA1 = $"=HYPERLINK(\"[{foundCell.WorkbookPath}]{foundCell}\",\"{Path.GetFileName(foundCell.WorkbookPath)}#{foundCell}\")";
                worksheet.Cell(workRow, 2).Value = foundCell.ErrorMessage ?? "赤字または取り消し線あり";
                workRow++;
            }

            workbook.Save();

            return workRow;
        }
    }
}
