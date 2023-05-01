using DevExtreme.AspNet.Mvc.FileManagement;
using Microsoft.AspNetCore.Mvc;


namespace Microgate.Extranet.API.Controllers
{


	public class FileManagerScriptsApiController : Controller
	{
		IWebHostEnvironment _webHostEnvironment;
		private readonly ILogger<FileManagerScriptsApiController> _log;


		public FileManagerScriptsApiController(IWebHostEnvironment webHostEnvironment,
			ILogger<FileManagerScriptsApiController> log)
		{
			_webHostEnvironment = webHostEnvironment;
			_log = log;

		}

		[HttpGet]
		[Microsoft.AspNetCore.Mvc.Route("api/file-manager-file-system", Name = "FileManagementFileSystemApi")]
		public object FileSystem( FileSystemCommand command, string arguments)
		{
			try
			{
				var path = _webHostEnvironment.ContentRootPath + "/wwwroot/files";
				var fileSystemProvider = new PhysicalFileSystemProvider(path,
					(fileSystemItem, clientItem) =>
					{
						if (!clientItem.IsDirectory)
							clientItem.CustomFields["url"] = GetFileItemUrl(fileSystemItem);
					}
				);


				var config = new FileSystemConfiguration
				{
					Request = Request,
					FileSystemProvider = fileSystemProvider

				};
				config.AllowDownload = true;

				config.AllowCopy = true;
				config.AllowCreate = true;
				config.AllowMove = true;
				config.AllowDelete = true;
				config.AllowRename = true;
				config.AllowUpload = true;

				config.AllowCreate = true;
				config.AllowUpload = true;
				

				var processor = new FileSystemCommandProcessor(config);
				var result = processor.Execute(command, arguments);
				object res = result.GetClientCommandResult();
				return res;
			}
			catch (Exception ex)
			{
				_log.LogError(ex, ex.Message);
				return BadRequest(ex.Message);
			}

		}

		

		private string GetFileItemUrl(FileSystemInfo fileSystemItem)
		{
			var relativeUrl = fileSystemItem.FullName
				.Replace(_webHostEnvironment.WebRootPath, "")
				.Replace(Path.DirectorySeparatorChar, '/');
			return $"{Request.Scheme}://{Request.Host}{Request.PathBase}{relativeUrl}";
		}
	}
}
