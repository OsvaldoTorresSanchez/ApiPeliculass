﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Favorito
    {
            public string media_type { get; set; }
            public int media_id { get; set; }
            public bool favorite { get; set; }

            public List<object> favoritos { get; set; }

    }
}
