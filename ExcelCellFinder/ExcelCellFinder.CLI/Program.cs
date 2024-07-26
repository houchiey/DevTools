using ExcelCellFinder.CLI;

// 引数が1つでない場合はUsageを表示して終了
if (args.Length != 1)
{
    Console.WriteLine("Usage: dotnet run <input>");
    return;
}

// 引数をパスとして取得
var path = args[0];

var resultFilePath = "";

// pathがディレクトリの場合はディレクトリ内のExcelファイルを処理
if (Directory.Exists(path))
{
    resultFilePath = Path.Combine(path, "result.xlsx");


    Console.WriteLine($"Processing files in {path}");
    FindRedColorAndStrikeThoroughCells.RunInDirectory(path, resultFilePath);
}
// pathがファイルの場合はそのファイルを処理
else if (File.Exists(path))
{
    resultFilePath = Path.Combine(Path.GetDirectoryName(path) ?? "", $"{Path.GetFileNameWithoutExtension(path)}_result.xlsx");
    FindRedColorAndStrikeThoroughCells.Run(path, resultFilePath);
}
// pathが存在しない場合はエラーメッセージを表示
else
{
    Console.WriteLine($"'{path}' not found.");
}
