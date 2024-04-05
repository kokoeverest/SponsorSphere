using SponsorSphere.Domain.Interfaces;
//using InternshipProject.Domain.Logs;

namespace SponsorSphere.Domain.Models
{
    public abstract class User(
        string name,
        string email,
        string password,
        string country
        ) : IUser
    {
        public int? Id { get; set; } = null;
        public string Name { get; set; } = name;
        public string Email { get; set; } = email;
        public string Password { private get; set; } = password;
        public string Country { get; set; } = country;
        public string Phone { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;
        public string PictureOrLogo { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
        public string FaceBookLink { get; set; } = string.Empty;
        public string InstagramLink { get; set; } = string.Empty;
        public string TwitterLink { get; set; } = string.Empty;
        public string StravaLink { get; set; } = string.Empty;
        public List<BlogPost> Posts { get; set; } = [];

        public async Task Register()
        {
            //Logger currentLog = new("Register", true);
            //try
            //{
            //    await currentLog.LogMessage(currentLog);
            //    Console.WriteLine($"{Name} registered successfully!");

            //}
            //catch (Exception)
            //{
            //    currentLog.Success = false;
            //    await currentLog.LogMessage(currentLog);
            //    await Console.Out.WriteLineAsync($"{Name} could not be registered!");
            //}
        }
        public async Task Login()
        {
            //Logger currentLog = new("Login", true);
            //try
            //{
            //    await currentLog.LogMessage(currentLog);
            //    Console.WriteLine($"{Name} logged in successfully!");

            //}
            //catch (Exception)
            //{
            //    currentLog.Success = false;
            //    await currentLog.LogMessage(currentLog);
            //    await Console.Out.WriteLineAsync($"Login unsuccessful for {Name}!");
            //}
        }
        public async Task Logout()
        {
            //Logger currentLog = new("Logout", true);
            //try
            //{
            //    await currentLog.LogMessage(currentLog);
            //    Console.WriteLine($"{Name} logged out successfully!");

            //}
            //catch (Exception)
            //{
            //    currentLog.Success = false;
            //    await currentLog.LogMessage(currentLog);
            //    await Console.Out.WriteLineAsync($"{Name} could not log out!");
            //}
        }
        public async Task ResetPassword()
        {
            //Logger currentLog = new("ResetPassword", true);
            //try
            //{
            //    await currentLog.LogMessage(currentLog);
            //    Console.WriteLine($"{Name} reset password successfully!");

            //}
            //catch (Exception)
            //{
            //    currentLog.Success = false;
            //    await currentLog.LogMessage(currentLog);
            //    await Console.Out.WriteLineAsync($"Password for {Name} could not be reset!");
            //}
        }
        public async Task EditProfile()
        {
            //Logger currentLog = new("EditProfile", true);
            //try
            //{
            //    await currentLog.LogMessage(currentLog);
            //    Console.WriteLine($"{Name} profile edited successfully!");

            //}
            //catch (Exception)
            //{
            //    currentLog.Success = false;
            //    await currentLog.LogMessage(currentLog);
            //    await Console.Out.WriteLineAsync($"{Name} profile could not be edited!");
            //}
        }
    }
}
