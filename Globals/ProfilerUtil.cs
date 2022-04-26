using System;
using Godot;
public static class ProfilerUtil {

    private static long _gcMemory;

    public static void PrintMemoryOnChange() {
        long newMem = GC.GetTotalMemory(false);
        if (newMem != _gcMemory) {
            _gcMemory = newMem;
            GD.Print("GC Managed Memory: " + _gcMemory);
        }
    }
}