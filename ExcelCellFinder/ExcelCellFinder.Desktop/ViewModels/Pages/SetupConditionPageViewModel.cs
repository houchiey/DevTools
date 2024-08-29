using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
            // TODO 引数付コンストラクタにしてしまうと、
            RoutingService.Instance.MoveTo(new FindResultPageViewModel(this));
        }
    }
}
