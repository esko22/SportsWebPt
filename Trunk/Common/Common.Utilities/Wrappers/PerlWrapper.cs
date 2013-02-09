using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace SportsWebPt.Common.Utilities
{
    public class PerlWrapper
    {

        #region Fields

        private readonly string exePath = @"C:\Perl64\bin\Perl.exe";  // TODO: pull from CFG
        private readonly string scriptPath;

        private static Process perl;

        private static List<string> stdoutBuffer;

        #endregion

        #region Constructors

        public PerlWrapper(string _scriptPath)
        {
            scriptPath = _scriptPath;
        }

        #endregion

        #region Properties

        
        #endregion

        #region Methods

        protected void Init(string[] args, Action<IEnumerable<string>> finishedCallback = null)
        {
            perl = new Process();

            ProcessStartInfo processStartInfo = new ProcessStartInfo(exePath);
            processStartInfo.Arguments = String.Format("{0} {1}", scriptPath, args == null ? String.Empty : String.Join(" ", args.ToArray()));
            processStartInfo.UseShellExecute = false;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.CreateNoWindow = true;

            if (finishedCallback != null)
            {
                perl.EnableRaisingEvents = true;
                perl.Exited += (sender, e) => finishedCallback(stdoutBuffer);
            }

            perl.StartInfo = processStartInfo;
            perl.OutputDataReceived += (sender, e) => stdoutBuffer.Add(e.Data); ;

            stdoutBuffer = new List<string>();
        }

        protected void StartExecution(string[] args = null, Action<IEnumerable<string>> finishedCallback = null)
        {
            try
            {
                Init(args, finishedCallback);

                perl.Start();
                perl.BeginOutputReadLine();
            }
            catch (Exception e)
            {
                // TODO: something
            }
        }

        protected void CloseStdin() { perl.StandardInput.Close(); }

        protected void WriteToStdin(string input)
        {
            perl.StandardInput.WriteLine(input);
            perl.StandardInput.Flush();
        }

        #endregion
    }
}
