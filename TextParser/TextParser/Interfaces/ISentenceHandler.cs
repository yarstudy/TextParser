using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextParser.Interfaces
{
    public interface ISentenceHandler : ICountElements
    {
        List<IWordHandler> Words { get; }
        bool SentenceIsInterrogative { get; }
        char Separator { get; }
    }
}
