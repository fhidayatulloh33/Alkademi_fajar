using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Facebooker.Models;
using Facebooker.Services;

namespace Facebooker.Controllers
{
    public class FeedController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IFeedService _feedService;

        public FeedController(IFileService fileService, IFeedService feedService)
        {
            _fileService = fileService;
            _feedService = feedService;
        }

        public async Task<IActionResult> Index()
        {
            var data = _fileService.Read();
//            var feeds = _feedService.GetFeeds();
            return View(data);
        }

        [HttpPost]
        public async Task<ActionResult> Create(FeedViewModel collection)
        {
            await _fileService.Write(collection);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Feed()
        {
            return View();
        }
    }
}
