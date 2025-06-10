using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.FlatsRepo
{
    public interface IFlatsRepository
    { 
        List<Flats> GetAllFlats(int id);

        Task<bool> AddFlats(FlatsDto flats);

        Task<Flats> UpdateFlats(Flats flats, int id);
    }
}
