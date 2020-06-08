using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SharpDX.DXGI;
using SharpDX.WIC;

namespace SharpDXGetBitDepth
{
    //Source - https://stackoverflow.com/questions/46500477/how-do-i-use-a-sharpdx-wic-bitmap-in-a-wpf-application
    //         https://docs.microsoft.com/en-us/archive/blogs/dwayneneed/implementing-a-custom-bitmapsource
    
    //.net 4.0 reduced for max portability 
    public class WicBitmapSource : System.Windows.Media.Imaging.BitmapSource, IDisposable
    {
        public WicBitmapSource(string filePath)
        {
            if (filePath == null)
                //throw new ArgumentNullException(nameof(filePath)); //c#6.0
                throw new ArgumentException("file path is null " + filePath); 



            using (var fac = new ImagingFactory())
            {
                using (var dec = new SharpDX.WIC.BitmapDecoder(fac, filePath, DecodeOptions.CacheOnDemand))
                {
                    Frame = dec.GetFrame(0);
                }
            }
        }

        public WicBitmapSource(BitmapFrameDecode frame)
        {
            if (frame == null)
                //throw new ArgumentNullException(nameof(frame)); //c#6.0
                throw new ArgumentNullException("Null BitmapFrameDecode = " + frame.ToString());

            Frame = frame;
        }

        //c# 6.0 declerations
        //public BitmapFrameDecode Frame { get; }
        //public override int PixelWidth => Frame.Size.Width;
        //public override int PixelHeight => Frame.Size.Height;
        //public override double Height => PixelHeight;
        //public override double Width => PixelWidth;

        //public override double DpiX
        //{
        //    get
        //    {
        //        Frame.GetResolution(out double dpix, out double dpiy);
        //        return dpix;
        //    }
        //}

        //public override double DpiY
        //{
        //    get
        //    {
        //        Frame.GetResolution(out double dpix, out double dpiy);
        //        return dpiy;
        //    }
        //}

        public BitmapFrameDecode Frame { get; set; }
        //public override int PixelWidth => Frame.Size.Width;

        private int _PixelWidth;
        public override int PixelWidth
        {
            get { return _PixelWidth; }
            //set { _PixelWidth = value; }
        }
        //public override int PixelHeight => Frame.Size.Height;
        private int _PixelHeight;
        public override int PixelHeight
        {
            get { return _PixelHeight; }
            //set { _PixelHeight = value; }
        }
    
        //public override double Height => PixelHeight;
        //private double _dPixelHeight;
        //public override double doPixelHeight
        //{
        //    get { return _dPixelHeight; }
        //    set { _dPixelHeight = value; }
        //}
        ////public override double Width => PixelWidth;
        //private double _dPixelWidth;
        //public override double PixelWidth
        //{
        //    get { return _dPixelWidth; }
        //    set { _dPixelWidth = value; }
        //}
        
        private double _dpix;
        private double _dpiy;
    
        public override double DpiX
        {
            //set { _dpix = value; }
            get
            {  
                Frame.GetResolution(out _dpix, out _dpiy);
                return _dpix;
            }
        }

        public override double DpiY
        {
            //set { _dpiy = value; }
            get
            {
                Frame.GetResolution(out _dpix, out _dpiy);
                return _dpiy;
            }
        }



        /// <summary>
        /// PixelFormatEnum represents the format of the bits of an image or surface.
        /// </summary>
        //Source - https://referencesource.microsoft.com/#PresentationCore/Core/CSharp/System/Windows/Media/PixelFormat.cs
        //internal enum PixelFormatEnum
        //{
        //    /// <summary>
        //    /// Default: (DontCare) the format is not important
        //    /// </summary>
        //    Default = 0,

        //    /// <summary>
        //    /// Extended: the pixel format is 3rd party - we don't know anything about it.
        //    /// </summary>
        //    Extended = Default,

        //    /// <summary>
        //    /// Indexed1: Paletted image with 2 colors.
        //    /// </summary>
        //    Indexed1 = 0x1,

        //    /// <summary>
        //    /// Indexed2: Paletted image with 4 colors.
        //    /// </summary>
        //    Indexed2 = 0x2,

        //    /// <summary>
        //    /// Indexed4: Paletted image with 16 colors.
        //    /// </summary>
        //    Indexed4 = 0x3,

        //    /// <summary>
        //    /// Indexed8: Paletted image with 256 colors.
        //    /// </summary>
        //    Indexed8 = 0x4,

        //    /// <summary>
        //    /// BlackWhite: Monochrome, 2-color image, black and white only.
        //    /// </summary>
        //    BlackWhite = 0x5,

        //    /// <summary>
        //    /// Gray2: Image with 4 shades of gray
        //    /// </summary>
        //    Gray2 = 0x6,

        //    /// <summary>
        //    /// Gray4: Image with 16 shades of gray
        //    /// </summary>
        //    Gray4 = 0x7,

        //    /// <summary>
        //    /// Gray8: Image with 256 shades of gray
        //    /// </summary>
        //    Gray8 = 0x8,

        //    /// <summary>
        //    /// Bgr555: 16 bpp SRGB format
        //    /// </summary>
        //    Bgr555 = 0x9,

        //    /// <summary>
        //    /// Bgr565: 16 bpp SRGB format
        //    /// </summary>
        //    Bgr565 = 0xA,

        //    /// <summary>
        //    /// Gray16: 16 bpp Gray format
        //    /// </summary>
        //    Gray16 = 0xB,

        //    /// <summary>
        //    /// Bgr24: 24 bpp SRGB format
        //    /// </summary>
        //    Bgr24 = 0xC,

        //    /// <summary>
        //    /// BGR24: 24 bpp SRGB format
        //    /// </summary>
        //    Rgb24 = 0xD,

        //    /// <summary>
        //    /// Bgr32: 32 bpp SRGB format
        //    /// </summary>
        //    Bgr32 = 0xE,

        //    /// <summary>
        //    /// Bgra32: 32 bpp SRGB format
        //    /// </summary>
        //    Bgra32 = 0xF,

        //    /// <summary>
        //    /// Pbgra32: 32 bpp SRGB format
        //    /// </summary>
        //    Pbgra32 = 0x10,

        //    /// <summary>
        //    /// Gray32Float: 32 bpp Gray format, gamma is 1.0
        //    /// </summary>
        //    Gray32Float = 0x11,

        //    /// <summary>
        //    /// Bgr101010: 32 bpp Gray fixed point format
        //    /// </summary>
        //    Bgr101010 = 0x14,

        //    /// <summary>
        //    /// Rgb48: 48 bpp RGB format
        //    /// </summary>
        //    Rgb48 = 0x15,

        //    /// <summary>
        //    /// Rgba64: 64 bpp extended format; Gamma is 1.0
        //    /// </summary>
        //    Rgba64 = 0x16,

        //    /// <summary>
        //    /// Prgba64: 64 bpp extended format; Gamma is 1.0
        //    /// </summary>
        //    Prgba64 = 0x17,

        //    /// <summary>
        //    /// Rgba128Float: 128 bpp extended format; Gamma is 1.0
        //    /// </summary>
        //    Rgba128Float = 0x19,

        //    /// <summary>
        //    /// Prgba128Float: 128 bpp extended format; Gamma is 1.0
        //    /// </summary>
        //    Prgba128Float = 0x1A,

        //    /// <summary>
        //    /// PABGR128Float: 128 bpp extended format; Gamma is 1.0
        //    /// </summary>
        //    Rgb128Float = 0x1B,

        //    /// <summary>
        //    /// CMYK32: 32 bpp CMYK format.
        //    /// </summary>
        //    Cmyk32 = 0x1C
        //}
        /// <summary>
        /// Gets bits per channel strictly speaking, but is this commonly confused as bits per pixel and we'll use this expectation
        /// </summary>
        /// <param name="pixelFormat">System.Windows.Media.PixelFormat</param>
        /// <returns>int bits per channel</returns>
        /// <author>Mark Pahulje, MetadataConsulting.ca</author>
        /// <created>Sun 07-Jun-20 1:27am</created>
        //  https://en.wikipedia.org/wiki/Color_depth
        //  get from source - https://referencesource.microsoft.com/#PresentationCore/Core/CSharp/System/Windows/Media/PixelFormat.cs
        public int GetBitsPerPixelakaChannel(System.Windows.Media.PixelFormat pixelFormat)
        {
            
            //var x = (System.Windows.Media.PixelFormat)pixelformat;  //no conversion available

            string strPF = pixelFormat.ToString(); 

            switch (strPF)
            {
                case "Default": 
                    return -1; //Gets the pixel format that is best suited for the particular operation, or here not found!

                case "Indexed1":
                    return 1; //Gets the pixel format specifying a paletted bitmap with 2 colors.

                case "Indexed2":
                    return 2; //Gets the pixel format specifying a paletted bitmap with 4 colors.

                case "Indexed4":
                    return 4; //Gets the pixel format specifying a paletted bitmap with 16 colors.

                case "Indexed8":
                    return 8; //Gets the pixel format specifying a paletted bitmap with 256 colors.

                case "BlackWhite":
                    return 1; //Gets the black and white pixel format which displays one bit of data per pixel as either black or white.

                case "Gray2":
                    return 2; //Gets the Gray2 pixel format which displays a 2 bits-per-pixel grayscale channel, allowing 4 shades of gray.

                case "Gray4":
                    return 4; //Gets the Gray4 pixel format which displays a 4 bits-per-pixel grayscale channel, allowing 16 shades of gray.

                case "Gray8":
                    return 8; //Gets the Gray8 pixel format which displays an 8 bits-per-pixel grayscale channel, allowing 256 shades of gray.

                case "Gray16":
                    return 16; //Gets the Gray16 pixel format which displays a 16 bits-per-pixel grayscale channel, allowing 65536 shades of gray. This format has a gamma of 1.0.

                case "Gray32Float":
                    return 32; //Gets the Gray32Float pixel format. Gray32Float displays a 32 bits per pixel (BPP) grayscale channel, allowing over 4 billion shades of gray. This format has a gamma of 1.0.

                case "Bgr555":
                    return 5; //Gets the Bgr555 pixel format. Bgr555 is a sRGB format with 16 bits per pixel (BPP). Each color channel (blue, green, and red) is allocated 5 bits per pixel (BPP).

                case "Bgr565"://Note: Hard to choose a good value here, unless we return a decimal perhaps 5.65 then
                    return 6; //Gets the Bgr565 pixel format. Bgr565 is a sRGB format with 16 bits per pixel(BPP).Each color channel(blue, green, and red) is allocated 5, 6, and 5 bits per pixel(BPP) respectively.

                case "Bgr101010":
                    return 10; //Gets the Bgr101010 pixel format. Bgr101010 is a sRGB format with 32 bits per pixel (BPP). Each color channel (blue, green, and red) is allocated 10 bits per pixel (BPP).

                case "Bgr24":
                    return 8; //Gets the Bgr24 pixel format. Bgr24 is a sRGB format with 24 bits per pixel (BPP). Each color channel (blue, green, and red) is allocated 8 bits per pixel (BPP).

                case "Rgb24":
                    return 8; //Gets the Rgb24 pixel format. Rgb24 is a sRGB format with 24 bits per pixel (BPP). Each color channel (red, green, and blue) is allocated 8 bits per pixel (BPP).

                case "Bgr32":
                    return 8; //Gets the Bgr32 pixel format. Bgr32 is a sRGB format with 32 bits per pixel (BPP). Each color channel (blue, green, and red) is allocated 8 bits per pixel (BPP).

                case "Bgra32":
                    return 8; //Gets the Bgra32 pixel format. Bgra32 is a sRGB format with 32 bits per pixel (BPP). Each channel (blue, green, red, and alpha) is allocated 8 bits per pixel (BPP).

                case "Pbgra32":
                    return 8; //Gets the Pbgra32 pixel format. Pbgra32 is a sRGB format with 32 bits per pixel (BPP). Each channel (blue, green, red, and alpha) is allocated 8 bits per pixel (BPP). Each color channel is pre-multiplied by the alpha value.

                case "Rgb48":
                    return 16; //Gets the Rgb48 pixel format. Rgb48 is a sRGB format with 48 bits per pixel (BPP). Each color channel (red, green, and blue) is allocated 16 bits per pixel (BPP). This format has a gamma of 1.0.

                case "Rgba64":
                    return 16; //Gets the Rgba64 pixel format. Rgba64 is an sRGB format with 64 bits per pixel (BPP). Each channel (red, green, blue, and alpha) is allocated 16 bits per pixel (BPP). This format has a gamma of 1.0.

                case "Prgba64":
                    return 32; //Gets the Prgba64 pixel format. Prgba64 is a sRGB format with 64 bits per pixel (BPP). Each channel (blue, green, red, and alpha) is allocated 32 bits per pixel (BPP). Each color channel is pre-multiplied by the alpha value. This format has a gamma of 1.0.

        
                case "Rgb128Float":
                    return 32; //Gets the Rgb128Float pixel format. Rgb128Float is a ScRGB format with 128 bits per pixel (BPP). Each color channel is allocated 32 BPP. This format has a gamma of 1.0.

                case "Rgba128Float":
                    return 32; //Gets the Rgba128Float pixel format. Rgba128Float is a ScRGB format with 128 bits per pixel (BPP). Each color channel is allocated 32 bits per pixel (BPP). This format has a gamma of 1.0.

                case "Prgba128Float":
                    return 32; //Gets the Prgba128Float pixel format. Prgba128Float is a ScRGB format with 128 bits per pixel (BPP). Each channel (red, green, blue, and alpha) is allocated 32 bits per pixel (BPP). Each color channel is pre-multiplied by the alpha value. This format has a gamma of 1.0.

                case "Cmyk32":
                    return 8; //Gets the Cmyk32 pixel format which displays 32 bits per pixel (BPP) with each color channel (cyan, magenta, yellow, and black) allocated 8 bits per pixel (BPP).
            }

            return -1; 
        }

        //We need this to get Pixel Format 
        public override System.Windows.Media.PixelFormat Format
        {
            get
            {
                // this is a hack as PixelFormat is not public...
                // it would be better to do proper matching
                var ct = typeof(System.Windows.Media.PixelFormat).GetConstructor(
                    BindingFlags.Instance | BindingFlags.NonPublic,
                    null,
                    new[] { typeof(Guid) },
                    null);
                return (System.Windows.Media.PixelFormat)ct.Invoke(new object[] { Frame.PixelFormat });
            }
        }

        // mostly for GIFs support (indexed palette of 256 colors)
        public override BitmapPalette Palette
        {
            get
            {
                using (var fac = new ImagingFactory())
                {
                    var palette = new Palette(fac);
                    try
                    {
                        Frame.CopyPalette(palette);
                    }
                    catch
                    {
                        // no indexed palette (PNG, JPG, etc.)
                        // it's a pity SharpDX throws here,
                        // it would be better to return null more gracefully as this is not really an error
                        // if you only want to support non indexed palette images, just return null for the property w/o trying to get a palette
                        return null;
                    }

                    var list = new List<Color>();
                    foreach (var c in palette.GetColors<int>())
                    {
                        var bytes = BitConverter.GetBytes(c);
                        var color = Color.FromArgb(bytes[3], bytes[2], bytes[1], bytes[0]);
                        list.Add(color);
                    }
                    return new BitmapPalette(list);
                }
            }
        }

        public override void CopyPixels(Int32Rect sourceRect, Array pixels, int stride, int offset)
        {
            if (offset != 0)
                throw new NotSupportedException();

            Frame.CopyPixels(
                new SharpDX.Mathematics.Interop.RawRectangle(sourceRect.X, sourceRect.Y, sourceRect.Width, sourceRect.Height),
                (byte[])pixels, stride);
        }

        //public void Dispose() => Frame.Dispose(); //c#6.0+
        public void Dispose()
        {
            Frame.Dispose(); 
        }

        //NOT REQUIRED FOR OUR EXAMPLE
        //public override event EventHandler<ExceptionEventArgs> DecodeFailed;
        //public override event EventHandler DownloadCompleted;
        //public override event EventHandler<ExceptionEventArgs> DownloadFailed;
        //public override event EventHandler<DownloadProgressEventArgs> DownloadProgress;
        

        //protected override Freezable CreateInstanceCore() => throw new NotImplementedException(); //c#6.0+
        protected override Freezable CreateInstanceCore()
        {
            throw new NotImplementedException();
        }

    }

}
