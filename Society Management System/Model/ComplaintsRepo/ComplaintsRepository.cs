
using Microsoft.EntityFrameworkCore;
using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.ComplaintsRepo
{
    public class ComplaintsRepository : IComplaintsRepository
    {
        private readonly SocietyContext _societyContext;
        public ComplaintsRepository(SocietyContext societyContext)
        {
            _societyContext = societyContext;
        }
        public async Task<Complaints> AddComplaints(ComplaintsDto complaints)
        {
            Flats flat = _societyContext.Flats.Include(e => e.Users).FirstOrDefault(e => e.Users.Name == complaints.Name);

            Complaints complaints1 = new Complaints
            {
                DateCreated = DateTime.Now,
                Description = complaints.Description,
                Title = complaints.Title,
                Status = "Pending",
                DateResolved = null,
                Flats = flat
            };
            _societyContext.Complaints.Add(complaints1);
            await _societyContext.SaveChangesAsync();
            return complaints1;


        }

        public async Task<bool> DeleteComplaints(int id)
        {
            Complaints complaints = await _societyContext.Complaints.FirstOrDefaultAsync(e => e.ComplaintsId == id);

            if (complaints == null)
            {
                return false;
            }
            _societyContext.Complaints.Remove(complaints);
            await _societyContext.SaveChangesAsync();
            return true;
        }

        public List<Complaints> GetAllComplaints(int id)
        {

            return _societyContext.Complaints.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.SocietyId == id).OrderByDescending(e => e.DateCreated).ToList();
        }

        public List<Complaints> GetMyComplaints(string name)
        {
            return _societyContext.Complaints.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.Users.Name == name).OrderByDescending(e => e.DateCreated).ToList();
        }
 

        public async Task<int> TotalComplaints(int id)
        {
            var total  = await _societyContext.Complaints.Include(e => e.Flats).Where(e => e.Flats.SocietyId == id).ToListAsync();
            var final = total.Where(e => e.Status != "Completed").Count();
            return final; 
        } 
        public async Task<int> TotalCompletedComplaints(int id)
        {
            var total  = await _societyContext.Complaints.Include(e => e.Flats).Where(e => e.Flats.SocietyId == id).ToListAsync();
            var final = total.Where(e => e.Status == "Completed").Count();
            return final; 
        }
         




        public async Task<Complaints> UpdateComplaints(ComplaintsDto complaints, int id)
        {
            Complaints complaints1 = _societyContext.Complaints.Find(id);
            complaints1.Status = complaints.Status;
            if(complaints1.Status == "Completed")
            {
                complaints1.DateResolved = DateTime.Now;
                complaints1.FeedBack = complaints.FeedBack;
            }
            _societyContext.Complaints.Update(complaints1);
            await _societyContext.SaveChangesAsync();
            return complaints1;


        }

        public async Task<int> MyComplaintsNumber(string name)
        {
           var total = await _societyContext.Complaints.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.Users.Name == name).ToListAsync();
            var final = total.Where(e => e.Status != "Completed").Count();
            return final;
        }
        public async Task<int> MyCompletedComplaintsNumber(string name)
        {
           var total = await _societyContext.Complaints.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.Users.Name == name).ToListAsync();
            var final = total.Where(e => e.Status == "Completed").Count();
            return final;
        } 
         
         
    }
}
