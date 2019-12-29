using System;
using System.Collections.Generic;
using System.Text;
using Demo4NER.Models;

namespace Demo4NER.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public User User { get; set; } = new User();
    }
}
