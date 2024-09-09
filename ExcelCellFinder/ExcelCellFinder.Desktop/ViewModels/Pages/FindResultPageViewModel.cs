using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExcelCellFinder.Core.Result.Interface;
using ExcelCellFinder.Desktop.Models;
using ExcelCellFinder.Desktop.Services;
using ExcelCellFinder.Desktop.Services.OpenExcel;
using System.Collections.ObjectModel;

namespace ExcelCellFinder.Desktop.ViewModels.Pages
{
    public partial class FindResultPageViewModel : PageViewModelBase
    {
        private SetupConditionPageViewModel _originViewModel;

        private IResult _findCellResult;

        [ObservableProperty]
        private ObservableCollection<FindResultGridItem> _findResultGridData;

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
            if (SelectedItem != null)
            {
                var service = OpenExcelServiceFactory.GetService();
                service.OpenExcelFile(SelectedItem.FoundFilePath, SelectedItem.FoundSheet, SelectedItem.FoundCell);
            }            
        }

        private ObservableCollection<FindResultGridItem> ConvertResultToGridData(IResult findCellResult)
        {
            var gridData = new ObservableCollection<FindResultGridItem>();

            foreach (var file in findCellResult.ProcessedFiles)
            {
                foreach (var cell in file.FoundCells)
                {
                    gridData.Add(new FindResultGridItem
                    {
                        FoundFilePath = file.FileInfo.FullName,
                        FoundSheet = cell.SheetName,
                        FoundCell = cell.ToString(),
                    });
                }
            }

            return gridData;
        }
    }
}
