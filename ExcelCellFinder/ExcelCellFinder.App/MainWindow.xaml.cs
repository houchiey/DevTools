using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExcelCellFinder.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Page SetupConditionsPage { get; set; }
        public Page ResultPage { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            SetupConditionsPage = new SetupConditionsPage();
            ResultPage = new ResultPage();

            Content.Navigate(SetupConditionsPage);
        }
    }
}