

namespace CarServices.Identity.Models
{
    using System.ComponentModel.DataAnnotations;

    public class LoginRequestModel
    {
        public LoginRequestModel()
        {
        }

        public LoginRequestModel(string userName, string password, bool isCurrentlyRegistered)
        {
            this.UserName = userName;
            this.Password = password;
            this.IsCurrentlyRegistered = isCurrentlyRegistered;
        }

        [Required]
        public string UserName { get; set; }


        [Required]
        public string Password { get; set; }

        [Required]
        public bool IsCurrentlyRegistered { get; set; } = false;
    }
}
