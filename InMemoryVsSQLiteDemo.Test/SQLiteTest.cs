using InMemoryVsSQLiteDemo.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace InMemoryVsSQLiteDemo.Test
{
    public class SQLiteTest
    {
        [Fact]
        public void Task_Add_Without_Relation()
        {
            //Arrange  
            var factory = new ConnectionFactory();

            //Get the instance of BlogDBContext
            var context = factory.CreateContextForSQLite();

            var post = new Post() { Title = "Test Title 3", Description = "Test Description 3", CreatedDate = DateTime.Now };

            //Act  
            var data = context.Post.Add(post);
            
            //Assert 
            Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            Assert.Empty(context.Post.ToList());
        }

        [Fact]
        public void Task_Add_With_Relation_Return_Exception()
        {
            //Arrange  
            var factory = new ConnectionFactory();

            //Get the instance of BlogDBContext
            var context = factory.CreateContextForSQLite();

            var post = new Post() { Title = "Test Title 3", Description = "Test Description 3", CategoryId = 2, CreatedDate = DateTime.Now };

            //Act  
            var data = context.Post.Add(post);
            
            //Assert 
            Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            Assert.Empty(context.Post.ToList());
        }

        [Fact]
        public void Task_Add_With_Relation_Return_No_Exception()
        {
            //Arrange  
            var factory = new ConnectionFactory();

            //Get the instance of BlogDBContext
            var context = factory.CreateContextForSQLite();
            var post = new Post() { Title = "Test Title 3", Description = "Test Description 3", CategoryId = 2, CreatedDate = DateTime.Now };

            //Act  
            for (int i = 1; i < 4; i++){
                var category = new Category() { Id = i, Name = "Category " + i, Slug = "slug" + i };
                context.Category.Add(category);                
            }
            context.SaveChanges();

            var data = context.Post.Add(post);
            context.SaveChanges();

            //Assert           

            //Get the post count
            var postCount = context.Post.Count();
            if (postCount != 0)
            {
                Assert.Equal(1, postCount);
            }

            //Get single post detail
            var singlePost = context.Post.FirstOrDefault();
            if (singlePost != null)
            {
                Assert.Equal("Test Title 3", singlePost.Title);
            }
        }

        [Fact]
        public void Task_Add_Time_Test()
        {
            //Arrange  
            var factory = new ConnectionFactory();

            //Get the instance of BlogDBContext
            var context = factory.CreateContextForInMemory();

            //Act 
            for (int i = 1; i < 4; i++)
            {
                var category = new Category() { Id = i, Name = "Category " + i, Slug = "slug" + i };
                context.Category.Add(category);                
            }

            context.SaveChanges();

            for (int i = 1; i <= 1000; i++)
            {
                var post = new Post() { Title = "Test Title " + i, Description = "Test Description " + i, CategoryId = 2, CreatedDate = DateTime.Now };
                context.Post.Add(post);                
            }
            
            context.SaveChanges();

            //Assert  
            //Get the post count
            var postCount = context.Post.Count();
            if (postCount != 0)
            {
                Assert.Equal(1000, postCount);
            }

            //Get single post detail
            var singlePost = context.Post.Where(x => x.PostId == 1).FirstOrDefault();
            if (singlePost != null)
            {
                Assert.Equal("Test Title 1", singlePost.Title);
            }
        }
    }
}
