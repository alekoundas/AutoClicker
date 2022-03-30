using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace AutoClicker
{
    class Engine2
    {
        //public Point? Find(Bitmap haystack, Bitmap needle)
        //{
        //    if (null == haystack || null == needle)
        //        return null;

        //    if (haystack.Width < needle.Width || haystack.Height < needle.Height)
        //        return null;

        //    byte[][] haystackArray = GetPixelArray(haystack);
        //    byte[][] needleArray = GetPixelArray(needle);


        //    haystackArray = ConvertToHalfResolution((Image)haystack);
        //    needleArray = ConvertToHalfResolution((Image)needle);

        //    haystackArray = CleanPixelArray(haystackArray);
        //    needleArray = CleanPixelArray(needleArray);

        //    SavePixelArray(haystackArray, haystackArray.Length, haystackArray.First().Length, "CleanedScreenshot");
        //    SavePixelArray(needleArray, needleArray.Length, needleArray.First().Length, "CleanedAction");

        //    var maxHits = 0;
        //    var pointX = 0;
        //    var pointY = 0;

        //    foreach (var firstLineMatchPoint in FindMatch(haystackArray, needleArray[0]))
        //    {

        //        Console.WriteLine(firstLineMatchPoint);
        //        var hits = IsNeedlePresentAtLocation(haystackArray, needleArray, firstLineMatchPoint);

        //        if (maxHits < hits)
        //        {
        //            maxHits = hits;
        //            pointX = firstLineMatchPoint.X;
        //            pointY = firstLineMatchPoint.Y;
        //        }


        //        var maxPossibleHits = needle.Width * needle.Height * 4;
        //        var successRate = (maxHits * 100) / maxPossibleHits;
        //        Console.WriteLine("Hits:" + maxHits + " out of " + maxPossibleHits);
        //        Console.WriteLine(successRate + "%");
        //        if (maxHits == 0)
        //        {
        //            return null;
        //        }
        //    }
        //    return new Point(pointX / 4, pointY);
        //}
        public Point? Find(Bitmap haystack, Bitmap needle)
        {
            if (null == haystack || null == needle)
                return null;

            if (haystack.Width < needle.Width || haystack.Height < needle.Height)
                return null;

            byte[][] haystackArray = GetPixelArray(haystack, out var haystackStride);
            byte[][] needleArray = GetPixelArray(needle, out var needleStride);


            //haystackArray = ConvertToHalfResolution((Image)haystack);
            //needleArray = ConvertToHalfResolution((Image)needle);

            //haystackArray = CleanPixelArray(haystackArray);
            //needleArray = CleanPixelArray(needleArray);

            SavePixelArray(haystackArray, haystackStride, "CleanedScreenshot");
            SavePixelArray(needleArray, needleStride, "CleanedAction");


            var firstLineMatchPoint = FindMatch(haystackArray, needleArray[0], needleArray.Length);

            Console.WriteLine(firstLineMatchPoint);
            var hits = IsNeedlePresentAtLocation(haystackArray, needleArray, firstLineMatchPoint);


            var maxPossibleHits = needle.Width * needle.Height;
            var successRate = (hits * 100) / maxPossibleHits;
            Console.WriteLine("Hits:" + hits + " out of " + maxPossibleHits);
            Console.WriteLine(successRate + "%");
            return new Point(firstLineMatchPoint.X , firstLineMatchPoint.Y);
        }



        //private byte[][] ConvertToHalfResolution(Image array)
        //{
        //    return GetPixelArray(new Bitmap(array, new Size(array.Width / 4, array.Height / 4)));
        //}

        private byte[][] CleanPixelArray(byte[][] array)
        {

            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array[i].Length; j++)
                    if (array[i][j] >0)
                        array[i][j] = 255;

            return array;
        }


        private byte[][] GetPixelArray(Bitmap bitmap, out int stride)
        {
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format1bppIndexed);
            var result = new byte[bitmap.Height][];
            var byteArray = new byte[(bitmapData.Width * bitmapData.Height)];

            stride = bitmapData.Stride;


            Marshal.Copy(bitmapData.Scan0, byteArray, 0, bitmapData.Height * bitmapData.Stride);
            bitmap.UnlockBits(bitmapData);


            var count = 0;
            for (int y = 0; y < bitmapData.Height; y++)
            {
                result[y] = new byte[bitmapData.Width];
                for (int x = 0; x < bitmapData.Width; x++)
                {
                    result[y][x] = byteArray[count];
                    count++;
                }
            }

            return result;
        }


        private void SavePixelArray(byte[][] array, int stride, string fileName)
        {
            List<byte> arrayLst = new List<byte>();
            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array[i].Length; j++)
                    arrayLst.Add(array[i][j]);

            var bitmapArr = arrayLst.ToArray();
            unsafe
            {
                fixed (byte* ptr = bitmapArr)
                {
                    using (Bitmap image = new Bitmap(array.First().Length, array.Length, stride, PixelFormat.Format1bppIndexed, new IntPtr(ptr)))
                    {

                        image.Save(@"C:\Repos\AutoClicker\AutoClicker\Images\ScreenShot\" + fileName + ".png");
                    }
                }
            }
        }
        //private void SavePixelArray(byte[][] array, string fileName)
        //{
        //    List<byte> arrayLst = new List<byte>();
        //    for (int i = 0; i < array.Length; i++)
        //    {

        //        for (int j = 0; j < array[i].Length; j++)
        //        {
        //            arrayLst.Add(array[i][j]);
        //        }
        //    }
        //    var bitmapArr = arrayLst.ToArray();


        //    int columns = array.First().Length;
        //    int rows = array.Length;
        //    int stride = columns;
        //    byte[] newbytes = PadLines(bitmapArr, rows, columns);

        //    Bitmap image = new Bitmap(columns, rows, stride,
        //             PixelFormat.Format1bppIndexed,
        //             Marshal.UnsafeAddrOfPinnedArrayElement(newbytes, 0));

        //    image.Save(@"C:\Users\psych\source\repos\AutoClicker\AutoClicker\Images\ScreenShot\" + fileName + ".png");

        //}

        private byte[] PadLines(byte[] bytes, int rows, int columns)
        {
            int currentStride = columns; // 3
            int newStride = columns;  // 4
            byte[] newBytes = new byte[newStride * rows];
            for (int i = 0; i < rows; i++)
                Buffer.BlockCopy(bytes, currentStride * i, newBytes, newStride * i, currentStride);
            return newBytes;
        }





        //private IEnumerable<Point> FindMatch(byte[][] haystackLines, byte[] needleLine)
        //{
        //    var hits = 0;

        //    for (int y = 0; y < haystackLines.Length; y++)
        //        for (int x = 0; x < haystackLines[y].Length - needleLine.Length; x++)
        //            if (ContainSameElements(haystackLines[y], x, needleLine))
        //                yield return new Point(x, y);
        //}

        private Point FindMatch(byte[][] haystackLines, byte[] needleLine, int needleHeight)
        {
            var hits = 0;
            var maxHits = 0;
            int pointX = 0;
            int pointY = 0;
            for (int y = 0; y < haystackLines.Length- needleHeight; y++)
                for (int x = 0; x < haystackLines[y].Length - needleLine.Length; x++)
                {

                    hits = ContainSameElements(haystackLines[y], x, needleLine);
                    if (maxHits < hits)
                    {
                        maxHits = hits;
                        pointY = y;
                        pointX = x;
                    }
                }


            var maxPossibleHits = needleLine.Length* needleHeight;
            var successRate = (hits * 100) / maxPossibleHits;
            Console.WriteLine("Needle hits:" + hits + " out of " + maxPossibleHits + "       "+successRate + "%");
            return new Point(pointX, pointY);
        }



        private int ContainSameElements(byte[] haystackLine, int haystackLine_X, byte[] needleLine)
        {
            var hits = 0;
            for (int x = 0; x < needleLine.Length; x++)
                if (haystackLine[haystackLine_X + x] == needleLine[x])
                    hits++;
            return hits;

        }

        private int IsNeedlePresentAtLocation(byte[][] haystack, byte[][] needle, Point point)
        {
            var hits = 0;
            for (int y = 0; y < needle.Length; y++)
                for (int x = 1; x < needle[y].Length; x++)
                    if (haystack[point.Y + y][point.X + x] == needle[y][x])
                        hits++;

            return hits;
        }

    }
}
