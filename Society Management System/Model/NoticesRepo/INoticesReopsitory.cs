using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.NoticesRepo
{
    public interface INoticesReopsitory
    {
        Task<List<Notices>> GetAllNotices(int id);
        Task<List<Notices>> GetOneNotice(int id);

        Task<bool> AddNotices(NoticesDto Notices);

        Task<bool> UpdateNotices(NoticesDto Notices, int id);
        Task<bool> DeleteNotices(int id);
    }
}
