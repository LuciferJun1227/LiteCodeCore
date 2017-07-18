using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LiteCode.WebSite.Areas.SysManager;
using LuckyCode.Core.Redis;
using LuckyCode.IService;
using LuckyCode.WebFrameWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace LuckyCode.WebSite.Areas.SysManager.Controllers
{
    public class Base64Upload
    {
        public string ImageString { get; set; }
    }

    public class HomeController : BaseController
    {
        private IHostingEnvironment _environment;
        private ISysModulesService _modulesService;
        private RedisClientManager _redisClient;
        private ICacheClient _client;

        public HomeController(ICacheClient client, ISysModulesService modulesService,IHostingEnvironment environment, RedisClientManager redisClient)
        {
            _modulesService = modulesService;
            _environment = environment;
            _redisClient = redisClient;
            _client = client;

        }
        // GET: Home
        public ActionResult Index()
        {
            StringBuilder sb = new StringBuilder();
            
            return View();
        }
       
        public async Task<IActionResult> LeftNav()
        {
            string roleId = "";
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Claim claim = ((ClaimsIdentity)User.Identity).Claims.SingleOrDefault(a => a.Type == ClaimTypes.Role);
                if (claim != null)
                {
                    roleId = claim.Value;
                }
            }
            var model =await _modulesService.GetSysModuleViewModels(roleId);
            
            return PartialView("_SiteManagerLeft",model);
        }
        [HttpPost]
        public async Task<IActionResult> Base64Upload(Base64Upload imagestr)
        {
            string saveUrl = "/Uploads/";
            string dirPath = _environment.WebRootPath + saveUrl;
            string fileExt = ".png";
            string newFileName = Guid.NewGuid().ToString("N") + fileExt;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            var filePath = Path.Combine(dirPath, newFileName);
            var reg = new Regex("data:image/(.*);base64,");
            imagestr.ImageString = reg.Replace(imagestr.ImageString, "");
            byte[] imageBytes = Convert.FromBase64String(imagestr.ImageString);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                using (var files = new FileStream(filePath, FileMode.CreateNew))
                {
                    await ms.CopyToAsync(files);
                }
            }
            return Json(new {url = ""});
        }
       /// <summary>
       /// FineUpload 文件上传
       /// </summary>
       /// <param name="upload"></param>
       /// <returns></returns>
       [HttpPost]
       public ActionResult ProcessUpload(FineUpload upload)
       {
           string saveUrl = "/Uploads/";
           string dirPath = _environment.WebRootPath+saveUrl;
           string fileExt = Path.GetExtension(upload.Filename).ToLower();
           string newFileName = Guid.NewGuid().ToString("N") + fileExt;
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            var filePath = Path.Combine(dirPath, newFileName);
           try
           {
               upload.SaveAs(filePath);
           }
           catch (Exception ex)
           {
               return Json(new { success = false, Url = "/Uploads/" + newFileName, isUploaded = false, message = "上传失败" });
           }

           return Json(new { success=true, Url = "/Uploads/" + newFileName, isUploaded = true, message = "上传成功" });
       }
 
       public ActionResult UploadImages()
       {
           string savePath = "/Uploads/Images/";
           string saveUrl = "/Uploads/Images/";
           string fileTypes = "gif,jpg,jpeg,png,bmp";
           int maxSize = 20000000;//20 M

           Hashtable hash = new Hashtable();

           var file = Request.Form.Files[0];
           if (file == null)
           {
               hash = new Hashtable();
               hash["error"] = 0;
               hash["url"] = "请选择文件";
               return Json(hash);
           }

           string dirPath = _environment.WebRootPath+savePath;
           if(!Directory.Exists(dirPath))
           {
               Directory.CreateDirectory(dirPath);
           }
           if (!Directory.Exists(dirPath))
           {
               hash = new Hashtable();
               hash["error"] = 0;
               hash["url"] = "上传目录不存在";
               return Json(hash);
           }

           string fileName = file.FileName;
           string fileExt = Path.GetExtension(fileName).ToLower();

           ArrayList fileTypeList = ArrayList.Adapter(fileTypes.Split(','));

           if (file == null || file.Length > maxSize)
           {
               hash = new Hashtable();
               hash["error"] = 0;
               hash["url"] = "上传文件大小超过限制";
               return Json(hash);
           }

           if (string.IsNullOrEmpty(fileExt) || Array.IndexOf(fileTypes.Split(','), fileExt.Substring(1).ToLower()) == -1)
           {
               hash = new Hashtable();
               hash["error"] = 0;
               hash["url"] = "上传文件扩展名是不允许的扩展名";
               return Json(hash);
           }

           string newFileName = DateTime.Now.ToString("yyyyMMddHHmmss_ffff", DateTimeFormatInfo.InvariantInfo) + fileExt;
            string fileUrl = saveUrl + newFileName;
            using (var files = new FileStream(dirPath+newFileName, FileMode.CreateNew))
                file.OpenReadStream().CopyTo(files);
            
          

           hash = new Hashtable();
           hash["error"] = 0;
           hash["url"] = fileUrl;

           return Json(hash); ;
       }
       
       #region 浏览
       public ActionResult ProcessRequest()
       {
           //String aspxUrl = context.Request.Path.Substring(0, context.Request.Path.LastIndexOf("/") + 1);

           //根目录路径，相对路径
           String rootPath =_environment.WebRootPath+ "/Uploads/Images/";
           //根目录URL，可以指定绝对路径，比如 http://www.yoursite.com/attached/
           String rootUrl = "/Uploads/Images/";
           //图片扩展名
           String fileTypes = "gif,jpg,jpeg,png,bmp";

           String currentPath = "";
           String currentUrl = "";
           String currentDirPath = "";
           String moveupDirPath = "";

           //根据path参数，设置各路径和URL
           String path = Request.Query["path"];
           path = String.IsNullOrEmpty(path) ? "" : path;
           if (path == "")
           {
               currentPath = (rootPath);
               currentUrl = rootUrl;
               currentDirPath = "";
               moveupDirPath = "";
           }
           else
           {
               currentPath = (rootPath);
               currentUrl = rootUrl + path;
               currentDirPath = path;
               moveupDirPath = Regex.Replace(currentDirPath, @"(.*?)[^\/]+\/$", "$1");
           }

           //排序形式，name or size or type
           String order = Request.Query["order"];
           order = String.IsNullOrEmpty(order) ? "" : order.ToLower();

           //不允许使用..移动到上一级目录
           if (Regex.IsMatch(path, @"\.\."))
           {
               Response.WriteAsync("Access is not allowed.");
               
           }
           //最后一个字符不是/
           if (path != "" && !path.EndsWith("/"))
           {
               Response.WriteAsync("Parameter is not valid.");
               
           }
           //目录不存在或不是目录
           if (!Directory.Exists(currentPath))
           {
               Response.WriteAsync("Directory does not exist.");
               
           }

           //遍历目录取得文件信息
           string[] dirList = Directory.GetDirectories(currentPath);
           string[] fileList = Directory.GetFiles(currentPath);

           switch (order)
           {
               case "size":
                   Array.Sort(dirList, new NameSorter());
                   Array.Sort(fileList, new SizeSorter());
                   break;
               case "type":
                   Array.Sort(dirList, new NameSorter());
                   Array.Sort(fileList, new TypeSorter());
                   break;
               case "name":
               default:
                   Array.Sort(dirList, new NameSorter());
                   Array.Sort(fileList, new NameSorter());
                   break;
           }

           Hashtable result = new Hashtable();
           result["moveup_dir_path"] = moveupDirPath;
           result["current_dir_path"] = currentDirPath;
           result["current_url"] = currentUrl;
           result["total_count"] = dirList.Length + fileList.Length;
           List<Hashtable> dirFileList = new List<Hashtable>();
           result["file_list"] = dirFileList;
           for (int i = 0; i < dirList.Length; i++)
           {
               DirectoryInfo dir = new DirectoryInfo(dirList[i]);
               Hashtable hash = new Hashtable();
               hash["is_dir"] = true;
               hash["has_file"] = (dir.GetFileSystemInfos().Length > 0);
               hash["filesize"] = 0;
               hash["is_photo"] = false;
               hash["filetype"] = "";
               hash["filename"] = dir.Name;
               hash["datetime"] = dir.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
               dirFileList.Add(hash);
           }
           for (int i = 0; i < fileList.Length; i++)
           {
               FileInfo file = new FileInfo(fileList[i]);
               Hashtable hash = new Hashtable();
               hash["is_dir"] = false;
               hash["has_file"] = false;
               hash["filesize"] = file.Length;
               hash["is_photo"] = (Array.IndexOf(fileTypes.Split(','), file.Extension.Substring(1).ToLower()) >= 0);
               hash["filetype"] = file.Extension.Substring(1);
               hash["filename"] = file.Name;
               hash["datetime"] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
               dirFileList.Add(hash);
           }
           //Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
           //context.Response.Write(JsonMapper.ToJson(result));
           //context.Response.End();
           return Json(result);
       }

       public class NameSorter : IComparer
       {
           public int Compare(object x, object y)
           {
               if (x == null && y == null)
               {
                   return 0;
               }
               if (x == null)
               {
                   return -1;
               }
               if (y == null)
               {
                   return 1;
               }
               FileInfo xInfo = new FileInfo(x.ToString());
               FileInfo yInfo = new FileInfo(y.ToString());

               return xInfo.FullName.CompareTo(yInfo.FullName);
           }
       }

       public class SizeSorter : IComparer
       {
           public int Compare(object x, object y)
           {
               if (x == null && y == null)
               {
                   return 0;
               }
               if (x == null)
               {
                   return -1;
               }
               if (y == null)
               {
                   return 1;
               }
               FileInfo xInfo = new FileInfo(x.ToString());
               FileInfo yInfo = new FileInfo(y.ToString());

               return xInfo.Length.CompareTo(yInfo.Length);
           }
       }

       public class TypeSorter : IComparer
       {
           public int Compare(object x, object y)
           {
               if (x == null && y == null)
               {
                   return 0;
               }
               if (x == null)
               {
                   return -1;
               }
               if (y == null)
               {
                   return 1;
               }
               FileInfo xInfo = new FileInfo(x.ToString());
               FileInfo yInfo = new FileInfo(y.ToString());

               return xInfo.Extension.CompareTo(yInfo.Extension);
           }
       }

       public bool IsReusable
       {
           get
           {
               return true;
           }
       }
       #endregion
       
    }
}