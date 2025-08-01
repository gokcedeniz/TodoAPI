using Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TNote : TBase
    {
        //TODO: Notlar kısmını yap
        public string Title { get; set; }           
        public string Content { get; set; }
        public string Category { get; set; }
    }
}
