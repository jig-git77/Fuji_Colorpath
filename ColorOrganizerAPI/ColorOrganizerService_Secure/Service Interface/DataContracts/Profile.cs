using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace FujifilmUSA.ColorOrganizerService.DataContracts
{
    [DataContract]
    public class Profile
    {
        [DataMember]
        public byte[] ProfileData { get; set; }
    }
}
