using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Erp.Modules.HRM.Enums
{
    public enum Gender
    {
        [EnumMember(Value = "MALE")]
        Male = 1,
        [EnumMember(Value = "FEMALE")]
        Female = 2,
        [EnumMember(Value = "OTHER")]
        Other = 3
    }
}
