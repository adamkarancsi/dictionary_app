using DictionaryDataAccess;
using DictionaryDataAccess.Model;
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

foreach (var line in lines.Skip(1)) {
    var words = line.Split(";", StringSplitOptions.RemoveEmptyEntries);

    if (words.Length != languages.Length)
        throw new Exception("The word count does not conform to the language count.");

    dbContext.LocalizationRecords.Add(new LocalizationRecord(words[1], words[0]));
}

await dbContext.SaveChangesAsync();

var dbCount = await dbContext.LocalizationRecords.CountAsync();

Console.WriteLine($"Loaded {dbCount} records into DB.");