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
            new Feed(1, "fajar","a","19 maret 2022","selamat pagi bandung","a"),
            new Feed(2, "andri","b","20 maret 2022","selamat pagi bandung","b"),
            new Feed(3, "fandi","c","21 maret 2022","selamat pagi bandung","c"),
            new Feed(4, "brian","d","22 maret 2022","selamat pagi bandung","d"),
            new Feed(5, "syahrul","e","23 maret 2022","selamat pagi bandung","e"),
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

        // // GET: LoginController/Create
        // public ActionResult Create()
        // {
        //     return View();
        // }

        // // POST: LoginController/Create
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Create(TweetViewModel collection)
        // {
        //     if(!ModelState.IsValid)
        //     {
        //         return View(collection);
        //     }

        //     try
        //     {
        //         return RedirectToAction(nameof(Index));
        //     }
        //     catch
        //     {
        //         return View(collection);
        //     }
        // }

        // // GET: Controller/Edit/5
        // public ActionResult Edit(int id)
        // {
        //     return View();
        // }

        // // POST: TweetController/Edit/5
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public ActionResult Edit(int id, IFormCollection collection)
        // {
        //     try
        //     {
        //         return RedirectToAction(nameof(Index));
        //     }
        //     catch
        //     {
        //         return View();
        //     }
        // }

        // // GET: TweetController/Delete/5
        // public ActionResult Delete(int id)
        // {
        //     return View();
        // }

}
