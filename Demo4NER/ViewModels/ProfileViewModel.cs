using Demo4NER.Models;
using Demo4NER.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        public User NewUser { get; set; } = new User();

        public Command LoadUserCommand { get; set; }

        public ProfileViewModel() 
        {
            LoadUserCommand = new Command(async () => await LoadUserCommandExecute());
        }

        private async Task LoadUserCommandExecute()
        {
            using (var db = new NerContext()) {
                var user = await db.Users.Where(s => s.Name == "Ricardo").FirstOrDefaultAsync<User>();
                NewUser = user;
            }
        }
    }
}
