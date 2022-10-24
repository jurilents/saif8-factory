using System;
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

		public AxisFuncs HF { get; }

		public AxisFuncs WF { get; }


		public Size(int w, int h, bool loopX = false, bool loopY = false)
		{
			if (w == 0 || h == 0) throw new ArgumentOutOfRangeException();

			Width = w;
			Height = h;

			LoopByX = loopX;
			LoopByY = loopY;

			HF = new AxisFuncs(Height);
			WF = new AxisFuncs(Width);
		}

		public Size(Vector2Int size, bool loopX = false, bool loopY = false)
				: this(size.x, size.y, loopX, loopY) { }


		public class AxisFuncs
		{
			private readonly float _limit;

			public AxisFuncs(float limit)
			{
				_limit = limit;
			}


			public float Linear(float x) => x / _limit;
			public float InverseLinear(float x) => Linear(_limit - x);


			// public float Sigmoid(float x, float antiSmooth = 1, float xOffset = 1) =>
			//     1 / (1 + Mathf.Exp(-(x / _limit * antiSmooth) + xOffset));

			// public float InverseSigmoid(float x, float antiSmooth = 1, float xOffset = 1) =>
			//     Sigmoid(_limit - x, antiSmooth, xOffset);


			// public float ReLU(float x, float min) => Mathf.Max(0, (x / _limit) - _limit * min);
			// public float InverseReLU(float x, float min) => ReLU(_limit - x, min);


			// public float Atan(float x, float kx = 1, float ky = 1) => Mathf.Atan(x / _limit - kx) + ky;
			// public float InverseAtan(float x, float kx = 1, float ky = 1) => Atan(_limit - x, kx, ky);

			public float ClampedReLU(float x, float min, float max, float minBase = 0f, float maxBase = 1f)
			{
				x /= _limit;
				if (x > max) return maxBase;
				if (x < min) return minBase;
				return (maxBase - minBase) * (x - min) / (max - min) + minBase;
			}

			public float InverseClampedReLU(float x, float min, float max, float minBase = 0f, float maxBase = 1f) =>
					ClampedReLU(_limit - x, min, max, minBase, maxBase);
		}
	}
}