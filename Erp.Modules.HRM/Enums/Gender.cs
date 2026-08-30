using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Erp.Modules.HRM.Enums
{
    public enum Gender
    {
        [EnumMember(Value = "MALE")]
        Male,
        [EnumMember(Value = "FEMALE")]
        Female
    }
}
