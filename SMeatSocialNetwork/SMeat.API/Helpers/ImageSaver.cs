using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SMeat.API.Helpers
{
    public class ImageSaver
    {
        private static Random random = new Random();

        public string SaveImage(IFormFile image, IHostingEnvironment env)
        {
            var modelImage = image;

            var ms = new MemoryStream();

            string[] separators = new string[] { "image/" };

            var fileName = Guid.NewGuid();
            var fileType = modelImage.ContentType.Split(separators[0], StringSplitOptions.RemoveEmptyEntries);

            modelImage.CopyTo(ms);
            var fileBytes = ms.ToArray();

            var webRoot = env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\");

            var filePath = webRoot + Path.GetFileName(fileName + "." + fileType[0]);
            var returnFilePath = "http://localhost:27121/images/" + fileName + "." + fileType[0];

            File.WriteAllBytes(filePath, fileBytes); //creating file from bytes

            return returnFilePath;
        }

        //public static string RandomString(int length)
        //{
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //    return new string(Enumerable.Repeat(chars, length)
        //      .Select(s => s[random.Next(s.Length)]).ToArray());
        //}
    }
}
