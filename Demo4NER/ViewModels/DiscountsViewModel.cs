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
    public class DiscountsViewModel : BaseBusinessListViewModel
    {
        public override async Task<List<Business>> GetBusinesses()
        {
            using (var db = new NerContext())
            {
                return await db.Businesses.Where(b => b.HasDiscounts).ToListAsync();
            }
        }
    }
}
