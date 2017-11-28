using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WhyNotRun.BO;
using WhyNotRun.Filters;
using WhyNotRun.Models.TechieViewModel;

namespace WhyNotRun.Controllers
{
    public class TechieController : ApiController
    {
        private TechieBO _techieBo;

        public TechieController()
        {
            _techieBo = new TechieBO();
        }

        [HttpPost]
        [Route("techies")]
        [WhyNotRunJwtAuth]
        public async Task<IHttpActionResult> CreateTechie(CreateTechieViewModel model)
        {
            var result = await _techieBo.CreateTechie(model.ToTechie());
            if (result != null)
            {
                return Ok(result);
            }
            return InternalServerError();
        }

        [HttpGet]
        [Route("techies")]
        public async Task<IHttpActionResult> ListTechies(int page)
        {
            var result = await _techieBo.ListTechie(page);
            if (result != null)
            {
                return Ok(ViewTechieViewModel.ToList(result));
            }

            return NotFound();
        }

        [HttpGet]
        [Route("techies")]
        public async Task<IHttpActionResult> SugestTechie(string text)
        {
            var result = await _techieBo.SugestTechie(text);
            if (result != null)
            {
                return Ok(TechiesViewModel.ToList(result));
            }
            return NotFound();
        }


    }
}
