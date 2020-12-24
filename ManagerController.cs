using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TectoreService.Models;

namespace TectoreService.Controllers
{
    public class ManagerController : ApiController
    {
        public IHttpActionResult get()
        {
            TectoraService Ts = new TectoraService();

            List<Manager> lman = Ts.Managers.ToList();
            List<Client> lcli = Ts.Clients.ToList();

            var query = from m in lman
                        join c in lcli on m.ManagerName equals c.ManagerName
                        select new
                        {

                            ManagerName = m.ManagerName,
                            Email = m.Email,
                            Position = m.Position,
                            Clientname = c.ClientName
                        };
            return Ok(query);

        }

        public HttpResponseMessage Post([FromBody]Manager man)
        {
            try
            {
                using (TectoraService Ts = new TectoraService())
                {
                    Ts.Managers.Add(man);
                    Ts.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, man);
                    message.Headers.Location = new Uri(Request.RequestUri + man.MgrId.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        public HttpResponseMessage Delete(int Id)
        {
            try
            {

                 using (TectoraService Ts = new TectoraService())
                {
                    var detail = Ts.Managers.FirstOrDefault(m => m.MgrId == Id);

                    if (detail == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Manager With Id=" + Id.ToString() + "NotFound");
                    }
                    else
                    {
                        Ts.Managers.Remove(detail);
                        Ts.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK);
                    }


                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }

        public HttpResponseMessage Put(int Id, [FromBody]Manager man)
        {
            try
            {

                using (TectoraService Ts = new TectoraService())
                {
                    var detail = Ts.Managers.FirstOrDefault(m => m.MgrId == Id);

                    if (detail == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Manager With Id=" + Id.ToString() + "NotFound");
                    }
                    else
                    {
                        detail.ManagerName = man.ManagerName;
                        detail.Email = man.Email;
                        detail.Position = man.Position;
                        Ts.SaveChanges();

                        return Request.CreateResponse(HttpStatusCode.OK, detail);
                    }
                }
            }

            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}
