using CommunityToolkit.Mvvm.Input;
using ExcelCellFinder.Desktop.Services;
using ExcelCellFinder.Desktop.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCellFinder.Desktop.ViewModels.Pages
{
    public partial class FindResultPageViewModel : PageViewModelBase
    {
        private SetupConditionPageViewModel _originViewModel;
        public FindResultPageViewModel(SetupConditionPageViewModel origin)
        {
            PageTitle = "検索結果";
            _originViewModel = origin;
        }

        [RelayCommand]
        private void ReturnToSetupCondition()
        {
            RoutingService.Instance.MoveTo(_originViewModel);
        }
    }
}
