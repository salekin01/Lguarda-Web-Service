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
    
    public partial class LG_USER_FILES
    {
        public decimal FILE_ID { get; set; }
        public short FILE_TYPE { get; set; }
        public string USER_ID { get; set; }
        public byte[] DATA { get; set; }
        public string AUTH_STATUS_ID { get; set; }
        public string LAST_ACTION { get; set; }
        public Nullable<System.DateTime> LAST_UPDATE_DT { get; set; }
        public System.DateTime MAKE_DT { get; set; }
        public string FILE_NAME { get; set; }
        public string CONTENT_TYPE { get; set; }
    }
}
