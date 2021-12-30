using System;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using DAL.Services;
using Domain.Download;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class DownloadService : IDownloadService
    {
        private readonly DbSet<File> _fileRepository;
        private readonly DbSet<Download> _downloadRepository;
        private readonly ICommonDataService _db;
        
        public DownloadService(ICommonDataService dataService)
        {
            _fileRepository = dataService.GetDbSet<File>();
            _downloadRepository = dataService.GetDbSet<Download>();
            _db = dataService;
        }

        public async Task<Byte[]> DownloadLastVersionAsync()
        {
            var task = await Task.Run(DownloadLastVersion);
            return task;
        }

        public void MakeDownloadRecordInDb(string version, string ip)
        {
            var now = DateTime.Now;
            var download = new Download()
            {
                Id = Guid.NewGuid(),
                Time = TimeOnly.FromDateTime(now),
                Date = DateOnly.FromDateTime(now),
                Version = version,
                IP = ip
            };
            _downloadRepository.Add(download);
            _db.SaveChanges();
        }

        private Byte[] DownloadLastVersion()
        {
            var res = _fileRepository.FirstOrDefault(e=>e.Ext=="apk")?.Data;
            return res;
        }
    }
}