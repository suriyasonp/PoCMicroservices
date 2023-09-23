namespace Poc.Frontend.AspNetCoreMVC;

public interface IBaseService
{
    Task<ResponseDto?> SendAsync(RequestDto requestDto);
}
