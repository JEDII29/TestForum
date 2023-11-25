using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;
using TestForum.API.Abstract;
using TestForum.API.Models;
using TestForum.API.Requests;
using TestForum.API.Services;
using TestForum.Data.Entities;

namespace TestForum.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class VoteController : Controller
	{
		private readonly IVotesService _votesService;
		private readonly IReputationService _reputationService;
		public VoteController(IVotesService votesService, IReputationService reputationService)
		{
			_votesService = votesService;
			_reputationService = reputationService;
		}

		[Authorize(Roles = "user")]
		[HttpPost]
		public async Task<IActionResult> AddNewVote([FromBody] VoteRequest newVote)
		{
			try
			{
				var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
				Guid guid = Guid.NewGuid();
				VoteDTO voteDTO = new VoteDTO(guid, userId, newVote.TargetId, newVote.Type);
				_votesService.AddVote(voteDTO);
				_reputationService.ChangeRepAfterCommentVote(voteDTO.UserId, voteDTO.TargetId, voteDTO.Type);

				return Ok();
			}
			catch (Exception ex)
			{
				return BadRequest(ex);
			}
		}
	}
}
