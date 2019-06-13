using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Hangfire_PoC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget Job Executed"));
            BackgroundJob.Schedule(() => Console.WriteLine("Delayed job executed"), TimeSpan.FromMinutes(1));
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Minutely Job executed"), Cron.Minutely);
            var id = BackgroundJob.Enqueue(() => Console.WriteLine("Hello, "));
            BackgroundJob.ContinueJobWith(id, () => Console.WriteLine("world!"));
            return View();
        }
    }
}