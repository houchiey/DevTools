﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelCellFinder.Core.Options.Interface;

namespace ExcelCellFinder.Core.Options
{
    internal class FindCellOptions : IFindCellOptions
    {
        public FileInfo? TargetFileInfo { get; set; }
        public DirectoryInfo? TargetDirectoryInfo { get; set; }
        public TargetMode? Mode { get; set; }
        public bool IsRecursively { get; set; }
        public IEnumerable<TargetCellType>? TargetCellTypes { get; set; }

        public bool IsValidOption()
        {
            return CheckFileAndDirectoryValidity() && TargetCellTypes != null;
        }

        private bool CheckFileAndDirectoryValidity()
        {
            switch (Mode)
            {
                case TargetMode.File:
                    if (TargetFileInfo == null)
                    {
                        return false;
                    }
                    break;
                case TargetMode.Directory:
                    if (TargetDirectoryInfo == null)
                    {
                        return false;
                    }
                    break;
            }

            return true;
        }
    }
}