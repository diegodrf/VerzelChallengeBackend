using System.Text.RegularExpressions;
using WebApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApi.Utils
{
    public class ImageUtils
    {
        public static string GetImageExtentionFromBase64(string base64)
        {
            var imageExtension = base64.Split("/")[1].Split(";")[0];
            return $".{imageExtension}";
        }
        public static string ParseBase64(string base64)
        {
            var data = new Regex(@"^data:image\/[a-z]+;base64,")
                .Replace(base64, "");
            return data;
        }

        public static async Task SaveImage(string filename, string base64)
        {
            var imageDirPath = @"C:\Users\diego\source\repos\VerzelChallengeBackend\WebApi\Assets\Images\";
            var bytes = Convert.FromBase64String(base64);

            await File.WriteAllBytesAsync($"{imageDirPath}{filename}", bytes);
        }
    }
}
