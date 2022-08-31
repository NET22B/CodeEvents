﻿using AutoMapper;
using CodeEvents.Api.Core.Dto;
using CodeEvents.Api.Data;
using CodeEvents.Api.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeEvents.Api.Controllers
{
    [Route("api/events/{name}/lectures")]
    [ApiController]
    public class LecturesController : ControllerBase
    {
        private readonly CodeEventsApiContext db;
        private readonly IMapper mapper;
        private readonly UnitOfWork uow;

        public LecturesController(CodeEventsApiContext context, IMapper mapper)
        {
            db = context;
            this.mapper = mapper;
            uow = new UnitOfWork(db);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LectureDto>>> GetLectures(string name)
        {
            if ((await uow.CodeEventRepository.GetAsync(name)) is null) 
                return NotFound(new { Error = new[] { $"CodeEvent with name: [{name}] not found" } });

            var lectures = await uow.LectureRepository.GetLecturesForEvent(name);
            return Ok(mapper.Map<IEnumerable<LectureDto>>(lectures));
        }
    }
}