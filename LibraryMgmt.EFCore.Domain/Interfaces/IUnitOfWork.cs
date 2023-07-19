using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryMgmt.EFCore.Domain.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IBookRepository Books { get; }
        IStaffRepository Staffs { get; }  
        ICustomerRepository Customers { get; }
        IOrderRepository Orders { get; }
        Task<int> Complete();
    }
}
