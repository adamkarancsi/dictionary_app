using System.ComponentModel.DataAnnotations.Schema;

namespace DictionaryDataAccess.Model
{
    [Table("Localizations")]
    public class LocalizationRecord
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string English { get; set; }
        public string Hungarian { get; set; }

        public LocalizationRecord(int id, string hungarian, string english)
        {
            Id = id;
            Hungarian = hungarian;
            English = english;
        }
    }
}
