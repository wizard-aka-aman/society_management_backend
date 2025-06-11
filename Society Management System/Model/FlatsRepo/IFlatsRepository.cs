using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.FlatsRepo
{
    public interface IFlatsRepository
    { 
        List<Flats> GetAllFlats(int id);

        Task<bool> AddFlats(FlatsDto flats);

        Task<bool> UpdateFlats(FlatsDto flats, int id);
        Task<bool> DeleteFlats(int id);
    }
}
