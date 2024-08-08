using CommunityToolkit.Mvvm.ComponentModel;
using ExcelCellFinder.App.ViewModels.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExcelCellFinder.App.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {
        public MainWindowViewModel()
        {
            _currentPage = new SetupConditionPageViewModel();
        }
        
        [ObservableProperty]
        private PageViewModelBase _currentPage;
    }
}
