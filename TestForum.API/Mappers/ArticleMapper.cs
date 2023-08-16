using AutoMapper;
using TestForum.API.Models;
using TestForum.Data.Entities;

namespace TestForum.API.Mappers
{
	public class ArticleMapper
	{
		private readonly IMapper _mapper;

		public ArticleMapper() 
		{
			var config = new MapperConfiguration(cfg =>
			{ cfg.CreateMap<ArticleEntity, ArticleDTO>();
				cfg.CreateMap<ArticleDTO, ArticleEntity>();
			}) ;

			_mapper = config.CreateMapper();
		}

		public ArticleDTO MapToDTO(ArticleEntity articleEntity)
			=> _mapper.Map<ArticleDTO>(articleEntity);
		public IEnumerable<ArticleDTO> MapToDTO(IQueryable<ArticleEntity> articleEntities)
			=> _mapper.ProjectTo<ArticleDTO>(articleEntities);
		public IEnumerable<ArticleDTO> MapToDTO(IEnumerable<ArticleEntity> articleEntities)
			=> articleEntities.Select(_mapper.Map<ArticleDTO>);

		public ArticleEntity MapToEntity(ArticleDTO articleDTO)
			=> _mapper.Map<ArticleEntity>(articleDTO);
		public IEnumerable<ArticleEntity> MapToEntity(IQueryable<ArticleDTO> articleDTOs)
			=> _mapper.ProjectTo<ArticleEntity>(articleDTOs);
		public IEnumerable<ArticleEntity> MapToEntity(IEnumerable<ArticleDTO> articleDTOs)
			=> articleDTOs.Select(_mapper.Map<ArticleEntity>);


		//public IEnumerable<ArticleEntity> MapToEntity(IEnumerable<ArticleDTO> articleDTOs)
		//	=> _mapper.Map<ArticleEntity>(articleDTO);
	}
}
