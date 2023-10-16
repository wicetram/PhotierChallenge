using ChallengeBusiness.Abstract;
using ChallengeEntity.Dto.Complete;
using ChallengeEntity.Dto.Delete;
using ChallengeEntity.Dto.Search;
using ChallengeEntity.Dto.Start;
using Microsoft.AspNetCore.Mvc;

namespace ChallangeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChallengeController : ControllerBase
    {
        private readonly IChallengeService _challengeService;

        public ChallengeController(IChallengeService challengeService)
        {
            _challengeService = challengeService;
        }

        [HttpPost]
        [Route("startChallenge")]
        public object StartChallenge(StartRequestDto startRequestDto)
        {
            return _challengeService.StartChallenge(startRequestDto);
        }

        [HttpGet]
        [Route("getChallenge")]
        public object GetChallenge()
        {
            return _challengeService.GetTodoItems();
        }

        [HttpGet]
        [Route("searchChallenge")]
        public object SearchChallenge(SearchRequestDto searchRequestDto)
        {
            return _challengeService.SearchTodoItems(searchRequestDto);
        }

        [HttpDelete]
        [Route("deleteChallenge")]
        public object DeleteChallenge(DeleteRequestDto deleteRequestDto)
        {
            return _challengeService.DeleteTodoItem(deleteRequestDto);
        }

        [HttpPost]
        [Route("completeChallenge")]
        public object CompleteChallenge(CompleteRequestDto completeRequestDto)
        {
            return _challengeService.CompleteChallenge(completeRequestDto);
        }
    }
}
