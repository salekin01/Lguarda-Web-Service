//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.EDMX
{
    using System;
    using System.Collections.Generic;
    
    public partial class LG_FNR_ROLE_PERMISSION_DETAILS
    {
        public string PERMISSION_ID { get; set; }
        public string PERMISSION_DETAILS { get; set; }
        public string AUTH_STATUS_ID { get; set; }
        public string LAST_ACTION { get; set; }
        public Nullable<System.DateTime> LAST_UPDATE_DT { get; set; }
        public System.DateTime MAKE_DT { get; set; }
        public string FUNCTION_ID { get; set; }
        public short FUNCTION_PERMISSION_FLAG { get; set; }
    
        public virtual LG_FNR_FUNCTION LG_FNR_FUNCTION { get; set; }
    }
}
