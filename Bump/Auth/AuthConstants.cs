namespace Bump.Auth
{
    public static class AuthConstants
    {
        public const string Admin = "Admin";
        public const string Moderator = "Moderator";
        public const string User = "User";

        public const string LoginPath = "/Identity/Account/Logout";
        public const string LogoutPath = "/Identity/Account/Login";
        public const string AccessDeniedPath = "/Identity/Account/AccessDenied";

        public static readonly string[] Roles = {Admin, Moderator, User};
    }
}