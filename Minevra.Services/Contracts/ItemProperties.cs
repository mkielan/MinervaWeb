﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Minevra.Services.Contracts
{
    [DataContract]
    public class ItemProperties
    {

        [DataMember]
        public string name;
    }
}