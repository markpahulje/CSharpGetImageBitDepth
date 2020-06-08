# CSharpGetImageBitDepth
Get image "Bit Depth" or specifically color depth .NET 40 imp

This sample solution illustrates the shortcomings of standard windows image library
and uses a third party SharpDX lib to that seems to work consistently, in regards to getting an
image format, and secondly the "bit depth" of an image. 

This is custom solution is provided for bits per pixel for an image which is really report bits per channel, but commonly misinterpreted.

There is a great deal of confusion, for the general public regarding the expectation what bit depth is,
it loosely refers to number of bits used in an image versus number of bits used per each channel or color.
When referring to a pixel, the concept can be defined as bits per pixel (bpp),
which specifies the number of bits used. When referring to a color component, 
the concept can be defined as bits per component, bits per channel, bits per color 
(all three abbreviated bpc), and also bits per pixel component, bits per color channel 
or bits per sample (bps).

See - https://en.wikipedia.org/wiki/Color_depth 

