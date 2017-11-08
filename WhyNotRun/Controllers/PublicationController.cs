using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WhyNotRun.BO;

namespace WhyNotRun.Controllers
{
    public class PublicationController : ApiController
    {
        private PublicationBO _publicationBo;

        public PublicationController()
        {
            _publicationBo = new PublicationBO();
        }

        /// <summary>
        /// Listar publicacoes
        /// </summary>
        /// <returns>Lista de publicações</returns>
        [HttpGet]
        [Route("publication/publications")]
        public async Task<IHttpActionResult> ListPublications()
        {
            var resultado = await _publicationBo.ListPublications();
            if (resultado != null)
            {
                return Ok(resultado);
            }
            return NotFound();
        }



    }
}
