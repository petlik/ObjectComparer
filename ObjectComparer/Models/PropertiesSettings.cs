﻿using System;
using System.Collections.Generic;

namespace ObjectComparer
{
    class PropertiesSettings
    {
        public string Name { get; set; }
        public Type Type { get; set; }
        public List<ComparerFlags> Flags { get; set; }
    }
}
