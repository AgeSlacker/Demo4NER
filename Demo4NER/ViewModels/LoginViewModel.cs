using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.Models;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public User User { get; set; } = new User();
        public event EventHandler<LoginResult> LoginAttempted;
        public Command LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await Login());
        }

        private async Task Login()
        {
            LoginAttempted?.Invoke(this,LoginResult.Success);
        }

        public enum LoginResult
        {
            Success, InvalidPassword, NoUsername
        }
    }
}
