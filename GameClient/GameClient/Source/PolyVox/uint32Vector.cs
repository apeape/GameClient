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

public class uint32Vector : IDisposable, System.Collections.IEnumerable
#if !SWIG_DOTNET_1
    , System.Collections.Generic.IList<uint>
#endif
 {
  private HandleRef swigCPtr;
  protected bool swigCMemOwn;

  internal uint32Vector(IntPtr cPtr, bool cMemoryOwn) {
    swigCMemOwn = cMemoryOwn;
    swigCPtr = new HandleRef(this, cPtr);
  }

  internal static HandleRef getCPtr(uint32Vector obj) {
    return (obj == null) ? new HandleRef(null, IntPtr.Zero) : obj.swigCPtr;
  }

  ~uint32Vector() {
    Dispose();
  }

  public virtual void Dispose() {
    lock(this) {
      if (swigCPtr.Handle != IntPtr.Zero) {
        if (swigCMemOwn) {
          swigCMemOwn = false;
          PolyVoxCorePINVOKE.delete_uint32Vector(swigCPtr);
        }
        swigCPtr = new HandleRef(null, IntPtr.Zero);
      }
      GC.SuppressFinalize(this);
    }
  }

  public uint32Vector(System.Collections.ICollection c) : this() {
    if (c == null)
      throw new ArgumentNullException("c");
    foreach (uint element in c) {
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

  public uint this[int index]  {
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
  public void CopyTo(uint[] array)
#endif
  {
    CopyTo(0, array, 0, this.Count);
  }

#if SWIG_DOTNET_1
  public void CopyTo(System.Array array, int arrayIndex)
#else
  public void CopyTo(uint[] array, int arrayIndex)
#endif
  {
    CopyTo(0, array, arrayIndex, this.Count);
  }

#if SWIG_DOTNET_1
  public void CopyTo(int index, System.Array array, int arrayIndex, int count)
#else
  public void CopyTo(int index, uint[] array, int arrayIndex, int count)
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
  System.Collections.Generic.IEnumerator<uint> System.Collections.Generic.IEnumerable<uint>.GetEnumerator() {
    return new uint32VectorEnumerator(this);
  }
#endif

  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
    return new uint32VectorEnumerator(this);
  }

  public uint32VectorEnumerator GetEnumerator() {
    return new uint32VectorEnumerator(this);
  }

  // Type-safe enumerator
  /// Note that the IEnumerator documentation requires an InvalidOperationException to be thrown
  /// whenever the collection is modified. This has been done for changes in the size of the
  /// collection but not when one of the elements of the collection is modified as it is a bit
  /// tricky to detect unmanaged code that modifies the collection under our feet.
  public sealed class uint32VectorEnumerator : System.Collections.IEnumerator
#if !SWIG_DOTNET_1
    , System.Collections.Generic.IEnumerator<uint>
#endif
  {
    private uint32Vector collectionRef;
    private int currentIndex;
    private object currentObject;
    private int currentSize;

    public uint32VectorEnumerator(uint32Vector collection) {
      collectionRef = collection;
      currentIndex = -1;
      currentObject = null;
      currentSize = collectionRef.Count;
    }

    // Type-safe iterator Current
    public uint Current {
      get {
        if (currentIndex == -1)
          throw new InvalidOperationException("Enumeration not started.");
        if (currentIndex > currentSize - 1)
          throw new InvalidOperationException("Enumeration finished.");
        if (currentObject == null)
          throw new InvalidOperationException("Collection modified.");
        return (uint)currentObject;
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
    PolyVoxCorePINVOKE.uint32Vector_Clear(swigCPtr);
  }

  public void Add(uint x) {
    PolyVoxCorePINVOKE.uint32Vector_Add(swigCPtr, x);
  }

  private uint size() {
    uint ret = PolyVoxCorePINVOKE.uint32Vector_size(swigCPtr);
    return ret;
  }

  private uint capacity() {
    uint ret = PolyVoxCorePINVOKE.uint32Vector_capacity(swigCPtr);
    return ret;
  }

  private void reserve(uint n) {
    PolyVoxCorePINVOKE.uint32Vector_reserve(swigCPtr, n);
  }

  public uint32Vector() : this(PolyVoxCorePINVOKE.new_uint32Vector__SWIG_0(), true) {
  }

  public uint32Vector(uint32Vector other) : this(PolyVoxCorePINVOKE.new_uint32Vector__SWIG_1(uint32Vector.getCPtr(other)), true) {
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint32Vector(int capacity) : this(PolyVoxCorePINVOKE.new_uint32Vector__SWIG_2(capacity), true) {
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  private uint getitemcopy(int index) {
    uint ret = PolyVoxCorePINVOKE.uint32Vector_getitemcopy(swigCPtr, index);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private uint getitem(int index) {
    uint ret = PolyVoxCorePINVOKE.uint32Vector_getitem(swigCPtr, index);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  private void setitem(int index, uint val) {
    PolyVoxCorePINVOKE.uint32Vector_setitem(swigCPtr, index, val);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public void AddRange(uint32Vector values) {
    PolyVoxCorePINVOKE.uint32Vector_AddRange(swigCPtr, uint32Vector.getCPtr(values));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public uint32Vector GetRange(int index, int count) {
    IntPtr cPtr = PolyVoxCorePINVOKE.uint32Vector_GetRange(swigCPtr, index, count);
    uint32Vector ret = (cPtr == IntPtr.Zero) ? null : new uint32Vector(cPtr, true);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Insert(int index, uint x) {
    PolyVoxCorePINVOKE.uint32Vector_Insert(swigCPtr, index, x);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public void InsertRange(int index, uint32Vector values) {
    PolyVoxCorePINVOKE.uint32Vector_InsertRange(swigCPtr, index, uint32Vector.getCPtr(values));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveAt(int index) {
    PolyVoxCorePINVOKE.uint32Vector_RemoveAt(swigCPtr, index);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public void RemoveRange(int index, int count) {
    PolyVoxCorePINVOKE.uint32Vector_RemoveRange(swigCPtr, index, count);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public static uint32Vector Repeat(uint value, int count) {
    IntPtr cPtr = PolyVoxCorePINVOKE.uint32Vector_Repeat(value, count);
    uint32Vector ret = (cPtr == IntPtr.Zero) ? null : new uint32Vector(cPtr, true);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
    return ret;
  }

  public void Reverse() {
    PolyVoxCorePINVOKE.uint32Vector_Reverse__SWIG_0(swigCPtr);
  }

  public void Reverse(int index, int count) {
    PolyVoxCorePINVOKE.uint32Vector_Reverse__SWIG_1(swigCPtr, index, count);
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public void SetRange(int index, uint32Vector values) {
    PolyVoxCorePINVOKE.uint32Vector_SetRange(swigCPtr, index, uint32Vector.getCPtr(values));
    if (PolyVoxCorePINVOKE.SWIGPendingException.Pending) throw PolyVoxCorePINVOKE.SWIGPendingException.Retrieve();
  }

  public bool Contains(uint value) {
    bool ret = PolyVoxCorePINVOKE.uint32Vector_Contains(swigCPtr, value);
    return ret;
  }

  public int IndexOf(uint value) {
    int ret = PolyVoxCorePINVOKE.uint32Vector_IndexOf(swigCPtr, value);
    return ret;
  }

  public int LastIndexOf(uint value) {
    int ret = PolyVoxCorePINVOKE.uint32Vector_LastIndexOf(swigCPtr, value);
    return ret;
  }

  public bool Remove(uint value) {
    bool ret = PolyVoxCorePINVOKE.uint32Vector_Remove(swigCPtr, value);
    return ret;
  }

}

}