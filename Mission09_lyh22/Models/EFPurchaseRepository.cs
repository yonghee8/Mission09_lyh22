using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Models
{
    public class EFPurchaseRepository : IPurchaseRepository
    {
        private BookstoreContext context;
        public EFPurchaseRepository(BookstoreContext temp)
        {
            context = temp;
        }
        public IQueryable<Purchasement> Purchasements => context.Purchasements.Include(x => x.Lines).ThenInclude(x => x.Book);

        IQueryable<Purchasement> IPurchaseRepository.Purchasements { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void SavePurchasement(Purchasement purchasement)
        {
            context.AttachRange(purchasement.Lines.Select(x => x.Book));

            if (purchasement.PurchasementId == 0)
            {
                context.Purchasements.Add(purchasement);
            }
            context.SaveChanges();
        }
    }
}
