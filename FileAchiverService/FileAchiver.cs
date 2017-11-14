using System;
using System.Messaging;
using System.ServiceProcess;
using System.Threading;
using System.Threading.Tasks;

namespace FileAchiverService
{
	partial class FileAchiver : ServiceBase
	{
		internal const string SericeName = "FileAchiverService";
		private Thread workThread;
		private ManualResetEvent stopWorkEvent = new ManualResetEvent(false);
		private string todoMessageQueue;
		private string doneMessageQueue;
		private int maxTaskCount;
		private int maxImagesCountinOneThread;

		public FileAchiver(string inMessageQueue, string outMessageQueue, int taskCount, int imagesPerThread)
		{
			this.CanStop = true;
			this.ServiceName = FileAchiver.SericeName;
			this.AutoLog = false;

			maxTaskCount = taskCount;
			maxImagesCountinOneThread = imagesPerThread;
			todoMessageQueue = inMessageQueue;
			doneMessageQueue = outMessageQueue;
			workThread = new Thread(WorkProcedure);

		}

		protected override void OnStart(string[] args)
		{
			EventLog.WriteEntry("FileAchiverService starting");
			stopWorkEvent.Reset();

			workThread.Start();
			EventLog.WriteEntry("FileAchiverService started");
		}

		private MessageQueue InitializeMQ(string MQName, XmlMessageFormatter formatter)
		{
			MessageQueue MQ;

			if (MessageQueue.Exists(MQName))
				MQ = new MessageQueue(MQName);
			else
				MQ = MessageQueue.Create(MQName);

			MQ.Formatter = formatter;

			return MQ;
		}

		protected override void OnStop()
		{
			stopWorkEvent.Set();
			MessageQueue.Delete(todoMessageQueue);
			MessageQueue.Delete(doneMessageQueue);
			workThread.Join();
		}

		protected void WorkProcedure(object obj)
		{
			EventLog.WriteEntry("WorkProcedure starting");
			MessageQueue todoQueue = InitializeMQ(todoMessageQueue, new XmlMessageFormatter(
				new Type[] { typeof(ImagesAndSettingsForResizing), typeof(ResizeSettingsModel) }));
			MessageQueue doneQueue = InitializeMQ(doneMessageQueue, new XmlMessageFormatter(
				new Type[] { typeof(ResizedImageInfo) }));

			EventLog.WriteEntry("MessageQueue started");

			Task task = null;
			MultiThreadResizerWorker worker = null;
			do
			{
				if (task == null)
				{
					using (todoQueue)
					{
						Message message = null;
						try
						{
							EventLog.WriteEntry("Receiving");
							message = todoQueue.Receive();
							EventLog.WriteEntry("Received");
						}
						catch (Exception ex)
						{
							EventLog.WriteEntry("Receive ex" + ex.Message + ex.StackTrace);
						}

						ImagesAndSettingsForResizing todo = message.Body as ImagesAndSettingsForResizing;

						EventLog.WriteEntry("check todo");
						if (todo != null)
						{
							EventLog.WriteEntry("worker starting");
							try
							{
								worker = new MultiThreadResizerWorker(maxTaskCount, maxImagesCountinOneThread);
								todo.Settings.ForEach(set =>
								{
									worker.ListOfResizeSettings.Add(new CustomResizeSettings(set.Name, set.Width, set.Height));
								});
								worker.AddListOfImages(todo.ImagesForWorker);
								task = worker.StartResizingTask(600);
							}
							catch (Exception ex)
							{
								EventLog.WriteEntry("worker ex" + ex.Message + ex.StackTrace);
							}
							EventLog.WriteEntry("worker started");
						}
					}
				}
				else
				{
					EventLog.WriteEntry("Task is not null");
					if (task.IsCompleted)
					{
						EventLog.WriteEntry("Task is completed");
						using (doneQueue)
						{
							try
							{
								EventLog.WriteEntry("Sending");
								doneQueue.Send(worker.ResizedImagesList);
								EventLog.WriteEntry("Sent");
								task = null;
							}
							catch (Exception ex)
							{
								EventLog.WriteEntry("Sent ex" + ex.Message + ex.StackTrace);
							}
						}
					}
					else
					{
						EventLog.WriteEntry("Task is not completed");
					}
				}
			}
			while (WaitHandle.WaitAny(new WaitHandle[] { stopWorkEvent },
				       TimeSpan.FromMilliseconds(10000)) != 0);
		}
	}

	partial class FileAchiver
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			this.ServiceName = "FileAchiverService";
		}

		#endregion
	}
}