using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace autoShutdown
{
    class Shutdown
    {
        private Process process;
        // 自动关机超时时间
        private readonly long timeout = 600; 

        public Shutdown()
        {
            process = new Process();
            process.StartInfo.FileName = "cmd.exe";//start the command line
            process.StartInfo.UseShellExecute = false;//do not use shell mode instead by programme mode
            process.StartInfo.RedirectStandardError = true;//re-direct IO
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.CreateNoWindow = true;//do not creat a form
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
        }

        public void ExecuteShutdown()
        {
            process.StandardInput.WriteLine($"shutdown /s /t {timeout}");
            process.StandardInput.AutoFlush = true;
        }

        public void CancelShutdown()
        {
            process.StandardInput.WriteLine("shutdown /a");
            process.StandardInput.AutoFlush = true;
        }
    }
}
