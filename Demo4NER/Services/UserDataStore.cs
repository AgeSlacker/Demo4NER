using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Demo4NER.Models;

namespace Demo4NER.Services
{
    class UserDataStore : IDataStore<User>
    {
        List<User> users;

        public UserDataStore() {
            users = new List<User>();
            var actualUsers = new List<User>
            {
                new User { Name = "Achilles", Email = "achilles_o_krl@ezmail.com", Password="o krl", Nationality="Brasileira"},
                new User { Name = "Oleksandr", Email = "hackerman@ezmail.com", Password="cyka", Nationality="Ucraniana"},
                new User { Name = "Ricardo", Email = "atuamaede4@ezmail.com", Password="boomer", Nationality="Portuguesa"}
            };

            foreach (var user in actualUsers)
            {
                users.Add(user);
            }
        }

        public async Task<bool> AddItemAsync(User user)
        {
            users.Add(user);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(User user)
        {
            var oldUser = users.Where((User arg) => arg.Name == user.Name).FirstOrDefault();
            users.Remove(oldUser);
            users.Add(user);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string name)
        {
            var oldUser = users.Where((User arg) => arg.Name == name).FirstOrDefault();
            users.Remove(oldUser);

            return await Task.FromResult(true);
        }

        public async Task<User> GetItemAsync(string name)
        {
            return await Task.FromResult(users.FirstOrDefault(s => s.Name == name));
        }

        public async Task<IEnumerable<User>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(users);
        }
    }
}
