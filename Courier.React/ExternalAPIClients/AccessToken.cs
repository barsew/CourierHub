namespace Courier.React.ExternalAPIClients
{
    public class AccessToken
    {
        public string JWT { get; set; }
        public DateTime ExpireDate { get; set; }
        public string TokenType { get; set; }
        public string Scope { get; set; }
    }
}
