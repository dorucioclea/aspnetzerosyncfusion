using System.ComponentModel.DataAnnotations;

namespace Practia.AppDemo.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
