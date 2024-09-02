using ExcelCellFinder.Core.Options.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCellFinder.Core.Result.Interface
{
    public interface IResult
    {
        IFindCellOptions ExecutedOptions { get; protected set; }

        bool IsError { get; protected set; }

        IList<IResultFile> ProcessedFiles { get; protected set; }
    }
}
