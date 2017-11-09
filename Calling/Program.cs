using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnmanagedTask;

namespace Calling
{
	class Program
	{
		static void Main(string[] args)
		{
			var liba = (IPowrProfLibaCall)new PowrProfLibaCall();

			Console.WriteLine(liba.LastSleepTime());

			Console.WriteLine(liba.LastWakeTime());
			Console.WriteLine(liba.SystemReserveHiberFile(true));
			Console.WriteLine(liba.SystemReserveHiberFile(false));

			Console.WriteLine(liba.SystemBatteryState());

			Console.WriteLine(liba.SystemPowerInfo());

			Console.ReadKey();
		}
	}
}
