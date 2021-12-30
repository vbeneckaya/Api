using System;
using System.IO;
using Domain.Download;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private readonly IDownloadService _downloadService;
        private readonly string _version;
        private readonly string _pathSource;
        
        public DownloadController(IConfiguration configuration, IDownloadService downloadService) 
        {
            _downloadService = downloadService;
            _version = configuration.GetSection("Download").GetSection("Version").Value;
            _pathSource = configuration.GetSection("Download").GetSection("Path").Value;
        }
        
        [HttpGet]
        public ActionResult<string> Version()
        {
            return _version;
        }

        [HttpGet]
        public FileContentResult Apk()
        {
            var pathSource = _pathSource;
            
            using (FileStream fsSource = new FileStream(pathSource, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader pgReader = new BinaryReader(new BufferedStream(fsSource)))
                {
                    byte[] data = pgReader.ReadBytes(Convert.ToInt32(fsSource.Length));

                  //  var http = this.HttpContext;
                    if (HttpContext.Connection.RemoteIpAddress != null)
                        _downloadService.MakeDownloadRecordInDb(_version,
                             HttpContext.Connection.RemoteIpAddress.ToString()
                        );

                    return new FileContentResult(data, "application/vnd.android.package-archive")
                    {
                        FileDownloadName = "bibigame.apk"
                    };
                }
            }
        }
        
    }
}