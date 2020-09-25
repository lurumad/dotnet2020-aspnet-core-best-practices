using AspNetCore.Hashids.Json;
using System.Text.Json.Serialization;

namespace Dotnet2020.Api.Features.Security
{
    public class CustomerDto
    {
        [JsonConverter(typeof(HashidsJsonConverter))]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
