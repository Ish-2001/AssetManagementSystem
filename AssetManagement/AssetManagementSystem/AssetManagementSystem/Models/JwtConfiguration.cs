namespace AssetManagementSystem.Models
{
    public class JwtConfiguration
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int TokenExpirationTimeInMinutes { get; set; }

        public string TokenSecret { get; set; }

       /* public int AccessTokenExpirationTimeInMinutes { get; set; }

        public string RefreshTokenSecret { get; set; }

        public int RefreshTokenExpirationTimeInMinutes { get; set; }


        public int RecoveryTokenExpirationTimeInMinutes { get; set; }

        public string IdentityTokenSecret { get; set; }

        public int IdentityTokenExpirationTimeInMinutes { get; set; }*/
    }
}
