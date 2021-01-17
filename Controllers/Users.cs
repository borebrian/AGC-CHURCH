using AGC_CHURCH.Context;
using AGC_CHURCH.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AGC_CHURCH.Controllers
{
    public class UsersController : Controller
    {
        private readonly AgcDbContext _context;

        public UsersController(AgcDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View();
        }
        public async Task<IActionResult> PhoneAuth()
        {
            return View();
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string id, string Name, string Email, string Phone, string ImageUrl, string provider, string Role,string BabtismStatus, string ChurchBranch, string Category, string Gender)
        {
            var ifExist = await _context.Users.FirstOrDefaultAsync(j => j.id == id);
            if (ifExist != null)
            {
                var log = _context.Users.Where(x => x.id == id).SingleOrDefault();
                log.id = id;
                log.Name = Name;
                log.Email = Email;
                log.Phone = Phone;
                log.ImageUrl = ImageUrl;
                log.provider = provider;
                //log.Gender = Gender;
                //log.Category = Category;
                //log.ChurchBranch = ChurchBranch;
                //log.BabtismStatus = BabtismStatus;
                _context.Add(log);

                _context.Entry(log).State = EntityState.Modified;
                _context.SaveChanges();
                var key = Encoding.ASCII.GetBytes("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
                var JWToken = new JwtSecurityToken(
                    issuer: "",
                    audience: "",
                    claims: GetUserClaims(ifExist),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );
                var JWTtoken = new JwtSecurityTokenHandler().WriteToken(JWToken);
                if (JWTtoken != null)
                {
                    HttpContext.Session.SetString("JWToken", JWTtoken);
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                var a = new User
                {
                    Email = Email,
                    Name = Name,
                    id = id,
                    ImageUrl = ImageUrl,
                    Phone = Phone,
                    provider = provider,
                    Role = Role,
                    Gender = Gender,
                    Category = Category,
                    ChurchBranch = ChurchBranch,
                    BabtismStatus = BabtismStatus
            };
                _context.Add(a);
                await _context.SaveChangesAsync();
                var existing = await _context.Users.FirstOrDefaultAsync(j => j.id == id);
                var key = Encoding.ASCII.GetBytes("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
                var JWToken = new JwtSecurityToken
                   (
                    issuer: "",
                    audience: "",
                    claims: GetUserClaims(existing),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddDays(1)).DateTime,
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                  );
                var JWTtoken = new JwtSecurityTokenHandler().WriteToken(JWToken);
                if (JWTtoken != null)
                {
                    HttpContext.Session.SetString("JWToken", JWTtoken);
                    return RedirectToAction("Index", "Home");
                }
            }

            return View();
        }

        private IEnumerable<Claim> GetUserClaims(User user)
        {
            List<Claim> claims = new List<Claim>();
            Claim _claim;
            _claim = new Claim(ClaimTypes.Name, user.id);
            claims.Add(_claim);
            //_claim = new Claim(ClaimTypes.NameIdentifier, user.strUserId);
            //claims.Add(_claim);
            //_claim = new Claim("EMAILID", user.strEmail);
            //claims.Add(_claim);
            //_claim = new Claim("PHONE", user.strPhone);
            //claims.Add(_claim);
            _claim = new Claim(ClaimTypes.Role, user.Role);
            claims.Add(_claim);
            if (user.Role != "")
            {
                _claim = new Claim(user.Role, user.Role);
                claims.Add(_claim);
            }
            return claims.AsEnumerable<Claim>();
        }



        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id,Name,Email,Phone,ImageUrl,provider,Role")] User user)
        {
            if (id != user.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.id == id);
        }

    }
}
