using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

internal struct LASTINPUTINFO
{
    public uint cbSize;
    public uint dwTime;
}
public class Win32_idle
{
    [DllImport("User32.dll")]
    public static extern bool LockWorkStation();

    [DllImport("User32.dll")]
    private static extern bool GetLastInputInfo(ref LASTINPUTINFO plii);

    [DllImport("Kernel32.dll")]
    private static extern uint GetLastError();

    public static uint GetIdleTime()
    {
        LASTINPUTINFO lastInPut = new LASTINPUTINFO();
        lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
        GetLastInputInfo(ref lastInPut);

        return ((uint)Environment.TickCount - lastInPut.dwTime);
    }

    public static long GetTickCount()
    {
        return Environment.TickCount;
    }

    public static long GetLastInputTime()
    {
        LASTINPUTINFO lastInPut = new LASTINPUTINFO();
        lastInPut.cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf(lastInPut);
        if (!GetLastInputInfo(ref lastInPut))
        {
            throw new Exception(GetLastError().ToString());
        }

        return lastInPut.dwTime;
    }


}
