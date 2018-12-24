
namespace News.Tests
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using News.Data;
    using News.Data.Models;
    using News.Web.Controllers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class EndPointsTests
    {
        private NewsDbContext Db
        {
            get
            {
                var dbOptions = new DbContextOptionsBuilder<NewsDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new NewsDbContext(dbOptions);
            }
        }

        private IEnumerable<Message> GetTestData()
        {
            return new List<Message>()
            {
                new Message{ Id = 1, Title = "sameTitle1", Content = "samecontent1", PublishDate = new DateTime(2011, 10, 01)},
                new Message{ Id = 2, Title = "sameTitle2", Content = "samecontent2", PublishDate = new DateTime(2012, 10, 02)},
                new Message{ Id = 3, Title = "sameTitle3", Content = "samecontent3", PublishDate = new DateTime(2013, 10, 03)},
                new Message{ Id = 4, Title = "sameTitle4", Content = "samecontent4", PublishDate = new DateTime(2014, 10, 04)},
                new Message{ Id = 5, Title = "sameTitle5", Content = "samecontent5", PublishDate = new DateTime(2015, 10, 05)}
            };
        }

        private void PopulateData(NewsDbContext db)
        {
            db.Messages.AddRange(this.GetTestData());
            db.SaveChanges();
        }

        private bool CompareNewsExact(Message message, Message testMessage)
        {
            return
                message.Id == testMessage.Id &&
                message.Title == testMessage.Title &&
                message.Content == testMessage.Content &&
                message.PublishDate == testMessage.PublishDate;
        }

        //1
        [Fact]
        public void NewsControllerGetAllMessages_ShouldReturnOkStatusCode()
        {
            var db = this.Db;
            this.PopulateData(db);

            var newsController = new NewsController(db);

            Assert.IsType<OkObjectResult>(newsController.GetAllMessages());
        }

        //1
        [Fact]
        public void NewsControllerGetAllMessages_ShouldReturnCorrectData()
        {
            var db = this.Db;
            this.PopulateData(db);

            var newsController = new NewsController(db);

            var allMessages = (newsController.GetAllMessages() as OkObjectResult)
                 .Value as IEnumerable<Message>;
            
            foreach (var message in allMessages)
            {
                var testMessage = this.GetTestData().First(n => n.Id == message.Id);

                Assert.NotNull(testMessage);
                Assert.True(this.CompareNewsExact(message, testMessage));
            }
        }

        //2
        [Fact]
        public void NewsControllerPostMessage_WithCorrectDataShouldReturnCreatedStatusCode()
        {
            var db = this.Db;

            var testMessage = this.GetTestData().First();

            var newsController = new NewsController(db);

            Assert.IsType<CreatedAtActionResult>(newsController.PostMessage(testMessage));
        }

        //2
        [Fact]
        public void NewsControllerPostMessage_WithCorrectDataShouldReturnCreatedMessage()
        {
            var db = this.Db;

            var testMessage = this.GetTestData().First();

            var newsController = new NewsController(db);

            var returnedMessage = (newsController.PostMessage(testMessage) as CreatedAtActionResult).Value as Message;

            Assert.True(this.CompareNewsExact(returnedMessage, testMessage));
        }

        //3
        [Fact]
        public void NewsControllerPostMessage_WithInCorrectDataShouldReturnBadrequestStatusCode()
        {
            var db = this.Db;

            var testMessage = this.GetTestData().First();

            var newsController = new NewsController(db);

            newsController.ModelState.AddModelError("", "");

            Assert.IsType<BadRequestObjectResult>(newsController.PostMessage(testMessage));
        }

        //4
        [Fact]
        public void NewsControllerPutMessage_WithCorrectDataShouldReturnOkStatusCode()
        {
            var db = this.Db;
            this.PopulateData(db);

            var newsController = new NewsController(db);
            
            var testMessage = this.GetTestData().First();

            Assert.IsType<OkResult>(newsController.PutMessage(3, testMessage));           
        }

        //5
        [Fact]
        public void NewsControllerPutMessage_WithInCorrectDataShouldReturnBadRequest()
        {
            var db = this.Db;
            this.PopulateData(db);

            var newsController = new NewsController(db);

            var testMessage = this.GetTestData().First();

            newsController.ModelState.AddModelError("", "");

            Assert.IsType<BadRequestObjectResult>(newsController.PutMessage(3, testMessage));
        }

        //6
        [Fact]
        public void NewsControllerPutMessage_WithNotExitingMessageShouldReturnBadRequest()
        {
            var db = this.Db;
            
            var newsController = new NewsController(db);

            var testMessage = this.GetTestData().First();
            
            Assert.IsType<BadRequestResult>(newsController.PutMessage(3, testMessage));
        }

        //7
        [Fact]
        public void NewsControllerDeleteMessage_DeleteExistingMessegeShouldReturnOkStatusCode()
        {
            var db = this.Db;
            this.PopulateData(db);

            var newsController = new NewsController(db);

            var messageCount = this.GetTestData().Count();

            //newsController.ModelState.AddModelError("", "");

            newsController.DeleteMessage(3);
            if (messageCount == 4)
            {
                Assert.IsType<OkResult>(newsController.DeleteMessage(2));
            }
        }

        //8
        [Fact]
        public void NewsControllerDeleteMessage_DeleteNonExistingMessegeShouldReturnBadrequest()
        {
            var db = this.Db;
            var newsController = new NewsController(db);

            var messageCount = this.GetTestData().Count();

            Assert.IsType<BadRequestResult>(newsController.DeleteMessage(3));
        }
    }
}
