using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.InfraStructure.IntegrationTest.Repositories.Mongo.Fixtures.Fakers
{
    public interface IFaker<T>
    {
        List<T> Generate(int count);
        T Generate();
    }
}
