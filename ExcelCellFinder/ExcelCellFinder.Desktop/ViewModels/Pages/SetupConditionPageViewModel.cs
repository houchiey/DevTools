using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExcelCellFinder.Core.Options;
using ExcelCellFinder.Core.Options.Interface;
using ExcelCellFinder.Desktop.Services;
using ExcelCellFinder.Desktop.Services.FindCell;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ExcelCellFinder.Desktop.ViewModels.Pages
{
    public partial class SetupConditionPageViewModel : PageViewModelBase
    {
        public SetupConditionPageViewModel()
        {
            PageTitle = "条件設定";
            _findFolderPath = "";
        }

        [ObservableProperty]
        private string _findFolderPath;

        [ObservableProperty]
        private bool _isRecursively = true;

        public IFindCellOptions FindCellOption { get; } = OptionFactory.GetOption();

        [RelayCommand]
        private void SelectFolder()
        {
            var dialog = new CommonOpenFileDialog("フォルダを選択")
            {
                IsFolderPicker = true
            };

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                FindFolderPath = dialog.FileName;
            }
        }

        [RelayCommand]
        private void ExecuteSearch()
        {
            if (string.IsNullOrEmpty(FindFolderPath))
            {
                return;
            }

            FindCellOption.TargetDirectoryInfo = new System.IO.DirectoryInfo(FindFolderPath);
            FindCellOption.Mode = TargetMode.Directory;
            FindCellOption.IsRecursively = IsRecursively;
            FindCellOption.TargetCellTypes = new[] { TargetCellType.RedColor, TargetCellType.StrikeLine };

            var service = FindCellServiceFactory.GetService();
            var result = service.FindCell(FindCellOption);

            RoutingService.Instance.MoveTo(new FindResultPageViewModel(result, this));
        }
    }
}
