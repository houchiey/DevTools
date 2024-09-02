using ExcelCellFinder.Core.Options.Interface;
using ExcelCellFinder.Core.Result.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCellFinder.Core.Result
{
    internal class FindCellResult : IResult
    {
        internal FindCellResult(IFindCellOptions executedOption, bool isError)
        {
            ExecutedOptions = executedOption;
            IsError = isError;
            ProcessedFiles = [];
        }
        
        public IFindCellOptions ExecutedOptions { get; set; }
        public bool IsError { get; set; }
        public IList<IResultFile> ProcessedFiles { get; set; }
    }
}
