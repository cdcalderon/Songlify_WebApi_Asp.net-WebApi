using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.WindowsAzure.Mobile.Service;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using SonglifyApi.DataObjects;
using SonglifyApi.Models;
using System.Data.Entity;

namespace SonglifyApi.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.Anonymous)]
    public class MemberController : TableController<Member>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            MobileServiceContext context = new MobileServiceContext();
            DomainManager = new EntityDomainManager<Member>(context, Request, Services);
        }

        // GET tables/Member
        public IQueryable<Member> GetAllMember()
        {
            return Query(); 
        }

        // GET tables/Member/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public IHttpActionResult GetMember(string id)
        {
            MobileServiceContext context = new MobileServiceContext();
            var member = context.Members.Include(m => m.Favorites).FirstOrDefault(m => m.UserName == id);
            return Ok(member);
            // return Lookup(id);
        }

        // PATCH tables/Member/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<Member> PatchMember(string id, Delta<Member> patch)
        {
             return UpdateAsync(id, patch);
        }

        // POST tables/Member
        public async Task<IHttpActionResult> PostMember(Member item)
        {
            //Member current = await InsertAsync(item);
            //return CreatedAtRoute("Tables", new { id = current.Id }, current);

            MobileServiceContext context = new MobileServiceContext();
            var member = context.Members.FirstOrDefault(m => m.UserName == item.UserName);
            if (member == null)
            {
                var guid = "123456";
                item.SessionId = item.UserName + guid;
                Member current = await InsertAsync(item);

                return CreatedAtRoute("Tables", new { id = current.Id }, current);
            }
            else
            {
                return NotFound();
            }
        }

        // DELETE tables/Member/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteMember(string id)
        {
             return DeleteAsync(id);
        }

    }
}





















//////////
/// 
/// 
/// 
/// using System;

//namespace SonglifyApi.Controllers
//{
//    [AuthorizeLevel(AuthorizationLevel.Anonymous)]
//    public class MemberController : TableController<Member>
//    {
//        protected override void Initialize(HttpControllerContext controllerContext)
//        {
//            base.Initialize(controllerContext);
//            MobileServiceContext context = new MobileServiceContext();
//            DomainManager = new EntityDomainManager<Member>(context, Request, Services);
//        }

//        // GET tables/Member
//        public IQueryable<Member> GetAllMember()
//        {
//            return Query();
//        }

//        // GET tables/Member/48D68C86-6EA6-4C25-AA33-223FC9A27959
//        public IHttpActionResult GetMember(string username)
//        {
//            MobileServiceContext context = new MobileServiceContext();
//            var member = context.Members.FirstOrDefault(m => m.UserName == username);

//            if (member != null)
//            {
//                return Ok(member);
//            }
//            else
//            {
//                return NotFound();
//            }
            
//        }

//        // PATCH tables/Member/48D68C86-6EA6-4C25-AA33-223FC9A27959
//        public Task<Member> PatchMember(string id, Delta<Member> patch)
//        {
//            return UpdateAsync(id, patch);
//        }

//        // POST tables/Member
//        public async Task<IHttpActionResult> PostMember(Member item)
//        {
//            MobileServiceContext context = new MobileServiceContext();
//            var member = context.Members.FirstOrDefault(m => m.UserName == item.UserName);
//            if (member != null)
//            {
//                item.SessionId = Guid.NewGuid() + item.UserName;
//                Member current = await InsertAsync(item);

//                return CreatedAtRoute("Tables", new { id = current.Id }, current);
//            }
//            else
//            {
//                return NotFound();
//            }
            
//        }

//        // DELETE tables/Member/48D68C86-6EA6-4C25-AA33-223FC9A27959
//        public Task DeleteMember(string id)
//        {
//            return DeleteAsync(id);
//        }

//    }
//}