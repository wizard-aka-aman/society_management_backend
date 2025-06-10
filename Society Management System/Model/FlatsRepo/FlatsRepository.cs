
using Microsoft.EntityFrameworkCore;
using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.FlatsRepo
{
    public class FlatsRepository : IFlatsRepository
    {
        private readonly SocietyContext _societyContext;
        public FlatsRepository(SocietyContext societyContext)
        {
            _societyContext = societyContext;
        }
        public async Task<bool> AddFlats(FlatsDto flats)
        {
            Users user = _societyContext.Users.FirstOrDefault(e=> e.Name == flats.Name);

            var flatNumberFound = _societyContext.Flats.FirstOrDefault(e => e.FlatNumber == flats.FlatNumber);
            if(flatNumberFound != null)
            {
                return false;
            }

            Flats flat = new Flats
            {
                FlatNumber = flats.FlatNumber,
                Block = flats.Block,
                FloorNumber = flats.FloorNumber,
                SocietyId = flats.SocietyId,
                Users = user
            };

           _societyContext.Flats.Add(flat);
            await _societyContext.SaveChangesAsync();
            return true;
        }

        public List<Flats> GetAllFlats(int id)
        {
            return _societyContext.Flats.Include(e => e.Users).Where(e => e.SocietyId == id).ToList();
        }

        public async Task<Flats> UpdateFlats(Flats flats, int id)
        {
            Flats flats1 = _societyContext.Flats.Find(id);
            flats1.Users.Name = flats.Users.Name;
            flats1.Users.Email = flats.Users.Email;
            flats1.Users.Role = flats.Users.Role;

            _societyContext.Flats.Update(flats1);
            await _societyContext.SaveChangesAsync();
            return flats1;
        }
    }
}
