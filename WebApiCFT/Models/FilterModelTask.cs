using System;
using System.Diagnostics;
using System.Linq;

namespace WebApiCFT.Models
{
    /*
     * Model for filtering Tasks
     */
    public class FilterModelTask : FilterModelBase<Task>
    {
        public string SortBy { get; set; }
        public int? ProjectId { get; set; }
        
        //all tasks with creation time < StartCreationTime will be skipped
        public DateTime? StartCreationTime { get; set; }
        
        //all tasks with last modification time < StartCreationTime will be skipped
        public DateTime? StartModificationTime { get; set; }
        
        //all tasks with creation time > FinalCreationTime will be skipped
        public DateTime? FinalCreationTime { get; set; }
        
        //all tasks with last modification time > FinalModificationTime will be skipped
        public DateTime? FinalModificationTime { get; set; }
        public int? Priority { get; set; }
        public Status? Status { get; set; }


        /*
         * checking correctness for filtering parameters 
         */
        public override bool IsValid(out string errorMessage)
        {
            //checking value for 'SortBy'
            if (!String.IsNullOrEmpty(SortBy))
            {
                if (SortBy != "creation_time" &&
                    SortBy != "creation_time_desc" &&
                    SortBy != "modification_time" &&
                    SortBy != "modification_time_desc" &&
                    SortBy != "priority" &&
                    SortBy != "priority_desc")
                {
                    errorMessage = "Wrong parameters. Incorrect value for 'SortBy'.";
                    return false;
                }
            }
            
            //checking parameters for pagination
            return base.IsValid(out errorMessage);
        }

        public override IQueryable<Task> Filter(IQueryable<Task> tasks)
        {
            if (ProjectId.HasValue)
            {
                tasks = tasks.Where(p => p.ProjectId == ProjectId);
            }

            if (StartCreationTime.HasValue)
            {
                tasks = tasks.Where(p => p.CreationTime >= StartCreationTime);
            }

            if (FinalCreationTime.HasValue)
            {
                tasks = tasks.Where(p => p.CreationTime <= FinalCreationTime);
            }

            if (StartModificationTime.HasValue)
            {
                tasks = tasks.Where(p => p.LastModificationTime >= StartModificationTime);
            }
            
            if (FinalModificationTime.HasValue)
            {
                tasks = tasks.Where(p => p.LastModificationTime <= FinalModificationTime);
            }

            if (Priority.HasValue)
            {
                tasks = tasks.Where(p => p.Priority == Priority);
            }

            if (this.Status.HasValue)
            {
                tasks = tasks.Where(p => p.Status == this.Status);
            }
            
            //sorting and pagination
            return base.Filter(this.Sort(tasks));
        }

        private IQueryable<Task> Sort(IQueryable<Task> tasks)
        {
            //if user didn't pass 'SortBy' string, we don't need to sort
            if (String.IsNullOrEmpty(SortBy))
            {
                return tasks;
            }

            IQueryable<Task> tmp;
            switch (SortBy)
            {
                case "creation_time": 
                    tmp = tasks.OrderBy(p => p.CreationTime);
                    break;
                case "creation_time_desc":
                    tmp = tasks.OrderByDescending(p => p.CreationTime);
                    break;
                case "modification_time": 
                    tmp = tasks.OrderBy(p => p.LastModificationTime);
                    break;
                case "modification_time_desc":
                    tmp = tasks.OrderByDescending(p => p.LastModificationTime);
                    break;
                case "priority":
                    tmp = tasks.OrderBy(p => p.Priority);
                    break;
                case "priority_desc":
                    tmp = tasks.OrderByDescending(p => p.Priority);
                    break;
                default:
                    tmp = tasks;
                    break;
            }

            return tmp;
        }
    }
}