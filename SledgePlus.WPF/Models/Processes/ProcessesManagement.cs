using System.Diagnostics;
using System.Management;

namespace SledgePlus.WPF.Models.Processes;

public static class ProcessesManagement
{
    public static void KillProcessAndChildren(int pid)
    {
        using var searcher = new ManagementObjectSearcher
            ("Select * From Win32_Process Where ParentProcessID=" + pid);

        var moc = searcher.Get();
        foreach (var o in moc)
        {
            var mo = (ManagementObject)o;
            KillProcessAndChildren(Convert.ToInt32(mo["ProcessID"]));
        }
        try
        {
            var proc = Process.GetProcessById(pid);
            proc.Kill();
        }
        catch (Exception)
        {
            // Process already exited.
        }
    }
}