﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Functions.Services.Messages
{
    public class CreatedMessage : BaseMessage
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string UserId { get; set; }
    }
}
