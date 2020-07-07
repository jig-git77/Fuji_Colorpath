
using System;
using System.Runtime.InteropServices;

public class CLVectorDataSet {
  private HandleRef swigCPtr;

  internal CLVectorDataSet(IntPtr cPtr, bool futureUse) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  public CLVectorDataSet() {
    swigCPtr = new HandleRef(null, CrossXColorPINVOKE.CLAllocVectorDataSet());
  }

  internal static HandleRef getCPtr(CLVectorDataSet obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }
}
