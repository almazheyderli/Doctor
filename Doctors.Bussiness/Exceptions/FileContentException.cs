﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doctors.Bussiness.Exceptions
{
    public  class FileContentException:Exception
    {
        public FileContentException( string propertyName,string? message) : base(message)
        {
            PropertyName = propertyName;
        }

        public string PropertyName { get; set; }
    }
}
