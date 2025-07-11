﻿using Society_Management_System.Model.Dto_s;

namespace Society_Management_System.Model.BillsRepo
{
    public interface IBillsRepository
    {
        Task<List<Bills>> GetAllBills(int id);

        Task<List<Bills>> GetMyBills(string name);

        Task<Bills> AddBill(BillsDto bills);

        Task<bool> UpdateBill(BillsDto bills , int id);
        Task<bool> PayBill( int id);

        Task<bool> DeleteBill(int id);
    }
}
