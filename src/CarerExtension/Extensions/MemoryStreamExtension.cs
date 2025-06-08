using System;

namespace CarerExtension.Extensions;

public static class MemoryStreamExtension
{
    public static void Clear(this MemoryStream stream)
    {
        stream.SetLength(0);
        stream.Position = 0;
    }
}
