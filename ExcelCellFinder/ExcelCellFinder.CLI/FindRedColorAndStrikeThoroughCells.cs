using ExcelCellFinder.Core;

namespace ExcelCellFinder.CLI
{
    internal static class FindRedColorAndStrikeThoroughCells
    {
        private static readonly Logger logger = new Logger(
#if DEBUG
                Console.WriteLine,
#else
                (message) => { return; },
#endif
                (message, ex) =>
                {
                    Console.WriteLine(message);
                    Console.WriteLine(ex);
                });

        public static void Run(string path, string result)
        {
            var excel = new Excel(path, logger);
            var reslut = new Excel(result, logger);
            reslut.CreateNewFile(["result"]);

            reslut.WriteFoundCells("result", excel.FindRedColorAndStrikeThroughCells());
        }

        public static void RunInDirectory(string path, string resultFileName)
        {
            var resultExcel = new Excel(resultFileName, logger);
            resultExcel.CreateNewFile(["result"]);

            var resultRow = 1;

            foreach (var file in Directory.EnumerateFiles(path))
            {
                if (file == resultFileName) continue; // 結果ファイルは処理しない

                // xlsxまたはxlsmのファイルのみ処理
                if (file.EndsWith(".xlsx") || file.EndsWith(".xlsm"))
                {
                    var excel = new Excel(file, logger);

                    resultRow = resultExcel.WriteFoundCells("result", excel.FindRedColorAndStrikeThroughCells(), resultRow);
                }
            }
        }
    }
}
