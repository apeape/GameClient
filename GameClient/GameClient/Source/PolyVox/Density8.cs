/* ----------------------------------------------------------------------------
 * This file was automatically generated by SWIG (http://www.swig.org).
 * Version 2.0.1
 *
 * Do not make changes to this file unless you know what you are doing--modify
 * the SWIG interface file instead.
 * ----------------------------------------------------------------------------- */

namespace PolyVoxCore {

using System;
using System.Runtime.InteropServices;

public class Density8 : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Density8(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(Density8 obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~Density8() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          PolyVoxCorePINVOKE.delete_Density8(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public Density8() : this(PolyVoxCorePINVOKE.new_Density8__SWIG_0(), true) {
  }

  public Density8(byte uDensity) : this(PolyVoxCorePINVOKE.new_Density8__SWIG_1(uDensity), true) {
  }

  public bool equals(Density8 rhs) {
    bool ret = PolyVoxCorePINVOKE.Density8_equals(swigCPtr, Density8.getCPtr(rhs));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool notEqualTo(Density8 rhs) {
    bool ret = PolyVoxCorePINVOKE.Density8_notEqualTo(swigCPtr, Density8.getCPtr(rhs));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool lessThan(Density8 rhs) {
    bool ret = PolyVoxCorePINVOKE.Density8_lessThan(swigCPtr, Density8.getCPtr(rhs));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public byte getDensity() {
    byte ret = PolyVoxCorePINVOKE.Density8_getDensity(swigCPtr);
    return ret;
  }

  public byte getMaterial() {
    byte ret = PolyVoxCorePINVOKE.Density8_getMaterial(swigCPtr);
    return ret;
  }

  public void setDensity(byte uDensity) {
    PolyVoxCorePINVOKE.Density8_setDensity(swigCPtr, uDensity);
  }

  public void setMaterial(byte uMaterial) {
    PolyVoxCorePINVOKE.Density8_setMaterial(swigCPtr, uMaterial);
  }

  public static byte getMaxDensity() {
    byte ret = PolyVoxCorePINVOKE.Density8_getMaxDensity();
    return ret;
  }

  public static byte getMinDensity() {
    byte ret = PolyVoxCorePINVOKE.Density8_getMinDensity();
    return ret;
  }

  public static byte getThreshold() {
    byte ret = PolyVoxCorePINVOKE.Density8_getThreshold();
    return ret;
  }

}

}
