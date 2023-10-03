using AutoMapper;
using TestForum.API.Models;
using TestForum.Data.Entities;
using TestForum.Models;

namespace TestForum.API.Mappers
{
	public class CommentMapper
	{
		private readonly IMapper _mapper;

		public CommentMapper()
		{
			var config = new MapperConfiguration(cfg =>
			{
				cfg.CreateMap<CommentEntity, CommentDTO>();
				cfg.CreateMap<CommentDTO, CommentEntity>();
			});

			_mapper = config.CreateMapper();
		}

		public CommentDTO MapToDTO(CommentEntity CommentEntity)
			=> _mapper.Map<CommentDTO>(CommentEntity);
		public CommentEntity MapToEntity(CommentDTO CommentDTO)
			=> _mapper.Map<CommentEntity>(CommentDTO);
	}
}