
using Microsoft.EntityFrameworkCore;

namespace Society_Management_System.Model.ComplaintsRepo
{
    public class ComplaintsRepository : IComplaintsRepository
    {
        private readonly SocietyContext _societyContext;
        public ComplaintsRepository(SocietyContext societyContext)
        {
            _societyContext = societyContext;
        }
        public async Task<Complaints> AddComplaints(Complaints complaints)
        {
            complaints.DateCreated = DateTime.Now;
            _societyContext.Complaints.Add(complaints);
            await _societyContext.SaveChangesAsync();
            return complaints;


        }

        public List<Complaints> GetAllComplaints(int id)
        {

            return _societyContext.Complaints.Include(e => e.Flats).Where(e => e.Flats.SocietyId == id).ToList();
        }

        public List<Complaints> GetMyComplaints(string name)
        {
            return _societyContext.Complaints.Include(e => e.Flats).ThenInclude(e => e.Users).Where(e => e.Flats.Users.Name == name).ToList();
        }



        public async Task<Complaints> UpdateComplaints(Complaints complaints, int id)
        {
            Complaints complaints1 = _societyContext.Complaints.Find(id);
            complaints1.Status = complaints.Status;
            _societyContext.Complaints.Update(complaints1);
            await _societyContext.SaveChangesAsync();
            return complaints1;


        }
    }
}
