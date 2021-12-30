using System;
using System.Threading.Tasks;

namespace Domain.Download
{
    public interface IDownloadService
    {
        Task<Byte[]> DownloadLastVersionAsync();
        void MakeDownloadRecordInDb ( string version, string ip);
    }
}