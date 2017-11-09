using System.Runtime.InteropServices;

namespace UnmanagedTask
{
	[ComVisible(true)]
	[Guid("b66387ac-11ca-4ba9-808b-c8ed652db71c")]
	[InterfaceType(ComInterfaceType.InterfaceIsDual)]
	public interface IPowrProfLibaCall
	{
		ulong LastSleepTime();
		ulong LastWakeTime();
		bool SetSuspendState();
		string SystemBatteryState();
		string SystemPowerInfo();
		ulong SystemReserveHiberFile(bool restore);
	}
}