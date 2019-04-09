using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiCFT.Models
{
    public class Project
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        
        public DateTime CreationTime { get; set; }
        public DateTime LastModificationTime { get; set; }
    }
}