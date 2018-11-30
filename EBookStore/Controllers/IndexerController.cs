using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EBookStore.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace EBookStore.Controllers
{
    [Route("api/index")]
    [ApiController]
    public class IndexerController : ControllerBase
    {
       
        [AllowAnonymous]
        public IActionResult IndexFile()
        {
            
            return Ok();
        }
    }
}