using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GetIT.Controllers
{
    [Route("api/[Controller]")]
    public class PController : Controller
    {
        private readonly ILogger<PController> _logger;


        public PController(ILogger<PController> logger)
        {
            this._logger = logger;
        }

        public IEnumerable<string> GetProducts()
        {
            List<string> listOfProducts = new List<string> { "sdfsdf", "dsfsdfsdf" };
            return listOfProducts;
        }


    }
}
