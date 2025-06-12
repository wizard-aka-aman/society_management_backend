
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

        public async Task<bool> DeleteFlats(int id)
        {
            Flats flats1 = _societyContext.Flats.Find(id);
            if (flats1 == null) {
                return false;
            }
            var item = _societyContext.Flats.Remove(flats1);
            if (item == null) {
                return false;
            }
            await _societyContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Flats>> GetAllFlats(int id)
        {
            return await _societyContext.Flats.Include(e => e.Users).Where(e => e.SocietyId == id).ToListAsync();
        }

        public async Task<bool> UpdateFlats(FlatsDto flats, int id)
        {
            Users user = _societyContext.Users.FirstOrDefault(e=> e.Name == flats.Name);
            Flats flats1 = await _societyContext.Flats.Include(e => e.Users).FirstOrDefaultAsync(f => f.FlatsId == id);

            var flatNumberFound = _societyContext.Flats.FirstOrDefault(e => e.FlatNumber == flats.FlatNumber && e.FlatsId != id);
            if (flatNumberFound != null)
            {
                return false;
            }

            if (user == null)
            {
                flats1.Users = null; // <-- Make sure Flats entity has this property 
            }
            else
            {
                flats1.Users = user;
            }
            flats1.Block = flats.Block;
            flats1.FlatNumber = flats.FlatNumber;
            flats1.FloorNumber = flats.FloorNumber;

            _societyContext.Flats.Update(flats1);
            await _societyContext.SaveChangesAsync();
            return true;
        }
    }
}
