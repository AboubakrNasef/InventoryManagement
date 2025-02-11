using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTO
{
    public record ProductDto(int Id, string Name, string description, int Quantity, int Price);
}
