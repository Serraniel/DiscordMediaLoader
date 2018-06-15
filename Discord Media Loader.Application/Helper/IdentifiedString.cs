using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DML.Application.Helper
{
    internal class IdentifiedString<T>
    {
        internal T Id { get; set; }
        internal string Caption { get; set; }
        
        internal IdentifiedString(T id, string caption)
        {
            Id = id;
            Caption = caption;
        }

        public override string ToString() => Caption;
    }
}
