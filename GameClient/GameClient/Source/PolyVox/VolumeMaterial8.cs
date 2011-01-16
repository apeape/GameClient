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

public class VolumeMaterial8 : IDisposable {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal VolumeMaterial8(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(VolumeMaterial8 obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~VolumeMaterial8() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          PolyVoxCorePINVOKE.delete_VolumeMaterial8(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public VolumeMaterial8(ushort uWidth, ushort uHeight, ushort uDepth, ushort uBlockSideLength) : this(PolyVoxCorePINVOKE.new_VolumeMaterial8__SWIG_0(uWidth, uHeight, uDepth, uBlockSideLength), true) {
  }

  public VolumeMaterial8(ushort uWidth, ushort uHeight, ushort uDepth) : this(PolyVoxCorePINVOKE.new_VolumeMaterial8__SWIG_1(uWidth, uHeight, uDepth), true) {
  }

  public Material8 getBorderValue() {
    Material8 ret = new Material8(PolyVoxCorePINVOKE.VolumeMaterial8_getBorderValue(swigCPtr), true);
    return ret;
  }

  public Region getEnclosingRegion() {
    Region ret = new Region(PolyVoxCorePINVOKE.VolumeMaterial8_getEnclosingRegion(swigCPtr), true);
    return ret;
  }

  public ushort getWidth() {
    ushort ret = PolyVoxCorePINVOKE.VolumeMaterial8_getWidth(swigCPtr);
    return ret;
  }

  public ushort getHeight() {
    ushort ret = PolyVoxCorePINVOKE.VolumeMaterial8_getHeight(swigCPtr);
    return ret;
  }

  public ushort getDepth() {
    ushort ret = PolyVoxCorePINVOKE.VolumeMaterial8_getDepth(swigCPtr);
    return ret;
  }

  public ushort getLongestSideLength() {
    ushort ret = PolyVoxCorePINVOKE.VolumeMaterial8_getLongestSideLength(swigCPtr);
    return ret;
  }

  public ushort getShortestSideLength() {
    ushort ret = PolyVoxCorePINVOKE.VolumeMaterial8_getShortestSideLength(swigCPtr);
    return ret;
  }

  public float getDiagonalLength() {
    float ret = PolyVoxCorePINVOKE.VolumeMaterial8_getDiagonalLength(swigCPtr);
    return ret;
  }

  public Material8 getVoxelAt(ushort uXPos, ushort uYPos, ushort uZPos) {
    Material8 ret = new Material8(PolyVoxCorePINVOKE.VolumeMaterial8_getVoxelAt__SWIG_0(swigCPtr, uXPos, uYPos, uZPos), true);
    return ret;
  }

  public Material8 getVoxelAt(Vector3DUint16 v3dPos) {
    Material8 ret = new Material8(PolyVoxCorePINVOKE.VolumeMaterial8_getVoxelAt__SWIG_1(swigCPtr, Vector3DUint16.getCPtr(v3dPos)), true);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void setBorderValue(Material8 tBorder) {
    PolyVoxCorePINVOKE.VolumeMaterial8_setBorderValue(swigCPtr, Material8.getCPtr(tBorder));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool setVoxelAt(ushort uXPos, ushort uYPos, ushort uZPos, Material8 tValue) {
    bool ret = PolyVoxCorePINVOKE.VolumeMaterial8_setVoxelAt__SWIG_0(swigCPtr, uXPos, uYPos, uZPos, Material8.getCPtr(tValue));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public bool setVoxelAt(Vector3DUint16 v3dPos, Material8 tValue) {
    bool ret = PolyVoxCorePINVOKE.VolumeMaterial8_setVoxelAt__SWIG_1(swigCPtr, Vector3DUint16.getCPtr(v3dPos), Material8.getCPtr(tValue));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void resize(ushort uWidth, ushort uHeight, ushort uDepth, ushort uBlockSideLength) {
    PolyVoxCorePINVOKE.VolumeMaterial8_resize__SWIG_0(swigCPtr, uWidth, uHeight, uDepth, uBlockSideLength);
  }

  public void resize(ushort uWidth, ushort uHeight, ushort uDepth) {
    PolyVoxCorePINVOKE.VolumeMaterial8_resize__SWIG_1(swigCPtr, uWidth, uHeight, uDepth);
  }

  public void tidyUpMemory(uint uNoOfBlocksToProcess) {
    PolyVoxCorePINVOKE.VolumeMaterial8_tidyUpMemory__SWIG_0(swigCPtr, uNoOfBlocksToProcess);
  }

  public void tidyUpMemory() {
    PolyVoxCorePINVOKE.VolumeMaterial8_tidyUpMemory__SWIG_1(swigCPtr);
  }

}

}