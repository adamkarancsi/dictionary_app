using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryDataAccess.Localization.Model
{
    [Table("Localizations")]
    [Index(nameof(RowId))]
    [Index(nameof(Language))]
    [Index(nameof(Phrase))]
    public class LocalizationRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int RowId { get; set; }
        public string Language { get; set; }
        public string Phrase { get; set; }

        public LocalizationRecord(int rowId, string language, string phrase)
        {
            RowId = rowId;
            Language = language;
            Phrase = phrase;
        }
    }
}
