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

using Microsoft.AspNetCore.JsonPatch;
using CodeEvents.Common.Dto;

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
        
        [HttpGet]
        [Route("{name}")]
        public async Task<ActionResult<IEnumerable<CodeEvent>>> GetCodeEvent(string name, bool includeLectures)
        {

            var codeevent = await uow.CodeEventRepository.GetAsync(name, includeLectures);

            if (codeevent is null) return NotFound();

            var dto = mapper.Map<CodeEventDto>(codeevent);

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<CodeEvent>>> CreateCodeEvent(CodeEventDto dto)
        {

            if(await uow.CodeEventRepository.GetAsync(dto.Name) != null)
            {
                ModelState.AddModelError("Name", "Name is in use");
                return BadRequest(ModelState);
            }

            var codeevent = mapper.Map<CodeEvent>(dto);
            await uow.CodeEventRepository.AddAsync(codeevent);
            await uow.CompleteAsync();


            return CreatedAtAction(nameof(GetCodeEvent), new { name = codeevent.Name }, dto);
           
        }

        [HttpPut("{name}")]
        public async Task<ActionResult<CodeEventDto>> PutEvent(string name, CodeEventDto dto)
        {
            var codeevent = await uow.CodeEventRepository.GetAsync(name);

            if (codeevent is null) return NotFound();
            mapper.Map(dto, codeevent);

            await uow.CompleteAsync();
            
            return Ok(mapper.Map<CodeEventDto>(codeevent));
        }
         
        
        [HttpPatch("{name}")]
        public async Task<ActionResult<CodeEventDto>> PatchEvent(string name, JsonPatchDocument<CodeEventDto> patchDocument)
        {
            var codeevent = await uow.CodeEventRepository.GetAsync(name, true);

            if (codeevent is null) return NotFound();

            var dto = mapper.Map<CodeEventDto>(codeevent);

            patchDocument.ApplyTo(dto, ModelState);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            mapper.Map(dto, codeevent);

            await uow.CompleteAsync();

            return Ok(mapper.Map<CodeEventDto>(codeevent));
        }


    }
}
