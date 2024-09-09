namespace ExcelCellFinder.Desktop.Services.OpenExcel
{
    internal static class OpenExcelServiceFactory
    {
        public static IOpenExcelService GetService()
        {
            return new OpenExcelService();
        }
    }
}
