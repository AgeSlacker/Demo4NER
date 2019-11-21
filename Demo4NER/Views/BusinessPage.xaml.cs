using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Demo4NER.Models;
using Demo4NER.Views;
using Demo4NER.ViewModels;

namespace Demo4NER.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessPage : ContentPage
    {
        public ObservableCollection<Bussiness> Businesses{ get; set; }
        public Command LoadBusinessesCommand { get; set; }
        public BusinessPage()
        {
            InitializeComponent();
            BindingContext = this;

            Title = "Browse";
            Businesses = new ObservableCollection<Bussiness>();
            LoadBusinessesCommand = new Command(async () => await ExecuteLoadBusinessesCommand());

            MessagingCenter.Subscribe<NewItemPage, Business>(this, "AddItem", async (obj, item) =>
            {
                var newBus = business as Business;
                Businesses.Add(newBus);
                await DataStore.AddItemAsync(newBus);
            });
        }

        async void OnBusinessSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var business = args.SelectedItem as Bussiness;


        }

        async Task ExecuteLoadBusinessesCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Businesses.Clear();
                var businesses = await DataStore.GetItemsAsync(true);
                foreach (var item in items)
                {
                    Items.Add(item);
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