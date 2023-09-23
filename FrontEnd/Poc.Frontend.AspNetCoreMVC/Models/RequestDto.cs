using static Poc.Frontend.AspNetCoreMVC.SD;

namespace Poc.Frontend.AspNetCoreMVC;

public class RequestDto
{
    public ApiType ApiType { get; set; } = ApiType.GET;
    public string Url { get; set; }
    public object Data { get; set; }
    public string AccessToken { get; set; }
}
