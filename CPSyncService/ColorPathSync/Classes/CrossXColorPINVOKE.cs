
using System;
using System.Runtime.InteropServices;

class CrossXColorPINVOKE {

  protected class XPLATExceptionHelper {

    public delegate void ExceptionDelegate(string message);
    public delegate void ExceptionArgumentDelegate(string message, string paramName);

    static ExceptionDelegate applicationDelegate = new ExceptionDelegate(SetPendingApplicationException);
    static ExceptionDelegate arithmeticDelegate = new ExceptionDelegate(SetPendingArithmeticException);
    static ExceptionDelegate divideByZeroDelegate = new ExceptionDelegate(SetPendingDivideByZeroException);
    static ExceptionDelegate indexOutOfRangeDelegate = new ExceptionDelegate(SetPendingIndexOutOfRangeException);
    static ExceptionDelegate invalidCastDelegate = new ExceptionDelegate(SetPendingInvalidCastException);
    static ExceptionDelegate invalidOperationDelegate = new ExceptionDelegate(SetPendingInvalidOperationException);
    static ExceptionDelegate ioDelegate = new ExceptionDelegate(SetPendingIOException);
    static ExceptionDelegate nullReferenceDelegate = new ExceptionDelegate(SetPendingNullReferenceException);
    static ExceptionDelegate outOfMemoryDelegate = new ExceptionDelegate(SetPendingOutOfMemoryException);
    static ExceptionDelegate overflowDelegate = new ExceptionDelegate(SetPendingOverflowException);
    static ExceptionDelegate systemDelegate = new ExceptionDelegate(SetPendingSystemException);

    static ExceptionArgumentDelegate argumentDelegate = new ExceptionArgumentDelegate(SetPendingArgumentException);
    static ExceptionArgumentDelegate argumentNullDelegate = new ExceptionArgumentDelegate(SetPendingArgumentNullException);
    static ExceptionArgumentDelegate argumentOutOfRangeDelegate = new ExceptionArgumentDelegate(SetPendingArgumentOutOfRangeException);

    [DllImport("CrossXColorWrap", EntryPoint="XPLATRegisterExceptionCallbacks_CrossXColor")]
    public static extern void XPLATRegisterExceptionCallbacks_CrossXColor(
                                ExceptionDelegate applicationDelegate,
                                ExceptionDelegate arithmeticDelegate,
                                ExceptionDelegate divideByZeroDelegate, 
                                ExceptionDelegate indexOutOfRangeDelegate, 
                                ExceptionDelegate invalidCastDelegate,
                                ExceptionDelegate invalidOperationDelegate,
                                ExceptionDelegate ioDelegate,
                                ExceptionDelegate nullReferenceDelegate,
                                ExceptionDelegate outOfMemoryDelegate, 
                                ExceptionDelegate overflowDelegate, 
                                ExceptionDelegate systemExceptionDelegate);

    [DllImport("CrossXColorWrap", EntryPoint="XPLATRegisterExceptionArgumentCallbacks_CrossXColor")]
    public static extern void XPLATRegisterExceptionCallbacksArgument_CrossXColor(
                                ExceptionArgumentDelegate argumentDelegate,
                                ExceptionArgumentDelegate argumentNullDelegate,
                                ExceptionArgumentDelegate argumentOutOfRangeDelegate);

    static void SetPendingApplicationException(string message) {
      XPLATPendingException.Set(new System.ApplicationException(message, XPLATPendingException.Retrieve()));
    }
    static void SetPendingArithmeticException(string message) {
      XPLATPendingException.Set(new System.ArithmeticException(message, XPLATPendingException.Retrieve()));
    }
    static void SetPendingDivideByZeroException(string message) {
      XPLATPendingException.Set(new System.DivideByZeroException(message, XPLATPendingException.Retrieve()));
    }
    static void SetPendingIndexOutOfRangeException(string message) {
      XPLATPendingException.Set(new System.IndexOutOfRangeException(message, XPLATPendingException.Retrieve()));
    }
    static void SetPendingInvalidCastException(string message) {
      XPLATPendingException.Set(new System.InvalidCastException(message, XPLATPendingException.Retrieve()));
    }
    static void SetPendingInvalidOperationException(string message) {
      XPLATPendingException.Set(new System.InvalidOperationException(message, XPLATPendingException.Retrieve()));
    }
    static void SetPendingIOException(string message) {
      XPLATPendingException.Set(new System.IO.IOException(message, XPLATPendingException.Retrieve()));
    }
    static void SetPendingNullReferenceException(string message) {
      XPLATPendingException.Set(new System.NullReferenceException(message, XPLATPendingException.Retrieve()));
    }
    static void SetPendingOutOfMemoryException(string message) {
      XPLATPendingException.Set(new System.OutOfMemoryException(message, XPLATPendingException.Retrieve()));
    }
    static void SetPendingOverflowException(string message) {
      XPLATPendingException.Set(new System.OverflowException(message, XPLATPendingException.Retrieve()));
    }
    static void SetPendingSystemException(string message) {
      XPLATPendingException.Set(new System.SystemException(message, XPLATPendingException.Retrieve()));
    }

    static void SetPendingArgumentException(string message, string paramName) {
      XPLATPendingException.Set(new System.ArgumentException(message, paramName, XPLATPendingException.Retrieve()));
    }
    static void SetPendingArgumentNullException(string message, string paramName) {
      Exception e = XPLATPendingException.Retrieve();
      if (e != null) message = message + " Inner Exception: " + e.Message;
      XPLATPendingException.Set(new System.ArgumentNullException(paramName, message));
    }
    static void SetPendingArgumentOutOfRangeException(string message, string paramName) {
      Exception e = XPLATPendingException.Retrieve();
      if (e != null) message = message + " Inner Exception: " + e.Message;
      XPLATPendingException.Set(new System.ArgumentOutOfRangeException(paramName, message));
    }

    static XPLATExceptionHelper() {
      XPLATRegisterExceptionCallbacks_CrossXColor(
                                applicationDelegate,
                                arithmeticDelegate,
                                divideByZeroDelegate,
                                indexOutOfRangeDelegate,
                                invalidCastDelegate,
                                invalidOperationDelegate,
                                ioDelegate,
                                nullReferenceDelegate,
                                outOfMemoryDelegate,
                                overflowDelegate,
                                systemDelegate);

      XPLATRegisterExceptionCallbacksArgument_CrossXColor(
                                argumentDelegate,
                                argumentNullDelegate,
                                argumentOutOfRangeDelegate);
    }
  }

  protected static XPLATExceptionHelper swigExceptionHelper = new XPLATExceptionHelper();

  public class XPLATPendingException {
    [ThreadStatic]
    private static Exception pendingException = null;
    private static int numExceptionsPending = 0;

    public static bool Pending {
      get {
        bool pending = false;
        if (numExceptionsPending > 0)
          if (pendingException != null)
            pending = true;
        return pending;
      } 
    }

    public static void Set(Exception e) {
      if (pendingException != null)
        throw new ApplicationException("FATAL: An earlier pending exception from unmanaged code was missed and thus not thrown (" + pendingException.ToString() + ")", e);
      pendingException = e;
      lock(typeof(CrossXColorPINVOKE)) {
        numExceptionsPending++;
      }
    }

    public static Exception Retrieve() {
      Exception e = null;
      if (numExceptionsPending > 0) {
        if (pendingException != null) {
          e = pendingException;
          pendingException = null;
          lock(typeof(CrossXColorPINVOKE)) {
            numExceptionsPending--;
          }
        }
      }
      return e;
    }
  }


  protected class XPLATStringHelper {

    public delegate string XPLATStringDelegate(string message);
    static XPLATStringDelegate stringDelegate = new XPLATStringDelegate(CreateString);

    [DllImport("CrossXColorWrap", EntryPoint="XPLATRegisterStringCallback_CrossXColor")]
    public static extern void XPLATRegisterStringCallback_CrossXColor(XPLATStringDelegate stringDelegate);

    static string CreateString(string cString) {
      return cString;
    }

    static XPLATStringHelper() {
      XPLATRegisterStringCallback_CrossXColor(stringDelegate);
    }
  }

  static protected XPLATStringHelper swigStringHelper = new XPLATStringHelper();


  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLNewCLProfilerRef")]
  public static extern IntPtr CLNewCLProfilerRef();

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLFreeCLProfilerRef")]
  public static extern void CLFreeCLProfilerRef(HandleRef jarg1);

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLNewICCProfileRef")]
  public static extern IntPtr CLNewICCProfileRef();

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLFreeICCProfileRef")]
  public static extern void CLFreeICCProfileRef(HandleRef jarg1);

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLNewFileSpecRef")]
  public static extern IntPtr CLNewFileSpecRef();

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLFreeFileSpecRef")]
  public static extern void CLFreeFileSpecRef(HandleRef jarg1);

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLName2FileSpec")]
  public static extern IntPtr CLName2FileSpec(HandleRef jarg1, string jarg2);

  [DllImport("CrossXColorWrap", EntryPoint = "CSharp_CLAllocVectorDataSet")]
  public static extern IntPtr CLAllocVectorDataSet();

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLInitVectorDataSet")]
  public static extern IntPtr CLInitVectorDataSet(HandleRef jarg1, int jarg2, double[] jarg3, int jarg4);

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLSetProfilerOption")]
  public static extern int CLSetProfilerOption(HandleRef jarg1, string jarg2, string jarg3);

  [DllImport("CrossXColorWrap", EntryPoint = "CSharp_CLSetOptionProfile")]
  public static extern int CLSetOptionProfile(HandleRef jarg1, string jarg2, HandleRef jarg3);

  [DllImport("CrossXColorWrap", EntryPoint = "CSharp_CLReadProfile")]
  public static extern int CLReadProfile(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CrossXColorWrap", EntryPoint = "CSharp_CLReadProfileFromMemory")]
  public static extern int CLReadProfileFromMemory(HandleRef jarg1, IntPtr jarg2, int jarg3);

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLSetProfilerData")]
  public static extern int CLSetProfilerData(HandleRef jarg1, int jarg2, HandleRef jarg3, HandleRef jarg4);

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLWriteProfile")]
  public static extern int CLWriteProfile(HandleRef jarg1, HandleRef jarg2, int jarg3);

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLWriteProfileGetSize")]
  public static extern int CLWriteProfileGetSize(HandleRef jarg1, ref int jarg2);

  [DllImport("CrossXColorWrap", EntryPoint = "CSharp_CLWriteProfileToMemory")]
  public static extern int CLWriteProfileToMemory(HandleRef jarg1, IntPtr jarg2, int jarg3);

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLCalcProfile")]
  public static extern int CLCalcProfile(HandleRef jarg1, HandleRef jarg2);

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLConcatICCProfileRefs")]
  public static extern IntPtr CLConcatICCProfileRefs(int jarg1, HandleRef[] jarg2, int[] jarg3);

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLDoTrafo")]
  public static extern int CLDoTrafo(HandleRef jarg1, double [] jarg2, double [] jarg3);

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLConcatenateTrafos")]
  public static extern IntPtr CLConcatenateTrafos(HandleRef jarg1, HandleRef jarg2, HandleRef jarg3, HandleRef jarg4, HandleRef jarg5, HandleRef jarg6);

  [DllImport("CrossXColorWrap", EntryPoint="CSharp_CLFreeCLTrafoRef")]
  public static extern void CLFreeCLTrafoRef(HandleRef jarg1);
}
