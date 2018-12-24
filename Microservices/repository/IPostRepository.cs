using Common.Repository;
using PostService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostService.repository
{
    public interface IPostRepository : IWriteRepository<Models.Post>
    {

    }
}
