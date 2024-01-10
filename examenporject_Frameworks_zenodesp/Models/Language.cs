using examenporject_Frameworks_zenodesp.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace examenporject_Frameworks_zenodesp.Models
{
    public class Language
    {
        public static List<Language> AllLanguages { get; set; }

        public static Dictionary<string, Language> LanguagesByid { get; set; }

        [Key]
        [Display(Name = "Code")]
        [MaxLength(2)]
        [MinLength(2)]
        public string Id { get; set; }
        [Display(Name = "Language")]

        public string Name { get; set; }

        [Display(Name = "System Language?")]

        public bool IsSystemLanguage { get; set; }
        [Display(Name = "Available?")]
        public DateTime isAvailable { get; set; }

        public static void GetLanguages(ApplicationDbContext context)
        {
            AllLanguages = context.Languages.Where(l => l.isAvailable < DateTime.Now).OrderBy(l => l.Name).ToList();
            LanguagesByid = new Dictionary<string, Language>();
            foreach (Language language in AllLanguages)
            {
                LanguagesByid[language.Id] = language;
            }
        }

    }
}
