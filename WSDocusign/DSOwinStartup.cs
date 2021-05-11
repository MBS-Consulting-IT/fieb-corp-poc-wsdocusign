using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security;
using System.Configuration;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: Microsoft.Owin.OwinStartupAttribute(typeof(.DocuSign.eSignature.DSOwinStartup))]
namespace WSDocusign

{
    public class DSOwinStartup
    {
        public void ConfigureAuth(IAppBuilder app)
        {

            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            app.Use(typeof(DocuSignAuthenticationMiddleware), app, new DocuSignAuthenticationOptions
            {
                ClientId = ConfigurationManager.AppSettings["IntegrationKey"],
                ClientSecret = ConfigurationManager.AppSettings["SecretKey"],
                AuthorizationEndpoint = "https://account-d.docusign.com/oauth/auth",
                TokenEndpoint = "https://account-d.docusign.com/oauth/token",
                UserInformationEndpoint = "https://account-d.docusign.com/oauth/userinfo",
                AppUrl = ConfigurationManager.AppSettings["AppUrl"],
                CallbackPath = new PathString("/ds/Callback")
            });
        }
    }
}
}