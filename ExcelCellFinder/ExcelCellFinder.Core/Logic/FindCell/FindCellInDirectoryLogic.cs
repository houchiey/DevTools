using ExcelCellFinder.Core.Options;
using ExcelCellFinder.Core.Options.Interface;
using ExcelCellFinder.Core.Result;
using ExcelCellFinder.Core.Result.Interface;
using System.Text.RegularExpressions;

namespace ExcelCellFinder.Core.Logic.FindCell
{
    internal class FindCellInDirectoryLogic : IFindCellLogic
    {
        private string _path;
        private bool _isRecursively;
        private Regex? _excludeDirectoryRegex;

        private IFindCellOptions _originalOption;

        internal FindCellInDirectoryLogic(IFindCellOptions options)
        {
            if (options.Mode != TargetMode.Directory)
            {
                throw new InvalidOperationException("Mode must be Directory");
            }

            if (options.TargetDirectoryInfo == null)
            {
                throw new ArgumentNullException(nameof(options.TargetDirectoryInfo));
            }

            this._path = options.TargetDirectoryInfo.FullName;
            this._isRecursively = options.IsRecursively;
            this._excludeDirectoryRegex = options.ExcludeDirectoryRegex;
            this._originalOption = options;
        }

        public IResult FindCell()
        {
            var processDirectory = new DirectoryInfo(this._path);
            var searchOption = this._isRecursively ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;

            var files = processDirectory
                .GetFiles("*.xlsx", searchOption)
                .Concat(processDirectory.GetFiles("*.xlsm", searchOption));

            if (_excludeDirectoryRegex != null)
            {
                files = files.Where(
                    file => _excludeDirectoryRegex.IsMatch(file.DirectoryName ?? "") == false);
            }

            IResult wholeResult = new FindCellResult(this._originalOption, false);

            var optionForFindCellInFile = OptionFactory.GetOption();
            optionForFindCellInFile.Mode = TargetMode.File;
            optionForFindCellInFile.TargetCellTypes = _originalOption.TargetCellTypes;

            foreach (var file in files)
            {
                optionForFindCellInFile.TargetFileInfo = file;
                var logic = FindCellLogicFactory.GetLogic(optionForFindCellInFile);
                var findResult = logic.FindCell();

                wholeResult = wholeResult.Merge(findResult);
            }

            return wholeResult;
        }
    }
}
