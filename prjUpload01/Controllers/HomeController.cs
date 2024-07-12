using Microsoft.AspNetCore.Mvc;

namespace prjUpload01.Controllers
{
	public class HomeController : Controller
	{
		string _path;

		public HomeController(IWebHostEnvironment hostEnvironment)
		{
			//會抓到Images下，圖檔完整路徑
			_path = $@"{hostEnvironment.WebRootPath}\Images";
		}

		FileInfo[] GetFiles()
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(_path);
			FileInfo[] files = directoryInfo.GetFiles();
			return files;
		}

		public IActionResult Index()
		{
			return View(GetFiles());
		}
		[HttpPost]
        public async Task<IActionResult> Index(IFormFile formFile)
        {
			if (formFile != null)
            {
				string savePath = $@"{_path}\{formFile.FileName}";
				using (var steam = new FileStream(savePath,FileMode.Create))
				{
					await formFile.CopyToAsync(steam);
				}
            }
            //回檔案列表
            return View(GetFiles());
        }
    }
}
