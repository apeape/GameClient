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

public class PositionMaterialVector : IDisposable, System.Collections.IEnumerable
#if !SWIG_DOTNET_1
    , System.Collections.Generic.IEnumerable<PositionMaterial>
#endif
 {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal PositionMaterialVector(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(PositionMaterialVector obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~PositionMaterialVector() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          PolyVoxCorePINVOKE.delete_PositionMaterialVector(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public PositionMaterialVector(System.Collections.ICollection c) : this() {
    if (c == null)
      throw new ArgumentNullException("c");
    foreach (PositionMaterial element in c) {
      this.Add(element);
    }
  }

  public bool IsFixedSize {
    get {
      return false;
    }
  }

  public bool IsReadOnly {
    get {
      return false;
    }
  }

  public PositionMaterial this[int index]  {
    get {
      return getitem(index);
    }
    set {
      setitem(index, value);
    }
  }

  public int Capacity {
    get {
      return (int)capacity();
    }
    set {
      if (value < size())
        throw new ArgumentOutOfRangeException("Capacity");
      reserve((uint)value);
    }
  }

  public int Count {
    get {
      return (int)size();
    }
  }

  public bool IsSynchronized {
    get {
      return false;
    }
  }

#if SWIG_DOTNET_1
  public void CopyTo(System.Array array)
#else
  public void CopyTo(PositionMaterial[] array)
#endif
  {
    CopyTo(0, array, 0, this.Count);
  }

#if SWIG_DOTNET_1
  public void CopyTo(System.Array array, int arrayIndex)
#else
  public void CopyTo(PositionMaterial[] array, int arrayIndex)
#endif
  {
    CopyTo(0, array, arrayIndex, this.Count);
  }

#if SWIG_DOTNET_1
  public void CopyTo(int index, System.Array array, int arrayIndex, int count)
#else
  public void CopyTo(int index, PositionMaterial[] array, int arrayIndex, int count)
#endif
  {
    if (array == null)
      throw new ArgumentNullException("array");
    if (index < 0)
      throw new ArgumentOutOfRangeException("index", "Value is less than zero");
    if (arrayIndex < 0)
      throw new ArgumentOutOfRangeException("arrayIndex", "Value is less than zero");
    if (count < 0)
      throw new ArgumentOutOfRangeException("count", "Value is less than zero");
    if (array.Rank > 1)
      throw new ArgumentException("Multi dimensional array.", "array");
    if (index+count > this.Count || arrayIndex+count > array.Length)
      throw new ArgumentException("Number of elements to copy is too large.");
    for (int i=0; i<count; i++)
      array.SetValue(getitemcopy(index+i), arrayIndex+i);
  }

#if !SWIG_DOTNET_1
  System.Collections.Generic.IEnumerator<PositionMaterial> System.Collections.Generic.IEnumerable<PositionMaterial>.GetEnumerator() {
    return new PositionMaterialVectorEnumerator(this);
  }
#endif

  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
    return new PositionMaterialVectorEnumerator(this);
  }

  public PositionMaterialVectorEnumerator GetEnumerator() {
    return new PositionMaterialVectorEnumerator(this);
  }

  // Type-safe enumerator
  /// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
  /// whenever the collection is modified. This has been done for changes in the size of the
  /// collection but not when one of the elements of the collection is modified as it is a bit
  /// tricky to detect unmanaged code that modifies the collection under our feet.
  public sealed class PositionMaterialVectorEnumerator : System.Collections.IEnumerator
#if !SWIG_DOTNET_1
    , System.Collections.Generic.IEnumerator<PositionMaterial>
#endif
  {
    private PositionMaterialVector collectionRef;
    private int currentIndex;
    private object currentObject;
    private int currentSize;

    public PositionMaterialVectorEnumerator(PositionMaterialVector collection) {
      collectionRef = collection;
      currentIndex = -1;
      currentObject = null;
      currentSize = collectionRef.Count;
    }

    // Type-safe iterator Current
    public PositionMaterial Current {
      get {
        if (currentIndex == -1)
          throw new InvalidOperationException("Enumeration not started.");
        if (currentIndex > currentSize - 1)
          throw new InvalidOperationException("Enumeration finished.");
        if (currentObject == null)
          throw new InvalidOperationException("Collection modified.");
        return (PositionMaterial)currentObject;
      }
    }

    // Type-unsafe IEnumerator.Current
    object System.Collections.IEnumerator.Current {
      get {
        return Current;
      }
    }

    public bool MoveNext() {
      int size = collectionRef.Count;
      bool moveOkay = (currentIndex+1 < size) && (size == currentSize);
      if (moveOkay) {
        currentIndex++;
        currentObject = collectionRef[currentIndex];
      } else {
        currentObject = null;
      }
      return moveOkay;
    }

    public void Reset() {
      currentIndex = -1;
      currentObject = null;
      if (collectionRef.Count != currentSize) {
        throw new InvalidOperationException("Collection modified.");
      }
    }

#if !SWIG_DOTNET_1
    public void Dispose() {
        currentIndex = -1;
        currentObject = null;
    }
#endif
  }

  public void Clear() {
    PolyVoxCorePINVOKE.PositionMaterialVector_Clear(swigCPtr);
  }

  public void Add(PositionMaterial x) {
    PolyVoxCorePINVOKE.PositionMaterialVector_Add(swigCPtr, PositionMaterial.getCPtr(x));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  private uint size() {
    uint ret = PolyVoxCorePINVOKE.PositionMaterialVector_size(swigCPtr);
    return ret;
  }

  private uint capacity() {
    uint ret = PolyVoxCorePINVOKE.PositionMaterialVector_capacity(swigCPtr);
    return ret;
  }

  private void reserve(uint n) {
    PolyVoxCorePINVOKE.PositionMaterialVector_reserve(swigCPtr, n);
  }

  public PositionMaterialVector() : this(PolyVoxCorePINVOKE.new_PositionMaterialVector__SWIG_0(), true) {
  }

  public PositionMaterialVector(PositionMaterialVector other) : this(PolyVoxCorePINVOKE.new_PositionMaterialVector__SWIG_1(PositionMaterialVector.getCPtr(other)), true) {
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public PositionMaterialVector(int capacity) : this(PolyVoxCorePINVOKE.new_PositionMaterialVector__SWIG_2(capacity), true) {
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  private PositionMaterial getitemcopy(int index) {
    PositionMaterial ret = new PositionMaterial(PolyVoxCorePINVOKE.PositionMaterialVector_getitemcopy(swigCPtr, index), true);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private PositionMaterial getitem(int index) {
    PositionMaterial ret = new PositionMaterial(PolyVoxCorePINVOKE.PositionMaterialVector_getitem(swigCPtr, index), false);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void setitem(int index, PositionMaterial val) {
    PolyVoxCorePINVOKE.PositionMaterialVector_setitem(swigCPtr, index, PositionMaterial.getCPtr(val));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddRange(PositionMaterialVector values) {
    PolyVoxCorePINVOKE.PositionMaterialVector_AddRange(swigCPtr, PositionMaterialVector.getCPtr(values));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public PositionMaterialVector GetRange(int index, int count) {
    IntPtr cPtr = PolyVoxCorePINVOKE.PositionMaterialVector_GetRange(swigCPtr, index, count);
    PositionMaterialVector ret = (cPtr == IntPtr.Zero) ? null : new PositionMaterialVector(cPtr, true);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Insert(int index, PositionMaterial x) {
    PolyVoxCorePINVOKE.PositionMaterialVector_Insert(swigCPtr, index, PositionMaterial.getCPtr(x));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public void InsertRange(int index, PositionMaterialVector values) {
    PolyVoxCorePINVOKE.PositionMaterialVector_InsertRange(swigCPtr, index, PositionMaterialVector.getCPtr(values));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAt(int index) {
    PolyVoxCorePINVOKE.PositionMaterialVector_RemoveAt(swigCPtr, index);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveRange(int index, int count) {
    PolyVoxCorePINVOKE.PositionMaterialVector_RemoveRange(swigCPtr, index, count);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public static PositionMaterialVector Repeat(PositionMaterial value, int count) {
    IntPtr cPtr = PolyVoxCorePINVOKE.PositionMaterialVector_Repeat(PositionMaterial.getCPtr(value), count);
    PositionMaterialVector ret = (cPtr == IntPtr.Zero) ? null : new PositionMaterialVector(cPtr, true);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Reverse() {
    PolyVoxCorePINVOKE.PositionMaterialVector_Reverse__SWIG_0(swigCPtr);
  }

  public void Reverse(int index, int count) {
    PolyVoxCorePINVOKE.PositionMaterialVector_Reverse__SWIG_1(swigCPtr, index, count);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRange(int index, PositionMaterialVector values) {
    PolyVoxCorePINVOKE.PositionMaterialVector_SetRange(swigCPtr, index, PositionMaterialVector.getCPtr(values));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

}

}