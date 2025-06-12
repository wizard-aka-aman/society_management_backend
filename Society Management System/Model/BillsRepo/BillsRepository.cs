using Microsoft.EntityFrameworkCore;
using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.BillsRepo
{
    public class BillsRepository : IBillsRepository
    {
        private readonly SocietyContext _societyContext;
        public BillsRepository(SocietyContext societyContext)
        {
            _societyContext = societyContext;
        }
        public async Task<bool> AddBill(BillsDto bills)
        {
            Flats flat =await _societyContext.Flats.Include(e =>e.Users).FirstOrDefaultAsync(e => e.FlatNumber == bills.FlatNumber);

            if (flat == null)
            {
                return false;
            }
            Bills bill = new Bills
            {
                Amount = bills.Amount,
                DueDate = bills.DueDate,
                Flats = flat,
                GeneratedDate = DateTime.Now,
                Type = bills.Type,
                IsPaid = false
            };
            _societyContext.Bills.Add(bill);
            await _societyContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteBill(int id)
        {
            var bill = await _societyContext.Bills.FirstOrDefaultAsync(e => e.BillsId == id);
            if (bill == null)
            {
                return false;
            }
            _societyContext.Bills.Remove(bill);
            await _societyContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Bills>> GetAllBills(int id)
        {
            var allBills = await _societyContext.Bills.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.SocietyId == id).ToListAsync();
            return allBills;
        }

        public async Task<List<Bills>> GetMyBills(string name)
        {
            var myBills = await _societyContext.Bills.Include(e => e.Flats).ThenInclude(e => e.Users)
                .Where(e => e.Flats.Users != null && e.Flats.Users.Name == name).ToListAsync();
            return myBills;
        }

        public async Task<bool> UpdateBill(BillsDto bills , int id)
        {
            var bill = await _societyContext.Bills.Include(e => e.Flats).FirstOrDefaultAsync(e => e.BillsId == id);
            if (bill == null)
            {
                return false;
            }
            bill.Flats.FlatNumber = bills.FlatNumber;
            bill.DueDate = bills.DueDate;
            bill.Amount = bills.Amount;
            bill.Type = bills.Type;

              _societyContext.Update(bill);

            await _societyContext.SaveChangesAsync();
            return true;
        }
    }
}
