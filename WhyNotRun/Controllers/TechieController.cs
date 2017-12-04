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
using WhyNotRun.Models;
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
        public async Task<IHttpActionResult> ListTechies(int page, string order)
        {
            var result = await _techieBo.ListTechie(page, order);

            if (result != null)
            {
                if (order == "name")
                {
                    return Ok(ViewTechieViewModel.ToList(result).OrderBy(a => a.Name));
                }
                else if (order == "posts")
                {
                    return Ok(ViewTechieViewModel.ToList(result).OrderByDescending(a => a.Posts));
                }
                else
                {
                    return Ok(ViewTechieViewModel.ToList(result).OrderBy(a => a.Name));//lembrar de mudar isso para pontos
                }
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
