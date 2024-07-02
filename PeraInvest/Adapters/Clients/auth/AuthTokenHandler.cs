using Microsoft.Extensions.Caching.Memory;

namespace PeraInvest.Adapters.Clients.auth {
    public class AuthTokenHandler : DelegatingHandler {

        private readonly AuthTokenClient authClient;
        private readonly ClientCredentialsTokenRequest tokenRequest;
        private readonly IMemoryCache cache;
        private readonly ILogger<AuthTokenHandler> log;

        public AuthTokenHandler(AuthTokenClient client, ClientCredentialsTokenRequest tokenRequest, IMemoryCache cache, ILogger<AuthTokenHandler> log) {
            this.authClient = client ?? throw new ArgumentNullException(nameof(client));
            this.tokenRequest = tokenRequest ?? throw new ArgumentNullException(nameof(tokenRequest));
            this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }



        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            String cacheKey = tokenRequest.clientId;

            if (cache.TryGetValue(cacheKey, out var cachedAccessToken)) {
                log.LogInformation("Using cached access token");
            }

            AccessToken accessToken = cachedAccessToken as AccessToken ?? GenerateAccessToken(request);

            request.Headers.Add("Authorization", $"Bearer {accessToken?.token}");
            
            return await base.SendAsync(request, cancellationToken);

        }

        private AccessToken GenerateAccessToken(HttpRequestMessage request) {
            log.LogInformation("Generating access token");

            AccessToken accessToken = authClient.GenerateToken().Result;

            TimeSpan expiresIn = TimeSpan.FromSeconds(accessToken.expiresIn - 60);

            cache.Set(tokenRequest.clientId, accessToken, expiresIn);

            return accessToken;
        }
    }
}
