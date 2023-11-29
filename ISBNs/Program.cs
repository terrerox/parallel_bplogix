using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using ISBNs;

string inputFile = "ISBN_Input_File.txt";
string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
string filePath = Path.Combine(projectPath, inputFile);

string outputFile = "output.csv";
string outputPath = Path.Combine(projectPath, outputFile);

string[] isbnLines = File.ReadAllLines(filePath);
List<OutputRow> outputLines = new List<OutputRow>();

Dictionary<string, BookInfo> cache = new Dictionary<string, BookInfo>();

for (int rowNumber = 1; rowNumber <= isbnLines.Length; rowNumber++)
{
    string[] isbns = isbnLines[rowNumber - 1].Split(',');

    foreach (string isbn in isbns)
    {
        OutputRow csvRow = await ProcessISBN(rowNumber, isbn, cache);
        outputLines.Add(csvRow);
    }
}

using (var writer = new StreamWriter(outputPath))
using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
{
    csv.WriteRecords(outputLines);
}


static async Task<OutputRow> ProcessISBN(int rowNumber, string isbn, Dictionary<string, BookInfo> cache)
{
    if (!cache.TryGetValue(isbn, out BookInfo bookInfo))
    {
        bookInfo = await ISBNHandler.RetrieveBookInfo(isbn);

        cache[isbn] = bookInfo;
        return new OutputRow
        {
            RowNumber = rowNumber.ToString(),
            Type = nameof(SourceType.Server), // Green color
            ISBN = isbn,
            Title = bookInfo.Title,
            Subtitle = bookInfo.Subtitle,
            Authors = string.Join(";", bookInfo.Authors),
            NumberOfPages = bookInfo.NumberOfPages.ToString(),
            PublishDate = bookInfo.PublishDate
        };
    }
    else
    {
        return new OutputRow
        {
            RowNumber = rowNumber.ToString(),
            Type = nameof(SourceType.Cache),
            ISBN = isbn,
            Title = bookInfo.Title,
            Subtitle = bookInfo.Subtitle,
            Authors = string.Join(";", bookInfo.Authors),
            NumberOfPages = bookInfo.NumberOfPages.ToString(),
            PublishDate = bookInfo.PublishDate
        };
    }
}

