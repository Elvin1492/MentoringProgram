using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace FileAchiverService
{
	[RunInstaller(true)]
	public class FileAchiverServiceInstaller : Installer
	{
		public FileAchiverServiceInstaller()
		{
			var serviceInstaller = new ServiceInstaller();
			serviceInstaller.StartType = ServiceStartMode.Manual;
			serviceInstaller.ServiceName = FileAchiver.SericeName;
			serviceInstaller.DisplayName = FileAchiver.SericeName;

			Installers.Add(serviceInstaller);

			var processInstaller = new ServiceProcessInstaller();
			processInstaller.Account = ServiceAccount.User;
			processInstaller.Username = @".\Elvin";
			processInstaller.Password = "12345";

			Installers.Add(processInstaller);
		}
	}
}