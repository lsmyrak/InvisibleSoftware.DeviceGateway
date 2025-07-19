namespace InvisibleSoftware.DeviceGateway.Api
{
    public class AuthenticationSetting
    {
        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; }
    }
}
