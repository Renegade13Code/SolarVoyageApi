using System.Security.Cryptography;
using System.Text;

namespace SolarVoyage.Aws.Cognito.Service.Utils;

internal static class HmacHelper
{
    public static string ComputeBase64HmacSha256(string clientSecretKey, string username, string clientId)
    {
        // Combine the Username and ClientId
        string message = username + clientId;

        // Convert the secret key and message to byte arrays
        byte[] keyBytes = Encoding.UTF8.GetBytes(clientSecretKey);
        byte[] messageBytes = Encoding.UTF8.GetBytes(message);

        // Compute the HMACSHA256 hash
        using (var hmacsha256 = new HMACSHA256(keyBytes))
        {
            byte[] hashBytes = hmacsha256.ComputeHash(messageBytes);

            // Convert the hash to a Base64 string
            string base64Hash = Convert.ToBase64String(hashBytes);

            return base64Hash;
        }
    }
}