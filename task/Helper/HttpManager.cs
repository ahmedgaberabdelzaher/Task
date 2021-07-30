using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using Xamarin.Essentials;

namespace task.Helper
{
    public static class HttpManager
    {
        public static bool IsInternet()
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static async Task<Tuple<T, bool, string>> GetAsync<T>(string requestUrl) where T : class

        {
            try
            {
                if (IsInternet())
                {
                    HttpClientHandler clientHandler = new HttpClientHandler();
                    clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
                    var client = new System.Net.Http.HttpClient(clientHandler);
                    //var client = new System.Net.Http.HttpClient();
                    var response = await Policy
                    .Handle<HttpRequestException>(ex => !ex.Message.ToLower().Contains("404"))
                    .RetryAsync
                    (
                        retryCount: 3,
                        onRetry: (ex, time) =>
                        {
                            Console.WriteLine($"Something went wrong: {ex.Message}, retrying...");
                        }
                    )
                    .ExecuteAsync(async () =>
                    {
                        Console.WriteLine($"Trying to fetch remote data...");

                        var resultJson = await client.GetAsync(requestUrl);

                        return resultJson;
                    });

                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            try
                            {
                                var responseJson = await response.Content.ReadAsStringAsync();
                                var JsonObject = JsonConvert.DeserializeObject<T>(responseJson);
                                return Tuple.Create(JsonObject, true, "");

                            }
                            catch (Exception ex)
                            { throw ex; }




                        }
                        else
                        {

                            return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false,"Server Error");
                        }
                    }
                    else
                    {
                        return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, "Server error or Internet Connection");
                    }

                }
                else
                {
                    return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false,"No internet Connection Please Check Your Internet");
                }

            }
            catch (System.Exception exp)
            {
                return Tuple.Create((T)Activator.CreateInstance(typeof(T)), false, exp.Message);
            }

        }

    }
}
