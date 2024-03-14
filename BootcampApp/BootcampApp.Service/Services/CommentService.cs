using AutoMapper;
using BootcampApp.Core.IUnitOfWorks;
using BootcampApp.Core.Models;
using BootcampApp.Core.Repositories;
using BootcampApp.Core.Services;

namespace BootcampApp.Service.Services
{
    public class CommentService : Service<Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public CommentService(IGenericRepository<Comment> repository, IUnitOfWork unitOfWork, IMapper mapper, ICommentRepository commentRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
        }
    }
}