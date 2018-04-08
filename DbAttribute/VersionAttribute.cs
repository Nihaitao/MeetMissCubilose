using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DbAttribute
{
    public class VersionAttribute : Attribute
    {
        public string Name { get; set; }
    }
}
