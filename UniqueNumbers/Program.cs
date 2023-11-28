// See https://aka.ms/new-console-template for more information

string fileName = "numbersFile.txt";
string projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
string filePath = Path.Combine(projectPath, fileName);

try
{
    HashSet<int> uniqueNumbers = GetUniqueNumbers(filePath);

    // Print or process the unique numbers along with explanations
    Console.WriteLine($"Output: [{string.Join(",", uniqueNumbers)}]");
    Console.WriteLine($"Explanation: {GetExplanation(uniqueNumbers)}");
}
catch (IOException ex)
{
    Console.WriteLine($"Error reading the file: {ex.Message}");
}

static HashSet<int> GetUniqueNumbers(string filePath)
{
    HashSet<int> uniqueNumbersSet = new HashSet<int>();
    
    foreach (string line in File.ReadLines(filePath))
    {
        if (int.TryParse(line, out int number))
        {
            uniqueNumbersSet.Add(number);
        }
    }

    return uniqueNumbersSet;
}

static string GetExplanation(HashSet<int> uniqueNumbers)
{
    switch (uniqueNumbers.Count)
    {
        case 0:
            return "No unique numbers found in the input file.";
        case 1:
            return "All values in the input file are the same, therefore, there is only one unique input value.";
        default:
            return $"The only unique values found in the input file are [{string.Join(",", uniqueNumbers.OrderBy(num => num))}], and the output order does not matter.";
    }
}