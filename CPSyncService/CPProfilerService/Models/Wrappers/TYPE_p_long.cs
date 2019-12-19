
using System;
using System.Runtime.InteropServices;

public class TYPE_p_long {
  private HandleRef swigCPtr;

  internal TYPE_p_long(IntPtr cPtr, bool futureUse) {
    swigCPtr = new HandleRef(this, cPtr);
  }

  protected TYPE_p_long() {
    swigCPtr = new HandleRef(null, IntPtr.Zero);
  }

  internal static HandleRef getCPtr(TYPE_p_long obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }
}
