using Demo4NER.Models;
using Demo4NER.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{

    public class DBTestsViewModel : BaseViewModel
    {
        int loadedUsers;
        public int LoadedUsers
        {
            get { return loadedUsers; }
            set { SetProperty(ref loadedUsers, value); }
        }
        public User NewUser { get; set; } = new User();
        public Command NewUserCommand { get; set; }
        public Command LoadUsersCommand { get; set; }
        public ObservableCollection<User> Users { get; } = new ObservableCollection<User>();
        public DBTestsViewModel()
        {
            NewUserCommand = new Command(execute: async () =>
            {
                await CreateNewUser(NewUser);
                NewUser = new User();
                await LoadUsers();
            });
            LoadUsersCommand = new Command(execute: async () => await LoadUsers());

        }
        private async Task CreateNewUser(User user)
        {
            using (var db = new NerContext())
            {
                await db.AddAsync(user);
                db.SaveChanges();
            }
        }

        private async Task LoadUsers()
        {
            using (var db = new NerContext())
            {
                var _users = await db.Users.ToListAsync();
                Users.Clear();
                foreach (User user in _users)
                    Users.Add(user);
            }
            LoadedUsers = Users.Count;
        }
    }
}
