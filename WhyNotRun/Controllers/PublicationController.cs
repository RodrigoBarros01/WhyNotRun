using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WhyNotRun.BO;
using WhyNotRun.Models.CommentViewModel;
using WhyNotRun.Models.PublicationViewModel;

namespace WhyNotRun.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
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
                var vpvm = new ViewPublicationViewModel();
                return Ok(vpvm.ToList(resultado));
            }
            return NotFound();
        }

        /// <summary>
        /// Cadastrar publicação
        /// </summary>
        /// <param name="model">publicação a ser cadastrada</param>
        /// <returns></returns>
        [HttpPost]
        [Route("publication")]
        public async Task<IHttpActionResult> CreatePublication(CreatePublicationViewModel model)
        {
            var resultado = await _publicationBo.CreatePublication(model.ToPublication());
            if (resultado != null)
            {
                return Ok(new ViewPublicationViewModel(resultado));
            }
            return InternalServerError();
        }

        /// <summary>
        /// Reage a uma publicação
        /// </summary>
        /// <param name="model">dados da publicação reagida</param>
        [HttpPatch]
        [Route("publication/react")]
        public async Task<IHttpActionResult> React(ReactPublicationViewModel model)
        {
            var resultado = await _publicationBo.React(model.UserId.ToObjectId(), model.PublicationId.ToObjectId(), model.Like);
            if (resultado)
            {
                return Ok(await _publicationBo.SearchPublication(model.PublicationId.ToObjectId()));
            }
            return InternalServerError();
        } 

        [HttpPatch]
        [Route("publication/comment")]
        public async Task<IHttpActionResult> AddComment(AddCommentViewModel model)
        {
            var resultado = await _publicationBo.AddComment(model.ToComment(), model.PublicationId.ToObjectId());
            if (resultado)
            {
                return Ok(await _publicationBo.SearchPublication(model.PublicationId.ToObjectId()));
            }
            return InternalServerError();

        }




    }
}
