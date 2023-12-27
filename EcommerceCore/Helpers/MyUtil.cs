using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Text;

namespace EcommerceCore.Helpers
{
    public class MyUtil
    {
        public static string UploadHinh(IFormFile Hinh, string folder)
        {
            try
            {
                var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot",
                "Hinh", folder, Hinh.FileName);//địa chỉ project
                using (var myfile = new FileStream(fullPath, FileMode.CreateNew))
                {
                    Hinh.CopyTo(myfile);

                }
                return Hinh.FileName;//return nếu ko lỗi
            }
            catch (Exception ex)
            {
                return string.Empty;
            }

        }
        public static string GenerateRandomKey(int length = 5)
        {
            var pattern = @"QSQQBNJMKasdgfhjksdfvgbhnjmQWERTYUIOdfghjkl!/*\|";
            var sb = new StringBuilder();
            var rd = new Random();
            for (int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }
            return sb.ToString();
        }
    }
}
