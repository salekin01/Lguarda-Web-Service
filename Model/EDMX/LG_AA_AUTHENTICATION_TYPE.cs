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
    
    public partial class LG_AA_AUTHENTICATION_TYPE
    {
        public LG_AA_AUTHENTICATION_TYPE()
        {
            this.LG_USER_SETUP_PROFILE = new HashSet<LG_USER_SETUP_PROFILE>();
        }
    
        public string AUTHENTICATION_ID { get; set; }
        public string AUTHENTICATION_NAME { get; set; }
        public string AUTH_STATUS_ID { get; set; }
        public string LAST_ACTION { get; set; }
        public Nullable<System.DateTime> LAST_UPDATE_DT { get; set; }
        public System.DateTime MAKE_DT { get; set; }
    
        public virtual ICollection<LG_USER_SETUP_PROFILE> LG_USER_SETUP_PROFILE { get; set; }
    }
}
