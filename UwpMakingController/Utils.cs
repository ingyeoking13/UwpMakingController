using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Storage;

namespace UwpMakingController.util
{
    public static class Utils
    {

        static readonly Random random = new Random();

        public static Random Random
        {
            get { return random; }
        }

        /// <summary>
        /// vector2 limit It's Size
        /// </summary>
        /// <param name="vector2"></param>
        /// <param name="designatedSize"></param>
        public static Vector2 limit(this Vector2 vector2, int designatedSize)
        {
            Vector2 ret = new Vector2() { X = vector2.X, Y = vector2.Y };

            if (ret.Length() > designatedSize)
            {
                float len = ret.Length();
                ret /= len;
                ret *= designatedSize;
            }
            return ret;
        }

        public static float constrain(float num, float lower, float upper)
        {
            if (num < lower) num = lower;
            if (num > upper) num = upper;
            return num;
        }

        public static float RandomBetween(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }


        /*
        extra but they are not used In System1 Project
         */ 

        public static float DegreesToRadians(float angle)
        {
            return angle * (float)Math.PI / 180;
        }

        public static async Task<byte[]> ReadAllBytes(string filename)
        {
            var uri = new Uri("ms-appx:///" + filename);
            var file = await StorageFile.GetFileFromApplicationUriAsync(uri);
            var buffer = await FileIO.ReadBufferAsync(file);

            return buffer.ToArray();
        }

        public static async Task<T> TimeoutAfter<T>(this Task<T> task, TimeSpan timeout)
        {
            if (task == await Task.WhenAny(task, Task.Delay(timeout)))
            {
                return await task;
            }
            else
            {
                throw new TimeoutException();
            }
        }

        public struct WordBoundary { public int Start; public int Length; }

        public static List<WordBoundary> GetEveryOtherWord(string str)
        {
            List<WordBoundary> result = new List<WordBoundary>();

            for (int i = 0; i < str.Length; ++i)
            {
                if (str[i] == ' ')
                {
                    int nextSpace = str.IndexOf(' ', i + 1);
                    int limit = nextSpace == -1 ? str.Length : nextSpace;

                    WordBoundary wb = new WordBoundary();
                    wb.Start = i + 1;
                    wb.Length = limit - i - 1;
                    result.Add(wb);
                    i = limit;
                }
            }
            return result;
        }

    }
}
