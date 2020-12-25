using UnityEngine;

namespace ArtificialFarm.FarmMap
{
    public readonly struct Size
    {
        public int Width { get; }
        public int Height { get; }

        public bool LoopByX { get; }

        public bool LoopByY { get; }

        public int Summary => Width * Height;

        public Size(int w, int h, bool loopX = false, bool loopY = false)
        {
            Width = w;
            Height = h;
            LoopByX = loopX;
            LoopByY = loopY;
        }

        public Size(Vector2Int size, bool loopX = false, bool loopY = false)
            : this(size.x, size.y, loopX, loopY)
        {
        }
    }
}