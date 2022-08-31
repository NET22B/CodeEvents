using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CodeEvents.Api.Data;
using CodeEvents.Api.Data.Repositories;
using CodeEvents.Api.Core.Entities;
using AutoMapper;
using CodeEvents.Api.Core.Dto;

namespace CodeEvents.Api.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class CodeEventsController : ControllerBase
    {
        private readonly CodeEventsApiContext db;
        private readonly IMapper mapper;
        private readonly UnitOfWork uow;

        public CodeEventsController(CodeEventsApiContext context, IMapper mapper)
        {
            db = context;
            this.mapper = mapper;
            uow = new UnitOfWork(db);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeEvent>>> GetCodeEvent(bool includeLectures)
        {
            var events = await uow.CodeEventRepository.GetAsync(includeLectures);
            var dto = mapper.Map<IEnumerable<CodeEventDto>>(events);
            return Ok(dto);
        }

   
    }
}
