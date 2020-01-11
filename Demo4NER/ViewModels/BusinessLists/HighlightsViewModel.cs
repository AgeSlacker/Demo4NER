using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo4NER.Models;
using Demo4NER.Services;
using Microsoft.EntityFrameworkCore;

namespace Demo4NER.ViewModels
{
    public class HighlightsViewModel : BaseBusinessListViewModel
    {
        public override async Task UpdateBusinessesListExecute()
        {
            await base.UpdateBusinessesListExecute();
            var tempBusiness = new List<Business>(BusinessesList.Where(b => b.IsFeatured));
            BusinessesList.Clear();
            foreach (Business business in tempBusiness)
            {
                BusinessesList.Add(business);
            }
        }
    }
}
