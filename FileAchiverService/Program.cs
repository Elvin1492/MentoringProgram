using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace FileAchiverService
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		static void Main()
		{
			string currentDir = Path.GetDirectoryName(Path.GetFullPath(Process.GetCurrentProcess().MainModule.FileName));

			string inQueue = @".\private$\todoQueue";
			string outQueue = @".\private$\doneQueue"; ;

			//Debugger.Launch();
			// InstallUtil.exe "D:\mentoring\window servises\Samples\FileProcessorService\bin\Debug\FileProcessorService.exe"

			var fileProcessor = new FileAchiver(inQueue, outQueue, 1, 10);

			ServiceBase.Run(fileProcessor);
		}
	}
}
