using ShortLink.DAL.Identity.Enums;

namespace ShortLink.DAL.Identity
{
    public static class RoleExtension
    {
        public static string ToIdentityRole (this Role role)
        {
            return role.ToString ();
        }
    }
}
