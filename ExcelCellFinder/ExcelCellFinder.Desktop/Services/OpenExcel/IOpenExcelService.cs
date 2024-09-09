namespace ExcelCellFinder.Desktop.Services.OpenExcel
{
    internal interface IOpenExcelService
    {
        void OpenExcelFile(string filePath, string sheetName, string cellAddress);
    }
}
