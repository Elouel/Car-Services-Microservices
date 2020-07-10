namespace CarServices.Identity.Models
{
    public class LoginResponseModel
    {
        public LoginResponseModel(string token, string userId, bool isCurrentlyRegisterd = false)
        {
            this.Token = token;
            this.UserId = userId;
            IsCurrentlyRegisterd = isCurrentlyRegisterd;
        }
        public string UserId { get; set; }
        public bool IsCurrentlyRegisterd { get; }
        public string Token { get; set; }
    }
}
