using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Model.EntityModel.LGModel
{
    [DataContract]
    public class Test
    {
        [DataMember]
        string p1 { set; get; }

        [DataMember]
        string p2 { set; get; }
    }
}
