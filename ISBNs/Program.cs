using ISBNs;

string inputFile = "ISBN_Input_File.txt";
string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
string filePath = Path.Combine(projectPath, inputFile);

string outputFile = "output.csv";
string outputPath = Path.Combine(projectPath, outputFile);

string[] isbnLines = File.ReadAllLines(filePath);
List<string> outputLines = new List<string>();

Dictionary<string, BookInfo> cache = new Dictionary<string, BookInfo>();

outputLines.Add("Row Number;Type;ISBN;Title;Subtitle;Author Name(s);Number Of Pages;Publish Date");

for (int rowNumber = 1; rowNumber <= isbnLines.Length; rowNumber++)
{
    string[] isbns = isbnLines[rowNumber - 1].Split(',');

    foreach (string isbn in isbns)
    {
        string csvRow = await ProcessISBN(rowNumber, isbn, cache);
        outputLines.Add(csvRow);
    }
}

File.WriteAllLines(outputPath, outputLines);


static async Task<string> ProcessISBN(int rowNumber, string isbn, Dictionary<string, BookInfo> cache)
{
    if (!cache.TryGetValue(isbn, out BookInfo bookInfo))
    {
        bookInfo = await ISBNHandler.RetrieveBookInfo(isbn);

        cache[isbn] = bookInfo;

        return $"{rowNumber};Server;{isbn};{bookInfo.Title};{bookInfo.Subtitle};{bookInfo.Authors};{bookInfo.NumberOfPages};{bookInfo.PublishDate}";
    }
    else
    {
        return $"{rowNumber};Cache;{isbn};{bookInfo.Title};{bookInfo.Subtitle};{bookInfo.Authors};{bookInfo.NumberOfPages};{bookInfo.PublishDate}";
    }
}

