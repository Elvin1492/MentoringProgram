using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace TaskAsync2.Models
{
    public class TaskModel
    {
        [Display(Name = "URL")]
        public string Site { get; set; }
        public Guid Id { get; set; }
        public WebClient Client { get; set; }
        public TaskModel(UrlModel model)
        {
            Site = model.Site;
            Id = model.Id;
        }
    }
}