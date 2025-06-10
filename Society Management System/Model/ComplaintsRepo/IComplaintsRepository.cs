namespace Society_Management_System.Model.ComplaintsRepo
{
    public interface IComplaintsRepository
    {
        List<Complaints> GetMyComplaints(string name);
        List<Complaints> GetAllComplaints(int id);

        Task<Complaints> AddComplaints(Complaints complaints);

        Task<Complaints> UpdateComplaints(Complaints complaints , int id);



    }
}
