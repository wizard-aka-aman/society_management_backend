using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.VisitorsRepo
{
    public interface IVisitorsRepository
    {
        Task<List<Visitors>> GetAllVisitors(int id);
        Task<List<Visitors>> GetOneVisitors(int id);

        Task<bool> AddVisitors(VisitorsDto visitor);

        Task<bool> UpdateVisitors(VisitorsDto visitor, int id);
        Task<bool> DeleteVisitors(int id);
    }
}
