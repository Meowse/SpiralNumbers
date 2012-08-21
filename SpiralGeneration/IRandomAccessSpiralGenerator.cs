namespace SpiralGeneration
{
    public interface IRandomAccessSpiralGenerator : ISpiralGenerator
    {
        int GetValueAt(int x, int y);
    }
}