using System;
using System.Collections.Generic;
using System.Text;
using Demo4NER.Models;
using Demo4NER.Views;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    class ProfileViewModel : BaseViewModel
    {
        public User User { get; set; }
        public Command LoadUserCommand { get; set; }
        public ProfileViewModel(User User = null)
        {
            Title = "Profile";
            LoadUserCommand = new Command(async () => await ExecuteLoadUserCommand());
        }
        async Task ExecuteLoadUserCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                User = null;
                var users = await UserStore.GetItemsAsync(true);
                foreach (var user in users)
                {
                    if (user.Name == "Ricardo")
                    {
                        User = user;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
