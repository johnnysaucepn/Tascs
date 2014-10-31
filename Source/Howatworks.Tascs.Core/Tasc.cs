﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howatworks.Tascs.Core
{
    public abstract class Tasc
    {

        public abstract ITascResult Execute(TascTarget target);

        public virtual void Cleanup()
        {
            
        }
    }

}
