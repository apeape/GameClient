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

public class Vector3DInt16 : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal Vector3DInt16(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(Vector3DInt16 obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~Vector3DInt16() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          PolyVoxCorePINVOKE.delete_Vector3DInt16(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public Vector3DInt16(short x, short y) : this(PolyVoxCorePINVOKE.new_Vector3DInt16__SWIG_0(x, y), true) {
  }

  public Vector3DInt16(short x, short y, short z) : this(PolyVoxCorePINVOKE.new_Vector3DInt16__SWIG_1(x, y, z), true) {
  }

  public Vector3DInt16(short x, short y, short z, short w) : this(PolyVoxCorePINVOKE.new_Vector3DInt16__SWIG_2(x, y, z, w), true) {
  }

  public Vector3DInt16() : this(PolyVoxCorePINVOKE.new_Vector3DInt16__SWIG_3(), true) {
  }

  public Vector3DInt16(Vector3DInt16 vector) : this(PolyVoxCorePINVOKE.new_Vector3DInt16__SWIG_4(Vector3DInt16.getCPtr(vector)), true) {
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public Vector3DInt16 Assignment(Vector3DInt16 rhs) {
    Vector3DInt16 ret = new Vector3DInt16(PolyVoxCorePINVOKE.Vector3DInt16_Assignment(swigCPtr, Vector3DInt16.getCPtr(rhs)), false);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool equals(Vector3DInt16 rhs) {
    bool ret = PolyVoxCorePINVOKE.Vector3DInt16_equals(swigCPtr, Vector3DInt16.getCPtr(rhs));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool lessThan(Vector3DInt16 rhs) {
    bool ret = PolyVoxCorePINVOKE.Vector3DInt16_lessThan(swigCPtr, Vector3DInt16.getCPtr(rhs));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3DInt16 additionAssignment(Vector3DInt16 rhs) {
    Vector3DInt16 ret = new Vector3DInt16(PolyVoxCorePINVOKE.Vector3DInt16_additionAssignment(swigCPtr, Vector3DInt16.getCPtr(rhs)), false);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3DInt16 subtractionAssignment(Vector3DInt16 rhs) {
    Vector3DInt16 ret = new Vector3DInt16(PolyVoxCorePINVOKE.Vector3DInt16_subtractionAssignment(swigCPtr, Vector3DInt16.getCPtr(rhs)), false);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3DInt16 multiplicationAssignment(short rhs) {
    Vector3DInt16 ret = new Vector3DInt16(PolyVoxCorePINVOKE.Vector3DInt16_multiplicationAssignment(swigCPtr, rhs), false);
    return ret;
  }

  public Vector3DInt16 divisionAssignment(short rhs) {
    Vector3DInt16 ret = new Vector3DInt16(PolyVoxCorePINVOKE.Vector3DInt16_divisionAssignment(swigCPtr, rhs), false);
    return ret;
  }

  public short getElement(uint index) {
    short ret = PolyVoxCorePINVOKE.Vector3DInt16_getElement(swigCPtr, index);
    return ret;
  }

  public short getX() {
    short ret = PolyVoxCorePINVOKE.Vector3DInt16_getX(swigCPtr);
    return ret;
  }

  public short getY() {
    short ret = PolyVoxCorePINVOKE.Vector3DInt16_getY(swigCPtr);
    return ret;
  }

  public short getZ() {
    short ret = PolyVoxCorePINVOKE.Vector3DInt16_getZ(swigCPtr);
    return ret;
  }

  public short getW() {
    short ret = PolyVoxCorePINVOKE.Vector3DInt16_getW(swigCPtr);
    return ret;
  }

  public void setElement(uint index, short tValue) {
    PolyVoxCorePINVOKE.Vector3DInt16_setElement(swigCPtr, index, tValue);
  }

  public void setElements(short x, short y) {
    PolyVoxCorePINVOKE.Vector3DInt16_setElements__SWIG_0(swigCPtr, x, y);
  }

  public void setElements(short x, short y, short z) {
    PolyVoxCorePINVOKE.Vector3DInt16_setElements__SWIG_1(swigCPtr, x, y, z);
  }

  public void setElements(short x, short y, short z, short w) {
    PolyVoxCorePINVOKE.Vector3DInt16_setElements__SWIG_2(swigCPtr, x, y, z, w);
  }

  public void setX(short tX) {
    PolyVoxCorePINVOKE.Vector3DInt16_setX(swigCPtr, tX);
  }

  public void setY(short tY) {
    PolyVoxCorePINVOKE.Vector3DInt16_setY(swigCPtr, tY);
  }

  public void setZ(short tZ) {
    PolyVoxCorePINVOKE.Vector3DInt16_setZ(swigCPtr, tZ);
  }

  public void setW(short tW) {
    PolyVoxCorePINVOKE.Vector3DInt16_setW(swigCPtr, tW);
  }

  public double length() {
    double ret = PolyVoxCorePINVOKE.Vector3DInt16_length(swigCPtr);
    return ret;
  }

  public double lengthSquared() {
    double ret = PolyVoxCorePINVOKE.Vector3DInt16_lengthSquared(swigCPtr);
    return ret;
  }

  public double angleTo(Vector3DInt16 vector) {
    double ret = PolyVoxCorePINVOKE.Vector3DInt16_angleTo(swigCPtr, Vector3DInt16.getCPtr(vector));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public Vector3DInt16 cross(Vector3DInt16 vector) {
    Vector3DInt16 ret = new Vector3DInt16(PolyVoxCorePINVOKE.Vector3DInt16_cross(swigCPtr, Vector3DInt16.getCPtr(vector)), true);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public short dot(Vector3DInt16 rhs) {
    short ret = PolyVoxCorePINVOKE.Vector3DInt16_dot(swigCPtr, Vector3DInt16.getCPtr(rhs));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void normalise() {
    PolyVoxCorePINVOKE.Vector3DInt16_normalise(swigCPtr);
  }

}

}