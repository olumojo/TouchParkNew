// Decompiled with JetBrains decompiler
// Type: TouchPark.ExceptionHandling.TouchParkException
// Assembly: TouchPark, Version=1.4.34.1, Culture=neutral, PublicKeyToken=null
// MVID: 2CCDAB4F-96EA-47FE-816A-ABA0F97C0D78
// Assembly location: C:\Users\olum.ojo\Desktop\TouchPark.exe

using System;
using System.Runtime.Serialization;

namespace TouchPark.ExceptionHandling
{
  [Serializable]
  public class TouchParkException : Exception
  {
    public TouchParkException()
    {
    }

    public TouchParkException(string message)
      : base(message)
    {
    }

    public TouchParkException(string message, Exception inner)
      : base(message, inner)
    {
    }

    protected TouchParkException(SerializationInfo info, StreamingContext context)
      : base(info, context)
    {
    }
  }
}
