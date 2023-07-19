using LibraryMgmt.EFCore.DataAccess.LibDbContext;
using LibraryMgmt.EFCore.Domain.Interfaces;
using LibraryMgmt.EFCore.Domain.Models;
using LibraryMgmt.EFCore.Domain.Repository;

namespace LibraryMgmt.EFCore.DataAccess.Repositories
{
    public class StaffRepository:GenericRepository<Staff>, IStaffRepository
    {
        public StaffRepository(LibraryDbContext context): base(context)
        {
            
        }

        public async Task<Staff> GetStaffbyId(int id)
        {
            var staff =await _libraryDbContext.Staffs.FindAsync(id);
            if (staff != null)
            {
                return staff;
            }
            return null;
        }
    }
}
