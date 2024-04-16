using Azure;

namespace Courier.React.ExternalAPIClients
{
    public class IdentityResponse
    {
        public string Access_Token { get; set; }
        public int Expires_In { get; set; }
        public string Token_Type { get; set; }
        public string Scope { get; set; }

        public AccessToken MakeAccessToken()
        {
            return new AccessToken()
            {
                JWT = this.Access_Token,
                ExpireDate = DateTime.Now.AddSeconds(this.Expires_In),
                TokenType = this.Token_Type,
                Scope = this.Scope
            };
        }
    }
}
