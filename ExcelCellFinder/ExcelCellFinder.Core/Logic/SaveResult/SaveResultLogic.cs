using ClosedXML.Excel;
using ExcelCellFinder.Core.Result.Interface;

namespace ExcelCellFinder.Core.Logic.SaveResult
{
    internal class SaveResultLogic : ISaveResultLogic
    {
        public void SaveResult(IResult result, FileInfo saveTo)
        {
            if (saveTo.Exists)
            {
                saveTo.Delete();
            }
            using var workbook = new XLWorkbook();

            var resultSheet = CreateResultSheet(workbook);

            var writtenRows = WriteFoundCells(resultSheet, result);

            workbook.SaveAs(saveTo.FullName);
        }

        private IXLWorksheet CreateResultSheet(XLWorkbook workbook)
        {
            var resultSheetName = "結果";
            var oldResultSheet = workbook.Worksheets.FirstOrDefault(sheet => sheet.Name == resultSheetName);
            if (oldResultSheet != default)
            {
                oldResultSheet.Delete();
            }

            return workbook.Worksheets.Add(resultSheetName);
        }

        public int WriteFoundCells(IXLWorksheet resultSheet, IResult foundResult, int startRow = 1)
        {
            var workRow = startRow;

            // ヘッダー
            resultSheet.Cell(workRow, 1).Value = "ファイル名";
            resultSheet.Cell(workRow, 2).Value = "シート名";
            resultSheet.Cell(workRow, 3).Value = "セル";
            resultSheet.Cell(workRow, 4).Value = "リンク";

            workRow++;

            // 結果リスト
            foreach (var processedFile in foundResult.ProcessedFiles)
            {
                foreach (var foundCell in processedFile.FoundCells)
                {
                    resultSheet.Cell(workRow, 1).Value = processedFile.FileInfo.Name;
                    resultSheet.Cell(workRow, 2).Value = foundCell.SheetName;
                    resultSheet.Cell(workRow, 3).Value = foundCell.GetCellName();
                    resultSheet.Cell(workRow, 4).FormulaA1 = $"=HYPERLINK(\"{processedFile.FileInfo.FullName}#{foundCell}\",\"リンク\")";

                    workRow++;
                }
            }

            return workRow;
        }
    }
}
