using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PostService.Models;
using PostService.repository;

namespace Microservices.Controllers
{
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        public readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Post post)
        {
            _postRepository.Create(post);
        }

        [HttpGet("{id}")]
        public Post Get(string id)
        {
            return _postRepository.ReadById(id);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public bool Delete(string id)
        {
            return _postRepository.Delete(id);
        }

        [HttpPut("{id}")]
        public Post Put(string id, Post post)
        {
            return _postRepository.Update(post);
        }
    }
}
