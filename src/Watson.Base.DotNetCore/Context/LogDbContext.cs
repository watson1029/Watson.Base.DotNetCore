using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Watson.Base.DotNetCore.Context
{
    internal class LogDbContext : DbContext
    {
        internal LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {

        }

        internal DbSet<DataBase.SysLog> SysLog { get; set; }
    }
}
