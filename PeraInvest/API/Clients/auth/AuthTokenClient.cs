using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PeraInvest.API.Clients.auth {

    public interface IAuthTokenClient {
        Task<AccessToken> GenerateToken();
    }
    public class AuthTokenClient : IAuthTokenClient {

        private readonly HttpClient client;
        private readonly ClientCredentialsTokenRequest tokenRequest;
        private readonly ILogger<AuthTokenClient> logger;

        public AuthTokenClient(HttpClient httpClient, ClientCredentialsTokenRequest tokenRequest, ILogger<AuthTokenClient> log) {
            client = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.tokenRequest = tokenRequest ?? throw new ArgumentNullException(nameof(tokenRequest));
            logger = log ?? throw new ArgumentNullException(nameof(log));
        }

        public async Task<AccessToken> GenerateToken() {
            logger.LogInformation("Generating access token");

            var request = new HttpRequestMessage(HttpMethod.Post, tokenRequest.url);
            var collection = new List<KeyValuePair<string, string>>();
            collection.Add(new("client_id", tokenRequest.clientId));
            collection.Add(new("client_secret", tokenRequest.clientSecret));
            collection.Add(new("grant_type", tokenRequest.grantType));
            var content = new FormUrlEncodedContent(collection);
            request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            var AccessToken = JsonSerializer.Deserialize<AccessToken>(jsonResponse);

            if (AccessToken is null) throw new JsonException();

            logger.LogInformation("Access Token generated!");

            return AccessToken;
        }
    }

    public record ClientCredentialsTokenRequest(
        string url,
        string clientId,
        string clientSecret,
        string grantType
    );

    public class AccessToken {
        [JsonPropertyName("access_token")]
        public string token { get; set; }

        [JsonPropertyName("expires_in")]
        public int expiresIn { get; set; }

        [JsonPropertyName("refresh_expires_in")]
        public int refreshExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public string tokenType { get; set; }

        [JsonPropertyName("not-before-policy")]
        public int notBeforePolicy { get; set; }

        [JsonPropertyName("scope")]
        public string scope { get; set; }
    }
}
