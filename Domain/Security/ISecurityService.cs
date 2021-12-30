
namespace Domain.Security
{
    public interface ISecurityService
    {
        string GetHashPbkdf2(string password);
    }
}
