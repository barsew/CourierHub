using Courier.Domain.Models;

namespace Courier.React.Models.UserModel
{
    public class UserInfo
    {
        public string? name { get; set; }
        public string? email { get; set; }
        public string? given_name { get; set; }
        public string? family_name { get; set; }
        public string? sub { get; set; }
        public string? phone_number { get; set; }
        public string? role { get; set; }

        public User MakeUser()
        {
            var user = new User();
            var subject = new Subject();
            subject.Surname = family_name != null ? family_name : "";
            subject.Email = email != null ? email : "";
            subject.Address = null; // TODO: no auth0 nie daje info o adresie wiec w update mozna najwyzej dodac jak kiedy user poda
            subject.Phone = phone_number != null ? phone_number : ""; //
            subject.Name = given_name != null ? given_name : "";
            user.Auth0Id = sub != null ? sub : "";
            user.Subject = subject;
            user.Company = null;
            user.Role = ConvertRole(role);
            return user;
        }
        public static Role ConvertRole(string? role)
        {
            if(role == null)
            {
                return Role.NormalUser;
            }

            switch (role)
            {
                case "User":
                    return Role.NormalUser;
                case "Admin":
                    return Role.Admin;
                case "OfficeWorker":
                    return Role.OfficeWorker;
                case "Courier":
                    return Role.Courier;
                case "ApiPartner":
                    return Role.ApiPartner;
                default:
                    return Role.NormalUser;
            }

        }
    }
}
