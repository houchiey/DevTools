using System.Diagnostics;
using System.IO;

namespace ExcelCellFinder.Desktop.Services.OpenExcel
{
    internal class OpenExcelService : IOpenExcelService
    {
        private readonly string _openExcelBat = Path.Combine("Resource","OpenExcel.bat");

        public void OpenExcelFile(string filePath, string sheetName, string cellAddress)
        {
            var type = Type.GetTypeFromProgID("Excel.Application");
            if(type == null)
            {
                throw new Exception("Excelがインストールされていません");
            }

            dynamic? excelApp = Activator.CreateInstance(type);
            if (excelApp == null) 
            {
                throw new Exception("Excelインスタンスの作成に失敗");
            }
            
            var excelAppPath = Path.Combine(excelApp.Path, "excel.exe");

            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);

            var startInfo = new ProcessStartInfo(_openExcelBat);
            startInfo.ArgumentList.Add(excelAppPath);
            startInfo.ArgumentList.Add(filePath);
            startInfo.ArgumentList.Add(sheetName);
            startInfo.ArgumentList.Add(cellAddress);

            Process.Start(startInfo);
        }
    }
}
