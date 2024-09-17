using ExcelCellFinder.Core.Options.Interface;

namespace ExcelCellFinder.Core.Logic.FindCell
{
    public class FindCellLogicFactory()
    {
        public static IFindCellLogic GetLogic(IFindCellOptions options)
        {
            if (options.Mode == TargetMode.File)
            {
                return new FindCellWithSingleFileLogic(options);
            }
            else if (options.Mode == TargetMode.Directory)
            {
                return new FindCellInDirectoryLogic(options);
            }
            else
            {
                throw new InvalidOperationException("Invalid Mode");
            }
        }
    }
}
