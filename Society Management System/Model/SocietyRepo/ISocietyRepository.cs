using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.SocietyRepo
{
    public interface ISocietyRepository
    {
        Task<List<Society>> GetAll();

        Task<bool> UpdateSociety (SocietyDto societyDto, int id);
        
        Task<bool> DeleteSociety (int id);

        Task<bool> AddSociety(SocietyDto societyDto);
        Task<bool> changeSocietyName(SocietyDto dto, int id);
        Task<bool> changeSocietyNotifyBefore(SocietyDto dto, int id);
        Task<Society> GetSocietyDetail(int id);
    }
}
