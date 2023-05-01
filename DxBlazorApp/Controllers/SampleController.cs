using Microsoft.AspNetCore.Mvc;


namespace Microgate.Extranet.API.Controllers;



public class SampleController : Controller
{
	IWebHostEnvironment _webHostEnvironment;
	private readonly ILogger<FileManagerScriptsApiController> _log;


	public SampleController(IWebHostEnvironment webHostEnvironment)

	{
		_webHostEnvironment = webHostEnvironment;
	}

	[HttpGet]

	[Microsoft.AspNetCore.Mvc.Route("api/GetDir", Name = "GetDir")]
	public string GetDir()
	{
		return $@"{_webHostEnvironment.ContentRootPath}\wwwroot\files";
	}


}
