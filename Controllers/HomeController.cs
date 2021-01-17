using AGC_CHURCH.Context;
using AGC_CHURCH.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AGC_CHURCH.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AgcDbContext _context;


        public HomeController(ILogger<HomeController> logger, AgcDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        //name

            
             public async Task<IActionResult> addbanch(string new_branch)
        {
            Random random = new Random();
            int randommm = random.Next(1000000, 9000000);
            var branch = new Branches
            {
                Branc = new_branch,
                id = randommm.ToString()
            };
            _context.Add(branch);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> addnote(string new_notification ,string time, string Title)
        {
            Random random = new Random();
            int randommm = random.Next(1000000, 9000000);
            var note = new Notifications
            {
                Notification = new_notification,
                id = randommm.ToString(),
                Timestamp=time,
                Title=Title
            };
            _context.Add(note);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        

            public async Task<IActionResult> Index( string category,string branch,string babtised)
        {
            var ifExist = await _context.Users.FirstOrDefaultAsync(j => j.id == User.Identity.Name);
            var Users =  _context.Users.Where(k=>(k.Category.Contains(category)|| category==null) || (k.ChurchBranch.Contains(branch) || branch == null) || (k.BabtismStatus.Contains(babtised) || babtised == null)).ToList();
            var AllllBranches = _context.Branches.ToList();
            var AllNotifications = _context.Notifications.ToList();
            ViewBag.Notifications = AllNotifications;
            ViewBag.AllBranches = AllllBranches;
            ViewBag.Users = Users;
            ViewBag.exist = ifExist.Gender;

            //counting
            ViewBag.Note = AllNotifications.Count();
            ViewBag.Usersss = _context.Users.Count();
                ViewBag.branchesss = AllllBranches.Count();

                ViewBag.gender = new SelectList(_context.Gender, "strGender", "strGender");
            ViewBag.category = new SelectList(_context.agpoCategorie, "strAgpo", "strAgpo");
            ViewBag.branches = new SelectList(_context.Branches, "Branc", "Branc");

            return View(Users);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> notbabtised( string Gender, string AGPO, string branch,string phone)
        {

            var ifExist = await _context.Users.FirstOrDefaultAsync(j => j.id == User.Identity.Name);
            if (ifExist != null)
            {
                var log = _context.Users.Where(x => x.id == User.Identity.Name).SingleOrDefault();
                log.Gender = Gender;
                log.Category = AGPO;
                log.ChurchBranch = branch;
                log.BabtismStatus = "false";
                log.Phone = phone;

                _context.Add(log);

                _context.Entry(log).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        
                public async Task<IActionResult> AllNotifications(string? id)

        {
            if (id == null)
            {
                return NotFound();
            }

            var AllNotifications = await _context.Notifications.FirstOrDefaultAsync(m => m.id == id);
            if (AllNotifications == null)
            {
                return NotFound();
            }

            return View(AllNotifications);
        }

        [HttpPost, ActionName("AllNotifications")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AllNotification(string id)
        {
            var AllNotifications = await _context.Notifications.FindAsync(id);
            _context.Notifications.Remove(AllNotifications);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var branches = await _context.Branches.FirstOrDefaultAsync(m => m.id == id);
            if (branches == null)
            {
                return NotFound();
            }

            return View(branches);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var userRegistration = await _context.Branches.FindAsync(id);
            _context.Branches.Remove(userRegistration);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
