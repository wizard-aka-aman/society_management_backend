using Microsoft.EntityFrameworkCore;
using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.VisitorsRepo
{
    public class VisitorsRepository : IVisitorsRepository
    {

        private readonly SocietyContext _societyContext;
        public VisitorsRepository(SocietyContext societyContext)
        {
            _societyContext = societyContext;
        }
        public async Task<bool> AddVisitors(VisitorsDto visitor)
        {
            Flats flat =await  _societyContext.Flats.Include(e => e.Users).FirstOrDefaultAsync(e => e.Users.Name == visitor.Username);
            Visitors visit = new Visitors
            {
                Flats = flat,
                Name = visitor.Username,
                Purpose = visitor.Purpose,
                VisitDateTime = DateTime.Now,
                SocietyId = visitor.SocietyId,
            };

            _societyContext.Visitors.Add(visit);
            await _societyContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteVisitors(int id)
        {

            Visitors visit = _societyContext.Visitors.Find(id);
            if (visit == null)
            { 
                return false;
            }
            var item = _societyContext.Visitors.Remove(visit);
            await _societyContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Visitors>> GetAllVisitors(int id)
        {
            return await _societyContext.Visitors.Where(e => e.SocietyId == id).OrderByDescending(e => e.VisitDateTime).ToListAsync();
        }
        public async Task<List<Visitors>> GetOneVisitors(int id)
        {
            return await _societyContext.Visitors.Where(e => e.SocietyId == id).OrderByDescending(e => e.VisitDateTime).Take(2).ToListAsync();
        }

        public async Task<bool> UpdateVisitors(VisitorsDto visitor, int id)
        {
            Visitors visit = await _societyContext.Visitors.FirstOrDefaultAsync(f => f.VisitorsId == id);

            visit.Purpose = visitor.Purpose;

            _societyContext.Visitors.Update(visit);
            await _societyContext.SaveChangesAsync();
            return true;
        }
    }
}
