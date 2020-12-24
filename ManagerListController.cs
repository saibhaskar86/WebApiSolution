using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TectoreService.Models;

namespace TectoreService.Controllers
{
    public class ManagerListController : ApiController
    {
        public IEnumerable<Manager> Get()
        {

            using (TectoraService Ts = new TectoraService())
            {
                return Ts.Managers.ToList();

            }
        }

    }
}