using AutoMapper;
using BootcampApp.Core.IUnitOfWorks;
using BootcampApp.Core.Models;
using BootcampApp.Core.Repositories;
using BootcampApp.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootcampApp.Service.Services
{
    public class CategoryService : Service<Post>, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public CategoryService(IGenericRepository<Post> repository, IUnitOfWork unitOfWork, IMapper mapper, IPostRepository postRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }
    }
}
