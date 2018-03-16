using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using ApplianceAndElectronicStore.Controllers;

namespace Microsoft.AspNetCore.Mvc
{
    public static class UrlHelperExtensions
    {
        public static string EmailConfirmationLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ConfirmEmail),
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }

        public static string ResetPasswordCallbackLink(this IUrlHelper urlHelper, string userId, string code, string scheme)
        {
            return urlHelper.Action(
                action: nameof(AccountController.ResetPassword),
                controller: "Account",
                values: new { userId, code },
                protocol: scheme);
        }

        public static string PersonalPageLink(this IUrlHelper urlHelper, IPrincipal user)
        {
            string[] roles = { "admin", "manager", "user" },
                     urls = {
                urlHelper.Action("Index", "Orders", "Admin"),
                urlHelper.Action("Index", "Reports", "Manager"),
                urlHelper.Action("Index", "Orders", "Customer")
            };

            string link = "";

            for (int i = 0; i < roles.Length; i++) {
                if (user.IsInRole(roles[i])) {
                    link = urls[i];
                    break;
                } // if
            } // for i

            return link;
        }

        public static string Action(this IUrlHelper urlHelper, string action, string controller, string areaName)
        {
            return urlHelper.Action(action, controller, new { area = areaName });
        }

        public static string LocalUrl(this IUrlHelper urlHelper, string requestedUrl, string defaultUrl = "~/")
        {
            return urlHelper.IsLocalUrl(requestedUrl) ? requestedUrl : defaultUrl;
        }
    }
}
