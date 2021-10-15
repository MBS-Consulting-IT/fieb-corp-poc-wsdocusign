using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using DocuSign.eSign.Client;
using DocuSign.eSign.Client.Auth;
using static DocuSign.eSign.Client.Auth.OAuth.UserInfo;
using System.Configuration;
using System.Text;
using System.IO;
using DocuSign.eSign.Model;

namespace WSAspNetMVCDocusign
{
    /// <summary>
    /// Descrição resumida de WSAssinatura
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class WSAssinatura : System.Web.Services.WebService
    {
        protected string accessToken;
        protected string accountId;
        protected string baseUri;

        [WebMethod]
        public string CriaEnvelope(string signerEmail, string signerName, string signerEmail2, string signerName2, string signerEmail3, string signerName3, string docPdf, string envStatus)
        {
            AuthenticateWithJWT();
            return SigningViaEmail.SendEnvelopeViaEmail(signerEmail, signerName, signerEmail2, signerName2, signerEmail3, signerName3, accessToken, baseUri, accountId, docPdf, envStatus);

        }

        [WebMethod]
        public string ConsultaStatus(string envID)
        {
            AuthenticateWithJWT();
            return GetEnvelope.GetEnvelopeData(accountId, envID, accessToken, baseUri).Status;

        }
        [WebMethod]
        public Recipients ConsutaRecipients(string envID)
        {
            AuthenticateWithJWT();
            return GetEnvelope.RecipentsData(accountId, envID, accessToken, baseUri);

        }
        public void AuthenticateWithJWT()
        {
            ApiClient apiClient = new ApiClient();
            string ik = ConfigurationManager.AppSettings["IntegrationKey"];
            string userId = ConfigurationManager.AppSettings["userId"];
            string authServer = ConfigurationManager.AppSettings["AuthServer"];
            string rsaKey = ConfigurationManager.AppSettings["RSAKey"];
            OAuth.OAuthToken authToken = apiClient.RequestJWTUserToken(ik, userId, authServer, Encoding.UTF8.GetBytes(File.ReadAllText(Server.MapPath("private.key"))), 1);
            
            apiClient.SetOAuthBasePath(authServer);
            OAuth.UserInfo userInfo = apiClient.GetUserInfo(authToken.access_token);
            Account acct = null;

            var accounts = userInfo.Accounts;
            {
                acct = accounts.FirstOrDefault(a => a.IsDefault == "true");
            }
            accountId = acct.AccountId;
            baseUri = acct.BaseUri + "/restapi";
            accessToken = authToken.access_token;
            return;
        }
    }
}
