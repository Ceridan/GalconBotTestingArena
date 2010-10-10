using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace GalconBotTestingArena
{
    public class ProcessRunner
    {
        public ProcessRunner(string fileName, string arguments)
        {
            this.fileName = fileName;
            this.arguments = arguments;
            outputString = string.Empty;
            errorString = string.Empty;
        }

        //Process worker
        public void ProcessWorker()
        {
            Process process = new Process();
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.OutputDataReceived += new DataReceivedEventHandler(OutputEventHandler);
            process.ErrorDataReceived += new DataReceivedEventHandler(ErrorEventHandler);
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            //string stdout = process.StandardOutput.ReadToEnd();
            //string stderr = process.StandardError.ReadToEnd();
            process.WaitForExit();
        }

        //Output Event Handler
        private void OutputEventHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            if (!string.IsNullOrEmpty(outLine.Data))
            {
                outputString += outLine.Data + "\n";
            }
        }

        //Error Event Handler
        private void ErrorEventHandler(object sendingProcess, DataReceivedEventArgs errorLine)
        {
            if (!string.IsNullOrEmpty(errorLine.Data))
            {
                errorString += errorLine.Data + "\n";
            }
        }

        public string OutputString()
        {
            return outputString;
        }

        public string ErrorString()
        {
            return errorString;
        }

        public string FileName()
        {
            return fileName;
        }

        public string Arguments()
        {
            return arguments;
        }

        private string outputString;
        private string errorString;
        private string fileName;
        private string arguments;

    }
}
