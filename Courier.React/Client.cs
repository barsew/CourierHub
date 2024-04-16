using Courier.Domain.Models;

namespace Courier.React
{
    // TODO: no nie mam pojecia gdzie mozemy trzymac zalogowanego klienta i w sumie czy jest nam to w ogole potrzebne 
    public static class Client
    {
        public static User? ActiveUser { get; set; }
        public static bool IsUserLogged
        {
            get { return ActiveUser != null; }
        }
    }
}
