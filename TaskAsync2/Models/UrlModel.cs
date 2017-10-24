using System;
using System.ComponentModel.DataAnnotations;

namespace TaskAsync2.Models
{
    public class UrlModel
    {
        [Display(Name = "URL")]
        public string Site { get; set; }
        public Guid Id { get; set; }
        public string Content { get; set; }
    }
}