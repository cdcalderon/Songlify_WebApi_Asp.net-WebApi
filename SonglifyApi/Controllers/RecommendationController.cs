using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using SonglifyApi.DataObjects;
using SonglifyApi.Models;

namespace SonglifyApi.Controllers
{
    public class RecommendationController : TableController<Recommendation>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Recommendation>(context, Request, Services);
        }

        // GET tables/Recommendation
        [AuthorizeLevel(AuthorizationLevel.Anonymous)]
        public IQueryable<Recommendation> GetAllRecommendation()
        {
            return Query(); 
        }

        // GET tables/Recommendation/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [AuthorizeLevel(AuthorizationLevel.Anonymous)]
        public SingleResult<Recommendation> GetRecommendation(string id)
        {
            return Lookup(id);
        }

        // GET tables/Recommendation/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [HttpGet]
        [Route("tables/RecommendationBySongId/{songId}")]
        [AuthorizeLevel(AuthorizationLevel.Anonymous)]
        public DataObjects.Recommendation GetRecommendationBySongId(string songId)
        {
            using (MobileServiceContext context = new MobileServiceContext())
            {
                return
                context.Recommendations.Where(x => x.SongId == songId)
                    .ToList()
                    .Select(r => new DataObjects.Recommendation
                    {
                        Id = r.Id,
                        Artist = r.Artist,
                        CreatedAt = r.CreatedAt,
                        Deleted = r.Deleted,
                        ImageLarge = r.ImageLarge,
                        ImageMedium = r.ImageMedium,
                        ImageSmall = r.ImageSmall,
                        SongId = r.SongId
                    }).FirstOrDefault();
            };


        }

        // PATCH tables/Recommendation/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [AuthorizeLevel(AuthorizationLevel.Anonymous)]
        public Task<Recommendation> PatchRecommendation(string id, Delta<Recommendation> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Recommendation
        [AuthorizeLevel(AuthorizationLevel.Anonymous)]
        public async Task<IHttpActionResult> PostRecommendation(Recommendation item)
        {
            using (MobileServiceContext context = new MobileServiceContext())
            {
                if (context.Recommendations.FirstOrDefault(r => r.SongId == item.SongId) == null)
                {
                    Recommendation current = await InsertAsync(item);
                    return CreatedAtRoute("Tables", new { id = current.Id }, current);
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // DELETE tables/Recommendation/48D68C86-6EA6-4C25-AA33-223FC9A27959
        [AuthorizeLevel(AuthorizationLevel.Anonymous)]
        public Task DeleteRecommendation(string id)
        {
             return DeleteAsync(id);
        }

    }
}