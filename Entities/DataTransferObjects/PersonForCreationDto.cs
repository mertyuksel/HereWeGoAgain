using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class PersonForCreationDto
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string From { get; set; }
    }
}
