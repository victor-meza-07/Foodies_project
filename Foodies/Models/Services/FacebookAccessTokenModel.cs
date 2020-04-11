namespace Foodies.Models.Services 
{
    /// <summary>
    /// This will only be accessed during runtime. 
    /// </summary>
    public class FacebookAccessTokenModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
    }

}