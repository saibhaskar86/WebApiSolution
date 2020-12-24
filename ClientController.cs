using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using TectoreService.Models;

namespace TectoreService.Controllers
{
    public class ClientController : ApiController
    {
        public IEnumerable<Client> Get()
        {
            using (TectoraService Ts = new TectoraService())
            {
                return Ts.Clients.ToList();
            }
        }

        public HttpResponseMessage Post([FromBody]Client  cs)
        {
            try
            {
                using (TectoraService Ts = new TectoraService())
                {
                    Ts.Clients.Add(cs);
                    Ts.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, cs);
                    message.Headers.Location = new Uri(Request.RequestUri + cs.ClientId.ToString());
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
                    var detail = Ts.Clients.FirstOrDefault(c => c.ClientId == Id);

                    if (detail == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Client With Id=" + Id.ToString() + "NotFound");
                    }
                    else 
                    {
                        Ts.Clients.Remove(detail);
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


        public HttpResponseMessage Put(int Id, [FromBody]Client client)
        {
            try
            {

                using (TectoraService Ts = new TectoraService())
                {
                    var detail = Ts.Clients.FirstOrDefault(c=>c.ClientId == Id);

                    if (detail == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Manager With Id=" + Id.ToString() + "NotFound");
                    }
                    else
                    {
                        detail.ClientName = client.ClientName;
                        detail.ClientMail = client.ClientMail;
                      detail.ManagerName = client.ManagerName;
                       
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
