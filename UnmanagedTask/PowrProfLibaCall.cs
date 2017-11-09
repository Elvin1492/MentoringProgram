using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UnmanagedTask
{
	public struct SystemBatteryState
	{
		public int AcOnLine;
		public int BatteryPresent;
		public int Charging;
		public int Discharging;
		public int Spare1;
		public uint MaxCapacity;
		public uint RemainingCapacity;
		public uint Rate;
		public uint EstimatedTime;
		public uint DefaultAlert1;
		public uint DefaultAlert2;
	}
	public struct SystemPowerInformation
	{
		public uint MaxIdlenessAllowed;
		public uint Idleness;
		public uint TimeRemaining;
		public byte CoolingMode;
	}

	[ComVisible(true)]
	[Guid("8E2C74B2-8B52-4C12-8FCF-23F86DE02EE4")]
	[ClassInterface(ClassInterfaceType.None)]
	public class PowrProfLibaCall : IPowrProfLibaCall
	{
		/*
        NTSTATUS WINAPI CallNtPowerInformation(
  _In_  POWER_INFORMATION_LEVEL InformationLevel,
  _In_  PVOID                   lpInputBuffer,
  _In_  ULONG                   nInputBufferSize,
  _Out_ PVOID                   lpOutputBuffer,
  _In_  ULONG                   nOutputBufferSize
);
            */


		/*
        LastSleepTime
15
The lpInBuffer parameter must be NULL; otherwise, the function returns ERROR_INVALID_PARAMETER.
The lpOutputBuffer buffer receives a ULONGLONG that specifies the interrupt-time count, 
in 100-nanosecond units, at the last system sleep time.
            LastWakeTime
14
The lpInBuffer parameter must be NULL; otherwise, the function returns ERROR_INVALID_PARAMETER.
The lpOutputBuffer buffer receives a ULONGLONG that specifies the interrupt-time count, 
in 100-nanosecond units, at the last system wake time.
            */

		[DllImport("PowrProf.dll")]
		public static extern uint CallNtPowerInformation(
				int informationLevel,
				IntPtr lpInputBuffer,
				int nInputBufferSize,
				out ulong lpOutputBuffer,
				int nOutputBufferSize);

		/*
        SystemBatteryState
        5
        The lpInBuffer parameter must be NULL; otherwise, the function returns ERROR_INVALID_PARAMETER.
        The lpOutputBuffer buffer receives a SYSTEM_BATTERY_STATE structure containing information about the current system battery.
            typedef struct {
      BOOLEAN AcOnLine;
      BOOLEAN BatteryPresent;
      BOOLEAN Charging;
      BOOLEAN Discharging;
      BOOLEAN Spare1[4];
      DWORD   MaxCapacity;
      DWORD   RemainingCapacity;
      DWORD   Rate;
      DWORD   EstimatedTime;
      DWORD   DefaultAlert1;
      DWORD   DefaultAlert2;
    } SYSTEM_BATTERY_STATE, *PSYSTEM_BATTERY_STATE;    
            */

		[DllImport("PowrProf.dll")]
		public static extern uint CallNtPowerInformation(
			int informationLevel,
			IntPtr lpInputBuffer,
			int nInputBufferSize,
			out SystemBatteryState sbs,
			int nOutputBufferSize);

		/*
        SystemPowerInformation
12
The lpInBuffer parameter must be NULL; otherwise, the function returns ERROR_INVALID_PARAMETER.
The lpOutputBuffer buffer receives a SYSTEM_POWER_INFORMATION structure.
Applications can use this level to retrieve information about the idleness of the system.
        typedef struct _SYSTEM_POWER_INFORMATION {
  ULONG MaxIdlenessAllowed;
  ULONG Idleness;
  ULONG TimeRemaining;
  UCHAR CoolingMode;
} SYSTEM_POWER_INFORMATION, *PSYSTEM_POWER_INFORMATION;
            */

		[DllImport("powrprof.dll")]
		static extern uint CallNtPowerInformation(
			int informationLevel,
			IntPtr lpInputBuffer,
			int nInputBufferSize,
			out SystemPowerInformation spi,
			int nOutputBufferSize
		);

		[DllImport("PowrProf.dll")]
		public static extern uint CallNtPowerInformation(
		int informationLevel,
		bool lpInputBuffer,
		int nInputBufferSize,
		out ulong lpOutputBuffer,
		int nOutputBufferSize);


		/// <summary>
		/// Suspends the system by shutting power down. Depending on the Hibernate parameter, the system either enters a suspend (sleep) state or hibernation (S4).
		/// </summary>
		/// <param name="hibernate">If this parameter is TRUE, the system hibernates. If the parameter is FALSE, the system is suspended.</param>
		/// <param name="forceCritical">Windows Server 2003, Windows XP, and Windows 2000:  If this parameter is TRUE, 
		/// the system suspends operation immediately; if it is FALSE, the system broadcasts a PBT_APMQUERYSUSPEND event to each 
		/// application to request permission to suspend operation.</param>
		/// <param name="disableWakeEvent">If this parameter is TRUE, the system disables all wake events. If the parameter is FALSE, any system wake events remain enabled.</param>
		/// <returns>If the function succeeds, the return value is true.</returns>
		[DllImport("Powrprof.dll", SetLastError = true)]
		static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

		ulong IPowrProfLibaCall.LastSleepTime()
		{
			ulong lastSleepTime;
			var status = CallNtPowerInformation(15, IntPtr.Zero, 0, out lastSleepTime, Marshal.SizeOf(typeof(ulong)));
			return status == 0 ? lastSleepTime : status;
		}
		ulong IPowrProfLibaCall.LastWakeTime()
		{
			ulong lastWakeTime;

			var status = CallNtPowerInformation(14, IntPtr.Zero, 0, out lastWakeTime, Marshal.SizeOf(typeof(ulong)));
			return status == 0 ? lastWakeTime : status;
		}
		string IPowrProfLibaCall.SystemBatteryState()
		{
			SystemBatteryState systemBatteryState;
			CallNtPowerInformation(
				5,
				IntPtr.Zero,
				0,
				out systemBatteryState,
				Marshal.SizeOf(typeof(SystemBatteryState))
			);

			return $@"AcOnLine {systemBatteryState.AcOnLine}, 
                BatteryPresent {systemBatteryState.BatteryPresent}, 
                Charging {systemBatteryState.Charging}, 
                Discharging {systemBatteryState.Discharging},
                Spare1 {systemBatteryState.Spare1}, 
                MaxCapacity {systemBatteryState.MaxCapacity}, 
                RemainingCapacity {systemBatteryState.RemainingCapacity}, 
                Rate {systemBatteryState.Rate}, 
                EstimatedTime {systemBatteryState.EstimatedTime}, 
                DefaultAlert1 {systemBatteryState.DefaultAlert1}, 
                DefaultAlert2 {systemBatteryState.DefaultAlert2}";
		}
		string IPowrProfLibaCall.SystemPowerInfo()
		{
			SystemPowerInformation systemPowerInfo;
			CallNtPowerInformation(
				12,
				IntPtr.Zero,
				0,
				out systemPowerInfo,
				Marshal.SizeOf(typeof(SystemPowerInformation))
			);
			return $@"MaxIdlenessAllowed {systemPowerInfo.MaxIdlenessAllowed}, 
                Idleness {systemPowerInfo.Idleness}, 
                TimeRemaining {systemPowerInfo.TimeRemaining}, 
                CoolingMode {systemPowerInfo.CoolingMode}";
		}
		ulong IPowrProfLibaCall.SystemReserveHiberFile(bool restore)
		{
			ulong systemReserveHiberFile;
			var status = CallNtPowerInformation(10, restore, Marshal.SizeOf(typeof(bool)), out systemReserveHiberFile, Marshal.SizeOf(typeof(ulong)));
			return status == 0 ? systemReserveHiberFile : status;
		}
		bool IPowrProfLibaCall.SetSuspendState()
		{
			// Sleeps the machine
			return SetSuspendState(false, false, false);
		}
	}
}
