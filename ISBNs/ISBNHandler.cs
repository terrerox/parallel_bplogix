using Newtonsoft.Json;

namespace ISBNs;

public class ISBNHandler
{
    public static async Task<BookInfo> RetrieveBookInfo(string isbn)
    {
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = $"https://openlibrary.org/api/books?bibkeys=ISBN:{isbn}&jscmd=data&format=json";
            string response = await client.GetStringAsync(apiUrl);

            dynamic jsonData = JsonConvert.DeserializeObject(response);
            dynamic bookData = jsonData[$"ISBN:{isbn}"];

            BookInfo bookInfo = new BookInfo
            {
                Title = bookData?.title ?? "N/A",
                Subtitle = bookData?.subtitle ?? "N/A",
                Authors = GetAuthorNames(bookData?.authors),
                NumberOfPages = bookData?.number_of_pages ?? "N/A",
                PublishDate = bookData?.publish_date ?? "N/A"
            };

            return bookInfo;
        }
    }

    private static string[] GetAuthorNames(dynamic authors)
    {
        string[] authorNames = new string[authors.Count];

        for (int i = 0; i < authors.Count; i++)
        {
            authorNames[i] = authors[i].name;
        }

        return authorNames;
    }
}