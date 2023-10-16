using ChallengeBusiness.Abstract;
using ChallengeBusiness.Const;
using ChallengeBusiness.Util;
using ChallengeEntity.Dto;
using ChallengeEntity.Dto.Complete;
using ChallengeEntity.Dto.Delete;
using ChallengeEntity.Dto.Get;
using ChallengeEntity.Dto.Search;
using ChallengeEntity.Dto.Start;
using Newtonsoft.Json;
using RestSharp;

namespace ChallangeWebApi.Business
{
    public class ChallengeManager : IChallengeService
    {
        private readonly string BaseUrl = PhotierConst.BaseUrl;
        private readonly string Token = PhotierConst.Token;

        /// <summary>
        /// This method initiates a challenge by sending a POST request to the "start" endpoint. It includes an email parameter in the request. 
        /// It then processes the response and returns a StartResponseDto object with the result of the challenge, including a message if successful. 
        /// If there are errors, it returns a failure result or handles exceptions.
        /// </summary>
        /// <param name="startRequestDto"></param>
        /// <returns></returns>
        public StartResponseDto StartChallenge(StartRequestDto startRequestDto)
        {
            var processResult = new StartResponseDto();
            try
            {
                var client = new RestClient($"{BaseUrl}/start");
                var request = new RestRequest
                {
                    Method = Method.Post,
                    AlwaysMultipartFormData = true
                };
                request.AddParameter("email", startRequestDto.Email);
                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        var result = JsonConvert.DeserializeObject<StartResponseDto>(response.Content);
                        if (result != null)
                        {
                            if (!string.IsNullOrEmpty(result.Message))
                            {
                                result.Result = ProcessResultHandler.SuccessHandler();
                                return result;
                            }
                        }
                    }
                    else
                    {
                        processResult.Result = ProcessResultHandler.FailureHandler("Response content is empty or couldn't be deserialized.", response.ResponseStatus.ToString());
                    }
                }
                else
                {
                    processResult.Result = ProcessResultHandler.FailureHandler(response.StatusCode.ToString() ?? "Request was not successful.", response.ResponseStatus.ToString());
                }
            }
            catch (Exception ex)
            {
                processResult.Result = ProcessResultHandler.ExceptionHandler(ex.Message);
            }
            return processResult;
        }

        /// <summary>
        /// This method retrieves a list of to-do items by sending a GET request to the "todos" endpoint with an authorization header containing a token. 
        /// It deserializes the response into a list of GetResponseData objects and returns a GetResponseDto containing the data if the first item is not completed. 
        /// It handles various error cases and exceptions.
        /// </summary>
        /// <returns></returns>
        public GetResponseDto GetTodoItems()
        {
            var processResult = new GetResponseDto();
            try
            {
                var request = new RestRequest { Method = Method.Get };
                request.AddHeader("Authorization", $"Bearer {Token}");
                var client = new RestClient($"{BaseUrl}/todos");
                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        var result = JsonConvert.DeserializeObject<List<GenericResponseDto>>(response.Content);
                        if (result != null)
                        {
                            if (!result[0].Completed)
                            {
                                processResult.Data = result;
                                processResult.Result = ProcessResultHandler.SuccessHandler();
                                return processResult;
                            }
                        }
                        else
                        {
                            processResult.Result = ProcessResultHandler.FailureHandler("Response content is empty or couldn't be deserialized.", response.ResponseStatus.ToString());
                        }
                    }
                    else
                    {
                        processResult.Result = ProcessResultHandler.FailureHandler(response.StatusCode.ToString() ?? "Request was not successful.", response.ResponseStatus.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                processResult.Result = ProcessResultHandler.ExceptionHandler(ex.Message);
            }
            return processResult;
        }

        /// <summary>
        /// This method searches for to-do items that match a search query by sending a GET request to the "todos" endpoint with a query parameter based on the provided message. 
        /// It also includes an authorization header with a token. 
        /// It processes the response and returns a SearchResponseDto containing the matching data if the first item is not completed. 
        /// It handles errors and exceptions.
        /// </summary>
        /// <param name="searchRequestDto"></param>
        /// <returns></returns>
        public SearchResponseDto SearchTodoItems(SearchRequestDto searchRequestDto)
        {
            var processResult = new SearchResponseDto();
            try
            {
                var request = new RestRequest { Method = Method.Get };
                request.AddHeader("Authorization", $"Bearer {Token}");
                var client = new RestClient($"{BaseUrl}/todos?query={searchRequestDto.Message}");
                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        var result = JsonConvert.DeserializeObject<List<GenericResponseDto>>(response.Content);
                        if (result != null)
                        {
                            if (!result[0].Completed)
                            {
                                processResult.Data = result;
                                processResult.Result = ProcessResultHandler.SuccessHandler();
                                return processResult;
                            }
                        }
                    }
                    else
                    {
                        processResult.Result = ProcessResultHandler.FailureHandler("Response content is empty or couldn't be deserialized.", response.ResponseStatus.ToString());
                    }
                }
                else
                {
                    processResult.Result = ProcessResultHandler.FailureHandler(response.StatusCode.ToString() ?? "Request was not successful.", response.ResponseStatus.ToString());
                }
            }
            catch (Exception ex)
            {
                processResult.Result = ProcessResultHandler.ExceptionHandler(ex.Message);
            }
            return processResult;
        }

        /// <summary>
        /// This method deletes a to-do item by sending a DELETE request to the "todos" endpoint with an ID parameter based on the provided message and an authorization header with a token.
        /// It deserializes the response into a DeleteResponseDto and returns it if successful. 
        /// It handles errors and exceptions as well.
        /// </summary>
        /// <param name="deleteRequestDto"></param>
        /// <returns></returns>
        public DeleteResponseDto DeleteTodoItem(DeleteRequestDto deleteRequestDto)
        {
            var processResult = new DeleteResponseDto();
            try
            {
                var request = new RestRequest { Method = Method.Delete };
                request.AddHeader("Authorization", $"Bearer {Token}");
                var client = new RestClient($"{BaseUrl}/todos?id={deleteRequestDto.Message}");
                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        var result = JsonConvert.DeserializeObject<List<GenericResponseDto>>(response.Content);
                        if (result != null)
                        {
                            if (!result[0].Completed)
                            {
                                processResult.Data = result;
                                processResult.Result = ProcessResultHandler.SuccessHandler();
                                return processResult;
                            }
                        }
                    }
                    else
                    {
                        processResult.Result = ProcessResultHandler.FailureHandler("Response content is empty or couldn't be deserialized.", response.ResponseStatus.ToString());
                    }
                }
                else
                {
                    processResult.Result = ProcessResultHandler.FailureHandler(response.StatusCode.ToString() ?? "Request was not successful.", response.ResponseStatus.ToString());
                }
            }
            catch (Exception ex)
            {
                processResult.Result = ProcessResultHandler.ExceptionHandler(ex.Message);
            }
            return processResult;
        }

        /// <summary>
        /// This method completes a challenge by sending a POST request to the "complete" endpoint. 
        /// It includes a code parameter and a zipped file in the request. 
        /// The code is sent as a string, and the zipped file is attached as an application/zip file. 
        /// The method processes the response and returns a CompleteResponseDto with the result of the challenge. 
        /// It handles errors and exceptions.
        /// </summary>
        /// <param name="completeRequestDto"></param>
        /// <returns></returns>
        public CompleteResponseDto CompleteChallenge(CompleteRequestDto completeRequestDto)
        {
            var processResult = new CompleteResponseDto();
            try
            {
                var options = new RestClientOptions(BaseUrl)
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/complete")
                {
                    Method = Method.Post,
                    AlwaysMultipartFormData = true
                };
                request.AddHeader("Authorization", $"Bearer {Token}");
                request.AddParameter("CODE", completeRequestDto.Code);
                request.AddFile("FILE", completeRequestDto.FilePath);
                RestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        processResult = JsonConvert.DeserializeObject<CompleteResponseDto>(response.Content);
                        if (processResult != null)
                        {
                            processResult.Result = ProcessResultHandler.SuccessHandler();
                            return processResult;
                        }
                    }
                    else
                    {
                        processResult.Result = ProcessResultHandler.FailureHandler("Response content is empty or couldn't be deserialized.", response.ResponseStatus.ToString());
                    }
                }
                else
                {
                    processResult.Result = ProcessResultHandler.FailureHandler(response.StatusCode.ToString() ?? "Request was not successful.", response.ResponseStatus.ToString());
                }
            }
            catch (Exception ex)
            {
                processResult.Result = ProcessResultHandler.ExceptionHandler(ex.Message);
            }
            return processResult;
        }
    }
}
