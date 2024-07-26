using ExcelCellFinder.Core;

// 引数が1つでない場合はUsageを表示して終了
if (args.Length != 1)
{
    Console.WriteLine("Usage: dotnet run <input>");
    return;
}

// 引数をパスとして取得
var path = args[0];

// Excelクラスのインスタンスを生成
var excel = new Excel(path);

// 赤字または取り消し線のセルを検索
foreach (var cell in excel.FindRedColorAndStrikeThroughCells())
{
    Console.WriteLine(cell);
}