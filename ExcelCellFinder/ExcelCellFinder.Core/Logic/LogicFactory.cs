using ExcelCellFinder.Core.Mocks.Logic;
using ExcelCellFinder.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCellFinder.Core.Logic
{
    public class LogicFactory()
    {
        public static IFindCellLogic GetLogic()
        {
            return new FindCellLogicMock();
        }
    }
}
