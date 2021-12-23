namespace SampleProject
{
    public class AuthenticatedUser
    {
        public string? access_token { get; set; }
        public string? refresh_token { get; set; }
        public string? user_id { get; set; }
        public string? device_id { get; set; }
    }
}
