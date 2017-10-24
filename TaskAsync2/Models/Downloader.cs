using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;

namespace TaskAsync2.Models
{
    public class Downloader
    {
        private static readonly ConcurrentDictionary<Guid, TaskModel> DownloaderManager
            = new ConcurrentDictionary<Guid, TaskModel>();

        public static void CreateNew(UrlModel model)
        {
            model.Id = Guid.NewGuid();
            var taskmodel = new TaskModel(model) { Client = new WebClient() };
            DownloaderManager.TryAdd(model.Id, taskmodel);
        }

        public static async Task<string> StartNew(Guid id)
        {
            TaskModel taskmodel;
            DownloaderManager.TryRemove(id, out taskmodel);
            var client = taskmodel.Client;
            var uri = new Uri(taskmodel.Site);
            return await client.DownloadStringTaskAsync(uri);
        }

        public static UrlModel Cancel(UrlModel model)
        {
            TaskModel taskmodel;
            DownloaderManager.TryGetValue(model.Id, out taskmodel);
            taskmodel?.Client?.CancelAsync();
            model.Content = "Canceled";

            return model;
        }

    }
}