using ExcelCellFinder.Core.Logic;
using ExcelCellFinder.Core.Options.Interface;
using ExcelCellFinder.Core.Result.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCellFinder.Desktop.Services
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
