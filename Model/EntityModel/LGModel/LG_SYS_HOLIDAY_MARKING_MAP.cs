using Model.EDMX;
using Model.EntityModel.Common;
using Model.Mapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Model.EntityModel.LGModel
{
  public  class LG_SYS_HOLIDAY_MARKING_MAP
    {
       [DataMember]
        public short SL_NO { get; set; }
       [DataMember]
        public short CLD_TYPE_ID { get; set; }
       [DataMember]
        public short HOLIDAY_ID { get; set; }
       [DataMember]
        public Nullable<System.DateTime> DATE_FROM { get; set; }
       [DataMember]
        public Nullable<System.DateTime> DATE_TO { get; set; }
       [DataMember]
        public string MAKE_BY { get; set; }
       [DataMember]
       public DateTime? MAKE_DT { get; set; }
       [DataMember]
        public string AUTH_STATUS_ID { get; set; }
       [DataMember]
        public string LAST_ACTION { get; set; }
       [DataMember]
        public DateTime? LAST_UPDATE_DT { get; set; }



    }
}
