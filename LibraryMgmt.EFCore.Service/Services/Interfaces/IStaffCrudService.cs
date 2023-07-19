using LibraryMgmt.EFCore.Service.DTOs;

namespace LibraryMgmt.EFCore.Service.Services.Interfaces
{
    public interface IStaffCrudService
    {
        Task<ServiceResponse<StaffDTO>> GetStaff(int id);

        Task<ServiceResponse<IEnumerable<StaffDTO>>> GetAll();
    }
}
