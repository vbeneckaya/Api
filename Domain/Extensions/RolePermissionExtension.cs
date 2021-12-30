using Common.Enums;


namespace Domain.Extensions
{
    public static class RolePermissionExtension
    {
        public const string ClaimType = "Permission";

        public static string GetPermissionName(this RolePermissions permission)
        {
            return $"{ClaimType}.{permission}";
        }
    }
}
