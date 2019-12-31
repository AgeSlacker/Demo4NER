﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.Models;
using Demo4NER.Services;
using Xamarin.Forms;

namespace Demo4NER.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        public User User { get; set; } = new User();
        private string _preHashPassword;

        public string PreHashPassword
        {
            get => _preHashPassword; 
            set => SetProperty(ref _preHashPassword, value);
        }
        private String _error = null;
        public String Error
        {
            get => _error;
            set => SetProperty(ref _error, value);
        }

        public Command RegisterCommand { get; set; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(async () => await Register());
        }

        private async Task Register()
        {
            if (IsBusy) return;
            IsBusy = true;
            try
            {
                if (string.IsNullOrEmpty(User.Email) || !(User.Email.Contains("@")))
                {
                    // TODO get if user already registered, on another thread so the user can have
                    // real time feedback
                    Error = "Email inválido!";
                    return;
                }
                else if (string.IsNullOrEmpty(User.Name))
                {
                    Error = "Nome não pode estar vazio";
                    return;
                }
                else if (string.IsNullOrEmpty(User.Nationality))
                {
                    Error = "Nacionalidade inválida";
                    return;
                }
                else if (string.IsNullOrEmpty(PreHashPassword)) // TODO verificar se pass é segura
                {
                    Error = "Password invália";
                    return;
                }
                // Success!
                // TODO hash password!
                User.Password = PreHashPassword;
                await Task.Run(() =>
                {
                    using (var db = new NerContext())
                    {
                        db.Users.Add(User);
                        db.SaveChanges();
                    }
                });
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }
            finally
            {
                PreHashPassword = null;
                IsBusy = false;
            }
        }
    }
}
