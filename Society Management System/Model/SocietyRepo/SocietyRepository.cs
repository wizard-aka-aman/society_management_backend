
using Microsoft.EntityFrameworkCore;
using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.SocietyRepo
{
    public class SocietyRepository : ISocietyRepository
    {
        private readonly SocietyContext _societyContext;
        public SocietyRepository(SocietyContext societyContext)
        {
            _societyContext = societyContext;
        }
        public async Task<bool> AddSociety(SocietyDto societyDto)
        {

            Society society = new Society
            {
                Admin = societyDto.Admin,
                CreatedWhen = DateTime.Now,
                Name = societyDto.Name,
            };

            _societyContext.Society.Add(society);
            await _societyContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSociety(int id)
        {
            Society society = _societyContext.Society.Find(id);
            if (society == null)
            {
                return false;
            }
            var item = _societyContext.Society.Remove(society);
            await _societyContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Society>> GetAll()
        {
            return await _societyContext.Society.OrderByDescending(e => e.CreatedWhen).ToListAsync();
        }


        public async Task<bool> UpdateSociety(SocietyDto societyDto, int id)
        {

            Users user = await _societyContext.Users.FirstOrDefaultAsync(e => e.Name == societyDto.Admin);
            if (user != null)
            {
                user.SocietyId = id;
                _societyContext.Users.Update(user);
                await _societyContext.SaveChangesAsync();
            }
            Society society = _societyContext.Society.FirstOrDefault(e => e.SocietyId == id);
            if (society == null)
            {
                return false;
            }
            society.Name = societyDto.Name;
            society.Admin = societyDto.Admin;
            _societyContext.Society.Update(society);
            await _societyContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> changeSocietyName(SocietyDto dto , int id)
        {
            Society society =  await _societyContext.Society.FirstOrDefaultAsync(e => e.SocietyId == id);
            if(society == null)
            {
                return false;
            }

            society.Name = dto.Name;
            _societyContext.Society.Update(society);
            await _societyContext.SaveChangesAsync();
            return true;

        }
    }
}
