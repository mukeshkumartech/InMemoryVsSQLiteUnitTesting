using System;
using Microsoft.EntityFrameworkCore;

namespace InMemoryVsSQLiteDemo.Model
{
    public partial class BlogDBContext : DbContext
    {
        public BlogDBContext(){}

        public BlogDBContext(DbContextOptions<BlogDBContext> options)
            : base(options){}

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Post> Post { get; set; }
    }
}
