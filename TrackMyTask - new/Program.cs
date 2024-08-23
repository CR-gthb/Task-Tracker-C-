using System;
using System.IO;
using System.Threading;

///Created By C.R

namespace TrackMyTask___new
{
    internal class Program
    {
        private const string FilePath = "L:\\TrackMyTask.csv";

        static void Main(string[] args)
        {
            Program program = new Program();

            bool continueRunning = true; // This variable controls the loop

            while (continueRunning) // Keep showing the menu until the user decides to exit
            {
                continueRunning = program.MenuSelect(); // Show menu and get user's choice
            }
        }

        public bool MenuSelect()
        {
            Console.WriteLine("Welcome To TrackMyTask");
            Console.WriteLine("[1] - View Task");
            Console.WriteLine("[2] - Create New Task");
            Console.WriteLine("[3] - Remove Tasks");
            Console.WriteLine("[0] - Exit");
            return MenuValidation();
        }

        public bool MenuValidation()
        {
            string userMenuInput = Console.ReadLine();

            switch (userMenuInput)
            {
                case "1":
                    return viewTasks();

                case "2":
                    return MakeTasks();

                case "3":
                    return RemoveTasks();

                case "0":
                    return false;

                default:
                    Console.WriteLine("[ERROR] Invalid input, please try again");
                    Thread.Sleep(2000);
                    return MenuSelect();
            }
        }

        public bool viewTasks()
        {
            try
            {
                string content = File.ReadAllText(FilePath);
                Console.WriteLine(content);
            }
            catch (IOException ex)
            {
                Console.WriteLine("[ERROR] Failed to read the file: " + ex.Message);
            }

            Console.ReadKey();
            return MenuSelect();
        }

        public bool MakeTasks()
        {
            var filePath = FilePath;
            try
            {
                using (var file = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.None))
                using (var writer = new StreamWriter(file))
                {
                    Console.WriteLine("\nWhat type of task do you want to do?\n");
                    string taskType = TaskInputValidation();

                    Console.WriteLine("\nWhat is the task you want to complete?");
                    string taskActivity = TaskActivityValidation();

                    Console.WriteLine("\nWhat is the date you want this to be completed by?");
                    string dateDue = DateTaskValidation();

                    writer.WriteLine($"{taskType},{taskActivity},{dateDue}");

                    Console.WriteLine("\nTask created successfully!");
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("[ERROR] Failed to write to the file: " + ex.Message);
            }

            return MenuSelect();
        }

        public string TaskInputValidation()
        {
            string taskType = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(taskType) || taskType.Length < 3 || taskType.Length > 16)
            {
                Console.WriteLine("[ERROR] Invalid input. Please enter an input within 3-16 characters");
                taskType = Console.ReadLine();
            }

            return taskType;
        }

        public string TaskActivityValidation()
        {
            string taskActivity = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(taskActivity) || taskActivity.Length < 3 || taskActivity.Length > 64)
            {
                Console.WriteLine("[ERROR] Invalid input. Please enter an input within 3-64 characters");
                taskActivity = Console.ReadLine();
            }

            return taskActivity;
        }

        public string DateTaskValidation()
        {
            string dateDue = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(dateDue) || dateDue.Length < 3 || dateDue.Length > 32)
            {
                Console.WriteLine("[ERROR] Invalid input. Please enter an input within 3-32 characters");
                dateDue = Console.ReadLine();
            }

            return dateDue;
        }

        public bool RemoveTasks()
        {
            Console.WriteLine("Are you sure you want to remove all tasks? (yes/no)");
            string removeInput = Console.ReadLine().ToLower();

            if (removeInput == "yes")
            {
                try
                {
                    File.Delete(FilePath);
                    Console.WriteLine("Tasks removed successfully!");
                }
                catch (IOException ex)
                {
                    Console.WriteLine("[ERROR] Failed to remove tasks: " + ex.Message);
                }
            }
            else if (removeInput == "no")
            {
                Console.WriteLine("Tasks removal canceled.");
            }
            else
            {
                Console.WriteLine("[ERROR] Invalid input. Task removal canceled.");
            }

            return MenuSelect();
        }
    }
}