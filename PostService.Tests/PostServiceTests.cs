using Microservices.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PostService.Models;
using PostService.repository;
using System;

namespace PostService.Tests
{
    [TestClass]
    public class PostServiceTests
    {
        private readonly PostsController postsController;
        public PostServiceTests()
        {
            var repoMock = new Mock<IPostRepository>();
            repoMock.Setup(p => p.Create(It.IsAny<Post>())).Returns(new Post() { Id = "1" });
            repoMock.Setup(p => p.ReadById(It.IsAny<string>())).Returns(new Post() { Id = "1", Title = "1" });
            repoMock.Setup(p => p.Update(It.IsAny<Post>())).Returns(new Post() { Id = "1", Title = "1" });
            repoMock.Setup(p => p.Delete(It.IsAny<string>())).Returns(true);

            postsController = new PostsController(repoMock.Object);
        }
        [TestMethod]
        public void Create_Post_Return_Post()
        {
            postsController.Post(new Post());
        }

        [TestMethod]
        public void PostController_Get1_Returns_Post()
        {
            var id = Guid.NewGuid().ToString();

            var post = postsController.Get(id);

            Assert.IsTrue(post.Id == "1");
        }

        [TestMethod]
        public void Put_Post_Updates_Post()
        {
            var id = Guid.NewGuid().ToString();
            var post = postsController.Put(id,new Post() { Id = id, Title = "Not Updated" });
            Assert.IsTrue(post.Title == "1");
        }

        [TestMethod]
        public void Delete_Post_Deletes_Post()
        {
            var id = Guid.NewGuid().ToString();
            var isDeleted = postsController.Delete(id);
            Assert.IsTrue(isDeleted);
        }
    }
}
