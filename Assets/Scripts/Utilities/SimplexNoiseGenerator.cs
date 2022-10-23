using UnityEngine;
using Random = System.Random;

namespace Utilities
{
    public class SimplexNoiseGenerator
    {
        #region Private Fields

        private readonly int[] A = new int[3];
        private readonly int[] T;

        private float s, u, v, w;
        private int i, j, k;

        private float onethird = 1f / 3f;
        private float onesixth = 1f / 6f;

        #endregion


        #region Constructor

        public SimplexNoiseGenerator()
        {
            if (T != null) return;
            var rand = new Random();
            T = new int[8];
            for (int q = 0; q < 8; q++)
                T[q] = rand.Next();
        }

        /// <summary>
        /// Seed example: {0x16, 0x38, 0x32, 0x2c, 0x0d, 0x13, 0x07, 0x2a}
        /// </summary>
        public SimplexNoiseGenerator(string seed)
        {
            T = new int[8];
            var seed_parts = seed.Split(' ');

            for (int q = 0; q < 8; q++)
                T[q] = int.TryParse(seed_parts[q], out int temp) ? temp : 0x0;
        }

        public SimplexNoiseGenerator(int[] seed)
        {
            T = seed;
        }

        #endregion


        #region Public

        public string GetSeed()
        {
            string seed = "";

            for (int q = 0; q < 8; q++)
            {
                seed += T[q].ToString();
                if (q < 7)
                    seed += " ";
            }

            return seed;
        }

        public float GetCoherentNoise(float x, float y, float z, int octaves = 1, int multiplier = 25,
            float amplitude = 0.5f, float lacunarity = 2, float persistence = 0.9f)
        {
            Vector3 v3 = new Vector3(x, y, z) / multiplier;
            float val = 0;
            for (int n = 0; n < octaves; n++)
            {
                val += GetNoise(v3.x, v3.y, v3.z) * amplitude;
                v3 *= lacunarity;
                amplitude *= persistence;
            }

            return val;
        }

        public int GetDensity(Vector3 loc)
        {
            float val = GetCoherentNoise(loc.x, loc.y, loc.z);
            return (int) Mathf.Lerp(0, 255, val);
        }


        /// <summary>
        /// Simplex Noise Generator
        /// </summary>
        public float GetNoise(Vector3 p) => GetNoise(p.x, p.y, p.z);


        /// <summary>
        /// Simplex Noise Generator
        /// </summary>
        public float GetNoise(float x, float y, float z)
        {
            s = (x + y + z) * onethird;
            i = FastFloor(x + s);
            j = FastFloor(y + s);
            k = FastFloor(z + s);

            s = (i + j + k) * onesixth;
            u = x - i + s;
            v = y - j + s;
            w = z - k + s;

            A[0] = 0;
            A[1] = 0;
            A[2] = 0;

            int hi = u >= w ? u >= v ? 0 : 1 : v >= w ? 1 : 2;
            int lo = u < w ? u < v ? 0 : 1 : v < w ? 1 : 2;

            return Kay(hi) + Kay(3 - hi - lo) + Kay(lo) + Kay(0);
        }

        #endregion


        #region Private

        private float Kay(int a)
        {
            s = (A[0] + A[1] + A[2]) * onesixth;
            float x = u - A[0] + s;
            float y = v - A[1] + s;
            float z = w - A[2] + s;

            float t = 0.6f - x * x - y * y - z * z;
            int h = Shuffle(i + A[0], j + A[1], k + A[2]);
            A[a]++;

            if (t < 0) return 0;
            int b5 = h >> 5 & 1;
            int b4 = h >> 4 & 1;
            int b3 = h >> 3 & 1;
            int b2 = h >> 2 & 1;
            int b1 = h & 3;

            float p = b1 == 1 ? x : b1 == 2 ? y : z;
            float q = b1 == 1 ? y : b1 == 2 ? z : x;
            float r = b1 == 1 ? z : b1 == 2 ? x : y;

            p = b5 == b3 ? -p : p;
            q = b5 == b4 ? -q : q;
            r = b5 != (b4 ^ b3) ? -r : r;
            t *= t;
            return 8 * t * t * (p + (b1 == 0 ? q + r : b2 == 0 ? q : r));
        }

        private int Shuffle(int it, int jt, int kt) =>
            Bar(it, jt, kt, 0) + Bar(jt, kt, it, 1) +
            Bar(kt, it, jt, 2) + Bar(it, jt, kt, 3) +
            Bar(jt, kt, it, 4) + Bar(kt, it, jt, 5) +
            Bar(it, jt, kt, 6) + Bar(jt, kt, it, 7);


        private int Bar(int it, int jt, int kt, int B) => T[Bar(it, B) << 2 | Bar(jt, B) << 1 | Bar(kt, B)];
        private int Bar(int N, int B) => N >> B & 1;

        private static int FastFloor(float n) => n > 0 ? (int) n : (int) n - 1;

        #endregion
    }
}