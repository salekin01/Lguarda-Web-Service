using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.EntityModel.Common
{
    [Serializable]
    [DataContract]
    public class Bool_Check
    {
        [DataMember]
        public bool CHECK { get; set; }

        [DataMember]
        public string ERROR { get; set; }
    }
}
