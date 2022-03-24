﻿// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
using Newtonsoft.Json;

#pragma warning disable

namespace SitrepAPI.Models.Auth0
{
    public class Identity
    {
        [JsonProperty("connection")]
        public string? Connection { get; set; }

        [JsonProperty("provider")]
        public string? Provider { get; set; }

        [JsonProperty("user_id")]
        public string? UserId { get; set; }

        [JsonProperty("isSocial")]
        public bool IsSocial { get; set; }
    }

    public class Telephone
    {
        [JsonProperty("number")]
        public string? Number { get; set; }
    }

    public class UserMetadata
    {
        [JsonProperty("telephone")]
        public Telephone? Telephone { get; set; }
    }

    public class Auth0UserDTO
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("email_verified")]
        public bool EmailVerified { get; set; }

        [JsonProperty("identities")]
        public List<Identity>? Identities { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("nickname")]
        public string? Nickname { get; set; }

        [JsonProperty("picture")]
        public string? Picture { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("user_id")]
        public string? UserId { get; set; }

        [JsonProperty("last_password_reset")]
        public DateTime LastPasswordReset { get; set; }

        [JsonProperty("user_metadata")]
        public UserMetadata? UserMetadata { get; set; }

        [JsonProperty("last_ip")]
        public string? LastIp { get; set; }

        [JsonProperty("last_login")]
        public DateTime LastLogin { get; set; }

        [JsonProperty("logins_count")]
        public int LoginsCount { get; set; }
    }

}
#pragma warning restore
