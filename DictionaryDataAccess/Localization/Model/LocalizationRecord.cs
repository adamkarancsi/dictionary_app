using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryDataAccess.Localization.Model
{
    [Table("Localizations")]
    public class LocalizationRecord
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string English { get; set; }
        public string Hungarian { get; set; }

        public LocalizationRecord(string hungarian, string english)
        {
            Hungarian = hungarian;
            English = english;
        }
    }
}
