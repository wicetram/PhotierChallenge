using ChallangeWebApi.Business;
using ChallengeEntity.Dto.Complete;
using ChallengeEntity.Dto.Delete;
using ChallengeEntity.Dto.Search;
using ChallengeEntity.Dto.Start;
using Newtonsoft.Json;

ChallengeManager challengeManager = new();

Console.Write("Enter your email address: ");
string email = Console.ReadLine() ?? "";

var firstRequest = new StartRequestDto { Email = email };

var first = challengeManager.StartChallenge(firstRequest);

Console.WriteLine(first);

var second = challengeManager.GetTodoItems();

Console.WriteLine("Second Response:");
Console.WriteLine(JsonConvert.SerializeObject(second, Formatting.Indented));


Console.Write("Enter first code: (hello) ");
string firstCode = Console.ReadLine() ?? "";

var secondRequest = new SearchRequestDto { Message = firstCode };

var third = challengeManager.SearchTodoItems(secondRequest);

Console.WriteLine("Third Response:");
Console.WriteLine(JsonConvert.SerializeObject(third, Formatting.Indented));

Console.Write("Enter second code: (42) ");
string secondCode = Console.ReadLine() ?? "";

var thirdRequest = new DeleteRequestDto { Message = secondCode };

var fourth = challengeManager.DeleteTodoItem(thirdRequest);

Console.WriteLine("Fourth Response:");
Console.WriteLine(JsonConvert.SerializeObject(fourth, Formatting.Indented));

Console.Write("Enter the last code: (menemen) ");
string lastCode = Console.ReadLine() ?? "";

Console.Write("Enter the zip location: ");
string filePath = Console.ReadLine() ?? "";

var lastRequest = new CompleteRequestDto { Code = lastCode, FilePath = filePath };
var fifth = challengeManager.CompleteChallenge(lastRequest);

Console.WriteLine("Fifth Response:");
Console.WriteLine(JsonConvert.SerializeObject(fifth, Formatting.Indented));

Console.ReadKey();