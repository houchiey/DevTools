using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExcelCellFinder.Core.Result.Interface;
using ExcelCellFinder.Desktop.Models;
using ExcelCellFinder.Desktop.Services;
using ExcelCellFinder.Desktop.Services.SaveResult;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace ExcelCellFinder.Desktop.ViewModels.Pages
{
    public partial class FindResultPageViewModel : PageViewModelBase
    {
        private SetupConditionPageViewModel _originViewModel;

        private IResult _findCellResult;

        [ObservableProperty]
        private ObservableCollection<FindResultGridItem> _findResultGridData;


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
        private void SaveResult()
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "Excelファイル|*.xlsx",
                DefaultExt = ".xlsx",
                FilterIndex = 1,
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                var saveFilePath = saveFileDialog.FileName;
                var service = SaveResultServiceFactory.GetService(saveFilePath);
                service.SaveResult(this._findCellResult);
            }
        }

        private static ObservableCollection<FindResultGridItem> ConvertResultToGridData(IResult findCellResult)
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
                        FoundCell = cell.GetCellName(),
                    });
                }
            }

            return gridData;
        }
    }
}
