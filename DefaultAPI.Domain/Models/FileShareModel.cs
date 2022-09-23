using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DefaultAPI.Domain.Models
{
    public class FileShareModel
    {
        public bool IsSuccess { get; set; } = false;
        public string FileName { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public string DataBase64 { get; set; } = string.Empty;

        public FileShareModel()
        {

        }

        public FileShareModel(string fileName, string fileExtension, Stream streamFile)
        {
            if (streamFile != null)
            {
                IsSuccess = true;
                FileName = fileName;
                FileExtension = fileExtension;
                DataBase64 = GetBase64FromStream(streamFile);
            }
        }

        private string GetBase64FromStream(Stream streamFile)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                streamFile.CopyTo(ms);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }
}
