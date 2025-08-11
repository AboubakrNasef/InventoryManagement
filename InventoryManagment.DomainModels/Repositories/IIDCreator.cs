using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagment.DomainModels.Repositories
{
    public interface IIDCreator
    {
        Guid CreateId();
    }
}
