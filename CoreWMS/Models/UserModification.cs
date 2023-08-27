using System.ComponentModel.DataAnnotations;

namespace CoreWMS.Models
{
    public class UserModification
    {
        [Required]
        public string UserName { get; set; }

        public string UserId { get; set; }

        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string[]? AddIds { get; set; }

        public string[]? DeleteIds { get; set; }
    }
}