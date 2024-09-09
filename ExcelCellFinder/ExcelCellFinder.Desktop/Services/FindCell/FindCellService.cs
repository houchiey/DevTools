using ExcelCellFinder.Core.Logic;
using ExcelCellFinder.Core.Options.Interface;
using ExcelCellFinder.Core.Result.Interface;

namespace ExcelCellFinder.Desktop.Services.FindCell
{
    internal class FindCellService : IFindCellService
    {
        public IResult FindCell(IFindCellOptions options)
        {
            var logic = LogicFactory.GetLogic();

            return logic.FindCell(options);
        }
    }
}
