using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.Models;
using Demo4NER.Services;
using Microsoft.EntityFrameworkCore;

namespace Demo4NER.ViewModels
{
    public class DiscountsViewModel : BaseBusinessListViewModel
    {
        public override async Task UpdateBusinessesListExecute()
        {
            await base.UpdateBusinessesListExecute();

            var tempBusinesses = new List<Business>(BusinessesList.Where(b => b.HasDiscounts));
            BusinessesList.Clear();
            foreach (Business business in tempBusinesses)
            {
                BusinessesList.Add(business);
            }
        }
    }
}
