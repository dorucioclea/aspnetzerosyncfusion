using System.ComponentModel.DataAnnotations;

namespace Practia.AppDemo.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}