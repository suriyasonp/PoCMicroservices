using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;

namespace Poc.Frontend.AspNetCoreMVC;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;
    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
    {
        HttpClient client = _httpClientFactory.CreateClient("PocAPI");
        HttpRequestMessage message = new();
        message.Headers.Add("Accept", "Application/json");

        //token

        message.RequestUri = new Uri(requestDto.Url);
        if (requestDto.Data != null)
        {
            message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
        }

        HttpResponseMessage? apiResponse = null;

        message.Method = requestDto.ApiType switch
        {
            SD.ApiType.POST => HttpMethod.Post,
            SD.ApiType.DELETE => HttpMethod.Delete,
            SD.ApiType.PUT => HttpMethod.Put,
            _ => HttpMethod.Get,
        };

        apiResponse = await client.SendAsync(message);

        try
        {
            switch (apiResponse.StatusCode)
            {
                case System.Net.HttpStatusCode.NotFound:
                    return new() { IsSuccess = false, Message = "Not found" };
                case System.Net.HttpStatusCode.Forbidden:
                    return new() { IsSuccess = false, Message = "Access Denied" };
                case System.Net.HttpStatusCode.Unauthorized:
                    return new() { IsSuccess = false, Message = "Unauthorized" };
                case System.Net.HttpStatusCode.InternalServerError:
                    return new() { IsSuccess = false, Message = "Internal Server Error" };
                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                    return apiResponseDto;
            }
        }
        catch (System.Exception ex)
        {
            var dto = new ResponseDto
            {
                Message = ex.Message.ToString(),
                IsSuccess = false
            };
            return dto;
        }
    }
}
