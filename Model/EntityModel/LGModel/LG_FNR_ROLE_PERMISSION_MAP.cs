using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.EntityModel.LGModel
{
    [DataContract]
    public class LG_FNR_ROLE_PERMISSION_MAP
    {
        [DataMember]
        public string SL_ID { get; set; }
        [DataMember]
        public string ROLE_ID { get; set; }
        [DataMember]
        public string PERMISSION_ID { get; set; }
        [DataMember]
        public string AUTH_STATUS_ID { get; set; }
        [DataMember]
        public string LAST_ACTION { get; set; }
        [DataMember]
        public DateTime? LAST_UPDATE_DT { get; set; }
        [DataMember]
        public DateTime MAKE_DT { get; set; }


        [DataMember]
        public string PERMISSION_DETAILS { get; set; }
    }
}
