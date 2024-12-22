namespace ServiceCentre.Helpers
{
    public static class Session
    {
        public static int UserId { get; set; }
        public static string Username { get; set; }
        public static string Role { get; set; }

        public static void Clear()
        {
            UserId = 0;
            Username = null;
            Role = null;
        }
        public static string GetUserRole()
        {
            return Role;
        }
    }
}