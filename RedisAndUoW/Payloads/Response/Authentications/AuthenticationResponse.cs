namespace RedisAndUoW.Payloads.Response.Authentications
{
    public class AuthenticationResponse
    {
        public string AccessToken { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }

        public AuthenticationResponse(string accessToken, string username, string email, string status)
        {
            AccessToken = accessToken;
            Username = username;
            Email = email;
            Status = status;
        }
    }
}
