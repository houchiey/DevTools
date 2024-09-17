using ClosedXML.Excel;
using ExcelCellFinder.Core.Options.Interface;
using ExcelCellFinder.Core.Result;
using ExcelCellFinder.Core.Result.Interface;

namespace ExcelCellFinder.Core.Logic.FindCell
{
    internal class FindCellWithSingleFileLogic : IFindCellLogic
    {
        private IFindCellOptions _originalOptions;
        private string _path;

        internal FindCellWithSingleFileLogic(IFindCellOptions options)
        {
            if (options.Mode != TargetMode.File)
            {
                throw new InvalidOperationException("Mode must be File");
            }

            if (options.TargetFileInfo == null)
            {
                throw new ArgumentNullException(nameof(options.TargetFileInfo));
            }

            this._originalOptions = options;
            this._path = options.TargetFileInfo.FullName;
        }

        public IResult FindCell()
        {
            var workbook = OpenExcelWorkBook();

            var foundCells = new List<(IXLCell, Exception?)>();
            foreach (var sheet in workbook.Worksheets)
            {
                if (this._originalOptions.TargetCellTypes.Contains(TargetCellType.RedColor)
                    && this._originalOptions.TargetCellTypes.Contains(TargetCellType.StrikeLine))
                {
                    foundCells.AddRange(GetCellsWithRedFontOrStrikethrough(sheet));
                }
            }

            IResult result = new FindCellResult(_originalOptions, false);
            result.ProcessedFiles.Add(new ResultFile(new FileInfo(this._path))
            {
                FoundCells = foundCells.Select(cell => (IFoundCell)new FoundCell(cell.Item1)).ToList()
            });

            return result;

        }

        private XLWorkbook OpenExcelWorkBook()
        {
            // Excelファイルを開く
            var fs = new FileStream(this._path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            return new XLWorkbook(fs);
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
    }
}
