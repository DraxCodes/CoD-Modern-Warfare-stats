using System.Threading.Tasks;

namespace CodMwStats.ApiWrapper
{
    public class ApiProcessor
    {
        public static async Task<string> GetUser(string apiUrl)
        {
            var jsonAsString = await ApiHelper.ApiClient.GetStringAsync(apiUrl);
            return jsonAsString ?? string.Empty;
        }
    }
}
