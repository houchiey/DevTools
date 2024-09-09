using System.Diagnostics;

namespace ExcelCellFinder.Desktop.Services.OpenExcel
{
    internal class OpenExcelService : IOpenExcelService
    {
        public void OpenExcelFile(string filePath, string sheetName, string cellAddress)
        {

            Process.Start("excel.exe", filePath);
        }
    }
}
