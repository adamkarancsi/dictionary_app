using DictionaryDataAccess;
using DictionaryDataAccess.Localization.Model;
using Microsoft.EntityFrameworkCore;
using System.Text;

Console.WriteLine($"Reading localizations from {args[0]}...");

var lines = await File.ReadAllLinesAsync(args[0], Encoding.UTF8);

var languages = lines[0].Split(';', StringSplitOptions.RemoveEmptyEntries);

Console.WriteLine($"Found {languages.Length} languages and {lines.Length - 1} phrases.");

using var dbContext = new DesignTimeDbContextFactory().CreateDbContext(Array.Empty<string>());

Console.WriteLine("Deleting all rows...");

dbContext.LocalizationRecords.RemoveRange(dbContext.LocalizationRecords); // this is fine for small tables
await dbContext.SaveChangesAsync();

Console.WriteLine("Loading into DB...");
int i = 1;
foreach (var line in lines.Skip(1)) {
    var words = line.Split(";", StringSplitOptions.RemoveEmptyEntries);

    if (words.Length != languages.Length)
        throw new Exception("The word count does not conform to the language count.");

    for (int j = 0; j < languages.Length; j++)
    {
        dbContext.LocalizationRecords.Add(new LocalizationRecord(i, languages[j], words[j]));
    }
    i++;
}

await dbContext.SaveChangesAsync();

var dbCount = await dbContext.LocalizationRecords.CountAsync();

Console.WriteLine($"Loaded {dbCount} records into DB.");