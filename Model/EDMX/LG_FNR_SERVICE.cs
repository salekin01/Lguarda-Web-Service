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
    
    public partial class LG_FNR_SERVICE
    {
        public string SERVICE_ID { get; set; }
        public string SERVICE_NM { get; set; }
        public string SERVICE_SH_NM { get; set; }
        public string APPLICATION_ID { get; set; }
        public string AUTH_STATUS_ID { get; set; }
        public string LAST_ACTION { get; set; }
        public Nullable<System.DateTime> LAST_UPDATE_DT { get; set; }
        public System.DateTime MAKE_DT { get; set; }
    
        public virtual LG_FNR_APPLICATION LG_FNR_APPLICATION { get; set; }
    }
}
