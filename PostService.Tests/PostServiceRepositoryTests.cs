using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PostService.Models;
using PostService.repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace PostService.Tests
{
    [TestClass]
    public class PostRepositoryTests
    {
        private readonly PostRepository _postRepository;

        public PostRepositoryTests()
        {

            _postRepository = new PostRepository(TestHelper.GetIConfigurationRoot(AppContext.BaseDirectory));
        }

        [TestMethod]
        public void Create_Post_Return_Post()
        {
            var id = Guid.NewGuid().ToString();
            var model = _postRepository.Create(new Post() { Id = id, Title = "Something", Content = "Something", CreatedDate = DateTime.Today });
            Assert.IsTrue(model.Id == id);
        }

        [TestMethod]
        public void PostRepository_GetRandom_Returns_PostWithIdRandom()
        {
            var id = Guid.NewGuid().ToString();
            _postRepository.Create(new Post() { Id = id });
            var post = _postRepository.ReadById(id);
            Assert.IsNotNull(post);
            Assert.IsTrue(post.Id == id);
        }

        [TestMethod]
        public void PostRepository_UpdateTitle_Returns_PostWithChangedTitle()
        {
            var id = Guid.NewGuid().ToString();
            _postRepository.Create(new Post() { Id = id, Title = "Not Updated" });
            var post = _postRepository.ReadById(id);
            post.Title = "Updated";
            var updated = _postRepository.Update(post);
            Assert.IsNotNull(post);
            Assert.IsTrue(updated.Title == "Updated");
        }

        [TestMethod]
        public void PostRepository_UpdateTitle_Returns_PostWithChangedContent()
        {
            var id = Guid.NewGuid().ToString();
            _postRepository.Create(new Post() { Id = id, Content = "Not Updated" });
            var post = _postRepository.ReadById(id);
            post.Content = "Updated";

            var updated = _postRepository.Update(post);
            Assert.IsNotNull(post);
            Assert.IsTrue(updated.Content == "Updated");
        }

        [TestMethod]
        public void PostRepository_Delete_Returns_DeletePost()
        {
            var id = Guid.NewGuid().ToString();
            _postRepository.Create(new Post() { Id = id, Content = "Not Updated" });
            var isDeleted = _postRepository.Delete(id);
            var post = _postRepository.ReadById(id);

            Assert.IsNull(post);
            Assert.IsTrue(isDeleted);

        }
    }
}
