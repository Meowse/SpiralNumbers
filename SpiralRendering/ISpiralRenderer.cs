using System.IO;
using Spirals;

namespace SpiralRendering
{
    public interface ISpiralRenderer
    {
        void Render(Spiral spiral, TextWriter writer);
    }
}