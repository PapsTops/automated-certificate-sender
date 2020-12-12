using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedCertificateSender.Controllers
{
    [Route("google-auth-callback")]
    [ApiController]
    public class GoogleAuthController : ControllerBase
    {
        [HttpGet]
        public string Get(string code)
        {
            return "tops";
        }
    }
}
