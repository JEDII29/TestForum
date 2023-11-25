﻿using AutoMapper;
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

				cfg.CreateMap<VoteEntity, VoteDTO>();
				cfg.CreateMap<VoteDTO, VoteEntity>();
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

		#region VoteMaps

		public VoteDTO MapToDTO(VoteEntity voteEntity)
			=> _mapper.Map<VoteDTO>(voteEntity);
		public IEnumerable<VoteDTO> MapToDTO(IQueryable<VoteEntity> voteEntities)
			=> _mapper.ProjectTo<VoteDTO>(voteEntities);
		public IEnumerable<VoteDTO> MapToDTO(IEnumerable<VoteEntity> voteEntities)
			=> voteEntities.Select(_mapper.Map<VoteDTO>);

		public VoteEntity MapToEntity(VoteDTO voteDTO)
			=> _mapper.Map<VoteEntity>(voteDTO);
		public IEnumerable<VoteEntity> MapToEntity(IQueryable<VoteDTO> voteDTOs)
			=> _mapper.ProjectTo<VoteEntity>(voteDTOs);
		public IEnumerable<VoteEntity> MapToEntity(IEnumerable<VoteDTO> voteDTOs)
			=> voteDTOs.Select(_mapper.Map<VoteEntity>);
		#endregion

		//public IEnumerable<ArticleEntity> MapToEntity(IEnumerable<ArticleDTO> articleDTOs)
		//	=> _mapper.Map<ArticleEntity>(articleDTO);
	}
}
