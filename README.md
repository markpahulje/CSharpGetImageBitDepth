# CSharpGetImageBitDepth

Gets the colloquial "Bit Depth" of an image, but technically gets the bits per channel.

This sample solution illustrates the shortcomings of standard windows image library
and uses a third party SharpDX lib to get image pixel format, and thus the "bit depth" of an image. 
It seems to be the only library that works consistently, in regards to getting the correct info for an image.

System.Windows.Media and System.Drawing report different pixel formats for same image and incorrectly for 16-bit images in particular.

This is custom solution is provided for bits per pixel for an image which is really report bits per channel, but commonly misinterpreted. See below.

Motivation

There is a great deal of confusion, for the general public regarding the expectation what bit depth is,
it loosely refers to number of bits used in an image versus number of bits used per each channel or color.
When referring to a pixel, the concept can be defined as bits per pixel (bpp),
which specifies the number of bits used. When referring to a color component, 
the concept can be defined as bits per component, bits per channel, bits per color 
(all three abbreviated bpc), and also bits per pixel component, bits per color channel 
or bits per sample (bps).

Refer to https://en.wikipedia.org/wiki/Color_depth for full digest.

This is Visual Studio 2010 project using .NET 4.0 (for max upgrade-ability) and the latest SharpDX lib 4.2.0.
