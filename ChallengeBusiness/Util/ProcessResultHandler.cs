using ChallengeEntity.Dto;

namespace ChallengeBusiness.Util
{
    public static class ProcessResultHandler
    {
        /// <summary>
        /// Success Handler
        /// </summary>
        /// <returns></returns>
        public static ProcessResult SuccessHandler()
        {
            return new ProcessResult
            {
                Result = true,
                ResultCode = "1",
                ResultMessage = "Success"
            };
        }

        /// <summary>
        /// Failure Handler
        /// </summary>
        /// <returns></returns>
        public static ProcessResult FailureHandler(string message, string resultCode)
        {
            return new ProcessResult
            {
                Result = false,
                ResultCode = resultCode,
                ResultMessage = message
            };
        }

        /// <summary>
        /// Exception Handler
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static ProcessResult ExceptionHandler(string ex)
        {
            return new ProcessResult
            {
                Result = false,
                ResultCode = "-99",
                ResultMessage = $"Exception: {ex}"
            };
        }
    }
}
