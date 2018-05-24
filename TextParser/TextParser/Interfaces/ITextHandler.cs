﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextParser.Interfaces
{
    public interface ITextHandler : ICountElements
    {
        List<ISentenceHandler> Sentences { get;}
    }
}
