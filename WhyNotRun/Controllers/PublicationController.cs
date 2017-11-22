using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using WhyNotRun.BO;
using WhyNotRun.Filters;
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
        [Route("publications")]
        public async Task<IHttpActionResult> ListPublications(int page)
        {
            var resultado = await _publicationBo.ListPublications(page);
            if (resultado != null)
            {
                return Ok(ViewPublicationViewModel.ToList(resultado));
            }
            return NotFound(); //mudar isso
        }
        

        /// <summary>
        /// Cadastrar publicação
        /// </summary>
        /// <param name="model">publicação a ser cadastrada</param>
        /// <returns></returns>
        [HttpPost]
        [Route("publications")]
        [WhyNotRunJwtAuth]
        public async Task<IHttpActionResult> CreatePublication(CreatePublicationViewModel model)
        {
            var resultado = await _publicationBo.CreatePublication(model.ToPublication());
            if (resultado != null)
            {
                return Ok(new ViewPublicationViewModel(resultado));
            }
            return StatusCode((HttpStatusCode)422);
        }

        /// <summary>
        /// Reage a uma publicação
        /// </summary>
        /// <param name="model">dados da publicação reagida</param>
        [HttpPatch]
        [Route("publications/{id}/react")]
        [WhyNotRunJwtAuth]
        public async Task<IHttpActionResult> React(string id, ReactPublicationViewModel model)
        {
            var resultado = await _publicationBo.React(model.UserId.ToObjectId(), id.ToObjectId(), model.Like);
            if (resultado)
            {
                return Ok(new ViewPublicationViewModel(await _publicationBo.SearchPublication(id.ToObjectId())));
            }
            return InternalServerError();
        }

        /// <summary>
        /// Adiciona um comentario a uma publicação
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("comments")]
        [WhyNotRunJwtAuth]
        public async Task<IHttpActionResult> AddComment(AddCommentViewModel model)
        {
            var resultado = await _publicationBo.AddComment(model.ToComment(), model.PublicationId.ToObjectId());
            if (resultado)
            {
                return Ok(new ViewPublicationViewModel(await _publicationBo.SearchPublication(model.PublicationId.ToObjectId())));
            }
            return StatusCode((HttpStatusCode)422);

        }

        [HttpGet]
        [Route("comments")]
        public async Task<IHttpActionResult> SeeMoreComments(string publicationId, string lastcommentId, int limit)
        {
            var result = await _publicationBo.SeeMoreComments(publicationId.ToObjectId(), lastcommentId.ToObjectId(), limit);
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }


       /// <summary>
       /// Busca publicações com base em uma palavra chave
       /// </summary>
       /// <param name="text"></param>
       [HttpGet]
       [Route("publications")]
       public async Task<IHttpActionResult> SearchPublications(string text, int page)
       {
           var result = await _publicationBo.SearchPublications(text, page);
           if (result != null)
           {
               return Ok(ViewPublicationViewModel.ToList(result));
           }
           return NotFound();
       }



    }
}
