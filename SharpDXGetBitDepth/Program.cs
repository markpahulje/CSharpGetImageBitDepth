using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO; 
using Bitmap = System.Drawing.Bitmap;

namespace SharpDXGetBitDepth
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();

            //*************************************************
            //This sample solution illustratrates the shortcommings of starndard windows image library
            //and illustrate a third party approach that seems to work consistently, in regards to getting an
            //image format, and secondly the bit depth. 
            //
            //A custom solution is provided for bits per pixel for an image which is really report bits per channel. 
            //*************************************************

            //*************************************************
            //There is a great deal of confusion, for the general public regarding the expectatation what bit depth is,
            //it loosely refers to number of bits used in an image versus number of bits used per each chanel or color.
            //When referring to a pixel, the concept can be defined as bits per pixel (bpp),
            //which specifies the number of bits used. When referring to a color component, 
            //the concept can be defined as bits per component, bits per channel, bits per color 
            //(all three abbreviated bpc), and also bits per pixel component, bits per color channel 
            //or bits per sample (bps).
            //*************************************************
            //https://en.wikipedia.org/wiki/Color_depth 


            //http://www.schaik.com/pngsuite/pngsuite_bas_png.html - test pictures
            //string filename = "basn0g01.png"; //basn0g01 - black & white  correct

            //string filename = "basn0g16.png"; //basn0g16 16 bit(64k level) grayscale //WRONG - but SHARPDX WIX correct

            //string filename = "basn3p04.png"; //basn3p04 - 4 bit (16 color) paletted - CORRECT
            //Indexed4 correct identified as 4 bits, in 1432684 ticks - super slow - NOT A METADATA READ

            string picturePath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures); 

            string filename = "basn4a16.png"; //basn4a16 - 16 bit grayscale + 16 bit alpha-channel  //WRONG - but SHARPDX WIX correct
            //Rgba64 is an sRGB format with 64 bits per pixel (BPP). 
            //Each channel (red, green, blue, and alpha) is allocated 16 bits per pixel (BPP). 
            //This format has a gamma of 1.0.
            //https://docs.microsoft.com/en-us/dotnet/api/system.windows.media.pixelformats.rgba64?view=netcore-3.1#System_Windows_Media_PixelFormats_Rgba64

            //string path = @"C:\Users\Markus\Pictures\8_bit.png";

            //Going to try to use SHARPDX to get the correct pixel format

            string path = Path.Combine(picturePath, filename); 

            var thisbmp = new WicBitmapSource(path);
           
            Console.WriteLine("SharpDX implementation of WIC, which produces the most consistently accurate PixelFormat value");
            Console.WriteLine(path);
            Console.WriteLine("WIC width = " + thisbmp.Width + " heigth = " + thisbmp.Height);
            //Console.WriteLine("WIC DPIX = " + thisbmp.DpiX);
            //Console.WriteLine("WIC DPIY = " + thisbmp.DpiY);

            Console.WriteLine("WIC PixelFormat = " + thisbmp.Format);
            Console.WriteLine("WIC Bit Per Pixel (bpp) = " + thisbmp.Format.BitsPerPixel);
            //Sat 06-Jun-20 2:56pm  - metadataconsulting.ca fix
            Console.WriteLine("\nMDC's Bit Per Pixel (bpp)  = " + thisbmp.GetBitsPerPixelakaChannel(thisbmp.Format)+"\n<<< MetadataConsulting.ca hack >>>\n"); //this inherites form media
            // Create a PixelFormat object.
            System.Windows.Media.PixelFormat mediaPixelFormat = new System.Windows.Media.PixelFormat(); //convert sharp dx to media pixel format
            mediaPixelFormat = thisbmp.Format;
            int mediaBPP = mediaPixelFormat.BitsPerPixel; //is wrong reports 64 


            Console.WriteLine("Converted to System.Windows.Media, PixelFormat = " + mediaPixelFormat); //now coverted to media 
            Console.WriteLine("Converted to System.Windows.Media, Bit Per Pixel (bpp) = " + mediaBPP); //this inherites from media


            //https://docs.microsoft.com/en-us/dotnet/api/system.drawing.imaging.pixelformat
            //var x = System.Drawing.Image.GetPixelFormatSize(thisbmp.Format); //not compatible

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds.ToString("#,0") + " ms  --- downside, very slow");
            Console.WriteLine();
            sw.Reset();
            sw.Start();

            System.Drawing.Image img = System.Drawing.Image.FromFile(path, true);
            Int32 imgBPP = System.Drawing.Image.GetPixelFormatSize(img.PixelFormat); 
            //basn4a16 reports Format32bppArgb which is WRONG
            //Format32bppArgb Specifies that the format is 32 bits per pixel; 
            //8 bits each are used for the alpha, red, green, and blue components. 

            Console.WriteLine("New System.Drawing.Image Dimensions = " + img.PhysicalDimension);
            Console.WriteLine("New System.Drawing.Image Sniffed PixelFormat = " + img.PixelFormat);
            Console.WriteLine("New System.Drawing.Image Bit Per Pixel (bpp) = " + imgBPP);


            //https://docs.microsoft.com/en-us/dotnet/api/system.drawing.imaging.pixelformat?view=dotnet-plat-ext-3.1
            //https://referencesource.microsoft.com/#System.Drawing/commonui/System/Drawing/Advanced/PixelFormat.cs,73040ebf67c92de1,references

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds.ToString("#,0") + " ms");
            Console.WriteLine();
            sw.Reset();
            sw.Start();

            //var imagebbp = picSample.GetHbitmap.bitsPerPixel; 

            BitmapImage source = new BitmapImage(new System.Uri(path));
            int bitsPerPixel = source.Format.BitsPerPixel;

            //source.GetPixelFormatSize(bitmap.PixelFormat); 
            Console.WriteLine("New System.Windows.Media.Image Sniffed Format = " + source.Format); 
            //basn4a16 reports Pbgra32 which is WRONG
            //Pbgra32 is a sRGB format with 32 bits per pixel(BPP).
            //Each channel(blue, green, red, and alpha) is allocated 8 bits per pixel(BPP).
            //Each color channel is pre - multiplied by the alpha value.

            Console.WriteLine("System.Windows.Media.Image Bit Per Pixel (bpp) = " + bitsPerPixel);    //wrong

            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds.ToString("#,0") + " ms");


            Console.ReadLine(); 

        }
    }
}
