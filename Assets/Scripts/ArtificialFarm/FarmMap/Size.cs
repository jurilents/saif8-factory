using UnityEngine;

namespace ArtificialFarm.FarmMap
{
    public readonly struct Size
    {
        public int Width { get; }
        public int Height { get; }

        public bool LoopByX { get; }

        public bool LoopByY { get; }

<<<<<<< HEAD
        public int Summary => Width * Height;

=======
>>>>>>> parent of 7347170... v0.1.0

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