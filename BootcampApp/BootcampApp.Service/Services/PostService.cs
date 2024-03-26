using AutoMapper;
using BootcampApp.Core.IUnitOfWorks;
using BootcampApp.Core.Models;
using BootcampApp.Core.Repositories;
using BootcampApp.Core.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        public async Task EditPostAsync(Post post, int[] categoryIds)
        {
            var entity=await _postRepository.GetByIdAsync(post.PostId);
            if (entity != null)
            {
               
                if (!categoryIds.Any())
                {
                    await _postRepository.EditPostAsync(entity);
                }
                else
                {
                    await _postRepository.EditPostAsync(entity, categoryIds);
                }
            }
           
            
   
        }
    }
}
