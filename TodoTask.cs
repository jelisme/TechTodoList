using System;

namespace TechTodoList
{
    public class TodoTask
    {
        // Properties (Data stored inside the object)
        public int Id { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }

        // Constructor (Runs automatically when a new task is created)
        public TodoTask(int id, string description)
        {
            Id = id;
            Description = description;
            IsCompleted = false; // Tasks start incomplete by default
        }
    }
}
