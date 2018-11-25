using InMemoryVsSQLiteDemo.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;

namespace InMemoryVsSQLiteDemo.Test
{
    public class ConnectionFactory : IDisposable
    {

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        public BlogDBContext CreateContextForInMemory()
        {
            var option = new DbContextOptionsBuilder<BlogDBContext>().UseInMemoryDatabase(databaseName: ":memory:").Options;

            var context = new BlogDBContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }

        public BlogDBContext CreateContextForSQLite()
        {
            var option = new DbContextOptionsBuilder<BlogDBContext>().UseSqlite("DataSource=" + Path.Combine("E:/Project", "blogging.db")).Options;

            var context = new BlogDBContext(option);
            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
