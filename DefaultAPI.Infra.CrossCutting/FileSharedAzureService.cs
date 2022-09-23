using DefaultAPI.Domain.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.File;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Infra.CrossCutting
{
    public static class FileSharedAzureService
    {
        private static CloudFileClient GetStorageClientFile()
        {
            string accountName = "Azure-FileShare-AccountName";
            string keyValue = "Azure-FileShare-KeyValue";

            CloudStorageAccount storageAccount =
              new CloudStorageAccount(
                new StorageCredentials(accountName, keyValue), true);

            return storageAccount.CreateCloudFileClient();
        }

        /// <summary>
        /// Metodo respónsavel pelo download de um documento armazenado no file share do Azure
        /// </summary>
        /// <param name="fileName">Nome completo do arquivo. ex: arquivo.pdf</param>
        /// <param name="fileExtension">Sua extensão somente. ex: .pdf</param>
        /// <param name="shareReference">Pasta em que o arquivo se encontra</param>
        /// <returns></returns>
        public static async Task<FileShareModel> DownloadFile(string fileName, string fileExtension = ".pdf", string shareReference = "documentFiles")
        {
            try
            {
                CloudFileShare fileShare = GetStorageClientFile().GetShareReference(shareReference);

                if (await fileShare.ExistsAsync())
                {
                    CloudFileDirectory rootDir = fileShare.GetRootDirectoryReference();
                    CloudFile file = rootDir.GetFileReference(fileName);
                    var ms = new MemoryStream();
                    return new FileShareModel(fileName, fileExtension, await file.OpenReadAsync());
                }

                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
