using Model.EDMX;
using Model.EntityModel.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace Model.EntityModel.LGModel
{
   public class LG_SYS_SESSION_TRACKER_MAP
    {
       [DataMember]
        public string USER_ID { get; set; }
       [DataMember]
         public string SESSION_ID { get; set; }
       [DataMember]
         public DateTime? START_TIME { get; set; }
       [DataMember]
         public DateTime? LAST_ACCESS_TIME { get; set; }
       [DataMember]
         public string IP_ADDRESS { get; set; }
       [DataMember]

         public short ACTIVE_FLAG_MULTI_LOGIN { get; set; }
       [DataMember]
         public string REMARKS { get; set; }
       [DataMember]
       public DateTime? SESSION_DT { get; set; }

       [DataMember]
       public string APPLICATION_ID { get; set; }

    }
}
