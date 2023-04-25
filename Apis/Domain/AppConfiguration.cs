﻿namespace Domain
{
    public class AppConfiguration
    {
        public ConnectionStrings ConnectionStrings { get; set; }
        public Jwt Jwt { get; set; }
        public BaseUrl BaseUrl { get; set; }
        public Token Token { get; set; }
    }

    public class Token
    {
        public string TenantId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessToken { get; set; }
    }
    public class ConnectionStrings
    {
        public string DatabaseConnection { get; set; }
    }
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
    public class BaseUrl
    {
        public string Outlook { get; set; }
    }
}