using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using facebook.Models;

namespace facebook.Controllers;

public class FeedController : Controller
{
    List<Feed> _listFeed = new List<Feed>();
    private readonly ILogger<FeedController> _logger;

    public FeedController(ILogger<HomeController> logger)
    {
        // _logger = logger;
        _listFeed = new List<Feed>(){
            new Feed(1, "fajar","a","19 maret 2022","selamat pagi bandung","a",null),
            new Feed(2, "andri","b","20 maret 2022","selamat pagi bandung","b",null),
            new Feed(3, "fandi","c","21 maret 2022","selamat pagi bandung","c",null),
            new Feed(4, "brian","d","22 maret 2022","selamat pagi bandung","d",null),
            new Feed(5, "syahrul","e","23 maret 2022","selamat pagi bandung","e",null),
        };
    }


    //lebih lanjut nanti
    [Route("feed/detail")]
    public IActionResult Index()
    {
        ViewData["ListFeed"] = _listFeed;
        ViewBag.ListFeed = _listFeed;

        return View(_listFeed);
    }

}
