using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Minerva.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Minerva.ApiControllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        private UserManager<ApplicationUser> UserManager { get; private set; }
        public AccountController()
        {
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new MinervaDbContext()));
        }

        [AllowAnonymous]
        public async Task<IHttpActionResult> Login(LoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await UserManager.FindAsync(model.Username, model.Password);
            if (user != null)
            {
                await SignInAsync(user, model.RememberMe);
                return Ok();
            }

            return NotFound();
        }

        public async Task<IHttpActionResult> Register()
        {
            return BadRequest();
        }

        private Task<IHttpActionResult> SignInAsync(ApplicationUser user, bool? nullable)
        {
            throw new NotImplementedException();
        }

        private Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            return null;
        }
    }
}
