using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ExcelCellFinder.Core.Options;
using ExcelCellFinder.Core.Options.Interface;
using ExcelCellFinder.Desktop.Services;
using ExcelCellFinder.Desktop.ViewModels.Pages;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

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
            FindCellOption.TargetDirectoryInfo = new System.IO.DirectoryInfo(FindFolderPath);
            FindCellOption.Mode = TargetMode.Directory;

            var service = new FindCellService();
            var result = service.FindCell(FindCellOption);

            RoutingService.Instance.MoveTo(new FindResultPageViewModel(result, this));
        }
    }
}
