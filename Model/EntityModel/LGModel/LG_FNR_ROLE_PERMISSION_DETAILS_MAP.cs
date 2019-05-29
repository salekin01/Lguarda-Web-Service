using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.EntityModel.LGModel
{
    [DataContract]
    public class LG_FNR_ROLE_PERMISSION_DETAILS_MAP
    {
        [DataMember]
        public string PERMISSION_ID { get; set; }
        [DataMember]
        public string PERMISSION_DETAILS { get; set; }
        [DataMember]
        public string AUTH_STATUS_ID { get; set; }
        [DataMember]
        public string LAST_ACTION { get; set; }
        [DataMember]
        public DateTime? LAST_UPDATE_DT { get; set; }
        [DataMember(IsRequired=false, EmitDefaultValue=false)]
        public DateTime MAKE_DT { get; set; }
        [DataMember]
        public string FUNCTION_ID { get; set; }
    }
}
