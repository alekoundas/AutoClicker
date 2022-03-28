using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace AutoClicker
{
    class Engine
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

            byte[][] haystackArray = GetPixelArray(haystack);
            byte[][] needleArray = GetPixelArray(needle);


            //haystackArray = ConvertToHalfResolution((Image)haystack);
            //needleArray = ConvertToHalfResolution((Image)needle);

            //haystackArray = CleanPixelArray(haystackArray);
            //needleArray = CleanPixelArray(needleArray);

            SavePixelArray(haystackArray, haystackArray.Length, haystackArray.First().Length, "CleanedScreenshot");
            SavePixelArray(needleArray, needleArray.Length, needleArray.First().Length, "CleanedAction");


            var firstLineMatchPoint = FindMatch(haystackArray, needleArray[0]);

            Console.WriteLine(firstLineMatchPoint);
            var hits = IsNeedlePresentAtLocation(haystackArray, needleArray, firstLineMatchPoint);


            var maxPossibleHits = needle.Width * needle.Height * 4;
            var successRate = (hits * 100) / maxPossibleHits;
            Console.WriteLine("Hits:" + hits + " out of " + maxPossibleHits);
            Console.WriteLine(successRate + "%");
            return new Point(firstLineMatchPoint.X / 4, firstLineMatchPoint.Y);
        }



        private byte[][] ConvertToHalfResolution(Image array)
        {
            return GetPixelArray(new Bitmap(array, new Size(array.Width / 4, array.Height / 4)));
        }

        private byte[][] CleanPixelArray(byte[][] array)
        {

            for (int i = 0; i < array.Length; i++)
                for (int j = 0; j < array[i].Length; j += 4)

                    //filter
                    //if (Math.Abs(array[i][j] - array[i][j + 1]) <= 20 && Math.Abs(array[i][j] - array[i][j + 2]) <= 20 && Math.Abs(array[i][j + 2] - array[i][j + 1]) <= 20)
                    //{
                    //    array[i][j] = 0;
                    //    array[i][j + 1] = 0;
                    //    array[i][j + 2] = 0;
                    //    array[i][j + 3] = 0;
                    //}
                    // White color find 
                    if (array[i][j] > 170 && array[i][j + 1] > 170 && array[i][j + 2] > 170)
                    {
                        array[i][j] = 255;
                        array[i][j + 1] = 255;
                        array[i][j + 2] = 255;
                        array[i][j + 3] = 255;
                    }
                    // Black color find
                    else if (array[i][j] < 60 && array[i][j + 1] < 60 && array[i][j + 2] < 60)
                    {
                        array[i][j] = 0;
                        array[i][j + 1] = 0;
                        array[i][j + 2] = 0;
                        array[i][j + 3] = 255;
                    }
                    // RGB color find
                    else
                    {
                        var kappa = 0;
                        var max = 0;
                        for (int k = 0; k < 3; k++)
                            if (max < array[i][j + k])
                            {
                                max = array[i][j + k];
                                kappa = k;
                            }

                        for (int k = 0; k < 3; k++)
                            array[i][j + k] = 0;

                        array[i][j + kappa] = 255;
                        array[i][j + 3] = 255;
                    }

            return array;
        }


        private byte[][] GetPixelArray(Bitmap bitmap)
        {
            var result = new byte[bitmap.Height - 4][];
            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly,
                PixelFormat.Format1bppIndexed);

            for (int i = 0; i < bitmap.Height - 4; i++)
            {
                result[i] = new byte[(bitmap.Width)];
                Marshal.Copy(bitmapData.Scan0 + i * bitmapData.Stride, result[i], 0, result[i].Length);
            }

            bitmap.UnlockBits(bitmapData);

            return result;
        }


        private void SavePixelArray(byte[][] array, int height, int width, string fileName)
        {
            List<byte> arrayLst = new List<byte>();
            for (int i = 0; i < array.Length; i++)
            {

                for (int j = 0; j < array[i].Length; j++)
                {
                    arrayLst.Add(array[i][j]);
                }
            }

            var bitmapArr = arrayLst.ToArray();
            unsafe
            {
                fixed (byte* ptr = bitmapArr)
                {
                    using (Bitmap image = new Bitmap(width, height, width, PixelFormat.Format1bppIndexed, new IntPtr(ptr)))
                    {

                        image.Save(@"C:\Users\psych\source\repos\AutoClicker\AutoClicker\Images\ScreenShot\" + fileName + ".png");
                    }
                }
            }
        }






        //private IEnumerable<Point> FindMatch(byte[][] haystackLines, byte[] needleLine)
        //{
        //    var hits = 0;

        //    for (int y = 0; y < haystackLines.Length; y++)
        //        for (int x = 0; x < haystackLines[y].Length - needleLine.Length; x++)
        //            if (ContainSameElements(haystackLines[y], x, needleLine))
        //                yield return new Point(x, y);
        //}

        private Point FindMatch(byte[][] haystackLines, byte[] needleLine)
        {
            var hits = 0;
            var maxHits = 0;
            int pointX = 0;
            int pointY = 0;
            for (int y = 0; y < haystackLines.Length; y++)
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
            for (int y = 0; y < haystack.Length - needle.Length; y++)
                for (int x = 1; x < haystack[y].Length - needle[y].Length; x++)
                    if (haystack[point.Y + y][point.X + x] == needle[y][x])
                        hits++;

            return hits;
        }

    }
}
