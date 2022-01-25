namespace Core.Services.EmailService.Contracts
{
    public interface INotificationArgs
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
