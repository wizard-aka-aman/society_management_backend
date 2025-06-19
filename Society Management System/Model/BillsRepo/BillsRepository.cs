using Microsoft.EntityFrameworkCore;
using Society_Management_System.Model.Dto_s;
using Society_Management_System.Model.JobRepo;
using Society_Management_System.Services.EmailService;

namespace Society_Management_System.Model.BillsRepo
{
    public class BillsRepository : IBillsRepository
    {
        private readonly SocietyContext _societyContext;
        private readonly IEmailService _emailService;
        private readonly IJobRepository _jobRepository;

         
        public BillsRepository(SocietyContext societyContext, IEmailService emailService, IJobRepository jobRepository)
        {
            _societyContext = societyContext;
            _emailService = emailService;
            _jobRepository = jobRepository;
        }
        public async Task<Bills> AddBill(BillsDto bills)
        {
            Flats flat = await _societyContext.Flats.Include(e => e.Users).Include(e => e.Users).FirstOrDefaultAsync(e => e.Users.Name == bills.Name);

            if (flat == null)
            {
                return null;
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
            EmailDto emailDto = new EmailDto
            {
                Amount = bills.Amount,
                DueDate = bills.DueDate.ToString(),
                Name = bills.Name,
                Type = bills.Type,
                Email = flat.Users.Email
            };

            _emailService.JobSendEmail(emailDto);
            EmailItemDto emailItemDto = new EmailItemDto
            {
                Amount = bills.Amount,
                BillId = bill.BillsId,
                DueDate = bills.DueDate.ToString(),
                Email = emailDto.Email,
                Name = bills.Name,
                Type = bills.Type,
                NotifyBefore = bills.NotifyBefore??1

            };

           await _jobRepository.CreateScheduleJob(emailItemDto);
            return bill;
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
            var allBills = await _societyContext.Bills.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.SocietyId == id).OrderByDescending(e => e.GeneratedDate).ToListAsync();
            return allBills;
        }

        public async Task<List<Bills>> GetMyBills(string name)
        {
            var myBills = await _societyContext.Bills.Include(e => e.Flats).ThenInclude(e => e.Users)
                .Where(e => e.Flats.Users != null && e.Flats.Users.Name == name).OrderByDescending(e => e.GeneratedDate).ToListAsync();
            return myBills;
        }

        public async Task<bool> UpdateBill(BillsDto bills, int id)
        {
            var bill = await _societyContext.Bills.Include(e => e.Flats).FirstOrDefaultAsync(e => e.BillsId == id);
            Flats flat = await _societyContext.Flats.Include(e => e.Users).Include(e => e.Users).FirstOrDefaultAsync(e => e.Users.Name == bills.Name);
            if (bill == null)
            {
                return false;
            }
            bill.Flats = flat;
            bill.DueDate = bills.DueDate;
            bill.Amount = bills.Amount;
            bill.Type = bills.Type;

            _societyContext.Update(bill);

            await _societyContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> PayBill(int id)
        {
            var bill = await _societyContext.Bills.Include(e => e.Flats).FirstOrDefaultAsync(e => e.BillsId == id);

            if (bill == null)
            {
                return false;
            }
            bill.IsPaid = true;
            bill.PaidDate = DateTime.Now;

            _societyContext.Update(bill);

            await _societyContext.SaveChangesAsync();
            return true;
        }
    }
}
