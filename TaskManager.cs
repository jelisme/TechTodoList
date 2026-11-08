using System;
using System.Collections.Generic;
using System.IO; // 👈 NEW: This gives your code access to your Mac's hard drive

namespace TechTodoList
{
    public class TaskManager
    {
        private List<TodoTask> _tasks = new List<TodoTask>();
        private int _nextId = 1;
        
        // 📄 NEW: This defines exactly where the data will be stored on your Mac
        private readonly string _filePath = "tasks.txt";

        // Constructor: Runs automatically when the app starts up
        public TaskManager()
        {
            LoadFromFile(); // 👈 NEW: Instantly restore saved tasks when the app boots
        }

        // 1. CREATE
        public void AddTask(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                Console.WriteLine("⚠️ Error: Task description cannot be empty.");
                return;
            }

            TodoTask newTask = new TodoTask(_nextId++, description);
            _tasks.Add(newTask);
            Console.WriteLine($"✅ Task added successfully! (ID: {newTask.Id})");
            
            SaveToFile(); // 👈 NEW: Save changes instantly
        }

        // 2. READ
        public void ListTasks()
        {
            if (_tasks.Count == 0)
            {
                Console.WriteLine("\n🎉 No tasks in your list!");
                return;
            }

            Console.WriteLine("\n--- Your Tasks ---");
            foreach (var task in _tasks)
            {
                string statusIcon = task.IsCompleted ? "[X]" : "[ ]";
                Console.WriteLine($"{statusIcon} ID: {task.Id} - {task.Description}");
            }
        }

        // 3. UPDATE
        public void MarkComplete(int id)
        {
            TodoTask task = _tasks.Find(t => t.Id == id);

            if (task == null)
            {
                Console.WriteLine($"⚠️ Error: Task with ID {id} not found.");
                return;
            }

            task.IsCompleted = true;
            Console.WriteLine($"🎯 Task {id} marked as complete!");
            
            SaveToFile(); // 👈 NEW: Save changes instantly
        }

        // 4. DELETE
        public void DeleteTask(int id)
        {
            TodoTask task = _tasks.Find(t => t.Id == id);

            if (task == null)
            {
                Console.WriteLine($"⚠️ Error: Task with ID {id} not found.");
                return;
            }

            _tasks.Remove(task);
            Console.WriteLine($"🗑️ Task {id} deleted successfully.");
            
            SaveToFile(); // 👈 NEW: Save changes instantly
        }

        // ==========================================
        // 💾 NEW PERSISTENCE METHODS (The Engineering Upgrade)
        // ==========================================

        private void SaveToFile()
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (var task in _tasks)
                {
                    // Convert the task into a single line of text separated by commas
                    lines.Add($"{task.Id},{task.Description},{task.IsCompleted}");
                }
                
                // Write all lines at once to the hard drive
                File.WriteAllLines(_filePath, lines);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Critical Storage Error while saving: {ex.Message}");
            }
        }

        private void LoadFromFile()
        {
            try
            {
                // If the file doesn't exist yet (first time running), just skip loading
                if (!File.Exists(_filePath)) return;

                string[] lines = File.ReadAllLines(_filePath);
                _tasks.Clear();
                int highestId = 0;

                foreach (string line in lines)
                {
                    // Split the line back into ID, Description, and Completed fields
                    string[] parts = line.Split(',');
                    if (parts.Length == 3)
                    {
                        int id = int.Parse(parts[0]);
                        string description = parts[1];
                        bool isCompleted = bool.Parse(parts[2]);

                        TodoTask loadedTask = new TodoTask(id, description)
                        {
                            IsCompleted = isCompleted
                        };

                        _tasks.Add(loadedTask);

                        if (id > highestId) highestId = id;
                    }
                }

                // Make sure our ID generator starts where the last session left off
                _nextId = highestId + 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️ Critical Storage Error while loading history: {ex.Message}");
            }
        }
    }
}
