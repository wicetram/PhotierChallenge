using Newtonsoft.Json;

namespace ChallengeEntity.Dto
{
    public class GenericResponseDto
    {
        [JsonProperty("completed")]
        public bool Completed { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
