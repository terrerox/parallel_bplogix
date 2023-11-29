using CsvHelper.Configuration.Attributes;

namespace ISBNs;

public class OutputRow
{
    [Name("Row Number")]
    public string RowNumber { get; set; }
    [Name("Data Retrieval Type")]
    public string Type { get; set; }
    [Name("ISBN")]
    public string ISBN { get; set; }
    [Name("Title")]
    public string Title { get; set; }
    [Name("Subtitle")]
    public string Subtitle { get; set; }
    [Name("Author Name(s)")]
    public string Authors { get; set; }
    [Name("Number Of Pages")]
    public string NumberOfPages { get; set; }
    [Name("Publish Date")]
    public string PublishDate { get; set; }
}