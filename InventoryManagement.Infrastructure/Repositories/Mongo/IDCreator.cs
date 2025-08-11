using InventoryManagment.DomainModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories.Mongo
{
    public class IDCreator : IIDCreator
    {
        public Guid CreateId()
        {
            return Guid.NewGuid();
        }
    }

}
