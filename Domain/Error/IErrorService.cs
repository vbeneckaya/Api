namespace Domain.Error
{
    public interface IErrorService
    {
        ErrorViewModel GetError(ErrorCodes errorCode);
    }
}
