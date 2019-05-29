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
    
    public partial class LG_FNR_APPLICATION
    {
        public LG_FNR_APPLICATION()
        {
            this.LG_2FA_OTP_CONFIG = new HashSet<LG_2FA_OTP_CONFIG>();
            this.LG_2FA_TOKEN_GEN_LOG = new HashSet<LG_2FA_TOKEN_GEN_LOG>();
            this.LG_SYS_MAIL_SERVER_CONFIG = new HashSet<LG_SYS_MAIL_SERVER_CONFIG>();
            this.LG_FNR_SERVICE = new HashSet<LG_FNR_SERVICE>();
        }
    
        public string APPLICATION_ID { get; set; }
        public string APPLICATION_NAME { get; set; }
        public string AUTH_STATUS_ID { get; set; }
        public string LAST_ACTION { get; set; }
        public Nullable<System.DateTime> LAST_UPDATE_DT { get; set; }
        public System.DateTime MAKE_DT { get; set; }
        public Nullable<int> APP_TYPE_ID { get; set; }
    
        public virtual ICollection<LG_2FA_OTP_CONFIG> LG_2FA_OTP_CONFIG { get; set; }
        public virtual ICollection<LG_2FA_TOKEN_GEN_LOG> LG_2FA_TOKEN_GEN_LOG { get; set; }
        public virtual ICollection<LG_SYS_MAIL_SERVER_CONFIG> LG_SYS_MAIL_SERVER_CONFIG { get; set; }
        public virtual LG_FNR_APPLICATION_TYPE LG_FNR_APPLICATION_TYPE { get; set; }
        public virtual ICollection<LG_FNR_SERVICE> LG_FNR_SERVICE { get; set; }
    }
}
