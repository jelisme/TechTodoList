using System;

namespace TechTodoList
{
    class Program
    {
        static void Main(string[] sender)
        {
            TaskManager manager = new TaskManager();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n=== SYSTEM ARCHITECTURE: TASK MANAGER ===");
                Console.WriteLine("1. View Tasks");
                Console.WriteLine("2. Add Task");
                Console.WriteLine("3. Complete Task");
                Console.WriteLine("4. Delete Task");
                Console.WriteLine("5. Exit");
                Console.Write("Select an operation (1-5): ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        manager.ListTasks();
                        break;

                    case "2":
                        Console.Write("Enter task description: ");
                        string desc = Console.ReadLine();
                        manager.AddTask(desc);
                        break;

                    case "3":
                        Console.Write("Enter task ID to complete: ");
                        if (int.TryParse(Console.ReadLine(), out int completeId))
                        {
                            manager.MarkComplete(completeId);
                        }
                        else
                        {
                            Console.WriteLine("⚠️ Invalid input. Please enter a valid number ID.");
                        }
                        break;

                    case "4":
                        Console.Write("Enter task ID to delete: ");
                        if (int.TryParse(Console.ReadLine(), out int deleteId))
                        {
                            manager.DeleteTask(deleteId);
                        }
                        else
                        {
                            Console.WriteLine("⚠️ Invalid input. Please enter a valid number ID.");
                        }
                        break;

                    case "5":
                        running = false;
                        Console.WriteLine("👋 Safely closing system. Goodbye!");
                        break;

                    default:
                        Console.WriteLine("⚠️ Invalid menu selection. Choose a number between 1 and 5.");
                        break;
                }
            }
        }
    }
}
