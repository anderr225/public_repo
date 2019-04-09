using System;

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
        public int ProjectId { get; set; }
       
        public string Name { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public Status Status { get; set; }
        
    }
}