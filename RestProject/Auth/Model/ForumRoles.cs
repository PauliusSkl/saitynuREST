namespace RestProject.Auth.Model
{
    public static class ForumRoles
    {
        public const string Admin = nameof(Admin);
        public const string registeredUser = nameof(registeredUser);

        public static readonly IReadOnlyCollection<string> All = new[] { Admin, registeredUser };
    }
}
