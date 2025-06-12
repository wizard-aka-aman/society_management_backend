using Microsoft.EntityFrameworkCore;
using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.NoticesRepo
{
    public class NoticesRepository : INoticesReopsitory
    {
        private readonly SocietyContext _societyContext;
        public NoticesRepository(SocietyContext societyContext)
        {
            _societyContext = societyContext;
        }
        public async Task<bool> AddNotices(NoticesDto Notices)
        { 

            Notices notice = new Notices
            {
               CreatedAt = DateTime.Now,
               Description  = Notices.Description,
               SocietyId = Notices.SocietyId,
               Title = Notices.Title,
            };

            _societyContext.Notices.Add(notice);
            await _societyContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteNotices(int id)
        {
            Notices notice = _societyContext.Notices.Find(id);
            if (notice == null)
            {
                return false;
            }
            var item = _societyContext.Notices.Remove(notice); 
            await _societyContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Notices>> GetAllNotices(int id)
        {
            return await _societyContext.Notices.Where(e => e.SocietyId == id).ToListAsync();
        }

        public async Task<bool> UpdateNotices(NoticesDto Notices, int id)
        {
           
            Notices notice = await _societyContext.Notices.FirstOrDefaultAsync(f => f.NoticesId == id);
             
             
            notice.Title = Notices.Title;
            notice.Description = Notices.Description;

            _societyContext.Notices.Update(notice);
            await _societyContext.SaveChangesAsync();
            return true;
        }
    }
}
