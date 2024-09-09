using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExcelCellFinder.Core.Result.Interface;
using ExcelCellFinder.Desktop.Models;
using ExcelCellFinder.Desktop.Services;
using ExcelCellFinder.Desktop.Services.OpenExcel;

namespace ExcelCellFinder.Desktop.ViewModels.Pages
{
    public partial class FindResultPageViewModel : PageViewModelBase
    {
        private SetupConditionPageViewModel _originViewModel;

        private IResult _findCellResult;

        [ObservableProperty]
        private IList<FindResultGridItem> _findResultGridData;

        [ObservableProperty]
        private FindResultGridItem? _selectedItem;


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

        [RelayCommand]
        private void OpenExcelFile()
        {
            if (SelectedItem == null)
            {
                return;
            }

            var service = OpenExcelServiceFactory.GetService();
            service.OpenExcelFile(SelectedItem.FoundFilePath, SelectedItem.FoundSheet, SelectedItem.FoundCell);
        }

        private IList<FindResultGridItem> ConvertResultToGridData(IResult findCellResult)
        {
            var gridData = new List<FindResultGridItem>();

            foreach (var file in findCellResult.ProcessedFiles)
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
