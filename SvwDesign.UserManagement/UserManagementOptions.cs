namespace SvwDesign.UserManagement
{
    public class UserManagementOptions
    {
        public bool ValidateAudience { get; set; }

        public bool ValidateIssuer { get; set; }

        public string ValidAudience { get; set; } = string.Empty;

        public string ValidIssuer { get; set; } = string.Empty;

        public string SecretKey { get; set; } = string.Empty;

        public int ExpiresInHours { get; set; }

        public string ConnectionstringName { get; set; } = string.Empty;
    }
}
