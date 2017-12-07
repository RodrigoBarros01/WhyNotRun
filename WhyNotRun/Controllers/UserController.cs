using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using WhyNotRun.BO;
using WhyNotRun.Models.UserViewModel;

namespace WhyNotRun.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        private UserBO _userBo;

        public UserController()
        {
            _userBo = new UserBO();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IHttpActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("");
            }
            var user = await _userBo.Login(loginViewModel.Email, loginViewModel.Password);
            if (user != null)
            {
                return Ok(new AuthenticatedViewModel(user));
            }
            return NotFound();
        }

        [HttpPost]
        [Route("users")]
        /*[ FAZER VALIDAÇÃO ]*/
        public async Task<IHttpActionResult> CreateUser(CreateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("");//passar a msg de erro
            }

            var result = await _userBo.CreateUser(model.ToUser());
            if (result != null)
            {
                return Ok(result);
            }
            return InternalServerError();
        }

        
    }
}
