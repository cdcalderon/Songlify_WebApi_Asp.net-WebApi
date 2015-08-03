using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.Mobile.Service.Security;
using SonglifyApi.Models;
using System.Data.Entity;

namespace SonglifyApi.Controllers
{
    [AuthorizeLevel(AuthorizationLevel.Anonymous)]
    public class FavoriteController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string sessionId)
        {
            try
            {
                if (string.IsNullOrEmpty(sessionId))
                {
                    return BadRequest();
                }
                MobileServiceContext context = new MobileServiceContext();
                var member = context.Members.Include(m => m.Favorites).FirstOrDefault(m => m.SessionId == sessionId);
                if (member != null)
                {
                    return Ok(member.Favorites);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        //[Route("api/favorite/{id}")]
        [HttpPut]
        public IHttpActionResult Put(string sessionId, string songId)
        {
            try
            {
                if (string.IsNullOrEmpty(sessionId) || string.IsNullOrEmpty(songId))
                {
                    return BadRequest();
                }
                MobileServiceContext context = new MobileServiceContext();
                var song = context.Recommendations.FirstOrDefault(r => r.SongId == songId);
                var member = context.Members.FirstOrDefault(m => m.SessionId == sessionId);

                if (song != null && member != null)
                {
                    member.Favorites.Add(song);
                    var status = context.SaveChanges();
                    if (status > 0)
                    {
                        return Ok(true);
                    }
                    return BadRequest();
                }
                return NotFound();

            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(string songId, string sessionId)
        {
            try
            {
                MobileServiceContext context = new MobileServiceContext();
                var member = context.Members.FirstOrDefault(m => m.SessionId == sessionId);
                if (member != null)
                {
                    var song = context.Recommendations.FirstOrDefault(s => s.SongId == songId);
                    member.Favorites.Remove(song);
                    context.SaveChanges();
                    return Ok(song);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
