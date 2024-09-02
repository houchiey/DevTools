using ExcelCellFinder.Core.Logic;
using ExcelCellFinder.Core.Mocks.Result;
using ExcelCellFinder.Core.Options.Interface;
using ExcelCellFinder.Core.Result;
using ExcelCellFinder.Core.Result.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCellFinder.Core.Mocks.Logic
{
    internal class FindCellLogicMock : IFindCellLogic
    {
        public IResult FindCell(IFindCellOptions options)
        {
            var result = new FindCellResult(options, false);

            var file = new ResultFile(new FileInfo(@"D:\work\ExcelCellFinder\ForMocks\MockExcelFile.xlsx"));
            file.FoundCells.Add(new FoundCellMock("Sheet1", 1, "A"));
            file.FoundCells.Add(new FoundCellMock("Sheet2", 1, "A"));
            file.FoundCells.Add(new FoundCellMock("Sheet3", 1, "A"));
            result.ProcessedFiles.Add(file);

            return result;
        }
    }
}
