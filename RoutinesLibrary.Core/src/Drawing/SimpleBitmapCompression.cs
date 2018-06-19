using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutinesLibrary.Core.Data;

namespace RoutinesLibrary.Core.Drawing
{
    class SimpleBitmapCompression
    {
        public enum Version
        {
            auto,
            B1,
            B2
        }


        public static byte[] Compress(Bitmap bitmap, Version version = Version.auto)
        {
            byte[] byteImage = ImageToBytes(bitmap);

            int indexImage = BitConverter.ToInt32(byteImage, 10);
            int widthImage = BitConverter.ToInt32(byteImage, 18);
            int heightImage = BitConverter.ToInt32(byteImage, 22);

            byte[] dataImage = ArrayHelper.SubArray(byteImage, indexImage, byteImage.Length - indexImage);
            dataImage = ReflectImage(dataImage, widthImage, heightImage);
            List<byte> compressImage = new List<byte>();

            ushort escape = 0;
            byte[] simpleOneData = new byte[0];
            byte[] simpleTwoData = new byte[0];

            // calculate data
            if (version == Version.B1 || version == Version.auto)
            {
                simpleOneData = Simple_One(dataImage);
            }
            if (version == Version.B2 || version == Version.auto)
            {
                escape = EscapeColor(dataImage);
                simpleTwoData = Simple_Two(dataImage, escape);
            }

            // get correct version
            if (simpleOneData.Length != 0 && simpleTwoData.Length != 0)
            {
                version = (simpleOneData.Length < simpleTwoData.Length) ? Version.B1 : Version.B2;
            }
            else
            {
                version = (simpleOneData.Length != 0) ? Version.B1 : Version.B2;
            }

            // image header
            compressImage.AddRange(Encoding.ASCII.GetBytes(version.ToString())); // version
            compressImage.AddRange(BitConverter.GetBytes(widthImage)); // width
            compressImage.AddRange(BitConverter.GetBytes(heightImage)); // height

            // add data
            if (version == Version.B1)
            {
                compressImage.AddRange(simpleOneData); // data
            }
            else if (version == Version.B2)
            {
                compressImage.AddRange(BitConverter.GetBytes(escape)); // escape character
                compressImage.AddRange(simpleTwoData); // data
            }

            return compressImage.ToArray();
        }


        #region Compress

        private static byte[] Simple_One(byte[] byteImage)
        {
            List<byte> dataImage = new List<byte>();

            if (byteImage.Length > 0)
            {
                ushort prevColor = BitConverter.ToUInt16(byteImage, 0);
                ushort color = BitConverter.ToUInt16(byteImage, 0);
                ushort countColor = 1;

                for (int i = 2; i < byteImage.Length; i += 2)
                {
                    color = BitConverter.ToUInt16(byteImage, i);

                    if (color == prevColor)
                    {
                        countColor++;
                    }
                    else
                    {
                        dataImage.AddRange(BitConverter.GetBytes(prevColor));
                        dataImage.AddRange(BitConverter.GetBytes(countColor));

                        prevColor = color;
                        countColor = 1;
                    }
                }

                // add last color value
                dataImage.AddRange(BitConverter.GetBytes(color));
                dataImage.AddRange(BitConverter.GetBytes(countColor));
            }

            return dataImage.ToArray();
        }

        private static byte[] Simple_Two(byte[] byteImage, ushort escape)
        {
            List<byte> dataImage = new List<byte>();

            if (byteImage.Length > 0)
            {
                ushort prevColor = BitConverter.ToUInt16(byteImage, 0);
                ushort color = BitConverter.ToUInt16(byteImage, 0);
                ushort countColor = 1;

                for (int i = 2; i < byteImage.Length; i += 2)
                {
                    color = BitConverter.ToUInt16(byteImage, i);

                    if (color == prevColor)
                    {
                        countColor++;
                    }
                    else
                    {
                        if (prevColor == escape)
                        {
                            dataImage.AddRange(BitConverter.GetBytes(escape));
                            dataImage.AddRange(BitConverter.GetBytes(prevColor));
                            dataImage.AddRange(BitConverter.GetBytes(countColor));
                        }
                        else if (countColor > 3)
                        {
                            dataImage.AddRange(BitConverter.GetBytes(escape));
                            dataImage.AddRange(BitConverter.GetBytes(prevColor));
                            dataImage.AddRange(BitConverter.GetBytes(countColor));
                        }
                        else
                        {
                            for (int j = 0; j < countColor; j++)
                            {
                                dataImage.AddRange(BitConverter.GetBytes(prevColor));
                            }
                        }

                        prevColor = color;
                        countColor = 1;
                    }
                }

                // add last color value
                if (color == escape)
                {
                    dataImage.AddRange(BitConverter.GetBytes(escape));
                    dataImage.AddRange(BitConverter.GetBytes(color));
                    dataImage.AddRange(BitConverter.GetBytes(countColor));
                }
                else if (countColor > 3)
                {
                    dataImage.AddRange(BitConverter.GetBytes(escape));
                    dataImage.AddRange(BitConverter.GetBytes(color));
                    dataImage.AddRange(BitConverter.GetBytes(countColor));
                }
                else
                {
                    for (int j = 0; j < countColor; j++)
                    {
                        dataImage.AddRange(BitConverter.GetBytes(color));
                    }
                }
            }

            return dataImage.ToArray();
        }

        #endregion


        #region Utils

        private static byte[] ImageToBytes(Bitmap bitmap)
        {
            MemoryStream stream = new MemoryStream();
            bitmap.Save(stream, ImageFormat.Bmp);

            return stream.GetBuffer();
        }

        private static ushort EscapeColor(byte[] byteImage)
        {
            ushort escape = 0;

            // get all the reps
            Hashtable escapeHashtable = new Hashtable();

            for (int i = 0; i < byteImage.Length; i += 2)
            {
                if (escapeHashtable.Contains(BitConverter.ToUInt16(byteImage, i)))
                {
                    int reps = 1 + (int)escapeHashtable[BitConverter.ToUInt16(byteImage, i)];
                    escapeHashtable[BitConverter.ToUInt16(byteImage, i)] = reps;
                }
                else
                {
                    escapeHashtable.Add(BitConverter.ToUInt16(byteImage, i), 1);
                }
            }

            // get an unused color
            List<ushort> usedColors = escapeHashtable.Keys.Cast<ushort>().ToList();
            bool found = false;

            for (ushort c = ushort.MinValue; c < ushort.MaxValue; c++)
            {
                if (!usedColors.Contains(c))
                {
                    escape = c;
                    found = true;
                    break;
                }
            }

            // get the least used color
            if (!found)
            {
                int escapeRep = int.MaxValue;

                foreach (DictionaryEntry escapeEntry in escapeHashtable)
                {
                    if ((int)escapeEntry.Value < escapeRep)
                    {
                        escapeRep = (int)escapeEntry.Value;
                        escape = (ushort)escapeEntry.Key;
                    }
                }
            }

            return escape;
        }

        private static byte[] ReflectImage(byte[] dataImage, int widthImage, int heightImage)
        {
            byte[] result = new byte[dataImage.Length];

            for (int i = 0; i < heightImage; i++)
            {
                for (int j = 0; j < widthImage; j++)
                {
                    Console.WriteLine((i * widthImage + j) + "-->" + ((heightImage - i - 1) * widthImage + j));
                    result[(heightImage - i - 1) * widthImage + j] = dataImage[i * widthImage + j];
                }
            }

            return result;
        }

        #endregion
    }
}
