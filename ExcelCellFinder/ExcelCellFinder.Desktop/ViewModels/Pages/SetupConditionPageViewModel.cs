using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExcelCellFinder.Core.Options;
using ExcelCellFinder.Core.Options.Interface;
using ExcelCellFinder.Desktop.Services;
using ExcelCellFinder.Desktop.Services.FindCell;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Text.RegularExpressions;

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

        [ObservableProperty]
        private string _excludeDirectoryRegex = "";

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

            var option = OptionFactory.GetOption();

            option.TargetDirectoryInfo = new System.IO.DirectoryInfo(FindFolderPath);
            option.Mode = TargetMode.Directory;
            option.IsRecursively = IsRecursively;
            option.ExcludeDirectoryRegex = string.IsNullOrEmpty(ExcludeDirectoryRegex) ? null : new Regex(ExcludeDirectoryRegex);
            option.TargetCellTypes = new[] { TargetCellType.RedColor, TargetCellType.StrikeLine };

            var service = FindCellServiceFactory.GetService();
            var result = service.FindCell(option);

            RoutingService.Instance.MoveTo(new FindResultPageViewModel(result, this));
        }
    }
}
