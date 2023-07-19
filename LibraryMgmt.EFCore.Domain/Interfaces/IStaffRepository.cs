using LibraryMgmt.EFCore.Domain.Models;

namespace LibraryMgmt.EFCore.Domain.Interfaces
{
    public interface IStaffRepository:IGenericRepository<Staff>
    {
        Task<Staff> GetStaffbyId(int id);
    }
}
