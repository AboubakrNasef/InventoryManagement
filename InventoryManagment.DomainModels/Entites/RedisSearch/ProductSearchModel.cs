using Redis.OM.Modeling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.RedisSearch
{
    [Document(StorageType = StorageType.Json, Prefixes = ["ProductSearchModel"])]
    public class ProductSearchModel
    {
        [RedisIdField]
        public int Id { get; set; }
        [Searchable]
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
