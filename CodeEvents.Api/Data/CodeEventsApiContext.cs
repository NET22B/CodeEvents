﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventsApi.Core.Entities;

namespace CodeEvents.Api.Data
{
    public class CodeEventsApiContext : DbContext
    {
        public CodeEventsApiContext (DbContextOptions<CodeEventsApiContext> options)
            : base(options)
        {
        }

        public DbSet<EventsApi.Core.Entities.CodeEvent> CodeEvent { get; set; } = default!;
    }
}