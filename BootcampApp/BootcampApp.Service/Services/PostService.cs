﻿using AutoMapper;
using BootcampApp.Core.IUnitOfWorks;
using BootcampApp.Core.Models;
using BootcampApp.Core.Repositories;
using BootcampApp.Core.Services;

namespace BootcampApp.Service.Services
{
    public class PostService : Service<Post>, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IGenericRepository<Post> repository, IUnitOfWork unitOfWork, IMapper mapper, IPostRepository postRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _postRepository = postRepository;
        }
    }
}
