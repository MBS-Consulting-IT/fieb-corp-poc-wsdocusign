using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using DocuSign.eSign.Api;
using DocuSign.eSign.Client;
using DocuSign.eSign.Model;

namespace WSAspNetMVCDocusign
{
    public class GetEnvelope
    {

        public static Envelope GetEnvelopeData(string accountId, string envID, string accessToken, string basePath)
        {
            ApiClient apiClient = new ApiClient(basePath);
            apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + accessToken);
            EnvelopesApi envelopesApi = new EnvelopesApi(apiClient);
            Envelope results = envelopesApi.GetEnvelope(accountId, envID);
            return results;
        }

        public static Recipients RecipentsData(string accountId, string envID, string accessToken, string basePath)
        {
            ApiClient apiClient = new ApiClient(basePath);
            apiClient.Configuration.DefaultHeader.Add("Authorization", "Bearer " + accessToken);
            EnvelopesApi envelopesApi = new EnvelopesApi(apiClient);
            Recipients results = envelopesApi.ListRecipients(accountId, envID);

            return results;
        }
    }
}