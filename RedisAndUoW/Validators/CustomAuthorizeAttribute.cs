using Microsoft.AspNetCore.Authorization;
using RedisAndUoW.Enums;
using RedisAndUoW.Utils;

namespace RedisAndUoW.Validators
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public CustomAuthorizeAttribute(params RoleEnum[] roleEnums)
        {
            var allowedRolesAsString = roleEnums.Select(x => x.GetDescriptionFromEnum());
            Roles = string.Join(",", allowedRolesAsString);
        }
    }
}
