using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiCFT.Models
{
    public enum Status
    {
        New,
        InWork,
        Closed
    }
    
    public class Task
    {
        public int Id { get; set; }
        [Range(1, Int32.MaxValue)]
        public int ProjectId { get; set; }
       
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public Status Status { get; set; }
        
    }
}