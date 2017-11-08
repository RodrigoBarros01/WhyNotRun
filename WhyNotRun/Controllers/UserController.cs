using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WhyNotRun.BO;
using WhyNotRun.Models.UserViewModel;

namespace WhyNotRun.Controllers
{
    public class UserController : ApiController
    {
        //private TokenBO _tokenBo;
        private UserBO _userBo;

        public UserController()
        {
            //_tokenBo = new TokenBO();
            _userBo = new UserBO();
        }

        // TODO: CRIAR ENDPOINT DE LOGIN

        [HttpPost]
        [Route("user")]
        /*[ FAZER VALIDAÇÃO ]*/
        public async Task<IHttpActionResult> CreateUser(CreateUserViewModel model)
        {
            var user = await _userBo.CreateUser(model.ToUser());
            //return Created("Cadastrado com sucesso.", new VisualizationUserViewModel(user));
            return Ok(user);
        }
    }
}
