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
    public class NewEstablishmentViewModel : BaseViewModel
    {
        public Establishment NewEstab { get; set; } = new Establishment();
        public ObservableCollection<Establishment> Establishments { get; set; } = new ObservableCollection<Establishment>();
        public Command CreateNewCommand { get; set; }
        public Command LoadListCommand { get; set; }

        public NewEstablishmentViewModel()
        {
            CreateNewCommand = new Command(async () => await CreateNewCommandExecute());
            LoadListCommand = new Command(async () => await LoadListCommandExecute());
        }

        private async Task LoadListCommandExecute()
        {
            var estabs = await GetEstablismetnsAsync();
            Establishments.Clear();
            foreach (var e in estabs)
            {
                Establishments.Add(e);
            }
        }

        private async Task CreateNewCommandExecute()
        {
            using (var db = new NerContext())
            {
                db.Add(NewEstab);
                await db.SaveChangesAsync();
            }
            await LoadListCommandExecute();
        }

        private async Task<List<Establishment>> GetEstablismetnsAsync()
        {
            using (var db = new NerContext())
            {
                return await db.Establishments.ToListAsync();
            }
        }
    }
}
