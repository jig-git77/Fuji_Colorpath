
using System;
using System.Runtime.InteropServices;

public class TYPE_p_p_void {
  private HandleRef swigCPtr;

  internal TYPE_p_p_void(IntPtr cPtr, bool futureUse) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  protected TYPE_p_p_void() {
    swigCPtr = new HandleRef(null, IntPtr.Zero);
  }

  internal static HandleRef getCPtr(TYPE_p_p_void obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }
}
