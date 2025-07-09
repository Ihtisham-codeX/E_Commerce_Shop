using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace E_Commerce_Shop.DL
{
    public static class CloudinaryHelper
    {
        private static Cloudinary cloudinary;
        static CloudinaryHelper()
        {
            Account account = new Account(
            "djn34a12e",
            "768756861134361",
            "G5cykXplBLDYCUNYLbMGPqZga9s");


            cloudinary = new Cloudinary(account);
        }
        public static string UploadImage(string FilePath)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(FilePath),
                UseFilename = true,
                UniqueFilename = false,
                Overwrite = true,
            };

            var uploadResult = cloudinary.Upload(uploadParams);
            return uploadResult.SecureUrl.AbsoluteUri;
        }
    }
}
