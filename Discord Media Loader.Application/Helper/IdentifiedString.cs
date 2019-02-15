#region LICENSE
/**********************************************************************************************
 * Copyright (C) 2017-2019 - All Rights Reserved
 * 
 * This file is part of "DML.Application".
 * The official repository is hosted at https://github.com/Serraniel/DiscordMediaLoader
 * 
 * "DML.Application" is licensed under Apache 2.0.
 * Full license is included in the project repository.
 * 
 * Users who edited IdentifiedString.cs under the condition of the used license:
 * - Serraniel (https://github.com/Serraniel)
 **********************************************************************************************/
#endregion

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
