using ChallengeEntity.Dto.Complete;
using ChallengeEntity.Dto.Delete;
using ChallengeEntity.Dto.Get;
using ChallengeEntity.Dto.Search;
using ChallengeEntity.Dto.Start;

namespace ChallengeBusiness.Abstract
{
    public interface IChallengeService
    {
        StartResponseDto StartChallenge(StartRequestDto startRequestDto);
        GetResponseDto GetTodoItems();
        SearchResponseDto SearchTodoItems(SearchRequestDto searchRequestDto);
        DeleteResponseDto DeleteTodoItem(DeleteRequestDto deleteRequestDto);
        CompleteResponseDto CompleteChallenge(CompleteRequestDto completeRequestDto);
    }
}
