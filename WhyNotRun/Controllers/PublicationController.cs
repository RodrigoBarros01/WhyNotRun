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
        [Route("publications")]
        public async Task<IHttpActionResult> ListPublications()
        {
            var resultado = await _publicationBo.ListPublications();
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
        public async Task<IHttpActionResult> CreatePublication(CreatePublicationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Keys.Where(k => ModelState[k].Errors.Count > 0).Select(k => new { propertyName = k, errorMessage = ModelState[k].Errors[0].ErrorMessage });
                return Ok(errors);
            }
            
            return Ok(new ViewPublicationViewModel(await _publicationBo.CreatePublication(model.ToPublication())));
        }

        /// <summary>
        /// Reage a uma publicação
        /// </summary>
        /// <param name="model">dados da publicação reagida</param>
        [HttpPatch]
        [Route("publications/{id}/react")]
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
        [Route("publications/{id}/comment")] // mudar para comments e receber o id no body
        public async Task<IHttpActionResult> AddComment(string id,AddCommentViewModel model)
        {
            var resultado = await _publicationBo.AddComment(model.ToComment(), id.ToObjectId());
            if (resultado)
            {
                return Ok(new ViewPublicationViewModel(await _publicationBo.SearchPublication(id.ToObjectId())));
            }
            return StatusCode((HttpStatusCode)422);

        }

        /// <summary>
        /// Busca publicações com base em uma palavra chave
        /// </summary>
        /// <param name="text"></param>
        [HttpGet]
        [Route("publications/{text}")]
        public async Task<IHttpActionResult> SearchPublications(string text)
        {
            var result = await _publicationBo.SearchPublications(text);
            if (result != null)
            {
                return Ok(ViewPublicationViewModel.ToList(result));
            }
            return NotFound();
        }



    }
}
