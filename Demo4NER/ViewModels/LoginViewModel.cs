using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.Models;
using Demo4NER.Services;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public User User { get; set; } = new User();
        private string _error;
        public String Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }
        public String PasswordInput { get; set; }
        public event EventHandler<LoginResult> LoginAttempted;
        public Command LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login());
        }

        private async Task Login()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            try
            {
                User resultUser = await Task.Run(() =>
                {
                    using (var db = new NerContext())
                    {
                        return db.Users.FirstOrDefault(u => u.Email == User.Email);
                    }
                });
                if (resultUser == null)
                {
                    Error = "No such user!";
                    return;
                }
                // Hash user input
                /* Fetch the stored value */
                string savedPasswordHash = resultUser.Password;
                /* Extract the bytes */
                byte[] hashBytes = Convert.FromBase64String(savedPasswordHash);
                /* Get the salt */
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                /* Compute the hash on the password the user entered */
                var pbkdf2 = new Rfc2898DeriveBytes(PasswordInput, salt, 1000);
                byte[] hash = pbkdf2.GetBytes(20);
                /* Compare the results */
                for (int i = 0; i < 20; i++)
                    if (hashBytes[i + 16] != hash[i])
                    {
                        Error = "Password doesn't match!";
                        return;
                    }

                User = resultUser;
                LoginAttempted?.Invoke(this, LoginResult.Success);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public enum LoginResult
        {
            Success, InvalidPassword, NoUsername
        }
    }
}
