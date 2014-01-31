
using System;
using System.Runtime.InteropServices;

public class CrossXColor {

    public const int icPerceptual = 0;
    public const int icRelativeColorimetric = 1;
    public const int icSaturation = 2;
    public const int icAbsoluteColorimetric = 3;
    public const int icMaxEnumIntent = 4;

    public const int icSigXYZData = 0x58595A20;
    public const int icSigLabData = 0x4C616220;
    public const int icSigLuvData = 0x4C757620;
    public const int icSigYCbCrData = 0x59436272;
    public const int icSigYxyData = 0x59787920;
    public const int icSigRgbData = 0x52474220;
    public const int icSigGrayData = 0x47524159;
    public const int icSigHsvData = 0x48535620;
    public const int icSigHlsData = 0x484C5320;
    public const int icSigCmykData = 0x434D594B;
    public const int icSigCmyData = 0x434D5920;
    public const int icSigDensityData = 0x44454E53;
    public const int clRemission = 0x7265666C;

    public const int clDensityCMYK = 0x44454E53;

    // CLErr values
    public const int CLNoErr = 0;
    public const int CLUndefProc = 1;
	public const int CLUnexpectedErr = 2;
	public const int CLArgErr = 3;
	public const int CLMemFullErr = 4;
	public const int CLBufferError = 5;
	public const int CLBufferTooSmall = 6;
	public const int CLIndexError = 7;
	public const int CLEOFReached = 8;
	public const int CLNotFound = 9;
	public const int CLWrongDataType = 10;
	public const int CLFileInvalidDesc = 11;
	public const int CLFileNotOpen = 12;
	public const int CLFileLengthFailed = 13;
	public const int CLFileSeekFailed = 14;
	public const int CLFilePosFailed = 15;
	public const int CLFileReadFailed = 16;
	public const int CLFileWriteFailed = 17;
	public const int CLFileAccessErr = 18;
	public const int CLFormatErr = 19;
	public const int CLPtrInvalid = 20;
	public const int CLStringInvalid = 21;
	public const int CLDivZero = 22;
	public const int CLDimMismatch = 23;
	public const int CLSingularMatrixErr = 24;
	public const int CLMatrixSVDErr = 25;
    public const int CLLibraryNLoadErr = 26;
	public const int CLCreateObjectErr = 27;
	public const int CLIllegalMeasurementData = 28;
	public const int CLInvalidGammaTable = 30;
	public const int CLInvalidLicense = 31;
	public const int CLInvalidICCTag = 32;
	public const int CLMissingICCTag = 33;
	public const int CLMissingICCTagData = 34;
	public const int CLInvalidTrafo = 35;
	public const int CLTrafoCreationErr = 36;
	public const int CLICCProfileClassErr = 37;
	public const int CLInvalidFileSpec = 38;

    public enum CLErrorType
    {

        CLNoErr = 0,

        CLUndefProc = 1,
        CLUnexpectedErr = 2,
        CLArgErr = 3,
        CLMemFullErr = 4,
        CLBufferError = 5,
        CLBufferTooSmall = 6,
        CLIndexError = 7,
        CLEOFReached = 8,
        CLNotFound = 9,
        CLWrongDataType = 10,
        CLFileInvalidDesc = 11,
        CLFileNotOpen = 12,
        CLFileLengthFailed = 13,
        CLFileSeekFailed = 14,
        CLFilePosFailed = 15,
        CLFileReadFailed = 16,
        CLFileWriteFailed = 17,
        CLFileAccessErr = 18,
        CLFormatErr = 19,
        CLPtrInvalid = 20,
        CLStringInvalid = 21,

        CLDivZero = 22,
        CLDimMismatch = 23,
        CLSingularMatrixErr = 24,
        CLMatrixSVDErr = 25,

        CLLibraryNLoadErr = 26,
        CLCreateObjectErr = 27,
        CLIllegalMeasurementData = 28,
        CLInvalidGammaTable = 30,
        CLInvalidLicense = 31,

        CLInvalidICCTag = 32,
        CLMissingICCTag = 33,
        CLMissingICCTagData = 34,
        CLInvalidTrafo = 35,
        CLTrafoCreationErr = 36,
        CLICCProfileClassErr = 37,

        CLInvalidFileSpec = 38

    }

  public static TYPE_p_void CLNewCLProfilerRef() {
    IntPtr cPtr = CrossXColorPINVOKE.CLNewCLProfilerRef();
    TYPE_p_void ret = (cPtr == IntPtr.Zero) ? null : new TYPE_p_void(cPtr, false);
    return ret;
  }

  public static void CLFreeCLProfilerRef(TYPE_p_void profRef) {
      CrossXColorPINVOKE.CLFreeCLProfilerRef(TYPE_p_void.getCPtr(profRef));
  }

  public static TYPE_p_void CLNewICCProfileRef() {
    IntPtr cPtr = CrossXColorPINVOKE.CLNewICCProfileRef();
    TYPE_p_void ret = (cPtr == IntPtr.Zero) ? null : new TYPE_p_void(cPtr, false);
    return ret;
  }

  public static void CLFreeICCProfileRef(TYPE_p_void profRef) {
    CrossXColorPINVOKE.CLFreeICCProfileRef(TYPE_p_void.getCPtr(profRef));
  }

  public static TYPE_p_p_void CLNewFileSpecRef() {
    IntPtr cPtr = CrossXColorPINVOKE.CLNewFileSpecRef();
    TYPE_p_p_void ret = (cPtr == IntPtr.Zero) ? null : new TYPE_p_p_void(cPtr, false);
    return ret;
  }

  public static void CLFreeFileSpecRef(TYPE_p_p_void fSpecRef)
  {
      CrossXColorPINVOKE.CLFreeFileSpecRef(TYPE_p_p_void.getCPtr(fSpecRef));
  }

  public static TYPE_p_p_void CLName2FileSpec(TYPE_p_p_void fSpecRef, string filename) {
      IntPtr cPtr = CrossXColorPINVOKE.CLName2FileSpec(TYPE_p_p_void.getCPtr(fSpecRef), filename);
    TYPE_p_p_void ret = (cPtr == IntPtr.Zero) ? null : new TYPE_p_p_void(cPtr, false);
    return ret;
  }

  public static CLVectorDataSet CLInitVectorDataSet(CLVectorDataSet set, int dataType, double[] data, int dataSize) {
    IntPtr cPtr = CrossXColorPINVOKE.CLInitVectorDataSet(CLVectorDataSet.getCPtr(set), dataType, data, dataSize);
    CLVectorDataSet ret = (cPtr == IntPtr.Zero) ? null : new CLVectorDataSet(cPtr, false);
    return ret;
  }

  public static int CLSetProfilerOption(TYPE_p_void profRef, string OptionName, string OptionValue)
  {
      int ret = CrossXColorPINVOKE.CLSetProfilerOption(TYPE_p_void.getCPtr(profRef), OptionName, OptionValue);
    return ret;
  }

  public static int CLSetOptionProfile(TYPE_p_void profRef, string OptionName, TYPE_p_void profileRef)
  {
      int ret = CrossXColorPINVOKE.CLSetOptionProfile(TYPE_p_void.getCPtr(profRef), OptionName, TYPE_p_void.getCPtr(profileRef));
      return ret;
  }

  public static int CLReadProfile(TYPE_p_void profRef, TYPE_p_p_void fileRef)
  {
      int ret = CrossXColorPINVOKE.CLReadProfile(TYPE_p_void.getCPtr(profRef), TYPE_p_p_void.getCPtr(fileRef));
      return ret;
  }

    public static int CLReadProfileFromMemory(TYPE_p_void profRef, IntPtr buf, int bufSize)
  {
    int ret = CrossXColorPINVOKE.CLReadProfileFromMemory(TYPE_p_void.getCPtr(profRef), buf, bufSize);
    return ret;
  }

  public static int CLSetProfilerData(TYPE_p_void profRef, int count, CLVectorDataSet DCSDataSet, CLVectorDataSet PCSDataSet)
  {
      int ret = CrossXColorPINVOKE.CLSetProfilerData(TYPE_p_void.getCPtr(profRef), count, CLVectorDataSet.getCPtr(DCSDataSet), CLVectorDataSet.getCPtr(PCSDataSet));
    return ret;
  }

  public static int CLWriteProfile(TYPE_p_void profRef, TYPE_p_p_void fileRef, int option)
  {
    int ret = CrossXColorPINVOKE.CLWriteProfile(TYPE_p_void.getCPtr(profRef), TYPE_p_p_void.getCPtr(fileRef), option);
    return ret;
  }

  public static int CLWriteProfileGetSize(TYPE_p_void profRef, ref int profSize)
  {
    int ret = CrossXColorPINVOKE.CLWriteProfileGetSize(TYPE_p_void.getCPtr(profRef), ref profSize);
    return ret;
  }

  public static int CLWriteProfileToMemory(TYPE_p_void profRef, IntPtr buf, int profSize)
  {
    int ret = CrossXColorPINVOKE.CLWriteProfileToMemory(TYPE_p_void.getCPtr(profRef), buf, profSize);
    return ret;
  }

  public static int CLCalcProfile(TYPE_p_void profRef, TYPE_p_void profileRef)
  {
      int ret = CrossXColorPINVOKE.CLCalcProfile(TYPE_p_void.getCPtr(profRef), TYPE_p_void.getCPtr(profileRef));
    return ret;
  }

  public static TYPE_p_void CLConcatICCProfileRefs(int numberOfProfiles, TYPE_p_void[] profileRefs, int[] renderingIntents)
  {
    HandleRef[] profArray = new HandleRef[numberOfProfiles];
    for (int i = 0; i < numberOfProfiles; i++) {
        profArray[i] = TYPE_p_void.getCPtr(profileRefs[i]);
    }
    IntPtr cPtr = CrossXColorPINVOKE.CLConcatICCProfileRefs(numberOfProfiles, profArray, renderingIntents);
    TYPE_p_void ret = (cPtr == IntPtr.Zero) ? null : new TYPE_p_void(cPtr, false);
    return ret;
  }

  public static int CLDoTrafo(TYPE_p_void tRef, double[] x, double[] y) {
    int ret = CrossXColorPINVOKE.CLDoTrafo(TYPE_p_void.getCPtr(tRef), x, y);
    return ret;
  }

  public static TYPE_p_void CLConcatenateTrafos(TYPE_p_void tRef1, TYPE_p_void tRef2, TYPE_p_void tRef3, TYPE_p_void tRef4, TYPE_p_void tRef5, TYPE_p_void tRef6)
  {
      IntPtr cPtr = CrossXColorPINVOKE.CLConcatenateTrafos(TYPE_p_void.getCPtr(tRef1), TYPE_p_void.getCPtr(tRef2), TYPE_p_void.getCPtr(tRef3), TYPE_p_void.getCPtr(tRef4), TYPE_p_void.getCPtr(tRef5), TYPE_p_void.getCPtr(tRef6));
      TYPE_p_void ret = (cPtr == IntPtr.Zero) ? null : new TYPE_p_void(cPtr, false);
      return ret;
  }

  public static void CLFreeCLTrafoRef(TYPE_p_void tRef) {
      CrossXColorPINVOKE.CLFreeCLTrafoRef(TYPE_p_void.getCPtr(tRef));
  }

  public static int CLCrossXColorSetOption(string OptionName, string OptionValue)
  {
      int ret = CrossXColorPINVOKE.CLCrossXColorSetOption(OptionName, OptionValue);
      return ret;
  }

}
