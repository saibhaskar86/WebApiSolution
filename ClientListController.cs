using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TectoreService.Models;

namespace TectoreService.Controllers
{
    public class ClientListController : ApiController
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
                             Clientname = c.ClientName,
                             Email=c.ClientMail,
                            ManagerName = m.ManagerName,
                            Position = m.Position,
                           
                        };
            return Ok(query);

        }    

    }
}
