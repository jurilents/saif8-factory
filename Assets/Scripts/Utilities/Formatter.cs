using System;
using UnityEngine;

namespace Utilities
{
    public static class Formatter
    {
        #region Custom convertors

        /// <summary>
        /// Convert 255 to FFF
        /// </summary>
        /// <param name="value">Number between 0 -> 255</param>
        /// <returns>00-FF hex-format value</returns>
        public static string DecToHex(byte value) => value.ToString("X2");


        /// <summary>
        /// Convert FFF to 255
        /// </summary>
        /// <param name="hex"></param>
        /// <returns>Number between 0 -> 255</returns>
        public static byte HexToDec(string hex) => (byte) Convert.ToInt32(hex, 16);


        /// <summary>
        /// Convert 1.0 to FFF
        /// </summary>
        /// <param name="value">Value between 0 -> 1</param>
        /// <returns>A hex string based on a value</returns>
        public static string Dec01ToHex(float value) => DecToHex((byte) Mathf.Round(value * 255f));


        /// <summary>
        /// Convert FFF to 1.0
        /// </summary>
        /// <param name="hex">One byte hex string like "F3"</param>
        /// <returns></returns>
        public static float HexToDec01(string hex) => HexToDec(hex) / 255f;


        /// <summary>
        /// To get Color from hex string like "F3F3F3"
        /// </summary>
        /// <param name="color">Hex string like "F3F3F3"</param>
        public static Color HexToColor(string color)
        {
            var (r, g, b, a) = ParseHexColor(color);
            return new Color(r, g, b, a);
        }

        #endregion


        #region Color extention methods

        /// <summary>
        /// Convert Color to FFF
        /// </summary>
        /// <returns></returns>
        public static string ToHexString(this Color self)
        {
            string red = Dec01ToHex(self.r);
            string green = Dec01ToHex(self.g);
            string blue = Dec01ToHex(self.b);
            string alpha = self.a < 1f ? Dec01ToHex(self.a) : "";
            return red + green + blue + alpha;
        }


        /// <summary>
        /// To get Color from Hex string FFF
        /// </summary>
        /// <param name="self">Color struct instance</param>
        /// <param name="color">Hex string like "F3F3F3"</param>
        public static Color FromHex(this Color self, string color)
        {
            (self.r, self.g, self.b, self.a) = ParseHexColor(color);
            return self;
        }

        #endregion


        #region Prive helper tools

        private static (float r, float g, float b, float a) ParseHexColor(string color)
        {
            float red = HexToDec(color.Substring(0, 2));
            float green = HexToDec(color.Substring(2, 2));
            float blue = HexToDec(color.Substring(4, 2));
            // Color string contains alpha
            float alpha = color.Length >= 8 ? HexToDec(color.Substring(6, 2)) : 1f;

            return (red, green, blue, alpha);
        }

        #endregion
    }
}