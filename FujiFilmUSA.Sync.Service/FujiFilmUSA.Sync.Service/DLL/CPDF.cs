/*---------------------------------------------------------+
| DynaPDF 4.0                                              |
+----------------------------------------------------------+
| Copyright (C) 2003-2023                                  |
| Jens Boschulte, DynaForms GmbH.                          |
| All rights reserved.                                     |
+----------------------------------------------------------+
| Please report errors or other potential problems to      |
| support@dynaforms.com.                                   |
| The current version is available at www.dynaforms.com.   |
+----------------------------------------------------------|
|                Creation date: December 15, 2023          |
+----------------------------------------------------------*/

using System;
using System.Collections;
using System.Runtime.InteropServices;
using lbool = System.Int32;

[module: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "IDE1006:These words must begin with upper case characters", Scope = "module")]
[module: System.Diagnostics.CodeAnalysis.SuppressMessage("Reliability", "IDE0028:Collection initialization can be simplified", Scope = "module")]

namespace DynaPDF
{
   // Helper class
   class _Conv
   {
      static public String ToString(IntPtr Ptr, bool Unicode)
      {
         if (IntPtr.Zero.Equals(Ptr)) return null;
         if (!Unicode)
            return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(Ptr);
         else
            return System.Runtime.InteropServices.Marshal.PtrToStringUni(Ptr);
      }

      static public String ToString(IntPtr Ptr, int Len, bool Unicode)
      {
         if (IntPtr.Zero.Equals(Ptr) || Len <= 0) return null;
         if (!Unicode)
            return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(Ptr, Len);
         else
            return System.Runtime.InteropServices.Marshal.PtrToStringUni(Ptr, Len);
      }

      static public String ToString(IntPtr PtrA, IntPtr PtrW)
      {
         if (!IntPtr.Zero.Equals(PtrA))
            return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(PtrA);
         else if (!IntPtr.Zero.Equals(PtrW))
            return System.Runtime.InteropServices.Marshal.PtrToStringUni(PtrW);
         else
            return null;
      }

      static public String ToString(IntPtr PtrA, IntPtr PtrW, int Len)
      {
         if (!IntPtr.Zero.Equals(PtrA))
            return System.Runtime.InteropServices.Marshal.PtrToStringAnsi(PtrA, Len);
         else if (!IntPtr.Zero.Equals(PtrW))
            return System.Runtime.InteropServices.Marshal.PtrToStringUni(PtrW, Len);
         else
            return null;
      }
   }

   public enum T3DActivationType
   {
      at3D_AppDefault,
      at3D_PageOpen,    // The annotaiton should be activated when the page is opened.
      at3D_PageVisible, // The annotaiton should be activated when the page becomes visible.
      at3D_Explicit     // The annotation should remain inactive until explicitely activated by a script or action (default).
   }

   public enum T3DDeActivateType
   {
      dt3D_AppDefault,
      dt3D_PageClosed,    // The annotaiton should be deactivated as soon as the page is closed.
      dt3D_PageInvisible, // The annotaiton should be deactivated as soon as the page becomes invisible (default).
      dt3D_Explicit       // The annotation should remain active until explicitely deactivated by a script or action.
   }

   public enum T3DDeActInstance
   {
      di3D_AppDefault,
      di3D_UnInstantiated, // The annotation will be uninstantiated (default)
      di3D_Instantiated,   // The annotation is left instantiated
      di3D_Live            // If the 3D artwork contains an animation then it will stay live
   }

   public enum T3DInstanceType
   {
      it3D_AppDefault,
      it3D_Instantiated, // The annotation will be instantiated but animations are disabled.
      it3D_Live          // The annotation will be instantiated and animations are enabled (default).
   }

   public enum T3DLightingSheme
   {
      lsArtwork, // Lights from file
      lsBlue,
      lsCAD,
      lsCube,
      lsDay,
      lsHard,
      lsHeadlamp,
      lsNight,
      lsNoLights,
      lsPrimary,
      lsRed,
      lsWhite,
      lsNotSet
   }

   public enum T3DNamedAction
   {
      naDefault,
      naFirst,
      naLast,
      naNext,
      naPrevious
   }

   public enum T3DProjType
   {
      pt3DOrthographic,
      pt3DPerspective
   }

   public enum T3DRenderingMode
   {
      rmBoundingBox,
      rmHiddenWireframe,
      rmIllustration,
      rmShadedIllustration,
      rmShadedVertices,
      rmShadedWireframe,
      rmSolid,
      rmSolidOutline,
      rmSolidWireframe,
      rmTransparent,
      rmTranspBBox,
      rmTranspBBoxOutline,
      rmTranspWireframe,
      rmVertices,
      rmWireframe,
      rmNotSet
   }

   public enum T3DScaleType
   {
      st3DValue,
      st3DWidth,
      st3DHeight,
      st3DMin,
      st3DMax
   }

   public enum TActionType
   {
      atGoTo,
      atGoToR,
      atHide,
      atImportData,
      atJavaScript,
      atLaunch,
      atMovie,
      atNamed,
      atRendition,    // PDF 1.5
      atReset,        // ResetForm
      atSetOCGState,  // PDF 1.5
      atSound,
      atSubmit,       // SubmitForm
      atThread,
      atTransition,
      atURI,
      atGoTo3DView,   // PDF 1.6
      atGoToE,        // PDF 1.6 Like atGoToR but refers to an embedded PDF file.
      atRichMediaExec // PDF 1.7 Extension Level 3
   }

   public enum TAFRelationship
   {
      arAssociated,
      arData,
      arSource,
      arSupplement,
      arAlternative,
      arFormData
   }

   public enum TAFDestObject
   {
      adAnnotation,
      adCatalog,    // The documents catalog is the root object
      adField,
      adImage,
      adPage,
      adTemplate
   }

   public enum TAnnotColor
   {
      acBackColor,
      acBorderColor,
      acTextColor
   }

   public enum TAnnotFlags
   {
      afNone           = 0x00000000,
      afInvisible      = 0x00000001,
      afHidden         = 0x00000002,
      afPrint          = 0x00000004,
      afNoZoom         = 0x00000008,
      afNoRotate       = 0x00000010,
      afNoView         = 0x00000020,
      afReadOnly       = 0x00000040,
      afLocked         = 0x00000080,
      afToggleNoView   = 0x00000100,
      afLockedContents = 0x00000200
   }

   /*
      By default all annotations which have an appearance stream and which have the print flag set are flattened.
      All annotations are deleted when the function returns with the exception of file attachment annotations.
      If you want to flatten the view state then set the flag affUseViewState.
   */
   public enum TAnnotFlattenFlags
   {
      affNone               = 0x00000000, // Printable annotations independent of the type
      affUseViewState       = 0x00000001, // If set, annotations which are visible in a viewer become flattened.
      affMarkupAnnots       = 0x00000002, // If set, markup annotations are flattened only. Link, Sound, or FileAttach annotations are no markup annotations. These types will be left intact.
      affNonPDFA_1          = 0x00000004, // If set, flatten all annotations which are not supported in PDF/A 1.
      affNonPDFA_2          = 0x00000008, // If set, flatten all annotations which are not supported in PDF/A 2 or 3.
      affNonPDFA_4e         = 0x00000080, // If set, flatten all annotations which are not supported in PDF/A 4e
      affFormFields         = 0x00000010, // If set, form fields will be flattened too.
      affUseFieldViewState  = 0x00000020, // Meaningful only if affFormFields is set. If set, flatten the view state of form fields. Use the print state otherwise.
      affSigFields          = 0x00000040, // Meaningful only if affFormFields is not set. If set, signed signature fields will be flattened.

      affKeepLinkAnnots     = 0x00001000, // If set, link annotations will be kept.
      affKeepFileAttach     = 0x00002000, // If set, file attachment annotations will be kept.
      affKeepTextAnnots     = 0x00004000, // If set, text annotations will be kept.
      affKeepEmptySigFields = 0x00008000, // If set, signature fields which are not already signed will be kept.

      affPreserveZOrder     = 0x00400000  // If set, preserve the z-order under any curcumstances. That means visible overlapping annotations or form fields will be flattened, regardless the type.
   }

   public enum TAnnotIcon
   {
      aiComment,
      aiHelp,
      aiInsert,
      aiKey,
      aiNewParagraph,
      aiNote,
      aiParagraph
   }

   public enum TAnnotState
   {
      asNone,
      asAccepted,
      asRejected,
      asCancelled,
      asCompleted,
      asCreateReply // Don't add a migration state, create a reply instead. Set the contents of the reply with SetAnnotString().
   }

   public enum TAnnotString
   {
      asAuthor,
      asContent,
      asName,
      asSubject,
      asRichStyle, // Default style string. -> FreeText annotations only.
      asRichText   // Rich text string.     -> Supported by markup annotations.
   }

   public enum TAnnotType
   {
      atCaret,
      atCircle,
      atFileLink,    // A Link annotation with an associated GoToR action (go to remote)
      atFreeText,
      atHighlight,   // Highlight annotation
      atInk,
      atLine,
      atPageLink,    // A Link annotation with an associated GoTo action
      atPolygon,
      atPolyLine,
      atPopUp,
      atSquare,
      atSquiggly,    // Highlight annotation
      atStamp,
      atStrikeOut,   // Highlight annotation
      atText,        // Also used as container to store the State Model
      atUnderline,   // Highlight annotation
      atWebLink,     // A Link annotation with an associated URI action
      atWidget,      // Form Fields are handled separately
      at3D,          // PDF 1.6
      atSoundAnnot,  // PDF 1.2
      atFileAttach,  // PDF 1.3
      atRedact,      // PDF 1.7
      atWatermark,   // PDF 1.6
      atUnknown,     // Unknown annotation type
      atMovieAnnot,  // PDF 1.2
      atPrinterMark, // PDF 1.4
      atProjection,  // PDF 1.7 Extension Level 3
      atRichMedia,   // PDF 1.7 Extension Level 3
      atScreen,      // PDF 1.5
      atTrapNet      // PDF 1.3
   }

   public enum TBaseEncoding
   {
      beWinAnsi,
      beMacRoman,
      beMacExpert,
      beStandard
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TBBox
   {
      public float x1;
      public float y1;
      public float x2;
      public float y2;
   }

   public enum TBlendMode
   {
      bmNotSet,
      bmNormal,
      bmColor,
      bmColorBurn,
      bmColorDodge,
      bmDarken,
      bmDifference,
      bmExclusion,
      bmHardLight,
      bmHue,
      bmLighten,
      bmLuminosity,
      bmMultiply,
      bmOverlay,
      bmSaturation,
      bmScreen,
      bmSoftLight
   }

   public enum TDestType
   {
      dtXY_Zoom,     /* three parameters (a, b, c) -> (X, Y, Zoom)                    */
      dtFit,         /* no parameters                                                 */
      dtFitH_Top,    /* one parameter    (a)                                          */
      dtFitV_Left,   /* one parameter    (a)                                          */
      dtFit_Rect,    /* four parameters  (a, b, c, d) -> (left, bottom, right, top)   */
      dtFitB,        /* no parameters                                                 */
      dtFitBH_Top,   /* one parameter    (a)                                          */
      dtFitBV_Left   /* one parameter    (a)                                          */
   }

   public enum TBmkStyle
   {
      bmsNormal = 0,
      bmsItalic = 1,
      bmsBold   = 2
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TBookmark
   {
      public uint      Color;
      public int       DestPage;
      public TPDFRect  DestPos;
      public TDestType DestType;
      public bool      Open;
      public int       Parent;
      public TBmkStyle Style;
      public String    Title;
   }

   public enum TBorderEffect
   {
      beSolid,   // Default
      beCloudy1, // Circle diameter 9 units
      beCloudy2  // Circle diameter 17 units
   }

   public enum TBorderStyle
   {
      bsSolid,
      bsBevelled,
      bsInset,
      bsUnderline,
      bsDashed
   }

   public enum TBtnCaptionPos
   {
      bcpCaptionOnly,  // Default
      bcpImageOnly,    // No caption; image only
      bcpCaptionBelow, // Caption below the image
      bcpCaptionAbove, // Caption above the image
      bcpCaptionRight, // Caption on the right of the image
      bcpCaptionLeft,  // Caption on the left of the image
      bcpCaptionOver   // Caption overlaid directly on the image
   }

   public enum TButtonState
   {
      bsUp,
      bsDown,
      bsRollOver
   }

   public enum TCheckBoxChar
   {
      ccCheck,
      ccCircle,
      ccCross1,
      ccCross2,
      ccCross3,
      ccCross4,
      ccDiamond,
      ccSquare,
      ccStar
   }

   public enum TCheckBoxState
   {
      cbUnknown,
      cbChecked,
      cbUnChecked
   }

   public enum TCheckOptions : uint
   {
      coDefault                  = 0x0010FFFF,
      coEmbedSubsets             = 0x00000001,
      coDeleteTransferFuncs      = 0x00000002,
      coDeleteMultiMediaContents = 0x00000004,
      coDeleteActionsAndScripts  = 0x00000008,
      coDeleteInvRenderingIntent = 0x00000010,
      coFlattenFormFields        = 0x00000020,
      coReplaceV4ICCProfiles     = 0x00000040,
      coDeleteEmbeddedFiles      = 0x00000080,
      coDeleteOPIComments        = 0x00000100,
      coDeleteSignatures         = 0x00000200,
      coDeletePostscript         = 0x00000400, // Delete Postscript XObjects. Rarely used and such Postscript fragments are meaningful on a Postscript device only.
                                               // It is usually safe to delete such objects.
      coDeleteAlternateImages    = 0x00000800, // Alternate images are seldom used and prohibited in PDF/A.
      coReComprJPEG2000Images    = 0x00001000, // Recompression results usually in larger images. It is often better to keep such files as is.
      coResolveOverprint         = 0x00002000, // PDF/A 2 and 3. Set the overprint mode to 0 if overprint mode = 1 and if overprinting for fill or stroke is true
                                               // and if an ICCBased CMYK color space is used. Note that DeviceCMYK is treated as ICCBased color space due to implicit
                                               // color conversion rules.
      coMakeLayerVisible         = 0x00004000, // PDF/A 2 and 3 prohibit invisible layers. Layers can also be flattened if this is no option.
      coDeleteAppEvents          = 0x00008000, // PDF/A 2 and 3. Application events are prohibited in PDF/A. The view state will be applied.
      coReplCCITTFaxWithFlate    = 0x00010000, // Replace CCITT Fax compression with Flate.
      coApplyExportState         = 0x00020000, // Meaningful only if coDeleteAppEvents is set. Apply the export state.
      coApplyPrintState          = 0x00040000, // Meaningful only if coDeleteAppEvents is set. Apply the print state.
      coDeleteReplies            = 0x00080000, // Delete annotation replies. If absent, replies will be converted to regular text annotations.
      coDeleteHalftones          = 0x00100000, // Delete halftone screens.
      coFlattenLayers            = 0x00200000, // Flatten layers if any.
      coDeletePresentation       = 0x00400000, // Presentations are prohibited in PDF/A 2 and 3.
      coCheckImages              = 0x00800000, // If set, images will be decompressed to identify damaged images.
      coDeleteDamagedImages      = 0x01000000, // Meaningful only if coCheckImages is set.
      coRepairDamagedImages      = 0x02000000, // Meaningful only if coCheckImages is set. If set, try to recompress a damaged image. The new image is maybe not complete but error free.
      coNoFontEmbedding          = 0x10000000, // If this flag is set no valid PDF/A file will be produced!
      coFlushPages               = 0x20000000, // Write converted pages directly into the output file to reduce the memory usage.
      coAllowDeviceSpaces        = 0x40000000, // If set, device color spaces will not be replaced with ICC based color spaces. This flag is meaningful for normalization only.
      coResetAnnotAppearance     = 0x80000000, // If set, appearance streams of annotations are rebuild before executing CheckConformance().
      // Common default flags for normalization:
      coDefault_Normalize =       coAllowDeviceSpaces | coNoFontEmbedding,
      // Common default flags for different PDF/A versions:
      coDefault_PDFA_1           = coDefault | coFlattenLayers,               // The ability to flatten layers was added after coDefault was defined.
      coDefault_PDFA_2           = coDefault | coDeletePresentation,          // Presentations are prohibited in PDF/A 2 and PDF/A 3.
      coDefault_PDFA_3           = coDefault_PDFA_2 & ~coDeleteEmbeddedFiles, // Embedded files are allowed in PDF/A 3.
      coDefault_PDFA_4           = coDefault_PDFA_2 & ~coDeleteEmbeddedFiles  // Embedded files are allowed in PDF/A 4.
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TCIDMetric
   {
      public float Width;
      public float x;
      public float y;
   }

   public enum TClippingMode
   {
      cmEvenOdd,
      cmWinding
   }

   public enum TCodepage
   {
      /*---------------------------------------- 8 bit code pages --------------------------------------*/
      cp1250,
      cp1251,
      cp1252,
      cp1253,
      cp1254,
      cp1255,
      cp1256,
      cp1257,
      cp1258,
      cp8859_2,
      cp8859_3,
      cp8859_4,
      cp8859_5,
      cp8859_6,
      cp8859_7,
      cp8859_8,
      cp8859_9,
      cp8859_10,
      cp8859_13,
      cp8859_14,
      cp8859_15,
      cp8859_16,
      cpSymbol,
      cp437,
      cp737,
      cp775,
      cp850,
      cp852,
      cp855,
      cp857,
      cp860,
      cp861,
      cp862,
      cp863,
      cp864,
      cp865,
      cp866,
      cp869,
      cp874,

      /* ----------------------------------------- Unicode (UCS-2) -------------------------------------*/

      cpUnicode,

      /*-------------------------------- CJK encodings for use with Unicode fonts ----------------------*/
      // The following endcodings requires CJK or ISO-2022 input character sequences which are converted to Unicode
      // by DynaPDF. It is not possible to use these encodings with CJK fonts. If a CJK font should be used, choose
      // one of the native CJK character sets listed at the end this block. DynaPDF does not support a conversion
      // algorithm to convert ISO-2022 character sequences to CJK code points. If you needed such a mapping, please
      // send a mail to support@dynaforms.com.

      // UCS-2 does not support all available CJK code points. However, surrogates are still accessible if the used
      // TrueType font contains a cmap format 12. Another way is to use a CJK font with a native CJK charset.

      cpCJK_Big5_Uni,    // Big5 plus HKSCS extension.
      cpCJK_EUC_JP_Uni,  // EUC-JP.
      cpCJK_EUC_KR_Uni,  // EUC-KR.
      cpCJK_EUC_TW_Uni,  // CNS-11643-1992 (Planes 1-15).
      cpCJK_GBK_Uni,     // GBK is the Microsoft code page 936 (GB2312, EUC-CN plus GBK extension).
      cpCJK_GB12345_Uni, // GB-12345-1990 (Traditional Chinese form of GB-2312).
      cpCJK_HZ_Uni,      // Mixed ASCII / GB-2312 encoding
      cpCJK_2022_CN_Uni, // ISO-2022-CN-EXT (GB-2312 plus ISO-11643 Planes 1-7).
      cpCJK_2022_JP_Uni, // ISO-2022-JP.
      cpCJK_2022_KR_Uni, // ISO-2022-KR.
      cpCJK_646_CN_Uni,  // ISO-646-CN (GB-1988-80).
      cpCJK_646_JP_Uni,  // ISO-646-JP (JIS_C6220-1969-RO).
      cpCJK_IR_165_Uni,  // ISO-IR-165 (extended version of GB-2312).
      cpCJK_932_Uni,     // Microsoft extended version of SHIFT_JIS.
      cpCJK_949_Uni,     // EUC-KR extended with UHC (Unified Hangul Codes).
      cpCJK_950_Uni,     // Microsoft extended version of Big5.
      cpCJK_JOHAB_Uni,   // JOHAB.

      /*---------------------------------- Native CJK charsets (CJK fonts only) ------------------------*/

      // When using a CJK font, DynaPDF creates dynamically a "To Unicode CMAP" to enable cut & paste
      // operations in Adobe's Acrobat. If a CJK sequence has no Unicode code point, the CJK code is used instead.
      // In this case, cut & paste works for all other characters, but the unmapped character appears as arbitrary
      // wrong character when the string is paste to another document. A wrongly mapped character in the
      // "To Unicode CMAP" has no effect in viewing or printing the document.

      // The comments describe which mapping tables are available for a character set. Note these are character sets
      // not code pages.

      cpShiftJIS,     // ShiftJIS charset plus code page 932 extension.
      cpBig5,         // Big5 plus HKSCS extension.
      cpGB2312,       // GB2312 charset plus GBK and cp936 extension.
      cpWansung,      // Wansung
      cpJohab,        // Johab
      cpMacRoman,     // Mac Roman
      cpAdobeStd,     // This is an encoding for Type1 fonts. It should normally not be used.
      cpInternal,     // Internal -> not usable
      cpGlyphIndexes, // Can be used with TrueType and OpenType fonts only. DynaPDF creates a reverse mapping so that copy & paste will work.
      cpPDFDocEnc,    // Internal -> not usable. Used for form fonts. This is a superset of the code page 1252 and MacRoman.
      cpExtCMap,      // Internal -> not usable. This code page is set when a font was loaded with an external cmap.
      cpDingbats,     // Internal -> Special encoding for ZapfDingbats
      cpMacExpert,    // Internal -> not usable.
      cpRoman8        // This is a standard PCL 5/6 code page
   }

   // The data for user defined columns is stored in collection items.
   public enum TColColumnType
   {
      cisCreationDate, // Data comes from the embedded file
      cisDescription,  // Data comes from the embedded file
      cisFileName,     // Data comes from the embedded file
      cisModDate,      // Data comes from the embedded file
      cisSize,         // Data comes from the embedded file
      cisCustomDate,   // User defined date.
      cisCustomNumber, // User defined nummber.
      cisCustomString  // User defined string.
   }

   public enum TColorConvFlags : uint
   {
      ccfBW_To_Gray   = 0, // Default, RGB Black and White set with rg or RG inline operators are converted to gray
      ccfRGB_To_Gray  = 1, // If set, inline color operators rg and RG are converted to gray
      ccfToGrayAdjust = 2  // Converts RGB and gray inline operators to gray and allows to darken or lighten the colors
   }

   public enum TColorMode
   {
      cmFill,
      cmStroke,
      cmFillStroke
   }

   public enum TColView
   {
      civNotSet,
      civDetails,
      civTile,
      civHidden
   }

   public enum TCompBBoxFlags : uint
   {
      cbfNone =             0x00000000,
      cbfIgnoreWhiteAreas = 0x00000001, // Ignore white vector graphics or text.
   /*
      Please note that images must be decompressed if one of the following flags are set. Parsing gray or color images
      is in most cases not useful and you should not parse such images if it is not really required.
   */
      cbfParse1BitImages  = 0x00000002, // Find the visible area in 1 bit images. This is the most important case
                                        // since scanned faxes are usually 1 bit images.
      cbfParseGrayImages  = 0x00000004, // Find the visible area in gray images.
      cbfParseColorImages = 0x00000008, // Find the visible area in color images. This is usually not required
                                        // and slow downs processing a lot.
      cbfParseAllImages   = 0x0000000E  // Find the visible area in all images.
   }

   public enum TConformanceType
   {
      ctPDFA_1b_2005, // Convert the file to PDF/A if possible
      ctNormalize,    // Check the file for errors, rebuild all embedded fonts plus options
      ctPDFA_2b,
      ctPDFA_3b,
      /* --------------------------------------------------------------------------------------------------------------------
       *                                  ZUGFeRD, ZUGFeRD 2.0, and Factur-X output
       * --------------------------------------------------------------------------------------------------------------------
       * The following constants convert the file to PDF/A 3b and set the whished ZUGFeRD, ZUGFeRD 2.0 or Factur-X conformance
       * level in the XMP metadata. CheckConformance() does not validate the XML invoice but it checks whether it is present.
       * The embedded XML invoice must be named as follows (case-sensitive and without quotes):
       *
       *  ZUGFeRD 1.0: "ZUGFeRD-invoice.xml"
       *  ZUGFeRD 2.0: "zugferd-invoice.xml"
       *  Factur-X:    "factur-x.xml"
       *
       * Setting the correct ZUGFeRD or Factur-X conformance level is important since this value defines which fields must be
       * present in the XML invoice.
       */
      // ZUGFeRD 1.0:
      ctZUGFeRD_Basic,     // Basic profile
      ctZUGFeRD_Comfort,   // Comfort profile
      ctZUGFeRD_Extended,  // Extended profile
      // ZUGFeRD 2.0:
      ctZUGFeRD2_Minimum,  // Minimum profile
      ctZUGFeRD2_Basic,    // Basic profile
      ctZUGFeRD2_Basic_WL, // Basic WL profile
      ctZUGFeRD2_Comfort,  // EN 16931 profile
      ctZUGFeRD2_Extended, // Extended profile
      // Factur-X
      ctFacturX_Minimum,   // Minimum profile
      ctFacturX_Basic,     // Basic profile
      ctFacturX_Basic_WL,  // Basic WL profile
      ctFacturX_Comfort,   // EN 16931 profile
      ctFacturX_Extended,  // Extended profile
      ctFacturX_XRechnung, // German XRechnung profile
      ctPDFA_2u,           // Convert the file to PDF/A 2u if possible
      ctPDFA_3u,           // Convert the file to PDF/A 3u if possible
      ctPDFA_4,            // Convert the file to PDF/A 4 if possible. Embedded files are supported but no file attach annotations.
      ctPDFA_4e,           // Convert the file to PDF/A 4e if possible. This version allows 3D contents in rich media annotations.
      ctPDFA_4f            // Convert the file to PDF/A 4f if possible. This version allows file attach annotations.
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TCTM
   {
      public double a;
      public double b;
      public double c;
      public double d;
      public double x;
      public double y;
   }

   public enum TCompressionFilter
   {
      cfFlate                 = 0x00000000, // PDF or TIFF output
      cfJPEG                  = 0x00000001, // PDF, JPEG, or TIFF output
      cfCCITT3                = 0x00000002, // PDF or TIFF output -> B&W CCITT Fax G3 compression -> fast but less compression ratio
      cfCCITT4                = 0x00000003, // PDF or TIFF output -> B&W CCITT Fax G4 compression -> slower but higher compression ratio
      cfLZW                   = 0x00000004, // TIFF or GIF output -> Very fast but less compression ratios than flate
      cfLZWBW                 = 0x00000005, // TIFF
      cfFlateBW               = 0x00000006, // TIFF, PNG, or BMP output -> Dithered black & white output. The resulting image will be
                                            // compressed with Flate or left uncompressed if the output image format is a bitmap. If
                                            // you want to use CCITT Fax 4 compression (TIFF only) set the flag icUseCCITT4 in the
                                            // AddImage() function call. Note that this filter is not supported for PDF creation!
      cfJP2K                  = 0x00000007, // PDF or JPEG2000 output
      cfJBIG2                 = 0x00000008, // PDF output only
      cfNone                  = 0x00000009, // TIFF output only
      // Special flags for AddRasImage(), RenderPageToImage(), or RenderPDFFile() if the pixel format was set to pxfGray or pxfGrayA.
      // These flags can be combined with the filters cfFlate, cfCCITT3, cfCCITT4, cfJBIG2, cfLZW, and cfJP2K.
      cfDitherFloydSteinberg  = 0x00001000, // Floyd Steinberg dithering.
      cfConvGrayToOtsu        = 0x00002000, // The Otsu filter is a special filter to produce black & white images. It is very useful
                                            // if an OCR scan should be applied on the resulting 1 bit image. The flag is considered
                                            // in AddRasImage(), RenderPDFFile(), and RenderPageToImage() if the pixel format was set
                                            // to pxfGray.
      cfOrderedDithering      = 0x00004000, // Ordered dithering. The difference in comparison to the pixel format pxf1Bit is that
                                            // we render into a GrayA buffer instead of a native 1 bit buffer since this one produces
                                            // incorrect results in certain transparency calculations.
      cfIgnoreICCProfile      = 0x00008000, // Special flag for AddRasImage(). Meaningful only if color management is enabled. If set,
                                            // the device ICC profile is not embedded in the image file.
      // Special flags for Optimize()
      cfPresLosslessFilter    = 0x00010000, // Preserve loss-less compression filters and use the specified filter otherwise.
      cfPresLossyFilter       = 0x00020000  // Preserve lossy compression filters and use the specified filter otherwise.
                                            // cfPresLossLessFilter and cfPresLossyFilter can be combined to preserve the original
                                            // compression filter in either case.
   }

   public enum TCompressionLevel
   {
      clNone,
      clDefault,
      clFastest,
      clMax
   }

   public enum TDateType
   {
      dtCreationDate,
      dtModDate
   }

   public enum TDecodeFilter
   {
      dfNone,
      dfASCII85Decode,   // No parameters
      dfASCIIHexDecode,  // No parameters
      dfCCITTFaxDecode,  // Optional Parameters
      dfDCTDecode,       // Optional Parameters
      dfFlateDecode,     // Optional Parameters
      dfJBIG2Decode,     // Optional Parameters
      dfJPXDecode,       // No parameters
      dfLZWDecode,       // Optional Parameters
      dfRunLengthDecode  // No parameters
   }

   public enum TDecSeparator
   {  /* thousand separator, decimal separator */
      dsCommaDot,
      dsNoneDot,
      dsDotComma,
      dsNoneComma,
      dsQuoteDot
   }

   public enum TDocumentInfo
   {
      diAuthor,
      diCreator,
      diKeywords,
      diProducer,
      diSubject,
      diTitle,
      diCompany,
      diPDFX_Ver,     // GetInDocInfo() only -> The PDF/X version is set by SetPDFVersion()!
      diCustom,       // User defined key
      diPDFX_Conf,    // GetInDocInfo() or GetInDocInfoEx() only. The value of the GTS_PDFXConformance key.
      diCreationDate, // GetInDocInfo() or GetInDocInfoEx() or after ImnportPDFFile() was called.
      diModDate       // GetInDocInfo() or GetInDocInfoEx() only
   }

   public enum TDrawDirection
   {
      ddCounterClockwise,
      ddClockwise
   }

   public enum TDrawMode
   {
      dmNormal         = 0,
      dmStroke         = 1,
      dmFillStroke     = 2,
      dmInvisible      = 3,
      dmFillClip       = 4,
      dmStrokeClip     = 5,
      dmFillStrokeClip = 6,
      dmClipping       = 7,
      dmNone           = -1 // Internal, do not use this mode!!!
   }

   public enum TDuplexMode
   {
      dpmNone,          // Default
      dpmSimplex,
      dpmFlipShortEdge,
      dpmFlipLongEdge
   }

   public enum TEmbFileLocation
   {
      eflChild,         // The file is an embedded file in the current document
      eflChildAnnot,    // The file is located in a file attachment annotion in the current document
      eflExternal,      // The file is an embedded file in an external document
      eflExternalAnnot, // The file is located in a file attachment annotion in an external document 
      eflParent,        // The file is located in the parent document
      eflParentAnnot    // The file is located in a file attachment annotion in the parent document
   }

   public enum TEnumFontProcFlags
   {
      efpAnsiPath    = 0, // Code page 1252 on Windows, UTF-8 otherwise
      efpUnicodePath = 1, // FilePath is in Unicode format. Make a typecast to (UI16*) in this case.
      efpEmbeddable  = 2, // The font has embedding rights.
      efpEditable    = 4  // If set, the font has editing rights (important for form fields).
   }

   public enum TErrMode : uint
   {
      emIgnoreAll       = 0x00000000,
      emSyntaxError     = 0x00000001,
      emValueError      = 0x00000002,
      emWarning         = 0x00000004,
      emFileError       = 0x00000008,
      emFontError       = 0x00000010,
      emAllErrors       = 0x0000FFFF,
      emNoFuncNames     = 0x10000000, // Do not print function names in error messages
      emUseErrLog       = 0x20000000,
      // Special flags for CheckConformance().
      // These flags add info messages to the error log. The error callback function is not invoked.
      emLogFonts        = 0x00010000, // If set, CheckConformance() logs which fonts will were replaced with system fonts or converted to Type3
      emLogFontsVerbose = 0x00020000  // If set, the path to the font file is added to the error log too
   }

   // Note that error codes are negative; to determine the error type use the absolute value:
   // if (-errCode & DynaPDF.TErrType.E_FATAL_ERROR) Console.Write("A fatal error occurred!");
   public enum TErrType : uint
   {
      E_WARNING      = 0x02000000,
      E_SYNTAX_ERROR = 0x04000000,
      E_VALUE_ERROR  = 0x08000000,
      E_FONT_ERROR   = 0x10000000,
      E_FATAL_ERROR  = 0x20000000,
      E_FILE_ERROR   = 0x40000000,

      E_NEED_PWD        = 0xb2 | E_FILE_ERROR,
      E_WRONG_OPEN_PWD  = 0xb3 | E_FILE_ERROR,
      E_WRONG_OWNER_PWD = 0xb4 | E_FILE_ERROR,
      E_WRONG_PWD       = 0xb5 | E_FILE_ERROR
   }

   public enum TExtColorSpace
   {
      esDeviceRGB  = 0,  // Device color space
      esDeviceCMYK = 1,  // Device color space
      esDeviceGray = 2,  // Device color space
      esCalGray    = 3,  // CIE-based color space
      esCalRGB     = 4,  // CIE-based color space
      esLab        = 5,  // CIE-based color space
      esICCBased   = 6,  // CIE-based color space -> contains an ICC profile
      esPattern    = 7,  // Special color space
      esIndexed    = 8,  // Special color space
      esSeparation = 9,  // Special color space
      esDeviceN    = 10, // Special color space
      esNChannel   = 11, // Special color space
      esInitSpace  = -1  // Internal -> This value must not be used!
   }

   public enum TFileAttachIcon
   {
      faiGraph,
      faiPaperClip,
      faiPushPin,
      faiTag
   }

   public enum TFileOP
   {
      foOpen,
      foPrint
   }

   public enum TFieldColor
   {
      fcBackColor,
      fcBorderColor,
      fcTextColor
   }

   public enum TFieldFlags : uint
   {
      ffReadOnly         = 0x00000001,
      ffRequired         = 0x00000002,
      ffNoExport         = 0x00000004,

      ffInvisible        = 0x00000008,
      ffHidden           = 0x00000010,
      ffPrint            = 0x00000020,
      ffNoZoom           = 0x00000040,
      ffNoRotate         = 0x00000080,
      ffNoView           = 0x00000100,

      ffMultiline        = 0x00001000, /* Text fields only */
      ffPassword         = 0x00002000, /* Text fields only */
      ffNoToggleToOff    = 0x00004000, /* Radio buttons    */
      ffRadioIsUnion     = 0x02000000, /* PDF-1.5 check boxes of a radio button field only */
      ffCommitOnSelCh    = 0x04000000, /* PDF-1.5 combo boxes, list boxes */

      ffEdit             = 0x00040000, /* Combo boxes only */
      ffSorted           = 0x00080000, /* Use this flag to change the sort flag of combo boxes or list boxes */
      ffFileSelect       = 0x00100000, /* PDF 1.4 Text fields only */
      ffMultiSelect      = 0x00200000, /* PDF 1.4 List boxes only  */
      ffDoNotSpellCheck  = 0x00400000, /* PDF 1.4 Text fields, combo boxes. If the field is a combo box, this flag is meaningful only if ffEdit is also set. */
      ffDoNotScroll      = 0x00800000, /* PDF 1.4 Text fields only */
      ffComb             = 0x01000000  /* PDF 1.5 Text fields only */
   }

   public enum TFieldType
   {
      ftButton,
      ftCheckBox,
      ftRadioBtn,
      ftComboBox,
      ftListBox,
      ftText,
      ftSignature,
      ftGroup // this is not a real field type, it is just an array of fields
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TFltPoint
   {
      public TFltPoint(float x_, float y_)
      {
         x = x_;
         y = y_;
      }
      public float x;
      public float y;
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TI32Point
   {
      public TI32Point(int x_, int y_)
      {
         x = x_;
         y = y_;
      }
      public int x;
      public int y;
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TFltRect
   {
      public float Left;
      public float Bottom;
      public float Right;
      public float Top;
   }

   public enum TFlushPageFlags
   {
      fpfDefault      = 0, // Write anything to the file that is possible
      fpfImagesOnly   = 1, // If set, only images are written to the file. The pages are still
                           // in memory and can be modified with EditPage(). Flushed images can
                           // still be referenced in other pages. The image handles remain valid.
      fpfExclLastPage = 2  // If set, the last page is not flushed
   }

   public enum TFontBaseType
   {
      fbtTrueType,
      fbtType1,
      fbtOpenType, // OpenType font with Postscript outlines
      fbtStdFont,  // PDF Standard font
      fbtDisabled  // This value can be used to disable a specific font format. See SetFontSearchOrder() for further information.
   }

   public enum TFontFileSubtype
   {
      ffsType1C        = 0, // CFF based Type1 font
      ffsCIDFontType0C = 1, // CFF based Type1 CID font
      ffsOpenType      = 2, // TrueType based OpenType font
      ffsOpenTypeC     = 3, // CFF based OpenType font
      ffsCIDFontType2  = 4, // TrueType based CID Font
      ffsNoSubtype     = 9  // The font file is in the format of FontType
   }

   // If the font selection mode is set to smFullName or smPostScriptName the font styles fsItalic or fsBold are ignored.
   // The PostScript or Full Name are already unique font names incl. style information.
   public enum TFontSelMode
   {
      smFamilyName,
      smPostScriptName,
      smFullName
   }

   public enum TFontType
   {
      ftMMType1,
      ftTrueType,
      ftType0,    // Check the font file type to determine the font sub type
      ftType1,
      ftType3
   }

/*
   The font search run works as follows:

      - DynaPDF tries always to find the exact weight, if it cannot be found then a font with
        the next smaller weight is selected (if available).
      - Italic styles can always be emulated but it is not possible to emulate thinner weights or
        regular styles with an italic font.
      - If the specified weight is larger as the font weight the remaining weight will be emulated
        if the difference to the requested weight is larger than 200.
        With SetFontWeight() it is possible to control whether a missing weight should be emulated.
        If FontWeight is smaller or equal to the requested font weight then emulation will be disabled.

   TFStyle is a bitmask that is defined as follows:

      - Bits 0..7   // Style bits fsItalic, fsUnderlined, fsStriked
      - Bits 8..19  // Width class -> Defined for future use.
      - Bits 20..31 // Font Weight

   - A width class can be converted to a style constant by multiplying it with 256 (width << 8).
   - A font weight can be converted to a style constant by multiplying it with 1048576 (weight << 20).
   - Additional attributes can be added with a binary or operator (e.g. style or fsItalic).
   - Only one width class and one font weight can be set at time.

   - WidthFromStyle() extracts the width class.
   - WeightFromStyle() extracts the font weight.

   The following functions extract the width class or font weight from a style variable:

      widthClass  = WidthFromStyle(style);
      weightClass = WeightFromStyle(style);
*/

   public enum TFStyle
   {
      fsNone           = 0x00000000, // Regular weight (400)
      fsItalic         = 0x00000001,
      fsUnderlined     = 0x00000004,
      fsStriked        = 0x00000008,
      fsVerticalMode   = 0x00000010, // Not considered at this time
      fsDblUnderline   = 0x00000020,
      // Width class
      fsUltraCondensed = 0x00000100, // 1
      fsExtraCondensed = 0x00000200, // 2
      fsCondensed      = 0x00000300, // 3
      fsSemiCondensed  = 0x00000400, // 4
      fsNormal         = 0x00000500, // 5
      fsSemiExpanded   = 0x00000600, // 6
      fsExpanded       = 0x00000700, // 7
      fsExtraExpanded  = 0x00000800, // 8
      fsUltraExpanded  = 0x00000900, // 9
      // Weight class
      fsThin           = 0x06400000, // 100
      fsExtraLight     = 0x0c800000, // 200
      fsLight          = 0x12C00000, // 300
      fsRegular        = 0x19000000, // 400 -> Same as fsNone
      fsMedium         = 0x1F400000, // 500
      fsDemiBold       = 0x25800000, // 600
      fsBold           = 0x2BC00000, // 700 -> The old constant 2 is still supported to preserve backward compatibility
      fsExtraBold      = 0x32000000, // 800
      fsBlack          = 0x38400000, // 900
      fsUltraBlack     = 0x3E800000  // 1000
   }

   public enum TGStateFlags
   {
      gfCompatible         = 0x00000000, // Compatible graphics state to earlier DynaPDF versions -> default
      gfRestorePageCoords  = 0x00000001, // Restore the coordinate system with the graphics state (the value of PageCoords, see SetPageCoords())
      gfRealTopDownCoords  = 0x00000002, // If set, the page coordinate system is not reset to bottom-up when transforming
                                         // the coordinate system. However, real top-down coordinates require a large internal
                                         // overhead and where never fully implemented. The usage of this flag should be avoided
                                         // if possible.
      gfNativeBlackWhite   = 0x00000004, // Do not convert RGB black or white to DeviceGray
      gfUseImageColorSpace = 0x00000008, // If set, the active color space is ignored when inserting an image. The color space is taken
                                         // from the image file instead.
      gfIgnoreICCProfiles  = 0x00000010, // If set, embedded ICC profiles in images are ignored. The meaning of this flag depends on
                                         // whether the flag gfUseImageColorSpace is set too. If set, an embedded profile is not used to
                                         // create an ICCBased color space for the image. The image is inserted in the corresponding
                                         // device color space instead. If the flag gfUseImageColorSpace is absent an embedded ICC profile
                                         // is not used to convert CMYK images to DeviceRGB if necessary. This conversion is only applied
                                         // if the current color space is DeviceRGB and if the image color space is CMYK.
      gfAnsiStringIsUTF8   = 0x00000020, // If set, single byte strings in Ansi functions are treated as UTF-8 encoded Unicode strings.
      gfRealPassThrough    = 0x00000040, // If set, JPEG images are inserted as is. JPEG images are normally rebuild, also in pass-through mode, to avoid issues
                                         // with certain malformed JPEG images which cannot be displayed in Adobes Acrobat or Reader. If you know that your JPEG
                                         // images work then set this flag to avoid unnecessary processing time.
      gfNoBitmapAlpha      = 0x00000080, // If set, the alpha channel in 32 bit bitmaps will be ignored. Useful for bitmaps with an invalid alpha channel.
      gfNoImageDuplCheck   = 0x00000100, // If set, no duplicate check for images will be performed. This can significantly improve processing speed.
      gfNoObjCompression   = 0x00000200, // If set, object compression will be disabled.
      gfComplexText        = 0x00000400, // If set, text is processed with Uniscribe on Windows.
      gfDisableJavascript  = 0x00000800, // If set, the raw field value of text fields is used to create the apprearance stream.
      gfDisableBidiCtrls   = 0x00001000, // Meaningful only if gfComplexText is set too. If set, bidi control characters are ignored. This flag can be useful if
                                         // the result of web browsers should be emulated since web browsers do not support bidi control characters. This flag
                                         // is also used internally to create the appearance stream of form fields since form fields do not support bidi control
                                         // characters.
      gfDoNotComprMetadata = 0x00002000, // If set, arbitrary metadata stream associated with PDF objects other that the global metadata stream will not be
                                         // compressed. This can be useful since certain standards prohibit compression of metadata streams. The flag is
                                         // automatically for PDF/X files.
      gfUpdatePDFVTModDate = 0x00004000, // If set, the key GTS_PDFVTModDate will be set to the file's modification date.
      gfSkaleAnnotIcons    = 0x00008000  // If set, icons of text and file attach annotation are scaled with the page like every other annotation. These annotations
                                         // have normally a fixed size that is indendent of the zoom or scaling factor.
   }

   public enum THashType
   {
      htDetached, // CloseAndSignFileExt() returns the byte ranges of the finish PDF buffer to create a detached signature
      htSHA1      // CloseAndSignFileExt() returns the SHA1 hash of the PDF file so that it can be signed
   }

   public enum THighlightMode
   {
      hmNone,
      hmInvert,
      hmOutline,
      hmPush,
      hmPushUpd  // Update appereance stream on changes
   }

   public enum TICCProfileType
   {
      ictGray,
      ictRGB,
      ictCMYK,
      ictLab
   }

   public enum TImageConversionFlags
   {
      icNone      = 0, // Default
      icUseCCITT4 = 1  // Use CCITT Fax 4 compression instead of Flate for dithered images.
   }

   /*
      TIFF is the only format that supports different compression filters. The Filter parameter of the function
      AddImage() is ignored if the image format supports only one specific compression filter.
      Note that images are automatically converted to the nearest supported color space if the image format does
      not support the color space of the image.
   */
   public enum TImageFormat
   {
      ifmTIFF,     // DeviceRGB, DeviceCMYK, DeviceGray, Black & White -> CCITT Fax Group 3/4, JPEG, Flate, LZW.
      ifmJPEG,     // DeviceRGB, DeviceCMYK, DeviceGray    -> JPEG compression.
      ifmPNG,      // DeviceGray, DeviceRGB, Black & White -> Flate compression.
      ifmReserved, // Reserved for future extensions.
      ifmBMP,      // DeviceGray, DeviceRGB, Black & White -> Uncompressed.
      ifmJPC       // DeviceRGB, DeviceCMYK, DeviceGray    -> JPEG2000 compression.
   }

   public enum TImportFlags : uint
   {
      ifImportAll        = 0x0FFFFFFE, // default
      ifContentOnly      = 0x00000000,
      // If this flag is set, only interactive objects are imported if any, Otherwise only empty pages are imported.
      // This flag can be used to copy an Interactive Form, bookmarks or annotations to another PDF file.
      ifNoContent        = 0x00000001,
      // The imported page is not converted to a template if ifImportAsPage is set.
      // Note that this flag can cause resource conflicts. Use this flag carefully!
      ifImportAsPage     = 0x80000000,
      // base objects
      ifCatalogAction    = 0x00000002, // Open action, Catalog actions
      ifPageActions      = 0x00000004,
      ifBookmarks        = 0x00000008,
      ifArticles         = 0x00000010,
      ifPageLabels       = 0x00000020,
      ifThumbs           = 0x00000040,
      ifTranspGroups     = 0x00000080, // This flag is not longer considered.
      ifSeparationInfo   = 0x00000100,
      ifBoxColorInfo     = 0x00000200,
      ifStructureTree    = 0x00000400,
      ifTransition       = 0x00000800,
      ifSearchIndex      = 0x00001000,
      ifJavaScript       = 0x00002000, // Global JavaScripts
      ifJSActions        = 0x00004000, // JavaScript actions
      ifDocInfo          = 0x00008000, // Document info entries
      ifEmbeddedFiles    = 0x00200000, // File attachments
      ifFileCollections  = 0x00400000, // File collections (PDF 1.7)
      // Annotations -> Only the most important annotation types can be selected directly.
      // Note that all annotation types can be deleted with DeleteAnnotation.
      ifAllAnnots        = 0x009F0000,
         ifFreeText      = 0x00010000,
         ifTextAnnot     = 0x00020000,
         ifLink          = 0x00040000,
         ifStamp         = 0x00080000,
         if3DAnnot       = 0x00100000,
         ifOtherAnnots   = 0x00800000,
      // Interactive Form Fields are also annotations but we handle this type separately!
      ifFormFields       = 0x01000000,
      ifPieceInfo        = 0x02000000, // The PieceInfo dictionary contains arbitrary user defined data. The data in
                                       // this dictionary is meaningful only for the application that created the data.
      /* -------------------- Special flags -------------------- */
      ifPrepareForPDFA   = 0x10000000, // Replace LZW compression with Flate, set the Interpolate key of images to false, do not import embedded files.
      ifEnumFonts        = 0x20000000, // Import fonts for EnumDocFonts(). The document must be deleted when this flag is set!!!
      ifAllPageObjects   = 0x40000000  // Import links when using ImportPageEx() within an open page. The entire document should be imported in this case.
   }

   public enum TImportFlags2 : uint
   {
      if2MergeLayers     = 0x00000001, // If set, layers with identical name are merged. If this flag is absent DynaPDF
                                       // imports such layers separately so that each layer refers still to the pages
                                       // where it was originally used.
      if2Normalize       = 0x00000002, // Replace LZW compression with Flate, apply limit checks, repair errors if possible
      if2UseProxy        = 0x00000004, // Not meaningful for PDF files which are loaded from a memory buffer. If set, all streams are loaded from the file
                                       // on demand but they are never hold in memory. This reduces drastically the memory usage and enables the processing
                                       // of almost arbitrary large PDF files with minimal memory usage. The corresponding PDF file must not be deleted before
                                       // CloseFile() or CloseFileEx() was called.
      if2NoMetadata      = 0x00000008, // Ignore metadata streams which are attached to fonts, pages, images, and so on.
      if2DuplicateCheck  = 0x00000010, // Perform a duplicate check on color spaces, fonts, images, patterns, and templates when merging PDF files.
      if2NoResNameCheck  = 0x00000020, // Import resources as is. This flag can significantly imporove the loading time of pages with a huge resource tree.
                                       // This flag should only be set in viewer applications to improve the loading time of pages.
      if2CopyEncryptDict = 0x00000040, // If set, the encryption settings of an encrypted PDF file are copied to the new PDF file.
                                       // The flag does nothing if the file is not encrypted.
      if2SkipDPartData   = 0x00000080  // If set, DPart dictionaries are not imported.
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TIntRect
   {
      public int x1;
      public int y1;
      public int x2;
      public int y2;
   }

   public enum TKeyLen
   {
      kl40bit,    // RC4 Encryption -> Acrobat 3 or higher
      kl128bit,   // RC4 Encryption -> Acrobat 5 or higher
      kl128bitEx, // RC4 Encryption -> Acrobat 6 or higher
      klAES128,   // AES Encryption -> Acrobat 7 or higher
      klAES256,   // AES Encryption -> Acrobat 9 or higher
      klAESRev6   // AES Encryption -> Acrobat X or higher
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public class TLineAnnotParms
   {
      public int             StructSize;       // Must be set to sizeof(TLineAnnotParms)
      public lbool           Caption;          // If true, the annotation string Content is used as caption.
      public float           CaptionOffsetX;   // Horizontal offset of the caption from its normal position
      public float           CaptionOffsetY;   // Vertical offset of the caption from its normal position
      public TLineCaptionPos CaptionPos;       // The position where the caption should be drawn if present
      public float           LeaderLineLen;    // Length of the leader lines (positive or negative)
      public float           LeaderLineExtend; // Optional leader line extend beyond the leader line (must be a positive value or zero)
      public float           LeaderLineOffset; // Amount of space between the endpoints of the annotation and the leader lines (must be a positive value or zero)
   }

   public enum TLineCapStyle
   {
      csButtCap,
      csRoundCap,
      csSquareCap
   }

   public enum TLineEndStyle
   {
      leNone,
      leButt,
      leCircle,
      leClosedArrow,
      leDiamond,
      leOpenArrow,
      leRClosedArrow,
      leROpenArrow,
      leSlash,
      leSquare
   }

   public enum TLineCaptionPos
   {
      cpInline, // The caption is centered inside the line
      cpTop     // The caption is drawn on top of the line
   }

   public enum TLineJoinStyle
   {
      jsMiterJoin,
      jsRoundJoin,
      jsBevelJoin
   }

   public enum TLoadCMapFlags
   {
      lcmDefault   = 0, // Load the cmaps in the directory now
      lcmRecursive = 1, // Load sub directories recursively
      lcmDelayed   = 2  // Load the cmap files only when a font requires an external cmap
   }

   public enum TMeasureNumFormat
   {
      mnfDecimal,
      mnfFractional,
      mnfRound,
      mnfTruncate
   }

   public enum TMeasureLblPos
   {
      mlpSuffix,
      mlpPrefix
   }

   public enum TMetadataObj
   {
      mdoCatalog,
      mdoFont,
      mdoImage,
      mdoPage,
      mdoTemplate
   }

   public enum TMetaFlags : uint
   {
      mfDefault           = 0x00000000, // Default conversion
      mfDebug             = 0x00000001, // Write debug information into the content stream
      mfShowBounds        = 0x00000002, // Show the bounding boxes of text strings
      mfNoTextScaling     = 0x00000004, // Do not scale text
      mfClipView          = 0x00000008, // Draw the file into a clipping rectangle
      mfUseRclBounds      = 0x00000010, // Use rclBounds instead of rclFrame
      mfNoClippingRgn     = 0x00000040, // Disables SelectClippingRegion and IntersectClipRect
      mfNoFontEmbedding   = 0x00000080, // Do not embed fonts -> Fonts should be embedded!!!
      mfNoImages          = 0x00000100, // Ignore image records
      mfNoStdPatterns     = 0x00000200, // Ignore standard patterns
      mfNoBmpPatterns     = 0x00000400, // Ignore bitmap patterns
      mfNoText            = 0x00000800, // Ignore text records
      mfUseUnicode        = 0x00001000, // Ignore ANSI_CHARSET
      mfUseTextScaling    = 0x00004000, // Scale text instead of using the intercharacter spacing array
      mfNoUnicode         = 0x00008000, // Avoid usage of Unicode fonts -> recommended to enable PDF 1.2 compability
      mfFullScale         = 0x00010000, // Recommended if 32 bit coordinates are used
      mfUseRclFrame       = 0x00020000, // This flag should be set if the rclFrame rectangle is properly set
      mfDefBkModeTransp   = 0x00040000, // Initialize the background mode to transparent (SetBkMode() overrides this state).
      mfApplyBidiAlgo     = 0x00080000, // Apply the bidirectional algorithm on Unicode strings
      mfGDIFontSelection  = 0x00100000, // Use the GDI to select fonts
      mfRclFrameEx        = 0x00200000, // If set, and if the rclBounds rectangle is larger than rclFrame, the function
                                        // extends the output rectangle according to rclBounds and uses the resulting
                                        // bounding box to calculate the image size (rclBounds represents the unscaled
                                        // image size). This is probably the correct way to calculate the image size.
                                        // However, to preserve backward compatibility the default calculation cannot
                                        // be changed.
      mfNoTextClipping    = 0x00400000, // If set, the ETO_CLIPPED flag in text records is ignored.
      mfSrcCopy_Only      = 0x00800000, // If set, images which use a ROP code other than SRCCOPY are ignored. This is useful when processing Excel 2007 spool files.
      mfClipRclBounds     = 0x01000000, // If set, the graphic is drawn into a clipping path with the size of rclBounds.
                                        // This flag is useful if the graphic contains content outside of its bounding box.
      mfDisableRasterEMF  = 0x02000000, // If set, EMF files which use unsupported ROP codes are not rastered.
      mfNoBBoxCheck       = 0x04000000, // If set, the rclBounds and rclFrame rectangles are used as is. DynaPDF uses normally
                                        // the rclBounds rectangle to calculate the picture size if the resolution of the EMF file
                                        // seems to be larger than 1800 DPI since this is mostly an indication that the rclFrame
                                        // rectangle was incorrectly calculated. If you process EMF files in such a high resolution
                                        // then this flag must be set. The flag can be set by default.
      mfIgnoreEmbFonts    = 0x08000000, // If set, embedded fonts in GDIComment records will be ignored. This flag must be set if the fonts
                                        // of an EMF spool file were pre-loaded with ConvertEMFSpool(). Spool fonts must always be loaded
                                        // in a pre-processing step since required fonts are not necessarily embedded in the EMF files.
      mfDontSkipROP_DPa   = 0x10000000, // If set, the ROP code 0x00A000C9 (DPa) is considered in BitBlt, StretchBlt, TranparentBlt, and AlphaBlend records.
                                        // This kind of ROP code combines a pattern brush with the backdrop. If the flag mfDisableRasterEMF is absent, the
                                        // usage of such a ROP code causes that the file is rendered to an image.
      mfCurrComprFilter   = 0x20000000, // If set, the currently active compression filter is used to compress the image (see SetCompressionFilter()).
                                        // This flag is used only if the file is rastered to an image.

      // Obsolete flags -> these flags are ignored, do no longer use them!
      mfUseSpacingArray   = 0x00000020, // enabled by default -> can be disabled with mfUseTextScaling
      mfIntersectClipRect = 0x00002000  // enabled by default -> can be disabled with mfNoClippingRgn
   }

   public enum TNamedAction
   {
      naFirstPage,
      naLastPage,
      naNextPage,
      naPrevPage,
      naGoBack,
      naOpenDlg,
      naPrintDlg,
      naGeneralInfo,
      naFontsInfo,
      naSaveAs,
      naSecurityInfo,
      naFitPage,
      naFullScreen,
      naDeletePages,
      naQuit,
      naUserDefined   // Not usable
   }

   public enum TNegativeStyle
   {
      nsMinusBlack,
      nsRed,
      nsParensBlack,
      nsParensRed
   }

   public enum TObjEvent
   { /* All actions which should be applied to an event (except OnMouseUp) must be a JavaScript action! */
      oeNoEvent,           /* Internal use only -> DO NOT USE THIS VALUE!!! */
      oeOnOpen,            /* Catalog, Pages */
      oeOnClose,           /* Pages only */
      oeOnMouseUp,         /* All fields, page link annotations, bookmarks */
      oeOnMouseEnter,      /* Form fields only */
      oeOnMouseExit,       /* Form fields only */
      oeOnMouseDown,       /* Form fields only */
      oeOnFocus,           /* Form fields only */
      oeOnBlur,            /* Form fields only */
      oeOnKeyStroke,       /* Text fields only */
      oeOnFormat,          /* Text fields only */
      oeOnCalc,            /* Text fields, combo boxes, list boxes */
      oeOnValidate,        /* All form fields, except buttons  */
      oeOnPageVisible,     /* PDF 1.5 -> Form fields only */
      oeOnPageInVisible,   /* PDF 1.5 -> Form fields only */
      oeOnPageOpen,        /* PDF 1.5 -> Form fields only */
      oeOnPageClose,       /* PDF 1.5 -> Form fields only */
      oeOnBeforeClosing,   /* PDF 1.4 -> Catalog only */
      oeOnBeforeSaving,    /* PDF 1.4 -> Catalog only */
      oeOnAfterSaving,     /* PDF 1.4 -> Catalog only */
      oeOnBeforePrinting,  /* PDF 1.4 -> Catalog only */
      oeOnAfterPrinting    /* PDF 1.4 -> Catalog only */
   }

   public enum TObjType
   {
      otAction,
      otAnnotation,
      otBookmark,
      otCatalog,    // PDF 1.4
      otField,
      otPage,
      otPageLink
   }

   public enum TOCAppEvent
   {
      aeExport = 1,
      aePrint  = 2,
      aeView   = 4
   }

   public enum TOCObject
   {
      ooAnnotation,
      ooField,
      ooImage,
      ooTemplate
   }

   public enum TOCGIntent
   {
      oiDesign = 2,
      oiView   = 4,   // Default
      oiAll    = 8,
      oiEmpty  = 16,  // Internal
      // Special flag for GetOCG().
      oiVisible = 32  // This flag is not considered when creating a layer. It is only used in GetOCG() to determine whether a layer is visible.
   }

   public enum TOCPageElement
   {
      peBackgroundImage, // BG
      peForegroundImage, // FG
      peHeaderFooter,    // HF
      peLogo,            // L
      peNone
   }

   public enum TOCGUsageCategory
   {
      oucNone     = 0,
      oucExport   = 1,
      oucLanguage = 2,
      oucPrint    = 4,
      oucUser     = 8,
      oucView     = 16,
      oucZoom     = 32
   }

   public enum TOCUserType
   {
      utIndividual,
      utOrganization,
      utTitle,
      utNotSet
   }

   public enum TOCVisibility
   {
      ovAllOff,
      ovAllOn,
      ovAnyOff,
      ovAnyOn,
      ovNotSet  // Internal
   }

   public enum TOptimizeFlags : uint
   {
      ofDefault               = 0x00000000, // Just rebuild the content streams.
      ofInMemory              = 0x00000001, // Optimize the file fully in memory. Only useful for small PDF files.
      ofConvertAllColors      = 0x00000002, // If set, Separation, DeviceN, and NChannel color spaces will be converted to the device space.
      ofIgnoreICCBased        = 0x00000004, // If set, ICCBased color spaces will be left unchanged.
      ofScaleImages           = 0x00000008, // Scale images as specified in the TOptimizeParams structure.
      ofSkipMaskedImages      = 0x00000010, // Meaningful only if ofScaleImages is set. If set, don't scale images with a color mask.
      ofNewLinkNames          = 0x00000020, // If set, rename all object links to short names like F1, F2 etc.
      ofDeleteInvPaths        = 0x00000040, // Delete invisible paths. An invisible path is a path that was finished with the no-op operator "n".
      ofFlattenLayers         = 0x00000080, // Flatten layers if any.
      ofDeletePrivateData     = 0x00000100, // Delete private data objects from pages, templates, and images.
      ofDeleteThumbnails      = 0x00000200, // Thumbnails can be deleted since PDF viewers can create thumbnails easily on demand.
      ofDeleteAlternateImages = 0x00000400, // If set, alternate images will be deleted.
      ofNoImageSizeCheck      = 0x00000800, // Meaningful only if ofScaleImages is set. If set, do not check whether the scaled image is smaller as the original image.
      ofIgnoreZeroLineWidth   = 0x00001000, // Meaningful only if the parameter MinLineWidth of the structure TOptimizeParams is greater zero.
                                            // If set, ignore line width operators with a value of zero (zero means one device unit).
      ofAdjZeroLineWidthOnly  = 0x00002000, // Meaningful only if the parameter MinLineWidth of the structure TOptimizeParams is greater zero.
                                            // If set, change the line width of real hairlines only (a hairline is a one pixel width line -> LineWidth == 0).
      ofCompressWithJBIG2     = 0x00004000, // If set, 1 bit images are compressed with JBIG2 if not already compressed with this filter.
      ofNoFilterCheck         = 0x00008000, // Meaningful only, if the flag ofCompressWithJBIG2 is set. If set, re-compress all 1 bit images, also if already compressed with JBIG2.
                                            // This flag is mainly a debug flag to compare the compression ratio with other JBIG2 implementations.
      ofConvertGrayTo1Bit     = 0x00010000, // Useful for scanned faxes since many scanners create gray images for black & white input.

      /* ---------------------------- Notice: ----------------------------------------------
       * Special color spaces like Separation, DeviceN, and NChannel are ignored unless the flag ofConvertAllColors is set too.
       */
      ofConvertToGray         = 0x00020000, // If set, images, text, and vector graphics are converted to DeviceGray.
      ofConvertToRGB          = 0x00040000, // If set, images, text, and vector graphics are converted to DeviceRGB.
      ofConvertToCMYK         = 0x00080000, // If set, images, text, and vector graphics are converted to DeviceCMYK.
      ofReplaceJP2KWithJPEG   = 0x00100000, // Meaningful only, if the flag ofConvertToGray, ofConvertToRGB, or ofConvertToCMYK is set. If set, JPEG 2000 compressed images
                                            // (which are converted to another color space) will be compressed with JPEG instead JPEG 2000. JPEG 2000 compression is very slow
                                            // and requires much memory. JPEG compression is around 10 times faster and produces almost identical results.
      ofUseOtsuFilter         = 0x00200000, // Meaningful only, if the flag ofConvertGrayTo1Bit is set. The Otsu filter is useful for gray scanned faxes.
      ofConvTextToOutlines    = 0x00400000, // If set, texts of non-Type3 fonts are converted to outlines or vector graphics. The resulting file contains no fonts with
                                            // exception of Type3 fonts, if any. Such files are large but avoid printing issues due to missing or damaged fonts.
      ofConvNonEmbFontsOnly   = 0x00800000, // Meaningful only, if the flag ofConvTextToOutlines is set. If set, text of non-embedded fonts are converted to outlines only.
                                            // Embedded fonts remain embedded.
      ofIgnoreDeviceN         = 0x01000000, // If set, DeviceN color spaces are left unchanged.
      ofIgnoreNChannel        = 0x02000000, // If set, NChannel color spaces are left unchanged.
      ofIgnoreSeparation      = 0x04000000, // If set, Separation color spaces are left unchanged.
      ofFailOnOverprinting    = 0x08000000, // Meaningful only if a color conversion flag is set. If set, the function returns with a fatal error if an object with enabled
                                            // overprinting was found on the page. The appearance of overprinted objects would change and make the page maybe unusable.
                                            // Overprinted objects can be left unchanged (set the flag ofSkipOverprintedObj in this case) or Optimize() can return with a fatal
                                            // error to avoid the creation of an invalid document.
      ofSkipOverprintedObj    = 0x10000000, // Meaningful only if a color conversion flag is set. If set, objects that would be rendered with enabled overprinting are left
                                            // unchanged to avoid color errors on the page.
      ofRemoveBatesNumbers    = 0x20000000, // Remove bates numbers if any. The type BatesN can also contain headers / footers.
      ofRemoveHeaderFooter    = 0x40000000, // Remove headers / footers if any. This type is set if no bates numbering was added.
      ofIgnoreSeparationAll   = 0x80000000  // Meaningful only if a color conversion flag is set. If set, Separation color spaces with the special colorant "All" will be ignored.
   }

   public enum TOptimizeFlags2
   {
      of2Default               = 0x00000000, // Nothing to do
      of2DeleteWatermarks      = 0x00000001, // Delete watermark templates
      of2DeleteWatermarkAnnots = 0x00000002  // If set, watermark annotations will be deleted.
   }

   public enum TOptimizeFlags3
   {
      of3Default              = 0x00000000  // Nothing to do
   }

   public enum TOptimizeFlags4
   {
      of4Default              = 0x00000000  // Nothing to do
   }

   public class TOptimizeParams
   {
      public int[]              ExcludeCS;      // Array of color space handles which should be excluded from color conversion.

      public TOptimizeFlags2    Flags2;         // Additional conversion flags
      public TOptimizeFlags3    Flags3;         // Additional conversion flags
      public TOptimizeFlags4    Flags4;         // Additional conversion flags

      public int                Min1BitRes;     // Minimum resolution before scaling.
      public int                MinGrayRes;     // Minimum resolution before scaling.
      public int                MinColorRes;    // Minimum resolution before scaling.

      public int                Res1BitImages;  // 1 bit black & white images.
      public int                ResGrayImages;  // Gray images.
      public int                ResColorImages; // Multi-channel images.

      public TCompressionFilter Filter1Bit;     // Filter for black & white images.
      public TCompressionFilter FilterGray;     // Filter for gray images.
      public TCompressionFilter FilterColor;    // Filter for multi-channel images.
      public int                JPEGQuality;    // JPEG quality.
      public int                JP2KQuality;    // JPEG 2000 quality.
      public float              MinLineWidth;   // Zero means no hair line removal.
   }

   public enum TOrigin
   {
      orDownLeft,
      orTopLeft
   }

   public enum TPageBoundary
   {
      pbArtBox,
      pbBleedBox,
      pbCropBox,
      pbTrimBox,
      pbMediaBox
   }

   public enum TPageCoord
   {
      pcBottomUp,
      pcTopDown
   }

   public enum TPageFormat
   {
      pfDIN_A3,
      pfDIN_A4,
      pfDIN_A5,
      pfDIN_B4,
      pfDIN_B5,
      pfDIN_B6,
      pfDIN_C3,
      pfDIN_C4,
      pfDIN_C5,
      pfDIN_C6,
      pfDIN_C65,
      pfDIN_DL,
      pfDIN_E4,
      pfDIN_E5,
      pfDIN_E6,
      pfDIN_E65,
      pfDIN_M5,
      pfDIN_M65,
      pfUS_Legal,
      pfUS_Letter,
      pfDIN_A0,
      pfDIN_A1,
      pfDIN_A2,
      pfDIN_A6,
      pfDIN_A7,
      pfDIN_A8,
      pfDIN_A9,
      pfDIN_A10
   }

   public enum TPageLabelFormat
   {
      plfDecimalArabic,    // 1,2,3,4...
      plfUppercaseRoman,   // I,II,III,IV...
      plfLowercaseRoman,   // i,ii,iii,iv...
      plfUppercaseLetters, // A,B,C,D...
      plfLowercaseLetters, // a,b,c,d...
      plfNone
   }

   public enum TPageLayout
   {
      plSinglePage,
      plOneColumn,
      plTwoColumnLeft,
      plTwoColumnRight,
      plTwoPageLeft,    // PDF 1.5
      plTwoPageRight,   // PDF 1.5
      plDefault         // Use viewer's default settings
   }

   public enum TPageMode
   {
      pmUseNone,
      pmUseOutlines,
      pmUseThumbs,
      pmFullScreen,
      pmUseOC,         // PDF 1.5
      pmUseAttachments // PDF 1.6
   }

   public enum TParseFlags : uint
   {
      pfNone              = 0x00000000,
      pfDecomprAllImages  = 0x00000002, // This flag causes that all image formats will be decompressed
                                        // with the exception of JBIG2 compressed images. If this flag is
                                        // not set, images which are already stored in a valid image file
                                        // format are returned as is. This is the case for Gray and RGB JPEG
                                        // images and for JPEG2000 images. If you want to extract the images
                                        // of a PDF file this flag should NOT be set!

      pfNoJPXDecode       = 0x00000004, // Meaningful only if the flag pfDecomprAllImages is set. If set,
                                        // JPEG2000 images are returned as is so that you can use your own
                                        // library to decompress such images.

      // The following flags are ignored if an image is not decompressed. Note that only one flag must be set
      // at time. If no color space conversion flag is set images are returned in their native or alternate
      // device color space. Note that these flags do not convert colors of vector graphics and so on.
      // Use the function ConvColor() to convert colors of the graphics state into a device space.
      pfDitherImagesToBW  = 0x00000008, // Floyd-Steinberg dithering.
      pfConvImagesToGray  = 0x00000010,
      pfConvImagesToRGB   = 0x00000020,
      pfConvImagesToCMYK  = 0x00000040,
      pfImageInfoOnly     = 0x00000080, // If set, images are not decompressed. This flag is useful if you want
                                        // to enumerate the images of a PDF file or if you want to determine how
                                        // many images are stored in it.
                                        // Note that images can be compressed with multiple filters. The member
                                        // Filter of the structure TPDFImage contains only the last filter with
                                        // which the image was compressed. There is no indication whether multiple
                                        // decode filters are required to decompress the image buffer. So, it
                                        // makes no sense to set this flag if you want to try to decompress the
                                        // image buffer manually with your own decode filters.
      pfErrorCheckOnly   = 0x00000100,  // If set, images are checked for decompression errors only. Used internally by CheckConformance().
      pfFailOnError      = 0x80000000   // Fail on decompression errors. Used internally by CheckConformance().
   }

   public enum TPathFillMode
   {
      fmFillNoClose,
      fmStrokeNoClose,
      fmFillStrokeNoClose,
      fmFill,
      fmStroke,
      fmFillStroke,
      fmFillEvOdd,
      fmFillStrokeEvOdd,
      fmFillEvOddNoClose,
      fmFillStrokeEvOddNoClose,
      fmNoFill,
      fmClose  // Close the path, but do not fill or stroke it. More vertices are required or stroke the path in a separate step.
   }

   public enum TPatternType
   {
      ptColored,
      ptUnColored,
      ptShadingPattern
   }

   public enum TPDFFontFlags
   {
      pffFixedPitch  = 0x00000001,
      pffSerifs      = 0x00000002,
      pffSymbolic    = 0x00000004,
      pffScriptStyle = 0x00000008,
      pffReserved1   = 0x00000010,
      pffNonSymbolic = 0x00000020,
      pffItalic      = 0x00000040,
      pffAllCap      = 0x00010000,
      pffSmallCap    = 0x00020000,
      pffForceBold   = 0x00040000
   }

   public enum TPDFMarkInfo
   {
      miNotTagged      = 0,
      miTagged         = 1,
      miSuspects       = 2,
      miUserProperties = 4
   }

   public struct TPDFAnnotation
   {
      public TAnnotType      Type;
      public bool            Deleted;
      public TPDFRect        BBox;
      public double          BorderWidth;
      public uint            BorderColor;
      public TBorderStyle    BorderStyle;
      public uint            BackColor;
      public int             Handle;
      public String          Author;
      public String          Content;
      public String          Name;
      public String          Subject;
      public int             PageNum;
      public THighlightMode  HighlightMode;
   }

   public struct TPDFAnnotationEx
   {
      public TAnnotType      Type;
      public bool            Deleted;
      public TPDFRect        BBox;
      public float           BorderWidth;
      public uint            BorderColor;
      public TBorderStyle    BorderStyle;
      public uint            BackColor;
      public int             Handle;
      public String          Author;
      public String          Content;
      public String          Name;
      public String          Subject;
      public int             PageNum;
      public THighlightMode  HighlightMode;
      public int             DestPage;
      public TPDFRect        DestPos;
      public TDestType       DestType;
      public String          DestFile;         // File link or web link annotations
      public int             Icon;             // The Icon type depends on the annotation type. If the annotation type is atText then the Icon
                                               // is of type TAnnotIcon. If the annotation type is atFileAttach then it is of type
                                               // TFileAttachIcon. If the annotation type is atStamp then the Icon is the stamp type (TRubberStamp).
                                               // For any other annotation type this value is not set (-1).
      public String          StampName;        // Set only, if Icon == rsUserDefined
      public uint            AnnotFlags;       // See TAnnotFlags for available flags

      public String          CreateDate;       // Creation Date -> Optional
      public String          ModDate;          // Modification Date -> Optional
      public bool            Grouped;          // (Reply type) Meaningful only if Parent != -1 and Type != atPopUp. If true,
                                               // the annotation is part of an annotation group. Properties like Content, CreateDate,
                                               // ModDate, BackColor, Subject, and Open must be taken from the parent annotation.
      public bool            Open;             // Meaningful only for annotations which have a corresponding PopUp annotation.
      public int             Parent;           // Parent annotation handle of a PopUp Annotation or the parent annotation if
                                               // this annotation represents a state of a base annotation. In this case,
                                               // the annotation type is always atText and only the following members should
                                               // be considered:
                                               //    State      // The current state
                                               //    StateModel // Marked, Review, and so on
                                               //    CreateDate // Creation Date
                                               //    ModDate    // Modification Date
                                               //    Author     // The user who has set the state
                                               //    Content    // Not displayed in Adobe's Acrobat...
                                               //    Subject    // Not displayed in Adobe's Acrobat...
                                               // The PopUp annotation of a text annotation which represent an Annotation State
                                               // must be ignored.
      public int             PopUp;            // Handle of the corresponding PopUp annotation if any.
      public String          State;            // The state of the annotation.
      public String          StateModel;       // The state model (Marked, Review, and so on).
      public int             EmbeddedFile;     // FileAttach annotations only. A handle of an embedded file -> GetEmbeddedFile().
      public String          Subtype;          // Set only, if Type == atUnknownAnnot
      public int             PageIndex;        // The page index is used to sort form fields. See SortFieldsByIndex().
      public bool            MarkupAnnot;      // If true, the annotation is a markup annotation. Markup annotations can be flattened
                                               // separately, see FlattenAnnots().
      public float           Opacity;          // Opacity = 1.0 = Opaque, Opacity < 1.0 = Transparent, Markup annotations only
      public float[]         QuadPoints;       // Highlight, Link, and Redact annotations only. The array contains the raw floating point values.
                                               // Since a quadpoint requires always four coordinate pairs, the number of QuadPoints is QuadPointsCount divided by 8.

      public float[]         DashPattern;      // Only present if BorderStyle == bsDashed

      public String          Intent;           // Markup annotations only. The intent allows to distinguish between different uses of an annotation.
                                               // For example, line annotations have two intents: LineArrow and LineDimension.
      public TLineEndStyle   LE1;              // Line end style of the start point -> Line and PolyLine annotations only
      public TLineEndStyle   LE2;              // Line end style of the end point -> Line and PolyLine annotations only
      public float[]         Vertices;         // Line, PolyLine, and Polygon annotations only. The array contains the raw floating point values.
                                               // Since a vertice requires always two coordinate pairs, the number of vertices
                                               // or points is VerticeCount divided by 2.

      // Line annotations only. These properties should only be considered if the member Intent is set to the string LineDimension.
      public bool            Caption;          // If true, the annotation string Content is used as caption. The string is shown in a PopUp annotation otherwise.
      public float           CaptionOffsetX;   // Horizontal offset of the caption from its normal position
      public float           CaptionOffsetY;   // Vertical offset of the caption from its normal position
      public TLineCaptionPos CaptionPos;       // The position where the caption should be drawn if present
      public float           LeaderLineLen;    // Length of the leader lines (positive or negative)
      public float           LeaderLineExtend; // Optional leader line extend beyond the leader line (must be a positive value or zero)
      public float           LeaderLineOffset; // Amount of space between the endpoints of the annotation and the leader lines (must be a positive value or zero)

      public TBorderEffect   BorderEffect;     // Circle, Square, FreeText, and Polygon annotations.
      public IntPtr[]        InkList;          // Ink annotations only. Array of array. The sub arrays can be accessed with GetInkList().
      public String          RichStyle;        // Optional default style string.      -> FreeText annotations only.
      public String          RichText;         // Optional rich text string (RC key). -> Markup annotations only.
      public int             OC;               // Handle of an OCG or OCMD or -1 if not set. See help file for further information.

      public float[]         RD;               // Caret, Circle, Square, and FreeText annotations.
      public int             Rotate;           // Caret annotations only. Must be zero or a multiple of 90. This key is not documented in the specs.
   }

   public enum TPDFBarcodeType
   {
      bctNotInitialized    = 0x0000, // No valid value
      bctAustraliaPost     = 0x003F, // Australia Post Standard Customer
      bctAustraliaRedir    = 0x0044, // Australia Post Redirection
      bctAustraliaReply    = 0x0042, // Australia Post Reply Paid
      bctAustraliaRout     = 0x0043, // Australia Post Routing
      bctAztec             = 0x005C, // Aztec Code
      bctAztecRunes        = 0x0080, // Aztec Runes
      bctC2Of5IATA         = 0x0004, // Code 2 of 5 IATA
      bctC2Of5Industrial   = 0x0007, // Code 2 of 5 Industrial
      bctC2Of5Interleaved  = 0x0003, // Interleaved 2 of 5
      bctC2Of5Logic        = 0x0006, // Code 2 of 5 Data Logic
      bctC2Of5Matrix       = 0x0002, // Standard Code 2 of 5
      bctChannelCode       = 0x008C, // Channel Code
      bctCodabar           = 0x0012, // Codabar
      bctCodablockF        = 0x004A, // Codablock-F
      bctCode11            = 0x0001, // Code 11
      bctCode128           = 0x0014, // Code 128 (automatic subset switching)
      bctCode128B          = 0x003C, // Code 128 (Subset B)
      bctCode16K           = 0x0017, // Code 16K
      bctCode32            = 0x0081, // Code 32
      bctCode39            = 0x0008, // Code 3 of 9 (Code 39)
      bctCode49            = 0x0018, // Code 49
      bctCode93            = 0x0019, // Code 93
      bctCodeOne           = 0x008D, // Code One
      bctDAFT              = 0x005D, // DAFT Code
      bctDataBarOmniTrunc  = 0x001D, // GS1 DataBar-14 Omnidirectional and truncated / RSS14
      bctDataBarExpStacked = 0x0051, // GS1 DataBar Expanded Stacked
      bctDataBarExpanded   = 0x001F, // GS1 DataBar Expanded / RSS_EXP
      bctDataBarLimited    = 0x001E, // GS1 DataBar Limited  / RSS_LTD
      bctDataBarStacked    = 0x004F, // GS1 DataBar-14 Stacked
      bctDataBarStackedO   = 0x0050, // GS1 DataBar-14 Stacked Omnidirectional
      bctDataMatrix        = 0x0047, // Data Matrix ECC200
      bctDotCode           = 0x0073, // DotCode
      bctDPD               = 0x0060, // DPD code
      bctDPIdentCode       = 0x0016, // Deutsche Post Identcode
      bctDPLeitcode        = 0x0015, // Deutsche Post Leitcode
      bctEAN128            = 0x0010, // GS1-128 (UCC.EAN-128)
      bctEAN128_CC         = 0x0083, // Composite Symbol with GS1-128 linear component
      bctEAN14             = 0x0048, // EAN-14
      bctEANX              = 0x000D, // EAN
      bctEANX_CC           = 0x0082, // Composite Symbol with EAN linear component
      bctEANXCheck         = 0x000E, // EAN + Check Digit
      bctExtCode39         = 0x0009, // Extended Code 3 of 9 (Code 39+)
      bctFIM               = 0x0031, // FIM
      bctFlattermarken     = 0x001C, // Flattermarken
      bctHIBC_Aztec        = 0x0070, // HIBC Aztec Code
      bctHIBC_CodablockF   = 0x006E, // HIBC Codablock-F
      bctHIBC_Code128      = 0x0062, // HIBC Code 128
      bctHIBC_Code39       = 0x0063, // HIBC Code 39
      bctHIBC_DataMatrix   = 0x0066, // HIBC Data Matrix ECC200
      bctHIBC_MicroPDF417  = 0x006C, // HIBC MicroPDF417
      bctHIBC_PDF417       = 0x006A, // HIBC PDF417
      bctHIBC_QR           = 0x0068, // HIBC QR Code
      bctISBNX             = 0x0045, // ISBN (EAN-13 with verification stage)
      bctITF14             = 0x0059, // ITF-14
      bctJapanPost         = 0x004C, // Japanese Postal Code
      bctKIX               = 0x005A, // Dutch Post KIX Code
      bctKoreaPost         = 0x004D, // Korea Post
      bctLOGMARS           = 0x0032, // LOGMARS
      bctMailmark          = 0x0079, // Royal Mail 4-State Mailmark
      bctMaxicode          = 0x0039, // Maxicode
      bctMicroPDF417       = 0x0054, // Micro PDF417
      bctMicroQR           = 0x0061, // Micro QR Code
      bctMSIPlessey        = 0x0047, // MSI Plessey
      bctNVE18             = 0x004B, // NVE-18
      bctPDF417            = 0x0037, // PDF417
      bctPDF417Truncated   = 0x0038, // PDF417 Truncated
      bctPharmaOneTrack    = 0x0033, // Pharmacode One-Track
      bctPharmaTwoTrack    = 0x0035, // Pharmacode Two-Track
      bctPLANET            = 0x0052, // PLANET
      bctPlessey           = 0x0056, // Plessey
      bctPostNet           = 0x0028, // PostNet
      bctPZN               = 0x0034, // PZN
      bctQRCode            = 0x003A, // QR Code
      bctRMQR              = 0x0091, // Rectangular Micro QR Code (rMQR)
      bctRoyalMail4State   = 0x0046, // Royal Mail 4 State (RM4SCC)
      bctRSS_EXP_CC        = 0x0086, // Composite Symbol with GS1 DataBar Extended component
      bctRSS_EXPSTACK_CC   = 0x008B, // Composite Symbol with GS1 DataBar Expanded Stacked component
      bctRSS_LTD_CC        = 0x0085, // Composite Symbol with GS1 DataBar Limited component
      bctRSS14_CC          = 0x0084, // Composite Symbol with GS1 DataBar-14 linear component
      bctRSS14Stacked_CC   = 0x0089, // Composite Symbol with GS1 DataBar-14 Stacked component
      bctRSS14StackOMNI_CC = 0x008A, // Composite Symbol with GS1 DataBar-14 Stacked Omnidirectional component
      bctTelepen           = 0x0020, // Telepen Alpha
      bctTelepenNumeric    = 0x0057, // Telepen Numeric
      bctUltracode         = 0x0090, // Ultracode
      bctUPCA              = 0x0022, // UPC A
      bctUPCA_CC           = 0x0087, // Composite Symbol with UPC A linear component
      bctUPCACheckDigit    = 0x0023, // UPC A + Check Digit
      bctUPCE              = 0x0025, // UCP E
      bctUPCE_CC           = 0x0088, // Composite Symbol with UPC E linear component
      bctUPCECheckDigit    = 0x0026, // UPC E + Check Digit
      bctUPNQR             = 0x008F, // UPNQR (Univerzalni Placilni Nalog QR)
      bctUSPSOneCode       = 0x0055, // USPS OneCode
      bctVIN               = 0x0049  // Vehicle Identification Number (America)
   }

   public enum TPDFBarcodeDataType
   {
      bcdtBinary     = 0, // Binary data.
      bcdtUnicode    = 1, // UTF-8.
      bcdtGS1Mode    = 2, // Encodes GS1 data using FNC1 characters.
      bcdtEscapeMode = 8  // Scan input data for escape sequences. This is a flag that can be combined
                          // with the other constants, e.g. TBarcodeDataType(bcdtUnicode | bcdtEscapeMode).
   }

   // The following flags can be combined with a binary or operator, e.g. TBarcodeOptions(bcoNoASCII | bcoDottyMode).
   public enum TPDFBarcodeOptions
   {
      bcoDefault          = 0x00000000, // Use default settings.
      bcoNoASCII          = 0x00000001, // Consider non-ASCII characters when creating the barcode.
      bcoBind             = 0x00000002, // Boundary bars above and below the symbol and between rows if stacking multiple symbols.
      bcoBox              = 0x00000004, // Add a box surrounding the symbol and whitespace.
      bcoReaderInit       = 0x00000010, // Add a reader initialisation symbol to the data before encoding.
      bcoSmallText        = 0x00000020, // Use a smaller font for human readable text.
      bcoBoldText         = 0x00000040, // Embolden human readable text.
      bcoDottyMode        = 0x00000100, // Plot a matrix symbol using dots rather than squares.
      bcoGS1_GS_Separator = 0x00000200, // Use GS instead FNC1 as GS1 separator.
      bcoImageOutput      = 0x01000000, // Draw the barcode as an image. Images are usually larger than vectors and require more processing time.
      bcoUseActiveFont    = 0x02000000  // Vector output only. If set, text is output with the active font. Helvetica is used otherwise.
   }

   // Structure to create a barcode on a page
   [StructLayout(LayoutKind.Sequential, Pack = 0)]
   public struct TPDFBarcode2
   {
      public int                 StructSize;  // Must be set to sizeof(TPDFBarcode2).
      public TPDFBarcodeType     BarcodeType; // The type of barcode that should be created.
      public int                 BgColor;     // Background color -> Default NO_COLOR (transparent).
      public int                 BorderWidth; // Border width -> default 0.
      [MarshalAs(UnmanagedType.LPStr)]
      public String              Data;        // Data buffer.
      public TPDFBarcodeDataType DataType;    // Data type and escape flag.
      public float               DotSize;     // Dotty mode only -> Default 4.0/5.0.
      public int                 Eci;         // Extended Channel Interpretation mode -> Default 0.
      public int                 FgColor;     // RGB Foreground color -> Default 0.
      public float               FSizeFactor; // Font size correction factor -> Default 0.7. Considered in vector mode only.
      public int                 Option1;     // Various, depends on barcode type. See help file for further information. -> Default -1.
      public int                 Option2;     // Various, depends on barcode type. See help file for further information. -> Default 0.
      public int                 Option3;     // Various, depends on barcode type. See help file for further information. -> Default 0.
      public TPDFBarcodeOptions  Options;     // Output options.
      public int                 Orientation; // Supported values: 0, 90, -90, 180, -180, 270, -270.
      [MarshalAs(UnmanagedType.LPStr)]
      public String              Primary;     // Composite codes only. Primary data message (max 127 bytes).
      public float               Scale;       // Default 1.0.
      public lbool               ShowText;    // Show human readable text? Default true.
      public int                 SpaceWidth;  // Whitespace width in pixels -> Default 0 (auto).
      public int                 SymbHeight;  // Default 50.
      public float               TextOffsetY; // Offset to correct the y-coordinate of human readable text -> Default 0.
      public TFltRect            OuterBorder; // Optional border around the scaled barcode measured in user space.
   }

   // The tags have the same meaning as the corresponding HTML tags.
   // See PDF Reference for further information.
   public enum TPDFBaseTag
   {
      btArt,
      btArtifact,
      btAnnot,
      btBibEntry,      // BibEntry -> Bibliography entry
      btBlockQuote,
      btCaption,
      btCode,
      btDiv,
      btDocument,
      btFigure,
      btForm,
      btFormula,
      btH,
      btH1,
      btH2,
      btH3,
      btH4,
      btH5,
      btH6,
      btIndex,
      btLink,
      btList,          // L
      btListElem,      // LI
      btListText,      // LBody
      btNote,
      btP,
      btPart,
      btQuote,
      btReference,
      btSection,       // Sect
      btSpan,
      btTable,
      btTableDataCell, // TD
      btTableHeader,   // TH
      btTableRow,      // TR
      btTOC,
      btTOCEntry,      // TOCI
      btCL,            // CL (Continued List)
      btLabel,         // Lbl
      btRuby,
      btWarichu,
      // PDF 2.0 tags:
      btAbbr,
      btCallout,
      btDek,
      btEm,
      btLineNum,
      btReason,
      btRedaction,
      btSidebar,
      btStrong,
      btSub,
      btSup
   }

   public struct TPDFBarcode
   {
      public String Caption;      // Optional, the ansi string takes precedence
      public float  ECC;          // 0..8 for PDF417, or 0..3 for QRCode
      public float  Height;       // Height in inches
      public float  nCodeWordCol; // Required for PDF417. The number of codewords per barcode coloumn.
      public float  nCodeWordRow; // Required for PDF417. The number of codewords per barcode row.
      public int    Resolution;   // Required -> Should be 300
      public String Symbology;    // PDF417, QRCode, or DataMatrix.
      public float  Version;      // Should be 1
      public float  Width;        // Width in inches
      public float  XSymHeight;   // Only needed for PDF417. The vertical distance between two barcode modules,
                                  // measured in pixels. The ratio XSymHeight/XSymWidth shall be an integer
                                  // value. For PDF417, the acceptable ratio range is from 1 to 4. For QRCode
                                  // and DataMatrix, this ratio shall always be 1.
      public float XSymWidth;     // Required -> The horizontal distance, in pixels, between two barcode modules.
   }

   /*
      Notice:
      When using a bidirectional 8 bit code page the bidi algorithm is applied by default in Left to Right mode
      also if the bidi mode is set to bmNone (default). This mode produces identical results in comparison to
      applications like Edit or WordPad.

      The Right to Left mode is available in applications which use Microsoft's Uniscribe, e.g. BabelPad. This
      mode works very well with the Reference Bidi Algorithm which is used by DynaPDF.

      However, Uniscribe's Left to Right mode produces different results in comparison to the Reference Bidi
      Algorithm. Because the bidi algorithm that is used in Uniscribe is not published it is practically
      impossible to get the same result in Left to Right mode without using this library.
   */
   public enum TPDFBidiMode
   {
      bmLeftToRight, // Apply the bidi algorithm on Unicode strings in Left to Right layout.
      bmRightToLeft, // Apply the bidi algorithm on Unicode strings in Right to Left layout.
      bmNone         // Default -> not apply the bidi algorithm
   }

   [StructLayout(LayoutKind.Sequential, Pack = 0)]
   public struct TPDFBitmap
   {
      public int    StructSize; // Must be set to sizeof(TPDFBitmap)
      public IntPtr Buffer;     // Image buffer
      public int    BufSize;    // Buffer size in bytes
      public int    DestX;      // Destination x-coordinate on the main image (the rendered page)
      public int    DestY;      // Destination y-coordinate on the main image (the rendered page)
      public int    Height;     // Image height in pixels
      public int    Stride;     // Scanline length in bytes
      public int    Width;      // Image width
   }

   public struct TPDFBookmark
   {
      public int       Action;     // Action handle or -1 if not set.
      public uint      Color;      // Optional text color. Black is the default value (0).
      public int       DestPage;   // Destination page.
      public TPDFRect  DestPos;    // Destination position.
      public TDestType DestType;   // Destination type.
      public String    NamedDest;  // Optional name of a named destination. If set, DestPage, DestPos, and DestType were already taken from the destination if it was found.
      public bool      Open;       // If true, child nodes should be visible.
      public int       Parent;     // The immediate parent node or -1 for a root node.
      public IntPtr    StructElem; // Bookmarks can contain a reference to a structure element. There is no API to access structure elements yet.
      public TBmkStyle Style;      // The font style that should be used to render this node.
      public String    Title;      // The bookmark title.
   };

   public struct TPDFChoiceValue
   {
      public String ExpValue;
      public String Value;
      public bool   Selected;
   }

   /*
      The structure contains several duplicate fields because CMap files contain usually a DSC comment
      section which provides Postscript specific initialization code. With exception of DSCResName the
      strings in the DSC section should not differ from their CMap counterparts. The Identity mapping
      of a character collection should contain the DSC comment "%%BeginResource: CMap (Identity)".
      Otherwise the string should be set to the CMap name.
   */
   [StructLayout(LayoutKind.Sequential, Pack = 0)]
   public struct TPDFCMap
   {
      public int    StructSize;     // Must be set to sizeof(TPDFCMap) before calling GetCMap()!
      [MarshalAs(UnmanagedType.LPStr)]
      public String BaseCMap;       // If set, this base cmap is required when loading the cmap.
      public uint   CIDCount;       // 0 if not set.
      [MarshalAs(UnmanagedType.LPStr)]
      public String CMapName;       // The CMap name.
      public uint   CMapType;       // Should be 1!
      public float  CMapVersion;    // The CMap version.
      [MarshalAs(UnmanagedType.LPStr)]
      public String DSCBaseCMap;    // DSC comment.
      public float  DSCCMapVersion; // DSC comment.
      [MarshalAs(UnmanagedType.LPStr)]
      public String DSCResName;     // DSC comment. If the CMap uses an Identity mapping this string should be set to Identity.
      [MarshalAs(UnmanagedType.LPStr)]
      public String DSCTitle;       // DSC comment -> DSC CMap name + Registry + Ordering + Supplement, e.g. "GB-EUC-H Adobe GB1 0"
      [MarshalAs(UnmanagedType.LPStr)]
      public String FileNameA;      // The file name and CMap name should be identical!
      [MarshalAs(UnmanagedType.LPWStr)]
      public String FileNameW;      // The file name and CMap name should be identical!
      [MarshalAs(UnmanagedType.LPStr)]
      public String FilePathA;      // The Ansi string is set if the Ansi version of SetCMapDir() was used.
      [MarshalAs(UnmanagedType.LPWStr)]
      public String FilePathW;      // The Unicode string is set if the Unicode version of SetCMapDir() was used.
      [MarshalAs(UnmanagedType.LPStr)]
      public String Ordering;       // CIDSystemInfo -> The Character Collection, e.g. Japan1.
      [MarshalAs(UnmanagedType.LPStr)]
      public String Registry;       // CIDSystemInfo -> The registrant of the Character Collection is usually Adobe.
      public uint   Supplement;     // CIDSystemInfo -> The Supplement number should be supported in the used PDF Version.
      public uint   WritingMode;    // 0 == Horizontal, 1 == Vertical
   }

   public enum TPDFColSplitInfo
   {
      spiNone,       // Do not display a splitter
      spiHorizontal, // Display a horizontal splitter
      spiVertical    // Display a vertical splitter
   }

   public struct TPDFCollectionInfo
   {
      public int              InitalFile; // Possible values are -2, -1, or a valid embedded files handle (a value greater or equal zero). See help file for further information.
      public TColView         InitalView; // Determines how embedded files should be displayed.
      public String           SortBy;     // Optional name of the first field that should be used to sort the list of embedded files.
      public bool             SortDesc;   // If true, the list of embedded files should be sorted in descending order.
      public TPDFColSplitInfo SplitInfo;  // Determines whether a splitter bar should be displayed.
      public float            SplitPos;   // Meaningful only, if SplitInfo is not spiNone. The initial position of the splitter bar,
                                          // specified as a percentage of the available window area (0 to 100).
   };

   public enum TPDFColorSpace
   {
      csDeviceRGB,
      csDeviceCMYK,
      csDeviceGray
   }

   public struct TPDFColorSpaceObj
   {
      public TExtColorSpace Type;
      public TExtColorSpace Alternate;        // Alternate color space or base space of an Indexed or Pattern color space.
      public IntPtr         IAlternate;       // Only set if the color space contains an alternate or base color space -> GetColorSpaceObjEx().
      public IntPtr         Buffer;           // Contains either an ICC profile or the color table of an Indexed color space.
      public int            BufSize;          // Buffer length in bytes.
      public float[]        BlackPoint;       // CIE blackpoint. If set, the array contains exactly 3 values.
      public float[]        WhitePoint;       // CIE whitepoint. If set, the array contains exactly 3 values.
      public float[]        Gamma;            // If set, one value per component.
      public float[]        Range;            // min/max for each component or for the .a and .b components of a Lab color space.
      public float[]        Matrix;           // XYZ matrix. If set, the array contains exactly 9 values.
      public int            NumInComponents;  // Number of input components.
      public int            NumOutComponents; // Number of output components.
      public int            NumColors;        // HiVal + 1 as specified in the color space. Indexed color space only.
      public String[]       Colorants;        // Colorant names (Separation, DeviceN, and NChannel only).
      public IntPtr         Metadata;         // Optional XMP metadata stream -> ICCBased only.
      public int            MetadataSize;     // Metadata length in bytes.
      public IntPtr         IFunction;        // Pointer to function object -> Separation, DeviceN, and NChannel only.
      public IntPtr         IAttributes;      // Optional attributes of DeviceN or NChannel color spaces -> GetNChannelAttributes().
      public IntPtr         IColorSpaceObj;   // Pointer of the corresponding color space object
      public int            Handle;           // Handle of the color space object or -1 if an internal color space object was returned by ParseContent(), for example.
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TDeviceNAttributes
   {
      public IntPtr   IProcessColorSpace;  // Pointer to process color space or NULL -> GetColorSpaceEx().
      public String[] ProcessColorants;    // Colorant names
      public IntPtr[] Separations;         // Optional pointers to Separation color spaces -> GetColorSpaceEx().
      public IntPtr   IMixingHints;        // Optional pointer to mixing hints. There is no API function at this time to access mixing hints.
   }

   public enum TPDFDateTime
   {
      dfMM_D,
      dfM_D_YY,
      dfMM_DD_YY,
      dfMM_YY,
      dfD_MMM,
      dfD_MMM_YY,
      dfDD_MMM_YY,
      dfYY_MM_DD,
      dfMMM_YY,
      dfMMMM_YY,
      dfMMM_D_YYYY,
      dfMMMM_D_YYYY,
      dfM_D_YY_H_MM_TT,
      dfM_D_YY_HH_MM,
      /* time formats */
      df24HR_MM,
      df12HR_MM,
      df24HR_MM_SS,
      df12HR_MM_SS
   }

   public struct TPDFError
   {
      public String Message; // The error message
      public int    ObjNum;  // -1 if not available
      public int    Offset;  // -1 if not available
      public String SrcFile; // Source file
      public int    SrcLine; // Source line
      public int    ErrCode; // ErrCode and ErrType are set if the error mode was set to emUseErrLog. See SetErrorMode() for further information.
      public int    ErrType; // ErrCode and ErrType are set if the error mode was set to emUseErrLog. See SetErrorMode() for further information.
   }

   // It is not possible to set all available graphic state parameters with DynaPDF, such as black generation functions,
   // halftones and so on. The structure will be extended when further objects are supported.
   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TPDFExtGState
   {
      public int              AutoStrokeAdjust; // PDF_MAX_INT if not set
      public TBlendMode       BlendMode;        // Default bmNotSet
      public float            FlatnessTol;      // -1.0 if not set
      public int              OverPrintFill;    // PDF_MAX_INT if not set
      public int              OverPrintStroke;  // PDF_MAX_INT if not set
      public int              OverPrintMode;    // PDF_MAX_INT if not set
      public TRenderingIntent RenderingIntent;  // riNone if not set
      public float            SmoothnessTol;    // -1.0 if not set
      public float            FillAlpha;        // -1.0 if not set
      public float            StrokeAlpha;      // -1.0 if not set
      public int              AlphaIsShape;     // PDF_MAX_INT if not set
      public int              TextKnockout;     // PDF_MAX_INT if not set
      public int              SoftMaskNone;     // Disables the active soft mask
      public IntPtr           SoftMask;         // Soft mask pointer or IntPtr.Zero. See CreateSoftMask() for further information.
      public IntPtr           Reserved1;
      public IntPtr           Reserved2;
      public IntPtr           Reserved3;
      public IntPtr           Reserved4;
      public IntPtr           Reserved5;
      public IntPtr           Reserved6;
      public IntPtr           Reserved7;
   }

   // Extended graphics state dictionary
   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TPDFExtGState2
   {
      public lbool            AlphaIsShape;       // PDF_MAX_INT if not set
      public lbool            AutoStrokeAdjust;   // PDF_MAX_INT if not set
      public IntPtr           BlackGen;           // Function handle or NULL -> GetFunction()
      public IntPtr           BlackGen2;          // Function handle or NULL -> GetFunction()
      public IntPtr           BlendMode;          // Array of blend modes -> GetBlendMode()
      public int              BlendModeCount;     // Number of blend modes
      public float            FillAlpha;          // -1.0 if not set
      public float            FlatnessTol;        // -1.0 if not set
      public IntPtr           Halftone;           // Halftone handle or NULL -> GetHalftoneDict()
      public lbool            OverPrintFill;      // PDF_MAX_INT if not set
      public lbool            OverPrintStroke;    // PDF_MAX_INT if not set
      public lbool            OverPrintMode;      // PDF_MAX_INT if not set
      public TRenderingIntent RenderingIntent;    // riNone if not set
      public float            SmoothnessTol;      // -1.0 if not set
      public IntPtr           SoftMask;           // Soft mask handle or NULL
      public float            StrokeAlpha;        // -1.0 if not set
      public lbool            TextKnockout;       // PDF_MAX_INT if not set
      public IntPtr           TransferFunc;       // Array of functions -> GetFunction()
      public int              TransferFuncCount;  // Number of transfer functions
      public IntPtr           TransferFunc2;      // Array of functions -> GetFunction()
      public int              TransferFunc2Count; // Number of transfer functions
      public IntPtr           UnderColorRem;      // Function handle or NULL -> GetFunction()
      public IntPtr           UnderColorRem2;     // Function handle or NULL -> GetFunction()
      public IntPtr           Reserved1;
      public IntPtr           Reserved2;
      public IntPtr           Reserved3;
      public IntPtr           Reserved4;

      public TBlendMode GetBlendMode()
      {
         if (BlendModeCount == 0)
            return TBlendMode.bmNormal;
         else
         {
            int[] modes = new int[BlendModeCount];
            Marshal.Copy(BlendMode, modes, 0, BlendModeCount);
            return (TBlendMode)modes[0];
         }
      }
   }

   public struct TPDFField
   {
      public TFieldType     FieldType;
      public bool           Deleted;
      public TPDFRect       BBox;
      public int            Handle;
      public String         FieldName;
      public TPDFColorSpace BackCS;
      public TPDFColorSpace TextCS;
      public uint           BackColor;
      public uint           BorderColor;
      public uint           TextColor;
      public bool           Checked;
      public int            Parent;
      public int            KidCount;
      public String         Font;
      public double         FontSize;
      public String         Value;
      public String         ToolTip;
   }

   public struct TPDFFieldEx
   {
      public bool            Deleted;       // If true, the field was marked as deleted by DeleteField()
      public TPDFRect        BBox;          // Bounding box of the field in bottom-up coordinates
      public TFieldType      FieldType;     // Field type
      public TFieldType      GroupType;     // If GroupType != FieldType the field is a terminal field of a field group
      public int             Handle;        // Field handle
      public uint            BackColor;     // Background color
      public TExtColorSpace  BackColorSP;   // Color space of the background color
      public uint            BorderColor;   // Border color
      public TExtColorSpace  BorderColorSP; // Color space of the border color
      public TBorderStyle    BorderStyle;   // Border style
      public float           BorderWidth;   // Border width
      public float           CharSpacing;   // Text fields only
      public bool            Checked;       // Check boxes only
      public int             CheckBoxChar;  // ZapfDingbats character that is used to display the on state
      public TCheckBoxState  DefState;      // Check boxes only
      public String          DefValue;      // Optional default value
      public IntPtr          IEditFont;     // Pointer to default editing font
      public String          EditFont;      // Postscript name of the editing font
      public int             ExpValCount;   // Combo and list boxes only. The values can be accessed with GetFieldExpValueEx()
      public String          ExpValue;      // Check boxes only
      public TFieldFlags     FieldFlags;    // Field flags
      public IntPtr          IFieldFont;    // Pointer to the font that is used by the field
      public String          FieldFont;     // Postscript name of the font
      public double          FontSize;      // Font size. 0.0 means auto font size
      public String          FieldName;     // Note that children of a field group or radio button have no name
      public THighlightMode  HighlightMode; // Highlight mode
      public bool            IsCalcField;   // If true, the OnCalc event of the field is connected with a JavaScript action
      public String          MapName;       // Optional unique mapping name of the field
      public int             MaxLen;        // Text fields only -> zero means not restricted
      public IntPtr[]        Kids;          // Array of child fields -> GetFieldEx2()
      public IntPtr          Parent;        // Pointer to parent field or NULL
      public int             PageNum;       // Page on which the field is used or -1
      public int             Rotate;        // Rotation angle in degrees
      public TTextAlign      TextAlign;     // Text fields only
      public uint            TextColor;     // Text color
      public TExtColorSpace  TextColorSP;   // Color space of the field's text
      public float           TextScaling;   // Text fields only
      public String          ToolTip;       // Optional tool tip
      public String          UniqueName;    // Optional unique name (NM key)
      public String          Value;         // Field value
      public float           WordSpacing;   // Text fields only
      public int             PageIndex;     // The page index is used to sort form fields. See SortFieldsByIndex().
      public IntPtr          IBarcode;      // If present, this field is a barcode field. The field type is set to ftText
                                            // since barcode fields are extended text fields. -> GetBarcodeDict().
      public IntPtr          ISignature;    // Signature fields only. Present only for imported signature fields which
                                            // which have a value. That means the file was digitally signed. -> GetSigDict().
                                            // Signed signature fields are always marked as deleted!
      public String          ModDate;       // Last modification date (optional)

      // Push buttons only. The down and roll over states are optional. If not present, then all states use the up state.
      // The handles of the up, down, and roll over states are template handles! The templates can be opened for editing
      // with EditTemplate2() and parsed with ParseContent().
      public TBtnCaptionPos  CaptionPos;    // Where to position the caption relative to its image
      public String          DownCaption;   // Caption of the down state
      public int             DownImage;     // Image of the down state
      public String          RollCaption;   // Caption of the roll over state
      public int             RollImage;     // Image of the roll over state
      public String          UpCaption;     // Caption of the up state
      public int             UpImage;       // Image of the up state -> if > -1, the button is an image button
      public int             OC;            // Handle of an OCG or OCMD or -1 if not set. See help file for further information.
      public int             Action;        // Action handle or -1 if not set. This action is executed when the field is activated.
      public TActionType     ActionType;    // Meaningful only, if Action >= 0.
      public IntPtr          Events;        // See GetObjEvent() if set.
      public uint            FmtTextColor;  // Zero if not set. Usually RGB red otherwise.
      public String          FmtText;       // Text fields only. The formatted field value if the field contains an OnFormat Javascript action.
   }

   public struct TPDFFileSpec
   {
      public IntPtr Buffer;      // Buffer of an embedded file
      public int    BufSize;     // Buffer size
      public bool   Compressed;  // Should be false if Decompress was true in the GetEmbeddedFile() call, otherwise usually true.
                                 // DynaPDF decompresses Flate encoded streams only. Other filters can occur but this is very unusual.
      public IntPtr ColItem;     // If != IntPtr.Zero the embedded file contains a collection item with user defined data. This entry
                                 // can occur in PDF Collections only (PDF 1.7). See "PDF Collections" in the help file for further
                                 // information.
      public String Name;        // Name of the file specification in the name tree.
      public String FileName;    // File name as 7 bit ASCII string.
      public bool   IsURL;       // If true, FileName contains a URL.
      public String UF;          // PDF 1.7. Same as FileName but Unicode is allowed.
      public String Desc;        // Description
      public int    FileSize;    // Size of the decompressed stream or zero if not known. Note: this is either the Size key of
                                 // the Params dictionary if present or the DL key in the file stream. Whether this value is
                                 // correct depends on the file creator! The parameter is definitely correct if the file was
                                 // decompressed.
      public String MIMEType;    // MIME media type name as defined in Internet RFC 2046.
      public String CreateDate;  // Creation date as string. See help file "The standard date format".
      public String ModDate;     // Modification date as string. See help file "The standard date format".
      public byte[] CheckSum;    // 16 byte MD5 digest.
   }

   public struct TPDFEmbFileNode
   {
      public String       Name;       // UTF-8 encoded name. This key contains usually a 7 bit ASCII string.
      public TPDFFileSpec EF;         // Embedded file.
      public IntPtr       NextNode;   // Next node if any.
   }

   public struct TPDFFileSpecEx
   {
      public String AFRelationship; // PDF 2.0
      public IntPtr ColItem;        // If != NULL the embedded file contains a collection item with user defined data. This entry can
                                    // occur in PDF Collections only (PDF 1.7). See "PDF Collections" in the help file for further information.
      public String Description;    // Optional description string.
      public String DOS;            // Optional DOS file name.
      public IntPtr EmbFileNode;    // GetEmbeddedFileNode().
      public String FileName;       // File name as 7 bit ASCII string.
      public bool   FileNameIsURL;  // If true, FileName contains a URL.
      public byte[] ID1;            // Optional file ID. Meaningful only if FileName points to a PDF file.
      public byte[] ID2;          // Optional file ID. Meaningful only if FileName points to a PDF file.
      public bool   IsVolatile;     // If true, the file changes frequently with time.
      public String Mac;            // Optional Mac file name.
      public String Unix;           // Optional Unix file name.
      public IntPtr RelFileNode;    // Optional related files array. -> GetRelFileNode().
      public IntPtr Thumb;          // Optional thumb nail image. -> GetImageObjEx().
      public String UFileName;      // PDF 1.7. Same as FileName but Unicode is allowed.
   }

   public struct TPDFFontInfo
   {
      public float            Ascent;           // Ascent (optional).
      public float            AvgWidth;         // Average character width (optional).
      public TBaseEncoding    BaseEncoding;     // Valid only if HaveEncoding is true.
      public String           BaseFont;         // PostScript Name or Family Name.
      public float            CapHeight;        // Cap height (optional).
      public String           CharSet;          // The charset describes which glyphs are present in the font.
      public String           CIDOrdering;      // SystemInfo -> A string that uniquely names the character collection within the specified registry.
      public String           CIDRegistry;      // SystemInfo -> Issuer of the character collection.
      public byte[]           CIDSet;           // CID fonts only. This is a table of bits indexed by CIDs.
      public int              CIDSupplement;    // CIDSystemInfo -> The supplement number of the character collection.
      public byte[]           CIDToGIDMap;      // Allowed for embedded TrueType based CID fonts only.
      public byte[]           CMapBuf;          // Only available if the CMap was embedded.
      public String           CMapName;         // CID fonts only (this is the encoding if the CMap is not embedded).
      public float            Descent;          // Descent (optional).
      public char[]           Encoding;         // Unicode mapping 0..255 -> not available for CID fonts.
      public int              FirstChar;        // First char (simple fonts only).
      public TPDFFontFlags    Flags;            // The font flags describe various characteristics of the font. See help file for further information.
      public TBBox            FontBBox;         // This is the size of the largest glyph in this font. The bounding box is important for text selection.
      public byte[]           FontBuffer;       // The font buffer is present if the font was embedded or if it was loaded from a file buffer.
      public String           FontFamily;       // Optional Font Family (Family Name, always available for system fonts).
      public String           FontFilePath;     // Only available for system fonts.
      public TFontFileSubtype FontFileType;     // See description in the help file for further information.
      public String           FontName;         // Font name (should be the same as BaseFont).
      public String           FontStretch;      // Optional -> UltraCondensed, ExtraCondensed, Condensed, and so on.
      public TFontType        FontType;         // If ftType0 the font is a CID font. The Encoding is not set in this case.
      public float            FontWeight;       // Font weight (optional).
      public String           FullName;         // System fonts only.
      public bool             HaveEncoding;     // If true, BaseEncoding was set from the font's encoding.
      public float[]          HorzWidths;       // Horizontal glyph widths.
      public bool             Imported;         // If true, the font was imported from an external PDF file.
      public float            ItalicAngle;      // Italic angle.
      public String           Lang;             // Optional language code defined by BCP 47.
      public int              LastChar;         // Last char (simple fonts only).
      public float            Leading;          // Leading (optional).
      public int              Length1;          // Length of the clear text portion of a Type1 font.
      public int              Length2;          // Length of the encrypted portion of a Type1 font program (Type1 fonts only).
      public int              Length3;          // Length of the fixed-content portion of a Type1 font program or zero if not present.
      public float            MaxWidth;         // Maximum glyph width (optional).
      public byte[]           Metadata;         // Optional XMP stream that contains metadata about the font file.
      public float            MisWidth;         // Missing width (default = 0.0).
      public byte[]           Panose;           // CID fonts only -> Optional 12 bytes long Panose string as described in Microsoft’s TrueType 1.0 Font Files Technical Specification.
      public String           PostScriptName;   // System fonts only.
      public float            SpaceWidth;       // Space width in font units. A default value is set if the font contains no space character.
      public float            StemH;            // The thickness, measured vertically, of the dominant horizontal stems of glyphs in the font.
      public float            StemV;            // The thickness, measured horizontally, of the dominant vertical stems of glyphs in the font.
      public byte[]           ToUnicode;        // Only available for imported fonts. This is an embedded CMap that translates PDF strings to Unicode.
      public TFltPoint        VertDefPos;       // Default vertical displacement vector.
      public TCIDMetric[]     VertWidths;       // Vertical glyph widths.
      public int              WMode;            // Writing Mode -> 0 == Horizontal, 1 == Vertical.
      public float            XHeight;          // The height of lowercase letters (like the letter x), measured from the baseline, in fonts that have Latin characters.
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TPDFFontMetrics
   {
      public int     StructSize;     // Must be set to sizeof(TPDFFontMetrics)
      public float   Ascent;         // Ascent (usWinAscent)
      public float   AvgWidth;       // Average character width
      public float   CapHeight;      // Cap height
      public float   DefWidth;       // Default width
      public float   Descent;        // sTypoDescender of the OS/2 table of TrueType and OpenType fonts
      public int     FirstChar;      // First char
      public float   FixedWidth;     // Width of all glyphs in a fixed pitch font
      public TFStyle FontStyle;      // Font style with which the font was loaded
      public TBBox   FontBBox;       // Font's bounding box
      public lbool   IsFixedPitch;   // Is this a fixed pitch font?
      public lbool   IsSymbolFont;   // Is this is a symbolic font?
      public lbool   IsStdFont;      // Is this is a standard font?
      public float   ItalicAngle;    // Italic angle of italic fonts
      public int     LastChar;       // Last char
      public float   LineGap;        // sTypoLineGap of the OS/2 table of TrueType and OpenType fonts
      public int     MacStyle;       // macOS style flags
      public TFStyle RealStyle;      // This is the real style of the font (bold and italic styles are maybe emulated)
      public float   StemV;          // Width of vertical stems
      public float   StrikeoutPos;   // Strikeout position
      public float   StrikeoutWidth; // Width of the strikeout line
      public float   UnderlinePos;   // Underline position
      public float   UnderlineWidth; // Width of the underline
      public uint    UnicodeRange1;  // Bitset -> TUnicodeRange1
      public uint    UnicodeRange2;  // Bitset -> TUnicodeRange2
      public uint    UnicodeRange3;  // Bitset -> TUnicodeRange3
      public uint    UnicodeRange4;  // Bitset -> TUnicodeRange4
      public int     Weight;         // Font weight
      public int     WidthClass;     // Width class of the font
      public float   WinDescent;     // usWinDescent of the OS/2 table of TrueType and OpenType fonts
      public uint    WinStyle;       // fsSelection of the OS/2 table of TrueType and OpenType fonts
      public float   XHeight;        // X-height
      public float   TypoAscender;   // sTypoAscender of the OS/2 table of TrueType and OpenType fonts
      public float   TypoLeading;    // Typographic leading or line height of the font. This is TypoAscender - Descent + LineGap.
                                     // If typographic metrics are not available then TypoAscender is set to Ascent.
   }

   public struct TPDFFontObj
   {
      public float            Ascent;        // Ascent
      public String           BaseFont;      // PostScript Name or Family Name
      public float            CapHeight;     // Cap height
      public float            Descent;       // Descent
      public char[]           Encoding;      // Unicode mapping 0..255 -> not set if a CID font is selected
      public int              FirstChar;     // First char
      public TPDFFontFlags    Flags;         // Font flags -> font descriptor
      public String           FontFamily;    // Optional Font Family (Family Name)
      public String           FontName;      // Font name -> font descriptor
      public TFontType        FontType;      // If ftType0 the font is a CID font. The Encoding is not set in this case.
      public float            ItalicAngle;   // Italic angle
      public int              LastChar;      // Last char
      public float            SpaceWidth;    // Space width in font units. A default value is set if the font contains no space character.
      public float[]          Widths;        // Glyph widths -> 0..WidthsCount -1
      public float            XHeight;       // x-height
      public float            DefWidth;      // Default character widths -> CID fonts only
      public IntPtr           FontFile;      // Font file buffer -> only imported fonts are returned.
      public int              Length1;       // Length of the clear text portion of the Type1 font, or the length of the entire font program if FontType != ffType1.
      public int              Length2;       // Length of the encrypted portion of the Type1 font program (Type1 fonts only).
      public int              Length3;       // Length of the fixed-content portion of the Type1 font program or zero if not present.
      public TFontFileSubtype FontFileType;  // See description in the help file for further information.
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TFRect
   {
      public Int16 MinX;
      public Int16 MinY;
      public Int16 MaxX;
      public Int16 MaxY;
   }

   public struct TPDFGlyphOutline
   {
      public float       AdvanceX;
      public float       AdvanceY;
      public float       OriginX;
      public float       OriginY;
      public Int16       Lsb;
      public Int16       Tsb;
      public bool        HaveBBox;
      public TFRect      BBox;
      public TI32Point[] Outline;
   }

   public struct TPDFGoToAction
   {
      public int         DestPage;       // Destination page (the first page is denoted by 1).
      public float[]     DestPos;        // Destination position -> Array of 4 floating point values if set.
      public TDestType   DestType;       // Destination type.
      // GoToR (GoTo Remote) actions only:
      public IntPtr      DestFile;       // see GetFileSpec().
      public String      DestName;       // Optional named destination that shall be loaded when opening the file.
      public int         NewWindow;      // Meaningful only if the destination file points to a PDF file.
                                         // -1 = viewer default, 0 = false, 1 = true.
      public int         NextAction;     // -1 or next action handle to be executed if any.
      public TActionType NextActionType; // Only set if NextAction is >= 0.
   }
   
   public struct TPDFHdrFtr
   {
      public uint           Color;    // Text color.
      public TExtColorSpace CS;       // Color space -> default = esInitSpace.
      public int            CSHandle; // Optional color space handle if CS is set to a non-device space

      public TCodepage      Codepage; // The code page that should be used to load the font. It is always set to cpUnicode if the property was loaded with LoadHeaderFooterSettings().
      public bool           Embed;    // If true, the font will be embedded. Always set to false if the property was loaded with LoadHeaderFooterSettings().
      public String         Font;     // Optional font name.
      public float          FontSize; // Font size -> considered only if a font name is set.
      public String         FontType; // Optional font type ("TrueType", "OpenType", or "Type1").

      public bool           IsHeader; // If true, the record is treated as header.
      public TTextAlign     Position; // Valid values are taLeft, taCenter, and taRight.
      public float          ShiftX;   // Positive values move the text to the right, negative to the left.
      public float          ShiftY;   // Positive values move the text up or down depending on the used coordinate system (top down or bottom up). Negative values move the text into the opposite direction.
      public String         Text;    // The text that should be output. Either the Ansi or Unicode string must be set.
   }

   public enum TPDFHdrFtrFlags
   {
      hffDefault    = 3,  // Output header / footer text on even and odd pages
      hffEvenPages  = 1,  // Output header / footer text on even pages
      hffOddPages   = 2,  // Output header / footer text on odd pages
      hffFixedPrint = 4,  // Can be set by LoadHeaderFooterSettings(). Not considered yet.
      hffLoadUTF8   = 8,  // Load all strings in UTF-8 format.
      hffShrink     = 16, // Can be set by LoadHeaderFooterSettings(). Not considered yet.
      hffLoadBatesN = 32, // If set, LoadHeaderFooterSetting() loads bates numbers only.
      hffLoadHF     = 64, // If set, LoadHeaderFooterSetting() loads header / footer settings only.
      hffSearchRun  = 128 // Scan the file for available header / footer types. See LoadHeaderFooterSetting() for further information.
   }

   public struct TPDFHeaderFooter
   {
      public int             FirstPage;      // In/Out -> The first output page.
      public TPDFHdrFtrFlags Flags;          // In/Out -> Additional processing flags.
      public uint            InitColor;      // In/Out -> Initial text color.
      public TExtColorSpace  InitCS;         // In/Out -> Initial color space -> default = esDeviceGray.
      public int             InitCSHandle;   // In     -> Optional color space handle if InitCS is set to a non-device space.
      public String          InitDate;       // Out    -> Initial date format.
      public TCodepage       InitCodepage;   // In/Out -> The code page that should be used to load the font. It is always set to cpUnicode if the property was loaded with LoadHeaderFooterSettings().
      public bool            InitEmbed;      // In     -> If true, the font will be embedded. Always set to false if the property was loaded with LoadHeaderFooterSettings().
      public String          InitFont;       // In/Out -> Optional font name.
      public float           InitFontSize;   // In/Out -> Optional font size. Considered only if a font name is set.
      public String          InitFontType;   // Out    -> Optional font type ("TrueType", "OpenType", or "Type1").
      public String          InitPageFormat; // Out    -> Initial page format string.
      public int             LastPage;       // In/Out -> The last output page.
      public TFltRect        Margin;         // In/Out -> Margin to output the header/footer text.
   };

   public struct TPDFHideAction
   {
      public IntPtr[]    Fields;         // Array of field pointers -> GetFieldEx2().
      public bool        Hide;           // A flag indicating whether to hide or show the fields in the array.
      public int         NextAction;     // -1 or next action handle to be executed if any.
      public TActionType NextActionType; // Only set if NextAction is >= 0.
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TPDFImage
   {
      public IntPtr              Buffer;           // Each scanline is aligned to a full byte.
      public int                 BufSize;          // The size of the image buffer in bytes.
      public TDecodeFilter       Filter;           // Required decode filter if the image is compressed.
                                                   // Possible values are dfDCTDecode (JPEG), dfJPXDecode (JPEG2000),
                                                   // and dfJBIG2Decode. Other filters are always removed by DynaPDF since
                                                   // a conversion to a native file format is then always required.
      public TDecodeFilter       OrgFilter;        // The image was compressed with this filter in the PDF file. This info is
                                                   // useful to determine which compression filter should be used when creating
                                                   // a new image file from the image buffer.
      public IntPtr              JBIG2Globals;     // Optional global page 0 segment (dfJBIG2Decode filter only).
      public int                 JBIG2GlobalsSize; // The size of the bit stream in bytes.
      public int                 BitsPerPixel;     // Bit depth of the image buffer. Possible values are 1, 2, 4, 8, 24, 32, and 64.
      public TExtColorSpace      ColorSpace;       // The color space refers either to the image buffer or to the color table if set.
                                                   // Note that 1 bit images can occur with and without a color table.
      public int                 NumComponents;    // The number of components stored in the image buffer.
      public lbool               MinIsWhite;       // If true, the colors of 1 bit images are reversed.
      public IntPtr              IColorSpaceObj;   // Pointer to the original color space.
      public IntPtr              ColorTable;       // The color table or NULL.
      public int                 ColorCount;       // The number of colors in the color table.
      public int                 Width;            // Image width in pixel.
      public int                 Height;           // Image height in pixel.
      public int                 ScanLineLength;   // The length of a scanline in bytes.
      public lbool               InlineImage;      // If true, the image is an inline image.
      public lbool               Interpolate;      // If true, image interpolation should be performed.
      public lbool               Transparent;      // The meaning is different depending on the bit depth and whether a color
                                                   // table is available. If the image is a 1 bit image and if no color table is available,
                                                   // black pixels must be drawn with the current fill color.
                                                   // If the image contains a color table ColorMask contains the range of indexes
                                                   // in the form min/max index which appears transparent. If no color table is
                                                   // present ColorMask contains the transparent ranges in the form min/max for
                                                   // each component.
      public IntPtr              ColorMask;        // The array contains ranges in the form min/max (2 values per component) for each
                                                   // component before decoding.
      public IntPtr              IMaskImage;       // If set, a 1 bit image is used as a transparency mask. Call GetImageObjEx() to decode the image.
      public IntPtr              ISoftMask;        // If set, a grayscale image is used as alpha channel. Call GetImageObjEx() to decode the image.
      public IntPtr              Decode;           // If set, samples must be decoded. The array contains 2 * NumComponents float values.
                                                   // The decode array is never set if the image is returned decompressed since
                                                   // it is already applied during decompression.
      public TRenderingIntent    Intent;           // Default riNone.
      public int                 SMaskInData;      // JPXDecode only, PDF_MAX_INT if not set. See PDF Reference for further information.
      public IntPtr              OC;               // Pointer of an OCG or OCMD if set. -> See GetOCHandle()
      public IntPtr              Metadata;         // Optional XML Metadata stream.
      public int                 MetadataSize;     // Length of Metadata in bytes.
      public IntPtr              ObjectPtr;        // Internal pointer to the image class.
      public float               ResolutionX;      // Image resolution on the x-axis.
      public float               ResolutionY;      // Image resolution on the y-axis.
      public IntPtr              Measure;          // Optional measure dictionary -> GetMeasureObj().
      public IntPtr              PtData;           // Pointer of a Point Data dictionary. The value can be accessed with GetPtDataObj().
      /*
         The parallelogram into which the image is drawn.
         p1--p2
         |   |
         p4--p3
      */
      public float               DestX1;           // 0
      public float               DestY1;           // 0
      public float               DestX2;           // 1
      public float               DestY2;           // 0
      public float               DestX3;           // 1
      public float               DestY3;           // 1
      public float               DestX4;           // 0
      public float               DestY4;           // 1
      public float               DestWidth;        // Destination width (can be negative).
      public float               DestHeight;       // Destination height (can be negative).
      public uint                FillColor;        // The current fill color. An image mask is drawn with the current fill color.
      public TPDFColorSpace      FillColorSpace;   // The color space in which FillColor is defined.
      // Reserved fields for future extensions
      internal int Reserved1;
      internal int Reserved2;
      internal int Reserved3;
      internal int Reserved4;
   }

   public struct TPDFImportDataAction
   {
      public TPDFFileSpecEx Data;           // The data or file to be loaded.
      public int            NextAction;     // -1 or next action handle to be executed if any
      public TActionType    NextActionType; // Only set if NextAction is >= 0.
   }

   public struct TPDFJavaScriptAction
   {
      public String      Script;         // The script.
      public int         NextAction;     // -1 or next action handle to be executed if any.
      public TActionType NextActionType; // Only set if NextAction is >= 0.
   }

   public struct TPDFLaunchAction
   {
      public String      AppName;        // Optional. The name of the application that should be launched.
      public String      DefDir;         // Optional default directory.
      public IntPtr      File;           // see GetFileSpec().
      public int         NewWindow;      // -1 = viewer default, 0 = false, 1 = true.
      public int         NextAction;     // -1 or next action handle to be executed if any.
      public TActionType NextActionType; // Only set if NextAction is >= 0.
      public String      Operation;      // Optional string specifying the operation to perform (open or print).
      public String      Parameter;      // Optional parameter string that shall be passed to the application (AppName).
   }

   public struct TPDFMeasure
   {
      public bool        IsRectilinear; // If true, the members of the rectilinear measure dictionary are set. The geospatial members otherwise.
      /* --- Rectilinear measure dictionary --- */
      public IntPtr[]    Angles;        // Number format array to measure angles -> GetNumberFormatObj()
      public IntPtr[]    Area;          // Number format array to measure areas -> GetNumberFormatObj()
      public float       CXY;           // Optional, meaningful only when Y is present.
      public IntPtr[]    Distance;      // Number format array to measure distances -> GetNumberFormatObj()
      public float       OriginX;       // Origin of the measurement coordinate system.
      public float       OriginY;       // Origin of the measurement coordinate system.
      public String      R;             // A text string expressing the scale ratio of the drawing.
      public IntPtr[]    Slope;         // Number format array for measurement of the slope of a line -> GetNumberFormatObj()
      public IntPtr[]    X;             // Number format array for measurement of change along the x-axis and, if Y is not present, along the y-axis as well.
      public IntPtr[]    Y;             // Number format array for measurement of change along the y-axis.

      /* --- Geospatial measure dictionary --- */
      public float[]     Bounds;        // Array of numbers taken pairwise to describe the bounds for which geospatial transforms are valid.

      // The DCS coordinate system is optional.
      public bool        DCS_IsSet;     // If true, the DCS members are set.
      public bool        DCS_Projected; // If true, the DCS values contains a pojected coordinate system.
      public int         DCS_EPSG;      // Optional, either EPSG or WKT is set.
      public String      DCS_WKT;       // Optional ASCII string

      // The GCS coordinate system is required and should be present.
      public bool        GCS_Projected; // If true, the GCS values contains a pojected coordinate system.
      public int         GCS_EPSG;      // Optional, either EPSG or WKT is set.
      public String      GCS_WKT;       // Optional ASCII string

      public float[]     GPTS;          // Required, an array of numbers that shall be taken pairwise, defining points in geographic space as degrees of latitude and longitude, respectively.
      public float[]     LPTS;          // Optional, an array of numbers that shall be taken pairwise to define points in a 2D unit square.

      public String      PDU1;          // Optional preferred linear display units.
      public String      PDU2;          // Optional preferred area display units.
      public String      PDU3;          // Optional preferred angular display units.
   }

   public struct TPDFMovieAction
   {
      public int          Annot;          // Optional. The movie annotation handle identifying the movie that shall be played.
      [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
      public float[]      FWPosition;     // FWPosition
      [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
      public int[]        FWScale;        // FWScale
      public String       Mode;           // Mode
      public int          NextAction;     // -1 or next action handle to be executed if any
      public TActionType  NextActionType; // Only set if NextAction is >= 0.
      public String       Operation;      // Operation
      public float        Rate;           // Rate
      public bool         ShowControls;   // ShowControls
      public bool         Synchronous;    // Synchronous
      public String       Title;          // The title of a movie annotation that shall be played. Either Annot or Title should be set, but not both.
      public float        Volume;         // Volume
   }

   public struct TPDFNamedAction
   {
      public String       Name;           // Only set if Type == naUserDefined.
      public int          NewWindow;      // -1 = viewer default, 0 = false, 1 = true.
      public int          NextAction;     // -1 or next action handle to be executed if any.
      public TActionType  NextActionType; // Only set if NextAction is >= 0.
      public TNamedAction Type;           // Known pre-defined actions.
   }

   // Named Destination
   public struct TPDFNamedDest
   {
      public String    Name;
      public String    DestFile;
      public int       DestPage;
      public TPDFRect  DestPos;
      public TDestType DestType;
   }

   public struct TPDFNumberFormat
   {
      public float             C;
      public int               D;
      public TMeasureNumFormat F;
      public bool              FD;
      public TMeasureLblPos    O;
      public String            PS;
      public String            RD;
      public String            RT;
      public String            SS;
      public String            U;
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TPDFObjActions
   {
      public int         StructSize; // Must be set to sizeof(TPDFObjActions).
      public int         Action;     // Action handle or -1 if not set.
      public TActionType ActionType; // The type of the action if Action >= 0.
      public IntPtr      Events;     // Additional events if any. -> GetObjEvent().
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TPDFObjEvent
   {
      public int         StructSize; // Must be set to sizeof(TPDFObjEvent).
      public int         Action;     // Action to be executed.
      public TActionType ActionType; // The type of the action.
      public TObjEvent   Event;      // The event when the action should be executed.
      public IntPtr      Next;       // Pointer to the next event if any.
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TPDFOCG
   {
      public int     StructSize;    // Must be set to sizeof(TPDFOCG)
      public int     Handle;        // Handle or array index
      public int     Intent;        // Bitmask -> TOCGIntent
      private IntPtr NameA;         // Layer name
      private IntPtr NameW;         // Layer name
      public lbool   HaveContUsage; // If true, the layer contains a Content Usage dictionary. -> GetOCGContUsage().
      // The following two members can only be set if HaveContUsage is true.
      public int     AppEvents;     // Bitmask -> see TOCAppEvent. If non-zero, the layer is included in one or more app events which control the layer state.
      public int     Categories;    // Bitmask -> see TOCGUsageCategory. The Usage Categories which control the layer state.
      public string  Name(){return _Conv.ToString(NameA, NameW);}
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TPDFOCGContUsage
   {
      public  int            StructSize;     // Must be set to sizeof(TPDFOCGContUsage)
      public  int            ExportState;    // 0 = Off, 1 = On, PDF_MAX_INT = not set.
      private IntPtr         InfoCreatorA;   // CreatorInfo -> The application that created the group
      private IntPtr         InfoCreatorW;   // CreatorInfo -> The application that created the group
      private IntPtr         InfoSubtypeA;   // CreatorInfo -> A name defining the type of content, e.g. Artwork, Technical etc.
      private IntPtr         LanguageA;      // A language code as described at SetLanguage() in the help file.
      private IntPtr         LanguageW;      // A language code as described at SetLanguage() in the help file.
      public  int            LangPreferred;  // 0 = Off, 1 = On, PDF_MAX_INT = not set. The preffered state if there is a partial but no exact match of the language identifier.
      public  TOCPageElement PageElement;    // If the group contains a pagination artefact.
      public  int            PrintState;     // 0 = Off, 1 = On, PDF_MAX_INT = not set.
      private IntPtr         PrintSubtypeA;  // The type of content that is controlled by the OCG, e.g. Trapping, PrintersMarks or Watermark.
      public  int            UserNamesCount; // The user names (if any) can be accessed with GetOCGUsageUserName().
      public  TOCUserType    UserType;       // The user for whom this optional content group is primarily intendet.
      public  int            ViewState;      // 0 = Off, 1 = On, PDF_MAX_INT = not set.
      public  float          ZoomMin;        // The minimum magnification factor at which the group should be On. -1 if not set.
      public  float          ZoomMax;        // The maximum magnification factor at which the group should be On. -1 if not set.
      public  string InfoCreator() {return _Conv.ToString(InfoCreatorA, InfoCreatorW);}
      public  string InfoSubtype() {return _Conv.ToString(InfoSubtypeA, IntPtr.Zero);}
      public  string Language()    {return _Conv.ToString(LanguageA, LanguageW);}
      public  string PrintSubtype(){return _Conv.ToString(PrintSubtypeA, IntPtr.Zero);}
   }

   public struct TPDFOCLayerConfig
   {
      public TOCGIntent Intent;    // Possible values oiDesign, oiView, or oiAll.
      public bool       IsDefault; // If true, this is the default configuration.
      public String     Name;      // Optional configuration name. The default config has usually no name but all others should have one.
   }

   public class TPDFOCUINode
   {
      public String Label;     // Optional label.
      public IntPtr NextChild; // If set, the next child node that must be loaded.
      public bool   NewNode;   // If true, a new child node must be created.
      public int    OCG;       // Optional OCG handle. -1 if not set -> GetOCG().
   }

   [StructLayout(LayoutKind.Sequential, Pack = 0)]
   public class TOptimizeParams_I
   {
      public int                StructSize;     // Must be set to sizeof(TOptimizeParams).

      public int                Min1BitRes;     // Minimum resolution before scaling.
      public int                MinGrayRes;     // Minimum resolution before scaling.
      public int                MinColorRes;    // Minimum resolution before scaling.

      public int                Res1BitImages;  // 1 bit black & white images.
      public int                ResGrayImages;  // Gray images.
      public int                ResColorImages; // Multi-channel images.

      public TCompressionFilter Filter1Bit;     // Filter for black & white images.
      public TCompressionFilter FilterGray;     // Filter for gray images.
      public TCompressionFilter FilterColor;    // Filter for multi-channel images.
      public int                JPEGQuality;    // JPEG quality.
      public int                JP2KQuality;    // JPEG 2000 quality.
      public float              MinLineWidth;   // Zero means no hair line removal.
      public IntPtr             ExcludeCS;      // Array of color space handles which should be excluded from color conversion.
      public int                ExcludeCSCount; // Number of color space handles in the array.
      public TOptimizeFlags2    Flags2;         // Additional conversion flags
      public TOptimizeFlags3    Flags3;         // Additional conversion flags
      public TOptimizeFlags4    Flags4;         // Additional conversion flags
   }

   public struct TPDFOutputIntent
   {
      public byte[] Buffer;
      public String Info;
      public int    NumComponents;
      public String OutputCondition;
      public String OutputConditionID;
      public String RegistryName;
      public String SubType;
   }

   public struct TPDFPageLabel
   {
      public int              StartRange;   // Number of the first page in the range. If no further label follows, the last
                                            // page in the range is pdfGetPageCount(). The first page is denoted by 1.
      public TPageLabelFormat Format;       // Number format to be used.
      public int              FirstPageNum; // First page number to be displayed in the page label. Subsequent pages are
                                            // numbered sequentially from this value.
      public String           Prefix;       // Optional prefix
   }

   public class TPDFParseCallbacks
   {
      // Dummy type, not yet defined. Parameter must be set to null!
   }

   public enum TPDFPrintFlags
   {
      pffDefault             = 0x00000000, // Gray printing
      pff1Bit                = 0x00000001, // Black & White output
      pffColor               = 0x00000002, // Color output
      pffAutoRotateAndCenter = 0x00000004, // Rotate and center the page if necessary
      pffPrintAsImage        = 0x00000008, // Defined for future use
      pffShrinkToPrintArea   = 0x00000010, // Scale the page so that it fits into the printable area
      pffNoStartDoc          = 0x00000020, // If set, StartDoc() of the Windows print API will not be called
      pffNoStartPage         = 0x00000040, // If set, StartPage() of the Windows print API will not be called
      pffNoEndDoc            = 0x00000080, // If set, EndDoc() of the Windows print API will not be called
      pffPrintPageAsIs       = 0x00000100, // If set, do not scale or rotate a page to fit into the printable area
      pffAutoRotate          = 0x00000200, // Rotate the page if necessary
      pffAutoCenter          = 0x00000400  // Center the page
   }

   
   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public class TPDFPrintParams
   {
      internal int              StructSize;   // Must be set to sizeof(TPDFPrintParams).
      public lbool              Compress;     // Meaningful only for image output. If true, compressed images are send to the printer.
      public TCompressionFilter FilterGray;   // Meaningful only for image output. Supported filters on Windows: cfFlate and cfJPEG.
      public TCompressionFilter FilterColor;  // Meaningful only for image output. Supported filters on Windows: cfFlate and cfJPEG.
      public int                JPEGQuality;  // JPEG Quality in percent. Zero == Default == 60.
      public int                MaxRes;       // Maximum print resolution. Zero == Default == 600 DPI.
      public lbool              IgnoreDCSize; // If true, PageSize is used to calculate the output format.
      public TIntRect           PageSize;     // Considered only, if IgnoreDCSize is set to true.
      public TPDFPrintParams()
      {
         StructSize = Marshal.SizeOf(this);
      }
   }
   

   public struct TPDFPrintSettings
   {
      public TDuplexMode   DuplexMode;
      public int           NumCopies;         // -1 means not set. Values larger than 5 are ignored in viewer applications.
      public int           PickTrayByPDFSize; // -1 means not set. 0 == false, 1 == true.
      public int[]         PrintRanges;       // If set, the array contains PrintRangesCount * 2 values. Each pair consists
                                              // of the first and last pages in the sub-range. The first page in the PDF file
                                              // is denoted by 0.
      public TPrintScaling PrintScaling;      // dpmNone means not set.
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TPDFRect
   {
      public double Left;
      public double Bottom;
      public double Right;
      public double Top;
   }

   public struct TPDFRawImage
   {
      public int            StructSize;       // Must be set to sizeof(TPDFRawImage)
      public IntPtr         Buffer;           // Image buffer
      public int            BufSize;          // Buffer size
      public int            BitsPerComponent; // Bits per component
      public int            NumComponents;    // Number of components (max 32)
      public TExtColorSpace CS;               // Image color space
      public int            CSHandle;         // Color space handle (non-device color spaces only)
      public int            Stride;           // Scanline length in bytes -> If negative, the image is defined in bottom up coordinates, top down otherwise
      public lbool          HasAlpha;         // If true, the last component is an alpha channel
      public lbool          IsBGR;            // esDeviceRGB only -> If true, the image components are defined in BGR format instead of RGB
      public lbool          MinIsWhite;       // 1 bit images only -> If true, zero pixel values must be treated as white instead of black
      public int            Width;            // Width in pixels (must be greater zero)
      public int            Height;           // Height in pixels (must be greater zero)
   }

   public struct TPDFRelFileNode
   {
      public String       Name;     // Name of this file spcification.
      public TPDFFileSpec EF;       // Embedded file.
      public IntPtr       NextNode; // Next node if any.
   }

   public struct TPDFResetFormAction
   {
      public IntPtr[]    Fields;         // Array of field pointers -> GetFieldEx2().
      public bool        Include;        // If true, the fields in the Fields array must be reset. If false, these fields must be excluded.
      public int         NextAction;     // -1 or next action handle to be executed if any.
      public TActionType NextActionType; // Only set if NextAction is >= 0.
   }

   public struct TPDFSigDict
   {
      public int[]      ByteRange;      // ByteRange -> Byte offset followed by the corresponding length.
                                        // The byte ranges are required to create the digest. The values
                                        // are returned as is. So, you must check whether the offsets and
                                        // length values are valid. There are normally at least two ranges.
                                        // Overlapping ranges are not allowed! Any error breaks processing
                                        // and the signature should be considered as invalid.
      public byte[]     Cert;           // X.509 Certificate when SubFilter is adbe.x509.rsa_sha1.
      public int[]      Changes;        // If set, an array of three integers that specify changes to the
                                        // document that have been made between the previous signature and
                                        // this signature in this order: the number of pages altered, the
                                        // number of fields altered, and the number of fields filled in.
      public String     ContactInfo;    // Optional contact info string, e.g. an email address
      public byte[]     Contents;       // The signature. This is either a DER encoded PKCS#1 binary data
                                        // object or a DER-encoded PKCS#7 binary data object depending on
                                        // the used SubFilter.
      public String     Filter;         // The name of the security handler, usually Adobe.PPKLite.
      public String     Location;       // Optional location of the signer
      public String     SignTime;       // Date/Time string
      public String     Name;           // Optional signers name
      public int        PropAuthTime;   // Optional -> The number of seconds since the signer was last authenticated.
      public String     PropAuthType;   // Optional -> The method that shall be used to authenticate the signer.
                                        // Valid values are PIN, Password, and Fingerprint.
      public String     Reason;         // Optional reason
      public int        Revision;       // Optional -> The version of the signature handler that was used to create
                                        // the signature.
      public String     SubFilter;      // A name that describes the encoding of the signature value. Should be
                                        // adbe.x509.rsa_sha1, adbe.pkcs7.detached, or adbe.pkcs7.sha1.
      public int        Version;        // The version of the signature dictionary format.
   }

   public struct TPDFSigParms
   {
      public int           PKCS7ObjLen; // The maximum length of the signed PKCS#7 object
      public THashType     HashType;    // If set to htDetached the bytes ranges of the PDF file will be returned.
      public byte[]        Range1;      // Out -> Contains either the hash or the first byte range to create a detached signature
      public byte[]        Range2;      // Out -> Set only if HashType == htDetached
      public String        ContactInfo; // Optional, e.g. an email address
      public String        Location;    // Optional location of the signer
      public String        Reason;      // Optional reason why the file was signed
      public String        Signer;      // Optional, the issuer of the certificate takes precedence
      public bool          Encrypt;     // If true, the file will be encrypted
      // These members will be ignored if Encrypt is set to false
      public String        OpenPwd;     // Open password (Ansi String)
      public String        OwnerPwd;    // Owner password to change the security settings (Ansi String)
      public TKeyLen       KeyLen;      // Key length to be used to encrypt the file
      public TRestrictions Restrict;    // What should be restricted?
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TPDFStack
   {
      public TCTM           ctm;          // Pre-multiplied global transformation matrix
      public TCTM           tm;           // Pre-multiplied text transformation matrix
      public double         x;            // Unused -> always 0.0
      public double         y;            // Unused -> always 0.0
      public double         FontSize;     // Font size
      public double         CharSP;       // Character spacing
      public double         WordSP;       // Word spacing
      public double         HScale;       // Horizontal text scaling
      public double         TextRise;     // Text rise -> always 0.0 because it is already included in the text transformation matrix
      public double         Leading;      // Leading
      public double         LineWidth;    // Line width
      public TDrawMode      DrawMode;     // Text draw mode
      public TPDFColorSpace FillCS;       // Fill color space
      public TPDFColorSpace StrokeCS;     // Stroke color space
      public uint           FillColor;    // Fill color
      public uint           StrokeColor;  // Stroke color
      public IntPtr         BaseObject;   // Internal
      public lbool          CIDFont;      // If true, ReplacePageText() can only be used to delete a string
      public IntPtr         Text;         // Raw text without kerning space
      public int            TextLen;      // Raw text length
      public IntPtr         RawKern;      // Raw kerning array (type TTextArrayA[])
      public IntPtr         Kerning;      // Already translated Unicode kerning array (type TTextArrayW[])
      public int            KerningCount; // Number of kerning records
      public float          TextWidth;    // The width of the entire text record measured in text space
      public IntPtr         IFont;        // Font object used to print the string -> fntGetFont() can be used to return the font properties
      public lbool          Embedded;     // If true, the font is embedded
      public float          SpaceWidth;   // Measured in text space
      /*
         These members can be modified after the structure has been initialized with InitStack().
         If the destination color space should be DeviceCMYK initialize FillColor and StrokeColor
         with PDF_CMYK(0,0,0,255); which represents black.
      */
      public lbool                ConvColors;   // If set to true (default), all colors are converted to the specified destination color space
      public TPDFColorSpace       DestSpace;    // Destination color space -> default == csDeviceRGB

      // This member can be used in combination with ReplacePageText() to preserve a number
      // of kerning records from deletion. All records above this value will be deleted.
      // Take a look into the file examples/util/pdf_edit_text.cs to determine how this member
      // can be used.
      public int                  DeleteKerningAt;
      public uint                 FontFlags;
      
      // ------------------------------- Reserved fields -------------------------------
      internal int                  Reserved1;
      internal int                  Reserved2;
      internal int                  Reserved3;
      internal int                  Reserved4;
      internal int                  Reserved5;
      internal int                  Reserved6;
      internal int                  Reserved7;
      internal int                  Reserved8;
      internal int                  Reserved9;
      internal int                  Reserved10;
      internal int                  Reserved11;
      internal IntPtr               ContentPtr;
      
   }

   public struct TPDFSubmitFormAction
   {
      public String       CharSet;        // Optional charset in which the form should be submitted.
      public IntPtr[]     Fields;         // Array of field pointers -> GetFieldEx2().
      public TSubmitFlags Flags;          // Various flags, see CreateSubmitAction() for further information.
      public String       URL;            // The URL of the script at the Web server that will process the submission.
      public int          NextAction;     // -1 or next action handle to be executed if any.
      public TActionType  NextActionType; // Only set if NextAction is >= 0.
   }

   public enum TPDFVersion
   {
      pvPDF_1_0     = 0,
      pvPDF_1_1     = 1,
      pvPDF_1_2     = 2,
      pvPDF_1_3     = 3,
      pvPDF_1_4     = 4,
      pvPDF_1_5     = 5,
      pvPDF_1_6     = 6,
      pvPDF_1_7     = 7,
      pvPDF_2_0     = 8,  // PDF 2.0
      pvReserved    = 9,  // Reserved for future use
      pvPDFX1a_2001 = 10, // PDF/X-1a:2001
      pvPDFX1a_2003 = 11, // PDF/X-1a:2003
      pvPDFX3_2002  = 12, // PDF/X-3:2002
      pvPDFX3_2003  = 13, // PDF/X-3:2003
      pvPDFA_2005   = 14, // PDF/A-1b 2005
      pvPDFX_4      = 15, // PDF/X-4
      pvPDFA_1a     = 16, // PDF/A 1a 2005
      pvPDFA_2a     = 17, // PDF/A 2a
      pvPDFA_2b     = 18, // PDF/A 2b
      pvPDFA_2u     = 19, // PDF/A 2u
      pvPDFA_3a     = 20, // PDF/A 3a
      pvPDFA_3b     = 21, // PDF/A 3b
      pvPDFA_3u     = 22, // PDF/A 3u
      // The following constants are flags which can be combined with pvPDFA_3a, pvPDFA_3b, and pvPDFA_3u.
      // If used stand alone PDF/A 3b with the correspondig ZUGFeRD metadata will be created.
      // The embedded XML invoice must be named ZUGFeRD-invoice.xml. Note that every ZUGFeRD version uses
      // another name for the XML invoice!
      pvZUGFeRD_Basic     = 0x00010000,
      pvZUGFeRD_Comfort   = 0x00020000,
      pvZUGFeRD_Extended  = 0x00040000,
      pvZUGFeRD_Mask      = pvZUGFeRD_Basic | pvZUGFeRD_Comfort | pvZUGFeRD_Extended,
      // Factur-X / ZUGFeRD 2.1, and ZUGFeRD 2.0 profiles. The usage is identically in comparision to
      // the older ZUGFeRD 1.0 flags. That means if used alone, a PDF/A 3b file will be created.
      // ZUGFeRD 2.1 and Factur-X are identically defined in PDF. In order to create a ZUGFeRD 2.1 invoice,
      // set one of the Factur-X constants but don't set the flag pvZUGFeRD2_Flag.
      /* --------------------------------------------------------------------------------------------------------------
       *                                         Factur-X vs. ZUGFeRD 2.0
       *---------------------------------------------------------------------------------------------------------------
       * ZUGFeRD 2.0 and Factur-X are almost identically defined. Both standards support the very same profiles. To create
       * a ZUGFeRD 2.0 invoice with the profile Basic WL, for example, set the PDF version as follows:
       *
       *    pdfSetPDFVersion(pdf, TPDFVersion(pvFacturX_Basic_WL | pvZUGFeRD2_Flag));
       *
       * The PDF version is set to PDF/A 3b in the above example.
       *
       * If you want to create the very same invoice but as PDF/A 3u file, for example, then set also the whished output PDF
       * version:
       *
       *    pdfSetPDFVersion(pdf, TPDFVersion(pvPDFA_3u | pvFacturX_Basic_WL | pvZUGFeRD2_Flag));
       *
      */
      pvFacturX_Minimum   = 0x00080000, // Minimum profile  -> Factur-X and ZUGFeRD 2.1
      pvFacturX_Basic     = 0x00100000, // Basic profile    -> Factur-X and ZUGFeRD 2.1
      pvFacturX_Basic_WL  = 0x00200000, // Basic WL profile -> Factur-X and ZUGFeRD 2.1
      pvFacturX_Comfort   = 0x00400000, // EN 16931 profile -> Factur-X and ZUGFeRD 2.1
      pvFacturX_Extended  = 0x00800000, // Extended profile -> Factur-X and ZUGFeRD 2.1
      pvFacturX_XRechnung = 0x02000000, // German XRechnung profile. The XML invoice must be named xrechnung.xml.
      pvFacturX_Mask      = pvFacturX_Minimum | pvFacturX_Basic | pvFacturX_Basic_WL | pvFacturX_Comfort | pvFacturX_Extended | pvFacturX_XRechnung,
      pvZUGFeRD2_Flag     = 0x01000000, // If set, the PDF version will be set to the corresponding ZUGFeRD2 profile.
                                        // The embedded XML file must be named zugferd-invoice.xml.
      pvPDFX5g            = 23,         // PDF/X-5g
      pvPDFX5n            = 24,         // PDF/X-5n
      pvPDFX5pg           = 25,         // PDF/X-5pg
      pvPDFX6             = 26,         // PDF/X 6
      pvPDFX6n            = 27,         // PDF/X 6n
      pvPDFX6p            = 28,         // PDF/X 6p
      pvPDFUA1            = 29,         // PDF/UA-1 (ISO 14289-1)
      pvPDFX4p            = 30,         // PDF/X-4p
      pvPDFVT1            = 0x10000000, // PDF/VT 1 flag. This is just a flag because PDF/VT requires always a PDF/X base version.
      pvPDFVT2            = 0x04000000, // PDF/VT 2 flag. This is just a flag because PDF/VT requires always a PDF/X base version.
      pvPDFVT3            = 0x08000000, // PDF/VT 3 flag. This is just a flag because PDF/VT requires always a PDF/X base version.
      pvPDFVTMask         = pvPDFVT1 | pvPDFVT2 | pvPDFVT3,
      pvPDFUAFlag         = 0x20000000, // PDF/UA flag. Can be used in addition with other standards. File metadata will contain PDF/A and PDF/UA entries.
      pvPDFA_4            = 31,         // PDF/A 4
      pvPDFA_4e           = 32,         // PDF/A 4e allows 3D contents in rich media annotations.
      pvPDFA_4f           = 33          // PDF/A 4f allows file attach annotations.
   }

   // We have different members for PDF/A, PDF/E, PDF/VT, and PDF/X because multiple versions can be set at time.
   // PDF/VT files require also PDF/X version information.
   public struct TPDFVersionInfo
   {
      public int    Major;   // PDF major version, e.g. 1
      public int    Minor;   // PDF minor version, e.g. 7
      public String MainVer; // Set only if the file conforms to a specific PDF standard, e.g. "PDF/X 1a:2003", "PDF/A 3b", and so on.
      public String SubVer;  // e.g. "ZUGFeRD 2.0", "Factur-X", and so on
      // PDF/A
      public String PDFAConformance; // e.g. A, B, E, F, U, or UA.
      public int    PDFAVersion;     // If greater zero, this is a PDF/A file.
      // PDF/E
      public String PDFEVersion;     // e.g. PDF/E-1
      // PDF/VT
      public String PDFVTModDate;    // Modification date, e.g. 2016-05-07T15:42:23+01:00
      public String PDFVTVersion;    // e.g. PDF/VT-2
      // PDF/X
      public String PDFXConformance; // e.g. PDF/X-1a:2001 or PDF/X-1a:2003. PDF/X 3 and above do not set this key anymore.
      public String PDFXVersion;     // e.g. PDF/X-1:2001, PDF/X-1:2003, and so on.
      // ZUGFeRD, Factur-X, XRechnung
      public String FXConfLevel;     // ConformanceLevel
      public String FXDocName;       // DocumentFileName
      public String FXDocType;       // DocumentType
      public String FXVersion;       // Version
      public int    VersionConst;    // If >= 0 this is the TPDFVersion constant as integer. If the version is not supported by DynaPDF, this member is set to -1.
   };

   public enum TPrintScaling
   {
      psAppDefault, // Default
      psNone
   }

   public enum TPwdType
   {
      ptOpen        = 0,
      ptOwner       = 1,
      ptForceRepair = 2, // Meaningful only when opening a PDF file with OpenImportFile() or OpenImportBuffer().
                         // If set, the PDF parser rebuilds the cross-reference table by scanning all the objects
                         // in the file. This can be useful if the cross-reference table contains damages while
                         // the top level objects are intact. Setting this flag makes only sence if the file
                         // was already previously opened in normal mode and if errors occured when importing
                         // pages of it.
      ptDontCopyBuf = 4  // If set, OpenImportBuffer() does not copy the PDF buffer to an internal buffer. This
                         // increases the processing speed and reduces the memory usage. The PDF buffer must not
                         // be released until CloseImportFile() or CloseFile() was called.
   }

   public enum TRawImageFlags
   {
      rifByteAligned  = 0x1000,
      rifRGBData      = 0x2000,
      rifCMYKData     = 0x4000
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TRectL
   {
      public int Left;
      public int Top;
      public int Right;
      public int Bottom;
   }

   public enum TRenderingIntent
   {
      riAbsoluteColorimetric,
      riPerceptual,
      riRelativeColorimetric,
      riSaturation,
      riNone // Internal
   }

   public enum TReplaceImageFlags
   {
      rifDefault         = 0, // Nothing special to do.
      rifDeleteAltImages = 1, // Delete all alternate images that are associated with this image if any.
      rifDeleteMetadata  = 2, // Delete the meta data that was associated with the image.
      rifDeleteOCG       = 4, // Delete the Optional Content Group if any. This changes the visibility state of the image. Normally the OCG should be left as is.
      rifDeleteSoftMask  = 8  // An image can contain a soft mask that acts as an alpha channel. This mask can be deleted or left as is.
                              // The mask will always be deleted if the new image contains a soft mask or an alpha channel.
   }

   public enum TRestrictions
   {
      rsDenyNothing      = 0x00000000,
      rsDenyAll          = 0x00000F3C,
      rsPrint            = 0x00000004,
      rsModify           = 0x00000008,
      rsCopyObj          = 0x00000010,
      rsAddObj           = 0x00000020,
      /* 128/256 bit encryption only -> these flags are ignored if 40 bit encryption is used */
      rsFillInFormFields = 0x00000100,
      rsExtractObj       = 0x00000200,
      rsAssemble         = 0x00000400,
      rsPrintHighRes     = 0x00000800,
      rsExlMetadata      = 0x00001000, // PDF 1.5 Exclude metadata streams -> 128/256 bit encryption bit only.
      rsEmbFilesOnly     = 0x00002000  // PDF 1.6 Encrypt embedded files only -> Requires AES encryption.
   }

   public enum TRubberStamp
   {
     rsApproved,
     rsAsIs,
     rsConfidential,
     rsDepartmental,
     rsDraft,
     rsExperimental,
     rsExpired,
     rsFinal,
     rsForComment,
     rsForPublicRelease,
     rsNotApproved,
     rsNotForPublicRelease,
     rsSold,
     rsTopSecret,
     rsUserDefined
   }

   public enum TShadingType
   {
      stUnknown, // cannot occur -> internal use
      stFunctionBased,
      stAxial,
      stRadial,
      stFreeFormGouraud,
      stLatticeFormGouraud,
      stCoonsPatch,
      stTensorProduct
   }

   public enum TSoftMaskType
   {
      smtAlpha,
      smtLuminosity
   }

   public enum TSpoolConvFlags
   {
      spcDefault            = 0,
      spcIgnorePaperFormat  = 1, // If set, the current page format is used as is for the entire spool file.
      spcDontAddMargins     = 2, // If set, the page format is calculated from the EMF files as is. The current page format is not used to calculate
                                 // margins which are maybe required. Note that the parameters LeftMargin and TopMargin will still be considered.
      spcLoadSpoolFontsOnly = 4, // If set, only embedded fonts will be loaded. The EMF files must be converted with the flag mfIgnoreEmbFonts in this
                                 // case. This flag can be useful if you want to use your own code to convert the EMF files of the spool file.
      spcFlushPages         = 8  // If set, the function writes every finish page directly to the output file to reduce the memory usage. This flag
                                 // is meaningful only if the PDF file is not created in memory. Note also that it is not possible to access already
                                 // flushed pages again with EditPage().
   }

   public enum TStdPattern
   {
      spHorizontal, /* ----- */
      spVertical,   /* ||||| */
      spRDiagonal,  /* \\\\\ */
      spLDiagonal,  /* ///// */
      spCross,      /* +++++ */
      spDiaCross    /* xxxxx */
   }

   public enum TSubmitFlags
   {
      sfNone             = 0x00000000,
      sfExlude           = 0x00000001, // if set, the fields in a submit form action are excluded if any
      sfInclNoValFields  = 0x00000002,
      sfHTML             = 0x00000004,
      sfGetMethod        = 0x00000008,
      sfSubmCoords       = 0x00000010,
      sfXML              = 0x00000024,
      sfInclAppSaves     = 0x00000040,
      sfInclAnnots       = 0x00000080,
      sfPDF              = 0x00000100,
      sfCanonicalFormat  = 0x00000200,
      sfExlNonUserAnnots = 0x00000400,
      sfExlFKey          = 0x00000800,
      sfEmbedForm        = 0x00002000 // PDF 1.5 embed the entire form into a file stream inside the FDF file
   }

   public struct TPDFSysFont
   {
      public TFontBaseType      BaseType;        // Font type
      public String             CIDOrdering;     // OpenType CID fonts only
      public String             CIDRegistry;     // OpenType CID fonts only
      public int                CIDSupplement;   // OpenType CID fonts only
      public int                DataOffset;      // Data offset
      public String             FamilyName;      // Family name
      public String             FilePath;        // Font file path
      public int                FileSize;        // File size in bytes
      public TEnumFontProcFlags Flags;           // Bitmask
      public String             FullName;        // Full name
      public int                Length1;         // Length of the clear text portion of a Type1 font
      public int                Length2;         // Length of the eexec encrypted binary portion of a Type1 font
      public String             PostScriptName;  // Postscript mame
      public int                Index;           // Zero based font index if the font is stored in a TrueType collection
      public bool               IsFixedPitch;    // If true, the font is a fixed pitch font. A proprtional font otherwise.
      public TFStyle            Style;           // Font style
      public TUnicodeRange1     UnicodeRange1;   // Bitmask
      public TUnicodeRange2     UnicodeRange2;   // Bitmask
      public TUnicodeRange3     UnicodeRange3;   // Bitmask
      public TUnicodeRange4     UnicodeRange4;   // Bitmask
   }

   public enum TTabOrder
   {
      toRow,
      toColumn,
      toStructure,
      toNone
   }

   public enum TTextAlign
   {
      taLeft      = 0,
      taCenter    = 1,
      taRight     = 2,
      taJustify   = 3,
      taPlainText = 0x10000000 // If this flag is set alignment and command tags are interpreted as plain text.
                               // See WriteFText() in the help file for further information.
   }

   public enum TTextExtractionFlags
   {
      tefDefault               = 0, // Create text lines in the original order.
      tefSortTextX             = 1, // Sort text records in x-direction.
      tefSortTextY             = 2, // Sort text records in y-direction.
      tefSortTextXY            = tefSortTextX | tefSortTextY,
      tefDeleteOverlappingText = 4 // Text extraction only.
   }

   public enum TTilingType
   {
      ttConstSpacing,
      ttNoDistortion,
      ttFastConstSpacing
   }

   public enum TUnicodeRange1
   {
      urBasicLatin                  = 1<<0,  // 0000-007F
      urLatin1Supplement            = 1<<1,  // 0080-00FF
      urLatinExtendedA              = 1<<2,  // 0100-017F
      urLatinExtendedB              = 1<<3,  // 0180-024F
      urIPAExtensions               = 1<<4,  // 0250-02AF, 1D00-1D7F, 1D80-1DBF
      urSpacingModifierLetters      = 1<<5,  // 02B0-02FF, A700-A71F
      urCombiningDiacriticalMarks   = 1<<6,  // 0300-036F, 1DC0-1DFF
      urGreekandCoptic              = 1<<7,  // 0370-03FF
      urCoptic                      = 1<<8,  // 2C80-2CFF
      urCyrillic                    = 1<<9,  // 0400-04FF, 0500-052F, 2DE0-2DFF, A640-A69F
      urArmenian                    = 1<<10, // 0530-058F
      urHebrew                      = 1<<11, // 0590-05FF
      urVai                         = 1<<12, // A500-A63F
      urArabic                      = 1<<13, // 0600-06FF, 0750-077F
      urNKo                         = 1<<14, // 07C0-07FF
      urDevanagari                  = 1<<15, // 0900-097F
      urBengali                     = 1<<16, // 0980-09FF
      urGurmukhi                    = 1<<17, // 0A00-0A7F
      urGujarati                    = 1<<18, // 0A80-0AFF
      urOriya                       = 1<<19, // 0B00-0B7F
      urTamil                       = 1<<20, // 0B80-0BFF
      urTelugu                      = 1<<21, // 0C00-0C7F
      urKannada                     = 1<<22, // 0C80-0CFF
      urMalayalam                   = 1<<23, // 0D00-0D7F
      urThai                        = 1<<24, // 0E00-0E7F
      urLao                         = 1<<25, // 0E80-0EFF
      urGeorgian                    = 1<<26, // 10A0-10FF, 2D00-2D2F
      urBalinese                    = 1<<27, // 1B00-1B7F
      urHangulJamo                  = 1<<28, // 1100-11FF
      urLatinExtendedAdditional     = 1<<29, // 1E00-1EFF, 2C60-2C7F, A720-A7FF
      urGreekExtended               = 1<<30, // 1F00-1FFF
      urGeneralPunctuation          = 1<<31  // 2000-206F, 2E00-2E7F
   }

   public enum TUnicodeRange2
   {
      urSuperscriptsAndSubscripts   = 1<<0,  // 2070-209F
      urCurrencySymbols             = 1<<1,  // 20A0-20CF
      urCombDiacritMarksForSymbols  = 1<<2,  // 20D0-20FF
      urLetterlikeSymbols           = 1<<3,  // 2100-214F
      urNumberForms                 = 1<<4,  // 2150-218F
      urArrows                      = 1<<5,  // 2190-21FF, 27F0-27FF, 2900-297F, 2B00-2BFF
      urMathematicalOperators       = 1<<6,  // 2200-22FF, 2A00-2AFF, 27C0-27EF, 2980-29FF
      urMiscellaneousTechnical      = 1<<7,  // 2300-23FF
      urControlPictures             = 1<<8,  // 2400-243F
      urOpticalCharacterRecognition = 1<<9,  // 2440-245F
      urEnclosedAlphanumerics       = 1<<10, // 2460-24FF
      urBoxDrawing                  = 1<<11, // 2500-257F
      urBlockElements               = 1<<12, // 2580-259F
      urGeometricShapes             = 1<<13, // 25A0-25FF
      urMiscellaneousSymbols        = 1<<14, // 2600-26FF
      urDingbats                    = 1<<15, // 2700-27BF
      urCJKSymbolsAndPunctuation    = 1<<16, // 3000-303F
      urHiragana                    = 1<<17, // 3040-309F
      urKatakana                    = 1<<18, // 30A0-30FF, 31F0-31FF
      urBopomofo                    = 1<<19, // 3100-312F, 31A0-31BF
      urHangulCompatibilityJamo     = 1<<20, // 3130-318F
      urPhagsPa                     = 1<<21, // A840-A87F
      urEnclosedCJKLettersAndMonths = 1<<22, // 3200-32FF
      urCJKCompatibility            = 1<<23, // 3300-33FF
      urHangulSyllables             = 1<<24, // AC00-D7AF
      urNonPlane0                   = 1<<25, // D800-DFFF
      urPhoenician                  = 1<<26, // 10900-1091F
      urCJKUnifiedIdeographs        = 1<<27, // 4E00-9FFF, 2E80-2EFF, 2F00-2FDF, 2FF0-2FFF, 3400-4DBF, 20000-2A6DF, 3190-319F
      urPrivateUseAreaPlane0        = 1<<28, // E000-F8FF
      urCJKStrokes                  = 1<<29, // 31C0-31EF, F900-FAFF, 2F800-2FA1F
      urAlphabeticPresentationForms = 1<<30, // FB00-FB4F
      urArabicPresentationFormsA    = 1<<31  // FB50-FDFF
   }

   public enum TUnicodeRange3
   {
      urCombiningHalfMarks          = 1<<0,  // FE20-FE2F
      urVerticalForms               = 1<<1,  // FE10-FE1F, FE30-FE4F
      urSmallFormVariants           = 1<<2,  // FE50-FE6F
      urArabicPresentationFormsB    = 1<<3,  // FE70-FEFF
      urHalfwidthAndFullwidthForms  = 1<<4,  // FF00-FFEF
      urSpecials                    = 1<<5,  // FFF0-FFFF
      urTibetan                     = 1<<6,  // 0F00-0FFF
      urSyriac                      = 1<<7,  // 0700-074F
      urThaana                      = 1<<8,  // 0780-07BF
      urSinhala                     = 1<<9,  // 0D80-0DFF
      urMyanmar                     = 1<<10, // 1000-109F
      urEthiopic                    = 1<<11, // 1200-137F, 1380-139F, 2D80-2DDF
      urCherokee                    = 1<<12, // 13A0-13FF
      urUnifiedCanadianAboriginal   = 1<<13, // 1400-167F
      urOgham                       = 1<<14, // 1680-169F
      urRunic                       = 1<<15, // 16A0-16FF
      urKhmer                       = 1<<16, // 1780-17FF, 19E0-19FF
      urMongolian                   = 1<<17, // 1800-18AF
      urBraillePatterns             = 1<<18, // 2800-28FF
      urYiSyllables                 = 1<<19, // A000-A48F, A490-A4CF
      urTagalog                     = 1<<20, // 1700-171F, 1720-173F, 1740-175F, 1760-177F
      urOldItalic                   = 1<<21, // 10300-1032F
      urGothic                      = 1<<22, // 10330-1034F
      urDeseret                     = 1<<23, // 10400-1044F
      urMusicalSymbols              = 1<<24, // 1D000-1D0FF, 1D100-1D1FF, 1D200-1D24F
      urMathematicalAlphanumeric    = 1<<25, // 1D400-1D7FF
      urPrivateUsePlane15           = 1<<26, // FF000-FFFFD, 100000-10FFFD
      urVariationSelectors          = 1<<27, // FE00-FE0F, E0100-E01EF
      urTags                        = 1<<28, // E0000-E007F
      urLimbu                       = 1<<29, // 1900-194F
      urTaiLe                       = 1<<30, // 1950-197F
      urNewTaiLue                   = 1<<31  // 1980-19DF
   }

   public enum TUnicodeRange4
   {
      urBuginese                    = 1<<0,  // 1A00-1A1F
      urGlagolitic                  = 1<<1,  // 2C00-2C5F
      urTifinagh                    = 1<<2,  // 2D30-2D7F
      urYijingHexagramSymbols       = 1<<3,  // 4DC0-4DFF
      urSylotiNagri                 = 1<<4,  // A800-A82F
      urLinearBSyllabary            = 1<<5,  // 10000-1007F, 10080-100FF, 10100-1013F
      urAncientGreekNumbers         = 1<<6,  // 10140-1018F
      urUgaritic                    = 1<<7,  // 10380-1039F
      urOldPersian                  = 1<<8,  // 103A0-103DF
      urShavian                     = 1<<9,  // 10450-1047F
      urOsmanya                     = 1<<10, // 10480-104AF
      urCypriotSyllabary            = 1<<11, // 10800-1083F
      urKharoshthi                  = 1<<12, // 10A00-10A5F
      urTaiXuanJingSymbols          = 1<<13, // 1D300-1D35F
      urCuneiform                   = 1<<14, // 12000-123FF, 12400-1247F
      urCountingRodNumerals         = 1<<15, // 1D360-1D37F
      urSundanese                   = 1<<16, // 1B80-1BBF
      urLepcha                      = 1<<17, // 1C00-1C4F
      urOlChiki                     = 1<<18, // 1C50-1C7F
      urSaurashtra                  = 1<<19, // A880-A8DF
      urKayahLi                     = 1<<20, // A900-A92F
      urRejang                      = 1<<21, // A930-A95F
      urCham                        = 1<<22, // AA00-AA5F
      urAncientSymbols              = 1<<23, // 10190-101CF
      urPhaistosDisc                = 1<<24, // 101D0-101FF
      urCarian                      = 1<<25, // 102A0-102DF, 10280-1029F, 10920-1093F
      urDominoTiles                 = 1<<26  // 1F030-1F09F, 1F000-1F02F
   }

   public enum TViewerPreference
   {
      vpUseNone                = 0x00000000,
      vpHideToolBar            = 0x00000001,
      vpHideMenuBar            = 0x00000002,
      vpHideWindowUI           = 0x00000004,
      vpFitWindow              = 0x00000008,
      vpCenterWindow           = 0x00000010,
      vpDisplayDocTitle        = 0x00000020,
      vpNonFullScrPageMode     = 0x00000040,
      vpDirection              = 0x00000080,
      vpViewArea               = 0x00000100,
      vpViewClip               = 0x00000200,
      vpPrintArea              = 0x00000400,
      vpPrintClip              = 0x00000800
   }

   public enum TViewPrefAddVal
   {
      avNone                   = 0x00000000,
      avNonFullScrUseNone      = 0x00000001,
      avNonFullScrUseOutlines  = 0x00000002,
      avNonFullScrUseThumbs    = 0x00000004,
      avNonFullScrUseOC        = 0x00000400, // PDF 1.6
      avDirectionL2R           = 0x00000008,
      avDirectionR2L           = 0x00000010,
      avViewPrintArtBox        = 0x00000020,
      avViewPrintBleedBox      = 0x00000040,
      avViewPrintCropBox       = 0x00000080,
      avViewPrintMediaBox      = 0x00000100,
      avViewPrintTrimBox       = 0x00000200,

      // Use these masks to determine which value is defined.
      AV_NON_FULL_SRC_MASK     = 0x00000005,
      AV_DIRECTION_MASK        = 0x00000018,
      AV_VIEW_PRINT_MASK       = 0x000003E0
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TTextRecordA
   {
      public float  Advance; // Negative values move the cursor to right, positive to left. The value is measured in text space!
      public IntPtr Text;    // Raw text (not null-terminated)
      public int    Length;  // Raw text length in bytes
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TTextRecordW
   {
      public float  Advance; // Negative values move the cursor to right, positive to left. The value is measured in text space!
      public IntPtr Text;    // Already translated Unicode string (not null-terminated)
      public int    Length;  // Length in characters
      public float  Width;   // String width measured in text space
   }

   public struct TPDFURIAction
   {
      public String      BaseURL;        // Optional, if defined in the Catalog object.
      public bool        IsMap;          // A flag specifying whether to track the mouse position when the URI is resolved: e.g. http://test.org?50,70.
      public String      URI;            // Uniform Resource Identifier.
      public int         NextAction;     // -1 or next action handle to be executed if any.
      public TActionType NextActionType; // Only set if NextAction is >= 0.
   }

   public struct TPDFViewport
   {
      public TFltRect BBox;    // Bounding box
      public IntPtr   Measure; // Optional -> GetMeasureObj()
      public String   Name;    // Optional name
      public IntPtr   PtData;  // Pointer of a Point Data dictionary. The value can be accessed with GetPtDataObj().
   }

   public class TPDFXFAStream
   {
      public byte[] Buffer;
      public String Name;
   }

   /* ---------------------------------------------- Parser Interface ---------------------------------------------- */

   public delegate int  TApplyPattern(IntPtr Data, IntPtr Object, TPatternType Type, IntPtr Pattern);
   public delegate int  TBeginLayer(IntPtr Data, IntPtr OC, lbool InVisible);
   public delegate int  TBeginPattern(IntPtr Data, lbool Fill, int Handle, TPatternType Type, ref TPDFRect BBox, IntPtr Matrix /* type TCTM */, IntPtr XStep /* type double */, IntPtr YStep /* type double */);
   public delegate int  TBeginTemplate(IntPtr Data, IntPtr Object, int Handle, ref TPDFRect BBox, IntPtr Matrix /* type TCTM */);
   public delegate int  TBezierTo1(IntPtr Data, IntPtr Object, double x1, double y1, double x3, double y3);
   public delegate int  TBezierTo2(IntPtr Data, IntPtr Object, double x2, double y2, double x3, double y3);
   public delegate int  TBezierTo3(IntPtr Data, IntPtr Object, double x1, double y1, double x2, double y2, double x3, double y3);
   public delegate int  TClipPath(IntPtr Data, IntPtr Object, lbool EvenOdd, TPathFillMode Mode);
   public delegate int  TClosePath(IntPtr Data, IntPtr Object, TPathFillMode Mode);
   public delegate int  TDrawShading(IntPtr Data, IntPtr Object, TShadingType Type, IntPtr Shading);
   public delegate void TEndLayer(IntPtr Data, IntPtr OC, lbool InVisible);
   public delegate void TEndPattern(IntPtr Data);
   public delegate void TEndTemplate(IntPtr Data);
   public delegate int  TInsertImage(IntPtr Data, ref TPDFImage Image);
   public delegate int  TLineTo(IntPtr Data, IntPtr Object, double x, double y);
   public delegate int  TMoveTo(IntPtr Data, IntPtr Object, double x ,double y);
   public delegate void TMulMatrix(IntPtr Data, IntPtr Object, ref TCTM Matrix);
   public delegate int  TRectangle(IntPtr Data, IntPtr Object, double x, double y, double w, double h);
   public delegate int  TRestoreGraphicState(IntPtr Data);
   public delegate int  TSaveGraphicState(IntPtr Data);
   public delegate void TSetCharSpacing(IntPtr Data, IntPtr Object, double Value);
   public delegate void TSetExtGState(IntPtr Data, IntPtr Object, ref TPDFExtGState2 GS);
   public delegate void TSetFillColor(IntPtr Data, IntPtr Object, IntPtr Color, int NumComps, TExtColorSpace CS, IntPtr IColorSpace);
   public delegate void TSetFont(IntPtr Data, IntPtr Object, TFontType Type, lbool Embedded, IntPtr FontName, TFStyle Style, double FontSize, IntPtr IFont);
   public delegate void TSetLeading(IntPtr Data, IntPtr Object, double Value);
   public delegate void TSetLineCapStyle(IntPtr Data, IntPtr Object, TLineCapStyle Style);
   public delegate void TSetLineDashPattern(IntPtr Data, IntPtr Object, IntPtr Dash, int NumValues, int Phase);
   public delegate void TSetLineJoinStyle(IntPtr Data, IntPtr Object, TLineJoinStyle Style);
   public delegate void TSetLineWidth(IntPtr Data, IntPtr Object, double Value);
   public delegate void TSetMiterLimit(IntPtr Data, IntPtr Object, double Value);
   public delegate void TSetStrokeColor(IntPtr Data, IntPtr Object, IntPtr Color, int NumComps, TExtColorSpace CS, IntPtr IColorSpace);
   public delegate void TSetTextDrawMode(IntPtr Data, IntPtr Object, TDrawMode Mode);
   public delegate void TSetTextScale(IntPtr Data, IntPtr Object, double Value);
   public delegate void TSetWordSpacing(IntPtr Data, IntPtr Object, double Value);
   // It is not required to declare the marshalling attributes in the real callback function.
   public delegate int  TShowTextArrayA(IntPtr Data, IntPtr Object, ref TCTM Matrix, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] TTextRecordA[] Source, int Count, double Width);
   public delegate int  TShowTextArrayW(IntPtr Data, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] TTextRecordA[] Source, ref TCTM Matrix, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)]TTextRecordW[] Kerning, int Count, double Width, lbool Decoded);
   
   

   // Unnecessary callback functions can be set to null -> already default when creating a new instance of the structure.
   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TPDFParseInterface
   {
      public TApplyPattern          ApplyPattern;
      public TBeginPattern          BeginPattern;
      public TBeginTemplate         BeginTemplate;
      public TBezierTo1             BezierTo1;
      public TBezierTo2             BezierTo2;
      public TBezierTo3             BezierTo3;
      public TClipPath              ClipPath;
      public TClosePath             ClosePath;
      public TDrawShading           DrawShading;
      public TEndPattern            EndPattern;
      public TEndTemplate           EndTemplate;
      public TLineTo                LineTo;
      public TMoveTo                MoveTo;
      public TMulMatrix             MulMatrix;
      public TRectangle             Rectangle;
      public TRestoreGraphicState   RestoreGraphicState;
      public TSaveGraphicState      SaveGraphicState;
      public TSetCharSpacing        SetCharSpacing;
      public TSetExtGState          SetExtGState;
      public TSetFillColor          SetFillColor;
      public TSetFont               SetFont;
      public TSetLeading            SetLeading;
      public TSetLineCapStyle       SetLineCapStyle;
      public TSetLineDashPattern    SetLineDashPattern;
      public TSetLineJoinStyle      SetLineJoinStyle;
      public TSetLineWidth          SetLineWidth;
      public TSetMiterLimit         SetMiterLimit;
      public TSetStrokeColor        SetStrokeColor;
      public TSetTextDrawMode       SetTextDrawMode;
      public TSetTextScale          SetTextScale;
      public TSetWordSpacing        SetWordSpacing;
      internal IntPtr               Reserved001;
      internal IntPtr               Reserved002;
      public TShowTextArrayW        ShowTextArrayW;
      public TInsertImage           InsertImage;
      public TShowTextArrayA        ShowTextArrayA;
      public TBeginLayer            BeginLayer;
      public TEndLayer              EndLayer;
      internal IntPtr               Reserved01;
      internal IntPtr               Reserved02;
      internal IntPtr               Reserved03;
      internal IntPtr               Reserved04;
      internal IntPtr               Reserved05;
      internal IntPtr               Reserved06;
      internal IntPtr               Reserved07;
      internal IntPtr               Reserved08;
      internal IntPtr               Reserved09;
      internal IntPtr               Reserved10;
      internal IntPtr               Reserved11;
      internal IntPtr               Reserved12;
      internal IntPtr               Reserved13;
      internal IntPtr               Reserved14;
      internal IntPtr               Reserved15;
      internal IntPtr               Reserved16;
      internal IntPtr               Reserved17;
      internal IntPtr               Reserved18;
      internal IntPtr               Reserved19;
      internal IntPtr               Reserved20;
      internal IntPtr               Reserved21;
      internal IntPtr               Reserved22;
      internal IntPtr               Reserved23;
      internal IntPtr               Reserved24;
      internal IntPtr               Reserved25;
   }

   /* ---------------------------------------------- Callback functions ---------------------------------------------- */

   // Data is always a user defined pointer which is passed unchanged to the callback funtion.
   // However, this pointer is usually set to IntPtr.Zero because no pointers are available in
   // .Net languages. However, because anything is declared in classes there is normally no need
   // to use it. The pointer Data is required in other programming languages like C/C++ or Delphi.

   // Note: The data type lbool is an alias for int.
   public delegate int TErrorProc(IntPtr Data, int ErrCode, IntPtr ErrMessage, int ErrType);
   public delegate int TEnumFontProc(IntPtr Data, IntPtr FamilyName, IntPtr PostScriptName, int Style);
   public delegate int TEnumFontProc2(IntPtr Data, IntPtr PDFFont, TFontType Type, IntPtr BaseFont, IntPtr FontName, lbool Embedded, lbool IsFormFont, uint Flags);
   public delegate int TEnumFontProcEx(IntPtr Data, IntPtr FamilyName, IntPtr PostScriptName, TFStyle Style, TFontBaseType BaseType, TEnumFontProcFlags Embeddable, IntPtr FilePath);
   public delegate int TOnFontNotFoundProc(IntPtr Data, IntPtr PDFFont, IntPtr FontName, TFStyle Style, int StdFontIndex, lbool IsSymbolFont);
   public delegate int TOnPageBreakProc(IntPtr Data, double LastPosX, double LastPosY, lbool PageBreak);
   public delegate int TOnReplaceICCProfile(IntPtr Data, TICCProfileType Type, int ColorSpace);

   /* ------------------------------- Callback functions to control a progress bar ---------------------------------- */

   public enum TProgType
   {
      ptImportPage, // Start page import
      ptWritePage,  // Start writing a page to file or buffer
      ptPrintPage,  // Start printing the PDF file
      ptConvertPage // Start converting the PDF file
   }

   // TInitProgressProc is called before TProgressProc is called the first time.
   public delegate void TInitProgressProc(IntPtr Data, TProgType ProgType, int MaxCount);
   public delegate int  TProgressProc(IntPtr Data, int ActivePage);


   /* ----------------------------------- Rendering API ----------------------------------- */

   public enum TPDFCursor
   {
      pcrHandNormal = 0,
      pcrHandClosed = 1,
      pcrHandPoint  = 2,
      pcrIBeam      = 3
   }

   /*
      The path names can be set in Ansi (code page 1252 on Windows) or Unicode format. The Ansi version accepts
      UTF-8 strings on non-Windows operating systems. UTF-16 Unicode strings are converted to UTF-8 on non-Windows
      operating systems.

      In general, the DefInXXX profiles are used if no other profile is available for the color space. Possible
      sources are DefaultGray, DefaultRGB, DefaultCMYK, and the Rendering Intents.

      The SoftProof profile emulates the output device. This is typically a printer profile or a default CMYK
      profile. If no profile is set then no device will be emulated. What you see is maybe not what you get on
      a printer.

      To disable color management set the parameter Profiles of rasInitColormanagement() to NULL.
   */
   [StructLayout(LayoutKind.Sequential, Pack = 0)]
   public class TPDFColorProfiles
   {
      public int StructSize;       // Must be set to sizeof(TPDFColorProfile)
      [MarshalAs(UnmanagedType.LPStr)]
      public String DefInGrayA;     // Optional
      [MarshalAs(UnmanagedType.LPWStr)]
      public String DefInGrayW;     // Optional
      [MarshalAs(UnmanagedType.LPStr)]
      public String DefInRGBA;      // Optional, sRGB is the default. The "A" stands for Ansi string and not for Alpha...
      [MarshalAs(UnmanagedType.LPWStr)]
      public String DefInRGBW;      // Optional
      [MarshalAs(UnmanagedType.LPStr)]
      public String DefInCMYKA;     // Optional, CMYK colors are the problematic ones. The other profiles can be created on demand
      [MarshalAs(UnmanagedType.LPWStr)]
      public String DefInCMYKW;     // but this is not possible with a CMYK profile. So, this is the most important input profile.
      [MarshalAs(UnmanagedType.LPStr)]
      public String DeviceProfileA; // Optional, the output profile must be compatible with the output color space.
      [MarshalAs(UnmanagedType.LPWStr)]
      public String DeviceProfileW; // Gray, RGB, or CMYK profiles are supported.
      [MarshalAs(UnmanagedType.LPStr)]
      public String SoftProofA;     // Optional but very important. This profile emulates the output device.
      [MarshalAs(UnmanagedType.LPWStr)]
      public String SoftProofW;     // Optional.
   }

   [StructLayout(LayoutKind.Sequential, Pack = 0)]
   public class TPDFColorProfilesEx
   {
      public int    StructSize;       // Must be set to sizeof(TPDFColorProfilesEx)
      public IntPtr DefInGray;        // Optional
      public int    DefInGrayLen;     // Optional
      public IntPtr DefInRGB;         // Optional
      public int    DefInRGBLen;      // Optional
      public IntPtr DefInCMYK;        // Optional, CMYK colors are the problematic ones. The other profiles can be created on demand
      public int    DefInCMYKLen;     // but this is not possible with a CMYK profile. So, this is the most important input profile.
      public IntPtr DeviceProfile;    // Optional, the output profile must be compatible with the output color space.
      public int    DeviceProfileLen; // Gray, RGB, or CMYK profiles are supported.
      public IntPtr SoftProof;        // Optional but very important. This profile emulates the output device.
      public int    SoftProofLen;     // Optional.
   }

   public enum TPDFInitCMFlags
   {
      icmDefault        = 0,  // Default rules.
      icmBPCompensation = 1,  // Black point compensation preserves the black point when converting CMYK colors to different color spaces.
      icmCheckBlackPoint = 2  // If set, soft proofing will be disabled if the black point of the output intent is probably invalid.
   }

   public enum TInitCacheFlags
   {
      icfDefault          = 0,
      icfIgnoreOpenAction = 1,
      icfIgnorePageLayout = 2
   }

   public enum TPDFPixFormat
   {
      pxf1Bit,
      pxfGray,
      pxfRGB,
      pxfBGR,
      pxfRGBA,
      pxfBGRA,
      pxfARGB,
      pxfABGR,
      pxfGrayA,
      pxfCMYK,
      pxfCMYKA
   }

   public enum TPDFPageScale
   {
      psFitWidth,  // Scale the page to the width of the image buffer
      psFitHeight, // Scale the page to the height of the image buffer
      psFitBest,   // Scale the page so that it fits fully into the image buffer
      psFitZoom    // This mode should be used if the scaling factors of the transformation matrix are <> 1.0
   }

   public enum TPDFThreadPriority
   {
      ttpLowest,
      ttpIdle,
      ttpBelowNormal, // This is the default value. Normal can be used too but scrolling is smoother in this mode.
      ttpNormal,
      ttpAboveNormal,
      ttpHighest,     // Not really useful...
      ttpTimeCritical // Don't do that!
   }

   public enum TRasterFlags
   {
      rfDefault         = 0x00000000, // Render the page as usual
      rfScaleToMediaBox = 0x00000001, // Render the real paper format. Contents outside the crop box is clipped
      rfIgnoreCropBox   = 0x00000002, // Ignore the crop box and render anything inside the media box without clipping
      // Only one of these flags must be set at time!
      rfClipToArtBox    = 0x00000004, // Clip the page to the art box if any
      rfClipToBleedBox  = 0x00000008, // Clip the page to the bleed box if any
      rfClipToTrimBox   = 0x00000010, // Clip the page to the trim box if any
      rfExclAnnotations = 0x00000020, // Don't render annotations
      rfExclFormFields  = 0x00000040, // Don't render form fields
      rfSkipUpdateBG    = 0x00000080, // Don't generate an update event after initializing the background to white
      rfRotate90        = 0x00000100, // Rotate the page 90 degress
      rfRotate180       = 0x00000200, // Rotate the page 180 degress
      rfRotate270       = 0x00000400, // Rotate the page 270 degress
      rfInitBlack       = 0x00000800, // Initialize the image buffer to black before rendering (RGBA or GrayA must be initialized to black)
      rfCompositeWhite  = 0x00001000, // Composite pixel formats with an alpha channel finally with a white background. The alpha channel is
                                      // 255 everywhere arfer composition. This flag is mainly provided for debug purposes but it can also be
                                      // useful if the image must be copied on screen with a function that doesn't support alpha blending.
      rfExclPageContent = 0x00002000, // If set, only annotations and form fields will be rendered (if any).

      // If you want to render specific field types with RenderAnnotOrField() then use the following flags to exclude these fields.
      // If all fields should be skipped then set the flag rfExclFormFields instead.
      rfExclButtons     = 0x00004000,
      rfExclCheckBoxes  = 0x00008000,
      rfExclComboBoxes  = 0x00010000,
      rfExclListBoxes   = 0x00020000,
      rfExclTextFields  = 0x00040000,
      rfExclSigFields   = 0x00080000,
      // ---------------------------------
      rfScaleToBBox           = 0x00100000, // Considered only, if the flag rfClipToArtBox, rfClipToBleedBox, or rfClipToTrimBox is set.
                                            // If set, the picture size is set to the size of the whished bounding box.
      rfDisableAAClipping     = 0x00200000, // Disable Anti-Aliasing for clipping paths. This flag is the most important one since clipping paths
                                            // cause often visible artefacts in PDF files with flattened transparency.
      rfDisableAAText         = 0x00400000, // Disable Anti-Aliasing for text.
      rfDisableAAVector       = 0x00800000, // Disable Anti-Aliasing for vector graphics.
      rfDisableAntiAliasing   = rfDisableAAClipping | rfDisableAAText | rfDisableAAVector, // Fully disable Anti-Aliasing.
      rfDisableBiLinearFilter = 0x01000000, // Disable the BiLevel filter for images. Sometetimes useful if sharp images are needed, e.g. for barcodes.
      rfRenderInvisibleText   = 0x02000000, // If set, treat text rendering mode Invisible as Normal.
      rfEnableBlendCS         = 0x10000000, // If set, the page is rendered in the color space of the page group, soft proof color space, or output intent if set, and
                                            // finally converted to the destination color space.
      rfRenderPrintState      = 0x20000000, // If set, the print state of layers, annotations, and form fields will be rendered.
      rfForceInterpolation    = 0x40000000  // If set, image interpolation will be applied.
   }
   
   public enum TSelectObjType
   {
      sotDefault    = 0, // Hand tool
      sotAnnotation = 1, // Select annotations
      sotFormField  = 2, // Select form fields
      sotImage      = 4, // Select images
      sotText       = 8, // Select text
      sotVector     = 16 // Select vector graphics
   }

   public enum TUpdBmkAction
   {
      ubaDoNothing     = 0,  // Nothing to do
      ubaOpenPage      = 1,  // Jump to the new page. This flag is set if the bookmark contained a destination or go to action.
      ubaPageScale     = 2,  // Update the page scale with SetPageScale().
      ubaZoom          = 4,  // Zoom into the page, update the scroll ranges, and set the scroll positions.
      ubaUpdScrollBars = 8,  // This flag is always set if ubaZoom is set.
      ubaExecAction    = 16  // Check the parameter Action to execute further code. This flag can occur with or without ubaOpenPage.
   }

   public enum TUpdScrollbar
   {
      usbNoUpdate         = 0,   // Nothing to do
      usbVertRange        = 1,   // Update the vertical scroll range
      usbVertScrollPos    = 2,   // Update the vertical scroll position
      usbHorzRange        = 4,   // Update the horizontal scroll range
      usbHorzScrollPos    = 8,   // Update the horizontal scroll position
      usbUpdateAll        = 15,  // Update both scroll ranges and the scroll positions
      // The cursor constants are set by MouseMove. Since we have only one cursor there is never more than one constant set.
      usbCursorHandNormal = 16,  // This is the default if the left mouse button is not pressed and if we are not over an action field
      usbCursorHandClosed = 32,  // Occurs when the cursor leaves an action field and if the left mouse button is pressed
      usbCursorHandPoint  = 64,  // Occurs when we enter link or button field
      usbCursorIBeam      = 128, // Occurs when we enter an action field that accepts text input
      usbCursorMask       = 240  // Bitmask to mask out the cursor constants
   }

   public delegate int TOnUpdateWindow(IntPtr Data, ref TIntRect Area);

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TPDFRasterImage
   {
      public int              StructSize;            // Must be set to sizeof(TPDFRasterImage)
      public TRasterFlags     Flags;                 // This is a bit mask. Flags can be combined with a binary or operator
      public TPDFPageScale    DefScale;              // Specifies how the page should be scaled into the image buffer.

      public int              InitWhite;             // Boolean 0 or 1 -> If true, the image buffer is initialized to white before
                                                     // rendering. When a clipping rectangle is set, only the area inside the clipping
                                                     // rectangle is initialized to white.

      public TIntRect         ClipRect;              // Optional clipping rectangle defined in device coordinates (Pixels), default 0,0,0,0 (no clipping)
      public TCTM             Matrix;                // Optional transformation matrix. Initialize the variable to the identity matrix (1,0,0,1,0,0)
                                                     // if you don't need it. The matrix can be used to move and scale the picture inside the image. 
      public TCTM             PageSpace;             // Out -> The matrix represents the mapping from page space to device space. This matrix
                                                     // is required when further objects should be drawn on the page, e.g. the bounding boxes.

      public int              DrawFrameRect;         // If true, the area outside the page's bounding box is filled with the
                                                     // frame color. InitWhite can still be used, with or without a clipping
                                                     // rectangle.
      public uint             FrameColor;            // Must be defined in the color space of the pixel format but in the natural
                                                     // component order, e.g. RGB.

      public TOnUpdateWindow  OnUpdateWindow;        // Optional, UpdateOnPathCount and UpdateOnImageCoverage define when the function should be called
      public IntPtr           OnInitDecoder;         // Not yet defined
      public IntPtr           OnDecodeLine;          // Not yet defined
      public IntPtr           UserData;              // Arbitrary pointer that should be passed to the callback functions

      public int              UpdateOnPathCount;     // Optional -> Call OnUpdateWindow when the limit was reached.
                                                     // Clipping paths increment the number too.
                                                     // Only full paths are considered, independent of the number of vertices
                                                     // they contain. The value should be larger than 50 and smaller than 10000.

      public float            UpdateOnImageCoverage; // Optional -> DynaPDF multiplies the output image width and height with this
                                                     // factor to calculate the coverage limit. When an image is inserted the unscaled
                                                     // width and height is added to the current coverage value. When the number
                                                     // reaches the limit the OnUpdateWindow event is raised.
                                                     // The factor should be around 0.5 through 5.0. Larger values cause less
                                                     // frequently update events.
      // Statistics...
      public uint             NumAnnots;             // Out -> Number of rendered annotations (excluding invisible annotation but including annotations with no appearance)
      public uint             NumBezierCurves;       // Out -> Number of bezier curves which where rendered. Glyph outlines are not taken into account.
      public uint             NumClipPaths;          // Out -> Number of clipping paths used in the page. Should be small as possible!
      public uint             NumFormFields;         // Out -> Number of rendered form fields (excluding invisible fields but including fields with no appearance)
      public uint             NumGlyphs;             // Out -> When the number of glyphs equals NumTextRecords then there is probably some room for optimization...
      public uint             NumImages;             // Out -> Number of images which were rendered
      public uint             NumLineTo;             // Out -> Number of LineTo operators
      public uint             NumPaths;              // Out -> Number of paths which were processed
      public uint             NumPatterns;           // Out -> Number of pattern which were processed
      public uint             NumRectangles;         // Out -> Number of rectangle operators
      public uint             NumRestoreGState;      // Out -> Should be equal to NumSaveGState
      public uint             NumSaveGState;         // Out -> The number of save graphics state operators
      public uint             NumShadings;           // Out -> Number shadings which were processed
      public uint             NumSoftMasks;          // Out -> Number of soft masks that were processed. Alpha channels of images are not taken into account.
      public uint             NumTextRecords;        // Out -> Number of text records which were rendered
   }

   /* ------------------------------------------------------------------------------------- */

   public class CPDF : IDisposable
   {
      private const string m_DynaPDFLib = "dynapdf";
   
      public const int  PDF_MAX_INT = 0x7FFFFFFF;
      // Basic RGB colors
      public const uint PDF_AQUA    = 0x00FFFF00;
      public const uint PDF_BLACK   = 0x00000000;
      public const uint PDF_BLUE    = 0x00FF0000;
      public const uint PDF_CREAM   = 0x00F0FBFF;
      public const uint PDF_DKGRAY  = 0x00808080;
      public const uint PDF_FUCHSIA = 0x00FF00FF;
      public const uint PDF_GRAY    = 0x00808080;
      public const uint PDF_GREEN   = 0x00008000;
      public const uint PDF_LIME    = 0x0000FF00;
      public const uint PDF_LTGRAY  = 0x00C0C0C0;
      public const uint PDF_MAROON  = 0x00000080;
      public const uint PDF_MEDGRAY = 0x00A4A0A0;
      public const uint PDF_MOGREEN = 0x00C0DCC0;
      public const uint PDF_NAVY    = 0x00800000;
      public const uint PDF_OLIVE   = 0x00008080;
      public const uint PDF_PURPLE  = 0x00800080;
      public const uint PDF_RED     = 0x000000FF;
      public const uint PDF_SILVER  = 0x00C0C0C0;
      public const uint PDF_SKYBLUE = 0x00F0CAA6;
      public const uint PDF_WHITE   = 0x00FFFFFF;
      public const uint PDF_TEAL    = 0x00808000;
      public const uint PDF_YELLOW  = 0x0000FFFF;
      // This value can be used for form fields, annotations and standard patterns to determine that the specific color is not set.
      // For example, when a form field should have a transparent background, set the field background color
      // to NO_COLOR. The background appears then transparent.
      public const uint NO_COLOR    = 0xFFFFFFF1;
      // Special flag for GetPageFieldEx() to indicate that an annotation index
      // was passed to the function. See GetPageFieldEx() for further information.
      public const uint PDF_ANNOT_INDEX = 0x40000000;
      // This flag can be combined with the annotation handle in Set3DAnnotProps().
      // 3D Annotations with a transparent background are supported since PDF 1.7, Extension Level 3
      public int TRANSP_3D_ANNOT = 0x40000000;

      public CPDF()
      {
         m_Instance    = pdfNewPDF();
         m_ExtInstance = false;
      }

      public CPDF(IntPtr ExtInstance)
      {
         m_Instance    = ExtInstance;
         m_ExtInstance = true;
      }

      ~CPDF()
      {
         DoDispose();
      }

      public void Dispose()
      {
         // do dispose code
         DoDispose();

         //suppress finalize
         GC.SuppressFinalize(this);
      }

      private void DoDispose()
      {
         //prevent this code from being called when it is already being called in another thread.
         lock(this)
         {
            if (!IntPtr.Zero.Equals(m_Instance) && !m_ExtInstance)
            {
               pdfDeletePDF(m_Instance);
               m_Instance = IntPtr.Zero;
            }
         }
      }

      public void Abort(IntPtr RasPtr)
      {
         rasAbort(RasPtr);
      }

      public bool ActivateAltFontList(int Handle, bool EnableSysFonts)
      {
         return pdfActivateAltFontList(m_Instance, Handle, Convert.ToInt32(EnableSysFonts)) != 0;
      }

      public bool AddActionToObj(TObjType ObjType, TObjEvent Event, int ActHandle, int ObjHandle)
      {
         return pdfAddActionToObj(m_Instance, ObjType, Event, ActHandle, ObjHandle) != 0;
      }

      public bool AddAnnotToPage(int PageNum, int Handle)
      {
         return pdfAddAnnotToPage(m_Instance, PageNum, Handle) != 0;
      }

      public int AddArticle(double PosX, double PosY, double Width, double Height)
      {
         return pdfAddArticle(m_Instance, PosX, PosY, Width, Height);
      }

      public int AddBookmark(String Title, int Parent, int DestPage, bool Open)
      {
         return pdfAddBookmarkW(m_Instance, Title, Parent, DestPage, Convert.ToInt32(Open));
      }

      public int AddBookmarkA(String Title, int Parent, int DestPage, bool Open)
      {
         return pdfAddBookmarkA(m_Instance, Title, Parent, DestPage, Convert.ToInt32(Open));
      }

      public int AddBookmarkW(String Title, int Parent, int DestPage, bool Open)
      {
         return pdfAddBookmarkW(m_Instance, Title, Parent, DestPage, Convert.ToInt32(Open));
      }

      public int AddBookmarkEx(String Title, int Parent, int NamedDest, bool Open)
      {
         return pdfAddBookmarkExW(m_Instance, Title, Parent, NamedDest, Convert.ToInt32(Open));
      }

      public int AddBookmarkExA(String Title, int Parent, int NamedDest, bool Open)
      {
         return pdfAddBookmarkExA(m_Instance, Title, Parent, NamedDest, Convert.ToInt32(Open));
      }

      public int AddBookmarkExW(String Title, int Parent, int NamedDest, bool Open)
      {
         return pdfAddBookmarkExW(m_Instance, Title, Parent, NamedDest, Convert.ToInt32(Open));
      }

      public int AddBookmarkEx2(String Title, int Parent, String NamedDest, bool Unicode, bool Open)
      {
         if (Unicode)
            return pdfAddBookmarkEx2WW(m_Instance, Title, Parent, NamedDest, 1, Convert.ToInt32(Open));
         else
            return pdfAddBookmarkEx2WA(m_Instance, Title, Parent, NamedDest, 0, Convert.ToInt32(Open));
      }

      public int AddBookmarkEx2A(String Title, int Parent, String NamedDest, bool Unicode, bool Open)
      {
         if (Unicode)
            return pdfAddBookmarkEx2AW(m_Instance, Title, Parent, NamedDest, 1, Convert.ToInt32(Open));
         else
            return pdfAddBookmarkEx2AA(m_Instance, Title, Parent, NamedDest, 0, Convert.ToInt32(Open));
      }

      public int AddBookmarkEx2W(String Title, int Parent, String NamedDest, bool Unicode, bool Open)
      {
         if (Unicode)
            return pdfAddBookmarkEx2WW(m_Instance, Title, Parent, NamedDest, 1, Convert.ToInt32(Open));
         else
            return pdfAddBookmarkEx2WA(m_Instance, Title, Parent, NamedDest, 0, Convert.ToInt32(Open));
      }

      public bool AddButtonImage(int BtnHandle, TButtonState State, String Caption, String ImgFile)
      {
         return pdfAddButtonImage(m_Instance, BtnHandle, State, Caption, ImgFile) != 0;
      }

      public bool AddButtonImageEx(int BtnHandle, TButtonState State, String Caption, IntPtr hBitmap)
      {
         return pdfAddButtonImageEx(m_Instance, BtnHandle, State, Caption, hBitmap) != 0;
      }

      public bool AddButtonImageEx2(int BtnHandle, TButtonState State, String Caption, byte[] Buffer)
      {
         return pdfAddButtonImageEx2(m_Instance, BtnHandle, State, Caption, Buffer, Buffer.Length) != 0;
      }

      public bool AddButtonImageEx2(int BtnHandle, TButtonState State, String Caption, IntPtr Buffer, int BufSize)
      {
         return pdfAddButtonImageEx2I(m_Instance, BtnHandle, State, Caption, Buffer, BufSize) != 0;
      }

      public bool AddContinueText(String AText)
      {
         return pdfAddContinueTextW(m_Instance, AText) != 0;
      }

      public bool AddContinueTextA(String AText)
      {
         return pdfAddContinueTextA(m_Instance, AText) != 0;
      }

      public bool AddContinueTextW(String AText)
      {
         return pdfAddContinueTextW(m_Instance, AText) != 0;
      }

      public bool AddDeviceNProcessColorants(int DeviceNCS, String[] Colorants, int NumColorants, TExtColorSpace ProcessCS, int Handle)
      {
         return pdfAddDeviceNProcessColorants(m_Instance, DeviceNCS, Colorants, NumColorants, ProcessCS, Handle) != 0;
      }

      public bool AddDeviceNSeparations(int DeviceNCS, String[] Colorants, int[] SeparationCS, int NumColorants)
      {
         return pdfAddDeviceNSeparations(m_Instance, DeviceNCS, Colorants, SeparationCS, NumColorants) != 0;
      }

      public int AddDPartNode(int Parent, int FirstPage, int LastPage, String DPM)
      {
         return pdfAddDPartNode(m_Instance, Parent, FirstPage, LastPage, DPM);
      }

      public bool AddFieldToFormAction(int Action, int Field, bool Include)
      {
         return pdfAddFieldToFormAction(m_Instance, Action, Field, Convert.ToInt32(Include)) != 0;
      }

      public bool AddFieldToHideAction(int HideAct, int Field)
      {
         return pdfAddFieldToHideAction(m_Instance, HideAct, Field) != 0;
      }

      public bool AddFileCommentA(String AText)
      {
         return pdfAddFileCommentA(m_Instance, AText) != 0;
      }

      public bool AddFileCommentW(String AText)
      {
         return pdfAddFileCommentW(m_Instance, AText) != 0;
      }

      public int AddFontSearchPath(String APath, bool Recursive)
      {
         return pdfAddFontSearchPathW(m_Instance, APath, Convert.ToInt32(Recursive));
      }

      public int AddFontSearchPathA(String APath, bool Recursive)
      {
         return pdfAddFontSearchPathA(m_Instance, APath, Convert.ToInt32(Recursive));
      }

      public int AddFontSearchPathW(String APath, bool Recursive)
      {
         return pdfAddFontSearchPathW(m_Instance, APath, Convert.ToInt32(Recursive));
      }

      public bool AddHeaderFooter(ref TPDFHeaderFooter Value, ref TPDFHdrFtr[] HFArray)
      {
         int i;
         bool retval = false;
         TPDFHeaderFooter_I val = new TPDFHeaderFooter_I();
         TPDFHdrFtr_I[] hf = new TPDFHdrFtr_I[HFArray.Length];

         val.StructSize    = Marshal.SizeOf(val);
         val.FirstPage     = Value.FirstPage;
         val.Flags         = Value.Flags;
         val.InitCodepage  = Value.InitCodepage;
         val.InitColor     = Value.InitColor;
         val.InitCS        = Value.InitCS;
         val.InitCSHandle  = Value.InitCSHandle;
         val.InitEmbed     = Convert.ToInt32(Value.InitEmbed);
         val.InitFontW     = Marshal.StringToHGlobalUni(Value.InitFont);
         val.InitFontSize  = Value.InitFontSize;
         val.LastPage      = Value.LastPage;
         val.Margin        = Value.Margin;
         try
         {
            for (i = 0; i < HFArray.Length; i++)
               ConvertToHdrFtr_I(ref HFArray[i], ref hf[i]);
            retval = pdfAddHeaderFooter(m_Instance, ref val, hf, hf.Length) != 0;
         }finally
         {
            // Clean up
            Marshal.FreeHGlobal(val.InitFontW);
            for (i = 0; i < HFArray.Length; i++)
            {
               Marshal.FreeHGlobal(hf[i].FontW);
               Marshal.FreeHGlobal(hf[i].TextW);
            }
         }
         return retval;
      }

      public bool AddImage(TCompressionFilter Filter, TImageConversionFlags Flags, TPDFImage Image)
      {
         return pdfAddImage(m_Instance, Filter, Flags, ref Image) != 0;
      }

      public bool AddInkList(int InkAnnot, TFltPoint[] Points)
      {
         return pdfAddInkList(m_Instance, InkAnnot, Points, Points.Length) != 0;
      }

      public int AddJavaScript(String Name, String Script)
      {
         return pdfAddJavaScriptW(m_Instance, Name, Script);
      }

      public int AddJavaScriptA(String Name, String Script)
      {
         return pdfAddJavaScriptA(m_Instance, Name, Script);
      }

      public int AddJavaScriptW(String Name, String Script)
      {
         return pdfAddJavaScriptW(m_Instance, Name, Script);
      }

      public IntPtr AddLayerToDisplTree(IntPtr Parent, int Layer, String Title)
      {
         return pdfAddLayerToDisplTreeW(m_Instance, Parent, Layer, Title);
      }

      public IntPtr AddLayerToDisplTreeA(IntPtr Parent, int Layer, String Title)
      {
         return pdfAddLayerToDisplTreeA(m_Instance, Parent, Layer, Title);
      }

      public IntPtr AddLayerToDisplTreeW(IntPtr Parent, int Layer, String Title)
      {
         return pdfAddLayerToDisplTreeW(m_Instance, Parent, Layer, Title);
      }

      public bool AddMaskImage(int BaseImage, byte[] Buffer, int BufSize, int Stride, int BitsPerPixel, int Width, int Height)
      {
         return pdfAddMaskImage(m_Instance, BaseImage, Buffer, BufSize, Stride, BitsPerPixel, Width, Height) != 0;
      }

      public bool AddMaskImage(int BaseImage, IntPtr Buffer, int BufSize, int Stride, int BitsPerPixel, int Width, int Height)
      {
         return pdfAddMaskImage(m_Instance, BaseImage, Buffer, BufSize, Stride, BitsPerPixel, Width, Height) != 0;
      }

      public bool AddObjectToLayer(int OCG, TOCObject ObjType, int Handle)
      {
         return pdfAddObjectToLayer(m_Instance, OCG, ObjType, Handle) != 0;
      }

      public bool AddOCGToAppEvent(int Handle, TOCAppEvent Events, TOCGUsageCategory Categories)
      {
         return pdfAddOCGToAppEvent(m_Instance, Handle, Events, Categories) != 0;
      }

      public int AddOutputIntent(String ICCFile)
      {
         return pdfAddOutputIntentW(m_Instance, ICCFile);
      }

      public int AddOutputIntentA(String ICCFile)
      {
         return pdfAddOutputIntentA(m_Instance, ICCFile);
      }

      public int AddOutputIntentW(String ICCFile)
      {
         return pdfAddOutputIntentW(m_Instance, ICCFile);
      }

      public int AddOutputIntentEx(byte[] Buffer)
      {
         return pdfAddOutputIntentEx(m_Instance, Buffer, Buffer.Length);
      }

      public int AddPageLabel(int StartRange, TPageLabelFormat Format, String Prefix, int AddNum)
      {
         return pdfAddPageLabelW(m_Instance, StartRange, Format, Prefix, AddNum);
      }

      public int AddPageLabelA(int StartRange, TPageLabelFormat Format, String Prefix, int AddNum)
      {
         return pdfAddPageLabelA(m_Instance, StartRange, Format, Prefix, AddNum);
      }

      public int AddPageLabelW(int StartRange, TPageLabelFormat Format, String Prefix, int AddNum)
      {
         return pdfAddPageLabelW(m_Instance, StartRange, Format, Prefix, AddNum);
      }

      public bool AddRasImage(IntPtr RasPtr, TCompressionFilter Filter)
      {
         return pdfAddRasImage(m_Instance, RasPtr, Filter) != 0;
      }

      public int AddRenderingIntent(String ICCFile)
      {
         return pdfAddRenderingIntentW(m_Instance, ICCFile);
      }

      public int AddRenderingIntentA(String ICCFile)
      {
         return pdfAddRenderingIntentA(m_Instance, ICCFile);
      }

      public int AddRenderingIntentW(String ICCFile)
      {
         return pdfAddRenderingIntentW(m_Instance, ICCFile);
      }

      public int AddRenderingIntentEx(byte[] Buffer)
      {
         return pdfAddRenderingIntentEx(m_Instance, Buffer, Buffer.Length);
      }

      public bool AddValToChoiceField(int Field, String ExpValue, String Value, bool Selected)
      {
         return pdfAddValToChoiceFieldW(m_Instance, Field, ExpValue, Value, Convert.ToInt32(Selected)) != 0;
      }

      public bool AddValToChoiceFieldA(int Field, String ExpValue, String Value, bool Selected)
      {
         return pdfAddValToChoiceFieldA(m_Instance, Field, ExpValue, Value, Convert.ToInt32(Selected)) != 0;
      }

      public bool AddValToChoiceFieldW(int Field, String ExpValue, String Value, bool Selected)
      {
         return pdfAddValToChoiceFieldW(m_Instance, Field, ExpValue, Value, Convert.ToInt32(Selected)) != 0;
      }

      public bool Append()
      {
         return pdfAppend(m_Instance) != 0;
      }

      public bool ApplyAppEvent(TOCAppEvent Event, bool SaveResult)
      {
         return pdfApplyAppEvent(m_Instance, Event, Convert.ToInt32(SaveResult)) != 0;
      }

      public bool ApplyPattern(int PattHandle, TColorMode ColorMode, uint Color)
      {
         return pdfApplyPattern(m_Instance, PattHandle, ColorMode, Color) != 0;
      }

      public bool ApplyShading(int ShadHandle)
      {
         return pdfApplyShading(m_Instance, ShadHandle) != 0;
      }

      public bool AssociateEmbFile(TAFDestObject DestObject, int DestHandle, TAFRelationship Relationship, int EmbFile)
      {
         return pdfAssociateEmbFile(m_Instance, DestObject, DestHandle, Relationship, EmbFile) != 0;
      }

      public int AttachFile(String FilePath, String Description, bool Compress)
      {
         return pdfAttachFileW(m_Instance, FilePath, Description, Convert.ToInt32(Compress));
      }

      public int AttachFileA(String FilePath, String Description, bool Compress)
      {
         return pdfAttachFileA(m_Instance, FilePath, Description, Convert.ToInt32(Compress));
      }

      public int AttachFileW(String FilePath, String Description, bool Compress)
      {
         return pdfAttachFileW(m_Instance, FilePath, Description, Convert.ToInt32(Compress));
      }

      public int AttachFileEx(byte[] Buffer, int BufSize, String FileName, String Description, bool Compress)
      {
         return pdfAttachFileExW(m_Instance, Buffer, BufSize, FileName, Description, Convert.ToInt32(Compress));
      }

      public int AttachFileExA(byte[] Buffer, int BufSize, String FileName, String Description, bool Compress)
      {
         return pdfAttachFileExA(m_Instance, Buffer, BufSize, FileName, Description, Convert.ToInt32(Compress));
      }

      public int AttachFileExW(byte[] Buffer, int BufSize, String FileName, String Description, bool Compress)
      {
         return pdfAttachFileExW(m_Instance, Buffer, BufSize, FileName, Description, Convert.ToInt32(Compress));
      }

      public bool AttachImageBuffer(IntPtr RasPtr, IntPtr Rows, IntPtr Buffer, int Width, int Height, int ScanlineLen)
      {
         return rasAttachImageBuffer(RasPtr, Rows, Buffer, Width, Height, ScanlineLen) != 0;
      }

      public bool AutoTemplate(int Templ, double PosX, double PosY, double Width, double Height)
      {
         return pdfAutoTemplate(m_Instance, Templ, PosX, PosY, Width, Height) != 0;
      }

      public bool BeginClipPath()
      {
         return pdfBeginClipPath(m_Instance) != 0;
      }

      public bool BeginContinueText(double PosX, double PosY)
      {
         return pdfBeginContinueText(m_Instance, PosX, PosY) != 0;
      }

      public bool BeginLayer(int OCG)
      {
         return pdfBeginLayer(m_Instance, OCG) != 0;
      }

      public bool BeginPageTemplate(String Name, lbool UseAutoTemplates)
      {
         return pdfBeginPageTemplate(m_Instance, Name, UseAutoTemplates) != 0;
      }

      public int BeginPattern(TPatternType PatternType, TTilingType TilingType, double Width, double Height)
      {
         return pdfBeginPattern(m_Instance, PatternType, TilingType, Width, Height);
      }

      public int BeginTemplate(double Width, double Height)
      {
         return pdfBeginTemplate(m_Instance, Width, Height);
      }

      public int BeginTemplateEx(ref TPDFRect BBox, ref TCTM Matrix)
      {
         return pdfBeginTemplateEx(m_Instance, ref BBox, ref Matrix);
      }

      public int BeginTransparencyGroup(double x1, double y1, double x2, double y2, bool Isolated, bool Knockout, TExtColorSpace CS, int CSHandle)
      {
         return pdfBeginTransparencyGroup(m_Instance, x1, y1, x2, y2, Convert.ToInt32(Isolated), Convert.ToInt32(Knockout), CS, CSHandle);
      }

      public bool Bezier_1_2_3(double x1, double y1, double x2, double y2, double x3, double y3)
      {
         return pdfBezier_1_2_3(m_Instance, x1, y1, x2, y2, x3, y3) != 0;
      }

      public bool Bezier_1_3(double x1, double y1, double x3, double y3)
      {
         return pdfBezier_1_3(m_Instance, x1, y1, x3, y3) != 0;
      }

      public bool Bezier_2_3(double x2, double y2, double x3, double y3)
      {
         return pdfBezier_2_3(m_Instance, x2, y2, x3, y3) != 0;
      }

      public bool BuildFamilyNameAndStyle(IntPtr IFont, System.Text.StringBuilder Name, ref TFStyle Style)
      {
         if (Name.Capacity < 128) Name.Capacity = 128;
         Name.Length = 0;
         return fntBuildFamilyNameAndStyle(IFont, Name, ref Style) != 0;
      }

      public void CalcPagePixelSize(IntPtr PagePtr, TPDFPageScale DefScale, float Scale, int FrameWidth, int FrameHeight, TRasterFlags Flags, ref int Width, ref int Height)
      {
         rasCalcPagePixelSize(PagePtr, DefScale, Scale, FrameWidth, FrameHeight, Flags, ref Width, ref Height);
      }

      public double CalcWidthHeight(double OrgWidth, double OrgHeight, double ScaledWidth, double ScaledHeight)
      {
         return pdfCalcWidthHeight(m_Instance, OrgWidth, OrgHeight, ScaledWidth, ScaledHeight);
      }

      public int CaretAnnot(double PosX, double PosY, double Width, double Height, uint Color, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfCaretAnnotW(m_Instance, PosX, PosY, Width, Height, Color, CS, Author, Subject, Content);
      }

      public int CaretAnnotA(double PosX, double PosY, double Width, double Height, uint Color, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfCaretAnnotA(m_Instance, PosX, PosY, Width, Height, Color, CS, Author, Subject, Content);
      }

      public int CaretAnnotW(double PosX, double PosY, double Width, double Height, uint Color, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfCaretAnnotW(m_Instance, PosX, PosY, Width, Height, Color, CS, Author, Subject, Content);
      }

      public bool ChangeAnnotPos(int Handle, double PosX, double PosY, double Width, double Height)
      {
         return pdfChangeAnnotPos(m_Instance, Handle, PosX, PosY, Width, Height) != 0;
      }

      public bool ChangeAnnotName(int Handle, String Name)
      {
         return pdfChangeAnnotNameW(m_Instance, Handle, Name) != 0;
      }

      public bool ChangeAnnotNameA(int Handle, String Name)
      {
         return pdfChangeAnnotNameA(m_Instance, Handle, Name) != 0;
      }

      public bool ChangeAnnotNameW(int Handle, String Name)
      {
         return pdfChangeAnnotNameW(m_Instance, Handle, Name) != 0;
      }

      public bool ChangeBookmark(int ABmk, String Title, int Parent, int DestPage, bool Open)
      {
         return pdfChangeBookmarkW(m_Instance, ABmk, Title, Parent, DestPage, Convert.ToInt32(Open)) != 0;
      }

      public bool ChangeBookmarkA(int ABmk, String Title, int Parent, int DestPage, bool Open)
      {
         return pdfChangeBookmarkA(m_Instance, ABmk, Title, Parent, DestPage, Convert.ToInt32(Open)) != 0;
      }

      public bool ChangeBookmarkW(int ABmk, String Title, int Parent, int DestPage, bool Open)
      {
         return pdfChangeBookmarkW(m_Instance, ABmk, Title, Parent, DestPage, Convert.ToInt32(Open)) != 0;
      }

      public bool ChangeFont(int Handle)
      {
         return pdfChangeFont(m_Instance, Handle) != 0;
      }
      
      public bool ChangeFontEx(int Handle, double FontSize, TFStyle Style)
      {
         return pdfChangeFontEx(m_Instance, Handle, FontSize, Style) != 0;
      }

      public bool ChangeFontSize(double Size)
      {
         return pdfChangeFontSize(m_Instance, Size) != 0;
      }

      public bool ChangeFontStyle(TFStyle Style)
      {
         return pdfChangeFontStyle(m_Instance, Style) != 0;
      }

      public bool ChangeFontStyleEx(TFStyle Style)
      {
         return pdfChangeFontStyleEx(m_Instance, Style) != 0;
      }

      public bool ChangeJavaScript(int Handle, String NewScript)
      {
         return pdfChangeJavaScriptW(m_Instance, Handle, NewScript) != 0;
      }

      public bool ChangeJavaScriptA(int Handle, String NewScript)
      {
         return pdfChangeJavaScriptA(m_Instance, Handle, NewScript) != 0;
      }

      public bool ChangeJavaScriptW(int Handle, String NewScript)
      {
         return pdfChangeJavaScriptW(m_Instance, Handle, NewScript) != 0;
      }

      public bool ChangeJavaScriptAction(int Handle, String NewScript)
      {
         return pdfChangeJavaScriptActionW(m_Instance, Handle, NewScript) != 0;
      }

      public bool ChangeJavaScriptActionA(int Handle, String NewScript)
      {
         return pdfChangeJavaScriptActionA(m_Instance, Handle, NewScript) != 0;
      }

      public bool ChangeJavaScriptActionW(int Handle, String NewScript)
      {
         return pdfChangeJavaScriptActionW(m_Instance, Handle, NewScript) != 0;
      }

      public bool ChangeJavaScriptName(int Handle, String Name)
      {
         return pdfChangeJavaScriptNameW(m_Instance, Handle, Name) != 0;
      }

      public bool ChangeJavaScriptNameA(int Handle, String Name)
      {
         return pdfChangeJavaScriptNameA(m_Instance, Handle, Name) != 0;
      }

      public bool ChangeJavaScriptNameW(int Handle, String Name)
      {
         return pdfChangeJavaScriptNameW(m_Instance, Handle, Name) != 0;
      }

      public bool ChangeLinkAnnot(int Handle, String URL)
      {
         return pdfChangeLinkAnnot(m_Instance, Handle, URL) != 0;
      }

      public bool ChangeNamedDest(TObjType ObjType, int Handle, String NewDestName)
      {
         return pdfChangeNamedDestW(m_Instance, ObjType, Handle, NewDestName) != 0;
      }

      public bool ChangeOCGName(int Handle, String Value)
      {
         return pdfChangeOCGNameW(m_Instance, Handle, Value) != 0;
      }

      public bool ChangeSeparationColor(int CSHandle, uint NewColor, TExtColorSpace Alternate, int AltHandle)
      {
         return pdfChangeSeparationColor(m_Instance, CSHandle, NewColor, Alternate, AltHandle) != 0;
      }

      public int CheckAndRepairFonts()
      {
         return pdfCheckAndRepairFonts(m_Instance);
      }

      public bool CheckCollection()
      {
         return pdfCheckCollection(m_Instance) != 0;
      }

      public int CheckConformance(TConformanceType Type, TCheckOptions Options, IntPtr UserData, TOnFontNotFoundProc OnFontNotFound, TOnReplaceICCProfile OnReplaceICCProfile)
      {
         m_FontNotFoundProc    = OnFontNotFound;
         m_OnReplaceICCProfile = OnReplaceICCProfile;
         return pdfCheckConformance(m_Instance, Type, Options, UserData, m_FontNotFoundProc, m_OnReplaceICCProfile);
      }

      public int CheckFieldNames()
      {
         return pdfCheckFieldNames(m_Instance);
      }

      public int CircleAnnot(double PosX, double PosY, double Width, double Height, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Comment)
      {
         return pdfCircleAnnotW(m_Instance, PosX, PosY, Width, Height, LineWidth, FillColor, StrokeColor, CS, Author, Subject, Comment);
      }

      public int CircleAnnotA(double PosX, double PosY, double Width, double Height, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Comment)
      {
         return pdfCircleAnnotA(m_Instance, PosX, PosY, Width, Height, LineWidth, FillColor, StrokeColor, CS, Author, Subject, Comment);
      }

      public int CircleAnnotW(double PosX, double PosY, double Width, double Height, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Comment)
      {
         return pdfCircleAnnotW(m_Instance, PosX, PosY, Width, Height, LineWidth, FillColor, StrokeColor, CS, Author, Subject, Comment);
      }

      public bool ClearAutoTemplates()
      {
         return pdfClearAutoTemplates(m_Instance) != 0;
      }

      public void ClearErrorLog()
      {
         pdfClearErrorLog(m_Instance);
      }

      public bool ClearHostFonts()
      {
         return pdfClearHostFonts(m_Instance) != 0;
      }

      public bool ClipPath(TClippingMode ClipMode, TPathFillMode FillMode)
      {
         return pdfClipPath(m_Instance, ClipMode, FillMode) != 0;
      }

      public bool CloseAndSignFile(String CertFile, String Password, String Reason, String Location)
      {
         return pdfCloseAndSignFile(m_Instance, CertFile, Password, Reason, Location) != 0;
      }

      public bool CloseAndSignFileEx(String OpenPwd, String OwnerPwd, TKeyLen KeyLen, TRestrictions Restrict, String CertFile, String Password, String Reason, String Location)
      {
         return pdfCloseAndSignFileEx(m_Instance, OpenPwd, OwnerPwd, KeyLen, Restrict, CertFile, Password, Reason, Location) != 0;
      }

      public bool CloseAndSignFileExt(ref TPDFSigParms SigParms)
      {
         TPDFSigParms_I p = new TPDFSigParms_I();
         p.StructSize   = Marshal.SizeOf(p);
         p.ContactInfoW = SigParms.ContactInfo;
         p.Encrypt      = Convert.ToInt32(SigParms.Encrypt);
         p.HashType     = SigParms.HashType;
         p.KeyLen       = SigParms.KeyLen;
         p.LocationW    = SigParms.Location;
         p.OpenPwd      = SigParms.OpenPwd;
         p.OwnerPwd     = SigParms.OwnerPwd;
         p.PKCS7ObjLen  = SigParms.PKCS7ObjLen;
         p.ReasonW      = SigParms.Reason;
         p.SignerW      = SigParms.Signer;
         p.Restrict     = SigParms.Restrict;
         if (pdfCloseAndSignFileExt(m_Instance, ref p) == 0) return false;
         SigParms.Range1 = new byte[p.Range1Len];
         Marshal.Copy(p.Range1, SigParms.Range1, 0, p.Range1Len);
         if (p.Range2Len > 0)
         {
            SigParms.Range2 = new byte[p.Range2Len];
            Marshal.Copy(p.Range2, SigParms.Range2, 0, p.Range2Len);
         }else
         {
            SigParms.Range2 = null;
         }
         return true;
      }

      public bool CloseFile()
      {
         return pdfCloseFile(m_Instance) != 0;
      }

      public bool CloseFileEx(String OpenPwd, String OwnerPwd, TKeyLen KeyLen, TRestrictions Restrict)
      {
         return pdfCloseFileEx(m_Instance, OpenPwd, OwnerPwd, KeyLen, Restrict) != 0;
      }

      public bool CloseImage()
      {
         return pdfCloseImage(m_Instance) != 0;
      }

      public bool CloseImportFile()
      {
         return pdfCloseImportFile(m_Instance) != 0;
      }

      public bool CloseImportFileEx(int Handle)
      {
         return pdfCloseImportFileEx(m_Instance, Handle) != 0;
      }

      public bool ClosePath(TPathFillMode FillMode)
      {
         return pdfClosePath(m_Instance, FillMode) != 0;
      }

      public bool CloseTag()
      {
         return pdfCloseTag(m_Instance) != 0;
      }

      public uint CMYK(byte c, byte m, byte y, byte k)
      {
         return (uint)(k|(y<<8)|(m<<16)|(c<<24));
      }

      public bool ComputeBBox(ref TPDFRect BBox, TCompBBoxFlags Flags)
      {
         return pdfComputeBBox(m_Instance, ref BBox, Flags) != 0;
      }

      public uint ConvColor(IntPtr Color, int NumComps, TExtColorSpace SourceCS, IntPtr IColorSpace, TExtColorSpace DestCS)
      {
         return pdfConvColor(Color, NumComps, SourceCS, IColorSpace, DestCS);
      }

      public bool ConvertColors(TColorConvFlags Flags)
      {
         return pdfConvertColors(m_Instance, Flags, null) != 0;
      }

      public int ConvertEMFSpool(String SpoolFile, double LeftMargin, double TopMargin, TSpoolConvFlags Flags)
      {
         return pdfConvertEMFSpool(m_Instance, SpoolFile, LeftMargin, TopMargin, Flags);
      }

      public bool ConvertColors(TColorConvFlags Flags, float Add)
      {
         float[] tmp = new float[1];
         tmp[0] = Add;
         return pdfConvertColors(m_Instance, Flags, tmp) != 0;
      }

      public String ConvToUnicode(String AString, TCodepage CP)
      {
         IntPtr retval = pdfConvToUnicode(m_Instance, AString, CP);
         return _Conv.ToString(retval, true);
      }

      public bool CopyChoiceValues(int Source, int Dest, bool Share)
      {
         return pdfCopyChoiceValues(m_Instance, Source, Dest, Convert.ToInt32(Share)) != 0;
      }

      public IntPtr[] CopyIntPtrArray(IntPtr Source, int Count)
      {
         IntPtr[] retval = new IntPtr[Count];
         pdfCopyMemIntPtr(Source, retval, Count * IntPtr.Size);
         return retval;
      }

      public int Create3DAnnot(double PosX, double PosY, double Width, double Height, String Author, String Name, String U3DFile, String Image)
      {
         return pdfCreate3DAnnotW(m_Instance, PosX, PosY, Width, Height, Author, Name, U3DFile, Image);
      }

      public bool Create3DBackground(IntPtr IView, uint BackColor)
      {
         return pdfCreate3DBackground(m_Instance, IView, BackColor) != 0;
      }

      public int Create3DGotoViewAction(int Base3DAnnot, IntPtr IView, T3DNamedAction Named)
      {
         return pdfCreate3DGotoViewAction(m_Instance, Base3DAnnot, IView, Named);
      }

      public bool Create3DProjection(IntPtr IView, T3DProjType ProjType, T3DScaleType ScaleType, double Diameter, double FOV)
      {
         return pdfCreate3DProjection(m_Instance, IView, ProjType, ScaleType, Diameter, FOV) != 0;
      }

      public IntPtr Create3DView(int Base3DAnnot, String Name, bool SetAsDefault, double[] Matrix, double CamDistance, T3DRenderingMode RM, T3DLightingSheme LS)
      {
         return pdfCreate3DView(m_Instance, Base3DAnnot, Name, Convert.ToInt32(SetAsDefault), Matrix, CamDistance, RM, LS);
      }

      public int CreateAltFontList()
      {
         return pdfCreateAltFontList(m_Instance);
      }

      public int CreateAnnotAP(int Annot)
      {
         return pdfCreateAnnotAP(m_Instance, Annot);
      }

      public int CreateArticleThread(String ThreadName)
      {
         return pdfCreateArticleThreadW(m_Instance, ThreadName);
      }

      public int CreateArticleThreadA(String ThreadName)
      {
         return pdfCreateArticleThreadA(m_Instance, ThreadName);
      }

      public int CreateArticleThreadW(String ThreadName)
      {
         return pdfCreateArticleThreadW(m_Instance, ThreadName);
      }

      public int CreateAxialShading(double sX, double sY, double eX, double eY, double SCenter, uint SColor, uint EColor, bool Extend1, bool Extend2)
      {
         return pdfCreateAxialShading(m_Instance, sX, sY, eX, eY, SCenter, SColor, EColor, Convert.ToInt32(Extend1), Convert.ToInt32(Extend2));
      }

      public int CreateBarcodeField(String Name, int Parent, double PosX, double PosY, double Width, double Height, ref TPDFBarcode Barcode)
      {
         int retval;
         TPDFBarcode_I bc = new TPDFBarcode_I();
         bc.StructSize   = Marshal.SizeOf(bc);
         bc.CaptionW     = Marshal.StringToHGlobalUni(Barcode.Caption);
         bc.ECC          = Barcode.ECC;
         bc.Height       = Barcode.Height;
         bc.nCodeWordCol = Barcode.nCodeWordCol;
         bc.nCodeWordRow = Barcode.nCodeWordRow;
         bc.Resolution   = Barcode.Resolution;
         bc.Symbology    = Marshal.StringToHGlobalAnsi(Barcode.Symbology);
         bc.Version      = Barcode.Version;
         bc.Width        = Barcode.Width;
         bc.XSymHeight   = Barcode.XSymHeight;
         bc.XSymWidth    = Barcode.XSymWidth;

         retval = pdfCreateBarcodeField(m_Instance, Name, Parent, PosX, PosY, Width, Height, ref bc);
         Marshal.FreeHGlobal(bc.CaptionW);
         Marshal.FreeHGlobal(bc.Symbology);
         return retval;
      }

      public int CreateButton(String Name, String Caption, int Parent, double PosX, double PosY, double Width, double Height)
      {
         return pdfCreateButtonW(m_Instance, Name, Caption, Parent, PosX, PosY, Width, Height);
      }

      public int CreateButtonA(String Name, String Caption, int Parent, double PosX, double PosY, double Width, double Height)
      {
         return pdfCreateButtonA(m_Instance, Name, Caption, Parent, PosX, PosY, Width, Height);
      }

      public int CreateButtonW(String Name, String Caption, int Parent, double PosX, double PosY, double Width, double Height)
      {
         return pdfCreateButtonW(m_Instance, Name, Caption, Parent, PosX, PosY, Width, Height);
      }

      public int CreateCheckBox(String Name, String ExpValue, bool Checked, int Parent, double PosX, double PosY, double Width, double Height)
      {
         return pdfCreateCheckBox(m_Instance, Name, ExpValue, Convert.ToInt32(Checked), Parent, PosX, PosY, Width, Height);
      }

      public int CreateCIEColorSpace(TExtColorSpace Base, float[] WhitePoint, float[] BlackPoint, float[] Gamma, float[] Matrix)
      {
         return pdfCreateCIEColorSpace(m_Instance, Base, WhitePoint, BlackPoint, Gamma, Matrix);
      }

      public bool CreateCollection(TColView View)
      {
         return pdfCreateCollection(m_Instance, View) != 0;
      }

      public int CreateCollectionField(TColColumnType ColType, int Column, String Name, String Key, bool Visible, bool Editable)
      {
         return pdfCreateCollectionFieldW(m_Instance, ColType, Column, Name, Key, Convert.ToInt32(Visible), Convert.ToInt32(Editable));
      }

      public int CreateCollectionFieldA(TColColumnType ColType, int Column, String Name, String Key, bool Visible, bool Editable)
      {
         return pdfCreateCollectionFieldA(m_Instance, ColType, Column, Name, Key, Convert.ToInt32(Visible), Convert.ToInt32(Editable));
      }

      public int CreateCollectionFieldW(TColColumnType ColType, int Column, String Name, String Key, bool Visible, bool Editable)
      {
         return pdfCreateCollectionFieldW(m_Instance, ColType, Column, Name, Key, Convert.ToInt32(Visible), Convert.ToInt32(Editable));
      }

      public bool CreateColItemDate(int EmbFile, String Key, long Date, String Prefix)
      {
         return pdfCreateColItemDate(m_Instance, EmbFile, Key, Date, Prefix) != 0;
      }

      public bool CreateColItemNumber(int EmbFile, String Key, double Value, String Prefix)
      {
         return pdfCreateColItemNumber(m_Instance, EmbFile, Key, Value, Prefix) != 0;
      }

      public bool CreateColItemString(int EmbFile, String Key, String Value, String Prefix)
      {
         return pdfCreateColItemStringW(m_Instance, EmbFile, Key, Value, Prefix) != 0;
      }

      public bool CreateColItemStringA(int EmbFile, String Key, String Value, String Prefix)
      {
         return pdfCreateColItemStringA(m_Instance, EmbFile, Key, Value, Prefix) != 0;
      }

      public bool CreateColItemStringW(int EmbFile, String Key, String Value, String Prefix)
      {
         return pdfCreateColItemStringW(m_Instance, EmbFile, Key, Value, Prefix) != 0;
      }

      public int CreateComboBox(String Name, bool Sort, int Parent, double PosX, double PosY, double Width, double Height)
      {
         return pdfCreateComboBox(m_Instance, Name, Convert.ToInt32(Sort), Parent, PosX, PosY, Width, Height);
      }

      public int CreateDeviceNColorSpace(String[] Colorants, String PostScriptFunc, TExtColorSpace Alternate, int Handle)
      {
         return pdfCreateDeviceNColorSpace(m_Instance, Colorants, Colorants.Length, PostScriptFunc, Alternate, Handle);
      }

      public int CreateDeviceNColorSpace(String[] Colorants, int NumColorants, String PostScriptFunc, TExtColorSpace Alternate, int Handle)
      {
         return pdfCreateDeviceNColorSpace(m_Instance, Colorants, NumColorants, PostScriptFunc, Alternate, Handle);
      }

      public bool CreateDPartRoot(String[]NodeNameList, int RecordLevel)
      {
         return pdfCreateDPartRoot(m_Instance, NodeNameList, NodeNameList.Length, RecordLevel) != 0;
      }

      public int CreateExtGState(ref TPDFExtGState GS)
      {
         return pdfCreateExtGState(m_Instance, ref GS);
      }

      public int CreateGeospatialMeasure(int Viewport, String Attributes)
      {
         return pdfCreateGeospatialMeasure(m_Instance, Viewport, Attributes);
      }

      public int CreateGoToAction(TDestType DestType, int PageNum, double a, double b, double c, double d)
      {
         return pdfCreateGoToAction(m_Instance, DestType, PageNum, a, b, c, d);
      }

      public int CreateGoToActionEx(int NamedDest)
      {
         return pdfCreateGoToActionEx(m_Instance, NamedDest);
      }

      public int CreateGoToEAction(TEmbFileLocation Location, String Source, int SrcPage, String Target, String DestName, int DestPage, bool NewWindow)
      {
         return pdfCreateGoToEActionW(m_Instance, Location, Source, SrcPage, Target, DestName, DestPage, Convert.ToInt32(NewWindow));
      }

      public int CreateGoToEActionA(TEmbFileLocation Location, String Source, int SrcPage, String Target, String DestName, int DestPage, bool NewWindow)
      {
         return pdfCreateGoToEActionA(m_Instance, Location, Source, SrcPage, Target, DestName, DestPage, Convert.ToInt32(NewWindow));
      }

      public int CreateGoToEActionW(TEmbFileLocation Location, String Source, int SrcPage, String Target, String DestName, int DestPage, bool NewWindow)
      {
         return pdfCreateGoToEActionW(m_Instance, Location, Source, SrcPage, Target, DestName, DestPage, Convert.ToInt32(NewWindow));
      }

      public int CreateGoToRAction(String FileName, int PageNum)
      {
         return pdfCreateGoToRActionW(m_Instance, FileName, PageNum);
      }

      public int CreateGoToRActionA(String FileName, int PageNum)
      {
         return pdfCreateGoToRActionA(m_Instance, FileName, PageNum);
      }

      public int CreateGoToRActionW(String FileName, int PageNum)
      {
         return pdfCreateGoToRActionW(m_Instance, FileName, PageNum);
      }

      public int CreateGoToRActionEx(String FileName, String DestName, bool NewWindow)
      {
         return pdfCreateGoToRActionExW(m_Instance, FileName, DestName, Convert.ToInt32(NewWindow));
      }

      public int CreateGoToRActionExA(String FileName, String DestName, bool NewWindow)
      {
         return pdfCreateGoToRActionExA(m_Instance, FileName, DestName, Convert.ToInt32(NewWindow));
      }

      public int CreateGoToRActionExW(String FileName, String DestName, bool NewWindow)
      {
         return pdfCreateGoToRActionExW(m_Instance, FileName, DestName, Convert.ToInt32(NewWindow));
      }

      public int CreateGoToRActionExU(String FileName, String DestName, bool NewWindow)
      {
         return pdfCreateGoToRActionExUW(m_Instance, FileName, DestName, Convert.ToInt32(NewWindow));
      }

      public int CreateGoToRActionExUA(String FileName, String DestName, bool NewWindow)
      {
         return pdfCreateGoToRActionExUA(m_Instance, FileName, DestName, Convert.ToInt32(NewWindow));
      }

      public int CreateGoToRActionExUW(String FileName, String DestName, bool NewWindow)
      {
         return pdfCreateGoToRActionExUW(m_Instance, FileName, DestName, Convert.ToInt32(NewWindow));
      }

      public int CreateGroupField(String Name, int Parent)
      {
         return pdfCreateGroupField(m_Instance, Name, Parent);
      }

      public int CreateHideAction(int Field, bool Hide)
      {
         return pdfCreateHideAction(m_Instance, Field, Convert.ToInt32(Hide));
      }

      public int CreateICCBasedColorSpace(String ICCProfile)
      {
         return pdfCreateICCBasedColorSpaceW(m_Instance, ICCProfile);
      }

      public int CreateICCBasedColorSpaceA(String ICCProfile)
      {
         return pdfCreateICCBasedColorSpaceA(m_Instance, ICCProfile);
      }

      public int CreateICCBasedColorSpaceW(String ICCProfile)
      {
         return pdfCreateICCBasedColorSpaceW(m_Instance, ICCProfile);
      }

      public int CreateICCBasedColorSpaceEx(byte[] Buffer)
      {
         return pdfCreateICCBasedColorSpaceEx(m_Instance, Buffer, Buffer.Length);
      }

      public bool CreateImage(String FileName, TImageFormat Format)
      {
         return pdfCreateImageW(m_Instance, FileName, Format) != 0;
      }

      public bool CreateImageA(String FileName, TImageFormat Format)
      {
         return pdfCreateImageA(m_Instance, FileName, Format) != 0;
      }

      public bool CreateImageW(String FileName, TImageFormat Format)
      {
         return pdfCreateImageW(m_Instance, FileName, Format) != 0;
      }

      public int CreateImpDataAction(String DataFile)
      {
         return pdfCreateImpDataActionW(m_Instance, DataFile);
      }

      public int CreateImpDataActionA(String DataFile)
      {
         return pdfCreateImpDataActionA(m_Instance, DataFile);
      }

      public int CreateImpDataActionW(String DataFile)
      {
         return pdfCreateImpDataActionW(m_Instance, DataFile);
      }

      public int CreateIndexedColorSpace(TExtColorSpace Base, int Handle, byte[] ColorTable, int NumColors)
      {
         return pdfCreateIndexedColorSpace(m_Instance, Base, Handle, ColorTable, NumColors);
      }

      public int CreateJSAction(String Script)
      {
         return pdfCreateJSActionW(m_Instance, Script);
      }

      public int CreateJSActionA(String Script)
      {
         return pdfCreateJSActionA(m_Instance, Script);
      }

      public int CreateJSActionW(String Script)
      {
         return pdfCreateJSActionW(m_Instance, Script);
      }

      public int CreateLaunchAction(TFileOP OP, String FileName, String DefDir, String Param, bool NewWindow)
      {
         return pdfCreateLaunchAction(m_Instance, OP, FileName, DefDir, Param, Convert.ToInt32(NewWindow));
      }

      public int CreateLaunchActionEx(String FileName, bool NewWindow)
      {
         return pdfCreateLaunchActionExW(m_Instance, FileName, Convert.ToInt32(NewWindow));
      }

      public int CreateLaunchActionExA(String FileName, bool NewWindow)
      {
         return pdfCreateLaunchActionExA(m_Instance, FileName, Convert.ToInt32(NewWindow));
      }

      public int CreateLaunchActionExW(String FileName, bool NewWindow)
      {
         return pdfCreateLaunchActionExW(m_Instance, FileName, Convert.ToInt32(NewWindow));
      }

      public int CreateListBox(String Name, bool Sort, int Parent, double PosX, double PosY, double Width, double Height)
      {
         return pdfCreateListBox(m_Instance, Name, Convert.ToInt32(Sort), Parent, PosX, PosY, Width, Height);
      }

      public int CreateNamedAction(TNamedAction Action)
      {
         return pdfCreateNamedAction(m_Instance, Action);
      }

      public int CreateNamedDest(String Name, int DestPage, TDestType DestType, double a, double b, double c, double d)
      {
         return pdfCreateNamedDestW(m_Instance, Name, DestPage, DestType, a, b, c, d);
      }

      public int CreateNamedDestA(String Name, int DestPage, TDestType DestType, double a, double b, double c, double d)
      {
         return pdfCreateNamedDestA(m_Instance, Name, DestPage, DestType, a, b, c, d);
      }

      public int CreateNamedDestW(String Name, int DestPage, TDestType DestType, double a, double b, double c, double d)
      {
         return pdfCreateNamedDestW(m_Instance, Name, DestPage, DestType, a, b, c, d);
      }

      public bool CreateNewPDF(String OutPDF)
      {
         return pdfCreateNewPDF(m_Instance, OutPDF) != 0;
      }

      public int CreateOCG(String Name, bool DisplayInUI, bool Visible, TOCGIntent Intent)
      {
         return pdfCreateOCGW(m_Instance, Name, Convert.ToInt32(DisplayInUI), Convert.ToInt32(Visible), Intent);
      }

      public int CreateOCGA(String Name, bool DisplayInUI, bool Visible, TOCGIntent Intent)
      {
         return pdfCreateOCGA(m_Instance, Name, Convert.ToInt32(DisplayInUI), Convert.ToInt32(Visible), Intent);
      }

      public int CreateOCGW(String Name, bool DisplayInUI, bool Visible, TOCGIntent Intent)
      {
         return pdfCreateOCGW(m_Instance, Name, Convert.ToInt32(DisplayInUI), Convert.ToInt32(Visible), Intent);
      }

      public int CreateOCMD(TOCVisibility Visibility, int[] OCGs)
      {
         return pdfCreateOCMD(m_Instance, Visibility, OCGs, OCGs.Length);
      }

      public int CreateRadialShading(double sX, double sY, double R1, double eX, double eY, double R2, double SCenter, uint SColor, uint EColor, bool Extend1, bool Extend2)
      {
         return pdfCreateRadialShading(m_Instance, sX, sY, R1, eX, eY, R2, SCenter, SColor, EColor, Convert.ToInt32(Extend1), Convert.ToInt32(Extend2));
      }

      public int CreateRadioButton(String Name, String ExpValue, bool Checked, int Parent, double PosX, double PosY, double Width, double Height)
      {
         return pdfCreateRadioButton(m_Instance, Name, ExpValue, Convert.ToInt32(Checked), Parent, PosX, PosY, Width, Height);
      }

      public IntPtr CreateRasterizer(IntPtr Rows, IntPtr Buffer, int Width, int Height, int ScanlineLen, TPDFPixFormat PixFmt)
      {
         return rasCreateRasterizer(m_Instance, Rows, Buffer, Width, Height, ScanlineLen, PixFmt);
      }

      public IntPtr CreateRasterizerEx(IntPtr DC, int Width, int Height, TPDFPixFormat PixFmt)
      {
         return rasCreateRasterizerEx(m_Instance, DC, Width, Height, PixFmt);
      }

      public int CreateRectilinearMeasure(int Viewport, double Scale, String UnitDescription, String LabelDistance, String LabelArea)
      {
         return pdfCreateRectilinearMeasureW(m_Instance, Viewport, Scale, UnitDescription, LabelDistance, LabelArea);
      }

      public int CreateResetAction()
      {
         return pdfCreateResetAction(m_Instance);
      }

      public int CreateSeparationCS(String Colorant, TExtColorSpace Alternate, int Handle, uint Color)
      {
         return pdfCreateSeparationCS(m_Instance, Colorant, Alternate, Handle, Color);
      }

      public int CreateSetOCGStateAction(int[] On, int[] Off, int[] Toggle, bool PreserveRB)
      {
         int numOn = 0, numOff = 0, numToggle = 0;
         if (On     != null) numOn     = On.Length;
         if (Off    != null) numOff    = Off.Length;
         if (Toggle != null) numToggle = Toggle.Length;
         return pdfCreateSetOCGStateAction(m_Instance, On, numOn, Off, numOff, Toggle, numToggle, Convert.ToInt32(PreserveRB));
      }

      public int CreateSigField(String Name, int Parent, double PosX, double PosY, double Width, double Height)
      {
         return pdfCreateSigField(m_Instance, Name, Parent, PosX, PosY, Width, Height);
      }

      public int CreateSigFieldAP(int SigField)
      {
         return pdfCreateSigFieldAP(m_Instance, SigField);
      }

      public IntPtr CreateSoftMask(int TranspGroup, TSoftMaskType MaskType, uint BackColor)
      {
         return pdfCreateSoftMask(m_Instance, TranspGroup, MaskType, BackColor);
      }

      public int CreateStdPattern(TStdPattern Pattern, double LineWidth, double Distance, uint LineColor, uint BackColor)
      {
         return pdfCreateStdPattern(m_Instance, Pattern, LineWidth, Distance, LineColor, BackColor);
      }

      public bool CreateStructureTree()
      {
         return pdfCreateStructureTreeEx(m_Instance, TPDFBaseTag.btTOCEntry /*Dummy value, will be ignored */) != 0;
      }

      public bool CreateStructureTreeEx(TPDFBaseTag RootTag)
      {
         return pdfCreateStructureTreeEx(m_Instance, RootTag) != 0;
      }

      public int CreateSubmitAction(TSubmitFlags Flags, String URL)
      {
         return pdfCreateSubmitAction(m_Instance, Flags, URL);
      }

      public int CreateTextField(String Name, int Parent, bool Multiline, int MaxLen, double PosX, double PosY, double Width, double Height)
      {
         return pdfCreateTextField(m_Instance, Name, Parent, Convert.ToInt32(Multiline), MaxLen, PosX, PosY, Width, Height);
      }

      public int CreateURIAction(String URL)
      {
         return pdfCreateURIAction(m_Instance, URL);
      }

      public int CreateViewport(String Name, double X1, double Y1, double X2, double Y2)
      {
         return pdfCreateViewportW(m_Instance, Name, X1, Y1, X2, Y2);
      }

      public int CreateXFAStream(String Name, byte[] Buffer)
      {
         return pdfCreateXFAStreamA(m_Instance, Name, Buffer, Buffer.Length);
      }

      public int DecryptPDF(String FileName, TPwdType PwdType, String Password)
      {
         return pdfDecryptPDFW(m_Instance, FileName, PwdType, Password);
      }

      public int DecryptPDFA(String FileName, TPwdType PwdType, String Password)
      {
         return pdfDecryptPDFA(m_Instance, FileName, PwdType, Password);
      }

      public int DecryptPDFW(String FileName, TPwdType PwdType, String Password)
      {
         return pdfDecryptPDFW(m_Instance, FileName, PwdType, Password);
      }

      public void DeleteAcroForm()
      {
         pdfDeleteAcroForm(m_Instance);
      }

      public bool DeleteActionFromObj(TObjType ObjType, int ActHandle, int ObjHandle)
      {
         return pdfDeleteActionFromObj(m_Instance, ObjType, ActHandle, ObjHandle) != 0;
      }

      public bool DeleteActionFromObjEx(TObjType ObjType, int ObjHandle, int ActIndex)
      {
         return pdfDeleteActionFromObjEx(m_Instance, ObjType, ObjHandle, ActIndex) != 0;
      }

      public bool DeleteAltFontList(int Handle)
      {
         return pdfDeleteAltFontList(m_Instance, Handle) != 0;
      }

      public bool DeleteAnnotation(int Handle)
      {
         return pdfDeleteAnnotation(m_Instance, Handle) != 0;
      }

      public bool DeleteAnnotationFromPage(int PageNum, int Handle)
      {
         return pdfDeleteAnnotationFromPage(m_Instance, PageNum, Handle) != 0;
      }

      public int DeleteAppEvents(bool ApplyEvent, TOCAppEvent Event)
      {
         return pdfDeleteAppEvents(m_Instance, Convert.ToInt32(ApplyEvent), Event);
      }

      public int DeleteBookmark(int ABmk)
      {
         return pdfDeleteBookmark(m_Instance, ABmk);
      }

      public bool DeleteDPartNode(int Handle)
      {
         return pdfDeleteDPartNode(m_Instance, Handle) != 0;
      }

      public bool DeleteEmbeddedFile(int Handle)
      {
         return pdfDeleteEmbeddedFile(m_Instance, Handle) != 0;
      }

      public bool DeleteField(int Field)
      {
         return pdfDeleteField(m_Instance, Field) != 0;
      }

      public bool DeleteFieldEx(String Name)
      {
         return pdfDeleteFieldEx(m_Instance, Name) != 0;
      }

      public int DeletePage(int PageNum)
      {
         return pdfDeletePage(m_Instance, PageNum);
      }

      public void DeleteJavaScripts(bool DelJavaScriptActions)
      {
         pdfDeleteJavaScripts(m_Instance, Convert.ToInt32(DelJavaScriptActions));
      }

      public bool DeleteNamedDest(String Name)
      {
         return pdfDeleteNamedDestW(m_Instance, Name) != 0;
      }

      public bool DeleteNamedDestByIndex(int Index)
      {
         return pdfDeleteNamedDestByIndex(m_Instance, Index) != 0;
      }

      public bool DeleteOCGFromAppEvent(int Handle, TOCAppEvent Events, TOCGUsageCategory Categories, bool DelCategoryOnly)
      {
         return pdfDeleteOCGFromAppEvent(m_Instance, Handle, Events, Categories, Convert.ToInt32(DelCategoryOnly)) != 0;
      }

      public bool DeleteOCGFromDisplayTree(int OCGHandle, bool Recursive)
      {
         return pdfDeleteOCGFromDisplayTree(m_Instance, OCGHandle, Convert.ToInt32(Recursive)) != 0;
      }

      public bool DeleteOCUINode(IntPtr Node)
      {
         return pdfDeleteOCUINode(m_Instance, Node) != 0;
      }

      public int DeleteOutputIntent(int Index)
      {
         return pdfDeleteOutputIntent(m_Instance, Index);
      }

      public void DeletePageLabels()
      {
         pdfDeletePageLabels(m_Instance);
      }

      public void DeleteRasterizer(ref IntPtr RasPtr)
      {
         rasDeleteRasterizer(ref RasPtr);
      }

      public bool DeleteSeparationInfo(bool AllPages)
      {
         return pdfDeleteSeparationInfo(m_Instance, Convert.ToInt32(AllPages)) != 0;
      }

      public bool DeleteTemplate(int Handle)
      {
         return pdfDeleteTemplate(m_Instance, Handle) != 0;
      }

      public bool DeleteTemplateEx(int Index)
      {
         return pdfDeleteTemplateEx(m_Instance, Index) != 0;
      }

      public int DeleteWatermark(int PageNum, bool InclAnnots)
      {
         return pdfDeleteWatermark(m_Instance, PageNum, Convert.ToInt32(InclAnnots));
      }

      public void DeleteXFAForm()
      {
         pdfDeleteXFAForm(m_Instance);
      }

      public bool DrawArc(double PosX, double PosY, double Radius, double StartAngle, double EndAngle)
      {
         return pdfDrawArc(m_Instance, PosX, PosY, Radius, StartAngle, EndAngle) != 0;
      }

      public bool DrawArcEx(double PosX, double PosY, double Width, double Height, double StartAngle, double EndAngle)
      {
         return pdfDrawArcEx(m_Instance, PosX, PosY, Width, Height, StartAngle, EndAngle) != 0;
      }

      public bool DrawChord(double PosX, double PosY, double Width, double Height, double StartAngle, double EndAngle, TPathFillMode FillMode)
      {
         return pdfDrawChord(m_Instance, PosX, PosY, Width, Height, StartAngle, EndAngle, FillMode) != 0;
      }

      public bool DrawCircle(double PosX, double PosY, double Radius, TPathFillMode FillMode)
      {
         return pdfDrawCircle(m_Instance, PosX, PosY, Radius, FillMode) != 0;
      }

      public bool DrawNGon(double PosX, double PosY, double Radius, double Alpha, int NumSides, TPathFillMode FillMode)
      {
         return pdfDrawNGon(m_Instance, PosX, PosY, Radius, Alpha, NumSides, FillMode) != 0;
      }

      public bool DrawPie(double PosX, double PosY, double Width, double Height, double StartAngle, double EndAngle, TPathFillMode FillMode)
      {
         return pdfDrawPie(m_Instance, PosX, PosY, Width, Height, StartAngle, EndAngle, FillMode) != 0;
      }

      public bool EditPage(int PageNum)
      {
         return pdfEditPage(m_Instance, PageNum) != 0;
      }

      public bool EditTemplate(int Index)
      {
         return pdfEditTemplate(m_Instance, Index) != 0;
      }

      public bool EditTemplate2(int Handle)
      {
         return pdfEditTemplate2(m_Instance, Handle) != 0;
      }

      public bool Ellipse(double PosX, double PosY, double Width, double Height, TPathFillMode FillMode)
      {
         return pdfEllipse(m_Instance, PosX, PosY, Width, Height, FillMode) != 0;
      }

      public int EncryptPDF(String FileName, String OpenPwd, String OwnerPwd, TKeyLen KeyLen, TRestrictions Restrict)
      {
         return pdfEncryptPDFW(m_Instance, FileName, OpenPwd, OwnerPwd, KeyLen, Restrict);
      }

      public int EncryptPDFA(String FileName, String OpenPwd, String OwnerPwd, TKeyLen KeyLen, TRestrictions Restrict)
      {
         return pdfEncryptPDFA(m_Instance, FileName, OpenPwd, OwnerPwd, KeyLen, Restrict);
      }

      public int EncryptPDFW(String FileName, String OpenPwd, String OwnerPwd, TKeyLen KeyLen, TRestrictions Restrict)
      {
         return pdfEncryptPDFW(m_Instance, FileName, OpenPwd, OwnerPwd, KeyLen, Restrict);
      }

      public bool EndContinueText()
      {
         return pdfEndContinueText(m_Instance) != 0;
      }

      public bool EndLayer()
      {
         return pdfEndLayer(m_Instance) != 0;
      }

      public bool EndPage()
      {
         return pdfEndPage(m_Instance) != 0;
      }

      public bool EndPattern()
      {
         return pdfEndPattern(m_Instance) != 0;
      }

      public bool EndTemplate()
      {
         return pdfEndTemplate(m_Instance) != 0;
      }

      public int EnumDocFonts(IntPtr Data, TEnumFontProc2 EnumProc)
      {
         m_EnumFontProc2 = EnumProc;
         return pdfEnumDocFonts(m_Instance, Data, m_EnumFontProc2);
      }

      public int EnumHostFonts(IntPtr Data, TEnumFontProc EnumProc)
      {
         m_EnumFontProc = EnumProc;
         return pdfEnumHostFonts(m_Instance, Data, m_EnumFontProc);
      }

      public int EnumHostFontsEx(IntPtr Data, TEnumFontProcEx EnumProc)
      {
         m_EnumFontProcEx = EnumProc;
         return pdfEnumHostFontsEx(m_Instance, Data, m_EnumFontProcEx);
      }

      public bool ExchangeBookmarks(int Bmk1, int Bmk2)
      {
         return pdfExchangeBookmarks(m_Instance, Bmk1, Bmk2) != 0;
      }

      public bool ExchangePages(int First, int Second)
      {
         return pdfExchangePages(m_Instance, First, Second) != 0;
      }

      public bool ExtractText(int PageNum, TTextExtractionFlags Flags, ref String Text)
      {
         int txtLen = 0;
         IntPtr txt = IntPtr.Zero;
         if (pdfExtractText(m_Instance, PageNum, Flags, IntPtr.Zero, ref txt, ref txtLen) != 0)
         {
            Text = Marshal.PtrToStringUni(txt, txtLen);
            return true;
         }else
         {
            Text = null;
            return false;
         }
      }

      public bool ExtractText(int PageNum, TTextExtractionFlags Flags, ref TFltRect Area, ref String Text)
      {
         int txtLen = 0;
         IntPtr txt = IntPtr.Zero;
         if (pdfExtractText2(m_Instance, PageNum, Flags, ref Area, ref txt, ref txtLen) != 0)
         {
            Text = Marshal.PtrToStringUni(txt, txtLen);
            return true;
         }else
         {
            Text = null;
            return false;
         }
      }

      public int FileAttachAnnot(double PosX, double PosY, TFileAttachIcon Icon, String Author, String Desc, String AFile, bool Compress)
      {
         return pdfFileAttachAnnotW(m_Instance, PosX, PosY, Icon, Author, Desc, AFile, Convert.ToInt32(Compress));
      }

      public int FileAttachAnnotA(double PosX, double PosY, TFileAttachIcon Icon, String Author, String Desc, String AFile, bool Compress)
      {
         return pdfFileAttachAnnotA(m_Instance, PosX, PosY, Icon, Author, Desc, AFile, Convert.ToInt32(Compress));
      }

      public int FileAttachAnnotW(double PosX, double PosY, TFileAttachIcon Icon, String Author, String Desc, String AFile, bool Compress)
      {
         return pdfFileAttachAnnotW(m_Instance, PosX, PosY, Icon, Author, Desc, AFile, Convert.ToInt32(Compress));
      }

      public int FileAttachAnnotEx(double PosX, double PosY, TFileAttachIcon Icon, String FileName, String Author, String Desc, byte[] Buffer, bool Compress)
      {
         return pdfFileAttachAnnotEx(m_Instance, PosX, PosY, Icon, FileName, Author, Desc, Buffer, Buffer.Length, Convert.ToInt32(Compress));
      }

      public int FileLink(double PosX, double PosY, double Width, double Height, String AFilePath)
      {
         return pdfFileLinkW(m_Instance, PosX, PosY, Width, Height, AFilePath);
      }

      public int FileLinkA(double PosX, double PosY, double Width, double Height, String AFilePath)
      {
         return pdfFileLinkA(m_Instance, PosX, PosY, Width, Height, AFilePath);
      }

      public int FileLinkW(double PosX, double PosY, double Width, double Height, String AFilePath)
      {
         return pdfFileLinkW(m_Instance, PosX, PosY, Width, Height, AFilePath);
      }

      public int FindBookmark(int DestPage, String Title)
      {
         return pdfFindBookmarkW(m_Instance, DestPage, Title);
      }

      public int FindBookmarkA(int DestPage, String Title)
      {
         return pdfFindBookmarkA(m_Instance, DestPage, Title);
      }

      public int FindBookmarkW(int DestPage, String Title)
      {
         return pdfFindBookmarkW(m_Instance, DestPage, Title);
      }

      public int FindEmbeddedFile(String Name)
      {
         return pdfFindEmbeddedFile(m_Instance, Name);
      }

      public int FindField(String Name)
      {
         return pdfFindFieldW(m_Instance, Name);
      }

      public int FindFieldA(String Name)
      {
         return pdfFindFieldA(m_Instance, Name);
      }

      public int FindFieldW(String Name)
      {
         return pdfFindFieldW(m_Instance, Name);
      }

      public bool FinishSignature(byte[] PKCS7Obj)
      {
         return pdfFinishSignature(m_Instance, PKCS7Obj, PKCS7Obj.Length) != 0;
      }

      public int FindLinkAnnot(String URL)
      {
         return pdfFindLinkAnnot(m_Instance, URL);
      }

      public int FindNextBookmark()
      {
         return pdfFindNextBookmark(m_Instance);
      }

      public bool FlattenAnnotOrField(int Index, TAnnotFlattenFlags Flags)
      {
         return pdfFlattenAnnotOrField(m_Instance, Index, Flags) != 0;
      }

      public int FlattenAnnots(TAnnotFlattenFlags Flags)
      {
         return pdfFlattenAnnots(m_Instance, Flags);
      }

      public bool FlattenForm()
      {
         return pdfFlattenForm(m_Instance) != 0;
      }

      public bool FlushPageContent(ref TPDFStack Stack)
      {
         return pdfFlushPageContent(m_Instance, ref Stack) != 0;
      }

      public bool FlushPages(TFlushPageFlags Flags)
      {
         return pdfFlushPages(m_Instance, Flags) != 0;
      }

      public bool FlushPagesEx(TFlushPageFlags Flags, int LastPage)
      {
         return pdfFlushPagesEx(m_Instance, Flags, LastPage) != 0;
      }

      public void FreeImageBuffer()
      {
         pdfFreeImageBuffer(m_Instance);
      }

      public bool FreeImageObj(int Handle)
      {
         return pdfFreeImageObj(m_Instance, Handle) != 0;
      }

      public bool FreeImageObjEx(IntPtr ImagePtr)
      {
         return pdfFreeImageObjEx(m_Instance, ImagePtr) != 0;
      }

      public bool FreePDF()
      {
         return pdfFreePDF(m_Instance) != 0;
      }

      public int FreeTextAnnot(double PosX, double PosY, double Width, double Height, String Author, String AText, TTextAlign Align)
      {
         return pdfFreeTextAnnotW(m_Instance, PosX, PosY, Width, Height, Author, AText, Align);
      }

      public int FreeTextAnnotA(double PosX, double PosY, double Width, double Height, String Author, String AText, TTextAlign Align)
      {
         return pdfFreeTextAnnotA(m_Instance, PosX, PosY, Width, Height, Author, AText, Align);
      }

      public int FreeTextAnnotW(double PosX, double PosY, double Width, double Height, String Author, String AText, TTextAlign Align)
      {
         return pdfFreeTextAnnotW(m_Instance, PosX, PosY, Width, Height, Author, AText, Align);
      }

      public bool FreeUniBuf()
      {
         return pdfFreeUniBuf(m_Instance) != 0;
      }

      public bool Get3DAnnotStream(int Annot, ref byte[] Data, ref String SubType)
      {
         int size = 0;
         IntPtr pdata = IntPtr.Zero;
         SubType = null;
         Data    = null;
         if (pdfGet3DAnnotStream(m_Instance, Annot, ref pdata, ref size, ref SubType) != 0)
         {
            if (size > 0)
            {
               Data = new byte[size];
               Marshal.Copy(pdata, Data, 0, size);
            }
            return true;
         }
         return false;
      }

      public int GetActionCount()
      {
         return pdfGetActionCount(m_Instance);
      }

      public int GetActionHandle(TObjType ObjType, int ObjHandle, int ActIndex)
      {
         return pdfGetActionHandle(m_Instance, ObjType, ObjHandle, ActIndex);
      }

      public int GetActionType(int ActHandle)
      {
         return pdfGetActionType(m_Instance, ActHandle);
      }

      public int GetActionTypeEx(TObjType ObjType, int ObjHandle, int ActIndex)
      {
         return pdfGetActionTypeEx(m_Instance, ObjType, ObjHandle, ActIndex);
      }

      public int GetActiveFont()
      {
         return pdfGetActiveFont(m_Instance);
      }

      public int GetAllocBy()
      {
         return pdfGetAllocBy(m_Instance);
      }

      public bool GetAnnot(int Handle, ref TPDFAnnotation Annot)
      {
         TPDFAnnotation_I f = new TPDFAnnotation_I();
         if (pdfGetAnnot(m_Instance, Handle, ref f) == 0) return false;
         GetIntAnnot(f, ref Annot);
         return true;
      }

      public bool GetAnnotBBox(int Handle, ref TPDFRect BBox)
      {
         return pdfGetAnnotBBox(m_Instance, Handle, ref BBox) != 0;
      }

      public int GetAnnotCount()
      {
         return pdfGetAnnotCount(m_Instance);
      }

      public bool GetAnnotEx(int Handle, ref TPDFAnnotationEx Annot)
      {
         TPDFAnnotationEx_I f = new TPDFAnnotationEx_I();
         if (pdfGetAnnotEx(m_Instance, Handle, ref f) == 0) return false;
         GetIntAnnotEx(f, ref Annot);
         return true;
      }

      public int GetAnnotFlags()
      {
         return pdfGetAnnotFlags(m_Instance);
      }

      public String GetAnnotLink(int Handle)
      {
         IntPtr retval = pdfGetAnnotLink(m_Instance, Handle);
         return _Conv.ToString(retval, IntPtr.Zero);
      }

      public int GetAnnotType(int Handle)
      {
         return pdfGetAnnotType(m_Instance, Handle);
      }

      public double GetAscent()
      {
         return pdfGetAscent(m_Instance);
      }

      public bool GetBarcodeDict(IntPtr IBarcode, ref TPDFBarcode Barcode)
      {
         TPDFBarcode_I bc = new TPDFBarcode_I();
         bc.StructSize = Marshal.SizeOf(bc);
         if (pdfGetBarcodeDict(IBarcode, ref bc) == 0) return false;
         GetIntBarcode(bc, ref Barcode);
         return true;
      }

      public bool GetBBox(TPageBoundary Boundary, ref TPDFRect BBox)
      {
         return pdfGetBBox(m_Instance, Boundary, ref BBox) != 0;
      }

      public int GetBidiMode()
      {
         return pdfGetBidiMode(m_Instance);
      }

      public bool GetBookmark(int Handle, ref TBookmark Bmk)
      {
         TBookmark_I b = new TBookmark_I();
         if (pdfGetBookmark(m_Instance, Handle, ref b) == 0) return false;
         Bmk.Color    = b.Color;
         Bmk.DestPage = b.DestPage;
         Bmk.DestPos  = b.DestPos;
         Bmk.DestType = b.DestType;
         Bmk.Open     = b.Open != 0;
         Bmk.Parent   = b.Parent;
         Bmk.Style    = b.Style;
         Bmk.Title    = _Conv.ToString(b.Title, b.TitleLen, b.Unicode != 0);
         return true;
      }

      public int GetBookmarkCount()
      {
         return pdfGetBookmarkCount(m_Instance);
      }

      public bool GetBookmarkEx(int Handle, ref TPDFBookmark Bmk)
      {
         TPDFBookmark_I b = new TPDFBookmark_I();
         b.StructSize = Marshal.SizeOf(b);
         if (pdfGetBookmarkEx(m_Instance, Handle, ref b) == 0) return false;
         Bmk.Action     = b.Action;
         Bmk.Color      = b.Color;
         Bmk.DestPage   = b.DestPage;
         Bmk.DestPos    = b.DestPos;
         Bmk.DestType   = b.DestType;
         Bmk.NamedDest  = _Conv.ToString(b.NamedDestA, b.NamedDestW);
         Bmk.Open       = b.Open != 0;
         Bmk.Parent     = b.Parent;
         Bmk.StructElem = b.StructElem;
         Bmk.Style      = b.Style;
         Bmk.Title      = _Conv.ToString(b.TitleA, b.TitleW, b.TitleLen);
         return true;
      }

      public int GetBorderStyle()
      {
         return pdfGetBorderStyle(m_Instance);
      }

      public IntPtr GetBuffer(ref int BufSize)
      {
         // The function returns a pointer to the original buffer.
         return pdfGetBuffer(m_Instance, ref BufSize);
      }

      public byte[] GetBuffer()
      {
         int bufSize = 0;
         IntPtr buffer = GetBuffer(ref bufSize);
         if (IntPtr.Zero.Equals(buffer) || bufSize == 0) return null;
         byte[] retval = new byte[bufSize];
         System.Runtime.InteropServices.Marshal.Copy(buffer, retval, 0, bufSize);
         return retval;
      }

      public double GetCapHeight()
      {
         return pdfGetCapHeight(m_Instance);
      }

      public double GetCharacterSpacing()
      {
         return pdfGetCharacterSpacing(m_Instance);
      }

      public int GetCheckBoxChar()
      {
         return pdfGetCheckBoxChar(m_Instance);
      }

      public int GetCheckBoxCharEx(int Field)
      {
         return pdfGetCheckBoxCharEx(m_Instance, Field);
      }

      public int GetCheckBoxDefState(int Field)
      {
         return pdfGetCheckBoxDefState(m_Instance, Field);
      }

      public bool GetCMap(int Index, ref TPDFCMap CMap)
      {
         CMap.StructSize = Marshal.SizeOf(CMap);
         return pdfGetCMap(m_Instance, Index, ref CMap) != 0;
      }
      
      public int GetCMapCount()
      {
         return pdfGetCMapCount(m_Instance);
      }

      public bool GetCollectionInfo(IntPtr IPDF, ref TPDFCollectionInfo Value)
      {
         TPDFCollectionInfo_I info = new TPDFCollectionInfo_I();
         info.StructSize = Marshal.SizeOf(info);
         if (pdfGetCollectionInfo(m_Instance, ref info) == 0) return false;
         Value.InitalFile = info.InitalFile;
         Value.InitalView = info.InitalView;
         Value.SortBy     = _Conv.ToString(info.SortBy, false);
         Value.SortDesc   = info.SortDesc != 0;
         Value.SplitInfo  = info.SplitInfo;
         Value.SplitPos   = info.SplitPos;
         return true;
      }

      public int GetColorSpace()
      {
         return pdfGetColorSpace(m_Instance);
      }

      public int GetColorSpaceCount()
      {
         return pdfGetColorSpaceCount(m_Instance);
      }

      public bool GetColorSpaceObj(int Handle, ref TPDFColorSpaceObj CS)
      {
         TPDFColorSpaceObj_I ics = new TPDFColorSpaceObj_I();
         if (pdfGetColorSpaceObj(m_Instance, Handle, ref ics) == 0) return false;
         GetIntColorSpaceObj(ics, ref CS);
         return true;
      }

      public bool GetColorSpaceObjEx(IntPtr IColorSpace, ref TPDFColorSpaceObj CS)
      {
         TPDFColorSpaceObj_I ics = new TPDFColorSpaceObj_I();
         if (pdfGetColorSpaceObjEx(IColorSpace, ref ics) == 0) return false;
         GetIntColorSpaceObj(ics, ref CS);
         return true;
      }

      public int GetCompressionFilter()
      {
         return pdfGetCompressionFilter(m_Instance);
      }

      public int GetCompressionLevel()
      {
         return pdfGetCompressionLevel(m_Instance);
      }

      public int GetContent(ref byte[] Buffer)
      {
         IntPtr buf = IntPtr.Zero;
         int retval = pdfGetContent(m_Instance, ref buf);
         MemCopy(buf, ref Buffer, retval);
         return retval;
      }

      public int GetDefBitsPerPixel()
      {
         return pdfGetDefBitsPerPixel(m_Instance);
      }

      public double GetDescent()
      {
         return pdfGetDescent(m_Instance);
      }

      public bool GetDeviceNAttributes(IntPtr IAttributes, ref TDeviceNAttributes Attributes)
      {
         int i;
         TDeviceNAttributes_I attr = new TDeviceNAttributes_I();
         if (pdfGetDeviceNAttributes(IAttributes, ref attr) == 0) return false;
         Attributes.IProcessColorSpace = attr.IProcessColorSpace;
         Attributes.IMixingHints       = attr.IMixingHints;
         if (attr.ProcessColorantsCount > 0)
         {
            byte[] bytes;
            Attributes.ProcessColorants = new String[attr.ProcessColorantsCount];
            for (i = 0; i < attr.ProcessColorantsCount; i++)
            {
               bytes = System.Text.Encoding.Unicode.GetBytes(Marshal.PtrToStringAnsi(attr.ProcessColorants[i]));
               Attributes.ProcessColorants[i] = System.Text.Encoding.UTF8.GetString(bytes);
            }
         }else
         {
            Attributes.ProcessColorants = null;
         }
         if (attr.SeparationsCount > 0)
         {
            Attributes.Separations = new IntPtr[attr.SeparationsCount];
            for (i = 0; i < attr.SeparationsCount; i++)
            {
               Attributes.Separations[i] = attr.Separations[i];
            }
         }
         return true;
      }

      public int GetDocInfo(TDocumentInfo DInfo, ref String Value)
      {
         IntPtr val = IntPtr.Zero;
         int retval = pdfGetDocInfo(m_Instance, DInfo, ref val);
         Value = _Conv.ToString(val, retval, true);
         return retval;
      }

      public int GetDocInfoCount()
      {
         return pdfGetDocInfoCount(m_Instance);
      }

      public int GetDocInfoEx(int Index, ref TDocumentInfo DInfo, ref String Key, ref String Value)
      {
         int retval, uni = 0;
         IntPtr key = IntPtr.Zero;
         IntPtr val = IntPtr.Zero;
         retval = pdfGetDocInfoEx(m_Instance, Index, ref DInfo, ref key, ref val, ref uni);
         if (retval > 0)
         {
            Key   = _Conv.ToString(key, false);
            Value = _Conv.ToString(val, retval, uni != 0);
         }
         return retval;
      }

      public bool GetDocUsesTransparency(int Flags)
      {
         return pdfGetDocUsesTransparency(m_Instance, Flags) != 0;
      }

      public int GetDrawDirection()
      {
         return pdfGetDrawDirection(m_Instance);
      }

      public String GetDynaPDFVersion()
      {
         IntPtr retval = pdfGetDynaPDFVersion();
         return _Conv.ToString(retval, false);
      }

      public int GetDynaPDFVersionInt()
      {
         int retval = 0, value = 0, p = 0, len;
         IntPtr ver = pdfGetDynaPDFVersion();
         string str = Marshal.PtrToStringAnsi(ver);
         byte[] bval = System.Text.Encoding.ASCII.GetBytes(str);
         if ((len = ParseInt(bval, p, ref value)) == 0) return 0;
         p = len + 3;
         retval += value * 10000000;
         if ((len = ParseInt(bval, p, ref value)) == 0) return 0;
         p += len + 1;
         retval += value * 10000;
         if (ParseInt(bval, p, ref value) == 0) return 0;
         retval += value;
         return retval;
      }

      public bool GetEmbeddedFile(int Handle, ref TPDFFileSpec FileSpec, bool Decompress)
      {
         TPDFFileSpec_I f = new TPDFFileSpec_I();
         if (pdfGetEmbeddedFile(m_Instance, Handle, ref f, Convert.ToInt32(Decompress)) == 0) return false;
         /*
            We do not copy the buffer here. This can be done if you really need it.
            Copy the buffer into a byte array if you want to write it into a .Net file stream.
            A better solution would be to write the buffer directly into a file. Take a look into
            the CBinaryStream() class in utils/binary_stream...
         */
         FileSpec.Buffer     = f.Buffer;
         FileSpec.BufSize    = f.BufSize;
         FileSpec.ColItem    = f.ColItem;
         FileSpec.Compressed = f.Compressed != 0;
         FileSpec.FileSize   = f.FileSize;
         FileSpec.IsURL      = f.IsURL != 0;
         if (!IntPtr.Zero.Equals(f.CheckSum))
            MemCopy(f.CheckSum, ref FileSpec.CheckSum, 16);
         else
            FileSpec.CheckSum = null;
         FileSpec.CreateDate = _Conv.ToString(f.CreateDate, false);
         FileSpec.Desc       = _Conv.ToString(f.Desc, f.DescUnicode != 0);
         FileSpec.FileName   = _Conv.ToString(f.FileName, false);
         FileSpec.MIMEType   = _Conv.ToString(f.MIMEType, false);
         FileSpec.ModDate    = _Conv.ToString(f.ModDate, false);
         FileSpec.Name       = _Conv.ToString(f.Name, f.NameUnicode != 0);
         FileSpec.UF         = _Conv.ToString(f.Name, f.UFUnicode != 0);
         return true;
      }

      public int GetEmbeddedFileCount()
      {
         return pdfGetEmbeddedFileCount(m_Instance);
      }

      public bool GetEmbeddedFileNode(IntPtr IEF, ref TPDFEmbFileNode F, bool Decompress)
      {
         TPDFEmbFileNode_I node = new TPDFEmbFileNode_I();
         node.StructSize = Marshal.SizeOf(node);
         if (pdfGetEmbeddedFileNode(IEF, ref node, Convert.ToInt32(Decompress)) == 0) return false;
         byte[] bytes = System.Text.Encoding.Unicode.GetBytes(Marshal.PtrToStringAnsi(node.Name));
         F.Name          = System.Text.Encoding.UTF8.GetString(bytes);
         F.NextNode      = node.NextNode;
         F.EF.Buffer     = node.EF.Buffer;
         F.EF.BufSize    = node.EF.BufSize;
         F.EF.ColItem    = node.EF.ColItem;
         F.EF.Compressed = node.EF.Compressed != 0;
         F.EF.FileSize   = node.EF.FileSize;
         F.EF.IsURL      = node.EF.IsURL != 0;
         if (node.EF.CheckSum != IntPtr.Zero)
         {
            F.EF.CheckSum = new byte[16];
            Marshal.Copy(node.EF.CheckSum, F.EF.CheckSum, 0, 16);
         }else
            F.EF.CheckSum = null;
         F.EF.CreateDate = _Conv.ToString(node.EF.CreateDate, false);
         F.EF.Desc       = _Conv.ToString(node.EF.Desc, node.EF.DescUnicode != 0);
         F.EF.FileName   = _Conv.ToString(node.EF.FileName, false);
         F.EF.MIMEType   = _Conv.ToString(node.EF.MIMEType, false);
         F.EF.ModDate    = _Conv.ToString(node.EF.ModDate, false);
         F.EF.Name       = _Conv.ToString(node.EF.Name, node.EF.NameUnicode != 0);
         F.EF.UF         = _Conv.ToString(node.EF.UF, node.EF.UFUnicode != 0);
         return true;
      }

      public double GetEMFPatternDistance()
      {
         return pdfGetEMFPatternDistance(m_Instance);
      }

      public bool GetErrLogMessage(int Index, ref TPDFError Err)
      {
         TPDFError_I e = new TPDFError_I();
         e.StructSize = Marshal.SizeOf(e);
         if (pdfGetErrLogMessage(m_Instance, Index, ref e) == 0) return false;
         GetIntError(e, ref Err);
         return true;
      }

      public int GetErrLogMessageCount()
      {
         return pdfGetErrLogMessageCount(m_Instance);
      }

      public String GetErrorMessage()
      {
         IntPtr retval = pdfGetErrorMessage(m_Instance);
         return _Conv.ToString(retval, false);
      }

      public int GetErrorMode()
      {
         return pdfGetErrorMode(m_Instance);
      }

      public bool GetField(int Handle, ref TPDFField Field)
      {
         TPDFField_I f = new TPDFField_I();
         if (pdfGetField(m_Instance, Handle, ref f) == 0) return false;
         GetIntField(f, ref Field);
         return true;
      }

      public uint GetFieldBackColor()
      {
         return pdfGetFieldBackColor(m_Instance);
      }

      public uint GetFieldBorderColor()
      {
         return pdfGetFieldBorderColor(m_Instance);
      }

      public int GetFieldBorderStyle(int Field)
      {
         return pdfGetFieldBorderStyle(m_Instance, Field);
      }

      public double GetFieldBorderWidth(int Field)
      {
         return pdfGetFieldBorderWidth(m_Instance, Field);
      }

      public int GetFieldCalcOrder(ref int[] Out)
      {
         IntPtr ptr = IntPtr.Zero;
         int n = pdfGetFieldCalcOrder(m_Instance, ref ptr);
         if (n > 0)
         {
            Out = new int[n];
            Marshal.Copy(ptr, Out, 0, n);
         }else
            Out = null;
         return n;
      }

      public bool GetFieldChoiceValue(int AField, int ValIndex, ref TPDFChoiceValue Value)
      {
         TPDFChoiceValue_I v = new TPDFChoiceValue_I();
         v.StructSize = Marshal.SizeOf(v);
         if (pdfGetFieldChoiceValue(m_Instance, AField, ValIndex, ref v) == 0) return false;
         Value.ExpValue = _Conv.ToString(v.ExpValueA, v.ExpValueW, v.ExpValueLen);
         Value.Value    = _Conv.ToString(v.ValueA, v.ValueW, v.ValueLen);
         Value.Selected = v.Selected != 0;
         return true;
      }

      public bool GetFieldColor(int Field, TFieldColor ColorType, ref TPDFColorSpace ColorSpace, ref uint Color)
      {
         return pdfGetFieldColor(m_Instance, Field, ColorType, ref ColorSpace, ref Color) != 0;
      }

      public int GetFieldCount()
      {
         return pdfGetFieldCount(m_Instance);
      }

      public bool GetFieldEx(int Handle, ref TPDFFieldEx Field)
      {
         TPDFFieldEx_I f = new TPDFFieldEx_I();
         f.StructSize = Marshal.SizeOf(f);
         if (pdfGetFieldEx(m_Instance, Handle, ref f) == 0) return false;
         GetIntFieldEx(f, ref Field);
         return true;
      }

      public bool GetFieldEx2(IntPtr IField, ref TPDFFieldEx Field)
      {
         TPDFFieldEx_I f = new TPDFFieldEx_I();
         f.StructSize = Marshal.SizeOf(f);
         if (pdfGetFieldEx2(IField, ref f) == 0) return false;
         GetIntFieldEx(f, ref Field);
         return true;
      }

      public int GetFieldExpValCount(int Field)
      {
         return pdfGetFieldExpValCount(m_Instance, Field);
      }

      public int GetFieldExpValue(int Field, ref String Value)
      {
         IntPtr val = IntPtr.Zero;
         int retval = pdfGetFieldExpValue(m_Instance, Field, ref val);
         Value = _Conv.ToString(val, retval, false);
         return retval;
      }

      public bool GetFieldExpValueEx(int Field, int ValIndex, ref String Value, ref String ExpValue, ref bool Selected)
      {
         int sel = 0;
         IntPtr expVal = IntPtr.Zero;
         IntPtr val = IntPtr.Zero;
         if (pdfGetFieldExpValueEx(m_Instance, Field, ValIndex, ref val, ref expVal, ref sel) != 0)
         {
            Value    = _Conv.ToString(val, false);
            ExpValue = _Conv.ToString(expVal, false);
            Selected = sel != 0;
            return true;
         }else
         {
            Value    = null;
            ExpValue = null;
            Selected = false;
         }
         return false;
      }

      public int GetFieldFlags(int Field)
      {
         return pdfGetFieldFlags(m_Instance, Field);
      }

      public int GetFieldGroupType(int Field)
      {
         return pdfGetFieldGroupType(m_Instance, Field);
      }

      public int GetFieldHighlightMode(int Field)
      {
         return pdfGetFieldHighlightMode(m_Instance, Field);
      }

      public int GetFieldIndex(int Field)
      {
         return pdfGetFieldIndex(m_Instance, Field);
      }

      public int GetFieldMapName(int Field, ref String Value)
      {
         int retval, uni = 0;
         IntPtr val = IntPtr.Zero;
         retval = pdfGetFieldMapName(m_Instance, Field, ref val, ref uni);
         Value  = _Conv.ToString(val, retval, uni != 0);
         return retval;
      }

      public String GetFieldName(int Field)
      {
         IntPtr val = IntPtr.Zero;
         int retval = pdfGetFieldName(m_Instance, Field, ref val);
         int len    = pdfStrLenA(val);
         return _Conv.ToString(val, retval, len != retval);
      }

      public int GetFieldOrientation(int Field)
      {
         return pdfGetFieldOrientation(m_Instance, Field);
      }

      public int GetFieldTextAlign(int Field)
      {
         return pdfGetFieldTextAlign(m_Instance, Field);
      }

      public uint GetFieldTextColor()
      {
         return pdfGetFieldTextColor(m_Instance);
      }

      public int GetFieldToolTip(int Field, ref String Value)
      {
         int retval, uni = 0;
         IntPtr val = IntPtr.Zero;
         retval = pdfGetFieldToolTip(m_Instance, Field, ref val, ref uni);
         Value  = _Conv.ToString(val, retval, uni != 0);
         return retval;
      }

      public int GetFieldType(int Field)
      {
         return pdfGetFieldType(m_Instance, Field);
      }

      public bool GetFileSpec(IntPtr IFS, ref TPDFFileSpecEx F)
      {
         TPDFFileSpecEx_I fs = new TPDFFileSpecEx_I();
         fs.StructSize = Marshal.SizeOf(fs);
         if (pdfGetFileSpec(IFS, ref fs) == 0) return false;
         CopyFileSpecEx(ref fs, ref F);
         return true;
      }

      public uint GetFillColor()
      {
         return pdfGetFillColor(m_Instance);
      }

      public bool GetFont(IntPtr IFont, ref TPDFFontObj F)
      {
         TPDFFontObj_I font = new TPDFFontObj_I();
         if (fntGetFont(IFont, ref font) == 0) return false;
         GetIntFont(font, ref F);
         return true;
      }

      public int GetFontCount()
      {
         return pdfGetFontCount(m_Instance);
      }

      public bool GetFontEx(int Handle, ref TPDFFontObj F)
      {
         TPDFFontObj_I font = new TPDFFontObj_I();
         if (pdfGetFontEx(m_Instance, Handle, ref font) == 0) return false;
         GetIntFont(font, ref F);
         return true;
      }

      public bool GetFontInfo(IntPtr IFont, ref TPDFFontInfo F)
      {
         TPDFFontInfo_I font = new TPDFFontInfo_I();
         font.StructSize = Marshal.SizeOf(font);
         if (fntGetFontInfo(IFont, ref font) == 0) return false;
         GetIntFontInfo(font, ref F);
         return true;
      }

      public bool GetFontInfoEx(int Handle, ref TPDFFontInfo F)
      {
         TPDFFontInfo_I font = new TPDFFontInfo_I();
         font.StructSize = Marshal.SizeOf(font);
         if (pdfGetFontInfoEx(m_Instance, Handle, ref font) == 0) return false;
         GetIntFontInfo(font, ref F);
         return true;
      }

      public bool GetFontMetrics(int Handle, ref TPDFFontMetrics Metrics)
      {
         Metrics.StructSize = Marshal.SizeOf(Metrics);
         return pdfGetFontMetrics(m_Instance, Handle, ref Metrics) != 0;
      }

      public int GetFontOrigin()
      {
         return pdfGetFontOrigin(m_Instance);
      }

      public void GetFontSearchOrder(TFontBaseType[] Order)
      {
         pdfGetFontSearchOrder(m_Instance, Order);
      }

      public int GetFontSelMode()
      {
         return pdfGetFontSelMode(m_Instance);
      }

      public double GetFontSize()
      {
         return pdfGetFontSize(m_Instance);
      }

      public int GetFontWeight()
      {
         return pdfGetFontWeight(m_Instance);
      }

      public double GetFTextHeight(TTextAlign Align, String AText)
      {
         return pdfGetFTextHeightW(m_Instance, Align, AText);
      }

      public double GetFTextHeightA(TTextAlign Align, String AText)
      {
         return pdfGetFTextHeightA(m_Instance, Align, AText);
      }

      public double GetFTextHeightW(TTextAlign Align, String AText)
      {
         return pdfGetFTextHeightW(m_Instance, Align, AText);
      }

      public double GetFTextHeightEx(double Width, TTextAlign Align, String AText)
      {
         return pdfGetFTextHeightExW(m_Instance, Width, Align, AText);
      }

      public double GetFTextHeightExA(double Width, TTextAlign Align, String AText)
      {
         return pdfGetFTextHeightExA(m_Instance, Width, Align, AText);
      }

      public double GetFTextHeightExW(double Width, TTextAlign Align, String AText)
      {
         return pdfGetFTextHeightExW(m_Instance, Width, Align, AText);
      }

      public int GetFullyQualifiedFieldName(int Handle, ref String Value)
      {
         int    len = 0;
         IntPtr val = IntPtr.Zero;
         Value = null;
         len = pdfGetFullyQualifiedFieldName(m_Instance, Handle, ref val);
         if (len > 0)
            Value = Marshal.PtrToStringUni(val, len);
         return len;
      }

      public int GetGlyphIndex(int Index)
      {
         return pdfGetGlyphIndex(m_Instance, Index);
      }

      public bool GetGoToAction(int Handle, ref TPDFGoToAction Action)
      {
         TPDFGoToAction_I a = new TPDFGoToAction_I();
         a.StructSize = Marshal.SizeOf(a);
         if (pdfGetGoToAction(m_Instance, Handle, ref a) == 0) return false;
         Action.DestFile       = a.DestFile;
         Action.DestPage       = a.DestPage;
         Action.DestName       = _Conv.ToString(a.DestNameA, a.DestNameW);
         Action.DestType       = a.DestType;
         Action.NewWindow      = a.NewWindow;
         Action.NextAction     = a.NextAction;
         Action.NextActionType = a.NextActionType;
         if (a.DestPos != IntPtr.Zero)
         {
            Action.DestPos = new float[4];
            Marshal.Copy(a.DestPos, Action.DestPos, 0, 4);
         }else
            Action.DestPos = null;
         return true;
      }

      public bool GetGoToRAction(int Handle, ref TPDFGoToAction Action)
      {
         return GetGoToAction(Handle, ref Action);
      }

      public int GetGlyphOutline(int Index, ref TPDFGlyphOutline Outline)
      {
         TPDFGlyphOutline_I g = new TPDFGlyphOutline_I();
         int retval = pdfGetGlyphOutline(m_Instance, Index, null);
         Outline.Outline = null;
         if (retval >= 0)
         {
            int i;
            long p;
            IntPtr ptr;
            TI32Point dummy = new TI32Point();
            g.Outline = Marshal.AllocHGlobal(retval * Marshal.SizeOf(dummy));
            retval = pdfGetGlyphOutline(m_Instance, Index, g);
            Outline.Outline = new TI32Point[retval];
            ptr = g.Outline;
            p = ptr.ToInt64();
            for (i = 0; i < retval; i++)
            {
               Outline.Outline[i] = (TI32Point)Marshal.PtrToStructure(ptr, typeof(TI32Point));
               p += Marshal.SizeOf(dummy);
               ptr = new IntPtr(p);
            }
            Marshal.FreeHGlobal(g.Outline);
         }
         Outline.AdvanceX = g.AdvanceX;
         Outline.AdvanceY = g.AdvanceY;
         Outline.OriginX  = g.OriginX;
         Outline.OriginY  = g.OriginY;
         Outline.Lsb      = g.Lsb;
         Outline.Tsb      = g.Tsb;
         Outline.HaveBBox = g.HaveBBox != 0;
         Outline.BBox     = g.BBox;
         return retval;
      }

      public uint GetGStateFlags()
      {
         return pdfGetGStateFlags(m_Instance);
      }

      public bool GetHideAction(int Handle, ref TPDFHideAction Action)
      {
         TPDFHideAction_I a = new TPDFHideAction_I();
         a.StructSize = Marshal.SizeOf(a);
         if (pdfGetHideAction(m_Instance, Handle, ref a) == 0) return false;
         Action.Hide           = a.Hide != 0;
         Action.NextAction     = a.NextAction;
         Action.NextActionType = a.NextActionType;
         if (a.FieldsCount > 0)
         {
            Action.Fields = new IntPtr[a.FieldsCount];
            Marshal.Copy(a.Fields, Action.Fields, 0, a.FieldsCount);
         }else
            Action.Fields = null;
         return true;
      }

      public uint GetIconColor()
      {
         return pdfGetIconColor(m_Instance);
      }

      public IntPtr GetImageBuffer(ref int BufSize)
      {
         return pdfGetImageBuffer(m_Instance, ref BufSize);
      }

      public int GetImageCount(String FileName)
      {
         return pdfGetImageCount(m_Instance, FileName);
      }

      public int GetImageCountEx(byte[] Buffer)
      {
         return pdfGetImageCountEx(m_Instance, Buffer, Buffer.Length);
      }

      public int GetImageHeight(int Handle)
      {
         return pdfGetImageHeight(m_Instance, Handle);
      }

      public bool GetImageObj(int Handle, TParseFlags Flags, ref TPDFImage Image)
      {
         return pdfGetImageObj(m_Instance, Handle, Flags, ref Image) != 0;
      }

      public int GetImageObjCount()
      {
         return pdfGetImageObjCount(m_Instance);
      }

      public bool GetImageObjEx(IntPtr ImagePtr, TParseFlags Flags, ref TPDFImage Image)
      {
         return pdfGetImageObjEx(m_Instance, ImagePtr, Flags, ref Image) != 0;
      }

      public int GetImageWidth(int Handle)
      {
         return pdfGetImageWidth(m_Instance, Handle);
      }

      public bool GetImportDataAction(int Handle, ref TPDFImportDataAction Action)
      {
         TPDFImportDataAction_I a = new TPDFImportDataAction_I();
         a.StructSize = Marshal.SizeOf(a);
         if (pdfGetImportDataAction(m_Instance, Handle, ref a) == 0) return false;
         CopyFileSpecEx(ref a.Data, ref Action.Data);
         Action.NextAction     = a.NextAction;
         Action.NextActionType = a.NextActionType;
         return true;
      }

      public uint GetImportFlags()
      {
         return pdfGetImportFlags(m_Instance);
      }

      public uint GetImportFlags2()
      {
         return pdfGetImportFlags2(m_Instance);
      }

      public bool GetInBBox(int PageNum, TPageBoundary Boundary, ref TPDFRect BBox)
      {
         return pdfGetInBBox(m_Instance, PageNum, Boundary, ref BBox) != 0;
      }

      public int GetInDocInfo(TDocumentInfo DInfo, ref String Value)
      {
         int retval;
         IntPtr val = IntPtr.Zero;
         retval = pdfGetInDocInfo(m_Instance, DInfo, ref val);
         Value  = _Conv.ToString(val, retval, true);
         return retval;
      }

      public int GetInDocInfoCount()
      {
         return pdfGetInDocInfoCount(m_Instance);
      }

      public int GetInDocInfoEx(int Index, ref TDocumentInfo DInfo, ref String Key, ref String Value)
      {
         int retval, uni = 0;
         IntPtr key = IntPtr.Zero;
         IntPtr val = IntPtr.Zero;
         retval = pdfGetInDocInfoEx(m_Instance, Index, ref DInfo, ref key, ref val, ref uni);
         if (retval > 0)
         {
            Key   = _Conv.ToString(key, false);
            Value = _Conv.ToString(val, retval, uni != 0);
         }else
         {
            Key   = null;
            Value = null;
         }
         return retval;
      }

      public int GetInEncryptionFlags()
      {
         return pdfGetInEncryptionFlags(m_Instance);
      }

      public int GetInFieldCount()
      {
         return pdfGetInFieldCount(m_Instance);
      }

      public bool GetInIsCollection()
      {
         int retval;
         retval = pdfGetInIsCollection(m_Instance);
         if (retval < 0) return false;
         return (retval != 0);
      }

      public bool GetInIsEncrypted()
      {
         int retval;
         retval = pdfGetInIsEncrypted(m_Instance);
         if (retval < 0) return false;
         return (retval != 0);
      }

      public bool GetInIsSigned()
      {
         int retval;
         retval = pdfGetInIsSigned(m_Instance);
         if (retval < 0) return false;
         return (retval != 0);
      }

      public bool GetInIsTrapped()
      {
         int retval;
         retval = pdfGetInIsTrapped(m_Instance);
         if (retval < 0) return false;
         return (retval != 0);
      }

      public bool GetInIsXFAForm()
      {
         int retval;
         retval = pdfGetInIsXFAForm(m_Instance);
         if (retval < 0) return false;
         return (retval != 0);
      }

      public bool GetInkList(IntPtr List, ref float[] Value)
      {
         int cnt = 0;
         IntPtr v = IntPtr.Zero;
         if (pdfGetInkList(List, ref v, ref cnt) != 0)
         {
            if (cnt > 0)
            {
               Value = new float[cnt];
               Marshal.Copy(v, Value, 0, cnt);
            }else
               Value = null;
            return true;
         }else
         {
            Value = null;
            return false;
         }
      }

      public bool GetInMetadata(int PageNum, ref byte[] Buffer)
      {
         IntPtr buffer = new IntPtr();
         int bufSize = 0;
         Buffer = null;
         if (pdfGetInMetadata(m_Instance, PageNum, ref buffer, ref bufSize) != 0)
         {
            if (bufSize > 0)
            {
               Buffer = new byte[bufSize];
               Marshal.Copy(buffer, Buffer, 0, bufSize);
            }
            return true;
         }else
            return false;
      }

      public int GetInOrientation(int PageNum)
      {
         return pdfGetInOrientation(m_Instance, PageNum);
      }

      public int GetInPageCount()
      {
         return pdfGetInPageCount(m_Instance);
      }

      public int GetInPDFVersion()
      {
         return pdfGetInPDFVersion(m_Instance);
      }

      public bool GetInPDFVersionEx(ref TPDFVersionInfo Value)
      {
         TPDFVersionInfo_I v = new TPDFVersionInfo_I();
         v.StructSize = Marshal.SizeOf(v);
         if (pdfGetInPDFVersionEx(m_Instance, ref v) != 0)
         {
            Value.Major           = v.Major;
            Value.Minor           = v.Minor;
            Value.MainVer         = _Conv.ToString(v.MainVer, false);
            Value.SubVer          = _Conv.ToString(v.SubVer, false);
            Value.PDFAConformance = _Conv.ToString(v.PDFAConformance, false);
            Value.PDFAVersion     = v.PDFAVersion;
            Value.PDFEVersion     = _Conv.ToString(v.PDFEVersion, false);
            Value.PDFVTModDate    = _Conv.ToString(v.PDFVTModDate, false);
            Value.PDFVTVersion    = _Conv.ToString(v.PDFVTVersion, false);
            Value.PDFXConformance = _Conv.ToString(v.PDFXConformance, false);
            Value.PDFXVersion     = _Conv.ToString(v.PDFXVersion, false);
            Value.FXConfLevel     = _Conv.ToString(v.FXConfLevel, false);
            Value.FXDocName       = _Conv.ToString(v.FXDocName, false);
            Value.FXDocType       = _Conv.ToString(v.FXDocType, false);
            Value.FXVersion       = _Conv.ToString(v.FXVersion, false);
            Value.VersionConst    = v.VersionConst;
            return true;
         }
         return false;
      }

      public bool GetInPrintSettings(ref TPDFPrintSettings Settings)
      {
         TPDFPrintSettings_I s = new TPDFPrintSettings_I();
         if (pdfGetInPrintSettings(m_Instance, ref s) == 0) return false;
         Settings.DuplexMode        = s.DuplexMode;
         Settings.NumCopies         = s.NumCopies;
         Settings.PickTrayByPDFSize = s.PickTrayByPDFSize;
         Settings.PrintScaling      = s.PrintScaling;
         if (s.PrintRangesCount > 0)
         {
            Settings.PrintRanges = new int[s.PrintRangesCount<<1];
            Marshal.Copy(s.PrintRanges, Settings.PrintRanges, 0, s.PrintRangesCount<<1);
         }
         return true;
      }

      public bool GetInRepairMode()
      {
         return pdfGetInRepairMode(m_Instance) != 0;
      }

      public int GetInIsTaggedPDF()
      {
         return pdfGetInIsTaggedPDF(m_Instance);
      }

      public bool GetIsTaggingEnabled()
      {
         return pdfGetIsTaggingEnabled(m_Instance) != 0;
      }

      public bool GetIsFixedPitch()
      {
         return pdfGetIsFixedPitch(m_Instance) != 0;
      }

      public double GetItalicAngle()
      {
         return pdfGetItalicAngle(m_Instance);
      }

      public bool GetJavaScript(int Handle, ref String Script)
      {
         int len = 0;
         int uni = 0;
         IntPtr retval = pdfGetJavaScript(m_Instance, Handle, ref len, ref uni);
         Script = _Conv.ToString(retval, len, uni != 0);
         return (len > 0);
      }

      public bool GetJavaScriptAction(int Handle, ref String Script)
      {
         int len = 0;
         int uni = 0;
         IntPtr retval = pdfGetJavaScriptAction(m_Instance, Handle, ref len, ref uni);
         Script = _Conv.ToString(retval, len, uni != 0);
         return (len > 0);
      }
      
      public bool GetJavaScriptAction2(TObjType ObjType, int ObjHandle, int ActIndex, ref String Script, ref TObjEvent Event)
      {
         int len = 0;
         int uni = 0;
         IntPtr retval = pdfGetJavaScriptAction2(m_Instance, ObjType, ObjHandle, ActIndex, ref len, ref uni, ref Event);
         Script = _Conv.ToString(retval, len, uni != 0);
         return (len > 0);
      }

      public bool GetJavaScriptActionEx(int Handle, ref TPDFJavaScriptAction Action)
      {
         TPDFJavaScriptAction_I a = new TPDFJavaScriptAction_I();
         a.StructSize = Marshal.SizeOf(a);
         if (pdfGetJavaScriptActionEx(m_Instance, Handle, ref a) == 0) return false;
         Action.Script         = _Conv.ToString(a.ScriptA, a.ScriptW, a.ScriptLen);
         Action.NextAction     = a.NextAction;
         Action.NextActionType = a.NextActionType;
         return true;
      }

      public int GetJavaScriptCount()
      {
         return pdfGetJavaScriptCount(m_Instance);
      }

      public bool GetJavaScriptEx(String Name, ref String Script)
      {
         int len = 0;
         int uni = 0;
         IntPtr retval = pdfGetJavaScriptEx(m_Instance, Name, ref len, ref uni);
         Script = _Conv.ToString(retval, len, uni != 0);
         return (len > 0);
      }

      public String GetJavaScriptName(int Handle)
      {
         int len = 0;
         int uni = 0;
         IntPtr retval = pdfGetJavaScriptName(m_Instance, Handle, ref len, ref uni);
         return _Conv.ToString(retval, len, uni != 0);
      }

      public int GetJPEGQuality()
      {
         return pdfGetJPEGQuality(m_Instance);
      }

      public String GetLanguage()
      {
         IntPtr retval = pdfGetLanguage(m_Instance);
         return _Conv.ToString(retval, false);
      }

      public double GetLastTextPosX()
      {
         return pdfGetLastTextPosX(m_Instance);
      }

      public double GetLastTextPosY()
      {
         return pdfGetLastTextPosY(m_Instance);
      }

      public bool GetLayerConfig(int Index, ref TPDFOCLayerConfig Config)
      {
         TPDFOCLayerConfig_I cfg = new TPDFOCLayerConfig_I();
         cfg.StructSize = Marshal.SizeOf(cfg);
         if (pdfGetLayerConfig(m_Instance, Index, ref cfg) == 0) return false;
         Config.Intent    = cfg.Intent;
         Config.IsDefault = cfg.IsDefault != 0;
         Config.Name      = _Conv.ToString(cfg.NameA, cfg.NameW, cfg.NameLen);
         return true;
      }

      public int GetLayerConfigCount()
      {
         return pdfGetLayerConfigCount(m_Instance);
      }

      public bool GetLaunchAction(int Handle, ref TPDFLaunchAction Action)
      {
         TPDFLaunchAction_I a = new TPDFLaunchAction_I();
         a.StructSize = Marshal.SizeOf(a);
         if (pdfGetLaunchAction(m_Instance, Handle, ref a) == 0) return false;
         Action.AppName        = _Conv.ToString(a.AppName, false);
         Action.DefDir         = _Conv.ToString(a.DefDir, false);
         Action.File           = a.File;
         Action.NewWindow      = a.NewWindow;
         Action.NextAction     = a.NextAction;
         Action.NextActionType = a.NextActionType;
         Action.Operation      = _Conv.ToString(a.Operation, false);
         Action.Parameter      = _Conv.ToString(a.Parameter, false);
         return true;
      }

      public double GetLeading()
      {
         return pdfGetLeading(m_Instance);
      }

      public int GetLineCapStyle()
      {
         return pdfGetLineCapStyle(m_Instance);
      }

      public int GetLineJoinStyle()
      {
         return pdfGetLineJoinStyle(m_Instance);
      }

      public double GetLineWidth()
      {
         return pdfGetLineWidth(m_Instance);
      }

      public int GetLinkHighlightMode()
      {
         return pdfGetLinkHighlightMode(m_Instance);
      }

      public bool GetLogMetafileSize(String FileName, ref TRectL R)
      {
         return pdfGetLogMetafileSize(m_Instance, FileName, ref R) != 0;
      }

      public bool GetLogMetafileSizeEx(byte[] Buffer, ref TRectL R)
      {
         return pdfGetLogMetafileSizeEx(m_Instance, Buffer, Buffer.Length, ref R) != 0;
      }

      public bool GetMatrix(ref TCTM M)
      {
         return pdfGetMatrix(m_Instance, ref M) != 0;
      }

      public int GetMaxFieldLen(int TxtField)
      {
         return pdfGetMaxFieldLen(m_Instance, TxtField);
      }

      public bool GetMeasureObj(IntPtr MeasurePtr, ref TPDFMeasure Value)
      {
         TPDFMeasure_I m = new TPDFMeasure_I();
         m.StructSize = Marshal.SizeOf(m);
         if (pdfGetMeasureObj(MeasurePtr, ref m) == 0) return false;
         if (m.IsRectilinear != 0)
         {
            Value.IsRectilinear = true;
            Value.Angles   = CopyIntPtrArray(m.Angles, m.AnglesCount);
            Value.Area     = CopyIntPtrArray(m.Area, m.AreaCount);
            Value.CXY      = m.CXY;
            Value.Distance = CopyIntPtrArray(m.Distance, m.DistanceCount);
            Value.OriginX  = m.OriginX;
            Value.OriginY  = m.OriginY;
            Value.R        = _Conv.ToString(m.RA, m.RW);
            Value.Slope    = CopyIntPtrArray(m.Slope, m.SlopeCount);
            Value.X        = CopyIntPtrArray(m.X, m.XCount);
            Value.Y        = CopyIntPtrArray(m.Y, m.YCount);
         }else
         {
            Value.Bounds = CopyFloatArray(m.Bounds, m.BoundCount);
            if (m.DCS_IsSet != 0)
            {
               Value.DCS_IsSet     = true;
               Value.DCS_Projected = m.DCS_Projected != 0;
               Value.DCS_EPSG      = m.DCS_EPSG;
               Value.DCS_WKT       = _Conv.ToString(m.DCS_WKT, false);
            }
            Value.GCS_Projected = m.GCS_Projected != 0;
            Value.GCS_EPSG      = m.GCS_EPSG;
            Value.GCS_WKT       = _Conv.ToString(m.GCS_WKT, false);
            Value.GPTS          = CopyFloatArray(m.GPTS, m.GPTSCount);
            Value.LPTS          = CopyFloatArray(m.LPTS, m.LPTSCount);
            Value.PDU1          = _Conv.ToString(m.PDU1, false);
            Value.PDU2          = _Conv.ToString(m.PDU2, false);
            Value.PDU3          = _Conv.ToString(m.PDU3, false);
         }
         return true;
      }

      public uint GetMetaConvFlags()
      {
         return pdfGetMetaConvFlags(m_Instance);
      }

      public bool GetMetadata(TMetadataObj ObjType, int Handle, ref byte[] Buffer)
      {
         IntPtr buffer = new IntPtr();
         int bufSize = 0;
         Buffer = null;
         if (pdfGetMetadata(m_Instance, ObjType, Handle, ref buffer, ref bufSize) != 0)
         {
            if (bufSize > 0)
            {
               Buffer = new byte[bufSize];
               Marshal.Copy(buffer, Buffer, 0, bufSize);
            }
            return true;
         }else
            return false;
      }

      public uint[] GetMissingGlyphs()
      {
         int count = 0;
         IntPtr ptr = pdfGetMissingGlyphs(m_Instance, ref count);
         if (IntPtr.Zero.Equals(ptr))
            return null;
         else
         {
            uint[] retval = new uint[count];
            // Why does the marshaller not provide a copy function for uint[]?
            pdfCopyMemUInt(ptr, retval, count << 2);
            return retval;
         }
      }

      public double GetMiterLimit()
      {
         return pdfGetMiterLimit(m_Instance);
      }

      public bool GetMovieAction(int Handle, ref TPDFMovieAction Action)
      {
         TPDFMovieAction_I a = new TPDFMovieAction_I();
         a.StructSize = Marshal.SizeOf(a);
         if (pdfGetMovieAction(m_Instance, Handle, ref a) == 0) return false;
         Action.Annot          = a.Annot;
         Action.FWPosition[0]  = a.FWPosition[0];
         Action.FWPosition[1]  = a.FWPosition[1];
         Action.FWScale[0]     = a.FWScale[0];
         Action.FWScale[1]     = a.FWScale[1];
         Action.Mode           = _Conv.ToString(a.Mode, false);
         Action.NextAction     = a.NextAction;
         Action.NextActionType = a.NextActionType;
         Action.Operation      = _Conv.ToString(a.Operation, false);
         Action.Rate           = a.Rate;
         Action.ShowControls   = a.ShowControls != 0;
         Action.Synchronous    = a.Synchronous != 0;
         Action.Title          = _Conv.ToString(a.TitleA, a.TitleW);
         Action.Volume         = a.Volume;
         return true;
      }

      public bool GetNamedAction(int Handle, ref TPDFNamedAction Action)
      {
         TPDFNamedAction_I a = new TPDFNamedAction_I();
         a.StructSize = Marshal.SizeOf(a);
         if (pdfGetNamedAction(m_Instance, Handle, ref a) == 0) return false;
         Action.Name           = _Conv.ToString(a.Name, false);
         Action.NewWindow      = a.NewWindow;
         Action.NextAction     = a.NextAction;
         Action.NextActionType = a.NextActionType;
         Action.Type           = a.Type;
         return true;
      }

      public bool GetNamedDest(int Index, ref TPDFNamedDest Dest)
      {
         TPDFNamedDest_I d = new TPDFNamedDest_I();
         d.StructSize = Marshal.SizeOf(d);
         if (pdfGetNamedDest(m_Instance, Index, ref d) == 0) return false;
         Dest.Name     = _Conv.ToString(d.NameA, d.NameW, d.NameLen);
         Dest.DestFile = _Conv.ToString(d.DestFileA, d.DestFileW, d.DestFileLen);
         Dest.DestPage = d.DestPage;
         Dest.DestPos  = d.DestPos;
         Dest.DestType = d.DestType;
         return true;
      }

      public int GetNamedDestCount()
      {
         return pdfGetNamedDestCount(m_Instance);
      }

      public bool GetNeedAppearance()
      {
         return pdfGetNeedAppearance(m_Instance) != 0;
      }

      public bool GetNumberFormatObj(IntPtr NumberFmtPtr, ref TPDFNumberFormat Value)
      {
         TPDFNumberFormat_I nf = new TPDFNumberFormat_I();
         nf.StructSize = Marshal.SizeOf(nf);
         if (pdfGetNumberFormatObj(NumberFmtPtr, ref nf) == 0) return false;
         Value.C  = nf.C;
         Value.D  = nf.D;
         Value.F  = nf.F;
         Value.FD = nf.FD != 0;
         Value.O  = nf.O;
         Value.PS = _Conv.ToString(nf.PSA, nf.PSW);
         Value.RD = _Conv.ToString(nf.RDA, nf.RDW);
         Value.RT = _Conv.ToString(nf.RTA, nf.RTW);
         Value.SS = _Conv.ToString(nf.SSA, nf.SSW);
         Value.U  = _Conv.ToString(nf.UA, nf.UW);
         return true;
      }

      public int GetObjActionCount(TObjType ObjType, int ObjHandle)
      {
         return pdfGetObjActionCount(m_Instance, ObjType, ObjHandle);
      }

      public int GetObjActions(TObjType ObjType, int ObjHandle, ref TPDFObjActions Actions)
      {
         Actions.StructSize = Marshal.SizeOf(Actions);
         return pdfGetObjActions(m_Instance, ObjType, ObjHandle, ref Actions);
      }

      public bool GetObjEvent(IntPtr IEvent, ref TPDFObjEvent Event)
      {
         Event.StructSize = Marshal.SizeOf(Event);
         return pdfGetObjEvent(IEvent, ref Event) != 0;
      }

      public bool GetOCG(int Handle, ref TPDFOCG Out)
      {
         Out.StructSize = Marshal.SizeOf(Out);
         return pdfGetOCG(m_Instance, Handle, ref Out) != 0;
      }

      public bool GetOCGContUsage(int Handle, ref TPDFOCGContUsage Out)
      {
         Out.StructSize = Marshal.SizeOf(Out);
         return pdfGetOCGContUsage(m_Instance, Handle, ref Out) != 0;
      }

      public int GetOCGCount()
      {
         return pdfGetOCGCount(m_Instance);
      }

      public bool GetOCGUsageUserName(int Handle, int Index, ref String Name)
      {
         IntPtr nameA = new IntPtr(), nameW = new IntPtr();
         Name = null;
         if (pdfGetOCGUsageUserName(m_Instance, Handle, Index, ref nameA, ref nameW) != 0)
         {
            Name = _Conv.ToString(nameA, nameW);
            return true;
         }else
            return false;
      }

      public int GetOCHandle(IntPtr OC)
      {
         return pdfGetOCHandle(OC);
      }

      public IntPtr GetOCUINode(IntPtr Node, TPDFOCUINode OutNode)
      {
         if (Node.Equals(IntPtr.Zero))
            return pdfGetOCUINode(m_Instance, Node, null);
         else
         {
            TPDFOCUINode_I n = new TPDFOCUINode_I();
            n.StructSize = Marshal.SizeOf(n);
            IntPtr retval = pdfGetOCUINode(m_Instance, Node, n);
            OutNode.Label     = _Conv.ToString(n.LabelA, n.LabelW, n.LabelLength);
            OutNode.NewNode   = n.NewNode != 0;
            OutNode.NextChild = n.NextChild;
            OutNode.OCG       = n.OCG;
            return retval;
         }
      }

      public double GetOpacity()
      {
         return pdfGetOpacity(m_Instance);
      }

      public int GetOrientation()
      {
         return pdfGetOrientation(m_Instance);
      }

      public bool GetOutputIntent(int Index, ref TPDFOutputIntent Intent)
      {
         TPDFOutputIntent_I tmp = new TPDFOutputIntent_I();
         tmp.StructSize = Marshal.SizeOf(tmp);
         if (pdfGetOutputIntent(m_Instance, Index, ref tmp) == 0) return false;
         GetIntOutputIntent(ref tmp, ref Intent);
         return true;
      }

      public int GetOutputIntentCount()
      {
         return pdfGetOutputIntentCount(m_Instance);
      }

      public bool GetPageAnnot(int Index, ref TPDFAnnotation Annot)
      {
         TPDFAnnotation_I f = new TPDFAnnotation_I();
         if (pdfGetPageAnnot(m_Instance, Index, ref f) == 0) return false;
         GetIntAnnot(f, ref Annot);
         return true;
      }

      public int GetPageAnnotCount()
      {
         return pdfGetPageAnnotCount(m_Instance);
      }

      public bool GetPageAnnotEx(int Index, ref TPDFAnnotationEx Annot)
      {
         TPDFAnnotationEx_I f = new TPDFAnnotationEx_I();
         if (pdfGetPageAnnotEx(m_Instance, Index, ref f) == 0) return false;
         GetIntAnnotEx(f, ref Annot);
         return true;
      }

      public bool GetPageBBox(IntPtr PagePtr, TPageBoundary Boundary, ref TFltRect BBox)
      {
         return pdfGetPageBBox(PagePtr, Boundary, ref BBox) != 0;
      }

      public int GetPageCoords()
      {
         return pdfGetPageCoords(m_Instance);
      }

      public int GetPageCount()
      {
         return pdfGetPageCount(m_Instance);
      }

      public bool GetPageField(int Index, ref TPDFField Field)
      {
          TPDFField_I f = new TPDFField_I();
          if (pdfGetPageField(m_Instance, Index, ref f) == 0) return false;
          GetIntField(f, ref Field);
          return true;
      }

      public int GetPageFieldCount()
      {
         return pdfGetPageFieldCount(m_Instance);
      }

      public bool GetPageFieldEx(int Index, ref TPDFFieldEx Field)
      {
         TPDFFieldEx_I f = new TPDFFieldEx_I();
         f.StructSize = Marshal.SizeOf(f);
         if (pdfGetPageFieldEx(m_Instance, Index, ref f) == 0) return false;
         GetIntFieldEx(f, ref Field);
         return true;
      }

      public double GetPageHeight()
      {
         return pdfGetPageHeight(m_Instance);
      }

      public bool GetPageLabel(int Index, ref TPDFPageLabel Label)
      {
         TPDFPageLabel_I lbl = new TPDFPageLabel_I();
         if (pdfGetPageLabel(m_Instance, Index, ref lbl) == 0) return false;
         Label.StartRange   = lbl.StartRange;
         Label.Format       = lbl.Format;
         Label.FirstPageNum = lbl.FirstPageNum;
         Label.Prefix       = _Conv.ToString(lbl.Prefix, lbl.PrefixLen, lbl.PrefixUni != 0);
         return true;
      }

      public int GetPageLabelCount()
      {
         return pdfGetPageLabelCount(m_Instance);
      }

      public int GetPageLayout()
      {
         return pdfGetPageLayout(m_Instance);
      }

      public int GetPageMode()
      {
         return pdfGetPageMode(m_Instance);
      }

      public int GetPageNum()
      {
         return pdfGetPageNum(m_Instance);
      }

      public IntPtr GetPageObject(int PageNum)
      {
         return pdfGetPageObject(m_Instance, PageNum);
      }

      public int GetPageOrientation(IntPtr PagePtr)
      {
         return pdfGetPageOrientation(PagePtr);
      }

      public bool GetPageText(ref TPDFStack Stack)
      {
         if (pdfGetPageText(m_Instance, ref Stack) == 0) return false;
         return true;
      }

      public double GetPageWidth()
      {
         return pdfGetPageWidth(m_Instance);
      }

      public IntPtr GetPDFInstance()
      {
         return m_Instance;
      }

      public int GetPDFVersion()
      {
         return pdfGetPDFVersion(m_Instance);
      }

      public bool GetPDFVersionEx(ref TPDFVersionInfo Value)
      {
         TPDFVersionInfo_I v = new TPDFVersionInfo_I();
         v.StructSize = Marshal.SizeOf(v);
         if (pdfGetPDFVersionEx(m_Instance, ref v) != 0)
         {
            Value.Major           = v.Major;
            Value.Minor           = v.Minor;
            Value.MainVer         = _Conv.ToString(v.MainVer, false);
            Value.SubVer          = _Conv.ToString(v.SubVer, false);
            Value.PDFAConformance = _Conv.ToString(v.PDFAConformance, false);
            Value.PDFAVersion     = v.PDFAVersion;
            Value.PDFEVersion     = _Conv.ToString(v.PDFEVersion, false);
            Value.PDFVTModDate    = _Conv.ToString(v.PDFVTModDate, false);
            Value.PDFVTVersion    = _Conv.ToString(v.PDFVTVersion, false);
            Value.PDFXConformance = _Conv.ToString(v.PDFXConformance, false);
            Value.PDFXVersion     = _Conv.ToString(v.PDFXVersion, false);
            Value.FXConfLevel     = _Conv.ToString(v.FXConfLevel, false);
            Value.FXDocName       = _Conv.ToString(v.FXDocName, false);
            Value.FXDocType       = _Conv.ToString(v.FXDocType, false);
            Value.FXVersion       = _Conv.ToString(v.FXVersion, false);
            Value.VersionConst    = v.VersionConst;
            return true;
         }
         return false;
      }

      public bool GetPrintSettings(ref TPDFPrintSettings Settings)
      {
         TPDFPrintSettings_I s = new TPDFPrintSettings_I();
         if (pdfGetPrintSettings(m_Instance, ref s) == 0) return false;
         Settings.DuplexMode        = s.DuplexMode;
         Settings.NumCopies         = s.NumCopies;
         Settings.PickTrayByPDFSize = s.PickTrayByPDFSize;
         Settings.PrintScaling      = s.PrintScaling;
         if (s.PrintRangesCount > 0)
         {
            Settings.PrintRanges = new int[s.PrintRangesCount<<1];
            Marshal.Copy(s.PrintRanges, Settings.PrintRanges, 0, s.PrintRangesCount<<1);
         }
         return true;
      }

      public bool GetPtDataArray(IntPtr PtData, int Index, ref String DataType, ref float[] Values)
      {
         IntPtr dt = IntPtr.Zero, v = IntPtr.Zero;
         int valCount = 0;
         DataType = null;
         Values   = null;
         if (pdfGetPtDataArray(PtData, Index, ref dt, ref v, ref valCount) == 0) return false;
         if (valCount < 2) return false;
         DataType = _Conv.ToString(dt, false);
         Values   = new float[valCount];
         Marshal.Copy(v, Values, 0, valCount);
         return true;
      }

      public bool GetPtDataObj(IntPtr PtData, ref String Subtype, ref int NumArrays)
      {
         IntPtr st = new IntPtr();
         Subtype   = null;
         NumArrays = 0;
         if (pdfGetPtDataObj(PtData, ref st, ref NumArrays) == 0) return false;
         Subtype = _Conv.ToString(st, false);
         return true;
      }

      public bool GetRelFileNode(IntPtr IRF, ref TPDFRelFileNode F, bool Decompress)
      {
         TPDFRelFileNode_I node = new TPDFRelFileNode_I();
         node.StructSize = Marshal.SizeOf(node);
         if (pdfGetRelFileNode(IRF, ref node, Convert.ToInt32(Decompress)) == 0) return false;
         F.Name          = _Conv.ToString(node.NameA, node.NameW);
         F.NextNode      = node.NextNode;
         F.EF.Buffer     = node.EF.Buffer;
         F.EF.BufSize    = node.EF.BufSize;
         F.EF.ColItem    = node.EF.ColItem;
         F.EF.Compressed = node.EF.Compressed != 0;
         F.EF.FileSize   = node.EF.FileSize;
         F.EF.IsURL      = node.EF.IsURL != 0;
         if (node.EF.CheckSum != IntPtr.Zero)
         {
            F.EF.CheckSum = new byte[16];
            Marshal.Copy(node.EF.CheckSum, F.EF.CheckSum, 0, 16);
         }else
            F.EF.CheckSum = null;
         F.EF.CreateDate = _Conv.ToString(node.EF.CreateDate, false);
         F.EF.Desc       = _Conv.ToString(node.EF.Desc, node.EF.DescUnicode != 0);
         F.EF.FileName   = _Conv.ToString(node.EF.FileName, false);
         F.EF.MIMEType   = _Conv.ToString(node.EF.MIMEType, false);
         F.EF.ModDate    = _Conv.ToString(node.EF.ModDate, false);
         F.EF.Name       = _Conv.ToString(node.EF.Name, node.EF.NameUnicode != 0);
         F.EF.UF         = _Conv.ToString(node.EF.UF, node.EF.UFUnicode != 0);
         return true;
      }

      public bool GetResetAction(int Handle, ref TPDFResetFormAction Value)
      {
         TPDFResetFormAction_I v = new TPDFResetFormAction_I();
         v.StructSize = Marshal.SizeOf(v);
         if (pdfGetResetAction(m_Instance, Handle, ref v) != 0)
         {
            Value.Include = v.Include != 0;
            if (v.FieldsCount > 0)
            {
               Value.Fields = new IntPtr[v.FieldsCount];
               Marshal.Copy(v.Fields, Value.Fields, 0, v.FieldsCount);
            }else
               Value.Fields = null;
            return true;
         }else
            return false;
      }

      public int GetResolution()
      {
         return pdfGetResolution(m_Instance);
      }

      public bool GetSaveNewImageFormat()
      {
         return pdfGetSaveNewImageFormat(m_Instance) != 0;
      }

      public bool GetSeparationInfo(ref String Colorant, ref TExtColorSpace CS)
      {
         IntPtr clt = IntPtr.Zero;
         if (pdfGetSeparationInfo(m_Instance, ref clt, ref CS) == 0) return false;
         Colorant = _Conv.ToString(clt, false);
         return true;
      }

      public bool GetSigDict(IntPtr ISignature, ref TPDFSigDict SigDict)
      {
         TPDFSigDict_I sd = new TPDFSigDict_I();
         sd.StructSize = Marshal.SizeOf(sd);
         if (pdfGetSigDict(ISignature, ref sd) == 0) return false;
         GetIntSigDict(sd, ref SigDict);
         return true;
      }

      public double GetSpaceWidth(IntPtr IFont, double FontSize)
      {
         return fntGetSpaceWidth(IFont, FontSize);
      }

      public uint GetStrokeColor()
      {
         return pdfGetStrokeColor(m_Instance);
      }

      public bool GetSubmitAction(int Handle, ref TPDFSubmitFormAction Value)
      {
         TPDFSubmitFormAction_I v = new TPDFSubmitFormAction_I();
         v.StructSize = Marshal.SizeOf(v);
         if (pdfGetSubmitAction(m_Instance, Handle, ref v) != 0)
         {
            Value.CharSet = _Conv.ToString(v.CharSet, false);
            if (v.FieldsCount > 0)
            {
               Value.Fields = new IntPtr[v.FieldsCount];
               Marshal.Copy(v.Fields, Value.Fields, 0, v.FieldsCount);
            }else
               Value.Fields = null;
            Value.Flags = v.Flags;
            Value.URL   = _Conv.ToString(v.URL, false);
            return true;
         }else
            return false;
      }

      public int GetSysFontInfo(int Handle, ref TPDFSysFont Value)
      {
         int retval;
         TPDFSysFont_I f = new TPDFSysFont_I();
         f.StructSize = Marshal.SizeOf(f);
         retval = pdfGetSysFontInfo(m_Instance, Handle, ref f);
         if (retval < 0) return retval;
         if (retval == 0)
         {
            if (IntPtr.Zero.Equals(f.FamilyName))
            {
               Value = new TPDFSysFont();
               return 0;
            }
         }
         Value.BaseType       = f.BaseType;
         Value.CIDOrdering    = _Conv.ToString(f.CIDOrdering, false);
         Value.CIDRegistry    = _Conv.ToString(f.CIDRegistry, false);
         Value.CIDSupplement  = f.CIDSupplement;
         Value.DataOffset     = f.DataOffset;
         Value.FamilyName     = _Conv.ToString(f.FamilyName, true);
         Value.FilePath       = _Conv.ToString(f.FilePathA, f.FilePathW);
         Value.FileSize       = f.FileSize;
         Value.Flags          = f.Flags;
         Value.FullName       = _Conv.ToString(f.FullName, true);
         Value.Index          = f.Index;
         Value.IsFixedPitch   = f.IsFixedPitch != 0;
         Value.Length1        = f.Length1;
         Value.Length2        = f.Length2;
         Value.PostScriptName = _Conv.ToString(f.PostScriptNameA, f.PostScriptNameW);
         Value.Style          = f.Style;
         Value.UnicodeRange1  = f.UnicodeRange1;
         Value.UnicodeRange2  = f.UnicodeRange2;
         Value.UnicodeRange3  = f.UnicodeRange3;
         Value.UnicodeRange4  = f.UnicodeRange4;
         return retval;
      }

      public int GetTabLen()
      {
         return pdfGetTabLen(m_Instance);
      }

      public int GetTemplCount()
      {
         return pdfGetTemplCount(m_Instance);
      }

      public int GetTemplHandle()
      {
         return pdfGetTemplHandle(m_Instance);
      }

      public double GetTemplHeight(int Handle)
      {
         return pdfGetTemplHeight(m_Instance, Handle);
      }

      public double GetTemplWidth(int Handle)
      {
         return pdfGetTemplWidth(m_Instance, Handle);
      }

      public int GetTextDrawMode()
      {
         return pdfGetTextDrawMode(m_Instance);
      }

      public bool GetTextFieldValue(int Field, ref String Value, ref String DefValue)
      {
         IntPtr val = IntPtr.Zero, defVal = IntPtr.Zero;
         lbool valUni = 0, defValUni = 0;
         if (pdfGetTextFieldValue(m_Instance, Field, ref val, ref valUni, ref defVal, ref defValUni) == 0)
         {
            Value = null;
            DefValue = null;
            return false;
         }
         Value    = _Conv.ToString(val, valUni != 0);
         DefValue = _Conv.ToString(defVal, defValUni != 0);
         return true;
      }

      public bool GetTextRect(ref double PosX, ref double PosY, ref double Width, ref double Height)
      {
         return pdfGetTextRect(m_Instance, ref PosX, ref PosY, ref Width, ref Height) != 0;
      }

      public double GetTextRise()
      {
         return pdfGetTextRise(m_Instance);
      }

      public double GetTextScaling()
      {
         return pdfGetTextScaling(m_Instance);
      }

      public double GetTextWidth(IntPtr IFont, IntPtr Text, int Len, float CharSpacing, float WordSpacing, float TextScale)
      {
         return fntGetTextWidth(IFont, Text, Len, CharSpacing, WordSpacing, TextScale);
      }

      public double GetTextWidth(String AText)
      {
         return pdfGetTextWidthW(m_Instance, AText);
      }

      public double GetTextWidthA(String AText)
      {
         return pdfGetTextWidthA(m_Instance, AText);
      }

      public double GetTextWidthW(String AText)
      {
         return pdfGetTextWidthW(m_Instance, AText);
      }

      public double GetTextWidthEx(String AText, int Len)
      {
         return pdfGetTextWidthExW(m_Instance, AText, Len);
      }

      public double GetTextWidthExA(String AText, int Len)
      {
         return pdfGetTextWidthExA(m_Instance, AText, Len);
      }

      public double GetTextWidthExW(String AText, int Len)
      {
         return pdfGetTextWidthExW(m_Instance, AText, Len);
      }

      public uint GetTransparentColor()
      {
         return pdfGetTransparentColor(m_Instance);
      }

      public bool GetTrapped()
      {
         return pdfGetTrapped(m_Instance) != 0;
      }
      
      public double GetTypoLeading()
      {
         return pdfGetTypoLeading(m_Instance);
      }

      public bool GetURIAction(int Handle, ref TPDFURIAction Action)
      {
         TPDFURIAction_I a = new TPDFURIAction_I();
         a.StructSize = Marshal.SizeOf(a);
         if (pdfGetURIAction(m_Instance, Handle, ref a) == 0) return false;
         Action.BaseURL        = _Conv.ToString(a.BaseURL, false);
         Action.IsMap          = a.IsMap != 0;
         Action.NextAction     = a.NextAction;
         Action.NextActionType = a.NextActionType;
         Action.URI            = _Conv.ToString(a.URI, false);
         return true;
      }

      public bool GetUseExactPwd()
      {
         return pdfGetUseExactPwd(m_Instance) != 0;
      }

      public bool GetUseGlobalImpFiles()
      {
         return pdfGetUseGlobalImpFiles(m_Instance) != 0;
      }

      public int GetUserRights()
      {
         return pdfGetUserRights(m_Instance);
      }

      public float GetUserUnit()
      {
         return pdfGetUserUnit(m_Instance);
      }

      public bool GetUseStdFonts()
      {
         return pdfGetUseStdFonts(m_Instance) != 0;
      }

      public bool GetUseSystemFonts()
      {
         return pdfGetUseSystemFonts(m_Instance) != 0;
      }

      public int GetUsesTransparency(int PageNum)
      {
         return pdfGetUsesTransparency(m_Instance, PageNum);
      }

      public bool GetUseTransparency()
      {
         return pdfGetUseTransparency(m_Instance) != 0;
      }

      public bool GetUseVisibleCoords()
      {
         return pdfGetUseVisibleCoords(m_Instance) != 0;
      }

      public bool GetViewerPreferences(ref int Preference, ref int AddVal)
      {
         return pdfGetViewerPreferences(m_Instance, ref Preference, ref AddVal) != 0;
      }

      public bool GetViewport(int PageNum, int Index, ref TPDFViewport Value)
      {
         TPDFViewport_I vp = new TPDFViewport_I();
         vp.StructSize = Marshal.SizeOf(vp);
         if (pdfGetViewport(m_Instance, PageNum, Index, ref vp) == 0) return false;
         Value.BBox    = vp.BBox;
         Value.Measure = vp.Measure;
         Value.Name    = _Conv.ToString(vp.NameA, vp.NameW);
         Value.PtData  = vp.PtData;
         return true;
      }

      public int GetViewportCount(int PageNum)
      {
         return pdfGetViewportCount(m_Instance, PageNum);
      }

      public bool GetWidthHeight(IntPtr PagePtr, TRasterFlags Flags, ref float Width, ref float Height, int Rotate, ref TFltRect BBox)
      {
         IntPtr v = IntPtr.Zero;
         if (rasGetWidthHeight(PagePtr, Flags, ref Width, ref Height, Rotate, ref v) != 0)
         {
            BBox = (TFltRect)Marshal.PtrToStructure(v, typeof(TFltRect));
            return true;
         }
         return false;
      }

      public bool GetWMFDefExtent(ref int Width, ref int Height)
      {
         return pdfGetWMFDefExtent(m_Instance, ref Width, ref Height) != 0;
      }

      public int GetWMFPixelPerInch()
      {
         return pdfGetWMFPixelPerInch(m_Instance);
      }

      public double GetWordSpacing()
      {
         return pdfGetWordSpacing(m_Instance);
      }

      public bool GetXFAStream(int Index, TPDFXFAStream Out)
      {
         TPDFXFAStream_I strm = new TPDFXFAStream_I();
         strm.StructSize = Marshal.SizeOf(strm);
         if (Out == null)
            return pdfGetXFAStream(m_Instance, Index, null) != 0;
         else if (pdfGetXFAStream(m_Instance, Index, strm) != 0)
         {
            Out.Buffer = new byte[strm.BufSize];
            Marshal.Copy(strm.Buffer, Out.Buffer, 0, strm.BufSize);
            Out.Name = _Conv.ToString(strm.NameA, strm.NameW);
            return true;
         }else
         {
            Out.Buffer = null;
            Out.Name   = "";
            return false;
         }
      }

      public int GetXFAStreamCount()
      {
         return pdfGetXFAStreamCount(m_Instance);
      }

      public bool HaveDPartRoot()
      {
         return pdfHaveDPartRoot(m_Instance) != 0;
      }

      public bool HaveOpenDoc()
      {
         return pdfHaveOpenDoc(m_Instance) != 0;
      }

      public bool HaveOpenPage()
      {
         return pdfHaveOpenPage(m_Instance) != 0;
      }

      public int HighlightAnnot(TAnnotType SubType, double PosX, double PosY, double Width, double Height, uint Color, String Author, String Subject, String Comment)
      {
         return pdfHighlightAnnotW(m_Instance, SubType, PosX, PosY, Width, Height, Color, Author, Subject, Comment);
      }

      public int HighlightAnnotA(TAnnotType SubType, double PosX, double PosY, double Width, double Height, uint Color, String Author, String Subject, String Comment)
      {
         return pdfHighlightAnnotA(m_Instance, SubType, PosX, PosY, Width, Height, Color, Author, Subject, Comment);
      }

      public int HighlightAnnotW(TAnnotType SubType, double PosX, double PosY, double Width, double Height, uint Color, String Author, String Subject, String Comment)
      {
         return pdfHighlightAnnotW(m_Instance, SubType, PosX, PosY, Width, Height, Color, Author, Subject, Comment);
      }

      public int ImportBookmarks()
      {
         return pdfImportBookmarks(m_Instance);
      }

      public bool ImportCatalogObjects()
      {
         return pdfImportCatalogObjects(m_Instance) != 0;
      }

      public bool ImportDocInfo()
      {
         return pdfImportDocInfo(m_Instance) != 0;
      }

      public bool ImportEncryptionSettings()
      {
         return pdfImportEncryptionSettings(m_Instance) != 0;
      }

      public bool ImportOCProperties()
      {
         return pdfImportOCProperties(m_Instance) != 0;
      }

      public int ImportPage(int PageNum)
      {
         return pdfImportPage(m_Instance, PageNum);
      }

      public int ImportPageEx(int PageNum, double ScaleX, double ScaleY)
      {
         return pdfImportPageEx(m_Instance, PageNum, ScaleX, ScaleY);
      }

      public int ImportPDFFile(int DestPage, double ScaleX, double ScaleY)
      {
         return pdfImportPDFFile(m_Instance, DestPage, ScaleX, ScaleY);
      }

      public bool InitBarcode2(ref TPDFBarcode2 Barcode)
      {
         Barcode.StructSize = Marshal.SizeOf(Barcode);
         return pdfInitBarcode2(ref Barcode) != 0;
      }

      public bool InitColorManagement(TPDFColorProfiles Profiles, TPDFColorSpace DestSpace, TPDFInitCMFlags Flags)
      {
         if (Profiles != null) Profiles.StructSize = Marshal.SizeOf(Profiles);
         return pdfInitColorManagement(m_Instance, Profiles, DestSpace, Flags) != 0;
      }

      public bool InitColorManagementEx(TPDFColorProfilesEx Profiles, TPDFColorSpace DestSpace, TPDFInitCMFlags Flags)
      {
         if (Profiles != null) Profiles.StructSize = Marshal.SizeOf(Profiles);
         return pdfInitColorManagementEx(m_Instance, Profiles, DestSpace, Flags) != 0;
      }

      public bool InitExtGState(ref TPDFExtGState GS)
      {
         return pdfInitExtGState(ref GS) != 0;
      }

      public void InitHeaderFooter(ref TPDFHeaderFooter Value, ref TPDFHdrFtr[] HFArray)
      {
         TPDFHeaderFooter_I val = new TPDFHeaderFooter_I();
         TPDFHdrFtr_I[] hf = new TPDFHdrFtr_I[HFArray.Length];

         val.StructSize = Marshal.SizeOf(val);

         if (pdfInitHeaderFooter(ref val, hf, hf.Length) != 0)
            ConvertHeaderFooter(ref Value, ref HFArray, ref val, ref hf);
      }

      public bool InitOCGContUsage(ref TPDFOCGContUsage Value)
      {
         Value.StructSize = Marshal.SizeOf(Value);
         return pdfInitOCGContUsage(ref Value) != 0;
      }

      public bool InitStack(ref TPDFStack Stack)
      {
         if (pdfInitStack(m_Instance, ref Stack) == 0) return false;
         return true;
      }

      public int InkAnnot(TFltPoint[] Points, double LineWidth, uint Color, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfInkAnnotW(m_Instance, Points, Points.Length, LineWidth, Color, CS, Author, Subject, Content);
      }

      public int InkAnnotA(TFltPoint[] Points, double LineWidth, uint Color, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfInkAnnotA(m_Instance, Points, Points.Length, LineWidth, Color, CS, Author, Subject, Content);
      }

      public int InkAnnotW(TFltPoint[] Points, double LineWidth, uint Color, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfInkAnnotW(m_Instance, Points, Points.Length, LineWidth, Color, CS, Author, Subject, Content);
      }

      public int InsertBarcode(double PosX, double PosY, double Width, double Height, TCellAlign HAlign, TCellAlign VAlign, ref TPDFBarcode2 Barcode)
      {
         return pdfInsertBarcode(m_Instance, PosX, PosY, Width, Height, HAlign, VAlign, ref Barcode);
      }

      public int InsertBMPFromBuffer(double PosX, double PosY, double ScaleWidth, double ScaleHeight, byte[] Buffer)
      {
         return pdfInsertImageFromBuffer(m_Instance, PosX, PosY, ScaleWidth, ScaleHeight, Buffer, Buffer.Length, 0);
      }

      public int InsertBMPFromHandle(double PosX, double PosY, double ScaleWidth, double ScaleHeight, IntPtr hBitmap)
      {
         return pdfInsertBMPFromHandle(m_Instance, PosX, PosY, ScaleWidth, ScaleHeight, hBitmap);
      }

      public int InsertBookmark(String Title, int Parent, int DestPage, bool Open, bool AddChildren)
      {
         return pdfInsertBookmarkW(m_Instance, Title, Parent, DestPage, Convert.ToInt32(Open), Convert.ToInt32(AddChildren));
      }

      public int InsertBookmarkA(String Title, int Parent, int DestPage, bool Open, bool AddChildren)
      {
         return pdfInsertBookmarkA(m_Instance, Title, Parent, DestPage, Convert.ToInt32(Open), Convert.ToInt32(AddChildren));
      }

      public int InsertBookmarkW(String Title, int Parent, int DestPage, bool Open, bool AddChildren)
      {
         return pdfInsertBookmarkW(m_Instance, Title, Parent, DestPage, Convert.ToInt32(Open), Convert.ToInt32(AddChildren));
      }

      public int InsertBookmarkEx(String Title, int Parent, int NamedDest, bool Open, bool AddChildren)
      {
         return pdfInsertBookmarkExW(m_Instance, Title, Parent, NamedDest, Convert.ToInt32(Open), Convert.ToInt32(AddChildren));
      }

      public int InsertBookmarkExA(String Title, int Parent, int NamedDest, bool Open, bool AddChildren)
      {
         return pdfInsertBookmarkExA(m_Instance, Title, Parent, NamedDest, Convert.ToInt32(Open), Convert.ToInt32(AddChildren));
      }

      public int InsertBookmarkExW(String Title, int Parent, int NamedDest, bool Open, bool AddChildren)
      {
         return pdfInsertBookmarkExW(m_Instance, Title, Parent, NamedDest, Convert.ToInt32(Open), Convert.ToInt32(AddChildren));
      }

      public int InsertImage(double PosX, double PosY, double ScaleWidth, double ScaleHeight, String AFile)
      {
         return pdfInsertImage(m_Instance, PosX, PosY, ScaleWidth, ScaleHeight, AFile);
      }

      public int InsertImageEx(double PosX, double PosY, double ScaleWidth, double ScaleHeight, String AFile, int Index)
      {
         return pdfInsertImageEx(m_Instance, PosX, PosY, ScaleWidth, ScaleHeight, AFile, Index);
      }

      public int InsertImageFromBuffer(double PosX, double PosY, double ScaleWidth, double ScaleHeight, byte[] Buffer, int Index)
      {
         return pdfInsertImageFromBuffer(m_Instance, PosX, PosY, ScaleWidth, ScaleHeight, Buffer, Buffer.Length, Index);
      }

      public int InsertImageFromBuffer(double PosX, double PosY, double ScaleWidth, double ScaleHeight, IntPtr Buffer, int Length, int Index)
      {
         return pdfInsertImageFromBuffer2(m_Instance, PosX, PosY, ScaleWidth, ScaleHeight, Buffer, Length, Index);
      }

      public bool InsertMetafile(String FileName, double PosX, double PosY, double Width, double Height)
      {
         return pdfInsertMetafile(m_Instance, FileName, PosX, PosY, Width, Height) != 0;
      }

      public bool InsertMetafileEx(byte[] Buffer, double PosX, double PosY, double Width, double Height)
      {
         return pdfInsertMetafileEx(m_Instance, Buffer, Buffer.Length, PosX, PosY, Width, Height) != 0;
      }

      public bool InsertMetafileExt(String FileName, ref TRectL View, double PosX, double PosY, double Width, double Height)
      {
         return pdfInsertMetafileExt(m_Instance, FileName, ref View, PosX, PosY, Width, Height) != 0;
      }

      public bool InsertMetafileExtEx(byte[] Buffer, ref TRectL View, double PosX, double PosY, double Width, double Height)
      {
         return pdfInsertMetafileExtEx(m_Instance, Buffer, Buffer.Length, ref View, PosX, PosY, Width, Height) != 0;
      }

      public bool InsertMetafileFromHandle(IntPtr hEnhMetafile, double PosX, double PosY, double Width, double Height)
      {
         return pdfInsertMetafileFromHandle(m_Instance, hEnhMetafile, PosX, PosY, Width, Height) != 0;
      }

      public bool InsertMetafileFromHandleEx(IntPtr hEnhMetafile, ref TRectL View, double PosX, double PosY, double Width, double Height)
      {
         return pdfInsertMetafileFromHandleEx(m_Instance, hEnhMetafile, ref View, PosX, PosY, Width, Height) != 0;
      }

      public int InsertRawImage(byte[] Buffer, int BitsPerPixel, int ColorCount, int ImgWidth, int ImgHeight, double PosX, double PosY, double ScaleWidth, double ScaleHeight)
      {
         return pdfInsertRawImage(m_Instance, Buffer, BitsPerPixel, ColorCount, ImgWidth, ImgHeight, PosX, PosY, ScaleWidth, ScaleHeight);
      }

      public int InsertRawImage(IntPtr Buffer, int BitsPerPixel, int ColorCount, int ImgWidth, int ImgHeight, double PosX, double PosY, double ScaleWidth, double ScaleHeight)
      {
         return pdfInsertRawImage(m_Instance, Buffer, BitsPerPixel, ColorCount, ImgWidth, ImgHeight, PosX, PosY, ScaleWidth, ScaleHeight);
      }

      public int InsertRawImageEx(double PosX, double PosY, double ScaleWidth, double ScaleHeight, TPDFRawImage Image)
      {
         Image.StructSize = Marshal.SizeOf(Image);
         return pdfInsertRawImageEx(m_Instance, PosX, PosY, ScaleWidth, ScaleHeight, ref Image);
      }

      public int IsBidiText(String AText)
      {
         return pdfIsBidiText(m_Instance, AText);
      }

      public bool IsColorPage(bool GrayIsColor)
      {
         return pdfIsColorPage(m_Instance, Convert.ToInt32(GrayIsColor)) != 0;
      }

      public bool IsEmptyPage()
      {
         return pdfIsEmptyPage(m_Instance) != 0;
      }

      public int LineAnnot(double x1, double y1, double x2, double y2, double LineWidth, TLineEndStyle Start, TLineEndStyle End, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfLineAnnotW(m_Instance, x1, y1, x2, y2, LineWidth, Start, End, FillColor, StrokeColor, CS, Author, Subject, Content);
      }

      public int LineAnnotA(double x1, double y1, double x2, double y2, double LineWidth, TLineEndStyle Start, TLineEndStyle End, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfLineAnnotA(m_Instance, x1, y1, x2, y2, LineWidth, Start, End, FillColor, StrokeColor, CS, Author, Subject, Content);
      }

      public int LineAnnotW(double x1, double y1, double x2, double y2, double LineWidth, TLineEndStyle Start, TLineEndStyle End, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfLineAnnotW(m_Instance, x1, y1, x2, y2, LineWidth, Start, End, FillColor, StrokeColor, CS, Author, Subject, Content);
      }

      public bool IsWrongPwd(int ErrCode)
      {
         switch (-ErrCode)
         {
            case (int)TErrType.E_NEED_PWD:
            case (int)TErrType.E_WRONG_OPEN_PWD:
            case (int)TErrType.E_WRONG_OWNER_PWD:
            case (int)TErrType.E_WRONG_PWD:
               return true;
         }
         return false;
      }

      public bool LineTo(double PosX, double PosY)
      {
         return pdfLineTo(m_Instance, PosX, PosY) != 0;
      }

      public int LoadCMap(String CMapName, bool Embed)
      {
         return pdfLoadCMap(m_Instance, CMapName, Convert.ToInt32(Embed));
      }

      public bool LoadFDFData(String FileName, String Password, int Flags)
      {
         return pdfLoadFDFDataW(m_Instance, FileName, Password, Flags) != 0;
      }

      public bool LoadFDFDataA(String FileName, String Password, int Flags)
      {
         return pdfLoadFDFDataA(m_Instance, FileName, Password, Flags) != 0;
      }

      public bool LoadFDFDataW(String FileName, String Password, int Flags)
      {
         return pdfLoadFDFDataW(m_Instance, FileName, Password, Flags) != 0;
      }

      public bool LoadFDFDataEx(byte[] Buffer, String Password, int Flags)
      {
         return pdfLoadFDFDataEx(m_Instance, Buffer, Buffer.Length, Password, Flags) != 0;
      }

      public int LoadFont(byte[] Buffer, TFStyle Style, double Size, bool Embed, TCodepage CP)
      {
         return pdfLoadFont(m_Instance, Buffer, Buffer.Length, Style, Size, Convert.ToInt32(Embed), CP);
      }

      public int LoadFontEx(String FontFile, int Index, TFStyle Style, double Size, bool Embed, TCodepage CP)
      {
         return pdfLoadFontEx(m_Instance, FontFile, Index, Style, Size, Convert.ToInt32(Embed), CP);
      }

      public int LoadHeaderFooterSettings(ref TPDFHeaderFooter Value, ref TPDFHdrFtr[] HFArray)
      {
         int retval;
         TPDFHeaderFooter_I val = new TPDFHeaderFooter_I();
         TPDFHdrFtr_I[]      hf = new TPDFHdrFtr_I[6];

         if (HFArray.Length != 6) return 0;

         val.StructSize = Marshal.SizeOf(val);

         retval = pdfLoadHeaderFooterSettings(m_Instance, ref val, hf);

         if (retval > 0)
         {
            Array.Resize<TPDFHdrFtr>(ref HFArray, retval);
            ConvertHeaderFooter(ref Value, ref HFArray, ref val, ref hf);
         }else
            HFArray = null;

         return retval;
      }

      public bool LoadLayerConfig(int Index)
      {
         return pdfLoadLayerConfig(m_Instance, Index) != 0;
      }

      public bool LockLayer(int Layer)
      {
         return pdfLockLayer(m_Instance, Layer) != 0;
      }

      public bool MarkTemplateAsWatermark(int Handle)
      {
         return pdfMarkTemplateAsWatermark(m_Instance, Handle) != 0;
      }

      public void MemCopy(IntPtr Source, ref byte[] Dest, int Len)
      {
         if (Len <= 0)
         {
            Dest = null;
            return;
         }
         Dest = new Byte[Len];
         System.Runtime.InteropServices.Marshal.Copy(Source, Dest, 0, Len);
      }

      public bool MovePage(int Source, int Dest)
      {
         return pdfMovePage(m_Instance, Source, Dest) != 0;
      }

      public bool MoveTo(double PosX, double PosY)
      {
         return pdfMoveTo(m_Instance, PosX, PosY) != 0;
      }

      public int MultiplyMatrix(ref TCTM M1, ref TCTM M2, ref TCTM NewMatrix)
      {
         return pdfMultiplyMatrix(ref M1, ref M2, ref NewMatrix);
      }

      public int OpenImportBuffer(byte[] Buffer, TPwdType PwdType, String Password)
      {
         return pdfOpenImportBuffer(m_Instance, Buffer, Buffer.Length, PwdType, Password);
      }

      public int OpenImportFile(String FileName, TPwdType PwdType, String Password)
      {
         return pdfOpenImportFileW(m_Instance, FileName, PwdType, Password);
      }

      public int OpenImportFileA(String FileName, TPwdType PwdType, String Password)
      {
         return pdfOpenImportFileA(m_Instance, FileName, PwdType, Password);
      }

      public int OpenImportFileW(String FileName, TPwdType PwdType, String Password)
      {
         return pdfOpenImportFileW(m_Instance, FileName, PwdType, Password);
      }

      public bool OpenOutputFile(String OutPDF)
      {
         return pdfOpenOutputFile(m_Instance, OutPDF) != 0;
      }

      public bool OpenOutputFileEncrypted(String OutPDF, String OpenPwd, String OwnerPwd, TKeyLen KeyLen, TRestrictions Restrict)
      {
         return pdfOpenOutputFileEncrypted(m_Instance, OutPDF, OpenPwd, OwnerPwd, KeyLen, Restrict) != 0;
      }

      public bool OpenTag(TPDFBaseTag Tag, String Lang, String AltText, String Expansion)
      {
         return pdfOpenTagW(m_Instance, Tag, Lang, AltText, Expansion) != 0;
      }

      public bool OpenTagBBox(TPDFBaseTag Tag, String Lang, String AltText, String Expansion, ref TBBox BBox)
      {
         return pdfOpenTagBBoxW(m_Instance, Tag, Lang, AltText, Expansion, ref BBox) != 0;
      }

      public bool OpenTagEx(TPDFBaseTag Tag, String Lang, String AltText, String Expansion, String Attributes)
      {
         return pdfOpenTagExW(m_Instance, Tag, Lang, AltText, Expansion, Attributes) != 0;
      }

      public bool Optimize(TOptimizeFlags Flags, TOptimizeParams Parms)
      {
         bool retval = false;
         TOptimizeParams_I p = null;
         try
         {
            GetOptimizeParams(Parms, false, ref p);
            retval = pdfOptimize(m_Instance, Flags, p) != 0;
         }finally
         {
            if (p != null && p.ExcludeCS != IntPtr.Zero)
               Marshal.FreeHGlobal(p.ExcludeCS);
         }
         return retval;
      }

      public bool Optimize(TOptimizeFlags Flags, TOptimizeParams Parms, bool IncludeCSArray)
      {
         bool retval = false;
         TOptimizeParams_I p = null;
         try
         {
            GetOptimizeParams(Parms, IncludeCSArray, ref p);
            retval = pdfOptimize(m_Instance, Flags, p) != 0;
         }finally
         {
            if (p != null && p.ExcludeCS != IntPtr.Zero)
               Marshal.FreeHGlobal(p.ExcludeCS);
         }
         return retval;
      }

      public int PageLink(double PosX, double PosY, double Width, double Height, int DestPage)
      {
         return pdfPageLink(m_Instance, PosX, PosY, Width, Height, DestPage);
      }

      public int PageLink2(double PosX, double PosY, double Width, double Height, int NamedDest)
      {
         return pdfPageLink2(m_Instance, PosX, PosY, Width, Height, NamedDest);
      }

      public int PageLink3(double PosX, double PosY, double Width, double Height, String NamedDest)
      {
         return pdfPageLink3W(m_Instance, PosX, PosY, Width, Height, NamedDest);
      }

      public int PageLink3A(double PosX, double PosY, double Width, double Height, String NamedDest)
      {
         return pdfPageLink3A(m_Instance, PosX, PosY, Width, Height, NamedDest);
      }

      public int PageLink3W(double PosX, double PosY, double Width, double Height, String NamedDest)
      {
         return pdfPageLink3W(m_Instance, PosX, PosY, Width, Height, NamedDest);
      }

      public int PageLinkEx(double PosX, double PosY, double Width, double Height, TDestType DestType, int DestPage, double a, double b, double c, double d)
      {
         return pdfPageLinkEx(m_Instance, PosX, PosY, Width, Height, DestType, DestPage, a, b, c, d);
      }

      public bool ParseContent(IntPtr Data, ref TPDFParseInterface Stack, TParseFlags Flags)
      {
         return pdfParseContent(m_Instance, Data, ref Stack, Flags) != 0;
      }

      public bool PlaceImage(int ImgHandle, double PosX, double PosY, double ScaleWidth, double ScaleHeight)
      {
         return pdfPlaceImage(m_Instance, ImgHandle, PosX, PosY, ScaleWidth, ScaleHeight) != 0;
      }

      public bool PlaceSigFieldValidateIcon(int SigField, double PosX, double PosY, double Width, double Height)
      {
         return pdfPlaceSigFieldValidateIcon(m_Instance, SigField, PosX, PosY, Width, Height) != 0;
      }

      public bool PlaceTemplate(int TmplHandle, double PosX, double PosY, double ScaleWidth, double ScaleHeight)
      {
         return pdfPlaceTemplate(m_Instance, TmplHandle, PosX, PosY, ScaleWidth, ScaleHeight) != 0;
      }

      public bool PlaceTemplateEx(int TmplHandle, double PosX, double PosY, double ScaleWidth, double ScaleHeight)
      {
         return pdfPlaceTemplateEx(m_Instance, TmplHandle, PosX, PosY, ScaleWidth, ScaleHeight) != 0;
      }

      public bool PlaceTemplByMatrix(int TmplHandle)
      {
         return pdfPlaceTemplByMatrix(m_Instance, TmplHandle) != 0;
      }

      public int PolygonAnnot(TFltPoint[] Vertices, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfPolygonAnnotW(m_Instance, Vertices, Vertices.Length, LineWidth, FillColor, StrokeColor, CS, Author, Subject, Content);
      }

      public int PolygonAnnotA(TFltPoint[] Vertices, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfPolygonAnnotA(m_Instance, Vertices, Vertices.Length, LineWidth, FillColor, StrokeColor, CS, Author, Subject, Content);
      }

      public int PolygonAnnotW(TFltPoint[] Vertices, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfPolygonAnnotW(m_Instance, Vertices, Vertices.Length, LineWidth, FillColor, StrokeColor, CS, Author, Subject, Content);
      }

      public int PolyLineAnnot(TFltPoint[] Vertices, double LineWidth, TLineEndStyle Start, TLineEndStyle End, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfPolyLineAnnotW(m_Instance, Vertices, Vertices.Length, LineWidth, Start, End, FillColor, StrokeColor, CS, Author, Subject, Content);
      }

      public int PolyLineAnnotA(TFltPoint[] Vertices, double LineWidth, TLineEndStyle Start, TLineEndStyle End, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfPolyLineAnnotA(m_Instance, Vertices, Vertices.Length, LineWidth, Start, End, FillColor, StrokeColor, CS, Author, Subject, Content);
      }

      public int PolyLineAnnotW(TFltPoint[] Vertices, double LineWidth, TLineEndStyle Start, TLineEndStyle End, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content)
      {
         return pdfPolyLineAnnotW(m_Instance, Vertices, Vertices.Length, LineWidth, Start, End, FillColor, StrokeColor, CS, Author, Subject, Content);
      }

      public bool PrintPage(int PageNum, String DocName, IntPtr DC, TPDFPrintFlags Flags, ref TRectL Margin)
      {
         return pdfPrintPage(m_Instance, PageNum, DocName, DC, Flags, ref Margin, null) != 0;
      }

      public bool PrintPage(int PageNum, String DocName, IntPtr DC, TPDFPrintFlags Flags, ref TRectL Margin, TPDFPrintParams Parms)
      {
         return pdfPrintPage(m_Instance, PageNum, DocName, DC, Flags, ref Margin, Parms) != 0;
      }

      public bool PrintPDFFile(String TmpDir, String DocName, IntPtr DC, TPDFPrintFlags Flags, ref TRectL Margin)
      {
         return pdfPrintPDFFile(m_Instance, TmpDir, DocName, DC, Flags, ref Margin, null) != 0;
      }

      public bool PrintPDFFile(String TmpDir, String DocName, IntPtr DC, TPDFPrintFlags Flags, ref TRectL Margin, TPDFPrintParams Parms)
      {
         return pdfPrintPDFFile(m_Instance, TmpDir, DocName, DC, Flags, ref Margin, Parms) != 0;
      }

      public bool ReadImageFormat2(String FileName, int Index, ref int Width, ref int Height, ref int BitsPerPixel, ref int UseZip)
      {
         return pdfReadImageFormat2(m_Instance, FileName, Index, ref Width, ref Height, ref BitsPerPixel, ref UseZip) != 0;
      }

      public bool ReadImageFormatEx(IntPtr hBitmap, ref int Width, ref int Height, ref int BitsPerPixel, ref int UseZip)
      {
         return pdfReadImageFormatEx(m_Instance, hBitmap, ref Width, ref Height, ref BitsPerPixel, ref UseZip) != 0;
      }

      public bool ReadImageFormatFromBuffer(byte[] Buffer, int Index, ref int Width, ref int Height, ref int BitsPerPixel, ref int UseZip)
      {
         return pdfReadImageFormatFromBuffer(m_Instance, Buffer, Buffer.Length, Index, ref Width, ref Height, ref BitsPerPixel, ref UseZip) != 0;
      }

      public bool ReadImageResolution(String FileName, int Index, ref int ResX, ref int ResY)
      {
         return pdfReadImageResolution(m_Instance, FileName, Index, ref ResX, ref ResY) != 0;
      }

      public bool ReadImageResolutionEx(byte[] Buffer, int Index, ref int ResX, ref int ResY)
      {
         return pdfReadImageResolutionEx(m_Instance, Buffer, Buffer.Length, Index, ref ResX, ref ResY) != 0;
      }

      public bool Rectangle(double PosX, double PosY, double Width, double Height, TPathFillMode FillMode)
      {
         return pdfRectangle(m_Instance, PosX, PosY, Width, Height, FillMode) != 0;
      }

      public void Redraw(IntPtr RasPtr, IntPtr DC, int DestX, int DestY)
      {
         rasRedraw(RasPtr, DC, DestX, DestY);
      }

      public int ReEncryptPDF(String FileName, TPwdType PwdType, String InPwd, String NewOpenPwd, String NewOwnerPwd, TKeyLen NewKeyLen, TRestrictions Restrict)
      {
         return pdfReEncryptPDFW(m_Instance, FileName, PwdType, InPwd, NewOpenPwd, NewOwnerPwd, NewKeyLen, Restrict);
      }

      public int ReEncryptPDFA(String FileName, TPwdType PwdType, String InPwd, String NewOpenPwd, String NewOwnerPwd, TKeyLen NewKeyLen, TRestrictions Restrict)
      {
         return pdfReEncryptPDFA(m_Instance, FileName, PwdType, InPwd, NewOpenPwd, NewOwnerPwd, NewKeyLen, Restrict);
      }

      public int ReEncryptPDFW(String FileName, TPwdType PwdType, String InPwd, String NewOpenPwd, String NewOwnerPwd, TKeyLen NewKeyLen, TRestrictions Restrict)
      {
         return pdfReEncryptPDFW(m_Instance, FileName, PwdType, InPwd, NewOpenPwd, NewOwnerPwd, NewKeyLen, Restrict);
      }

      public int RenameSpotColor(String Colorant, String NewName)
      {
         return pdfRenameSpotColor(m_Instance, Colorant, NewName);
      }

      public int RenderAnnotOrField(int Handle, bool IsAnnot, TButtonState State, ref TCTM Matrix, TRasterFlags Flags, TPDFPixFormat PixFmt, TCompressionFilter Filter, ref TPDFBitmap Out)
      {
         Out.StructSize = Marshal.SizeOf(Out);
         return pdfRenderAnnotOrField(m_Instance, Handle, Convert.ToInt32(IsAnnot), State, ref Matrix, Flags, PixFmt, Filter, ref Out);
      }

      public bool RenderPage(IntPtr PagePtr, IntPtr RasPtr, ref TPDFRasterImage Img)
      {
         return pdfRenderPage(m_Instance, PagePtr, RasPtr, ref Img) != 0;
      }

      public bool RenderPageEx(IntPtr DC, ref int DestX, ref int DestY, IntPtr PagePtr, IntPtr RasPtr, ref TPDFRasterImage Img)
      {
         return pdfRenderPageEx(m_Instance, DC, ref DestX, ref DestY, PagePtr, RasPtr, ref Img) != 0;
      }

      public bool RenderPageToImage(int PageNum, String OutFile, int Resolution, int Width, int Height, TRasterFlags Flags, TPDFPixFormat PixFmt, TCompressionFilter Filter, TImageFormat Format)
      {
         return pdfRenderPageToImage(m_Instance, PageNum, OutFile, Resolution, Width, Height, Flags, PixFmt, Filter, Format) != 0;
      }

      public bool RenderPDFFile(String OutFile, int Resolution, TRasterFlags Flags, TPDFPixFormat PixFmt, TCompressionFilter Filter, TImageFormat Format)
      {
         return pdfRenderPDFFileW(m_Instance, OutFile, Resolution, Flags, PixFmt, Filter, Format) != 0;
      }

      public bool RenderPDFFileA(String OutFile, int Resolution, TRasterFlags Flags, TPDFPixFormat PixFmt, TCompressionFilter Filter, TImageFormat Format)
      {
         return pdfRenderPDFFileA(m_Instance, OutFile, Resolution, Flags, PixFmt, Filter, Format) != 0;
      }

      public bool RenderPDFFileW(String OutFile, int Resolution, TRasterFlags Flags, TPDFPixFormat PixFmt, TCompressionFilter Filter, TImageFormat Format)
      {
         return pdfRenderPDFFileW(m_Instance, OutFile, Resolution, Flags, PixFmt, Filter, Format) != 0;
      }

      public bool RenderPDFFileEx(String OutFile, int Resolution, int Width, int Height, TRasterFlags Flags, TPDFPixFormat PixFmt, TCompressionFilter Filter, TImageFormat Format)
      {
         return pdfRenderPDFFileEx(m_Instance, OutFile, Resolution, Width, Height, Flags, PixFmt, Filter, Format) != 0;
      }

      public bool ReOpenImportFile(int Handle)
      {
         return pdfReOpenImportFile(m_Instance, Handle) != 0;
      }

      public int ReplaceFont(IntPtr PDFFont, String Name, TFStyle Style, bool Embed)
      {
         return pdfReplaceFontW(m_Instance, PDFFont, Name, Style, Convert.ToInt32(Embed));
      }

      public int ReplaceFontA(IntPtr PDFFont, String Name, TFStyle Style, bool Embed)
      {
         return pdfReplaceFontA(m_Instance, PDFFont, Name, Style, Convert.ToInt32(Embed));
      }

      public int ReplaceFontW(IntPtr PDFFont, String Name, TFStyle Style, bool Embed)
      {
         return pdfReplaceFontW(m_Instance, PDFFont, Name, Style, Convert.ToInt32(Embed));
      }

      public int ReplaceFontEx(IntPtr PDFFont, String FontFile, bool Embed)
      {
         return pdfReplaceFontExW(m_Instance, PDFFont, FontFile, Convert.ToInt32(Embed));
      }

      public int ReplaceFontExA(IntPtr PDFFont, String FontFile, bool Embed)
      {
         return pdfReplaceFontExA(m_Instance, PDFFont, FontFile, Convert.ToInt32(Embed));
      }

      public int ReplaceFontExW(IntPtr PDFFont, String FontFile, bool Embed)
      {
         return pdfReplaceFontExW(m_Instance, PDFFont, FontFile, Convert.ToInt32(Embed));
      }

      public int ReplaceICCProfile(int ColorSpace, String ICCFile)
      {
         return pdfReplaceICCProfileW(m_Instance, ColorSpace, ICCFile);
      }

      public int ReplaceICCProfileA(int ColorSpace, String ICCFile)
      {
         return pdfReplaceICCProfileA(m_Instance, ColorSpace, ICCFile);
      }

      public int ReplaceICCProfileW(int ColorSpace, String ICCFile)
      {
         return pdfReplaceICCProfileW(m_Instance, ColorSpace, ICCFile);
      }

      public int ReplaceICCProfileEx(int ColorSpace, byte[] Buffer)
      {
         return pdfReplaceICCProfileEx(m_Instance, ColorSpace, Buffer, Buffer.Length);
      }

      public bool ReplaceImage(IntPtr Source, String Image, int Index, TExtColorSpace CS, int CSHandle, TReplaceImageFlags Flags)
      {
         return pdfReplaceImage(m_Instance, Source, Image, Index, CS, CSHandle, Flags) != 0;
      }

      public bool ReplaceImageEx(IntPtr Source, byte[] Buffer, int Index, TExtColorSpace CS, int CSHandle, TReplaceImageFlags Flags)
      {
         return pdfReplaceImageEx(m_Instance, Source, Buffer, Buffer.Length, Index, CS, CSHandle, Flags) != 0;
      }

      public bool ReplacePageTextA(String NewText, ref TPDFStack Stack)
      {
         return pdfReplacePageTextA(m_Instance, NewText, ref Stack) != 0;
      }

      public bool ReplacePageTextEx(String NewText, ref TPDFStack Stack)
      {
         return pdfReplacePageTextExW(m_Instance, NewText, ref Stack) != 0;
      }

      public bool ReplacePageTextExA(String NewText, ref TPDFStack Stack)
      {
         return pdfReplacePageTextExA(m_Instance, NewText, ref Stack) != 0;
      }

      public bool ReplacePageTextExW(String NewText, ref TPDFStack Stack)
      {
         return pdfReplacePageTextExW(m_Instance, NewText, ref Stack) != 0;
      }

      public bool ResetAnnotAP(int Handle)
      {
         return pdfResetAnnotAP(m_Instance, Handle) != 0;
      }

      public bool ResetEncryptionSettings()
      {
         return pdfResetEncryptionSettings(m_Instance) != 0;
      }

      public bool ResetLineDashPattern()
      {
         return pdfResetLineDashPattern(m_Instance) != 0;
      }

      public bool ResizeBitmap(IntPtr RasPtr, IntPtr DC, int Width, int Height)
      {
         return rasResizeBitmap(RasPtr, DC, Width, Height) != 0;
      }

      public bool RestoreGraphicState()
      {
         return pdfRestoreGraphicState(m_Instance) != 0;
      }

      public bool RotateCoords(double alpha, double OriginX, double OriginY)
      {
         return pdfRotateCoords(m_Instance, alpha, OriginX, OriginY) != 0;
      }

      public uint RGB(byte r, byte g, byte b)
      {
         return (uint)((r|(g<<8))|(b<<16));
      }

      public bool RoundRect(double PosX, double PosY, double Width, double Height, double Radius, TPathFillMode FillMode)
      {
         return pdfRoundRect(m_Instance, PosX, PosY, Width, Height, Radius, FillMode) != 0;
      }

      public bool RoundRectEx(double PosX, double PosY, double Width, double Height, double rWidth, double rHeight, TPathFillMode FillMode)
      {
         return pdfRoundRectEx(m_Instance, PosX, PosY, Width, Height, rWidth, rHeight, FillMode) != 0;
      }

      public bool SaveGraphicState()
      {
         return pdfSaveGraphicState(m_Instance) != 0;
      }

      public bool ScaleCoords(double sx, double sy)
      {
         return pdfScaleCoords(m_Instance, sx, sy) != 0;
      }

      public bool SelfTest()
      {
         return pdfSelfTest(m_Instance) != 0;
      }

      public bool Set3DAnnotProps(int Annot, T3DActivationType ActType, T3DDeActivateType DeActType, T3DInstanceType InstType, T3DDeActInstance DeInstType, bool DisplToolbar, bool DisplModelTree)
      {
         return pdfSet3DAnnotProps(m_Instance, Annot, ActType, DeActType, InstType, DeInstType, Convert.ToInt32(DisplToolbar), Convert.ToInt32(DisplModelTree)) != 0;
      }

      public bool Set3DAnnotScriptA(int Annot, String Value)
      {
         return pdfSet3DAnnotScriptA(m_Instance, Annot, Value, Value.Length) != 0;
      }

      public int SetAllocBy(int Value)
      {
         return pdfSetAllocBy(m_Instance, Value);
      }

      public bool SetAltFonts(int ListHandle, String[] List)
      {
         return pdfSetAltFontsW(m_Instance, ListHandle, List, List.Length) != 0;
      }

      public bool SetAnnotBorderEffect(int Handle, TBorderEffect Style)
      {
         return pdfSetAnnotBorderEffect(m_Instance, Handle, Style) != 0;
      }

      public bool SetAnnotBorderStyle(int Handle, TBorderStyle Style)
      {
         return pdfSetAnnotBorderStyle(m_Instance, Handle, Style) != 0;
      }

      public bool SetAnnotBorderWidth(int Handle, double LineWidth)
      {
         return pdfSetAnnotBorderWidth(m_Instance, Handle, LineWidth) != 0;
      }

      public bool SetAnnotColor(int Handle, TAnnotColor ColorType, TPDFColorSpace CS, uint Color)
      {
         return pdfSetAnnotColor(m_Instance, Handle, ColorType, CS, Color) != 0;
      }

      public bool SetAnnotFlags(TAnnotFlags Flags)
      {
         return pdfSetAnnotFlags(m_Instance, Flags) != 0;
      }

      public bool SetAnnotFlagsEx(int Handle, int Flags)
      {
         return pdfSetAnnotFlagsEx(m_Instance, Handle, Flags) != 0;
      }

      public bool SetAnnotHighlightMode(int Handle, THighlightMode Mode)
      {
         return pdfSetAnnotHighlightMode(m_Instance, Handle, Mode) != 0;
      }

      public bool SetAnnotIcon(int Handle, TAnnotIcon Icon)
      {
         return pdfSetAnnotIcon(m_Instance, Handle, Icon) != 0;
      }

      public bool SetAnnotLineDashPattern(int Handle, float[] Dash)
      {
         return pdfSetAnnotLineDashPattern(m_Instance, Handle, Dash, Dash.Length) != 0;
      }

      public bool SetAnnotLineEndStyle(int Handle, TLineEndStyle Start, TLineEndStyle End)
      {
         return pdfSetAnnotLineEndStyle(m_Instance, Handle, Start, End) != 0;
      }

      public int SetAnnotMigrationState(int Annot, TAnnotState State, String User)
      {
         return pdfSetAnnotMigrationState(m_Instance, Annot, State, User);
      }

      public bool SetAnnotOpacity(int Handle, double Value)
      {
         return pdfSetAnnotOpacity(m_Instance, Handle, Value) != 0;
      }

      public bool SetAnnotOpenState(int Handle, bool Open)
      {
         return pdfSetAnnotOpenState(m_Instance, Handle, Open) != 0;
      }

      public bool SetAnnotOrFieldDate(int Handle, bool IsField, TDateType Type, uint DateTime)
      {
         return pdfSetAnnotOrFieldDate(m_Instance, Handle, Convert.ToInt32(IsField), Type, DateTime) != 0;
      }

      public bool SetAnnotQuadPoints(int Handle, TFltPoint[] Value)
      {
         return pdfSetAnnotQuadPoints(m_Instance, Handle, Value, Value.Length) != 0;
      }

      public bool SetAnnotString(int Handle, TAnnotString StringType, String Value)
      {
         return pdfSetAnnotStringW(m_Instance, Handle, StringType, Value) != 0;
      }

      public bool SetAnnotStringA(int Handle, TAnnotString StringType, String Value)
      {
         return pdfSetAnnotStringA(m_Instance, Handle, StringType, Value) != 0;
      }

      public bool SetAnnotStringW(int Handle, TAnnotString StringType, String Value)
      {
         return pdfSetAnnotStringW(m_Instance, Handle, StringType, Value) != 0;
      }

      public bool SetAnnotSubject(int Handle, String Value)
      {
         return pdfSetAnnotSubjectW(m_Instance, Handle, Value) != 0;
      }

      public bool SetAnnotSubjectA(int Handle, String Value)
      {
         return pdfSetAnnotSubjectA(m_Instance, Handle, Value) != 0;
      }

      public bool SetAnnotSubjectW(int Handle, String Value)
      {
         return pdfSetAnnotSubjectW(m_Instance, Handle, Value) != 0;
      }

      public bool SetBBox(TPageBoundary Boundary, double LeftX, double LeftY, double RightX, double RightY)
      {
         return pdfSetBBox(m_Instance, Boundary, LeftX, LeftY, RightX, RightY) != 0;
      }

      public bool SetBidiMode(TPDFBidiMode Mode)
      {
         return pdfSetBidiMode(m_Instance, Mode) != 0;
      }

      public bool SetBookmarkDest(int ABmk, TDestType DestType, double a, double b, double c, double d)
      {
         return pdfSetBookmarkDest(m_Instance, ABmk, DestType, a, b, c, d) != 0;
      }

      public bool SetBookmarkStyle(int ABmk, TFStyle Style, uint RGBColor)
      {
         return pdfSetBookmarkStyle(m_Instance, ABmk, Style, RGBColor) != 0;
      }

      public bool SetBorderStyle(TBorderStyle Style)
      {
         return pdfSetBorderStyle(m_Instance, Style) != 0;
      }

      public bool SetCharacterSpacing(double Value)
      {
         return pdfSetCharacterSpacing(m_Instance, Value) != 0;
      }

      public bool SetCheckBoxChar(TCheckBoxChar CheckBoxChar)
      {
         return pdfSetCheckBoxChar(m_Instance, CheckBoxChar) != 0;
      }

      public bool SetCheckBoxDefState(int Field, bool Checked)
      {
         return pdfSetCheckBoxDefState(m_Instance, Field, Convert.ToInt32(Checked)) != 0;
      }

      public bool SetCheckBoxState(int Field, bool Checked)
      {
         return pdfSetCheckBoxState(m_Instance, Field, Convert.ToInt32(Checked)) != 0;
      }

      public int SetCIDFont(int CMapHandle, String Name, TFStyle Style, double Size, bool Embed)
      {
         return pdfSetCIDFontW(m_Instance, CMapHandle, Name, Style, Size, Convert.ToInt32(Embed));
      }

      public int SetCIDFontA(int CMapHandle, String Name, TFStyle Style, double Size, bool Embed)
      {
         return pdfSetCIDFontA(m_Instance, CMapHandle, Name, Style, Size, Convert.ToInt32(Embed));
      }

      public int SetCIDFontW(int CMapHandle, String Name, TFStyle Style, double Size, bool Embed)
      {
         return pdfSetCIDFontW(m_Instance, CMapHandle, Name, Style, Size, Convert.ToInt32(Embed));
      }

      public int SetCMapDir(String Path, TLoadCMapFlags Flags)
      {
         return pdfSetCMapDirW(m_Instance, Path, Flags);
      }

      public int SetCMapDirA(String Path, TLoadCMapFlags Flags)
      {
         return pdfSetCMapDirA(m_Instance, Path, Flags);
      }

      public int SetCMapDirW(String Path, TLoadCMapFlags Flags)
      {
         return pdfSetCMapDirW(m_Instance, Path, Flags);
      }

      public bool SetColDefFile(int EmbFile)
      {
         return pdfSetColDefFile(m_Instance, EmbFile) != 0;
      }

      public bool SetColorMask(int ImageHandle, int[] Mask)
      {
         int len = 0;
         if (Mask != null) len = Mask.Length;
         return pdfSetColorMask(m_Instance, ImageHandle, Mask, len) != 0;
      }

      public bool SetColors(uint Color)
      {
         return pdfSetColors(m_Instance, Color) != 0;
      }

      public bool SetColorSpace(TPDFColorSpace ColorSpace)
      {
         return pdfSetColorSpace(m_Instance, ColorSpace) != 0;
      }

      public bool SetColSortField(int ColField, bool AscendingOrder)
      {
         return pdfSetColSortField(m_Instance, ColField, Convert.ToInt32(AscendingOrder)) != 0;
      }

      public bool SetCompressionFilter(TCompressionFilter Filter)
      {
         return pdfSetCompressionFilter(m_Instance, Filter) != 0;
      }

      public bool SetCompressionLevel(TCompressionLevel CompressLevel)
      {
         return pdfSetCompressionLevel(m_Instance, CompressLevel) != 0;
      }

      public bool SetContent(String Buffer, int BufSize)
      {
         return pdfSetContent(m_Instance, Buffer, BufSize) != 0;
      }

      public bool SetDateTimeFormat(int TxtField, TPDFDateTime Fmt)
      {
         return pdfSetDateTimeFormat(m_Instance, TxtField, Fmt) != 0;
      }

      public bool SetDefBitsPerPixel(int Value)
      {
         return pdfSetDefBitsPerPixel(m_Instance, Value) != 0;
      }

      public bool SetDocInfo(TDocumentInfo DInfo, String Value)
      {
         return pdfSetDocInfoW(m_Instance, DInfo, Value) != 0;
      }

      public bool SetDocInfoA(TDocumentInfo DInfo, String Value)
      {
         return pdfSetDocInfoA(m_Instance, DInfo, Value) != 0;
      }

      public bool SetDocInfoW(TDocumentInfo DInfo, String Value)
      {
         return pdfSetDocInfoW(m_Instance, DInfo, Value) != 0;
      }

      public bool SetDocInfoEx(TDocumentInfo DInfo, String Key, String Value)
      {
         return pdfSetDocInfoExW(m_Instance, DInfo, Key, Value) != 0;
      }

      public bool SetDocInfoExA(TDocumentInfo DInfo, String Key, String Value)
      {
         return pdfSetDocInfoExA(m_Instance, DInfo, Key, Value) != 0;
      }

      public bool SetDocInfoExW(TDocumentInfo DInfo, String Key, String Value)
      {
         return pdfSetDocInfoExW(m_Instance, DInfo, Key, Value) != 0;
      }

      public bool SetDrawDirection(TDrawDirection Direction)
      {
         return pdfSetDrawDirection(m_Instance, Direction) != 0;
      }

      public bool SetEMFFrameDPI(int DPIX, int DPIY)
      {
         return pdfSetEMFFrameDPI(m_Instance, DPIX, DPIY) != 0;
      }

      public bool SetEMFPatternDistance(double Value)
      {
         return pdfSetEMFPatternDistance(m_Instance, Value) != 0;
      }

      public bool SetErrorMode(TErrMode ErrMode)
      {
         return pdfSetErrorMode(m_Instance, ErrMode) != 0;
      }

      public bool SetExtColorSpace(int Handle)
      {
         return pdfSetExtColorSpace(m_Instance, Handle) != 0;
      }

      public bool SetExtFillColorSpace(int Handle)
      {
         return pdfSetExtFillColorSpace(m_Instance, Handle) != 0;
      }

      public bool SetExtGState(int Handle)
      {
         return pdfSetExtGState(m_Instance, Handle) != 0;
      }

      public bool SetExtStrokeColorSpace(int Handle)
      {
         return pdfSetExtStrokeColorSpace(m_Instance, Handle) != 0;
      }

      public bool SetFieldBBox(int Field, ref TPDFRect BBox)
      {
         return pdfSetFieldBBox(m_Instance, Field, ref BBox) != 0;
      }

      public bool SetFieldBackColor(uint AColor)
      {
         return pdfSetFieldBackColor(m_Instance, AColor) != 0;
      }

      public bool SetFieldBorderColor(uint AColor)
      {
         return pdfSetFieldBorderColor(m_Instance, AColor) != 0;
      }

      public bool SetFieldBorderWidth(int Field, double LineWidth)
      {
         return pdfSetFieldBorderWidth(m_Instance, Field, LineWidth) != 0;
      }

      public bool SetFieldCalcOrder(int CurrIndex, int NewIndex)
      {
         return pdfSetFieldCalcOrder(m_Instance, CurrIndex, NewIndex) != 0;
      }

      public bool SetFieldColor(int Field, TFieldColor ColorType, TPDFColorSpace CS, uint Color)
      {
         return pdfSetFieldColor(m_Instance, Field, ColorType, CS, Color) != 0;
      }

      public bool SetFieldBorderStyle(int Field, TBorderStyle Style)
      {
         return pdfSetFieldBorderStyle(m_Instance, Field, Style) != 0;
      }

      public bool SetFieldExpValue(int Field, int ValIndex, String Value, String ExpValue, bool Selected)
      {
         return pdfSetFieldExpValueW(m_Instance, Field, ValIndex, Value, ExpValue, Convert.ToInt32(Selected)) != 0;
      }

      public bool SetFieldExpValueA(int Field, int ValIndex, String Value, String ExpValue, bool Selected)
      {
         return pdfSetFieldExpValueA(m_Instance, Field, ValIndex, Value, ExpValue, Convert.ToInt32(Selected)) != 0;
      }

      public bool SetFieldExpValueW(int Field, int ValIndex, String Value, String ExpValue, bool Selected)
      {
         return pdfSetFieldExpValueW(m_Instance, Field, ValIndex, Value, ExpValue, Convert.ToInt32(Selected)) != 0;
      }

      public bool SetFieldExpValueEx(int Field, int ValIndex, bool Selected, bool DefSelected)
      {
         return pdfSetFieldExpValueEx(m_Instance, Field, ValIndex, Convert.ToInt32(Selected), Convert.ToInt32(DefSelected)) != 0;
      }

      public bool SetFieldFlags(int Field, TFieldFlags Flags, bool Reset)
      {
         return pdfSetFieldFlags(m_Instance, Field, Flags, Convert.ToInt32(Reset)) != 0;
      }

      public int SetFieldFont(int Field, String Name, TFStyle Style, double Size, lbool Embed, TCodepage CP)
      {
         return pdfSetFieldFont(m_Instance, Field, Name, Style, Size, Convert.ToInt32(Embed), CP);
      }

      public bool SetFieldFontEx(int Field, int Handle, double FontSize)
      {
         return pdfSetFieldFontEx(m_Instance, Field, Handle, FontSize) != 0;
      }

      public bool SetFieldFontSize(int Field, double FontSize)
      {
         return pdfSetFieldFontSize(m_Instance, Field, FontSize) != 0;
      }

      public bool SetFieldHighlightMode(int Field, THighlightMode Mode)
      {
         return pdfSetFieldHighlightMode(m_Instance, Field, Mode) != 0;
      }

      public bool SetFieldIndex(int Field, int Index)
      {
         return pdfSetFieldIndex(m_Instance, Field, Index) != 0;
      }

      public bool SetFieldMapName(int Field, String Name)
      {
         return pdfSetFieldMapName(m_Instance, Field, Name) != 0;
      }

      public bool SetFieldName(int Field, String NewName)
      {
         return pdfSetFieldName(m_Instance, Field, NewName) != 0;
      }

      public bool SetFieldOrientation(int Field, int Value)
      {
         return pdfSetFieldOrientation(m_Instance, Field, Value) != 0;
      }

      public bool SetFieldTextAlign(int Field, TTextAlign Align)
      {
         return pdfSetFieldTextAlign(m_Instance, Field, Align) != 0;
      }

      public bool SetFieldTextColor(uint Color)
      {
         return pdfSetFieldTextColor(m_Instance, Color) != 0;
      }

      public bool SetFieldToolTip(int Field, String Value)
      {
         return pdfSetFieldToolTipW(m_Instance, Field, Value) != 0;
      }

      public bool SetFieldToolTipA(int Field, String Value)
      {
         return pdfSetFieldToolTipA(m_Instance, Field, Value) != 0;
      }

      public bool SetFieldToolTipW(int Field, String Value)
      {
         return pdfSetFieldToolTipW(m_Instance, Field, Value) != 0;
      }

      public bool SetFillColor(uint Color)
      {
         return pdfSetFillColor(m_Instance, Color) != 0;
      }

      public bool SetFillColorEx(byte[] Color, int NumComponents)
      {
         return pdfSetFillColorEx(m_Instance, Color, NumComponents) != 0;
      }

      public bool SetFillColorF(float[] Color, int NumComponents)
      {
         return pdfSetFillColorF(m_Instance, Color, NumComponents) != 0;
      }

      public bool SetFillColorSpace(TPDFColorSpace CS)
      {
         return pdfSetFillColorSpace(m_Instance, CS) != 0;
      }

      public bool SetFloatPrecision(int NumTextDecDigits, int NumVectDecDigits)
      {
         return pdfSetFloatPrecision(m_Instance, NumTextDecDigits, NumVectDecDigits) != 0;
      }

      public int SetFont(String Name, TFStyle Style, double Size, bool Embed, TCodepage CP)
      {
         return pdfSetFont(m_Instance, Name, Style, Size, Convert.ToInt32(Embed), CP);
      }

      public int SetFontEx(String Name, TFStyle Style, double Size, bool Embed, TCodepage CP)
      {
         return pdfSetFontEx(m_Instance, Name, Style, Size, Convert.ToInt32(Embed), CP);
      }

      public bool SetFontOrigin(TOrigin Origin)
      {
         return pdfSetFontOrigin(m_Instance, Origin) != 0;
      }

      public void SetFontSearchOrder(TFontBaseType[] Order)
      {
         pdfSetFontSearchOrder(m_Instance, Order);
      }

      public void SetFontSearchOrderEx(TFontBaseType S1, TFontBaseType S2, TFontBaseType S3, TFontBaseType S4)
      {
         pdfSetFontSearchOrderEx(m_Instance, S1, S2, S3, S4);
      }

      public bool SetFontSelMode(TFontSelMode Mode)
      {
         return pdfSetFontSelMode(m_Instance, Mode) != 0;
      }

      public bool SetFontWeight(int Weight)
      {
         return pdfSetFontWeight(m_Instance, Weight) != 0;
      }

      public void SetGStateFlags(TGStateFlags Flags, bool Reset)
      {
         pdfSetGStateFlags(m_Instance, Flags, Convert.ToInt32(Reset));
      }

      public bool SetIconColor(uint Color)
      {
         return pdfSetIconColor(m_Instance, Color) != 0;
      }

      public bool SetImportFlags(TImportFlags Flags)
      {
         return pdfSetImportFlags(m_Instance, Flags) != 0;
      }

      public bool SetImportFlags2(TImportFlags2 Flags)
      {
         return pdfSetImportFlags2(m_Instance, Flags) != 0;
      }

      public bool SetItalicAngle(double Angle)
      {
         return pdfSetItalicAngle(m_Instance, Angle) != 0;
      }

      public bool SetJPEGQuality(int Value)
      {
         return pdfSetJPEGQuality(m_Instance, Value) != 0;
      }

      public bool SetLanguage(String ISOTag)
      {
         return pdfSetLanguage(m_Instance, ISOTag) != 0;
      }

      public bool SetLeading(double Value)
      {
         return pdfSetLeading(m_Instance, Value) != 0;
      }

      public bool SetLicenseKey(String Value)
      {
         return pdfSetLicenseKey(m_Instance, Value) != 0;
      }

      public bool SetLineAnnotParms(int Handle, int FontHandle, double FontSize, TLineAnnotParms Parms)
      {
         if (Parms != null) Parms.StructSize = Marshal.SizeOf(Parms);
         return pdfSetLineAnnotParms(m_Instance, Handle, FontHandle, FontSize, Parms) != 0;
      }

      public bool SetLineCapStyle(TLineCapStyle Style)
      {
         return pdfSetLineCapStyle(m_Instance, Style) != 0;
      }

      public bool SetLineDashPattern2(float[] Dash, float Phase)
      {
         return pdfSetLineDashPattern2(m_Instance, Dash, Dash.Length, Phase) != 0;
      }

      public bool SetLineDashPatternEx(double[] Dash, int Phase)
      {
         return pdfSetLineDashPatternEx(m_Instance, Dash, Dash.Length, Phase) != 0;
      }

      public bool SetLineJoinStyle(TLineJoinStyle Style)
      {
         return pdfSetLineJoinStyle(m_Instance, Style) != 0;
      }

      public bool SetLineWidth(double Value)
      {
         return pdfSetLineWidth(m_Instance, Value) != 0;
      }

      public bool SetLinkHighlightMode(THighlightMode Mode)
      {
         return pdfSetLinkHighlightMode(m_Instance, Mode) != 0;
      }

      public bool SetListFont(int Handle)
      {
         return pdfSetListFont(m_Instance, Handle) != 0;
      }

      public bool SetMatrix(ref TCTM Matrix)
      {
         return pdfSetMatrix(m_Instance, ref Matrix) != 0;
      }

      public void SetMaxErrLogMsgCount(int Value)
      {
         pdfSetMaxErrLogMsgCount(m_Instance, Value);
      }

      public bool SetMaxFieldLen(int TxtField, int MaxLen)
      {
         return pdfSetMaxFieldLen(m_Instance, TxtField, MaxLen) != 0;
      }

      public bool SetMetaConvFlags(TMetaFlags Flags)
      {
         return pdfSetMetaConvFlags(m_Instance, Flags) != 0;
      }

      public bool SetMetadata(TMetadataObj ObjType, int Handle, byte[] Buffer)
      {
         return pdfSetMetadata(m_Instance, ObjType, Handle, Buffer, Buffer.Length) != 0;
      }

      public void SetMinLineWidth2(IntPtr RasPtr, float Value)
      {
         rasSetMinLineWidth2(RasPtr, Value);
      }

      public void SetScreenRes(IntPtr RasPtr, int Value)
      {
         rasSetScreenRes(RasPtr, Value);
      }

      public bool SetMiterLimit(double Value)
      {
         return pdfSetMiterLimit(m_Instance, Value) != 0;
      }

      public bool SetNeedAppearance(bool Value)
      {
         return pdfSetNeedAppearance(m_Instance, Convert.ToInt32(Value)) != 0;
      }

      public bool SetNumberFormat(int TxtField, TDecSeparator Sep, int DecPlaces, TNegativeStyle NegStyle, String CurrStr, bool Prepend)
      {
         return pdfSetNumberFormat(m_Instance, TxtField, Sep, DecPlaces, NegStyle, CurrStr, Convert.ToInt32(Prepend)) != 0;
      }

      public bool SetOCGContUsage(int Handle, ref TPDFOCGContUsage Value)
      {
         Value.StructSize = Marshal.SizeOf(Value);
         return pdfSetOCGContUsage(m_Instance, Handle, ref Value) != 0;
      }

      public bool SetOCGState(int Handle, bool On, bool SaveState)
      {
         return pdfSetOCGState(m_Instance, Handle, Convert.ToInt32(On), Convert.ToInt32(SaveState)) != 0;
      }

      public bool SetOnErrorProc(IntPtr Data, TErrorProc ErrProc)
      {
         m_ErrorProc = ErrProc;
         return pdfSetOnErrorProc(m_Instance, Data, m_ErrorProc) != 0;
      }

      public bool SetOnPageBreakProc(IntPtr Data, TOnPageBreakProc OnPageBreakProc)
      {
         m_OnPageBreakProc = OnPageBreakProc;
         return pdfSetOnPageBreakProc(m_Instance, Data, m_OnPageBreakProc) != 0;
      }

      public bool SetOpacity(double Value)
      {
         return pdfSetOpacity(m_Instance, Value) != 0;
      }

      public bool SetOrientation(int Value)
      {
         return pdfSetOrientation(m_Instance, Value) != 0;
      }

      public bool SetOrientationEx(int Value)
      {
         return pdfSetOrientationEx(m_Instance, Value) != 0;
      }

      public bool SetPageBBox(IntPtr PagePtr, TPageBoundary Boundary, ref TFltRect BBox)
      {
         return pdfSetPageBBox(PagePtr, Boundary, ref BBox) != 0;
      }

      public bool SetPageCoords(TPageCoord PageCoords)
      {
         return pdfSetPageCoords(m_Instance, PageCoords) != 0;
      }

      public bool SetPageFormat(TPageFormat Value)
      {
         return pdfSetPageFormat(m_Instance, Value) != 0;
      }

      public bool SetPageHeight(double Value)
      {
         return pdfSetPageHeight(m_Instance, Value) != 0;
      }

      public bool SetPageLayout(TPageLayout Layout)
      {
         return pdfSetPageLayout(m_Instance, Layout) != 0;
      }

      public bool SetPageMode(TPageMode Mode)
      {
         return pdfSetPageMode(m_Instance, Mode) != 0;
      }

      public bool SetPageOrientation(IntPtr PagePtr, int Value)
      {
         return pdfSetPageOrientation(m_Instance, PagePtr, Value) != 0;
      }

      public bool SetPageWidth(double Value)
      {
         return pdfSetPageWidth(m_Instance, Value) != 0;
      }

      public bool SetPDFVersion(TPDFVersion Version)
      {
         return pdfSetPDFVersion(m_Instance, Version) != 0;
      }

      public bool SetPrintSettings(TDuplexMode Mode, int PickTrayByPDFSize, int NumCopies, TPrintScaling PrintScaling, uint[] PrintRanges, int NumRanges)
      {
         return pdfSetPrintSettings(m_Instance, Mode, PickTrayByPDFSize, NumCopies, PrintScaling, PrintRanges, NumRanges) != 0;
      }

      public bool SetProgressProc(IntPtr Data, TInitProgressProc InitProgress, TProgressProc Progress)
      {
         m_InitProgressProc = InitProgress;
         m_ProgressProc     = Progress;
         return pdfSetProgressProc(m_Instance, Data, m_InitProgressProc, m_ProgressProc) != 0;
      }

      public bool SetResolution(int Value)
      {
         return pdfSetResolution(m_Instance, Value) != 0;
      }

      public bool SetSaveNewImageFormat(bool Value)
      {
         return pdfSetSaveNewImageFormat(m_Instance, Convert.ToInt32(Value)) != 0;
      }

      public bool SetSeparationInfo(int Handle)
      {
         // Handle must be a handle of a separation color space
         return pdfSetSeparationInfo(m_Instance, Handle) != 0;
      }

      public bool SetSpaceWidthFactor(double Value)
      {
         return pdfSetSpaceWidthFactor(m_Instance, Value) != 0;
      }

      public bool SetStrokeColor(uint Color)
      {
         return pdfSetStrokeColor(m_Instance, Color) != 0;
      }

      public bool SetStrokeColorEx(byte[] Color, int NumComponents)
      {
         return pdfSetStrokeColorEx(m_Instance, Color, NumComponents) != 0;
      }

      public bool SetStrokeColorF(float[] Color, int NumComponents)
      {
         return pdfSetStrokeColorF(m_Instance, Color, NumComponents) != 0;
      }

      public bool SetStrokeColorSpace(TPDFColorSpace CS)
      {
         return pdfSetStrokeColorSpace(m_Instance, CS) != 0;
      }

      public bool SetTabLen(int TabLen)
      {
         return pdfSetTabLen(m_Instance, TabLen) != 0;
      }

      public bool SetTemplBBox(int Handle, TPageBoundary Boundary, ref TPDFRect BBox)
      {
         return pdfSetTemplBBox(m_Instance, Handle, Boundary, ref BBox) != 0;
      }

      public bool SetTextDrawMode(TDrawMode Mode)
      {
         return pdfSetTextDrawMode(m_Instance, Mode) != 0;
      }

      public bool SetTextFieldValue(int Field, String Value, String DefValue, TTextAlign Align)
      {
         return pdfSetTextFieldValueW(m_Instance, Field, Value, DefValue, Align) != 0;
      }

      public bool SetTextFieldValueA(int Field, String Value, String DefValue, TTextAlign Align)
      {
         return pdfSetTextFieldValueA(m_Instance, Field, Value, DefValue, Align) != 0;
      }

      public bool SetTextFieldValueW(int Field, String Value, String DefValue, TTextAlign Align)
      {
         return pdfSetTextFieldValueW(m_Instance, Field, Value, DefValue, Align) != 0;
      }

      public bool SetTextFieldValueEx(int Field, String Value)
      {
         return pdfSetTextFieldValueExW(m_Instance, Field, Value) != 0;
      }

      public bool SetTextFieldValueExA(int Field, String Value)
      {
         return pdfSetTextFieldValueExA(m_Instance, Field, Value) != 0;
      }

      public bool SetTextFieldValueExW(int Field, String Value)
      {
         return pdfSetTextFieldValueExW(m_Instance, Field, Value) != 0;
      }

      public bool SetTextRect(double PosX, double PosY, double Width, double Height)
      {
         return pdfSetTextRect(m_Instance, PosX, PosY, Width, Height) != 0;
      }

      public bool SetTextRise(double Value)
      {
         return pdfSetTextRise(m_Instance, Value) != 0;
      }

      public bool SetTextScaling(double Value)
      {
         return pdfSetTextScaling(m_Instance, Value) != 0;
      }

      public bool SetTransparentColor(uint AColor)
      {
         return pdfSetTransparentColor(m_Instance, AColor) != 0;
      }

      public void SetTrapped(bool Value)
      {
         pdfSetTrapped(m_Instance, Convert.ToInt32(Value));
      }

      public bool SetUseExactPwd(bool Value)
      {
         return pdfSetUseExactPwd(m_Instance, Convert.ToInt32(Value)) != 0;
      }

      public bool SetUseGlobalImpFiles(bool Value)
      {
         return pdfSetUseGlobalImpFiles(m_Instance, Convert.ToInt32(Value)) != 0;
      }

      public bool SetUseImageInterpolation(int Handle, bool Value)
      {
         return pdfSetUseImageInterpolation(m_Instance, Handle, Convert.ToInt32(Value)) != 0;
      }

      public bool SetUseImageInterpolationEx(IntPtr Image, bool Value)
      {
         return pdfSetUseImageInterpolationEx(Image, Convert.ToInt32(Value)) != 0;
      }

      public bool SetUserUnit(float Value)
      {
         return pdfSetUserUnit(m_Instance, Value) != 0;
      }

      public bool SetUseStdFonts(bool Value)
      {
         return pdfSetUseStdFonts(m_Instance, Convert.ToInt32(Value)) != 0;
      }

      public bool SetUseSwapFile(bool SwapContents, int SwapLimit)
      {
         return pdfSetUseSwapFile(m_Instance, Convert.ToInt32(SwapContents), SwapLimit) != 0;
      }

      public bool SetUseSwapFileEx(bool SwapContents, int SwapLimit, String SwapDir)
      {
         return pdfSetUseSwapFileEx(m_Instance, Convert.ToInt32(SwapContents), SwapLimit, SwapDir) != 0;
      }

      public bool SetUseSystemFonts(bool Value)
      {
         return pdfSetUseSystemFonts(m_Instance, Convert.ToInt32(Value)) != 0;
      }

      public bool SetUseTransparency(bool Value)
      {
         return pdfSetUseTransparency(m_Instance, Convert.ToInt32(Value)) != 0;
      }

      public bool SetUseVisibleCoords(bool Value)
      {
         return pdfSetUseVisibleCoords(m_Instance, Convert.ToInt32(Value)) != 0;
      }

      public bool SetViewerPreferences(TViewerPreference Value, TViewPrefAddVal AddVal)
      {
         return pdfSetViewerPreferences(m_Instance, Value, AddVal) != 0;
      }

      public bool SetWMFDefExtent(int Width, int Height)
      {
         return pdfSetWMFDefExtent(m_Instance, Width, Height) != 0;
      }

      public bool SetWMFPixelPerInch(int Value)
      {
         return pdfSetWMFPixelPerInch(m_Instance, Value) != 0;
      }

      public bool SetWordSpacing(double Value)
      {
         return pdfSetWordSpacing(m_Instance, Value) != 0;
      }

      public bool SetXFAStream(int Index, byte[] Buffer)
      {
         return pdfSetXFAStream(m_Instance, Index, Buffer, Buffer.Length) != 0;
      }

      public bool SkewCoords(double alpha, double beta, double OriginX, double OriginY)
      {
         return pdfSkewCoords(m_Instance, alpha, beta, OriginX, OriginY) != 0;
      }

      public bool SortFieldsByIndex()
      {
         return pdfSortFieldsByIndex(m_Instance) != 0;
      }

      public bool SortFieldsByName()
      {
         return pdfSortFieldsByName(m_Instance) != 0;
      }

      public int SquareAnnot(double PosX, double PosY, double Width, double Height, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Comment)
      {
         return pdfSquareAnnotW(m_Instance, PosX, PosY, Width, Height, LineWidth, FillColor, StrokeColor, CS, Author, Subject, Comment);
      }

      public int SquareAnnotA(double PosX, double PosY, double Width, double Height, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Comment)
      {
         return pdfSquareAnnotA(m_Instance, PosX, PosY, Width, Height, LineWidth, FillColor, StrokeColor, CS, Author, Subject, Comment);
      }

      public int SquareAnnotW(double PosX, double PosY, double Width, double Height, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Comment)
      {
         return pdfSquareAnnotW(m_Instance, PosX, PosY, Width, Height, LineWidth, FillColor, StrokeColor, CS, Author, Subject, Comment);
      }

      public int StampAnnot(TRubberStamp SubType, double PosX, double PosY, double Width, double Height, String Author, String Subject, String Comment)
      {
         return pdfStampAnnotW(m_Instance, SubType, PosX, PosY, Width, Height, Author, Subject, Comment);
      }

      public int StampAnnotA(TRubberStamp SubType, double PosX, double PosY, double Width, double Height, String Author, String Subject, String Comment)
      {
         return pdfStampAnnotA(m_Instance, SubType, PosX, PosY, Width, Height, Author, Subject, Comment);
      }

      public int StampAnnotW(TRubberStamp SubType, double PosX, double PosY, double Width, double Height, String Author, String Subject, String Comment)
      {
         return pdfStampAnnotW(m_Instance, SubType, PosX, PosY, Width, Height, Author, Subject, Comment);
      }

      public bool StrokePath()
      {
         return pdfStrokePath(m_Instance) != 0;
      }

      public int TestGlyphs(int FontHandle, String Text)
      {
         return pdfTestGlyphsEx(m_Instance, FontHandle, Text, Text.Length);
      }

      public int TestGlyphs(int FontHandle, String Text, int Len)
      {
         if (Len > Text.Length) Len = Text.Length;
         return pdfTestGlyphsEx(m_Instance, FontHandle, Text, Len);
      }

      public int TextAnnot(double PosX, double PosY, double Width, double Height, String Author, String Text, TAnnotIcon Icon, bool Open)
      {
         return pdfTextAnnotW(m_Instance, PosX, PosY, Width, Height, Author, Text, Icon, Convert.ToInt32(Open));
      }

      public int TextAnnotA(double PosX, double PosY, double Width, double Height, String Author, String Text, TAnnotIcon Icon, bool Open)
      {
         return pdfTextAnnotA(m_Instance, PosX, PosY, Width, Height, Author, Text, Icon, Convert.ToInt32(Open));
      }

      public int TextAnnotW(double PosX, double PosY, double Width, double Height, String Author, String Text, TAnnotIcon Icon, bool Open)
      {
         return pdfTextAnnotW(m_Instance, PosX, PosY, Width, Height, Author, Text, Icon, Convert.ToInt32(Open));
      }

      public bool TranslateCoords(double OriginX, double OriginY)
      {
         return pdfTranslateCoords(m_Instance, OriginX, OriginY) != 0;
      }

      public int TranslateRawCode(IntPtr IFont, IntPtr Text, int Len, ref double Width, byte[] OutText, ref int OutLen, ref lbool Decoded, float CharSpacing, float WordSpacing, float TextScale)
      {
         return fntTranslateRawCode(IFont, Text, Len, ref Width, OutText, ref OutLen, ref Decoded, CharSpacing, WordSpacing, TextScale);
      }

      public int TranslateString(TPDFStack Stack, ref String OutText, int Flags)
      {
         byte[] buf = new byte[(Stack.TextLen * 16 / 10) * 2 + 64];
         int retval = fntTranslateString(ref Stack, buf, buf.Length >> 1, Flags);
         OutText = System.Text.UnicodeEncoding.Unicode.GetString(buf, 0, retval << 1);
         return retval;
      }

      public int TranslateString2(IntPtr IFont, IntPtr Text, int Len, ref String OutText, int Flags)
      {
         byte[] buf = new byte[(Len * 16 / 10) * 2 + 64];
         int retval = fntTranslateString2(IFont, Text, Len, buf, buf.Length >> 1, Flags);
         OutText = System.Text.UnicodeEncoding.Unicode.GetString(buf, 0, retval << 1);
         return retval;
      }

      public bool Triangle(double x1, double y1, double x2, double y2, double x3, double y3, TPathFillMode FillMode)
      {
         return pdfTriangle(m_Instance, x1, y1, x2, y2, x3, y3, FillMode) != 0;
      }

      public bool UnLockLayer(int Layer)
      {
         return pdfUnLockLayer(m_Instance, Layer) != 0;
      }

      public int WatermarkAnnot(double PosX, double PosY, double Width, double Height)
      {
         return pdfWatermarkAnnot(m_Instance, PosX, PosY, Width, Height);
      }

      public int WebLink(double PosX, double PosY, double Width, double Height, String URL)
      {
         return pdfWebLinkW(m_Instance, PosX, PosY, Width, Height, URL);
      }

      public int WebLinkA(double PosX, double PosY, double Width, double Height, String URL)
      {
         return pdfWebLinkA(m_Instance, PosX, PosY, Width, Height, URL);
      }

      public int WebLinkW(double PosX, double PosY, double Width, double Height, String URL)
      {
         return pdfWebLinkW(m_Instance, PosX, PosY, Width, Height, URL);
      }

      public uint WeightFromStyle(TFStyle Style)
      {
         return (((uint)Style & 0xFFF00000) >> 20);
      }

      public TFStyle WeightToStyle(uint Weight)
      {
         return (TFStyle)(Weight << 20);
      }

      public uint WidthFromStyle(TFStyle Style)
      {
         return (((uint)Style & 0x00000F00) >> 8);
      }

      public TFStyle WidthToStyle(uint Width)
      {
         return (TFStyle)(Width << 8);
      }

      public bool WriteAngleText(String AText, double Angle, double PosX, double PosY, double Radius, double YOrigin)
      {
         return pdfWriteAngleTextW(m_Instance, AText, Angle, PosX, PosY, Radius, YOrigin) != 0;
      }

      public bool WriteAngleTextA(String AText, double Angle, double PosX, double PosY, double Radius, double YOrigin)
      {
         return pdfWriteAngleTextA(m_Instance, AText, Angle, PosX, PosY, Radius, YOrigin) != 0;
      }

      public bool WriteAngleTextW(String AText, double Angle, double PosX, double PosY, double Radius, double YOrigin)
      {
         return pdfWriteAngleTextW(m_Instance, AText, Angle, PosX, PosY, Radius, YOrigin) != 0;
      }

      public bool WriteFText(TTextAlign Align, String AText)
      {
         return pdfWriteFTextW(m_Instance, Align, AText) != 0;
      }

      public bool WriteFTextA(TTextAlign Align, String AText)
      {
         return pdfWriteFTextA(m_Instance, Align, AText) != 0;
      }

      public bool WriteFTextW(TTextAlign Align, String AText)
      {
         return pdfWriteFTextW(m_Instance, Align, AText) != 0;
      }

      public bool WriteFTextEx(double PosX, double PosY, double Width, double Height, TTextAlign Align, String AText)
      {
         return pdfWriteFTextExW(m_Instance, PosX, PosY, Width, Height, Align, AText) != 0;
      }

      public bool WriteFTextExA(double PosX, double PosY, double Width, double Height, TTextAlign Align, String AText)
      {
         return pdfWriteFTextExA(m_Instance, PosX, PosY, Width, Height, Align, AText) != 0;
      }

      public bool WriteFTextExW(double PosX, double PosY, double Width, double Height, TTextAlign Align, String AText)
      {
         return pdfWriteFTextExW(m_Instance, PosX, PosY, Width, Height, Align, AText) != 0;
      }

      public bool WriteText(double PosX, double PosY, String AText)
      {
         return pdfWriteTextW(m_Instance, PosX, PosY, AText) != 0;
      }

      public bool WriteTextA(double PosX, double PosY, String AText)
      {
         return pdfWriteTextA(m_Instance, PosX, PosY, AText) != 0;
      }

      public bool WriteTextW(double PosX, double PosY, String AText)
      {
         return pdfWriteTextW(m_Instance, PosX, PosY, AText) != 0;
      }

      public bool WriteTextEx(double PosX, double PosY, String AText, int Len)
      {
         return pdfWriteTextExW(m_Instance, PosX, PosY, AText, Len) != 0;
      }

      public bool WriteTextExA(double PosX, double PosY, String AText, int Len)
      {
         return pdfWriteTextExA(m_Instance, PosX, PosY, AText, Len) != 0;
      }

      public bool WriteTextExW(double PosX, double PosY, String AText, int Len)
      {
         return pdfWriteTextExW(m_Instance, PosX, PosY, AText, Len) != 0;
      }

      public bool WriteTextMatrix(ref TCTM M, String AText)
      {
         return pdfWriteTextMatrixW(m_Instance, ref M, AText) != 0;
      }

      public bool WriteTextMatrixA(ref TCTM M, String AText)
      {
         return pdfWriteTextMatrixA(m_Instance, ref M, AText) != 0;
      }

      public bool WriteTextMatrixW(ref TCTM M, String AText)
      {
         return pdfWriteTextMatrixW(m_Instance, ref M, AText) != 0;
      }

      public bool WriteTextMatrixEx(ref TCTM M, String AText, int Len)
      {
         return pdfWriteTextMatrixExW(m_Instance, ref M, AText, Len) != 0;
      }

      public bool WriteTextMatrixExA(ref TCTM M, String AText, int Len)
      {
         return pdfWriteTextMatrixExA(m_Instance, ref M, AText, Len) != 0;
      }

      public bool WriteTextMatrixExW(ref TCTM M, String AText, int Len)
      {
         return pdfWriteTextMatrixExW(m_Instance, ref M, AText, Len) != 0;
      }

      /* ------------------------------------- Private functions ------------------------------------- */

      private void ConvertHeaderFooter(ref TPDFHeaderFooter OutVal, ref TPDFHdrFtr[] OutArray, ref TPDFHeaderFooter_I InVal, ref TPDFHdrFtr_I[] InArray)
      {
         int i;
         OutVal.FirstPage    = InVal.FirstPage;
         OutVal.Flags        = InVal.Flags;
         OutVal.InitCodepage = InVal.InitCodepage;
         OutVal.InitColor    = InVal.InitColor;
         OutVal.InitCS       = InVal.InitCS;
         OutVal.InitCSHandle = InVal.InitCSHandle;
         OutVal.InitDate     = Marshal.PtrToStringAnsi(InVal.InitDate);
         OutVal.InitEmbed    = InVal.InitEmbed != 0;

         if( InVal.InitFontA != IntPtr.Zero)
            OutVal.InitFont = Marshal.PtrToStringAnsi(InVal.InitFontA);
         else
            OutVal.InitFont = Marshal.PtrToStringUni(InVal.InitFontW);

         OutVal.InitFontSize = InVal.InitFontSize;
         OutVal.InitFontType = Marshal.PtrToStringAnsi(InVal.InitFontType);
         if (InVal.InitPageFormatA != IntPtr.Zero)
            OutVal.InitPageFormat = Marshal.PtrToStringAnsi(InVal.InitPageFormatA);
         else
            OutVal.InitPageFormat = Marshal.PtrToStringUni(InVal.InitPageFormatW);

         OutVal.LastPage = InVal.LastPage;
         OutVal.Margin   = InVal.Margin;

         for (i = 0; i < OutArray.Length; i++)
            ConvertToHdrFtr(ref InArray[i], ref OutArray[i]);
      }

      private void ConvertToHdrFtr(ref TPDFHdrFtr_I Source, ref TPDFHdrFtr Dest)
      {
         Dest.Codepage = Source.Codepage;
         Dest.Color    = Source.Color;
         Dest.CS       = Source.CS;
         Dest.CSHandle = Source.CSHandle;
         Dest.Embed    = Source.Embed != 0;
         if (Source.FontA != IntPtr.Zero)
            Dest.Font = Marshal.PtrToStringAnsi(Source.FontA);
         else
            Dest.Font = Marshal.PtrToStringUni(Source.FontW);
         Dest.FontSize = Source.FontSize;
         Dest.FontType = Marshal.PtrToStringAnsi(Source.FontType);
         Dest.IsHeader = Source.IsHeader != 0;
         Dest.Position = Source.Position;
         Dest.ShiftX = Source.ShiftX;
         Dest.ShiftY = Source.ShiftY;
         if (Source.TextLen > 0)
         {
            if (Source.TextA != IntPtr.Zero)
               Dest.Text = Marshal.PtrToStringAnsi(Source.TextA, Source.TextLen);
            else
               Dest.Text = Marshal.PtrToStringUni(Source.TextW, Source.TextLen);
         }
      }

      private void ConvertToHdrFtr_I(ref TPDFHdrFtr Source, ref TPDFHdrFtr_I Dest)
      {
         Dest.Codepage = Source.Codepage;
         Dest.Color    = Source.Color;
         Dest.CS       = Source.CS;
         Dest.CSHandle = Source.CSHandle;
         Dest.Embed    = Convert.ToInt32(Source.Embed);
         Dest.FontW    = Marshal.StringToHGlobalUni(Source.Font);
         Dest.FontSize = Source.FontSize;
         Dest.IsHeader = Convert.ToInt32(Source.IsHeader);
         Dest.Position = Source.Position;
         Dest.ShiftX   = Source.ShiftX;
         Dest.ShiftY   = Source.ShiftY;
         Dest.TextW    = Marshal.StringToHGlobalUni(Source.Text);
         if (Source.Text != null)
            Dest.TextLen = Source.Text.Length;
      }

      private void CopyFileSpecEx(ref TPDFFileSpecEx_I Src, ref TPDFFileSpecEx Dst)
      {
         Dst.AFRelationship = _Conv.ToString(Src.AFRelationship, false);
         Dst.ColItem        = Src.ColItem;
         Dst.Description    = _Conv.ToString(Src.DescriptionA, Src.DescriptionW);
         Dst.DOS            = _Conv.ToString(Src.DOS, false);
         Dst.EmbFileNode    = Src.EmbFileNode;
         Dst.FileName       = _Conv.ToString(Src.FileName, false);
         Dst.FileNameIsURL  = Src.FileNameIsURL != 0;
         if (Src.ID1Len > 0)
         {
            Dst.ID1 = new byte[Src.ID1Len];
            Marshal.Copy(Src.ID1, Dst.ID1, 0, Src.ID1Len);
         }else
            Dst.ID1 = null;
         if (Src.ID2Len > 0)
         {
            Dst.ID2 = new byte[Src.ID2Len];
            Marshal.Copy(Src.ID2, Dst.ID2, 0, Src.ID2Len);
         }else
            Dst.ID2 = null;
         Dst.IsVolatile  = Src.IsVolatile != 0;
         Dst.Mac         = _Conv.ToString(Src.Mac, false);
         Dst.Unix        = _Conv.ToString(Src.Unix, false);
         Dst.RelFileNode = Src.RelFileNode;
         Dst.Thumb       = Src.Thumb;
         Dst.UFileName   = _Conv.ToString(Src.UFileNameA, Src.UFileNameW);
      }

      public float[] CopyFloatArray(IntPtr Source, int Count)
      {
         float[] retval = new float[Count];
         Marshal.Copy(Source, retval, 0, Count);
         return retval;
      }

      private void GetIntAnnot(TPDFAnnotation_I IAnnot, ref TPDFAnnotation Annot)
      {
         Annot.BackColor     = IAnnot.BackColor;
         Annot.BBox          = IAnnot.BBox;
         Annot.BorderColor   = IAnnot.BorderColor;
         Annot.BorderStyle   = IAnnot.BorderStyle;
         Annot.BorderWidth   = IAnnot.BorderWidth;
         Annot.Deleted       = IAnnot.Deleted != 0;
         Annot.Handle        = IAnnot.Handle;
         Annot.HighlightMode = IAnnot.HighlightMode;
         Annot.PageNum       = IAnnot.PageNum;
         Annot.Type          = IAnnot.Type;
         Annot.Author        = _Conv.ToString(IAnnot.AuthorA, IAnnot.AuthorW);
         Annot.Content       = _Conv.ToString(IAnnot.ContentA, IAnnot.ContentW);
         Annot.Name          = _Conv.ToString(IAnnot.NameA, IAnnot.NameW);
         Annot.Subject       = _Conv.ToString(IAnnot.SubjectA, IAnnot.SubjectW);
      }
      
      private void GetIntAnnotEx(TPDFAnnotationEx_I IAnnot, ref TPDFAnnotationEx Annot)
      {
         Annot.AnnotFlags        = IAnnot.AnnotFlags;
         Annot.Author            = _Conv.ToString(IAnnot.AuthorA, IAnnot.AuthorW);
         Annot.BackColor         = IAnnot.BackColor;
         Annot.BBox              = IAnnot.BBox;
         Annot.BorderColor       = IAnnot.BorderColor;
         Annot.BorderEffect      = IAnnot.BorderEffect;
         Annot.BorderStyle       = IAnnot.BorderStyle;
         Annot.BorderWidth       = IAnnot.BorderWidth;
         Annot.Caption           = IAnnot.Caption != 0;
         Annot.CaptionOffsetX    = IAnnot.CaptionOffsetX;
         Annot.CaptionOffsetY    = IAnnot.CaptionOffsetY;
         Annot.CaptionPos        = IAnnot.CaptionPos;
         Annot.Content           = _Conv.ToString(IAnnot.ContentA, IAnnot.ContentW);
         Annot.CreateDate        = _Conv.ToString(IAnnot.CreateDate, false);
         Annot.Deleted           = IAnnot.Deleted != 0;
         Annot.DestFile          = _Conv.ToString(IAnnot.DestFile, false);
         Annot.DestPage          = IAnnot.DestPage;
         Annot.DestPos           = IAnnot.DestPos;
         Annot.DestType          = IAnnot.DestType;
         Annot.EmbeddedFile      = IAnnot.EmbeddedFile;
         Annot.Grouped           = IAnnot.Grouped != 0;
         Annot.Handle            = IAnnot.Handle;
         Annot.HighlightMode     = IAnnot.HighlightMode;
         Annot.Icon              = IAnnot.Icon;
         Annot.Intent            = _Conv.ToString(IAnnot.Intent, false);
         Annot.LE1               = IAnnot.LE1;
         Annot.LE2               = IAnnot.LE2;
         Annot.LeaderLineExtend  = IAnnot.LeaderLineExtend;
         Annot.LeaderLineLen     = IAnnot.LeaderLineLen;
         Annot.LeaderLineOffset  = IAnnot.LeaderLineOffset;
         Annot.MarkupAnnot       = IAnnot.MarkupAnnot != 0;
         Annot.ModDate           = _Conv.ToString(IAnnot.ModDate, false);
         Annot.Name              = _Conv.ToString(IAnnot.NameA, IAnnot.NameW);
         Annot.OC                = IAnnot.OC;
         Annot.Opacity           = IAnnot.Opacity;
         Annot.Open              = IAnnot.Open != 0;
         Annot.PageIndex         = IAnnot.PageIndex;
         Annot.PageNum           = IAnnot.PageNum;
         Annot.Parent            = IAnnot.Parent;
         Annot.PopUp             = IAnnot.PopUp;
         Annot.RichStyle         = _Conv.ToString(IAnnot.RichStyle, false);
         Annot.RichText          = _Conv.ToString(IAnnot.RichText, false);
         Annot.Rotate            = IAnnot.Rotate;
         Annot.StampName         = _Conv.ToString(IAnnot.StampName, false);
         Annot.State             = _Conv.ToString(IAnnot.State, false);
         Annot.StateModel        = _Conv.ToString(IAnnot.StateModel, false);
         Annot.Subject           = _Conv.ToString(IAnnot.SubjectA, IAnnot.SubjectW);
         Annot.Subtype           = _Conv.ToString(IAnnot.Subtype, false);
         Annot.Type              = IAnnot.Type;
         if (IAnnot.InkListCount > 0)
         {
            Annot.InkList = new IntPtr[IAnnot.InkListCount];
            Marshal.Copy(IAnnot.InkList, Annot.InkList, 0, IAnnot.InkListCount);
         }else
            Annot.InkList = null;
         if (IAnnot.RD != IntPtr.Zero)
         {
            Annot.RD = new float[4];
            Marshal.Copy(IAnnot.RD, Annot.RD, 0, 4);
         }else
            Annot.RD = null;
         if (IAnnot.QuadPointsCount > 0)
         {
            Annot.QuadPoints = new float[IAnnot.QuadPointsCount];
            Marshal.Copy(IAnnot.QuadPoints, Annot.QuadPoints, 0, IAnnot.QuadPointsCount);
         }else
            Annot.QuadPoints = null;

         if (IAnnot.DashPatternCount > 0)
         {
            Annot.DashPattern = new float[IAnnot.DashPatternCount];
            Marshal.Copy(IAnnot.DashPattern, Annot.DashPattern, 0, IAnnot.DashPatternCount);
         }else
            Annot.DashPattern = null;
         if (IAnnot.VerticesCount > 0)
         {
            Annot.Vertices = new float[IAnnot.VerticesCount];
            Marshal.Copy(IAnnot.Vertices, Annot.Vertices, 0, IAnnot.VerticesCount);
         }else
            Annot.Vertices = null;
      }

      private void GetIntBarcode(TPDFBarcode_I IBC, ref TPDFBarcode BC)
      {
         BC.Caption      = _Conv.ToString(IBC.CaptionA, IBC.CaptionW);
         BC.ECC          = IBC.ECC;
         BC.Height       = IBC.Height;
         BC.nCodeWordCol = IBC.nCodeWordCol;
         BC.nCodeWordRow = IBC.nCodeWordRow;
         BC.Resolution   = IBC.Resolution;
         BC.Symbology    = _Conv.ToString(IBC.Symbology, false);
         BC.Version      = IBC.Version;
         BC.Width        = IBC.Width;
         BC.XSymHeight   = IBC.XSymHeight;
         BC.XSymWidth    = IBC.XSymWidth;
      }

      private void GetIntColorSpaceObj(TPDFColorSpaceObj_I ICS, ref TPDFColorSpaceObj CS)
      {
         int i;
         CS.Type             = ICS.Type;
         CS.Alternate        = ICS.Alternate;
         CS.IAlternate       = ICS.IAlternate;
         CS.Buffer           = ICS.Buffer;
         CS.BufSize          = ICS.BufSize;
         CS.NumInComponents  = ICS.NumInComponents;
         CS.NumOutComponents = ICS.NumOutComponents;
         CS.NumColors        = ICS.NumColors;
         CS.Metadata         = ICS.Metadata;
         CS.MetadataSize     = ICS.MetadataSize;
         CS.IFunction        = ICS.IFunction;
         CS.IAttributes      = ICS.IAttributes;
         CS.IColorSpaceObj   = ICS.IColorSpaceObj;
         CS.Handle           = ICS.Handle;
         if (!IntPtr.Zero.Equals(ICS.BlackPoint))
         {
            CS.BlackPoint = new float[3];
            Marshal.Copy(ICS.BlackPoint, CS.BlackPoint, 0, 3);
         }else
         {
            CS.BlackPoint = null;
         }
         if (!IntPtr.Zero.Equals(ICS.WhitePoint))
         {
            CS.WhitePoint = new float[3];
            Marshal.Copy(ICS.WhitePoint, CS.WhitePoint, 0, 3);
         }else
         {
            CS.WhitePoint = null;
         }
         if (!IntPtr.Zero.Equals(ICS.Gamma))
         {
            CS.Gamma = new float[ICS.NumInComponents];
            Marshal.Copy(ICS.Gamma, CS.Gamma, 0, ICS.NumInComponents);
         }else
         {
            CS.Gamma = null;
         }
         if (!IntPtr.Zero.Equals(ICS.Range))
         {
            if (ICS.Type == TExtColorSpace.esLab)
            {
               CS.Range = new float[4];
               Marshal.Copy(ICS.Range, CS.Range, 0, 4);
            }else
            {
               CS.Range = new float[ICS.NumInComponents * 2];
               Marshal.Copy(ICS.Range, CS.Range, 0, ICS.NumInComponents * 2);
            }
         }else
         {
            CS.Range = null;
         }
         if (!IntPtr.Zero.Equals(ICS.Matrix))
         {
            CS.Matrix = new float[9];
            Marshal.Copy(ICS.Matrix, CS.Matrix, 0, 9);
         }else
         {
            CS.Matrix = null;
         }
         if (ICS.ColorantsCount == 0)
            CS.Colorants = null;
         else
         {
            int len;
            byte[] bytes;
            CS.Colorants = new String[ICS.ColorantsCount];
            // Convert the incoming UTF-8 strings to Unicode
            for(i = 0; i < ICS.ColorantsCount; i++)
            {
               len = pdfStrLenA(ICS.Colorants[i]);
               bytes = new byte[len];
               Marshal.Copy(ICS.Colorants[i], bytes, 0, len);
               bytes = System.Text.Encoding.Convert(System.Text.Encoding.UTF8, System.Text.Encoding.Unicode, bytes);
               CS.Colorants[i] = System.Text.Encoding.Unicode.GetString(bytes);
            }
         }
      }
      
      private void GetIntError(TPDFError_I IErr, ref TPDFError Err)
      {
         Err.Message = _Conv.ToString(IErr.Message, false);
         Err.ObjNum  = IErr.ObjNum;
         Err.Offset  = IErr.Offset;
         Err.SrcFile = _Conv.ToString(IErr.SrcFile, false);
         Err.SrcLine = IErr.SrcLine;
      }

      private void GetIntField(TPDFField_I IField, ref TPDFField Field)
      {
         Field.BackColor   = IField.BackColor;
         Field.BackCS      = IField.BackCS;
         Field.BBox        = IField.BBox;
         Field.BorderColor = IField.BorderColor;
         Field.Checked     = IField.Checked != 0;
         Field.Deleted     = IField.Deleted != 0;
         Field.FieldType   = IField.FieldType;
         Field.Font        = _Conv.ToString(IField.Font, false);
         Field.FontSize    = IField.FontSize;
         Field.Handle      = IField.Handle;
         Field.KidCount    = IField.KidCount;
         Field.Parent      = IField.Parent;
         Field.TextColor   = IField.TextColor;
         Field.TextCS      = IField.TextCS;
         Field.ToolTip     = _Conv.ToString(IField.ToolTip, IField.ToolTipLen, IField.UniToolTip != 0);
         Field.Value       = _Conv.ToString(IField.Value, IField.ValLen, IField.UniVal != 0);
         int len = pdfStrLenA(IField.FieldName);
         Field.FieldName = _Conv.ToString(IField.FieldName, IField.FieldNameLen, len != IField.FieldNameLen);
      }

      private void GetIntFieldEx(TPDFFieldEx_I IField, ref TPDFFieldEx Field)
      {
         Field.Action        = IField.Action;
         Field.ActionType    = IField.ActionType;
         Field.BackColor     = IField.BackColor;
         Field.BackColorSP   = IField.BackColorSP;
         Field.BBox          = IField.BBox;
         Field.BorderColor   = IField.BorderColor;
         Field.BorderColorSP = IField.BorderColorSP;
         Field.BorderStyle   = IField.BorderStyle;
         Field.BorderWidth   = IField.BorderWidth;
         Field.CharSpacing   = IField.CharSpacing;
         Field.CheckBoxChar  = IField.CheckBoxChar;
         Field.Checked       = IField.Checked != 0;
         Field.DefState      = IField.DefState;
         Field.DefValue      = _Conv.ToString(IField.DefValueA, IField.DefValueW);
         Field.Deleted       = IField.Deleted != 0;
         Field.EditFont      = _Conv.ToString(IField.EditFont, false);
         Field.ExpValCount   = IField.ExpValCount;
         Field.ExpValue      = _Conv.ToString(IField.ExpValueA, IField.ExpValueW);
         Field.Events        = IField.Events;
         Field.FieldFlags    = IField.FieldFlags;
         Field.FieldFont     = _Conv.ToString(IField.FieldFont, false);
         Field.FieldName     = _Conv.ToString(IField.FieldNameA, IField.FieldNameW);
         Field.FieldType     = IField.FieldType;
         Field.FmtText       = _Conv.ToString(IField.FmtTextA, IField.FmtTextW);
         Field.FmtTextColor  = IField.FmtTextColor;
         Field.FontSize      = IField.FontSize;
         Field.GroupType     = IField.GroupType;
         Field.Handle        = IField.Handle;
         Field.HighlightMode = IField.HighlightMode;
         Field.IEditFont     = IField.IEditFont;
         Field.IFieldFont    = IField.IFieldFont;
         Field.IsCalcField   = IField.IsCalcField != 0;
         Field.Kids          = CopyIntPtrArray(IField.Kids, IField.KidCount);
         Field.MapName       = _Conv.ToString(IField.MapNameA, IField.MapNameW);
         Field.MaxLen        = IField.MaxLen;
         Field.OC            = IField.OC;
         Field.PageNum       = IField.PageNum;
         Field.Parent        = IField.Parent;
         Field.Rotate        = IField.Rotate;
         Field.TextAlign     = IField.TextAlign;
         Field.TextColor     = IField.TextColor;
         Field.TextColorSP   = IField.TextColorSP;
         Field.TextScaling   = IField.TextScaling;
         Field.ToolTip       = _Conv.ToString(IField.ToolTipA, IField.ToolTipW);
         Field.UniqueName    = _Conv.ToString(IField.UniqueNameA, IField.UniqueNameW);
         Field.Value         = _Conv.ToString(IField.ValueA, IField.ValueW);
         Field.WordSpacing   = IField.WordSpacing;
         Field.PageIndex     = IField.PageIndex;
         Field.IBarcode      = IField.IBarcode;
         Field.ISignature    = IField.ISignature;
         Field.ModDate       = _Conv.ToString(IField.ModDate, false);
         Field.CaptionPos    = IField.CaptionPos;
         Field.DownCaption   = _Conv.ToString(IField.DownCaptionA, IField.DownCaptionW);
         Field.DownImage     = IField.DownImage;
         Field.RollCaption   = _Conv.ToString(IField.RollCaptionA, IField.RollCaptionW);
         Field.RollImage     = IField.RollImage;
         Field.UpCaption     = _Conv.ToString(IField.UpCaptionA, IField.UpCaptionW);
         Field.UpImage       = IField.UpImage;
      }

      private void GetIntFont(TPDFFontObj_I IFont, ref TPDFFontObj F)
      {
         F.Ascent       = IFont.Ascent;
         F.BaseFont     = _Conv.ToString(IFont.BaseFont, false);
         F.CapHeight    = IFont.CapHeight;
         F.DefWidth     = IFont.DefWidth;
         F.Descent      = IFont.Descent;
         F.FirstChar    = IFont.FirstChar;
         F.Flags        = IFont.Flags;
         F.FontFamily   = _Conv.ToString(IFont.FontFamily, IFont.FontFamilyUni != 0);
         F.FontName     = _Conv.ToString(IFont.FontName, false);
         F.FontType     = IFont.FontType;
         F.ItalicAngle  = IFont.ItalicAngle;
         F.LastChar     = IFont.LastChar;
         F.SpaceWidth   = IFont.SpaceWidth;
         F.XHeight      = IFont.XHeight;
         // We don't copy the buffer here! Copy the buffer manually with MemCopy() if you need it!
         // However, if F.Length1 == 0 and if !IntPtr.Zero.Equals(F.FontFile) then the variable contains
         // the file path to the used IFont file. This string is always an Ansi string at this time.
         F.FontFile     = IFont.FontFile;
         F.Length1      = IFont.Length1;
         F.Length2      = IFont.Length2;
         F.Length3      = IFont.Length3;
         F.FontFileType = IFont.FontFileType;
         if (!IntPtr.Zero.Equals(IFont.Encoding))
            F.Encoding = _Conv.ToString(IFont.Encoding, 256, true).ToCharArray();
         else
            F.Encoding = null;
         if (IFont.WidthsCount > 0)
         {
            F.Widths = new float[IFont.WidthsCount];
            Marshal.Copy(IFont.Widths, F.Widths, 0, IFont.WidthsCount);
         }else
            F.Widths = null;
      }

      private void GetIntFontInfo(TPDFFontInfo_I IFont, ref TPDFFontInfo Font)
      {
         Font.Ascent       = IFont.Ascent;
         Font.BaseEncoding = IFont.BaseEncoding;
         Font.BaseFont     = _Conv.ToString(IFont.BaseFont, false);
         Font.CapHeight    = IFont.CapHeight;
         Font.CharSet      = _Conv.ToString(IFont.CharSet, false);
         Font.CIDOrdering  = _Conv.ToString(IFont.CIDOrdering, false);
         Font.CIDRegistry  = _Conv.ToString(IFont.CIDRegistry, false);
         if (!IntPtr.Zero.Equals(IFont.CIDSet))
         {
            Font.CIDSet = new byte[IFont.CIDSetSize];
            Marshal.Copy(IFont.CIDSet, Font.CIDSet, 0, IFont.CIDSetSize);
         }else
            Font.CIDSet = null;
         Font.CIDSupplement = IFont.CIDSupplement;
         if (!IntPtr.Zero.Equals(IFont.CIDToGIDMap))
         {
            Font.CIDToGIDMap = new byte[IFont.CIDToGIDMapSize];
            Marshal.Copy(IFont.CIDToGIDMap, Font.CIDToGIDMap, 0, IFont.CIDToGIDMapSize);
         }else
            Font.CIDToGIDMap = null;
         if (!IntPtr.Zero.Equals(IFont.CMapBuf))
         {
            Font.CMapBuf = new byte[IFont.CMapBufSize];
            Marshal.Copy(IFont.CMapBuf, Font.CMapBuf, 0, IFont.CMapBufSize);
         }else
            Font.CMapBuf = null;
         Font.CMapName = _Conv.ToString(IFont.CMapName, false);
         Font.Descent  = IFont.Descent;
         if (!IntPtr.Zero.Equals(IFont.Encoding))
            Font.Encoding = _Conv.ToString(IFont.Encoding, 256, true).ToCharArray();
         else
            Font.Encoding = null;
         Font.FirstChar = IFont.FirstChar;
         Font.Flags     = IFont.Flags;
         Font.FontBBox  = IFont.FontBBox;
         if (!IntPtr.Zero.Equals(IFont.FontBuffer))
         {
            Font.FontBuffer = new byte[IFont.FontBufSize];
            Marshal.Copy(IFont.FontBuffer, Font.FontBuffer, 0, IFont.FontBufSize);
         }else
            Font.FontBuffer = null;
         Font.FontFamily   = _Conv.ToString(IFont.FontFamilyA, IFont.FontFamilyW);
         Font.FontFilePath = _Conv.ToString(IFont.FontFilePathA, IFont.FontFilePathW);
         Font.FontFileType = IFont.FontFileType;
         Font.FontName     = _Conv.ToString(IFont.FontName, false);
         Font.FontStretch  = _Conv.ToString(IFont.FontStretch, false);
         Font.FontType     = IFont.FontType;
         Font.FontWeight   = IFont.FontWeight;
         Font.FullName     = _Conv.ToString(IFont.FullNameA, IFont.FullNameW);
         Font.HaveEncoding = IFont.HaveEncoding != 0;
         if (IFont.HorzWidthsCount > 0)
         {
            Font.HorzWidths = new float[IFont.HorzWidthsCount];
            Marshal.Copy(IFont.HorzWidths, Font.HorzWidths, 0, IFont.HorzWidthsCount);
         }else
            Font.HorzWidths = null;
         Font.Imported    = IFont.Imported != 0;
         Font.ItalicAngle = IFont.ItalicAngle;
         Font.Lang        = _Conv.ToString(IFont.Lang, false);
         Font.LastChar    = IFont.LastChar;
         Font.Leading     = IFont.Leading;
         Font.Length1     = IFont.Length1;
         Font.Length2     = IFont.Length2;
         Font.Length3     = IFont.Length3;
         Font.MaxWidth    = IFont.MaxWidth;
         if (!IntPtr.Zero.Equals(IFont.Metadata))
         {
            Font.Metadata = new byte[IFont.MetadataSize];
            Marshal.Copy(IFont.Metadata, Font.Metadata, 0, IFont.MetadataSize);
         }else
            Font.Metadata = null;
         Font.MisWidth = IFont.MisWidth;
         if (!IntPtr.Zero.Equals(IFont.Panose))
         {
            Font.Panose = new byte[12];
            Marshal.Copy(IFont.Panose, Font.Panose, 0, 12);
         }else
            Font.Panose = null;
         Font.PostScriptName = _Conv.ToString(IFont.PostScriptNameA, IFont.PostScriptNameW);
         Font.SpaceWidth     = IFont.SpaceWidth;
         Font.StemH          = IFont.StemH;
         Font.StemV          = IFont.StemV;
         if (!IntPtr.Zero.Equals(IFont.ToUnicode))
         {
            Font.ToUnicode = new byte[IFont.ToUnicodeSize];
            Marshal.Copy(IFont.ToUnicode, Font.ToUnicode, 0, IFont.ToUnicodeSize);
         }else
            Font.ToUnicode = null;
         Font.VertDefPos = IFont.VertDefPos;
         if (IFont.VertWidthsCount > 0)
         {
            int i;
            CCIDMetric pt = new CCIDMetric();
            Font.VertWidths = new TCIDMetric[IFont.VertWidthsCount];
            long ptr = IFont.VertWidths.ToInt64();
            for (i = 0; i < IFont.VertWidthsCount; i++)
            {
               Marshal.PtrToStructure(new IntPtr(ptr), pt);
               Font.VertWidths[i] = pt;
               ptr += 12;
            }
         }else
            Font.VertWidths = null;
         Font.WMode   = IFont.WMode;
         Font.XHeight = IFont.XHeight;
      }

      private void GetIntOutputIntent(ref TPDFOutputIntent_I IIntent, ref TPDFOutputIntent Intent)
      {
         if (IIntent.BufSize > 0)
         {
            Intent.Buffer = new byte[IIntent.BufSize];
            Marshal.Copy(IIntent.Buffer, Intent.Buffer, 0, IIntent.BufSize);
         }else
            Intent.Buffer = null;
         Intent.Info              = _Conv.ToString(IIntent.InfoA, IIntent.InfoW);
         Intent.NumComponents     = IIntent.NumComponents;
         Intent.OutputCondition   = _Conv.ToString(IIntent.OutputConditionA, IIntent.OutputConditionW);
         Intent.OutputConditionID = _Conv.ToString(IIntent.OutputConditionIDA, IIntent.OutputConditionIDW);
         Intent.RegistryName      = _Conv.ToString(IIntent.RegistryNameA, IIntent.RegistryNameW);
         Intent.SubType           = _Conv.ToString(IIntent.SubType, false);
      }

      private void GetIntSigDict(TPDFSigDict_I ISD, ref TPDFSigDict SD)
      {
         if (ISD.ByteRangeCount > 0)
         {
            SD.ByteRange = new int[ISD.ByteRangeCount * 2];
            Marshal.Copy(ISD.ByteRange, SD.ByteRange, 0, ISD.ByteRangeCount * 2);
         }else
            SD.ByteRange = null;
         if (ISD.CertLen > 0)
         {
            SD.Cert = new byte[ISD.CertLen];
            Marshal.Copy(ISD.Cert, SD.Cert, 0, ISD.CertLen);
         }else
            SD.Cert = null;
         if (!IntPtr.Zero.Equals(ISD.Changes))
         {
            SD.Changes = new int[3];
            Marshal.Copy(ISD.Changes, SD.Changes, 0, 3);
         }else
            SD.Changes = null;
         SD.ContactInfo = _Conv.ToString(ISD.ContactInfoA, ISD.ContactInfoW);
         if (ISD.ContentsSize > 0)
         {
            SD.Contents = new byte[ISD.ContentsSize];
            Marshal.Copy(ISD.Contents, SD.Contents, 0, ISD.ContentsSize);
         }else
            SD.Contents = null;
         SD.Filter       = _Conv.ToString(ISD.Filter, false);
         SD.Location     = _Conv.ToString(ISD.LocationA, ISD.LocationW);
         SD.Name         = _Conv.ToString(ISD.NameA, ISD.NameW);
         SD.PropAuthTime = ISD.PropAuthTime;
         SD.PropAuthType = _Conv.ToString(ISD.PropAuthType, false);
         SD.Reason       = _Conv.ToString(ISD.ReasonA, ISD.ReasonW);
         SD.Revision     = ISD.Revision;
         SD.SignTime     = _Conv.ToString(ISD.SignTime, false);
         SD.SubFilter    = _Conv.ToString(ISD.SubFilter, false);
         SD.Version      = ISD.Version;
      }

      public void GetOptimizeParams(TOptimizeParams Parms, bool IncludeCSArray, ref TOptimizeParams_I Out)
      {
         Out = null;
         if (Parms != null)
         {
            Out = new TOptimizeParams_I();
            Out.StructSize = Marshal.SizeOf(Out);
            if (Parms.ExcludeCS != null && Parms.ExcludeCS.Length > 0)
            {
               Out.ExcludeCS = Marshal.AllocHGlobal(Parms.ExcludeCS.Length * 4);
               Marshal.Copy(Parms.ExcludeCS, 0, Out.ExcludeCS, Parms.ExcludeCS.Length);
               Out.ExcludeCSCount = Parms.ExcludeCS.Length;
               if (IncludeCSArray) Out.ExcludeCSCount = -Out.ExcludeCSCount;
            }
            Out.Filter1Bit     = Parms.Filter1Bit;
            Out.FilterColor    = Parms.FilterColor;
            Out.FilterGray     = Parms.FilterGray;
            Out.JP2KQuality    = Parms.JP2KQuality;
            Out.JPEGQuality    = Parms.JPEGQuality;
            Out.Min1BitRes     = Parms.Min1BitRes;
            Out.MinColorRes    = Parms.MinColorRes;
            Out.MinGrayRes     = Parms.MinGrayRes;
            Out.MinLineWidth   = Parms.MinLineWidth;
            Out.Res1BitImages  = Parms.Res1BitImages;
            Out.ResColorImages = Parms.ResColorImages;
            Out.ResGrayImages  = Parms.ResGrayImages;
         }
      }

      private bool IsNum(int c)
      {
         c -= 48;
         return (c < 10 && c > -1);
      }

      private int ParseInt(byte[] Str, int Pos, ref int Value)
      {
         bool isNegative = false;
         int c, p = Pos;

         Value = 0;
         if (Pos >= Str.Length) return 0;
         switch(Str[Pos])
         {
            case 45: isNegative = true; goto case 43;
            case 43: ++Pos; break;
         }
         while (Pos < Str.Length)
         {
            c = Str[Pos];
            if (!IsNum(c)) break;
            Value = Value * 10 + c - 48;
            ++Pos;
         }
         if (isNegative) Value = -Value;
         return (Pos - p);
      }

      internal bool                m_ExtInstance;
      private IntPtr               m_Instance;
      // We must save the pointer of a callback function because the .Net framework changes the pointer otherwise
      // when it reallocates memory the next time.
      private TErrorProc           m_ErrorProc;
      private TEnumFontProc        m_EnumFontProc;
      private TEnumFontProc2       m_EnumFontProc2;
      private TEnumFontProcEx      m_EnumFontProcEx;
      private TOnFontNotFoundProc  m_FontNotFoundProc;
      private TInitProgressProc    m_InitProgressProc;
      private TOnPageBreakProc     m_OnPageBreakProc;
      private TOnReplaceICCProfile m_OnReplaceICCProfile;
      private TProgressProc        m_ProgressProc;

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TBookmark_I
      {
         public uint      Color;
         public int       DestPage;
         public TPDFRect  DestPos;
         public TDestType DestType;
         public lbool     Open;
         public int       Parent;
         public TBmkStyle Style;
         public IntPtr    Title;
         public int       TitleLen;
         public lbool     Unicode;
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFAnnotation_I
      {
         public TAnnotType      Type;
         public lbool           Deleted;
         public TPDFRect        BBox;
         public double          BorderWidth;
         public uint            BorderColor;
         public TBorderStyle    BorderStyle;
         public uint            BackColor;
         public int             Handle;
         public IntPtr          AuthorA;
         public IntPtr          AuthorW;
         public IntPtr          ContentA;
         public IntPtr          ContentW;
         public IntPtr          NameA;
         public IntPtr          NameW;
         public IntPtr          SubjectA;
         public IntPtr          SubjectW;
         public int             PageNum;
         public THighlightMode  HighlightMode;
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFAnnotationEx_I
      {
         public TAnnotType      Type;
         public lbool           Deleted;
         public TPDFRect        BBox;
         public float           BorderWidth;
         public uint            BorderColor;
         public TBorderStyle    BorderStyle;
         public uint            BackColor;
         public int             Handle;
         public IntPtr          AuthorA;
         public IntPtr          AuthorW;
         public IntPtr          ContentA;
         public IntPtr          ContentW;
         public IntPtr          NameA;
         public IntPtr          NameW;
         public IntPtr          SubjectA;
         public IntPtr          SubjectW;
         public int             PageNum;
         public THighlightMode  HighlightMode;
         // Page link annotations only
         public int             DestPage;
         public TPDFRect        DestPos;
         public TDestType       DestType;
         // File link or web link annotations
         public IntPtr          DestFile;
         public int             Icon;             // The Icon type depends on the annotation type. If the annotation type is atText then the Icon
                                                  // is of type TAnnotIcon. If the annotation type is atFileAttach then it is of type
                                                  // TFileAttachIcon. If the annotation type is atStamp then the Icon is the stamp type (TRubberStamp).
                                                  // For any other annotation type this value is not set (-1).
         public IntPtr          StampName;        // Set only, if Icon == rsUserDefined
         public uint            AnnotFlags;       // See TAnnotFlags for available flags
         public IntPtr          CreateDate;       // Creation Date -> Optional
         public IntPtr          ModDate;          // Modification Date -> Optional
         public lbool           Grouped;          // (Reply type) Meaningful only if Parent != -1 and Type != atPopUp. If true,
                                                  // the annotation is part of an annotation group. Properties like Content, CreateDate,
                                                  // ModDate, BackColor, Subject, and Open must be taken from the parent annotation.
         public lbool           Open;             // Meaningful only for annotations which have a corresponding PopUp annotation.
         public int             Parent;           // Parent annotation handle of a PopUp Annotation or the parent annotation if
                                                  // this annotation represents a state of a base annotation. In this case,
                                                  // the annotation type is always atText and only the following members should
                                                  // be considered:
                                                  //    State      // The current state
                                                  //    StateModel // Marked, Review, and so on
                                                  //    CreateDate // Creation Date
                                                  //    ModDate    // Modification Date
                                                  //    Author     // The user who has set the state
                                                  //    Content    // Not displayed in Adobe's Acrobat...
                                                  //    Subject    // Not displayed in Adobe's Acrobat...
                                                  // The PopUp annotation of a text annotation which represent an Annotation State
                                                  // must be ignored.
         public int             PopUp;            // Handle of the corresponding PopUp annotation if any.
         public IntPtr          State;            // The state of the annotation.
         public IntPtr          StateModel;       // The state model (Marked, Review, and so on).
         public int             EmbeddedFile;     // FileAttach annotations only. A handle of an embedded file -> GetEmbeddedFile().
         public IntPtr          Subtype;          // Set only, if Type == atUnknownAnnot
         public int             PageIndex;        // The page index is used to sort form fields. See SortFieldsByIndex().
         public int             MarkupAnnot;      // If true, the annotation is a markup annotation. Markup annotations can be flattened
                                                  // separately, see FlattenAnnots().
         public float           Opacity;          // Opacity = 1.0 = Opaque, Opacity < 1.0 = Transparent, Markup annotations only
         public IntPtr          QuadPoints;       // Highlight, Link, and Redact annotations only
         public int             QuadPointsCount;  // Highlight, Link, and Redact annotations only

         public IntPtr          DashPattern;      // Only present if BorderStyle == bsDashed
         public int             DashPatternCount; // Number of values in the array

         public IntPtr          Intent;           // Markup annotations only. The intent allows to distinguish between different uses of an annotation.
                                                  // For example, line annotations have two intents: LineArrow and LineDimension.
         public TLineEndStyle   LE1;              // Line end style of the start point -> Line and PolyLine annotations only
         public TLineEndStyle   LE2;              // Line end style of the end point -> Line and PolyLine annotations only
         public IntPtr          Vertices;         // Line, PolyLine, and Polygon annotations only
         public int             VerticesCount;    // Number of values in the array. This is the raw number of floating point values.
                                                  // Since a vertice requires always two coordinate pairs, the number of vertices
                                                  // or points is VerticeCount divided by 2.
         // Line annotations only. These properties should only be considered if the member Intent is set to the string LineDimension.
         public lbool           Caption;          // If true, the annotation string Content is used as caption. The string is shown in a PopUp annotation otherwise.
         public float           CaptionOffsetX;   // Horizontal offset of the caption from its normal position
         public float           CaptionOffsetY;   // Vertical offset of the caption from its normal position
         public TLineCaptionPos CaptionPos;       // The position where the caption should be drawn if present
         public float           LeaderLineLen;    // Length of the leader lines (positive or negative)
         public float           LeaderLineExtend; // Optional leader line extend beyond the leader line (must be a positive value or zero)
         public float           LeaderLineOffset; // Amount of space between the endpoints of the annotation and the leader lines (must be a positive value or zero)

         public TBorderEffect   BorderEffect;     // Circle, Square, FreeText, and Polygon annotations.
         public IntPtr          RichStyle;        // Optional default style string.      -> FreeText annotations only.
         public IntPtr          RichText;         // Optional rich text string (RC key). -> Markup annotations only.

         public IntPtr          InkList;          // Ink annotations only. Array of array. The sub arrays can be accessed with GetInkList().
         public int             InkListCount;     // Number of ink arrays.
         public int             OC;               // Handle of an OCG or OCMD or -1 if not set. See help file for further information.

         public IntPtr          RD;               // Caret, Circle, Square, and FreeText annotations.
         public int             Rotate;           // Caret annotations only. Must be zero or a multiple of 90. This key is not documented in the specs.
      }

      [StructLayout(LayoutKind.Sequential, Pack = 0)]
      private struct TPDFBarcode_I
      {
         public int    StructSize;   // Must be set to sizeof(TPDFBarcode) before calling CreateBarcodeField()!
         public IntPtr CaptionA;     // Optional, the ansi string takes precedence
         public IntPtr CaptionW;     // Optional
         public float  ECC;          // 0..8 for PDF417, or 0..3 for QRCode
         public float  Height;       // Height in inches
         public float  nCodeWordCol; // Required for PDF417. The number of codewords per barcode coloumn.
         public float  nCodeWordRow; // Required for PDF417. The number of codewords per barcode row.
         public int    Resolution;   // Required -> Should be 300
         public IntPtr Symbology;    // PDF417, QRCode, or DataMatrix.
         public float  Version;      // Should be 1
         public float  Width;        // Width in inches
         public float  XSymHeight;   // Only needed for PDF417. The vertical distance between two barcode modules,
                                     // measured in pixels. The ratio XSymHeight/XSymWidth shall be an integer
                                     // value. For PDF417, the acceptable ratio range is from 1 to 4. For QRCode
                                     // and DataMatrix, this ratio shall always be 1.
         public float XSymWidth;     // Required -> The horizontal distance, in pixels, between two barcode modules.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFBookmark_I
      {
         public int       StructSize;    // Must be set to sizeof(TPDFBookmark)
         public int       Action;        // Action handle or -1 if not set.
         public uint      Color;         // Optional text color. Black is the default value (0).
         public int       DestPage;      // Destination page.
         public TPDFRect  DestPos;       // Destination position.
         public TDestType DestType;      // Destination type.
         public IntPtr    NamedDestA;    // Optional name of a named destination. If set, DestPage, DestPos, and DestType were already taken from the destination if it was found.
         public IntPtr    NamedDestW;    // Either the Unicode or Ansi string is set but never both.
         public lbool     Open;          // If true, child nodes should be visible.
         public int       Parent;        // The immediate parent node or -1 for a root node.
         public IntPtr    StructElem;    // Bookmarks can contain a reference to a structure element. There is no API to access structure elements yet.
         public TBmkStyle Style;         // The font style that should be used to render this node.
         public IntPtr    TitleA;        // The bookmark title.
         public IntPtr    TitleW;        // Either the Unicode or Ansi string is set but never both.
         public int       TitleLen;      // String length in characters.
      };

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFCollectionInfo_I
      {
         public int              StructSize; // Must be set to sizeof(TPDFColectionInfo)
         public int              InitalFile; // Possible values are -2, -1, or a valid embedded files handle (a value greater or equal zero). See help file for further information.
         public TColView         InitalView; // Determines how embedded files should be displayed.
         public IntPtr           SortBy;     // Optional name of the first field that should be used to sort the list of embedded files.
         public lbool            SortDesc;   // If true, the list of embedded files should be sorted in descending order.
         public TPDFColSplitInfo SplitInfo;  // Determines whether a splitter bar should be displayed.
         public float            SplitPos;   // Meaningful only, if SplitInfo is not spiNone. The initial position of the splitter bar,
                                             // specified as a percentage of the available window area (0 to 100).
      };

      [StructLayout(LayoutKind.Sequential, Pack = 0)]
      private struct TPDFChoiceValue_I
      {
         public int    StructSize; // Must be set to sizeof(TPDFChoiceValue)
         public IntPtr ExpValueA;
         public IntPtr ExpValueW;
         public int    ExpValueLen;
         public IntPtr ValueA;
         public IntPtr ValueW;
         public int    ValueLen;
         public lbool  Selected;
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private class CCIDMetric
      {
         public float Width;
         public float x;
         public float y;
         public static implicit operator TCIDMetric(CCIDMetric v)
         {
            TCIDMetric retval;
            retval.Width = v.Width;
            retval.x = v.x;
            retval.y = v.y;
            return retval;
         }
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFColorSpaceObj_I
      {
         public TExtColorSpace Type;
         public TExtColorSpace Alternate;        // Alternate color space or base space of an Indexed or Pattern color space.
         public IntPtr         IAlternate;       // Only set if the color space contains an alternate or base color space -> GetColorSpaceObjEx().
         public IntPtr         Buffer;           // Contains either an ICC profile or the color table of an Indexed color space.
         public int            BufSize;          // Buffer length in bytes.
         public IntPtr         BlackPoint;       // CIE blackpoint. If set, the array contains exactly 3 values.
         public IntPtr         WhitePoint;       // CIE whitepoint. If set, the array contains exactly 3 values.
         public IntPtr         Gamma;            // If set, one value per component.
         public IntPtr         Range;            // min/max for each component or for the .a and .b components of a Lab color space.
         public IntPtr         Matrix;           // XYZ matrix. If set, the array contains exactly 9 values.
         public int            NumInComponents;  // Number of input components.
         public int            NumOutComponents; // Number of output components.
         public int            NumColors;        // HiVal + 1 as specified in the color space. Indexed color space only.
         [MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
         public IntPtr[]       Colorants;        // UTF-8 Colorant names (Separation, DeviceN, and NChannel only).
         public int            ColorantsCount;   // The number of colorants in the array.
         public IntPtr         Metadata;         // Optional XMP metadata stream -> ICCBased only.
         public int            MetadataSize;     // Metadata length in bytes.
         public IntPtr         IFunction;        // Pointer to function object -> Separation, DeviceN, and NChannel only.
         public IntPtr         IAttributes;      // Optional attributes of DeviceN or NChannel color spaces -> GetNChannelAttributes().
         public IntPtr         IColorSpaceObj;   // Pointer of the corresponding color space object
         public int            Handle;           // Handle of the color space object
         public IntPtr         Reserved01;
         public IntPtr         Reserved02;
         public IntPtr         Reserved03;
         public IntPtr         Reserved04;
         public IntPtr         Reserved05;
         public IntPtr         Reserved06;
         public IntPtr         Reserved07;
         public IntPtr         Reserved08;
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TDeviceNAttributes_I
      {
         public IntPtr IProcessColorSpace;    // Pointer to process color space or NULL -> GetColorSpaceEx().
                                              // Does a process color space with more than 8 components exist? 6 components
                                              // is the maximum so far I know. However, 8 components should be large enough for
                                              // the next years...
         [MarshalAs(UnmanagedType.ByValArray, SizeConst=8)]
         public IntPtr[] ProcessColorants; // UTF-8 strings
         public int    ProcessColorantsCount; // Number of process colorants in the array or zero if not set.
         [MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
         public IntPtr[] Separations; // Optional pointers to Separation color spaces -> GetColorSpaceEx().
         public int    SeparationsCount;      // Number of color spaces in the array.
         public IntPtr IMixingHints;          // Optional mixing hints. There is no API function at this time to access mixing hints.
         public IntPtr Reserved01;
         public IntPtr Reserved02;
         public IntPtr Reserved03;
         public IntPtr Reserved04;
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFError_I
      {
         public int    StructSize; // Must be initialized to sizeof(TPDFError)
         public IntPtr Message;    // The error message
         public int    ObjNum;     // -1 if not available
         public int    Offset;     // -1 if not available
         public IntPtr SrcFile;    // Source file
         public int    SrcLine;    // Source line
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFField_I
      {
         public TFieldType     FieldType;
         public lbool          Deleted;
         public TPDFRect       BBox;
         public int            Handle;
         public IntPtr         FieldName;
         public int            FieldNameLen;
         public TPDFColorSpace BackCS;
         public TPDFColorSpace TextCS;
         public uint           BackColor;
         public uint           BorderColor;
         public uint           TextColor;
         public lbool          Checked;
         public int            Parent;
         public int            KidCount;
         public IntPtr         Font;
         public double         FontSize;
         public IntPtr         Value;
         public lbool          UniVal;
         public int            ValLen;
         public IntPtr         ToolTip;
         public lbool          UniToolTip;
         public int            ToolTipLen;
      }

      
      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFFieldEx_I
      {
         public int             StructSize;    // Must be set to sizeof(TPDFFieldEx)
         public lbool           Deleted;       // If true, the field was marked as deleted by DeleteField()
         public TPDFRect        BBox;          // Bounding box of the field in bottom-up coordinates
         public TFieldType      FieldType;     // Field type
         public TFieldType      GroupType;     // If GroupType != FieldType the field is a terminal field of a field group
         public int             Handle;        // Field handle
         public uint            BackColor;     // Background color
         public TExtColorSpace  BackColorSP;   // Color space of the background color
         public uint            BorderColor;   // Border color
         public TExtColorSpace  BorderColorSP; // Color space of the border color
         public TBorderStyle    BorderStyle;   // Border style
         public float           BorderWidth;   // Border width
         public float           CharSpacing;   // Text fields only
         public lbool           Checked;       // Check boxes only
         public int             CheckBoxChar;  // ZapfDingbats character that is used to display the on state
         public TCheckBoxState  DefState;      // Check boxes only
         public IntPtr          DefValueA;     // Optional default value
         public IntPtr          DefValueW;     // Optional default value
         public IntPtr          IEditFont;     // Pointer to default editing font
         public IntPtr          EditFont;      // Postscript name of the editing font
         public int             ExpValCount;   // Combo and list boxes only. The values can be accessed with GetFieldExpValueEx()
         public IntPtr          ExpValueA;     // Check boxes only
         public IntPtr          ExpValueW;     // Check boxes only
         public TFieldFlags     FieldFlags;    // Field flags
         public IntPtr          IFieldFont;    // Pointer to the font that is used by the field
         public IntPtr          FieldFont;     // Postscript name of the font
         internal int           Reserved;
         public double          FontSize;      // Font size. 0.0 means auto font size
         public IntPtr          FieldNameA;    // Note that children of a field group or radio button have no name
         public IntPtr          FieldNameW;    // Field name length in characters
         public THighlightMode  HighlightMode; // Highlight mode
         public lbool           IsCalcField;   // If true, the OnCalc event of the field is connected with a JavaScript action
         public IntPtr          MapNameA;      // Optional unique mapping name of the field
         public IntPtr          MapNameW;      // MapName length in characters
         public int             MaxLen;        // Text fields only -> zero means not restricted
         public IntPtr          Kids;          // Array of child fields -> GetFieldEx2()
         public int             KidCount;      // Number of fields in the array
         public IntPtr          Parent;        // Pointer to parent field or NULL
         public int             PageNum;       // Page on which the field is used or -1
         public int             Rotate;        // Rotation angle in degrees
         public TTextAlign      TextAlign;     // Text fields only
         public uint            TextColor;     // Text color
         public TExtColorSpace  TextColorSP;   // Color space of the field's text
         public float           TextScaling;   // Text fields only
         public IntPtr          ToolTipA;      // Optional tool tip
         public IntPtr          ToolTipW;      // Optional tool tip
         public IntPtr          UniqueNameA;   // Optional unique name (NM key)
         public IntPtr          UniqueNameW;   // Optional unique name (NM key)
         public IntPtr          ValueA;        // Field value
         public IntPtr          ValueW;        // Field value
         public float           WordSpacing;   // Text fields only
         public int             PageIndex;     // Array index to change the tab order, see SortFieldsByIndex().
         public IntPtr          IBarcode;      // If present, this field is a barcode field. The field type is set to ftText
                                               // since barcode fields are extended text fields. -> GetBarcodeDict().
         public IntPtr          ISignature;    // Signature fields only. Present only for imported signature fields which
                                               // which have a value. That means the file was digitally signed. -> GetSigDict().
                                               // Signed signature fields are always marked as deleted!
         public IntPtr          ModDate;       // Last modification date (optional)
         // Push buttons only. The down and roll over states are optional. If not present, then all states use the up state.
         // The handles of the up, down, and roll over states are template handles! The templates can be opened for editing
         // with EditTemplate2() and parsed with ParseContent().
         public TBtnCaptionPos  CaptionPos;    // Where to position the caption relative to its image
         public IntPtr          DownCaptionA;  // Caption of the down state
         public IntPtr          DownCaptionW;  // Caption of the down state
         public int             DownImage;     // Image of the down state
         public IntPtr          RollCaptionA;  // Caption of the roll over state
         public IntPtr          RollCaptionW;  // Caption of the roll over state
         public int             RollImage;     // Image of the roll over state
         public IntPtr          UpCaptionA;    // Caption of the up state
         public IntPtr          UpCaptionW;    // Caption of the up state
         public int             UpImage;       // Image of the up state -> if > -1, the button is an image button
         public int             OC;            // Handle of an OCG or OCMD or -1 if not set. See help file for further information.
         public int             Action;        // Action handle or -1 if not set. This action is executed when the field is activated.
         public TActionType     ActionType;    // Meaningful only, if Action >= 0.
         public IntPtr          Events;        // See GetObjEvent() if set.
         public int             Reserved2;     // Reserved field to avoid alignment errors
         public uint            FmtTextColor;  // Zero if not set. Usually RGB red otherwise.
         public IntPtr          FmtTextA;      // Text fields only. The formatted field value if the field contains an OnFormat Javascript action.
         public IntPtr          FmtTextW;      // Text fields only. The formatted field value if the field contains an OnFormat Javascript action.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFFileSpec_I
      {
         public IntPtr Buffer;      // Buffer of an embedded file
         public int    BufSize;     // Buffer size in bytes.
         public lbool  Compressed;  // Should be false if Decompress was true in the GetEmbeddedFile() call, otherwise usually true.
                                    // DynaPDF decompresses Flate encoded streams only. Other filters can occur but this is very unusual.
         public IntPtr ColItem;     // If != IntPtr.Zero the embedded file contains a collection item with user defined data. This entry
                                    // can occur in PDF Collections only (PDF 1.7). See "PDF Collections" in the help file for further
                                    // information.
         public IntPtr Name;        // Name of the file specification in the name tree. This value is always present.
         public lbool  NameUnicode; // Is Name in Unicode format?
         public IntPtr FileName;    // File name as 7 bit ASCII string.
         public lbool  IsURL;       // If true, FileName contains a URL.
         public IntPtr UF;          // PDF 1.7. Same as FileName but Unicode is allowed.
         public lbool  UFUnicode;   // Is UF in Unicode format?
         public IntPtr Desc;        // Description
         public lbool  DescUnicode; // Is Desc in Unicode format?
         public int    FileSize;    // Size of the decompressed stream or zero if not known. Note: this is either the Size key of
                                    // the Params dictionary if present or the DL key in the file stream. Whether this value is
                                    // correct depends on the file creator! The parameter is definitely correct if the file was
                                    // decompressed.
         public IntPtr MIMEType;    // MIME media type name as defined in Internet RFC 2046.
         public IntPtr CreateDate;  // Creation date as string. See help file "The standard date format".
         public IntPtr ModDate;     // Modification date as string. See help file "The standard date format".
         public IntPtr CheckSum;    // 16 byte MD5 digest. Note that this is a binary string. It is exactly 16 bytes long if set!
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFEmbFileNode_I
      {
         public int            StructSize; // Must be set to sizeof(TPDFEmbFileNode).
         public IntPtr         Name;       // UTF-8 encoded name. This key contains usually a 7 bit ASCII string.
         public TPDFFileSpec_I EF;         // Embedded file.
         public IntPtr         NextNode;   // Next node if any.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFFileSpecEx_I
      {
         public int    StructSize;     // Must be set to sizeof(TPDFFileSpecEx).
         public IntPtr AFRelationship; // PDF 2.0
         public IntPtr ColItem;        // If != NULL the embedded file contains a collection item with user defined data. This entry can
                                       // occur in PDF Collections only (PDF 1.7). See "PDF Collections" in the help file for further information.
         public IntPtr DescriptionA;   // Optional description string.
         public IntPtr DescriptionW;   // Optional description string.
         public IntPtr DOS;            // Optional DOS file name.
         public IntPtr EmbFileNode;    // GetEmbeddedFileNode().
         public IntPtr FileName;       // File name as 7 bit ASCII string.
         public lbool  FileNameIsURL;  // If true, FileName contains a URL.
         public IntPtr ID1;            // Optional file ID. Meaningful only if FileName points to a PDF file.
         public int    ID1Len;         // String length in bytes.
         public IntPtr ID2;            // Optional file ID. Meaningful only if FileName points to a PDF file.
         public int    ID2Len;         // String length in bytes.
         public lbool  IsVolatile;     // If true, the file changes frequently with time.
         public IntPtr Mac;            // Optional Mac file name.
         public IntPtr Unix;           // Optional Unix file name.
         public IntPtr RelFileNode;    // Optional related files array. -> GetRelFileNode().
         public IntPtr Thumb;          // Optional thumb nail image. -> GetImageObjEx().
         public IntPtr UFileNameA;     // PDF 1.7. Same as FileName but Unicode is allowed.
         public IntPtr UFileNameW;     // Either the Ansi or Unicode string is set but never both.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFFontInfo_I
      {
         public int              StructSize;       // Must be set to sizeof(TPDFFontInfo).
         public float            Ascent;           // Ascent (optional).
         public float            AvgWidth;         // Average character width (optional).
         public TBaseEncoding    BaseEncoding;     // Valid only if HaveEncoding is true.
         public IntPtr           BaseFont;         // PostScript Name or Family Name.
         public float            CapHeight;        // Cap height (optional).
         public IntPtr           CharSet;          // The charset describes which glyphs are present in the font.
         public int              CharSetSize;      // Length of the CharSet in bytes.
         public IntPtr           CIDOrdering;      // SystemInfo -> A string that uniquely names the character collection within the specified registry.
         public IntPtr           CIDRegistry;      // SystemInfo -> Issuer of the character collection.
         public IntPtr           CIDSet;           // CID fonts only. This is a table of bits indexed by CIDs.
         public int              CIDSetSize;       // Length of the CIDSet in bytes.
         public int              CIDSupplement;    // CIDSystemInfo -> The supplement number of the character collection.
         public IntPtr           CIDToGIDMap;      // Allowed for embedded TrueType based CID fonts only.
         public int              CIDToGIDMapSize;  // Length of the stream in bytes.
         public IntPtr           CMapBuf;          // Only available if the CMap was embedded.
         public int              CMapBufSize;      // Buffer size in bytes.
         public IntPtr           CMapName;         // CID fonts only (this is the encoding if the CMap is not embedded).
         public float            Descent;          // Descent (optional).
         public IntPtr           Encoding;         // Unicode mapping 0..255 -> not available for CID fonts.
         public int              FirstChar;        // First char (simple fonts only).
         public TPDFFontFlags    Flags;            // The font flags describe various characteristics of the font. See help file for further information.
         public TBBox            FontBBox;         // This is the size of the largest glyph in this font. The bounding box is important for text selection.
         public IntPtr           FontBuffer;       // The font buffer is present if the font was embedded or if it was loaded from a file buffer.
         public int              FontBufSize;      // Font file size in bytes.
         public IntPtr           FontFamilyA;      // Optional Font Family (Family Name, always available for system fonts).
         public IntPtr           FontFamilyW;      // Optional Font Family (either the Ansi or Unicode string is set, but never both).
         public IntPtr           FontFilePathA;    // Only available for system fonts.
         public IntPtr           FontFilePathW;    // Either the Ansi or Unicode path is set, but never both.
         public TFontFileSubtype FontFileType;     // See description in the help file for further information.
         public IntPtr           FontName;         // Font name (should be the same as BaseFont).
         public IntPtr           FontStretch;      // Optional -> UltraCondensed, ExtraCondensed, Condensed, and so on.
         public TFontType        FontType;         // If ftType0 the font is a CID font. The Encoding is not set in this case.
         public float            FontWeight;       // Font weight (optional).
         public IntPtr           FullNameA;        // System fonts only.
         public IntPtr           FullNameW;        // System fonts only (either the Ansi or Unicode string is set, but never both).
         public lbool            HaveEncoding;     // If true, BaseEncoding was set from the font's encoding.
         public IntPtr           HorzWidths;       // Horizontal glyph widths -> 0..HorzWidthsCount -1.
         public int              HorzWidthsCount;  // Number of horizontal widths in the array.
         public lbool            Imported;         // If true, the font was imported from an external PDF file.
         public float            ItalicAngle;      // Italic angle.
         public IntPtr           Lang;             // Optional language code defined by BCP 47.
         public int              LastChar;         // Last char (simple fonts only).
         public float            Leading;          // Leading (optional).
         public int              Length1;          // Length of the clear text portion of a Type1 font.
         public int              Length2;          // Length of the encrypted portion of a Type1 font program (Type1 fonts only).
         public int              Length3;          // Length of the fixed-content portion of a Type1 font program or zero if not present.
         public float            MaxWidth;         // Maximum glyph width (optional).
         public IntPtr           Metadata;         // Optional XMP stream that contains metadata about the font file.
         public int              MetadataSize;     // Buffer size in bytes.
         public float            MisWidth;         // Missing width (default = 0.0).
         public IntPtr           Panose;           // CID fonts only -> Optional 12 bytes long Panose string as described in Microsoft’s TrueType 1.0 Font Files Technical Specification.
         public IntPtr           PostScriptNameA;  // System fonts only.
         public IntPtr           PostScriptNameW;  // System fonts only (either the Ansi or Unicode string is set, but never both).
         public float            SpaceWidth;       // Space width in font units. A default value is set if the font contains no space character.
         public float            StemH;            // The thickness, measured vertically, of the dominant horizontal stems of glyphs in the font.
         public float            StemV;            // The thickness, measured horizontally, of the dominant vertical stems of glyphs in the font.
         public IntPtr           ToUnicode;        // Only available for imported fonts. This is an embedded CMap that translates PDF strings to Unicode.
         public int              ToUnicodeSize;    // Buffer size in bytes.
         public TFltPoint        VertDefPos;       // Default vertical displacement vector.
         public IntPtr           VertWidths;       // Vertical glyph widths -> 0..VertWidthsCount -1.
         public int              VertWidthsCount;  // Number of vertical widths in the array.
         public int              WMode;            // Writing Mode -> 0 == Horizontal, 1 == Vertical.
         public float            XHeight;          // The height of lowercase letters (like the letter x), measured from the baseline, in fonts that have Latin characters.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFFontObj_I
      {
         public float            Ascent;        // Ascent
         public IntPtr           BaseFont;      // PostScript Name or Family Name
         public float            CapHeight;     // Cap height
         public float            Descent;       // Descent
         public IntPtr           Encoding;      // Unicode mapping 0..255 -> not set if a CID font is selected
         public int              FirstChar;     // First char
         public TPDFFontFlags    Flags;         // Font flags -> font descriptor
         public IntPtr           FontFamily;    // Optional Font Family (Family Name)
         public lbool            FontFamilyUni; // Is FontFamily in Unicode format?
         public IntPtr           FontName;      // Font name -> font descriptor
         public TFontType        FontType;      // If ftType0 the font is a CID font. The Encoding is not set in this case.
         public float            ItalicAngle;   // Italic angle
         public int              LastChar;      // Last char
         public float            SpaceWidth;    // Space width in font units. A default value is set if the font contains no space character.
         public IntPtr           Widths;        // Glyph widths -> 0..WidthsCount -1
         public int              WidthsCount;   // Number of widths in the array
         public float            XHeight;       // x-height
         public float            DefWidth;      // Default character widths -> CID fonts only
         public IntPtr           FontFile;      // Font file buffer -> only imported fonts are returned.
         public int              Length1;       // Length of the clear text portion of the Type1 font, or the length of the entire font program if FontType != ffType1.
         public int              Length2;       // Length of the encrypted portion of the Type1 font program (Type1 fonts only).
         public int              Length3;       // Length of the fixed-content portion of the Type1 font program or zero if not present.
         public TFontFileSubtype FontFileType;  // See description in the help file for further information.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private class TPDFGlyphOutline_I
      {
         public float  AdvanceX;
         public float  AdvanceY;
         public float  OriginX;
         public float  OriginY;
         public Int16  Lsb;
         public Int16  Tsb;
         public lbool  HaveBBox;
         public TFRect BBox;
         public IntPtr Outline;
         public int    Size;
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFGoToAction_I
      {
         public int         StructSize;     // Must be set to sizeof(TPDFGoToAction).
         public int         DestPage;       // Destination page (the first page is denoted by 1).
         public IntPtr      DestPos;        // Destination position -> Array of 4 floating point values if set.
         public TDestType   DestType;       // Destination type.
         // GoToR (GoTo Remote) actions only:
         public IntPtr      DestFile;       // see GetFileSpec().
         public IntPtr      DestNameA;      // Optional named destination that shall be loaded when opening the file.
         public IntPtr      DestNameW;      // Either the Ansi or Unicode string is set but never both.
         public int         NewWindow;      // Meaningful only if the destination file points to a PDF file.
                                            // -1 = viewer default, 0 = false, 1 = true.
         public int         NextAction;     // -1 or next action handle to be executed if any.
         public TActionType NextActionType; // Only set if NextAction is >= 0.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFHdrFtr_I
      {
         public uint           Color;      // Text color.
         public TExtColorSpace CS;         // Color space -> default = esInitSpace.
         public int            CSHandle;   // Optional color space handle if CS is set to a non-device space.
         public TCodepage      Codepage;   // The code page that should be used to load the font. It is always set to cpUnicode if the property was loaded with LoadHeaderFooterSettings().
         public lbool          Embed;      // If true, the font will be embedded. Always set to false if the property was loaded with LoadHeaderFooterSettings().
         private int           Reserved1;  // Placeholder to avoid alignment issues
         public IntPtr         FontA;      // Optional font name.
         public IntPtr         FontW;      // Optional font name.
         public float          FontSize;   // Font size -> considered only if a font name is set.
         private int           Reserved2;  // Placeholder to avoid alignment issues
         public IntPtr         FontType;   // Optional font type ("TrueType", "OpenType", or "Type1").
         public lbool          IsHeader;   // If true, the record is treated as header.
         public TTextAlign     Position;   // Valid values are taLeft, taCenter, and taRight.
         public float          ShiftX;     // Positive values move the text to the right, negative to the left.
         public float          ShiftY;     // Positive values move the text up or down depending on the used coordinate system (top down or bottom up). Negative values move the text into the opposite direction.
         public IntPtr         TextA;      // The text that should be output. Either the Ansi or Unicode string must be set.
         public IntPtr         TextW;      // The text that should be output. Either the Ansi or Unicode string must be set.
         public int            TextLen;    // Text length in characters.
         private int           Reserved3;  // Placeholder to avoid alignment issues
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFHeaderFooter_I
      {
         public int             StructSize;      // Must be set to sizeof(TPDFHeaderFooter).
         public int             FirstPage;       // In/Out -> The first output page.
         public TPDFHdrFtrFlags Flags;           // In/Out -> Additional processing flags.
         public uint            InitColor;       // In/Out -> Initial text color.
         public TExtColorSpace  InitCS;          // In/Out -> Initial color space -> default = esDeviceGray.
         public int             InitCSHandle;    // In     -> Optional color space handle if InitCS is set to a non-device space.
         public IntPtr          InitDate;        // Out    -> Initial date format.
         public TCodepage       InitCodepage;    // In/Out -> The code page that should be used to load the font. It is always set to cpUnicode if the property was loaded with LoadHeaderFooterSettings().
         public lbool           InitEmbed;       // In     -> If true, the font will be embedded. Always set to false if the property was loaded with LoadHeaderFooterSettings().
         public IntPtr          InitFontA;       // In/Out -> Optional font name.
         public IntPtr          InitFontW;       // In/Out -> Optional font name.
         public float           InitFontSize;    // In/Out -> Optional font size. Considered only if a font name is set.
         private int            Reserved;        // Placeholder to avoid alignment issues
         public IntPtr          InitFontType;    // Out    -> Optional font type ("TrueType", "OpenType", or "Type1").
         public IntPtr          InitPageFormatA; // Out    -> Initial page format string.
         public IntPtr          InitPageFormatW; // Out    -> Initial page format string.
         public int             LastPage;        // In/Out -> The last output page.
         public TFltRect        Margin;          // In/Out -> Margin to output the header/footer text.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFHideAction_I
      {
         public int         StructSize;     // Must be set to sizeof(TPDFHideAction).
         public IntPtr      Fields;         // Array of field pointers -> GetFieldEx2().
         public int         FieldsCount;    // Number of fields in the array.
         public lbool       Hide;           // A flag indicating whether to hide or show the fields in the array.
         public int         NextAction;     // -1 or next action handle to be executed if any.
         public TActionType NextActionType; // Only set if NextAction is >= 0.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFImportDataAction_I
      {
         public int              StructSize;     // Must be set to sizeof(TPDFImportDataAction).
         public TPDFFileSpecEx_I Data;           // The data or file to be loaded.
         public int              NextAction;     // -1 or next action handle to be executed if any
         public TActionType      NextActionType; // Only set if NextAction is >= 0.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFJavaScriptAction_I
      {
         public int         StructSize;     // Must be set to sizeof(TPDFJavaScriptAction).
         public IntPtr      ScriptA;        // The script.
         public IntPtr      ScriptW;        // Either the Ansi or Unicode string is set but never both.
         public int         ScriptLen;      // Script length in characters, not bytes!
         public int         NextAction;     // -1 or next action handle to be executed if any.
         public TActionType NextActionType; // Only set if NextAction is >= 0.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFLaunchAction_I
      {
         public int         StructSize;     // Must be set to sizeof(TPDFLaunchAction).
         public IntPtr      AppName;        // Optional. The name of the application that should be launched.
         public IntPtr      DefDir;         // Optional default directory.
         public IntPtr      File;           // see GetFileSpec().
         public int         NewWindow;      // -1 = viewer default, 0 = false, 1 = true.
         public int         NextAction;     // -1 or next action handle to be executed if any.
         public TActionType NextActionType; // Only set if NextAction is >= 0.
         public IntPtr      Operation;      // Optional string specifying the operation to perform (open or print).
         public IntPtr      Parameter;      // Optional parameter string that shall be passed to the application (AppName).
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFMeasure_I
      {
         public int         StructSize;    // Must be set to sizeof(TPDFMeasure)
         public lbool       IsRectilinear; // If true, the members of the rectilinear measure dictionary are set. The geospatial members otherwise.
         /* --- Rectilinear measure dictionary --- */
         public IntPtr      Angles;        // Number format array to measure angles -> GetNumberFormatObj()
         public int         AnglesCount;   // Number of objects in the array.
         public IntPtr      Area;          // Number format array to measure areas -> GetNumberFormatObj()
         public int         AreaCount;     // Number of objects in the array.
         public float       CXY;           // Optional, meaningful only when Y is present.
         public IntPtr      Distance;      // Number format array to measure distances -> GetNumberFormatObj()
         public int         DistanceCount; // Number of objects in the array.
         public float       OriginX;       // Origin of the measurement coordinate system.
         public float       OriginY;       // Origin of the measurement coordinate system.
         public IntPtr      RA;            // A text string expressing the scale ratio of the drawing.
         public IntPtr      RW;            // A text string expressing the scale ratio of the drawing.
         public IntPtr      Slope;         // Number format array for measurement of the slope of a line -> GetNumberFormatObj()
         public int         SlopeCount;    // Number of objects in the array.
         public IntPtr      X;             // Number format array for measurement of change along the x-axis and, if Y is not present, along the y-axis as well.
         public int         XCount;        // Number of objects in the array.
         public IntPtr      Y;             // Number format array for measurement of change along the y-axis.
         public int         YCount;        // Number of objects in the array.

         /* --- Geospatial measure dictionary --- */
         public IntPtr      Bounds;        // Array of numbers taken pairwise to describe the bounds for which geospatial transforms are valid.
         public int         BoundCount;    // Number of values in the array. Should be a multiple of two.

         // The DCS coordinate system is optional.
         public lbool       DCS_IsSet;     // If true, the DCS members are set.
         public lbool       DCS_Projected; // If true, the DCS values contains a pojected coordinate system.
         public int         DCS_EPSG;      // Optional, either EPSG or WKT is set.
         public IntPtr      DCS_WKT;       // Optional ASCII string

         // The GCS coordinate system is required and should be present.
         public lbool       GCS_Projected; // If true, the GCS values contains a pojected coordinate system.
         public int         GCS_EPSG;      // Optional, either EPSG or WKT is set.
         public IntPtr      GCS_WKT;       // Optional ASCII string

         public IntPtr      GPTS;          // Required, an array of numbers that shall be taken pairwise, defining points in geographic space as degrees of latitude and longitude, respectively.
         public int         GPTSCount;     // Number of values in the array.
         public IntPtr      LPTS;          // Optional, an array of numbers that shall be taken pairwise to define points in a 2D unit square.
         public int         LPTSCount;     // Number of values in the array.

         public IntPtr      PDU1;          // Optional preferred linear display units.
         public IntPtr      PDU2;          // Optional preferred area display units.
         public IntPtr      PDU3;          // Optional preferred angular display units.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFMovieAction_I
      {
         public int          StructSize;     // Must be set to sizeof(TPDFMovieAction).
         public int          Annot;          // Optional. The movie annotation handle identifying the movie that shall be played.
         [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
         public float[]      FWPosition;     // FWPosition
         [MarshalAs(UnmanagedType.ByValArray, SizeConst=2)]
         public int[]        FWScale;        // FWScale
         public IntPtr       Mode;           // Mode
         public int          NextAction;     // -1 or next action handle to be executed if any
         public TActionType  NextActionType; // Only set if NextAction is >= 0.
         public IntPtr       Operation;      // Operation
         public float        Rate;           // Rate
         public lbool        ShowControls;   // ShowControls
         public lbool        Synchronous;    // Synchronous
         public IntPtr       TitleA;         // The title of a movie annotation that shall be played. Either Annot or Title should be set, but not both.
         public IntPtr       TitleW;         // Either the Ansi or Unicode string is set at time.
         public float        Volume;         // Volume
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFNamedAction_I
      {
         public int          StructSize;     // Must be set to sizeof(TPDFNamedAction).
         public IntPtr       Name;           // Only set if Type == naUserDefined.
         public int          NewWindow;      // -1 = viewer default, 0 = false, 1 = true.
         public int          NextAction;     // -1 or next action handle to be executed if any.
         public TActionType  NextActionType; // Only set if NextAction is >= 0.
         public TNamedAction Type;           // Known pre-defined actions.
      }

      // Named Destination
      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFNamedDest_I
      {
         public int       StructSize;
         public IntPtr    NameA;
         public IntPtr    NameW;
         public int       NameLen;
         public IntPtr    DestFileA;
         public IntPtr    DestFileW;
         public int       DestFileLen;
         public int       DestPage;
         public TPDFRect  DestPos;
         public TDestType DestType;
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFNumberFormat_I
      {
         public int               StructSize;
         public float             C;
         public int               D;
         public TMeasureNumFormat F;
         public lbool             FD;
         public TMeasureLblPos    O;
         public IntPtr            PSA;
         public IntPtr            PSW;
         public IntPtr            RDA;
         public IntPtr            RDW;
         public IntPtr            RTA;
         public IntPtr            RTW;
         public IntPtr            SSA;
         public IntPtr            SSW;
         public IntPtr            UA;
         public IntPtr            UW;
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFOCLayerConfig_I
      {
         public int        StructSize; // Must be set to sizeof(TOCLayerConfig)
         public TOCGIntent Intent;     // Possible values oiDesign, oiView, or oiAll.
         public lbool      IsDefault;  // If true, this is the default configuration.
         public IntPtr     NameA;      // Optional configuration name. The default config has usually no name but all others should have one.
         public IntPtr     NameW;      // Either the Ansi or Unicode string is set at time but never both.
         public int        NameLen;    // Length in characters.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private class TPDFOCUINode_I
      {
         public int    StructSize;  // Must be set to sizeof(TOCUINode)
         public IntPtr LabelA;      // Optional label.
         public IntPtr LabelW;      // Either the Ansi or Unicode string is set at time but never both.
         public int    LabelLength; // Length in characters.
         public IntPtr NextChild;   // If set, the next child node that must be loaded.
         public lbool  NewNode;     // If true, a new child node must be created.
         public int    OCG;         // Optional OCG handle. -1 if not set -> GetOCG().
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFOutputIntent_I
      {
         public int    StructSize;
         public IntPtr Buffer;
         public int    BufSize;
         public IntPtr InfoA;
         public IntPtr InfoW;
         public int    NumComponents;
         public IntPtr OutputConditionA;
         public IntPtr OutputConditionW;
         public IntPtr OutputConditionIDA;
         public IntPtr OutputConditionIDW;
         public IntPtr RegistryNameA;
         public IntPtr RegistryNameW;
         public IntPtr SubType;
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFPageLabel_I
      {
         public int              StartRange;
         public TPageLabelFormat Format;
         public int              FirstPageNum;
         public IntPtr           Prefix;
         public int              PrefixLen;
         public lbool            PrefixUni;
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFPrintSettings_I
      {
         public TDuplexMode   DuplexMode;
         public int           NumCopies;         // -1 means not set. Values larger than 5 are ignored in viewer applications.
         public int           PickTrayByPDFSize; // -1 means not set. 0 == false, 1 == true.
         public IntPtr        PrintRanges;       // If set, the array contains PrintRangesCount * 2 values. Each pair consists
                                                 // of the first and last pages in the sub-range. The first page in the PDF file
                                                 // is denoted by 0.
         public int           PrintRangesCount;  // Number of ranges available in PrintRanges.
         public TPrintScaling PrintScaling;      // dpmNone means not set.

         /* Reserved fields for future extensions*/
         internal int Reserved0;
         internal int Reserved1;
         internal int Reserved2;
         internal int Reserved3;
         internal int Reserved4;
         internal int Reserved5;
         internal int Reserved6;
         internal int Reserved7;
         internal int Reserved8;
         internal int Reserved9;
         
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFRelFileNode_I
      {
         public int            StructSize; // Must be set to sizeof(TPDFRelFileNode).
         public IntPtr         NameA;      // Name of this file spcification.
         public IntPtr         NameW;      // Either the Ansi or Unicode name is set but never both.
         public TPDFFileSpec_I EF;         // Embedded file.
         public IntPtr         NextNode;   // Next node if any.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFResetFormAction_I
      {
         public int    StructSize;  // Must be set to sizeof(TResetFormAction)
         public IntPtr Fields;      // Array of field pointers -> GetFieldEx2().
         public int    FieldsCount; // Number of fields in the array.
         public lbool  Include;     // If true, the fields in the Fields array must be reset. If false, these fields must be excluded.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFSubmitFormAction_I
      {
         public int          StructSize;  // Must be set to sizeof(TSubmitFormAction)
         public IntPtr       CharSet;     // Optional charset in which the form should be submitted.
         public IntPtr       Fields;      // Array of field pointers -> GetFieldEx2().
         public int          FieldsCount; // Number of fields in the array
         public TSubmitFlags Flags;       // Various flags, see CreateSubmitAction() for further information.
         public IntPtr       URL;         // The URL of the script at the Web server that will process the submission.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFSigDict_I
      {
         public int        StructSize;     // Must be set to sizeof(TPDFSigDict).
         public IntPtr     ByteRange;      // ByteRange -> Byte offset followed by the corresponding length.
                                           // The byte ranges are required to create the digest. The values
                                           // are returned as is. So, you must check whether the offsets and
                                           // length values are valid. There are normally at least two ranges.
                                           // Overlapping ranges are not allowed! Any error breaks processing
                                           // and the signature should be considered as invalid.
         public int        ByteRangeCount; // The number of Offset / Length pairs. ByteRange contains 2 * ByteRangeCount values!
         public IntPtr     Cert;           // X.509 Certificate when SubFilter is adbe.x509.rsa_sha1.
         public int        CertLen;        // Length in bytes
         public IntPtr     Changes;        // If set, an array of three integers that specify changes to the
                                           // document that have been made between the previous signature and
                                           // this signature in this order: the number of pages altered, the
                                           // number of fields altered, and the number of fields filled in.
         public IntPtr     ContactInfoA;   // Optional contact info string, e.g. an email address
         public IntPtr     ContactInfoW;   // Optional contact info string, e.g. an email address
         public IntPtr     Contents;       // The signature. This is either a DER encoded PKCS#1 binary data
                                           // object or a DER-encoded PKCS#7 binary data object depending on
                                           // the used SubFilter.
         public int        ContentsSize;   // Length in bytes.
         public IntPtr     Filter;         // The name of the security handler, usually Adobe.PPKLite.
         public IntPtr     LocationA;      // Optional location of the signer
         public IntPtr     LocationW;      // Optional location of the signer
         public IntPtr     SignTime;       // Date/Time string
         public IntPtr     NameA;          // Optional signers name
         public IntPtr     NameW;          // Optional signers name
         public int        PropAuthTime;   // Optional -> The number of seconds since the signer was last authenticated.
         public IntPtr     PropAuthType;   // Optional -> The method that shall be used to authenticate the signer.
                                           // Valid values are PIN, Password, and Fingerprint.
         public IntPtr     ReasonA;        // Optional reason
         public IntPtr     ReasonW;        // Optional reason
         public int        Revision;       // Optional -> The version of the signature handler that was used to create
                                           // the signature.
         public IntPtr     SubFilter;      // A name that describes the encoding of the signature value. Should be
                                           // adbe.x509.rsa_sha1, adbe.pkcs7.detached, or adbe.pkcs7.sha1.
         public int        Version;        // The version of the signature dictionary format.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFSigParms_I
      {
         public int           StructSize;   // Must be set to sizeof(TSignParms)
         public int           PKCS7ObjLen;  // The maximum length of the signed PKCS#7 object
         public THashType     HashType;     // If set to htDetached, the bytes ranges of the PDF file will be returned.
         public IntPtr        Range1;       // Out -> Contains either the hash or the first byte range to create a detached signature
         public int           Range1Len;    // Out -> Length of the buffer
         public IntPtr        Range2;       // Out -> Set only if HashType == htDetached
         public int           Range2Len;    // Out -> Length of the buffer
         [MarshalAs(UnmanagedType.LPStr)]
         public String        ContactInfoA; // Optional, e.g. an email address
         [MarshalAs(UnmanagedType.LPWStr)]
         public String        ContactInfoW; // Optional, e.g. an email address
         [MarshalAs(UnmanagedType.LPStr)]
         public String        LocationA;    // Optional location of the signer
         [MarshalAs(UnmanagedType.LPWStr)]
         public String        LocationW;    // Optional location of the signer
         [MarshalAs(UnmanagedType.LPStr)]
         public String        ReasonA;      // Optional reason why the file was signed
         [MarshalAs(UnmanagedType.LPWStr)]
         public String        ReasonW;      // Optional reason why the file was signed
         [MarshalAs(UnmanagedType.LPStr)]
         public String        SignerA;      // Optional, the issuer of the certificate takes precedence
         [MarshalAs(UnmanagedType.LPWStr)]
         public String        SignerW;      // Optional, the issuer of the certificate takes precedence
         public lbool         Encrypt;      // If true, the file will be encrypted
         // These members will be ignored if Encrypt is set to false
         [MarshalAs(UnmanagedType.LPStr)]
         public String        OpenPwd;      // Open password
         [MarshalAs(UnmanagedType.LPStr)]
         public String        OwnerPwd;     // Owner password to change the security settings
         public TKeyLen       KeyLen;       // Key length to be used to encrypt the file
         public TRestrictions Restrict;     // What should be restricted?
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFSysFont_I
      {
         public int                StructSize;      // Must be set to sizeof(TPDFSysFont)
         public TFontBaseType      BaseType;        // Font type
         public IntPtr             CIDOrdering;     // OpenType CID fonts only
         public IntPtr             CIDRegistry;     // OpenType CID fonts only
         public int                CIDSupplement;   // OpenType CID fonts only
         public int                DataOffset;      // Data offset
         public IntPtr             FamilyName;      // Family name
         public IntPtr             FilePathA;       // Font file path
         public IntPtr             FilePathW;       // Font file path
         public int                FileSize;        // File size in bytes
         public TEnumFontProcFlags Flags;           // Bitmask
         public IntPtr             FullName;        // Full name
         public int                Length1;         // Length of the clear text portion of a Type1 font
         public int                Length2;         // Length of the eexec encrypted binary portion of a Type1 font
         public IntPtr             PostScriptNameA; // Postscript mame
         public IntPtr             PostScriptNameW; // Postscript mame
         public int                Index;           // Zero based font index if the font is stored in a TrueType collection
         public lbool              IsFixedPitch;    // If true, the font is a fixed pitch font. A proprtional font otherwise.
         public TFStyle            Style;           // Font style
         public TUnicodeRange1     UnicodeRange1;   // Bitmask
         public TUnicodeRange2     UnicodeRange2;   // Bitmask
         public TUnicodeRange3     UnicodeRange3;   // Bitmask
         public TUnicodeRange4     UnicodeRange4;   // Bitmask
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFURIAction_I
      {
         public int         StructSize;     // Must be set to sizeof(TPDFURIAction)
         public IntPtr      BaseURL;        // Optional, if defined in the Catalog object.
         public lbool       IsMap;          // A flag specifying whether to track the mouse position when the URI is resolved: e.g. http://test.org?50,70.
         public IntPtr      URI;            // Uniform Resource Identifier.
         public int         NextAction;     // -1 or next action handle to be executed if any.
         public TActionType NextActionType; // Only set if NextAction is >= 0.
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFVersionInfo_I
      {
         public int    StructSize;      // Must be set to sizeof(TPDFVersionInfo)
         public int    Major;           // PDF major version, e.g. 1
         public int    Minor;           // PDF minor version, e.g. 7
         public IntPtr MainVer;         // Set only if the file conforms to a specific PDF standard, e.g. "PDF/X 1a:2003", "PDF/A 3b", and so on.
         public IntPtr SubVer;          // e.g. "ZUGFeRD 2.0", "Factur-X", and so on.
         // PDF/A
         public IntPtr PDFAConformance; // e.g. A, B, E, F, U, or UA.
         public int    PDFAVersion;     // If greater zero, this is a PDF/A file.
         // PDF/E
         public IntPtr PDFEVersion;     // e.g. PDF/E-1
         // PDF/VT
         public IntPtr PDFVTModDate;    // Modification date, e.g. 2016-05-07T15:42:23+01:00
         public IntPtr PDFVTVersion;    // e.g. PDF/VT-2
         // PDF/X
         public IntPtr PDFXConformance; // e.g. PDF/X-1a:2001 or PDF/X-1a:2003. PDF/X 3 and above do not set this key anymore.
         public IntPtr PDFXVersion;     // e.g. PDF/X-1:2001, PDF/X-1:2003, and so on.
         // ZUGFeRD, Factur-X, XRechnung
         public IntPtr FXConfLevel;     // ConformanceLevel
         public IntPtr FXDocName;       // DocumentFileName
         public IntPtr FXDocType;       // DocumentType
         public IntPtr FXVersion;       // Version
         public int    VersionConst;    // If >= 0 this is the TPDFVersion constant as integer. If the version is not supported by DynaPDF, this member is set to -1.
      };

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private struct TPDFViewport_I
      {
         public int      StructSize; // Must be set to sizeof(TPDFViewport)
         public TFltRect BBox;       // Bounding box
         public IntPtr   Measure;    // Optional -> GetMeasureObj()
         public IntPtr   NameA;      // Optional name
         public IntPtr   NameW;      // Optional name
         public IntPtr   PtData;     // Pointer of a Point Data dictionary. The value can be accessed with GetPtDataObj().
      }

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      private class TPDFXFAStream_I
      {
         public int    StructSize; // Must be set to sizeof(TPDFXFAStream_I)
         public IntPtr Buffer;
         public int    BufSize;
         public IntPtr NameA;
         public IntPtr NameW;
      }

      // The Unicode version of a function takes precedence over the Ansi version in the .Net framework.
      // This behaviour makes it impossible to declare overloaded string functions as "Auto" because only
      // the Unicode version would be called.
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         fntBuildFamilyNameAndStyle(IntPtr IFont, System.Text.StringBuilder Name, ref TFStyle Style);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         fntGetFont(IntPtr IFont, ref TPDFFontObj_I F);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         fntGetFontInfo(IntPtr IFont, ref TPDFFontInfo_I F);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern double        fntGetSpaceWidth(IntPtr IFont, double FontSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern double        fntGetTextWidth(IntPtr IFont, IntPtr Text, int Len, float CharSpacing, float WordSpacing, float TextScale);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern int           fntTranslateRawCode(IntPtr IFont, IntPtr Text, int Len, ref double Width, byte[] OutText, ref int OutLen, ref lbool Decoded, float CharSpacing, float WordSpacing, float TextScale);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern int           fntTranslateString(ref TPDFStack Stack, byte[] Buffer, int Size, int Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern int           fntTranslateString2(IntPtr IFont, IntPtr Text, int Len, byte[] OutText, int Size, int Flags);

      /* ------------------------------------------- Rendering API  ------------------------------------------- */

      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          rasAbort(IntPtr RasPtr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         rasAttachImageBuffer(IntPtr RasPtr, IntPtr Rows, IntPtr Buffer, int Width, int Height, int ScanlineLen);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          rasCalcPagePixelSize(IntPtr PagePtr, TPDFPageScale DefScale, float Scale, int FrameWidth, int FrameHeight, TRasterFlags Flags, ref int Width, ref int Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern IntPtr        rasCreateRasterizer(IntPtr IPDF, IntPtr Rows, IntPtr Buffer, int Width, int Height, int ScanlineLen, TPDFPixFormat PixFmt);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern IntPtr        rasCreateRasterizerEx(IntPtr IPDF, IntPtr DC, int Width, int Height, TPDFPixFormat PixFmt);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          rasDeleteRasterizer(ref IntPtr RasPtr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         rasGetWidthHeight(IntPtr PagePtr, TRasterFlags Flags, ref float Width, ref float Height, int Rotate, ref IntPtr BBox);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          rasRedraw(IntPtr RasPtr, IntPtr DC, int DestX, int DestY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         rasResizeBitmap(IntPtr RasPtr, IntPtr DC, int Width, int Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          rasSetMinLineWidth2(IntPtr RasPtr, float Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          rasSetScreenRes(IntPtr RasPtr, int Value);

      /* -------------------------------------------- Page Cache  --------------------------------------------- */

      [StructLayout(LayoutKind.Sequential, Pack=0)]
      public class CDummyAction
      {
         public int Dummy;
      }

      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern void           rasChangeBackColor(IntPtr CachePtr, uint Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern void           rasCloseFile(IntPtr CachePtr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern IntPtr         rasCreatePageCache(IntPtr IPDF, TPDFPixFormat PixFmt, int HBorder, int VBorder, uint BackColor);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern void           rasDeletePageCache(ref IntPtr CachePtr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern TUpdBmkAction  rasExecBookmark(IntPtr CachePtr, int Handle, ref int NewX, ref int NewY, ref float NewZoom, ref TPDFPageScale NewPageScale, CDummyAction Action /* Not defined yet */);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern int            rasGetCurrPage(IntPtr CachePtr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern float          rasGetCurrZoom(IntPtr CachePtr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern TPageLayout    rasGetDefPageLayout(IntPtr CachePtr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern int            rasGetPageAt(IntPtr CachePtr, int ScrollX, int ScrollY, int X, int Y);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern TPageLayout    rasGetPageLayout(IntPtr CachePtr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern int            rasGetPageMatrix(IntPtr CachePtr, int CursorX, int CursorY, ref int DestX, ref int DestY, ref int Width, ref int Height, ref TCTM Matrix);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern TPDFPageScale  rasGetPageScale(IntPtr CachePtr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern int            rasGetRotate(IntPtr CachePtr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern int            rasGetScrollLineDelta(IntPtr CachePtr, lbool Vertical);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern int            rasGetScrollPos(IntPtr CachePtr, lbool Vertical, int PageNum);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern lbool          rasGetScrollRange(IntPtr CachePtr, lbool Vertical, ref int Max, ref int SmallChange, ref int LargeChange);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern int            rasInitBaseObjects(IntPtr CachePtr, int Width, int Height, TInitCacheFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern lbool          rasInitColorManagement(IntPtr CachePtr, TPDFColorProfiles Profiles, TPDFInitCMFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern lbool          rasInitialize(IntPtr CachePtr, TPDFThreadPriority Priority);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern TPDFCursor     rasMouseDown(IntPtr CachePtr, int X, int Y);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern TUpdScrollbar  rasMouseMove(IntPtr CachePtr, IntPtr hWnd, lbool LeftBtnDown, ref int ScrollX, ref int ScrollY, int X, int Y);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern TUpdScrollbar  rasPaint(IntPtr CachePtr, IntPtr DC, ref int ScrollX, ref int ScrollY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern void           rasResetMousePos(IntPtr CachePtr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern lbool          rasResize(IntPtr CachePtr, int Width, int Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern lbool          rasProcessErrors(IntPtr CachePtr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern TUpdScrollbar  rasScroll(IntPtr CachePtr, lbool Vertical, int ScrollCode, ref int ScrollX, ref int ScrollY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]    public static extern int            rasSetCMapDir(IntPtr CachePtr, String Path, TLoadCMapFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern void           rasSetDefPageLayout(IntPtr CachePtr, TPageLayout Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern void           rasSetMinLineWidth(IntPtr CachePtr, float Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern lbool          rasSetOCGState(IntPtr CachePtr, int Handle, lbool On, lbool SaveState);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern void           rasSetOnPaintCallback(IntPtr CachePtr, IntPtr UserData, TOnUpdateWindow Callback);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern void           rasSetPageLayout(IntPtr CachePtr, TPageLayout Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern void           rasSetPageScale(IntPtr CachePtr, TPDFPageScale Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern void           rasSetRotate(IntPtr CachePtr, int Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern lbool          rasSetScrollLineDelta(IntPtr CachePtr, lbool Vertical, int Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern void           rasSetThreadPriority(IntPtr CachePtr, TPDFThreadPriority UpdateThread, TPDFThreadPriority RenderThread);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    public static extern lbool          rasZoom(IntPtr CachePtr, float Value, ref int HorzPos, ref int VertPos);

      /* ------------------------------------------------------------------------------------------------------ */


      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfActivateAltFontList(IntPtr IPDF, int Handle, int EnableSysFonts);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddActionToObj(IntPtr IPDF, TObjType ObjType, TObjEvent Event, int ActHandle, int ObjHandle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddAnnotToPage(IntPtr IPDF, int PageNum, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAddArticle(IntPtr IPDF, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAddBookmarkA(IntPtr IPDF, String Title, int Parent, int DestPage, lbool Open);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfAddBookmarkW(IntPtr IPDF, String Title, int Parent, int DestPage, lbool Open);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAddBookmarkExA(IntPtr IPDF, String Title, int Parent, int NamedDest, lbool Open);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfAddBookmarkExW(IntPtr IPDF, String Title, int Parent, int NamedDest, lbool Open);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAddBookmarkEx2AA(IntPtr IPDF, String Title, int Parent, String NamedDest, lbool Unicode, lbool Open);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAddBookmarkEx2AW(IntPtr IPDF, String Title, int Parent, [MarshalAs(UnmanagedType.LPWStr)]String NamedDest, lbool Unicode, lbool Open);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfAddBookmarkEx2WA(IntPtr IPDF, String Title, int Parent, [MarshalAs(UnmanagedType.LPStr)]String NamedDest, lbool Unicode, lbool Open);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfAddBookmarkEx2WW(IntPtr IPDF, String Title, int Parent, String NamedDest, lbool Unicode, lbool Open);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfAddButtonImage(IntPtr IPDF, int BtnHandle, TButtonState State, String Caption, String ImgFile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddButtonImageEx(IntPtr IPDF, int BtnHandle, TButtonState State, String Caption, IntPtr hBitmap);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddButtonImageEx2(IntPtr IPDF, int BtnHandle, TButtonState State, String Caption, byte[]Buffer, int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddButtonImageEx2I(IntPtr IPDF, int BtnHandle, TButtonState State, String Caption, IntPtr Buffer, int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddContinueTextA(IntPtr IPDF, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfAddContinueTextW(IntPtr IPDF, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddDeviceNProcessColorants(IntPtr IPDF, int DeviceNCS, String[]Colorants, int NumColorants, TExtColorSpace ProcessCS, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddDeviceNSeparations(IntPtr IPDF, int DeviceNCS, String[]Colorants, int[]SeparationCS, int NumColorants);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAddDPartNode(IntPtr IPDF, int Parent, int FirstPage, int LastPage, String DPM);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddFieldToFormAction(IntPtr IPDF, int Action, int Field, lbool Include);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddFieldToHideAction(IntPtr IPDF, int HideAct, int Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddFileCommentA(IntPtr IPDF, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfAddFileCommentW(IntPtr IPDF, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAddFontSearchPathA(IntPtr IPDF, String APath, lbool Recursive);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfAddFontSearchPathW(IntPtr IPDF, String APath, lbool Recursive);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddHeaderFooter(IntPtr IPDF, ref TPDFHeaderFooter_I Value, [MarshalAs(UnmanagedType.LPArray), In()]TPDFHdrFtr_I[]HFArray, int Count);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddImage(IntPtr IPDF, TCompressionFilter Filter, TImageConversionFlags Flags, ref TPDFImage Image);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddInkList(IntPtr IPDF, int InkAnnot, TFltPoint[]Points, int NumPoints);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAddJavaScriptA(IntPtr IPDF, String Name, String Script);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfAddJavaScriptW(IntPtr IPDF, [MarshalAs(UnmanagedType.LPStr)]String Name, [MarshalAs(UnmanagedType.LPWStr)]String Script);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfAddLayerToDisplTreeA(IntPtr IPDF, IntPtr Parent, int Layer, String Title);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern IntPtr        pdfAddLayerToDisplTreeW(IntPtr IPDF, IntPtr Parent, int Layer, String Title);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddMaskImage(IntPtr IPDF, int BaseImage, byte[]Buffer, int BufSize, int Stride, int BitsPerPixel, int Width, int Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddMaskImage(IntPtr IPDF, int BaseImage, IntPtr Buffer, int BufSize, int Stride, int BitsPerPixel, int Width, int Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddObjectToLayer(IntPtr IPDF, int OCG, TOCObject ObjType, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddOCGToAppEvent(IntPtr IPDF, int Handle, TOCAppEvent Events, TOCGUsageCategory Categories);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAddOutputIntentA(IntPtr IPDF, String ICCFile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfAddOutputIntentW(IntPtr IPDF, String ICCFile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAddOutputIntentEx(IntPtr IPDF, byte[]Buffer, int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAddPageLabelA(IntPtr IPDF, int StartRange, TPageLabelFormat Format, String Prefix, int AddNum);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfAddPageLabelW(IntPtr IPDF, int StartRange, TPageLabelFormat Format, String Prefix, int AddNum);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddRasImage(IntPtr IPDF, IntPtr RasPtr, TCompressionFilter Filter);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAddRenderingIntentA(IntPtr IPDF, String ICCFile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfAddRenderingIntentW(IntPtr IPDF, String ICCFile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAddRenderingIntentEx(IntPtr IPDF, byte[]Buffer, int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAddValToChoiceFieldA(IntPtr IPDF, int Field, String ExpValue, String Value, lbool Selected);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfAddValToChoiceFieldW(IntPtr IPDF, int Field, [MarshalAs(UnmanagedType.LPStr)]String ExpValue, [MarshalAs(UnmanagedType.LPWStr)]String Value, lbool Selected);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAppend(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfApplyAppEvent(IntPtr IPDF, TOCAppEvent Event, lbool SaveResult);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfApplyPattern(IntPtr IPDF, int PattHandle, TColorMode ColorMode, uint Color);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfApplyShading(IntPtr IPDF, int ShadHandle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAssociateEmbFile(IntPtr IPDF, TAFDestObject DestObject, int DestHandle, TAFRelationship Relationship, int EmbFile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAttachFileA(IntPtr IPDF, String FilePath, String Description, lbool Compress);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfAttachFileW(IntPtr IPDF, String FilePath, String Description, lbool Compress);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfAttachFileExA(IntPtr IPDF, byte[]Buffer, int BufSize, String FileName, String Description, lbool Compress);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfAttachFileExW(IntPtr IPDF, byte[]Buffer, int BufSize, String FileName, String Description, lbool Compress);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfAutoTemplate(IntPtr IPDF, int Templ, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfBeginClipPath(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfBeginContinueText(IntPtr IPDF, double PosX, double PosY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfBeginLayer(IntPtr IPDF, int OCG);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfBeginPageTemplate(IntPtr IPDF, String Name, lbool UseAutoTemplates);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfBeginPattern(IntPtr IPDF, TPatternType PatternType, TTilingType TilingType, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfBeginTemplate(IntPtr IPDF, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfBeginTemplateEx(IntPtr IPDF, ref TPDFRect BBox, ref TCTM Matrix);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfBeginTransparencyGroup(IntPtr IPDF, double x1, double y1, double x2, double y2, lbool Isolated, lbool Knockout, TExtColorSpace CS, int CSHandle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfBezier_1_2_3(IntPtr IPDF, double x1, double y1, double x2, double y2, double x3, double y3);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfBezier_1_3(IntPtr IPDF, double x1, double y1, double x3, double y3);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfBezier_2_3(IntPtr IPDF, double x2, double y2, double x3, double y3);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfCalcWidthHeight(IntPtr IPDF, double OrgWidth, double OrgHeight, double ScaledWidth, double ScaledHeight);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCaretAnnotA(IntPtr IPDF, double PosX, double PosY, double Width, double Height, uint Color, TPDFColorSpace CS, String Author, String Subject, String Content);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCaretAnnotW(IntPtr IPDF, double PosX, double PosY, double Width, double Height, uint Color, TPDFColorSpace CS, String Author, String Subject, String Content);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfChangeAnnotPos(IntPtr IPDF, int Handle, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfChangeAnnotNameA(IntPtr IPDF, int Handle, String Name);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfChangeAnnotNameW(IntPtr IPDF, int Handle, String Name);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfChangeBookmarkA(IntPtr IPDF, int ABmk, String Title, int Parent, int DestPage, lbool Open);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfChangeBookmarkW(IntPtr IPDF, int ABmk, String Title, int Parent, int DestPage, lbool Open);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfChangeFont(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfChangeFontEx(IntPtr IPDF, int Handle, double FontSize, TFStyle Style);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfChangeFontSize(IntPtr IPDF, double Size);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfChangeFontStyle(IntPtr IPDF, TFStyle Style);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfChangeFontStyleEx(IntPtr IPDF, TFStyle Style);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfChangeJavaScriptA(IntPtr IPDF, int Handle, String NewScript);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfChangeJavaScriptW(IntPtr IPDF, int Handle, String NewScript);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfChangeJavaScriptActionA(IntPtr IPDF, int Handle, String NewScript);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfChangeJavaScriptActionW(IntPtr IPDF, int Handle, String NewScript);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfChangeJavaScriptNameA(IntPtr IPDF, int Handle, String Name);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfChangeJavaScriptNameW(IntPtr IPDF, int Handle, String Name);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfChangeLinkAnnot(IntPtr IPDF, int Handle, String URL);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfChangeNamedDestW(IntPtr IPDF, TObjType ObjType, int Handle, String NewDestName);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfChangeOCGNameW(IntPtr IPDF, int Handle, String Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfChangeSeparationColor(IntPtr IPDF, int CSHandle, uint NewColor, TExtColorSpace Alternate, int AltHandle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCheckAndRepairFonts(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCheckCollection(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCheckConformance(IntPtr IPDF, TConformanceType Type, TCheckOptions Options, IntPtr UserData, TOnFontNotFoundProc OnFontNotFound, TOnReplaceICCProfile OnReplaceICCProfile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCheckFieldNames(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCircleAnnotA(IntPtr IPDF, double PosX, double PosY, double Width, double Height, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Comment);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCircleAnnotW(IntPtr IPDF, double PosX, double PosY, double Width, double Height, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Comment);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfClearAutoTemplates(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern void          pdfClearErrorLog(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfClearHostFonts(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfClipPath(IntPtr IPDF, TClippingMode ClipMode, TPathFillMode FillMode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCloseAndSignFile(IntPtr IPDF, String CertFile, String Password, String Reason, String Location);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCloseAndSignFileEx(IntPtr IPDF, String OpenPwd, String OwnerPwd, TKeyLen KeyLen, TRestrictions Restrict, String CertFile, String Password, String Reason, String Location);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCloseAndSignFileExt(IntPtr IPDF, ref TPDFSigParms_I SigParms);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCloseFile(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCloseFileEx(IntPtr IPDF, String OpenPwd, String OwnerPwd, TKeyLen KeyLen, TRestrictions Restrict);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCloseImage(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCloseImportFile(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCloseImportFileEx(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfClosePath(IntPtr IPDF, TPathFillMode FillMode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCloseTag(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfComputeBBox(IntPtr IPDF, ref TPDFRect BBox, TCompBBoxFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern uint          pdfConvColor(IntPtr Color, int NumComps, TExtColorSpace SourceCS, IntPtr IColorSpace, TExtColorSpace DestCS);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfConvertColors(IntPtr IPDF, TColorConvFlags Flags, float[]Add);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern int           pdfConvertEMFSpool(IntPtr IPDF, String SpoolFile, double LeftMargin, double TopMargin, TSpoolConvFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfConvToUnicode(IntPtr IPDF, String AString, TCodepage CP);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCopyChoiceValues(IntPtr IPDF, int Source, int Dest, lbool Share);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreate3DAnnotW(IntPtr IPDF, double PosX, double PosY, double Width, double Height, String Author, String Name, String U3DFile, String Image);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCreate3DBackground(IntPtr IPDF, IntPtr IView, uint BackColor);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreate3DGotoViewAction(IntPtr IPDF, int Base3DAnnot, IntPtr IView, T3DNamedAction Named);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCreate3DProjection(IntPtr IPDF, IntPtr IView, T3DProjType ProjType, T3DScaleType ScaleType, double Diameter, double FOV);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern IntPtr        pdfCreate3DView(IntPtr IPDF, int Base3DAnnot, String Name, lbool SetAsDefault, double[]Matrix, double CamDistance, T3DRenderingMode RM, T3DLightingSheme LS);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateAltFontList(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateAnnotAP(IntPtr IPDF, int Annot);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateArticleThreadA(IntPtr IPDF, String ThreadName);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateArticleThreadW(IntPtr IPDF, String ThreadName);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateAxialShading(IntPtr IPDF, double sX, double sY, double eX, double eY, double SCenter, uint SColor, uint EColor, lbool Extend1, lbool Extend2);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateBarcodeField(IntPtr IPDF, String Name, int Parent, double PosX, double PosY, double Width, double Height, ref TPDFBarcode_I Barcode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateButtonA(IntPtr IPDF, String Name, String Caption, int Parent, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateButtonW(IntPtr IPDF, [MarshalAs(UnmanagedType.LPStr)]String Name, [MarshalAs(UnmanagedType.LPWStr)]String Caption, int Parent, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateCheckBox(IntPtr IPDF, String Name, String ExpValue, lbool Checked, int Parent, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateCIEColorSpace(IntPtr IPDF, TExtColorSpace Base, float[]WhitePoint, float[]BlackPoint, float[]Gamma, float[]Matrix);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCreateCollection(IntPtr IPDF, TColView View);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateCollectionFieldA(IntPtr IPDF, TColColumnType ColType, int Column, String Name, String Key, lbool Visible, lbool Editable);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateCollectionFieldW(IntPtr IPDF, TColColumnType ColType, int Column, [MarshalAs(UnmanagedType.LPWStr)]String Name, [MarshalAs(UnmanagedType.LPStr)]String Key, lbool Visible, lbool Editable);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCreateColItemDate(IntPtr IPDF, int EmbFile, String Key, long Date, String Prefix);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCreateColItemNumber(IntPtr IPDF, int EmbFile, String Key, double Value, String Prefix);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCreateColItemStringA(IntPtr IPDF, int EmbFile, String Key, String Value, String Prefix);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfCreateColItemStringW(IntPtr IPDF, int EmbFile, [MarshalAs(UnmanagedType.LPStr)]String Key, [MarshalAs(UnmanagedType.LPWStr)]String Value, [MarshalAs(UnmanagedType.LPWStr)]String Prefix);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateComboBox(IntPtr IPDF, String Name, lbool Sort, int Parent, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateDeviceNColorSpace(IntPtr IPDF, String[]Colorants, int NumColorants, String PostScriptFunc, TExtColorSpace Alternate, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCreateDPartRoot(IntPtr IPDF, String[]NodeNameList, int NodeNamesCount, int RecordLevel);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateExtGState(IntPtr IPDF, ref TPDFExtGState GS);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateGeospatialMeasure(IntPtr IPDF, int Viewport, String Attributes);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateGoToAction(IntPtr IPDF, TDestType DestType, int PageNum, double a, double b, double c, double d);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateGoToActionEx(IntPtr IPDF, int NamedDest);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateGoToEActionA(IntPtr IPDF, TEmbFileLocation Location, String Source, int SrcPage, String Target, String DestName, int DestPage, lbool NewWindow);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateGoToEActionW(IntPtr IPDF, TEmbFileLocation Location, String Source, int SrcPage, String Target, String DestName, int DestPage, lbool NewWindow);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateGoToRActionA(IntPtr IPDF, String FileName, int PageNum);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateGoToRActionW(IntPtr IPDF, String FileName, int PageNum);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateGoToRActionExA(IntPtr IPDF, String FileName, String DestName, lbool NewWindow);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateGoToRActionExW(IntPtr IPDF, [MarshalAs(UnmanagedType.LPStr)]String FileName, [MarshalAs(UnmanagedType.LPWStr)]String DestName, lbool NewWindow);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateGoToRActionExUA(IntPtr IPDF, [MarshalAs(UnmanagedType.LPWStr)]String FileName, String DestName, lbool NewWindow);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateGoToRActionExUW(IntPtr IPDF, String FileName, String DestName, lbool NewWindow);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateGroupField(IntPtr IPDF, String Name, int Parent);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateHideAction(IntPtr IPDF, int Field, lbool Hide);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateICCBasedColorSpaceA(IntPtr IPDF, String ICCProfile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateICCBasedColorSpaceW(IntPtr IPDF, String ICCProfile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateICCBasedColorSpaceEx(IntPtr IPDF, byte[]Buffer, int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCreateImageA(IntPtr IPDF, String FileName, TImageFormat Format);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfCreateImageW(IntPtr IPDF, String FileName, TImageFormat Format);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateImpDataActionA(IntPtr IPDF, String DataFile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateImpDataActionW(IntPtr IPDF, String DataFile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateIndexedColorSpace(IntPtr IPDF, TExtColorSpace Base, int Handle, byte[]ColorTable, int NumColors);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateJSActionA(IntPtr IPDF, String Script);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateJSActionW(IntPtr IPDF, String Script);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateLaunchAction(IntPtr IPDF, TFileOP OP, String FileName, String DefDir, String Param, lbool NewWindow);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateLaunchActionExA(IntPtr IPDF, String FileName, lbool NewWindow);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateLaunchActionExW(IntPtr IPDF, String FileName, lbool NewWindow);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateListBox(IntPtr IPDF, String Name, lbool Sort, int Parent, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateNamedAction(IntPtr IPDF, TNamedAction Action);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateNamedDestA(IntPtr IPDF, String Name, int DestPage, TDestType DestType, double a, double b, double c, double d);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateNamedDestW(IntPtr IPDF, String Name, int DestPage, TDestType DestType, double a, double b, double c, double d);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfCreateNewPDF(IntPtr IPDF, String OutPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateOCGA(IntPtr IPDF, String Name, lbool DisplayInUI, lbool Visible, TOCGIntent Intent);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateOCGW(IntPtr IPDF, String Name, lbool DisplayInUI, lbool Visible, TOCGIntent Intent);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateOCMD(IntPtr IPDF, TOCVisibility Visibility, int[]OCGs, int Count);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateRadialShading(IntPtr IPDF, double sX, double sY, double R1, double eX, double eY, double R2, double SCenter, uint SColor, uint EColor, lbool Extend1, lbool Extend2);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateRadioButton(IntPtr IPDF, String Name, String ExpValue, int Checked, int Parent, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateRectilinearMeasureW(IntPtr IPDF, int Viewport, double Scale, String UnitDescription, String LabelDistance, String LabelArea);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateResetAction(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateSeparationCS(IntPtr IPDF, String Colorant, TExtColorSpace Alternate, int Handle, uint Color);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateSetOCGStateAction(IntPtr IPDF, int[]On, int OnCount, int[]Off, int OffCount, int[]Toggle, int ToggleCount, lbool PreserveRB);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateSigField(IntPtr IPDF, String Name, int Parent, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateSigFieldAP(IntPtr IPDF, int SigField);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfCreateSoftMask(IntPtr IPDF, int TranspGroup, TSoftMaskType MaskType, uint BackColor);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateStdPattern(IntPtr IPDF, TStdPattern Pattern, double LineWidth, double Distance, uint LineColor, uint BackColor);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfCreateStructureTreeEx(IntPtr IPDF, TPDFBaseTag RootTag);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateSubmitAction(IntPtr IPDF, TSubmitFlags Flags, String URL);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateTextField(IntPtr IPDF, String Name, int Parent, lbool Multiline, int MaxLen, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateURIAction(IntPtr IPDF, String URL);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfCreateViewportW(IntPtr IPDF, String Name, double X1, double Y1, double X2, double Y2);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCreateXFAStreamA(IntPtr IPDF, String Name, byte[]Buffer, int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfDecryptPDFA(IntPtr IPDF, String FileName, TPwdType PwdType, String Password);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfDecryptPDFW(IntPtr IPDF, [MarshalAs(UnmanagedType.LPWStr)]String FileName, TPwdType PwdType, [MarshalAs(UnmanagedType.LPStr)]String Password);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern void          pdfDeleteAcroForm(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteActionFromObj(IntPtr IPDF, TObjType ObjType, int ActHandle, int ObjHandle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteActionFromObjEx(IntPtr IPDF, TObjType ObjType, int ObjHandle, int ActIndex);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteAltFontList(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteAnnotation(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteAnnotationFromPage(IntPtr IPDF, int PageNum, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfDeleteAppEvents(IntPtr IPDF, lbool ApplyEvent, TOCAppEvent Event);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfDeleteBookmark(IntPtr IPDF, int ABmk);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteDPartNode(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteEmbeddedFile(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteField(IntPtr IPDF, int Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteFieldEx(IntPtr IPDF, String Name);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern void          pdfDeleteJavaScripts(IntPtr IPDF, lbool DelJavaScriptActions);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfDeleteNamedDestW(IntPtr IPDF, String Name);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteNamedDestByIndex(IntPtr IPDF, int Index);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteOCGFromAppEvent(IntPtr IPDF, int Handle, TOCAppEvent Events, TOCGUsageCategory Categories, lbool DelCategoryOnly);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteOCGFromDisplayTree(IntPtr IPDF, int OCGHandle, lbool Recursive);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteOCUINode(IntPtr IPDF, IntPtr Node);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfDeleteOutputIntent(IntPtr IPDF, int Index);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfDeletePage(IntPtr IPDF, int PageNum);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern void          pdfDeletePageLabels(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfDeletePDF(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteSeparationInfo(IntPtr IPDF, lbool AllPages);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteTemplate(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDeleteTemplateEx(IntPtr IPDF, int Index);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfDeleteWatermark(IntPtr IPDF, int PageNum, lbool InclAnnots);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern void          pdfDeleteXFAForm(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDrawArc(IntPtr IPDF, double PosX, double PosY, double Radius, double StartAngle, double EndAngle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDrawArcEx(IntPtr IPDF, double PosX, double PosY, double Width, double Height, double StartAngle, double EndAngle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDrawChord(IntPtr IPDF, double PosX, double PosY, double Width, double Height, double StartAngle, double EndAngle, TPathFillMode FillMode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDrawCircle(IntPtr IPDF, double PosX, double PosY, double Radius, TPathFillMode FillMode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDrawNGon(IntPtr IPDF, double PosX, double PosY, double Radius, double Alpha, int NumSides, TPathFillMode FillMode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfDrawPie(IntPtr IPDF, double PosX, double PosY, double Width, double Height, double StartAngle, double EndAngle, TPathFillMode FillMode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfEditPage(IntPtr IPDF, int PageNum);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfEditTemplate(IntPtr IPDF, int Index);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfEditTemplate2(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfEllipse(IntPtr IPDF, double PosX, double PosY, double Width, double Height, TPathFillMode FillMode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfEncryptPDFA(IntPtr IPDF, String FileName, String OpenPwd, String OwnerPwd, TKeyLen KeyLen, TRestrictions Restrict);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfEncryptPDFW(IntPtr IPDF, [MarshalAs(UnmanagedType.LPWStr)]String FileName, [MarshalAs(UnmanagedType.LPStr)]String OpenPwd, [MarshalAs(UnmanagedType.LPStr)]String OwnerPwd, TKeyLen KeyLen, TRestrictions Restrict);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfEndContinueText(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfEndLayer(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfEndPage(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfEndPattern(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfEndTemplate(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfEnumDocFonts(IntPtr IPDF, IntPtr Data, TEnumFontProc2 EnumProc);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfEnumHostFonts(IntPtr IPDF, IntPtr Data, TEnumFontProc EnumProc);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfEnumHostFontsEx(IntPtr IPDF, IntPtr Data, TEnumFontProcEx EnumProc);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfExchangeBookmarks(IntPtr IPDF, int Bmk1, int Bmk2);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfExchangePages(IntPtr IPDF, int First, int Second);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfExtractText(IntPtr IPDF, int PageNum, TTextExtractionFlags Flags, IntPtr Area, ref IntPtr Text, ref int TextLen);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfExtractText2(IntPtr IPDF, int PageNum, TTextExtractionFlags Flags, ref TFltRect Area, ref IntPtr Text, ref int TextLen);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfFileAttachAnnotA(IntPtr IPDF, double PosX, double PosY, TFileAttachIcon Icon, String Author, String Desc, String AFile, lbool Compress);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfFileAttachAnnotW(IntPtr IPDF, double PosX, double PosY, TFileAttachIcon Icon, String Author, String Desc, String AFile, lbool Compress);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern int           pdfFileAttachAnnotEx(IntPtr IPDF, double PosX, double PosY, TFileAttachIcon Icon, String FileName, String Author, String Desc, byte[]Buffer, int BufSize, lbool Compress);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfFileLinkA(IntPtr IPDF, double PosX, double PosY, double Width, double Height, String AFilePath);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfFileLinkW(IntPtr IPDF, double PosX, double PosY, double Width, double Height, String AFilePath);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfFindBookmarkA(IntPtr IPDF, int DestPage, String Title);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfFindBookmarkW(IntPtr IPDF, int DestPage, String Title);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfFindEmbeddedFile(IntPtr IPDF, String Name);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfFindFieldA(IntPtr IPDF, String Name);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfFindFieldW(IntPtr IPDF, String Name);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfFinishSignature(IntPtr IPDF, byte[]PKCS7Obj, int Length);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfFindLinkAnnot(IntPtr IPDF, String URL);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfFindNextBookmark(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfFlattenAnnotOrField(IntPtr IPDF, int Index, TAnnotFlattenFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfFlattenAnnots(IntPtr IPDF, TAnnotFlattenFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfFlattenForm(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfFlushPageContent(IntPtr IPDF, ref TPDFStack Stack);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfFlushPages(IntPtr IPDF, TFlushPageFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfFlushPagesEx(IntPtr IPDF, TFlushPageFlags Flags, int LastPage);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern void          pdfFreeImageBuffer(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfFreeImageObj(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfFreeImageObjEx(IntPtr IPDF, IntPtr ImagePtr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfFreePDF(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfFreeTextAnnotA(IntPtr IPDF, double PosX, double PosY, double Width, double Height, String Author, String AText, TTextAlign Align);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfFreeTextAnnotW(IntPtr IPDF, double PosX, double PosY, double Width, double Height, String Author, String AText, TTextAlign Align);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfFreeUniBuf(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGet3DAnnotStream(IntPtr IPDF, int Annot, ref IntPtr Data, ref int Size, ref String SubType);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetActionCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetActionHandle(IntPtr IPDF, TObjType ObjType, int ObjHandle, int ActIndex);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetActionType(IntPtr IPDF, int ActHandle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetActionTypeEx(IntPtr IPDF, TObjType ObjType, int ObjHandle, int ActIndex);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetActiveFont(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetAllocBy(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetAnnot(IntPtr IPDF, int Handle, ref TPDFAnnotation_I Annot);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetAnnotBBox(IntPtr IPDF, int Handle, ref TPDFRect BBox);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetAnnotCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetAnnotEx(IntPtr IPDF, int Handle, ref TPDFAnnotationEx_I Annot);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetAnnotFlags(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfGetAnnotLink(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetAnnotType(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetAscent(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetBarcodeDict(IntPtr IBarcode, ref TPDFBarcode_I Barcode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetBBox(IntPtr IPDF, TPageBoundary Boundary, ref TPDFRect BBox);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetBidiMode(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetBookmark(IntPtr IPDF, int Handle, ref TBookmark_I Bmk);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetBookmarkCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetBookmarkEx(IntPtr IPDF, int Handle, ref TPDFBookmark_I Bmk);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetBorderStyle(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfGetBuffer(IntPtr IPDF, ref int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetCapHeight(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetCharacterSpacing(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetCheckBoxChar(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetCheckBoxCharEx(IntPtr IPDF, int Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetCheckBoxDefState(IntPtr IPDF, int Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetCMap(IntPtr IPDF, int Index, ref TPDFCMap CMap);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetCMapCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetCollectionInfo(IntPtr IPDF, ref TPDFCollectionInfo_I Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetColorSpace(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetColorSpaceCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetColorSpaceObj(IntPtr IPDF, int Handle, ref TPDFColorSpaceObj_I CS);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetColorSpaceObjEx(IntPtr IColorSpace, ref TPDFColorSpaceObj_I CS);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetCompressionFilter(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetCompressionLevel(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetContent(IntPtr IPDF, ref IntPtr Buffer);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetDefBitsPerPixel(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetDescent(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetDeviceNAttributes(IntPtr IAttributes, ref TDeviceNAttributes_I Attributes);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetDocInfo(IntPtr IPDF, TDocumentInfo DInfo, ref IntPtr Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetDocInfoCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetDocInfoEx(IntPtr IPDF, int Index, ref TDocumentInfo DInfo, ref IntPtr Key, ref IntPtr Value, ref lbool Unicode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetDocUsesTransparency(IntPtr IPDF, int Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetDrawDirection(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfGetDynaPDFVersion();
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetEmbeddedFile(IntPtr IPDF, int Handle, ref TPDFFileSpec_I FileSpec, lbool Decompress);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetEmbeddedFileCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetEmbeddedFileNode(IntPtr IEF, ref TPDFEmbFileNode_I F, lbool Decompress);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetEMFPatternDistance(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetErrLogMessage(IntPtr IPDF, int Index, ref TPDFError_I Err);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetErrLogMessageCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfGetErrorMessage(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetErrorMode(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetField(IntPtr IPDF, int Handle, ref TPDFField_I Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern uint          pdfGetFieldBackColor(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern uint          pdfGetFieldBorderColor(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldBorderStyle(IntPtr IPDF, int Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetFieldBorderWidth(IntPtr IPDF, int Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldCalcOrder(IntPtr IPDF, ref IntPtr Out);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetFieldChoiceValue(IntPtr IPDF, int AField, int ValIndex, ref TPDFChoiceValue_I Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetFieldColor(IntPtr IPDF, int Field, TFieldColor ColorType, ref TPDFColorSpace ColorSpace, ref uint Color);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetFieldEx(IntPtr IPDF, int Handle, ref TPDFFieldEx_I Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetFieldEx2(IntPtr IField, ref TPDFFieldEx_I Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldExpValCount(IntPtr IPDF, int Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldExpValue(IntPtr IPDF, int Field, ref IntPtr Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetFieldExpValueEx(IntPtr IPDF, int Field, int ValIndex, ref IntPtr Value, ref IntPtr ExpValue, ref lbool Selected);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldFlags(IntPtr IPDF, int Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldGroupType(IntPtr IPDF, int Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldHighlightMode(IntPtr IPDF, int Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldIndex(IntPtr IPDF, int Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldMapName(IntPtr IPDF, int Field, ref IntPtr Value, ref lbool Unicode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldName(IntPtr IPDF, int Field, ref IntPtr Name);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldOrientation(IntPtr IPDF, int Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldTextAlign(IntPtr IPDF, int Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern uint          pdfGetFieldTextColor(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldToolTip(IntPtr IPDF, int Field, ref IntPtr Value, ref lbool Unicode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFieldType(IntPtr IPDF, int Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetFileSpec(IntPtr IFS, ref TPDFFileSpecEx_I F);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern uint          pdfGetFillColor(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFontCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetFontEx(IntPtr IPDF, int Handle, ref TPDFFontObj_I F);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetFontInfoEx(IntPtr IPDF, int Handle, ref TPDFFontInfo_I F);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetFontMetrics(IntPtr IPDF, int Handle, ref TPDFFontMetrics Metrics);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFontOrigin(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern void          pdfGetFontSearchOrder(IntPtr IPDF, TFontBaseType[]Order);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFontSelMode(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetFontSize(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFontWeight(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetFTextHeightA(IntPtr IPDF, TTextAlign Align, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern double        pdfGetFTextHeightW(IntPtr IPDF, TTextAlign Align, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetFTextHeightExA(IntPtr IPDF, double Width, TTextAlign Align, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern double        pdfGetFTextHeightExW(IntPtr IPDF, double Width, TTextAlign Align, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetFullyQualifiedFieldName(IntPtr IPDF, int Handle, ref IntPtr Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetGlyphIndex(IntPtr IPDF, int Index);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetGlyphOutline(IntPtr IPDF, int Index, TPDFGlyphOutline_I Outline);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetGoToAction(IntPtr IPDF, int Handle, ref TPDFGoToAction_I Action);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetGoToRAction(IntPtr IPDF, int Handle, ref TPDFGoToAction_I Action);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern uint          pdfGetGStateFlags(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetHideAction(IntPtr IPDF, int Handle, ref TPDFHideAction_I Action);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern uint          pdfGetIconColor(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfGetImageBuffer(IntPtr IPDF, ref int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern int           pdfGetImageCount(IntPtr IPDF, String FileName);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetImageCountEx(IntPtr IPDF, byte[]Buffer, int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetImageHeight(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetImageObj(IntPtr IPDF, int Handle, TParseFlags Flags, ref TPDFImage Image);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetImageObjCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetImageObjEx(IntPtr IPDF, IntPtr ImagePtr, TParseFlags Flags, ref TPDFImage Image);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetImageWidth(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern uint          pdfGetImportFlags(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern uint          pdfGetImportFlags2(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetImportDataAction(IntPtr IPDF, int Handle, ref TPDFImportDataAction_I Action);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetInBBox(IntPtr IPDF, int PageNum, TPageBoundary Boundary, ref TPDFRect BBox);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetInDocInfo(IntPtr IPDF, TDocumentInfo DInfo, ref IntPtr Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetInDocInfoCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetInDocInfoEx(IntPtr IPDF, int Index, ref TDocumentInfo DInfo, ref IntPtr Key, ref IntPtr Value, ref lbool Unicode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetInEncryptionFlags(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetInFieldCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetInIsCollection(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetInIsEncrypted(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetInIsSigned(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetInIsTrapped(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetInIsXFAForm(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetInkList(IntPtr List, ref IntPtr Points, ref int Count);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetInMetadata(IntPtr IPDF, int PageNum, ref IntPtr Buffer, ref int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetInOrientation(IntPtr IPDF, int PageNum);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetInPageCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetInPDFVersion(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetInPDFVersionEx(IntPtr IPDF, ref TPDFVersionInfo_I Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetInRepairMode(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetInPrintSettings(IntPtr IPDF, ref TPDFPrintSettings_I Settings);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetIsFixedPitch(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetInIsTaggedPDF(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetIsTaggingEnabled(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetItalicAngle(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfGetJavaScript(IntPtr IPDF, int Handle, ref int Len, ref lbool Unicode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfGetJavaScriptAction2(IntPtr IPDF, TObjType ObjType, int ObjHandle, int ActIndex, ref int Len, ref lbool Unicode, ref TObjEvent Event);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetJavaScriptActionEx(IntPtr IPDF, int Handle, ref TPDFJavaScriptAction_I Action);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfGetJavaScriptAction(IntPtr IPDF, int Handle, ref int Len, ref lbool Unicode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetJavaScriptCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfGetJavaScriptEx(IntPtr IPDF, String Name, ref int Len, ref lbool Unicode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfGetJavaScriptName(IntPtr IPDF, int Handle, ref int Len, ref lbool Unicode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetJPEGQuality(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfGetLanguage(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetLastTextPosX(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetLastTextPosY(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetLayerConfig(IntPtr IPDF, int Index, ref TPDFOCLayerConfig_I Config);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetLayerConfigCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetLaunchAction(IntPtr IPDF, int Handle, ref TPDFLaunchAction_I Action);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetLeading(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetLineCapStyle(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetLineJoinStyle(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetLineWidth(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetLinkHighlightMode(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfGetLogMetafileSize(IntPtr IPDF, String FileName, ref TRectL R);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetLogMetafileSizeEx(IntPtr IPDF, byte[]Buffer, int BufSize, ref TRectL R);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetMatrix(IntPtr IPDF, ref TCTM M);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetMaxFieldLen(IntPtr IPDF, int TxtField);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetMeasureObj(IntPtr Measure, ref TPDFMeasure_I Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern uint          pdfGetMetaConvFlags(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetMetadata(IntPtr IPDF, TMetadataObj ObjType, int Handle, ref IntPtr Buffer, ref int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfGetMissingGlyphs(IntPtr IPDF, ref int Count);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetMiterLimit(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetMovieAction(IntPtr IPDF, int Handle, ref TPDFMovieAction_I Action);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetNamedAction(IntPtr IPDF, int Handle, ref TPDFNamedAction_I Action);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetNamedDest(IntPtr IPDF, int Index, ref TPDFNamedDest_I Dest);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetNamedDestCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetNeedAppearance(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetNumberFormatObj(IntPtr NumberFmt, ref TPDFNumberFormat_I Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetObjActionCount(IntPtr IPDF, TObjType ObjType, int ObjHandle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetObjActions(IntPtr IPDF, TObjType ObjType, int ObjHandle, ref TPDFObjActions Actions);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetObjEvent(IntPtr IEvent, ref TPDFObjEvent Event);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetOCG(IntPtr IPDF, int Handle, ref TPDFOCG Out);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetOCGContUsage(IntPtr IPDF, int Handle, ref TPDFOCGContUsage Out);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetOCGCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetOCGUsageUserName(IntPtr IPDF, int Handle, int Index, ref IntPtr NameA, ref IntPtr NameW);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetOCHandle(IntPtr OC);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfGetOCUINode(IntPtr IPDF, IntPtr Node, TPDFOCUINode_I OutNode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetOpacity(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetOrientation(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetOutputIntent(IntPtr IPDF, int Index, ref TPDFOutputIntent_I Intent);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetOutputIntentCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetPageAnnot(IntPtr IPDF, int Handle, ref TPDFAnnotation_I Annot);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetPageAnnotCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetPageAnnotEx(IntPtr IPDF, int Handle, ref TPDFAnnotationEx_I Annot);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetPageBBox(IntPtr PagePtr, TPageBoundary Boundary, ref TFltRect BBox);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetPageCoords(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetPageCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetPageField(IntPtr IPDF, int Index, ref TPDFField_I Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetPageFieldCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetPageFieldEx(IntPtr IPDF, int Index, ref TPDFFieldEx_I Field);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetPageHeight(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetPageLabel(IntPtr IPDF, int Index, ref TPDFPageLabel_I Label);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetPageLabelCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetPageLayout(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetPageMode(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetPageNum(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfGetPageObject(IntPtr IPDF, int PageNum);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetPageOrientation(IntPtr PagePtr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetPageText(IntPtr IPDF, ref TPDFStack Stack);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetPageWidth(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetPDFVersion(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetPDFVersionEx(IntPtr IPDF, ref TPDFVersionInfo_I Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetPrintSettings(IntPtr IPDF, ref TPDFPrintSettings_I Settings);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetPtDataArray(IntPtr PtData, int Index, ref IntPtr DataType, ref IntPtr Values, ref int ValCount);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetPtDataObj(IntPtr PtData, ref IntPtr Subtype, ref int NumArrays);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetRelFileNode(IntPtr IRF, ref TPDFRelFileNode_I F, lbool Decompress);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetResetAction(IntPtr IPDF, int Handle, ref TPDFResetFormAction_I Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetResolution(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetSaveNewImageFormat(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetSeparationInfo(IntPtr IPDF, ref IntPtr Colorant, ref TExtColorSpace CS);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetSigDict(IntPtr ISignature, ref TPDFSigDict_I SigDict);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern uint          pdfGetStrokeColor(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetSubmitAction(IntPtr IPDF, int Handle, ref TPDFSubmitFormAction_I Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetSysFontInfo(IntPtr IPDF, int Handle, ref TPDFSysFont_I Out);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetTabLen(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetTemplCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetTemplHandle(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetTemplHeight(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetTemplWidth(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetTextDrawMode(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetTextFieldValue(IntPtr IPDF, int Field, ref IntPtr Value, ref lbool ValIsUnicode, ref IntPtr DefValue, ref lbool DefValIsUnicode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetTextRect(IntPtr IPDF, ref double PosX, ref double PosY, ref double Width, ref double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetTextRise(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetTextScaling(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetTextWidthA(IntPtr IPDF, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern double        pdfGetTextWidthW(IntPtr IPDF, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetTextWidthExA(IntPtr IPDF, String AText, int Len);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern double        pdfGetTextWidthExW(IntPtr IPDF, String AText, int Len);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern uint          pdfGetTransparentColor(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetTrapped(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetTypoLeading(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetURIAction(IntPtr IPDF, int Handle, ref TPDFURIAction_I Action);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetUseExactPwd(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetUseGlobalImpFiles(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetUserRights(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern float         pdfGetUserUnit(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetUseStdFonts(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetUseSystemFonts(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetUsesTransparency(IntPtr IPDF, int PageNum);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetUseTransparency(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetUseVisibleCoords(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetViewerPreferences(IntPtr IPDF, ref int Preference, ref int AddVal);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetViewport(IntPtr IPDF, int PageNum, int Index, ref TPDFViewport_I VP);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetViewportCount(IntPtr IPDF, int PageNum);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetWMFDefExtent(IntPtr IPDF, ref int Width, ref int Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetWMFPixelPerInch(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern double        pdfGetWordSpacing(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfGetXFAStream(IntPtr IPDF, int Index, TPDFXFAStream_I Out);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfGetXFAStreamCount(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfHaveDPartRoot(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfHaveOpenDoc(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfHaveOpenPage(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfHighlightAnnotA(IntPtr IPDF, TAnnotType SubType, double PosX, double PosY, double Width, double Height, uint Color, String Author, String Subject, String Comment);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfHighlightAnnotW(IntPtr IPDF, TAnnotType SubType, double PosX, double PosY, double Width, double Height, uint Color, String Author, String Subject, String Comment);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfImportBookmarks(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfImportCatalogObjects(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfImportDocInfo(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfImportEncryptionSettings(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfImportOCProperties(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfImportPage(IntPtr IPDF, int PageNum);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfImportPageEx(IntPtr IPDF, int PageNum, double ScaleX, double ScaleY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfImportPDFFile(IntPtr IPDF, int DestPage, double ScaleX, double ScaleY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfInitBarcode2(ref TPDFBarcode2 Barcode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfInitColorManagement(IntPtr IPDF, TPDFColorProfiles Profiles, TPDFColorSpace DestSpace, TPDFInitCMFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfInitColorManagementEx(IntPtr IPDF, TPDFColorProfilesEx Profiles, TPDFColorSpace DestSpace, TPDFInitCMFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfInitExtGState(ref TPDFExtGState GS);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfInitHeaderFooter(ref TPDFHeaderFooter_I Value, [MarshalAs(UnmanagedType.LPArray), In(), Out()]TPDFHdrFtr_I[]HFArray, int Count);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfInitOCGContUsage(ref TPDFOCGContUsage Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfInitStack(IntPtr IPDF, ref TPDFStack Stack);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfInkAnnotA(IntPtr IPDF, TFltPoint[]Points, int NumPoints, double LineWidth, uint Color, TPDFColorSpace CS, String Author, String Subject, String Content);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfInkAnnotW(IntPtr IPDF, TFltPoint[]Points, int NumPoints, double LineWidth, uint Color, TPDFColorSpace CS, String Author, String Subject, String Content);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfInsertBarcode(IntPtr IPDF, double PosX, double PosY, double Width, double Height, TCellAlign HAlign, TCellAlign VAlign, ref TPDFBarcode2 Barcode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfInsertBMPFromHandle(IntPtr IPDF, double PosX, double PosY, double ScaleWidth, double ScaleHeight, IntPtr hBitmap);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfInsertBookmarkA(IntPtr IPDF, String Title, int Parent, int DestPage, lbool Open, lbool AddChildren);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfInsertBookmarkW(IntPtr IPDF, String Title, int Parent, int DestPage, lbool Open, lbool AddChildren);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfInsertBookmarkExA(IntPtr IPDF, String Title, int Parent, int NamedDest, lbool Open, lbool AddChildren);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfInsertBookmarkExW(IntPtr IPDF, String Title, int Parent, int NamedDest, lbool Open, lbool AddChildren);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfInsertImage(IntPtr IPDF, double PosX, double PosY, double ScaleWidth, double ScaleHeight, String AFile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern int           pdfInsertImageEx(IntPtr IPDF, double PosX, double PosY, double ScaleWidth, double ScaleHeight, String AFile, int Index);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfInsertImageFromBuffer(IntPtr IPDF, double PosX, double PosY, double ScaleWidth, double ScaleHeight, byte[]Buffer, int BufSize, int Index);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfInsertImageFromBuffer2(IntPtr IPDF, double PosX, double PosY, double ScaleWidth, double ScaleHeight, IntPtr Buffer, int BufSize, int Index);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfInsertMetafile(IntPtr IPDF, String FileName, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfInsertMetafileEx(IntPtr IPDF, byte[]Buffer, int BufSize, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfInsertMetafileExt(IntPtr IPDF, String FileName, ref TRectL View, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfInsertMetafileExtEx(IntPtr IPDF, byte[]Buffer, int BufSize, ref TRectL View, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfInsertMetafileFromHandle(IntPtr IPDF, IntPtr hEnhMetafile, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfInsertMetafileFromHandleEx(IntPtr IPDF, IntPtr hEnhMetafile, ref TRectL View, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfInsertRawImage(IntPtr IPDF, byte[]Buffer, int BitsPerPixel, int ColorCount, int ImgWidth, int ImgHeight, double PosX, double PosY, double ScaleWidth, double ScaleHeight);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfInsertRawImage(IntPtr IPDF, IntPtr Buffer, int BitsPerPixel, int ColorCount, int ImgWidth, int ImgHeight, double PosX, double PosY, double ScaleWidth, double ScaleHeight);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfInsertRawImageEx(IntPtr IPDF, double PosX, double PosY, double ScaleWidth, double ScaleHeight, ref TPDFRawImage Image);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfIsBidiText(IntPtr IPDF, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfIsColorPage(IntPtr IPDF, lbool GrayIsColor);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfIsEmptyPage(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfLineAnnotA(IntPtr IPDF, double x1, double y1, double x2, double y2, double LineWidth, TLineEndStyle Start, TLineEndStyle End, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfLineAnnotW(IntPtr IPDF, double x1, double y1, double x2, double y2, double LineWidth, TLineEndStyle Start, TLineEndStyle End, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfLineTo(IntPtr IPDF, double PosX, double PosY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfLoadCMap(IntPtr IPDF, String CMapName, lbool Embed);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfLoadFDFDataA(IntPtr IPDF, String FileName, String Password, int Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfLoadFDFDataW(IntPtr IPDF, String FileName, [MarshalAs(UnmanagedType.LPStr)]String Password, int Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfLoadFDFDataEx(IntPtr IPDF, byte[]Buffer, int BufSize, String Password, int Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfLoadFont(IntPtr IPDF, byte[]Buffer, int BufSize, TFStyle Style, double Size, lbool Embed, TCodepage CP);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern int           pdfLoadFontEx(IntPtr IPDF, String FontFilte, int Index, TFStyle Style, double Size, lbool Embed, TCodepage CP);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfLoadHeaderFooterSettings(IntPtr IPDF, ref TPDFHeaderFooter_I Value, [MarshalAs(UnmanagedType.LPArray), In(), Out()]TPDFHdrFtr_I[]HFArray);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfLoadLayerConfig(IntPtr IPDF, int Index);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfLockLayer(IntPtr IPDF, int Layer);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfMarkTemplateAsWatermark(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfMovePage(IntPtr IPDF, int Source, int Dest);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfMoveTo(IntPtr IPDF, double PosX, double PosY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfMultiplyMatrix(ref TCTM M1, ref TCTM M2, ref TCTM NewMatrix);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern IntPtr        pdfNewPDF();
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfOpenImportBuffer(IntPtr IPDF, byte[]Buffer, int BufSize, TPwdType PwdType, String Password);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfOpenImportFileA(IntPtr IPDF, String FileName, TPwdType PwdType, String Password);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfOpenImportFileW(IntPtr IPDF, String FileName, TPwdType PwdType, [MarshalAs(UnmanagedType.LPStr)]String Password);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfOpenOutputFile(IntPtr IPDF, String OutPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfOpenOutputFileEncrypted(IntPtr IPDF, String OutPDF, [MarshalAs(UnmanagedType.LPStr)]String OpenPwd, [MarshalAs(UnmanagedType.LPStr)]String OwnerPwd, TKeyLen KeyLen, TRestrictions Restrict);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfOpenTagW(IntPtr IPDF, TPDFBaseTag Tag, [MarshalAs(UnmanagedType.LPStr)]String Lang, String AltText, String Expansion);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfOpenTagBBoxW(IntPtr IPDF, TPDFBaseTag Tag, [MarshalAs(UnmanagedType.LPStr)]String Lang, String AltText, String Expansion, ref TBBox BBox);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfOpenTagExW(IntPtr IPDF, TPDFBaseTag Tag, [MarshalAs(UnmanagedType.LPStr)]String Lang, String AltText, String Expansion, [MarshalAs(UnmanagedType.LPStr)]String Attributes);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfOptimize(IntPtr IPDF, TOptimizeFlags Flags, TOptimizeParams_I Parms);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfPageLink(IntPtr IPDF, double PosX, double PosY, double Width, double Height, int DestPage);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfPageLink2(IntPtr IPDF, double PosX, double PosY, double Width, double Height, int NamedDest);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfPageLink3A(IntPtr IPDF, double PosX, double PosY, double Width, double Height, String NamedDest);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfPageLink3W(IntPtr IPDF, double PosX, double PosY, double Width, double Height, String NamedDest);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfPageLinkEx(IntPtr IPDF, double PosX, double PosY, double Width, double Height, TDestType DestType, int DestPage, double a, double b, double c, double d);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfParseContent(IntPtr IPDF, IntPtr Data, ref TPDFParseInterface Stack, TParseFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfPlaceImage(IntPtr IPDF, int ImgHandle, double PosX, double PosY, double ScaleWidth, double ScaleHeight);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfPlaceSigFieldValidateIcon(IntPtr IPDF, int SigField, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfPlaceTemplate(IntPtr IPDF, int TmplHandle, double PosX, double PosY, double ScaleWidth, double ScaleHeight);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfPlaceTemplateEx(IntPtr IPDF, int TmplHandle, double PosX, double PosY, double ScaleWidth, double ScaleHeight);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfPlaceTemplByMatrix(IntPtr IPDF, int TmplHandle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfPolygonAnnotA(IntPtr IPDF, TFltPoint[]Vertices, int NumVertices, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfPolygonAnnotW(IntPtr IPDF, TFltPoint[]Vertices, int NumVertices, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfPolyLineAnnotA(IntPtr IPDF, TFltPoint[]Vertices, int NumVertices, double LineWidth, TLineEndStyle Start, TLineEndStyle End, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfPolyLineAnnotW(IntPtr IPDF, TFltPoint[]Vertices, int NumVertices, double LineWidth, TLineEndStyle Start, TLineEndStyle End, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Content);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfPrintPage(IntPtr IPDF, int PageNum, String DocName, IntPtr DC, TPDFPrintFlags Flags, ref TRectL Margin, TPDFPrintParams Parms);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfPrintPDFFile(IntPtr IPDF, String TmpDir, String DocName, IntPtr DC, TPDFPrintFlags Flags, ref TRectL Margin, TPDFPrintParams Parms);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfReadImageFormat2(IntPtr IPDF, String FileName, int Index, ref int Width, ref int Height, ref int BitsPerPixel, ref int UseZip);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfReadImageFormatEx(IntPtr IPDF, IntPtr hBitmap, ref int Width, ref int Height, ref int BitsPerPixel, ref int UseZip);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfReadImageFormatFromBuffer(IntPtr IPDF, byte[]Buffer, int BufSize, int Index, ref int Width, ref int Height, ref int BitsPerPixel, ref int UseZip);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfReadImageResolution(IntPtr IPDF, String FileName, int Index, ref int ResX, ref int ResY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfReadImageResolutionEx(IntPtr IPDF, byte[]Buffer, int BufSize, int Index, ref int ResX, ref int ResY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfRectangle(IntPtr IPDF, double PosX, double PosY, double Width, double Height, TPathFillMode FillMode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfReEncryptPDFA(IntPtr IPDF, String FileName, TPwdType PwdType, String InPwd, String NewOpenPwd, String NewOwnerPwd, TKeyLen NewKeyLen, TRestrictions Restrict);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfReEncryptPDFW(IntPtr IPDF, [MarshalAs(UnmanagedType.LPWStr)]String FileName, TPwdType PwdType, [MarshalAs(UnmanagedType.LPStr)]String InPwd, [MarshalAs(UnmanagedType.LPStr)]String NewOpenPwd, [MarshalAs(UnmanagedType.LPStr)]String NewOwnerPwd, TKeyLen NewKeyLen, TRestrictions Restrict);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfRenameSpotColor(IntPtr IPDF, String Colorant, String NewName);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfRenderAnnotOrField(IntPtr IPDF, int Handle, lbool IsAnnot, TButtonState State, ref TCTM Matrix, TRasterFlags Flags, TPDFPixFormat PixFmt, TCompressionFilter Filter, ref TPDFBitmap Out);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfRenderPage(IntPtr IPDF, IntPtr PagePtr, IntPtr RasPtr, ref TPDFRasterImage Img);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfRenderPageEx(IntPtr IPDF, IntPtr DC, ref int DestX, ref int DestY, IntPtr PagePtr, IntPtr RasPtr, ref TPDFRasterImage Img);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfRenderPageToImage(IntPtr IPDF, int PageNum, String OutFile, int Resolution, int Width, int Height, TRasterFlags Flags, TPDFPixFormat PixFmt, TCompressionFilter Filter, TImageFormat Format);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfRenderPDFFileA(IntPtr IPDF, String OutFile, int Resolution, TRasterFlags Flags, TPDFPixFormat PixFmt, TCompressionFilter Filter, TImageFormat Format);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfRenderPDFFileW(IntPtr IPDF, String OutFile, int Resolution, TRasterFlags Flags, TPDFPixFormat PixFmt, TCompressionFilter Filter, TImageFormat Format);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfRenderPDFFileEx(IntPtr IPDF, String OutFile, int Resolution, int Width, int Height, TRasterFlags Flags, TPDFPixFormat PixFmt, TCompressionFilter Filter, TImageFormat Format);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfReOpenImportFile(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfReplaceFontA(IntPtr IPDF, IntPtr PDFFont, String Name, TFStyle Style, lbool Embed);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfReplaceFontW(IntPtr IPDF, IntPtr PDFFont, String Name, TFStyle Style, lbool Embed);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfReplaceFontExA(IntPtr IPDF, IntPtr PDFFont, String FontFile, lbool Embed);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfReplaceFontExW(IntPtr IPDF, IntPtr PDFFont, String FontFile, lbool Embed);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfReplaceICCProfileA(IntPtr IPDF, int ColorSpace, String ICCFile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfReplaceICCProfileW(IntPtr IPDF, int ColorSpace, String ICCFile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfReplaceICCProfileEx(IntPtr IPDF, int ColorSpace, byte[]Buffer, int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern int           pdfReplaceImage(IntPtr IPDF, IntPtr Source, String Image, int Index, TExtColorSpace CS, int CSHandle, TReplaceImageFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfReplaceImageEx(IntPtr IPDF, IntPtr Source, byte[]Buffer, int BufSize, int Index, TExtColorSpace CS, int CSHandle, TReplaceImageFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfReplacePageTextA(IntPtr IPDF, String NewText, ref TPDFStack Stack);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfReplacePageTextExA(IntPtr IPDF, String NewText, ref TPDFStack Stack);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfReplacePageTextExW(IntPtr IPDF, String NewText, ref TPDFStack Stack);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfResetAnnotAP(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfResetEncryptionSettings(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfResetLineDashPattern(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfRestoreGraphicState(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfRotateCoords(IntPtr IPDF, double alpha, double OriginX, double OriginY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfRoundRect(IntPtr IPDF, double PosX, double PosY, double Width, double Height, double Radius, TPathFillMode FillMode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfRoundRectEx(IntPtr IPDF, double PosX, double PosY, double Width, double Height, double rWidth, double rHeight, TPathFillMode FillMode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSaveGraphicState(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfScaleCoords(IntPtr IPDF, double sx, double sy);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSelfTest(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSet3DAnnotProps(IntPtr IPDF, int Annot, T3DActivationType ActType, T3DDeActivateType DeActType, T3DInstanceType InstType, T3DDeActInstance DeInstType, lbool DisplToolbar, lbool DisplModelTree);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSet3DAnnotScriptA(IntPtr IPDF, int Annot, String Value, int Len);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfSetAllocBy(IntPtr IPDF, int Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfSetAltFontsW(IntPtr IPDF, int ListHandle, String[]List, int Count);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotBorderEffect(IntPtr IPDF, int Handle, TBorderEffect Style);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotBorderStyle(IntPtr IPDF, int Handle, TBorderStyle Style);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotBorderWidth(IntPtr IPDF, int Handle, double LineWidth);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotColor(IntPtr IPDF, int Handle, TAnnotColor ColorType, TPDFColorSpace CS, uint Color);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotFlags(IntPtr IPDF, TAnnotFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotFlagsEx(IntPtr IPDF, int Handle, int Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotHighlightMode(IntPtr IPDF, int Handle, THighlightMode Mode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotIcon(IntPtr IPDF, int Handle, TAnnotIcon Icon);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotLineDashPattern(IntPtr IPDF, int Handle, float[]Dash, int NumValues);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotLineEndStyle(IntPtr IPDF, int Handle, TLineEndStyle Start, TLineEndStyle End);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern int           pdfSetAnnotMigrationState(IntPtr IPDF, int Annot, TAnnotState State, String User);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotOpacity(IntPtr IPDF, int Handle, double Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotOpenState(IntPtr IPDF, int Handle, bool Open);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotOrFieldDate(IntPtr IPDF, int Handle, lbool IsField, TDateType Type, uint DateTime);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotQuadPoints(IntPtr IPDF, int Handle, TFltPoint[]Value, int Count);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotStringA(IntPtr IPDF, int Handle, TAnnotString StringType, String Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfSetAnnotStringW(IntPtr IPDF, int Handle, TAnnotString StringType, String Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetAnnotSubjectA(IntPtr IPDF, int Handle, String Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfSetAnnotSubjectW(IntPtr IPDF, int Handle, String Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetBBox(IntPtr IPDF, TPageBoundary Boundary, double LeftX, double LeftY, double RightX, double RightY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetBidiMode(IntPtr IPDF, TPDFBidiMode Mode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetBookmarkDest(IntPtr IPDF, int ABmk, TDestType DestType, double a, double b, double c, double d);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetBookmarkStyle(IntPtr IPDF, int ABmk, TFStyle Style, uint RGBColor);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetBorderStyle(IntPtr IPDF, TBorderStyle Style);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetCharacterSpacing(IntPtr IPDF, double Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetCheckBoxChar(IntPtr IPDF, TCheckBoxChar CheckBoxChar);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetCheckBoxDefState(IntPtr IPDF, int Field, lbool Checked);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetCheckBoxState(IntPtr IPDF, int Field, lbool Checked);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfSetCIDFontA(IntPtr IPDF, int CMapHandle, String Name, TFStyle Style, double Size, lbool Embed);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfSetCIDFontW(IntPtr IPDF, int CMapHandle, String Name, TFStyle Style, double Size, lbool Embed);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfSetCMapDirA(IntPtr IPDF, String Path, TLoadCMapFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfSetCMapDirW(IntPtr IPDF, String Path, TLoadCMapFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetColDefFile(IntPtr IPDF, int EmbFile);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetColorMask(IntPtr IPDF, int ImageHandle, int[]Mask, int Count);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetColors(IntPtr IPDF, uint Color);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetColorSpace(IntPtr IPDF, TPDFColorSpace ColorSpace);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetColSortField(IntPtr IPDF, int ColField, lbool AscendingOrder);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetCompressionFilter(IntPtr IPDF, TCompressionFilter Filter);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetCompressionLevel(IntPtr IPDF, TCompressionLevel CompressLevel);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetContent(IntPtr IPDF, String Buffer, int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetDateTimeFormat(IntPtr IPDF, int TxtField, TPDFDateTime Fmt);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetDefBitsPerPixel(IntPtr IPDF, int Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetDocInfoA(IntPtr IPDF, TDocumentInfo DInfo, String Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfSetDocInfoW(IntPtr IPDF, TDocumentInfo DInfo, String Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetDocInfoExA(IntPtr IPDF, TDocumentInfo DInfo, String Key, String Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfSetDocInfoExW(IntPtr IPDF, TDocumentInfo DInfo, [MarshalAs(UnmanagedType.LPStr)]String Key, [MarshalAs(UnmanagedType.LPWStr)]String Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetDrawDirection(IntPtr IPDF, TDrawDirection Direction);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetEMFFrameDPI(IntPtr IPDF, int DPIX, int DPIY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetEMFPatternDistance(IntPtr IPDF, double Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetErrorMode(IntPtr IPDF, TErrMode ErrMode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetExtColorSpace(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetExtFillColorSpace(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetExtGState(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetExtStrokeColorSpace(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldBBox(IntPtr IPDF, int Field, ref TPDFRect BBox);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldBackColor(IntPtr IPDF, uint AColor);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldBorderColor(IntPtr IPDF, uint AColor);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldBorderWidth(IntPtr IPDF, int Field, double LineWidth);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldCalcOrder(IntPtr IPDF, int CurrIndex, int NewIndex);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldColor(IntPtr IPDF, int Field, TFieldColor ColorType, TPDFColorSpace CS, uint Color);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldBorderStyle(IntPtr IPDF, int Field, TBorderStyle Style);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldExpValueA(IntPtr IPDF, int Field, int ValIndex, String Value, String ExpValue, lbool Selected);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldExpValueW(IntPtr IPDF, int Field, int ValIndex, [MarshalAs(UnmanagedType.LPWStr)]String Value, String ExpValue, lbool Selected);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldExpValueEx(IntPtr IPDF, int Field, int ValIndex, lbool Selected, lbool DefSelected);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldFlags(IntPtr IPDF, int Field, TFieldFlags Flags, lbool Reset);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern int           pdfSetFieldFont(IntPtr IPDF, int Field, String Name, TFStyle Style, double Size, lbool Embed, TCodepage CP);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldFontEx(IntPtr IPDF, int Field, int Handle, double FontSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldFontSize(IntPtr IPDF, int Field, double FontSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldHighlightMode(IntPtr IPDF, int Field, THighlightMode Mode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldIndex(IntPtr IPDF, int Field, int Index);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfSetFieldMapName(IntPtr IPDF, int Field, String Name);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern lbool         pdfSetFieldName(IntPtr IPDF, int Field, String NewName);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldOrientation(IntPtr IPDF, int Field, int Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldTextAlign(IntPtr IPDF, int Field, TTextAlign Align);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldTextColor(IntPtr IPDF, uint Color);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFieldToolTipA(IntPtr IPDF, int Field, String Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfSetFieldToolTipW(IntPtr IPDF, int Field, String Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFillColor(IntPtr IPDF, uint Color);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFillColorEx(IntPtr IPDF, byte[]Color, int NumComponents);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFillColorF(IntPtr IPDF, float[]Color, int NumComponents);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFillColorSpace(IntPtr IPDF, TPDFColorSpace CS);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFloatPrecision(IntPtr IPDF, int NumTextDecDigits, int NumVectDecDigits);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern int           pdfSetFont(IntPtr IPDF, String Name, TFStyle Style, double Size, lbool Embed, TCodepage CP);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern int           pdfSetFontEx(IntPtr IPDF, String Name, TFStyle Style, double Size, lbool Embed, TCodepage CP);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFontOrigin(IntPtr IPDF, TOrigin Origin);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern void          pdfSetFontSearchOrder(IntPtr IPDF, TFontBaseType[]Order);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern void          pdfSetFontSearchOrderEx(IntPtr IPDF, TFontBaseType S1, TFontBaseType S2, TFontBaseType S3, TFontBaseType S4);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFontSelMode(IntPtr IPDF, TFontSelMode Mode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetFontWeight(IntPtr IPDF, int Weight);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern void          pdfSetGStateFlags(IntPtr IPDF, TGStateFlags Flags, lbool Reset);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetIconColor(IntPtr IPDF, uint Color);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetImportFlags(IntPtr IPDF, TImportFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetImportFlags2(IntPtr IPDF, TImportFlags2 Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetItalicAngle(IntPtr IPDF, double Angle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetJPEGQuality(IntPtr IPDF, int Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetLanguage(IntPtr IPDF, String ISOTag);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetLeading(IntPtr IPDF, double Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetLicenseKey(IntPtr IPDF, String Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetLineAnnotParms(IntPtr IPDF, int Handle, int FontHandle, double FontSize, TLineAnnotParms Parms);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetLineCapStyle(IntPtr IPDF, TLineCapStyle Style);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetLineDashPattern2(IntPtr IPDF, float[]Dash, int NumValues, float Phase);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetLineDashPatternEx(IntPtr IPDF, double[]Dash, int NumValues, int Phase);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetLineJoinStyle(IntPtr IPDF, TLineJoinStyle Style);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetLineWidth(IntPtr IPDF, double Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetLinkHighlightMode(IntPtr IPDF, THighlightMode Mode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetListFont(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetMatrix(IntPtr IPDF, ref TCTM Matrix);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern void          pdfSetMaxErrLogMsgCount(IntPtr IPDF, int Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetMaxFieldLen(IntPtr IPDF, int TxtField, int MaxLen);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetMetaConvFlags(IntPtr IPDF, TMetaFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetMetadata(IntPtr IPDF, TMetadataObj ObjType, int Handle, byte[]Buffer, int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetMiterLimit(IntPtr IPDF, double Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetNeedAppearance(IntPtr IPDF, lbool Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetNumberFormat(IntPtr IPDF, int TxtField, TDecSeparator Sep, int DecPlaces, TNegativeStyle NegStyle, String CurrStr, lbool Prepend);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetOCGContUsage(IntPtr IPDF, int Handle, ref TPDFOCGContUsage Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetOCGState(IntPtr IPDF, int Handle, lbool On, lbool SaveState);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetOnErrorProc(IntPtr IPDF, IntPtr Data, TErrorProc ErrProc);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetOnPageBreakProc(IntPtr IPDF, IntPtr Data, TOnPageBreakProc OnBreakProc);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetOpacity(IntPtr IPDF, double Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetOrientation(IntPtr IPDF, int Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetOrientationEx(IntPtr IPDF, int Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetPageBBox(IntPtr PagePtr, TPageBoundary Boundary, ref TFltRect BBox);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetPageCoords(IntPtr IPDF, TPageCoord PageCoords);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetPageFormat(IntPtr IPDF, TPageFormat Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetPageHeight(IntPtr IPDF, double Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetPageLayout(IntPtr IPDF, TPageLayout Layout);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetPageMode(IntPtr IPDF, TPageMode Mode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetPageOrientation(IntPtr IPDF, IntPtr PagePtr, int Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetPageWidth(IntPtr IPDF, double Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetPDFVersion(IntPtr IPDF, TPDFVersion Version);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetPrintSettings(IntPtr IPDF, TDuplexMode Mode, int PickTrayByPDFSize, int NumCopies, TPrintScaling PrintScaling, uint[]PrintRanges, int NumRanges);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetProgressProc(IntPtr IPDF, IntPtr Data, TInitProgressProc InitProgress, TProgressProc Progress);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetRenderingIntent(IntPtr IPDF, int ImgHandle, TRenderingIntent Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetResolution(IntPtr IPDF, int Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetSaveNewImageFormat(IntPtr IPDF, lbool Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetSeparationInfo(IntPtr IPDF, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetSpaceWidthFactor(IntPtr IPDF, double Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetStrokeColor(IntPtr IPDF, uint Color);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetStrokeColorEx(IntPtr IPDF, byte[]Color, int NumComponents);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetStrokeColorF(IntPtr IPDF, float[]Color, int NumComponents);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetStrokeColorSpace(IntPtr IPDF, TPDFColorSpace CS);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetTabLen(IntPtr IPDF, int TabLen);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetTemplBBox(IntPtr IPDF, int Handle, TPageBoundary Boundary, ref TPDFRect BBox);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetTextDrawMode(IntPtr IPDF, TDrawMode Mode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetTextFieldValueA(IntPtr IPDF, int Field, String Value, String DefValue, TTextAlign Align);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfSetTextFieldValueW(IntPtr IPDF, int Field, String Value, String DefValue, TTextAlign Align);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetTextFieldValueExA(IntPtr IPDF, int Field, String Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfSetTextFieldValueExW(IntPtr IPDF, int Field, String Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetTextRect(IntPtr IPDF, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetTextRise(IntPtr IPDF, double Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetTextScaling(IntPtr IPDF, double Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetTransparentColor(IntPtr IPDF, uint AColor);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern void          pdfSetTrapped(IntPtr IPDF, lbool Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetUseExactPwd(IntPtr IPDF, lbool Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetUseGlobalImpFiles(IntPtr IPDF, lbool Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetUseImageInterpolation(IntPtr IPDF, int Handle, lbool Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetUseImageInterpolationEx(IntPtr Image, lbool Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetUserUnit(IntPtr IPDF, float Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetUseStdFonts(IntPtr IPDF, lbool Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetUseSwapFile(IntPtr IPDF, lbool SwapContents, int SwapLimit);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetUseSwapFileEx(IntPtr IPDF, lbool SwapContents, int SwapLimit, String SwapDir);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetUseSystemFonts(IntPtr IPDF, lbool Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetUseTransparency(IntPtr IPDF, lbool Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetUseVisibleCoords(IntPtr IPDF, lbool Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetViewerPreferences(IntPtr IPDF, TViewerPreference Value, TViewPrefAddVal AddVal);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetWMFDefExtent(IntPtr IPDF, int Width, int Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetWMFPixelPerInch(IntPtr IPDF, int Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetWordSpacing(IntPtr IPDF, double Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSetXFAStream(IntPtr IPDF, int Index, byte[]Buffer, int BufSize);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSkewCoords(IntPtr IPDF, double alpha, double beta, double OriginX, double OriginY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSortFieldsByIndex(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfSortFieldsByName(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfSquareAnnotA(IntPtr IPDF, double PosX, double PosY, double Width, double Height, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Comment);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfSquareAnnotW(IntPtr IPDF, double PosX, double PosY, double Width, double Height, double LineWidth, uint FillColor, uint StrokeColor, TPDFColorSpace CS, String Author, String Subject, String Comment);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfStampAnnotA(IntPtr IPDF, TRubberStamp SubType, double PosX, double PosY, double Width, double Height, String Author, String Subject, String Comment);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfStampAnnotW(IntPtr IPDF, TRubberStamp SubType, double PosX, double PosY, double Width, double Height, String Author, String Subject, String Comment);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfStrLenA(IntPtr AStr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfStrLenW(IntPtr AStr);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfStrokePath(IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]   private static extern int           pdfTestGlyphsEx(IntPtr IPDF, int FontHandle, String Text, int Len);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfTextAnnotA(IntPtr IPDF, double PosX, double PosY, double Width, double Height, String Author, String Text, TAnnotIcon Icon, lbool Open);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfTextAnnotW(IntPtr IPDF, double PosX, double PosY, double Width, double Height, String Author, String Text, TAnnotIcon Icon, lbool Open);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfTranslateCoords(IntPtr IPDF, double OriginX, double OriginY);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfTriangle(IntPtr IPDF, double x1, double y1, double x2, double y2, double x3, double y3, TPathFillMode FillMode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfUnLockLayer(IntPtr IPDF, int Layer);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfWatermarkAnnot(IntPtr IPDF, double PosX, double PosY, double Width, double Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfWebLinkA(IntPtr IPDF, double PosX, double PosY, double Width, double Height, String URL);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern int           pdfWebLinkW(IntPtr IPDF, double PosX, double PosY, double Width, double Height, String URL);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfWriteAngleTextA(IntPtr IPDF, String AText, double Angle, double PosX, double PosY, double Radius, double YOrigin);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfWriteAngleTextW(IntPtr IPDF, String AText, double Angle, double PosX, double PosY, double Radius, double YOrigin);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfWriteFTextA(IntPtr IPDF, TTextAlign Align, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfWriteFTextW(IntPtr IPDF, TTextAlign Align, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfWriteFTextExA(IntPtr IPDF, double PosX, double PosY, double Width, double Height, TTextAlign Align, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfWriteFTextExW(IntPtr IPDF, double PosX, double PosY, double Width, double Height, TTextAlign Align, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfWriteTextA(IntPtr IPDF, double PosX, double PosY, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfWriteTextW(IntPtr IPDF, double PosX, double PosY, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfWriteTextExA(IntPtr IPDF, double PosX, double PosY, String AText, int Len);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfWriteTextExW(IntPtr IPDF, double PosX, double PosY, String AText, int Len);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfWriteTextMatrixA(IntPtr IPDF, ref TCTM M, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfWriteTextMatrixW(IntPtr IPDF, ref TCTM M, String AText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern lbool         pdfWriteTextMatrixExA(IntPtr IPDF, ref TCTM M, String AText, int Len);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]private static extern lbool         pdfWriteTextMatrixExW(IntPtr IPDF, ref TCTM M, String AText, int Len);

      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCopyMemUInt(IntPtr Source, uint[]Dest, int Len);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]   private static extern int           pdfCopyMemIntPtr(IntPtr Source, IntPtr[]Dest, int Len);
      // Helper function to copy a kerning record into the managed type TTextRecordW.
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi, EntryPoint="pdfCopyMem")] public static extern int CopyKernRecord(IntPtr Source, ref TTextRecordW Record, int Len);
   }

   public enum TCellContType
   {
      cctText,
      cctImage,
      cctTable,
      cctTemplate,
      cctAction
   }

   public enum TDeleteContent
   {
      dcText       = 0x00000001, // Text is always a foreground object
      dcImage      = 0x00000002,
      dcTemplate   = 0x00000004, // Templates can contain imported PDF pages, contents from EMF files and anything else that can be drawn into a template
      dcTable      = 0x00000008,
      dcAction     = 0x00000010, // A cell action is always a background object
      dcAllCont    = 0x0000001F, // Delete all content types
      dcForeGround = 0x10000000,
      dcBackGround = 0x20000000,
      dcBoth       = 0x30000000  // Delete both foreground and background objects
   }

   public enum TCellAlign
   {
      coLeft   = 0,
      coTop    = coLeft,
      coRight  = 1,
      coBottom = coRight,
      coCenter = 2
   }

   public enum TCellSide
   {
      csLeft,
      csBottom,
      csRight,
      csTop
   }

   public enum TColumnAdjust
   {
      coaUniqueWidth, // Set the column widths uniquely to TableWidth / NumColumns
      coaAdjLeft,     // Decrease or increase the column widths starting from the left side
      coaAdjRight     // Decrease or increase the column widths starting from the right side
   }

   public enum TTableColor
   {
      tcBackColor     = 0, // Table, Columns, Rows, Cells -> default none (transparent)
      tcBorderColor   = 1, // Table, Columns, Rows, Cells -> default black
      tcGridHorzColor = 2, // Table                       -> default black
      tcGridVertColor = 3, // Table                       -> default black
      tcImageColor    = 4, // Table, Columns, Rows, Cells -> default RGB black
      tcTextColor     = 5  // Table, Columns, Rows, Cells -> default black
   }

   public enum TTableBoxProperty
   {
      tbpBorderWidth = 0, // Table, Columns, Rows, Cells -> default (0, 0, 0, 0)
      tbpCellSpacing = 1, // Table, Columns, Rows, Cells -> default (0, 0, 0, 0)
      tbpCellPadding = 2  // Table, Columns, Rows, Cells -> default (0, 0, 0, 0)
   }

   public enum TTableFlags
   {
      tfDefault         = 0,
      tfStatic          = 1,  // This flag marks a row, column, or cell as static to avoid the deletion of the content with ClearContent().
      tfHeaderRow       = 2,  // Header rows are drawn first after a page break occurred
      tfNoLineBreak     = 4,  // Prohibit line breaks in cells whith text -> Can be set to the entire table, columns, rows, and cells
      tfScaleToRect     = 8,  // If set, the specified output width and height represents the maximum size of the image or template.
                              // The image or template is scaled into this rectangle without changing the aspect ratio.
      tfUseImageCS      = 16, // If set, images are inserted in the native image color space.
      tfAddFlags        = 32, // If set, the new flags are added to the current ones. If absent, the new flags override the previous value.
      tfFixedTextHeight = 64  // If set, text does not increase the row height if necessary. The row height must be non-zero in this case.
   }

   public class CPDFTable : IDisposable
   {
      private const string m_DynaPDFLib = "dynapdf";
      private CPDF         m_PDF;
      private ArrayList    m_SubTables;
      private IntPtr       m_Table;

      public CPDFTable(CPDF PDFInst, int AllocRows, int NumCols, float Width, float DefRowHeight)
      {
         m_PDF = PDFInst;
         m_Table = tblCreateTable(m_PDF.GetPDFInstance(), AllocRows, NumCols, Width, DefRowHeight);
         if (IntPtr.Zero.Equals(m_Table)) throw new System.Exception("Out of memory!");
      }
      ~CPDFTable()
      {
         DoDispose();
      }

      public void Dispose()
      {
         // do dispose code
         DoDispose();

         //suppress finalize
         GC.SuppressFinalize(this);
      }

      private void DoDispose()
      {
         //prevent this code from being called when it is already being called in another thread.
         lock(this)
         {
            m_SubTables = null;
            if (!IntPtr.Zero.Equals(m_Table)) tblDeleteTable(ref m_Table);
            m_PDF = null;
         }
      }

      public int AddColumn(int Left, float Width)
      {
         return tblAddColumn(m_Table, Left, Width);
      }

      public int AddRow(float Height)
      {
         return tblAddRow(m_Table, Height);
      }

      public int AddRows(int Count, float Height)
      {
         return tblAddRows(m_Table, Count, Height);
      }

      public void ClearColumn(int Col, TDeleteContent Types)
      {
         tblClearColumn(m_Table, Col, Types);
      }

      public void ClearContent(TDeleteContent Types)
      {
         tblClearContent(m_Table, Types);
      }

      public void ClearRow(int Row, TDeleteContent Types)
      {
         tblClearRow(m_Table, Row, Types);
      }

      public void DeleteCol(int Col)
      {
         tblDeleteCol(m_Table, Col);
      }

      public void DeleteRow(int Row)
      {
         tblDeleteRow(m_Table, Row);
      }

      public void DeleteRows()
      {
         tblDeleteRows(m_Table);
      }

      public float DrawTable(float x, float y, float MaxHeight)
      {
         // This is normally not required but we need to make sure that .Net doesn't delete the CPDF class before the table class is released
         tblSetPDFInstance(m_Table, m_PDF.GetPDFInstance());
         return tblDrawTable(m_Table, x, y, MaxHeight);
      }

      public int GetFirstRow()
      {
         return tblGetFirstRow(m_Table);
      }

      public int GetFlags(int Row, int Col)
      {
         return tblGetFlags(m_Table, Row, Col);
      }

      protected IntPtr GetInstancePtr()
      {
         return m_Table;
      }

      public float GetNextHeight(float MaxHeight, ref int NextRow)
      {
         return tblGetNextHeight(m_Table, MaxHeight, ref NextRow);
      }

      public int GetNextRow()
      {
         return tblGetNextRow(m_Table);
      }

      public int GetNumCols()
      {
         return tblGetNumCols(m_Table);
      }

      public int GetNumRows()
      {
         return tblGetNumRows(m_Table);
      }

      public float GetTableHeight()
      {
         return tblGetTableHeight(m_Table);
      }

      public float GetTableWidth()
      {
         return tblGetTableWidth(m_Table);
      }

      public bool HaveMore()
      {
         return tblHaveMore(m_Table) != 0;
      }

      public bool SetBoxProperty(int Row, int Col, TTableBoxProperty PropType, float Left, float Right, float Top, float Bottom)
      {
         return tblSetBoxProperty(m_Table, Row, Col, PropType, Left, Right, Top, Bottom) != 0;
      }

      public bool SetCellAction(int Row, int Col, int ActionHandle, THighlightMode Mode)
      {
         return tblSetCellAction(m_Table, Row, Col, ActionHandle, Mode) != 0;
      }

      public bool SetCellDashPattern(int Row, int Col, TCellSide Side, TLineCapStyle Style, float[] Dash, float Phase)
      {
         return tblSetCellDashPattern(m_Table, Row, Col, Side, Style, Dash, Dash.Length, Phase) != 0;
      }

      public bool SetCellImage(int Row, int Col, bool ForeGround, TCellAlign HAlign, TCellAlign VAlign, float Width, float Height, String Image, int Index)
      {
         return tblSetCellImage(m_Table, Row, Col, Convert.ToInt32(ForeGround), HAlign, VAlign, Width, Height, Image, Index) != 0;
      }

      public bool SetCellImageEx(int Row, int Col, bool ForeGround, TCellAlign HAlign, TCellAlign VAlign, float Width, float Height, byte[] Buffer, int Index)
      {
         return tblSetCellImageEx(m_Table, Row, Col, Convert.ToInt32(ForeGround), HAlign, VAlign, Width, Height, Buffer, Buffer.Length, Index) != 0;
      }

      public bool SetCellOrientation(int Row, int Col, int Orientation)
      {
         return tblSetCellOrientation(m_Table, Row, Col, Orientation) != 0;
      }

      public bool SetCellTable(int Row, int Col, TCellAlign HAlign, TCellAlign VAlign, CPDFTable SubTable)
      {
         // We store a reference of the sub table in a list to avoid the deletion. This ensures that the pointer of the sub table stays valid for the entire lifetime of the table that holds a reference of it.
         if (m_SubTables == null)
         {
            m_SubTables = new ArrayList();
            m_SubTables.Add(SubTable);
         }else if (m_SubTables.IndexOf(SubTable) < 0)
            m_SubTables.Add(SubTable);
         return tblSetCellTable(m_Table, Row, Col, HAlign, VAlign, SubTable.GetInstancePtr()) != 0;
      }

      public bool SetCellTemplate(int Row, int Col, bool ForeGround, TCellAlign HAlign, TCellAlign VAlign, int TmplHandle, float Width, float Height)
      {
         return tblSetCellTemplate(m_Table, Row, Col, System.Convert.ToInt32(ForeGround), HAlign, VAlign, TmplHandle, Width, Height) != 0;
      }

      public bool SetCellText(int Row, int Col, TTextAlign HAlign, TCellAlign VAlign, String Text)
      {
         return tblSetCellTextW(m_Table, Row, Col, HAlign, VAlign, Text, Text.Length) != 0;
      }

      public bool SetCellTextA(int Row, int Col, TTextAlign HAlign, TCellAlign VAlign, String Text)
      {
         return tblSetCellTextA(m_Table, Row, Col, HAlign, VAlign, Text, Text.Length) != 0;
      }

      public bool SetCellTextW(int Row, int Col, TTextAlign HAlign, TCellAlign VAlign, String Text)
      {
         return tblSetCellTextW(m_Table, Row, Col, HAlign, VAlign, Text, Text.Length) != 0;
      }

      public bool SetColor(int Row, int Col, TTableColor ClrType, TPDFColorSpace CS, uint Color)
      {
         return tblSetColor(m_Table, Row, Col, ClrType, CS, Color) != 0;
      }

      public bool SetColorEx(int Row, int Col, TTableColor ClrType, float[] Color, DynaPDF.TExtColorSpace CS, int Handle)
      {
         return tblSetColorEx(m_Table, Row, Col, ClrType, Color, Color.Length, CS, Handle) != 0;
      }

      public bool SetColWidth(int Col, float Width, bool ExtTable)
      {
         return tblSetColWidth(m_Table, Col, Width, Convert.ToInt32(ExtTable)) != 0;
      }

      public bool SetFlags(int Row, int Col, TTableFlags Flags)
      {
         return tblSetFlags(m_Table, Row, Col, Flags) != 0;
      }

      public bool SetFont(int Row, int Col, String Name, TFStyle Style, bool Embed, TCodepage CP)
      {
         return tblSetFont(m_Table, Row, Col, Name, Style, Convert.ToInt32(Embed), CP) != 0;
      }

      public bool SetFontSelMode(int Row, int Col, TFontSelMode Mode)
      {
         return tblSetFontSelMode(m_Table, Row, Col, Mode) != 0;
      }

      public bool SetFontSize(int Row, int Col, float Value)
      {
         return tblSetFontSize(m_Table, Row, Col, Value) != 0;
      }

      public bool SetGridWidth(float Horz, float Vert)
      {
         return tblSetGridWidth(m_Table, Horz, Vert) != 0;
      }

      public bool SetRowHeight(int Row, float Value)
      {
         return tblSetRowHeight(m_Table, Row, Value) != 0;
      }

      public void SetTableWidth(float Value, TColumnAdjust AdjustType, float MinColWidth)
      {
         tblSetTableWidth(m_Table, Value, AdjustType, MinColWidth);
      }

      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern int           tblAddColumn(IntPtr Table, lbool Left, float Width);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern int           tblAddRow(IntPtr Table, float Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern int           tblAddRows(IntPtr Table, int Count, float Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          tblClearColumn(IntPtr Table, int Col, TDeleteContent Types);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          tblClearContent(IntPtr Table, TDeleteContent Types);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          tblClearRow(IntPtr Table, int Row, TDeleteContent Types);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern IntPtr        tblCreateTable(IntPtr IPDF, int AllocRows, int NumCols, float Width, float DefRowHeight);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          tblDeleteCol(IntPtr Table, int Col);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          tblDeleteRow(IntPtr Table, int Row);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          tblDeleteRows(IntPtr Table);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          tblDeleteTable(ref IntPtr Table);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern float         tblDrawTable(IntPtr Table, float x, float y, float MaxHeight);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern int           tblGetFirstRow(IntPtr Table);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern int           tblGetFlags(IntPtr Table, int Row, int Col);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern float         tblGetNextHeight(IntPtr Table, float MaxHeight, ref int NextRow);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern int           tblGetNextRow(IntPtr Table);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern int           tblGetNumCols(IntPtr Table);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern int           tblGetNumRows(IntPtr Table);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern float         tblGetTableHeight(IntPtr Table);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern float         tblGetTableWidth(IntPtr Table);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblHaveMore(IntPtr Table);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetBoxProperty(IntPtr Table, int Row, int Col, TTableBoxProperty Type, float Left, float Right, float Top, float Bottom);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetCellAction(IntPtr Table, int Row, int Col, int ActionHandle, THighlightMode Mode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetCellDashPattern(IntPtr Table, int Row, int Col, TCellSide Side, TLineCapStyle Style, float[] Dash, int NumValues, float Phase);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]    private static extern lbool         tblSetCellImage(IntPtr Table, int Row, int Col, lbool ForeGround, TCellAlign HAlign, TCellAlign VAlign, float Width, float Height, String Image, int Index);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetCellImageEx(IntPtr Table, int Row, int Col, lbool ForeGround, TCellAlign HAlign, TCellAlign VAlign, float Width, float Height, byte[] Buffer, int BufSize, int Index);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetCellOrientation(IntPtr Table, int Row, int Col, int Orientation);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetCellTable(IntPtr Table, int Row, int Col, TCellAlign HAlign, TCellAlign VAlign, IntPtr SubTable);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetCellTemplate(IntPtr Table, int Row, int Col, lbool ForeGround, TCellAlign HAlign, TCellAlign VAlign, int TmplHandle, float Width, float Height);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetCellTextA(IntPtr Table, int Row, int Col, TTextAlign HAlign, TCellAlign VAlign, String Text, int Len);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)] private static extern lbool         tblSetCellTextW(IntPtr Table, int Row, int Col, TTextAlign HAlign, TCellAlign VAlign, String Text, int Len);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetColor(IntPtr Table, int Row, int Col, TTableColor Type, TPDFColorSpace CS, uint Color);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetColorEx(IntPtr Table, int Row, int Col, TTableColor Type, float[] Color, int NumComps, DynaPDF.TExtColorSpace CS, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetColWidth(IntPtr Table, int Col, float Width, lbool ExtTable);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetFlags(IntPtr Table, int Row, int Col, TTableFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Auto)]    private static extern lbool         tblSetFont(IntPtr Table, int Row, int Col, String Name, TFStyle Style, lbool Embed, TCodepage CP);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetFontSelMode(IntPtr Table, int Row, int Col, TFontSelMode Mode);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetFontSize(IntPtr Table, int Row, int Col, float Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetGridWidth(IntPtr Table, float Horz, float Vert);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          tblSetPDFInstance(IntPtr Table, IntPtr IPDF);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern lbool         tblSetRowHeight(IntPtr Table, int Row, float Value);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]    private static extern void          tblSetTableWidth(IntPtr Table, float Value, TColumnAdjust AdjustType, float MinColWidth);
   }
   
   /* -------------------------------------------------------------------------------------------------------------------------------------------------------------------------- */
   
   [StructLayout(LayoutKind.Sequential, Pack=1)]
   public struct TContentOP
   {
      public byte   OP;    // Operator to excecute.
      public IntPtr Param; // It is guaranteed that this pointer is set for operators which have parameters.
   };

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public struct TContent
   {
      public int    Count; // Number of available operators.
      public IntPtr OP;    // Array of operators.
   };
   
   public enum TContentParseFlags
   {
      cpfDefault              = 0x00000000, // Nothing special to do.
      cpfComputeBBox          = 0x00000001, // Compute bounding boxes of all objects.
      cpfFlattenLayers        = 0x00000002, // Flatten layers.
      cpfSkipInvisibleObjects = 0x00000004, // Ignore invisible objects.
      cpfFullRecursive        = 0x00000008, // Parse all objects recursively.
      cpfNoInlineTemplate     = 0x00000010, // Do not resolve templates with a reference count of 1.
      cpfCalcDeviceColors     = 0x00000020, // Compute device colors of all colors which are set in the content streams.
      cpfImidiateMode         = 0x00000040, // Internal. This flag is always set if the TPDFParseCallbacks structure is passed to ParsePage(). It disables certain optimisations.
      cpfNewLinkNames         = 0x00000080, // Internal. Create new link names for all objects. This flag is used by Optimize() and CheckConformance().
      cpfEnableTextSelection  = 0x00000100, // This flag is required to enable text selection and text extraction.
      cpfInitMatrix           = 0x00000200, // The transformation matrix must be set in the TContentParseParms structure.
      cpfSkipClipPaths        = 0x00000400, // Useful for debugging purposes.
      cpfSkipImages           = 0x00000800, // Ignore all images. This flag is useful for text extraction.
      cpfSkipShadings         = 0x00001000, // Useful for debugging purposes.
      cpfSkipText             = 0x00002000, // Useful for debugging purposes.
      cpfSkipVector           = 0x00004000  // Useful for debugging purposes. Exclude vector graphics with exception of clipping paths.
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public class TContentParseParms
   {
      internal int  StructSize;
      public   TCTM InitMatrix; // Considered only if the flag cpfInitMatrix is set.
      
      public TContentParseParms()
      {
         StructSize = Marshal.SizeOf(this);
      }
   };

   public enum TDeleteTextFlags
   {
      dtfDefault = 0
   }

   public enum TReplaceTextFlags
   {
      rtfDefault = 0
   }

   public enum TSearchType
   {
      stDefault         = 0, // Case sensitive search
      stWholeWord       = 1, // Only whole words
      stCaseInSensitive = 2, // Case insensitive search
      stMatchAlways     = 4  // Return every character that was found
   }

   [StructLayout(LayoutKind.Sequential, Pack=0)]
   public class TTextSelection
   {
      internal int    StructSize; // Must be set to sizeof(TTextSelection).
      public   int    Line;       // Index of the text line.
      public   TCTM   Matrix;     // Text matrix pre-calculated in user space.
      public   int    Sub1;       // Index of the text node.
      public   int    Sub2;       // Index of the sub text node.
      public   int    TextLen;    // How many characters of this record are selected?
      public   IntPtr TextOP;     // The text record in which the text was found.
      public   int    TextPos;    // Index where the first character of the search text was found.
      public   float  X;          // X-Coordinate in text space
      public   float  Y;          // Y-Coordinate in text space
      public   IntPtr Next;       // Next record if the search text spans multiple text records.
      public   IntPtr Prev;       // Prev record if any.
      public TTextSelection()
      {
         StructSize = Marshal.SizeOf(this);
      }
   };

   public class CPDFContentParser : IDisposable
   {
      private const string m_DynaPDFLib = "dynapdf";
      private IntPtr            m_Ctx;
      private IntPtr            m_Instance;
      private TOptimizeParams_I m_Parms;
      private CPDF              m_PDF;
      public CPDFContentParser(CPDF PDFInst, TOptimizeFlags Flags, TOptimizeParams Parms)
      {
         m_Instance = PDFInst.GetPDFInstance();
         m_Parms    = null;
         m_PDF      = PDFInst;
         m_PDF.GetOptimizeParams(Parms, false, ref m_Parms);
         m_Ctx = psrCreateParserContext(m_Instance, Flags, m_Parms);
         if (IntPtr.Zero.Equals(m_Ctx)) throw new System.Exception("Out of Memory");
      }
      ~CPDFContentParser()
      {
         DoDispose();
      }

      public void Dispose()
      {
         // do dispose code
         DoDispose();

         //suppress finalize
         GC.SuppressFinalize(this);
      }

      private void DoDispose()
      {
         //prevent this code from being called when it is already being called in another thread.
         lock(this)
         {
            if (m_Parms != null && m_Parms.ExcludeCS != IntPtr.Zero)
               Marshal.FreeHGlobal(m_Parms.ExcludeCS);
            if (!IntPtr.Zero.Equals(m_Ctx)) psrDeleteParserContext(ref m_Ctx);
            m_PDF = null;
         }
      }

      public void Abort()
      {
         psrAbort(m_Ctx);
      }

      public bool ChangeAltFont(int Handle)
      {
         return psrChangeAltFont(m_Instance, m_Ctx, Handle) != 0;
      }

      public void ClearSelection()
      {
         psrClearSelection(m_Ctx);
      }

      public bool DeleteOperator(int Index)
      {
         return psrDeleteOperator(m_Ctx, Index) != 0;
      }

      public bool DeleteOperatorInObject(IntPtr ObjPtr, int Index)
      {
         return psrDeleteOperatorInObject(m_Ctx, ObjPtr, Index) != 0;
      }

      public bool DeleteText(TDeleteTextFlags Flags)
      {
         return psrDeleteText1(m_Instance, m_Ctx, IntPtr.Zero, Flags) != 0;
      }

      public bool DeleteText(ref TFltRect Area, TDeleteTextFlags Flags)
      {
         return psrDeleteText2(m_Instance, m_Ctx, ref Area, Flags) != 0;
      }

      public bool ExtractText(TTextExtractionFlags Flags, ref String Text)
      {
         int    len = 0;
         IntPtr txt = IntPtr.Zero;
         Text = null;
         if (psrExtractText1(m_Instance, m_Ctx, Flags, IntPtr.Zero, ref txt, ref len) != 0)
         {
            Text = Marshal.PtrToStringUni(txt, len);
            return true;
         }
         return false;
      }

      public bool ExtractText2(TTextExtractionFlags Flags, ref TFltRect Area, ref String Text)
      {
         int    len = 0;
         IntPtr txt = IntPtr.Zero;
         Text = null;
         if (psrExtractText2(m_Instance, m_Ctx, Flags, ref Area, ref txt, ref len) != 0)
         {
            Text = Marshal.PtrToStringUni(txt, len);
            return true;
         }
         return false;
      }

      public bool FindText(TSearchType SearchType, TTextSelection Last, String Text, TTextSelection SelText)
      {
         return psrFindText1(m_Instance, m_Ctx, IntPtr.Zero, SearchType, Last, Text, Text.Length, SelText) != 0;
      }

      public bool FindText(ref TFltRect Area, TSearchType SearchType, TTextSelection Last, String Text, TTextSelection SelText)
      {
         return psrFindText2(m_Instance, m_Ctx, ref Area, SearchType, Last, Text, Text.Length, SelText) != 0;
      }

      public bool GetSelBBox(TTextSelection SelText, ref TBBox BBox)
      {
         return psrGetSelBBox(m_Instance, m_Ctx, SelText, ref BBox) != 0;
      }

      public bool ParsePage(int PageNum, TContentParseFlags Flags, ref TContent Out)
      {
         return psrParsePage(m_Instance, m_Ctx, IntPtr.Zero, null, PageNum, Flags, null, ref Out) != 0;
      }

      public bool ParsePage(int PageNum, TContentParseFlags Flags, TContentParseParms Parms, ref TContent Out)
      {
         return psrParsePage(m_Instance, m_Ctx, IntPtr.Zero, null, PageNum, Flags, Parms, ref Out) != 0;
      }

      public bool ReplaceSelText(TReplaceTextFlags Flags, TTextSelection SelText, String Text)
      {
         return psrReplaceSelText(m_Instance, m_Ctx, Flags, SelText, Text, Text.Length) != 0;
      }

      public void Reset()
      {
         psrReset(m_Ctx);
      }

      public int SetAltFont(String Name, TFStyle Style, double Size, bool Embed, TCodepage CP)
      {
         return psrSetAltFontW(m_Instance, m_Ctx, Name, Style, Size, Convert.ToInt32(Embed), CP);
      }

      public bool WriteToPage(TOptimizeFlags Flags, TOptimizeParams Parms)
      {
         bool retval = false;
         TOptimizeParams_I p = null;
         try
         {
            m_PDF.GetOptimizeParams(Parms, false, ref p);
            retval = psrWriteToPage(m_Instance, m_Ctx, Flags, p) != 0;
         }finally
         {
            if (p != null && p.ExcludeCS != IntPtr.Zero)
               Marshal.FreeHGlobal(p.ExcludeCS);
         }
         return retval;
      }

      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]                               private static extern void   psrAbort(IntPtr Ctx);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]                               private static extern lbool  psrChangeAltFont(IntPtr IPDF, IntPtr Ctx, int Handle);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]                               private static extern void   psrClearSelection(IntPtr Ctx);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]                               private static extern IntPtr psrCreateParserContext(IntPtr IPDF, TOptimizeFlags Flags, TOptimizeParams_I Parms);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]                               private static extern lbool  psrDeleteOperator(IntPtr Ctx, int Index);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]                               private static extern lbool  psrDeleteOperatorInObject(IntPtr Ctx, IntPtr ObjPtr, int Index);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]                               private static extern void   psrDeleteParserContext(ref IntPtr Ctx);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi, EntryPoint="psrDeleteText")]   private static extern int    psrDeleteText1(IntPtr IPDF, IntPtr Ctx, IntPtr Area, TDeleteTextFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi, EntryPoint="psrDeleteText")]   private static extern int    psrDeleteText2(IntPtr IPDF, IntPtr Ctx, ref TFltRect Area, TDeleteTextFlags Flags);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi, EntryPoint="psrExtractText")]  private static extern lbool  psrExtractText1(IntPtr IPDF, IntPtr Ctx, TTextExtractionFlags Flags, IntPtr Area, ref IntPtr Text, ref int TextLen);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi, EntryPoint="psrExtractText")]  private static extern lbool  psrExtractText2(IntPtr IPDF, IntPtr Ctx, TTextExtractionFlags Flags, ref TFltRect Area, ref IntPtr Text, ref int TextLen);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode, EntryPoint="psrFindText")]  private static extern lbool  psrFindText1(IntPtr IPDF, IntPtr Ctx, IntPtr Area, TSearchType SearchType, TTextSelection Last, String Text, int TextLen, TTextSelection SelText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode, EntryPoint="psrFindText")]  private static extern lbool  psrFindText2(IntPtr IPDF, IntPtr Ctx, ref TFltRect Area, TSearchType SearchType, TTextSelection Last, String Text, int TextLen, TTextSelection SelText);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]                               private static extern lbool  psrGetSelBBox(IntPtr IPDF, IntPtr Ctx, TTextSelection SelText, ref TBBox BBox);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]                               private static extern lbool  psrParsePage(IntPtr IPDF, IntPtr Ctx, IntPtr UserData, TPDFParseCallbacks Funcs, int PageNum, TContentParseFlags Flags, TContentParseParms Parms, ref TContent Out);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]                            private static extern lbool  psrReplaceSelText(IntPtr IPDF, IntPtr Ctx, TReplaceTextFlags Flags, TTextSelection SelText, String Text, int TextLen);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]                               private static extern void   psrReset(IntPtr Ctx);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Unicode)]                            private static extern int    psrSetAltFontW(IntPtr IPDF, IntPtr Ctx, String Name, TFStyle Style, double Size, lbool Embed, TCodepage CP);
      [DllImport(m_DynaPDFLib, CharSet=CharSet.Ansi)]                               private static extern lbool  psrWriteToPage(IntPtr IPDF, IntPtr Ctx, TOptimizeFlags Flags, TOptimizeParams_I Parms);
   }
}
