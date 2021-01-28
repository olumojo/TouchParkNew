using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;

namespace TouchPark.Services
{
    public class WebServiceClient
    {
        private string _siteBoxRef;
        private string _terminalLink;
        public WebServiceClient()
        {
            _siteBoxRef = ConfigurationManager.AppSettings["SiteBoxRef"].ToString();
            _terminalLink = ConfigurationManager.AppSettings["TerminalLink"].ToString();
        }
        public string PostWebResponse(string url, object body)
        {
            var restClient = new RestClient();
            restClient.Authenticator = new HttpBasicAuthenticator(_siteBoxRef, _terminalLink);
            var restRequest = new RestRequest(url, Method.GET);
            restRequest.AddHeader("Accept", "application/json");
            restRequest.AddHeader("Content-Type", "application/json");

            var response = restClient.Execute(restRequest);

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent && response.StatusCode != HttpStatusCode.Created)
            {
                if (response.ErrorException == null)
                {
                    if (string.IsNullOrEmpty(response.ErrorMessage))
                    {
                        throw new Exception($"Failed to call {url} {response.ErrorMessage}");
                    }
                    else
                    {
                        throw new Exception($"Failed to call {url} {response.Content}");
                    }
                }
                else
                {
                    throw new Exception($"Failed to call {url}", response.ErrorException);
                }
            }

            return response.Content;
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
