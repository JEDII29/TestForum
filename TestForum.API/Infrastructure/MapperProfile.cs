using AutoMapper;
using TestForum.API.Models;
using TestForum.API.Requests;
using TestForum.Data.Entities;

namespace TestForum.API.Infrastructure
{
	public class MapperProfile
	{
		private readonly IMapper _mapper;

		public MapperProfile() 
		{
			var config = new MapperConfiguration(cfg =>
			{ 
				cfg.CreateMap<ArticleEntity, ArticleDTO>();
				cfg.CreateMap<ArticleDTO, ArticleEntity>();
				cfg.CreateMap<UserEntity, UserDTO>();
				cfg.CreateMap<UserDTO, UserEntity>();
				cfg.CreateMap<UserEntity, UserRequest>();
			}) ;

			_mapper = config.CreateMapper();
		}

		#region ArticleMaps
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
		#endregion

		#region UserMaps
		public UserDTO MapToDTO(UserEntity userEntity)
			=> _mapper.Map<UserDTO>(userEntity);
		public IEnumerable<UserDTO> MapToDTO(IQueryable<UserEntity> userEntities)
			=> _mapper.ProjectTo<UserDTO>(userEntities);
		public IEnumerable<UserDTO> MapToDTO(IEnumerable<UserEntity> userEntities)
			=> userEntities.Select(_mapper.Map<UserDTO>);

		public UserEntity MapToEntity(UserDTO userDTO)
			=> _mapper.Map<UserEntity>(userDTO);
		public UserEntity MapToEntity(UserRequest userRequest)
	=> _mapper.Map<UserEntity>(userRequest);
		public IEnumerable<UserEntity> MapToEntity(IQueryable<UserDTO> userDTOs)
			=> _mapper.ProjectTo<UserEntity>(userDTOs);
		public IEnumerable<UserEntity> MapToEntity(IEnumerable<UserDTO> userDTOs)
			=> userDTOs.Select(_mapper.Map<UserEntity>);
		#endregion



		//public IEnumerable<ArticleEntity> MapToEntity(IEnumerable<ArticleDTO> articleDTOs)
		//	=> _mapper.Map<ArticleEntity>(articleDTO);
	}
}
