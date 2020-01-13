using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.Forms;

using Demo4NER.Models;
using Demo4NER.Services;

namespace Demo4NER.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IDataStore<Item> DataStore => DependencyService.Get<IDataStore<Item>>() ?? new MockDataStore();
        public IDataStore<User> UserStore => DependencyService.Get<IDataStore<User>>() ?? new UserDataStore();

        public Command SendAskRefreshCommand { get; set; }

        public bool IsRefreshing    
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing,value);
        }

        public BaseViewModel()
        {
            SendAskRefreshCommand = new Command(async () =>
            {
                if (IsRefreshing) return;
                IsRefreshing = true;
                await Task.Run(()=>((App) Application.Current).SendRefreshToAllPages());
                IsRefreshing = false;
            });
        }

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); }
        }

        string title = string.Empty;
        private bool _isRefreshing;

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }



        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
