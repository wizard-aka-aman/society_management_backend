﻿using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.ComplaintsRepo
{
    public interface IComplaintsRepository
    {
        List<Complaints> GetMyComplaints(string name);
        List<Complaints> GetAllComplaints(int id);

        Task<Complaints> AddComplaints(ComplaintsDto complaints);

        Task<Complaints> UpdateComplaints(ComplaintsDto complaints , int id);

        Task<bool> DeleteComplaints(int id);

        Task<int> TotalComplaints(int id);
        Task<int> TotalCompletedComplaints(int id);
        Task<int> MyComplaintsNumber(string name);
        Task<int> MyCompletedComplaintsNumber(string name);



    }
}
