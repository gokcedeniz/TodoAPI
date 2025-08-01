using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Base
{
    public class TBase
    {
        public TBase()
        {
            IsDeleted = false;
            Id = Guid.NewGuid();
            CreatedDateTime = DateTime.UtcNow; 
        }
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
