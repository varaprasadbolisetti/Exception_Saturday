using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;



namespace Exceptionhandlinglearnings
{
    public class NetworkingHandling
    {
        static void Main(string[] args)
        {
            NetworkingHandling networking = new NetworkingHandling();
            networking.HandleNetwokungexceptions();
        }
        public bool HandleNetwokungexceptions()
        {
            try
            {
                AccessAnyApi();
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.Timeout)
            {
                Console.WriteLine("The request timed out. Try later.");
            }
            catch (WebException ex) when (ex.Status == WebExceptionStatus.NameResolutionFailure)
            {
                Console.WriteLine("DNS failed check internet and try later.");
            }
            catch (WebException ex)
            {
                Console.WriteLine($"A network error occured.{ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
            return true;
        }



        static bool AccessAnyApi()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string url = "https://jsonplaceholder.typicod.com/posts";
                    string response = client.DownloadString(url);
                }
            }
            catch (WebException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred in network request: {ex.Message}");
            }
            return true;
        }
    }
}
