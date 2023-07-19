using LibraryMgmt.EFCore.Service.DTOs;
using LibraryMgmt.EFCore.Domain.Interfaces;
using LibraryMgmt.EFCore.Service.Services.Interfaces;

namespace LibraryMgmt.EFCore.Service.Services
{
    public class StaffCrudService : IStaffCrudService
    {
        private IStaffRepository _staffRepository;
        public StaffCrudService(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }
        public async Task<ServiceResponse<StaffDTO>> GetStaff(int id)
        { 
            var response=new ServiceResponse<StaffDTO>();
            var staff =await _staffRepository.GetStaffbyId(id);
            if (staff != null)
            {
                response.Data= new StaffDTO()
                {
                    StaffName = staff.StaffName,
                    StaffRole=staff.StaffRole
                };
                response.Success= true;
                response.Message = "All staffs fetched";
            }
            else
            {
                response.Data = new StaffDTO();
                response.Success= false;
                response.Message = "No staffs found";
            }
            return response;
        }

        public async Task<ServiceResponse<IEnumerable<StaffDTO>>> GetAll()
        {
            var response = new ServiceResponse<IEnumerable<StaffDTO>>();
            var staffs=await _staffRepository.GetAll();
            if (staffs.Count() != 0)
            {
                response.Data= staffs.Select(
                    x => new StaffDTO()
                    {
                        StaffName = x.StaffName,
                        StaffRole = x.StaffRole
                    });
                response.Success = true;
                response.Message = "staff details fetched";
            }
            else
            {
                response.Data = Enumerable.Empty<StaffDTO>();
                response.Success = false;
                response.Message = "No staffs found";

            }
            return response;
        }
    }
}
