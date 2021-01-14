using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManager.Model;

namespace TaskManager
{
    public class TaskManagerContext : DbContext
    {
        public TaskManagerContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Note.db");
            base.OnConfiguring(optionsBuilder);
        }

        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<MetaData> MetaDatas { get; set; }
    }
}