using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExcelCellFinder.Core.Result.Interface;
using ExcelCellFinder.Desktop.Models;
using ExcelCellFinder.Desktop.Services;
using ExcelCellFinder.Desktop.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCellFinder.Desktop.ViewModels.Pages
{
    public partial class FindResultPageViewModel : PageViewModelBase
    {
        private SetupConditionPageViewModel _originViewModel;

        private IResult _findCellResult;

        [ObservableProperty]
        private IList<FindResultGridItem> _findResultGridData;

        public FindResultPageViewModel(IResult findCellResult, SetupConditionPageViewModel origin)
        {
            PageTitle = "検索結果";
            _findCellResult = findCellResult;
            _originViewModel = origin;

            _findResultGridData = ConvertResultToGridData(findCellResult);
        }

        [RelayCommand]
        private void ReturnToSetupCondition()
        {
            RoutingService.Instance.MoveTo(_originViewModel);
        }

        private IList<FindResultGridItem> ConvertResultToGridData(IResult findCellResult)
        {
            var gridData = new List<FindResultGridItem>();
            
            foreach(var file in findCellResult.ProcessedFiles)
            {
                gridData.AddRange(file.FoundCells.Select(s =>
                {
                    return new FindResultGridItem
                    {
                        FoundFilePath = file.FileInfo.FullName,
                        FoundSheet = s.SheetName,
                        FoundCell = s.ToString(),
                    };
                }));
            }

            return gridData;
        }
    }
}
