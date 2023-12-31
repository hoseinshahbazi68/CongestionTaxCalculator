﻿using Entities.User;
using System.Threading.Tasks;

namespace Services.Services
{
    public interface IJwtService
    {
        Task<AccessToken> GenerateAsync(User user);

        Task DeleteExpiredTokensAsync();

        Task InvalidateUserTokensAsync(int userId);

        Task DeleteTokensWithSameRefreshTokenSourceAsync(string refreshTokenIdHashSource);

        Task AddUserTokenAsync(UserToken userToken);

        Task<UserToken> FindTokenAsync(string refreshTokenValue);

        string GetRefreshTokenSerial(string refreshTokenValue);
    }
}