using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Services.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FunDoNoteContext : DbContext
    {
        public FunDoNoteContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Note> Note { get; set; }
    }
}
