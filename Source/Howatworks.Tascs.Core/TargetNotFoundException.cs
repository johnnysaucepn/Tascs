﻿using System;

namespace Howatworks.Tascs.Core
{
    class TargetNotFoundException : Exception
    {
        private readonly string _name;

        public TargetNotFoundException(string name)
        {
            _name = name;
        }

        public override string Message
        {
            get { return string.Format("Target '{0}' was not found", _name); }
        }
    }
}
