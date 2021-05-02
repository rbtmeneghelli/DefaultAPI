
using DefaultAPI.Domain.Models;
using ServiceTria.Framework.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Interfaces
{
    public interface IGeneralService
    {
        long GetCurrentUserId();
        string GetCurrentUserName();
        string CreateJwtToken(Credentials credentials);
        Task<FileDownloadDTO> Export2Excel<T>(List<T> result, string fileName = "ExportExcel.xlsx");
        Task<RequestData> RequestDataToExternalAPI(string url);
        Task<RequestData> RequestLogin(string url, string key = "");
        Task SendEmail(EmailConfig emailConfig);
        Task<ResultReturned> RunSqlProcedure(string procName, string paramName, string paramValue);
        Task<ResultReturned> RunSqlBackup(string directory);
        Task<MemoryStream> Export2Zip(string directory, int typeFile = 2);
    }
}
