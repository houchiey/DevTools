﻿using ExcelCellFinder.Core.Options.Interface;
using ExcelCellFinder.Core.Result.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCellFinder.Core.Logic
{
    public interface IFindCellLogic
    {
        public IResult FindCell(IFindCellOptions options);
    }
}