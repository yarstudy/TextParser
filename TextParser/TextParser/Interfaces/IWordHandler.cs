﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextParser.Interfaces
{
    public interface IWordHandler : ICountElements
    {
        bool FirstLetterIsConsonant { get; }
    }
}
