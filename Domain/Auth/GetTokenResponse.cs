using Domain.User;

namespace Domain.Auth
{
    public class GetTokenResponse
    {
        public string Token { get; set; }
        public GameDataDto GameData { get; set; }
    }
}