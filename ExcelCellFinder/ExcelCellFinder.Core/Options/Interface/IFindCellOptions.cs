using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCellFinder.Core.Options.Interface
{
    public interface IFindCellOptions
    {
        public FileInfo? TargetFileInfo { get; set; }
        public DirectoryInfo? TargetDirectoryInfo { get; set; }
        public TargetMode? Mode { get; set; }
        public bool IsRecursively { get; set; }
        public IEnumerable<TargetCellType>? TargetCellTypes { get; set; }

        public bool IsValidOption();
    }

    public enum TargetCellType
    {
        RedColor,
        StrikeLine
    }

    public enum TargetMode
    {
        File,
        Directory
    }
}
