namespace ExcelCellFinder.Desktop.Models
{
    public class FindResultGridItem
    {
        public string FoundFilePath { get; set; } = "";
        public string FoundSheet { get; set; } = "";
        public string FoundCell { get; set; } = "";

        //[RelayCommand]
        //private void OpenExcelFile()
        //{
        //    //if (this == null)
        //    //{
        //    //    return;
        //    //}

        //    var service = OpenExcelServiceFactory.GetService();
        //    service.OpenExcelFile(FoundFilePath, FoundSheet, FoundCell);
        //}
    }
}
