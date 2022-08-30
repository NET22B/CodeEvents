using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeEvents.Api.Data;
using EventsApi.Core.Entities;

namespace CodeEvents.Api.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class CodeEventsController : ControllerBase
    {
        private readonly CodeEventsApiContext _context;

        public CodeEventsController(CodeEventsApiContext context)
        {
            _context = context;
        }

        // GET: api/CodeEvents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeEvent>>> GetCodeEvent()
        {
           return null;
        }

   
    }
}
