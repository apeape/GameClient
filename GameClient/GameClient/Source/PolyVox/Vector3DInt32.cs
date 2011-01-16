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

public class Vector3DInt32 : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Vector3DInt32(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(Vector3DInt32 obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~Vector3DInt32() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          PolyVoxCorePINVOKE.delete_Vector3DInt32(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public Vector3DInt32(int x, int y) : this(PolyVoxCorePINVOKE.new_Vector3DInt32__SWIG_0(x, y), true) {
  }

  public Vector3DInt32(int x, int y, int z) : this(PolyVoxCorePINVOKE.new_Vector3DInt32__SWIG_1(x, y, z), true) {
  }

  public Vector3DInt32(int x, int y, int z, int w) : this(PolyVoxCorePINVOKE.new_Vector3DInt32__SWIG_2(x, y, z, w), true) {
  }

  public Vector3DInt32() : this(PolyVoxCorePINVOKE.new_Vector3DInt32__SWIG_3(), true) {
  }

  public Vector3DInt32(Vector3DInt32 vector) : this(PolyVoxCorePINVOKE.new_Vector3DInt32__SWIG_4(Vector3DInt32.getCPtr(vector)), true) {
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public Vector3DInt32 Assignment(Vector3DInt32 rhs) {
    Vector3DInt32 ret = new Vector3DInt32(PolyVoxCorePINVOKE.Vector3DInt32_Assignment(swigCPtr, Vector3DInt32.getCPtr(rhs)), false);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool equals(Vector3DInt32 rhs) {
    bool ret = PolyVoxCorePINVOKE.Vector3DInt32_equals(swigCPtr, Vector3DInt32.getCPtr(rhs));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool lessThan(Vector3DInt32 rhs) {
    bool ret = PolyVoxCorePINVOKE.Vector3DInt32_lessThan(swigCPtr, Vector3DInt32.getCPtr(rhs));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3DInt32 additionAssignment(Vector3DInt32 rhs) {
    Vector3DInt32 ret = new Vector3DInt32(PolyVoxCorePINVOKE.Vector3DInt32_additionAssignment(swigCPtr, Vector3DInt32.getCPtr(rhs)), false);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3DInt32 subtractionAssignment(Vector3DInt32 rhs) {
    Vector3DInt32 ret = new Vector3DInt32(PolyVoxCorePINVOKE.Vector3DInt32_subtractionAssignment(swigCPtr, Vector3DInt32.getCPtr(rhs)), false);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3DInt32 multiplicationAssignment(int rhs) {
    Vector3DInt32 ret = new Vector3DInt32(PolyVoxCorePINVOKE.Vector3DInt32_multiplicationAssignment(swigCPtr, rhs), false);
    return ret;
  }

  public Vector3DInt32 divisionAssignment(int rhs) {
    Vector3DInt32 ret = new Vector3DInt32(PolyVoxCorePINVOKE.Vector3DInt32_divisionAssignment(swigCPtr, rhs), false);
    return ret;
  }

  public int getElement(uint index) {
    int ret = PolyVoxCorePINVOKE.Vector3DInt32_getElement(swigCPtr, index);
    return ret;
  }

  public int getX() {
    int ret = PolyVoxCorePINVOKE.Vector3DInt32_getX(swigCPtr);
    return ret;
  }

  public int getY() {
    int ret = PolyVoxCorePINVOKE.Vector3DInt32_getY(swigCPtr);
    return ret;
  }

  public int getZ() {
    int ret = PolyVoxCorePINVOKE.Vector3DInt32_getZ(swigCPtr);
    return ret;
  }

  public int getW() {
    int ret = PolyVoxCorePINVOKE.Vector3DInt32_getW(swigCPtr);
    return ret;
  }

  public void setElement(uint index, int tValue) {
    PolyVoxCorePINVOKE.Vector3DInt32_setElement(swigCPtr, index, tValue);
  }

  public void setElements(int x, int y) {
    PolyVoxCorePINVOKE.Vector3DInt32_setElements__SWIG_0(swigCPtr, x, y);
  }

  public void setElements(int x, int y, int z) {
    PolyVoxCorePINVOKE.Vector3DInt32_setElements__SWIG_1(swigCPtr, x, y, z);
  }

  public void setElements(int x, int y, int z, int w) {
    PolyVoxCorePINVOKE.Vector3DInt32_setElements__SWIG_2(swigCPtr, x, y, z, w);
  }

  public void setX(int tX) {
    PolyVoxCorePINVOKE.Vector3DInt32_setX(swigCPtr, tX);
  }

  public void setY(int tY) {
    PolyVoxCorePINVOKE.Vector3DInt32_setY(swigCPtr, tY);
  }

  public void setZ(int tZ) {
    PolyVoxCorePINVOKE.Vector3DInt32_setZ(swigCPtr, tZ);
  }

  public void setW(int tW) {
    PolyVoxCorePINVOKE.Vector3DInt32_setW(swigCPtr, tW);
  }

  public double length() {
    double ret = PolyVoxCorePINVOKE.Vector3DInt32_length(swigCPtr);
    return ret;
  }

  public double lengthSquared() {
    double ret = PolyVoxCorePINVOKE.Vector3DInt32_lengthSquared(swigCPtr);
    return ret;
  }

  public double angleTo(Vector3DInt32 vector) {
    double ret = PolyVoxCorePINVOKE.Vector3DInt32_angleTo(swigCPtr, Vector3DInt32.getCPtr(vector));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3DInt32 cross(Vector3DInt32 vector) {
    Vector3DInt32 ret = new Vector3DInt32(PolyVoxCorePINVOKE.Vector3DInt32_cross(swigCPtr, Vector3DInt32.getCPtr(vector)), true);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public int dot(Vector3DInt32 rhs) {
    int ret = PolyVoxCorePINVOKE.Vector3DInt32_dot(swigCPtr, Vector3DInt32.getCPtr(rhs));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void normalise() {
    PolyVoxCorePINVOKE.Vector3DInt32_normalise(swigCPtr);
  }

}

}