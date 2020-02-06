using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLibrary.Models
{
    public class Role : ModelBase
    {
        public Role(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
