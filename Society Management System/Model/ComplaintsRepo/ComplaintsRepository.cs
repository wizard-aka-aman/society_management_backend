
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

        public List<Complaints> GetAllComplaints(int id)
        {

            return _societyContext.Complaints.Include(e => e.Flats).Where(e => e.Flats.SocietyId == id).ToList();
        }

        public List<Complaints> GetMyComplaints(string name)
        {
            return _societyContext.Complaints.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.Users.Name == name).ToList();
        }



        public async Task<Complaints> UpdateComplaints(ComplaintsDto complaints, int id)
        {
            Complaints complaints1 = _societyContext.Complaints.Find(id);
            complaints1.Status = complaints.Status;
            _societyContext.Complaints.Update(complaints1);
            await _societyContext.SaveChangesAsync();
            return complaints1;


        }
    }
}
