using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace RtlAPI.Helper
{
    public static class RequestHelper
    {
        public static T RequestMaker<T>(object dataToSend, string url, Method method)
        {
            var urlToRequest = $"{url}";
            var client = new RestClient(urlToRequest);

            var request = new RestRequest(method);
            request.AddHeader("Cache-Control", "no-cache");

            if (method == Method.POST)
            {
                request.AddParameter("undefined", JsonConvert.SerializeObject(dataToSend), ParameterType.RequestBody);
                request.RequestFormat = DataFormat.Json;
            }

            var response = client.Execute(request);
            return response.StatusCode == HttpStatusCode.OK
                ? JsonConvert.DeserializeObject<T>(response.Content)
                : default(T);
        }

        public static T Post<T>(object dataToSend, string url)
        {
            return RequestMaker<T>(dataToSend, url, Method.POST);
        }

        public static T Get<T>(string url)
        {
            return RequestMaker<T>(string.Empty, url, Method.GET);
        }
    }
}