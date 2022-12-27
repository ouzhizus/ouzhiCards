using System.Drawing;
using Pastel;

Random rnd = new Random();
string userName = Environment.UserName;
string dirPath = "C:\\Users\\"+userName+"\\OneDrive\\Документы\\cardsLibrary";
if (!Directory.Exists(dirPath))
{
    Directory.CreateDirectory(dirPath);
    Console.WriteLine("----------------------");
    Console.WriteLine("Folder 'cards library was created at: \n".Pastel(ConsoleColor.White) +
                      $"'{dirPath.Replace("\\cardsLibrary","")}'".Pastel(ConsoleColor.DarkCyan));
    Console.WriteLine("Folder is empty.\n You should go to created folder and create new '.txt' file(s)");
    Console.WriteLine("Tip: import words in format \n Word - Word");
    Thread.Sleep(1000);
    Console.WriteLine("Press any key to exit");
    Console.ReadKey();
}
else
{
    string[] allDirFiles = Directory.GetFiles(dirPath);
    if (allDirFiles.Length == 0)
    {
        Console.WriteLine("----------------------");
        Console.WriteLine("Folder is empty.\nYou should go to created folder and create new '.txt' file(s)");
        Console.WriteLine("Tip: import words in format Word - Word".Pastel(ConsoleColor.Cyan));
        Console.WriteLine("----------------------");
        Thread.Sleep(1000);
        Console.WriteLine("Press any key to exit".Pastel(ConsoleColor.Red));
        Console.ReadKey();
    }
    else
    {
        int counter = 0;
        foreach (var file in allDirFiles)
        {
            Console.WriteLine($"{counter + 1}) ".Pastel(ConsoleColor.Red) +
                              file.Replace("C:\\Users\\" + userName + "\\OneDrive\\Документы\\cardsLibrary\\", ""));
            counter++;
        }

        Console.WriteLine("----------------------");
        Console.Write("Choose a file: ");
        string userChoice = Console.ReadLine()!;
        int userIntChoice = 0;
        while (!Int32.TryParse(userChoice, out userIntChoice))
        {
            Console.Write("Please,input a number: ");
            userChoice = Console.ReadLine()!;
        }

        string? folderFile = allDirFiles[userIntChoice - 1]
            .Replace("C:\\Users\\" + userName + "\\OneDrive\\Документы\\cardsLibrary\\", "");
        string? userFile = folderFile;
        string? path = "C:\\Users\\" + userName + "\\OneDrive\\Документы\\cardsLibrary\\" + userFile;

        string[] lines = File.ReadAllLines(path);
        if (lines.Length == 0)
        {
            Console.WriteLine("File is empty.");
            Console.WriteLine("Tip: import words in format 'Word - Word'".Pastel(ConsoleColor.Green));
            Console.WriteLine("----------------------");
            Thread.Sleep(1000);
            Console.WriteLine("Press any key to exit".Pastel(ConsoleColor.Red));
            Console.ReadKey();
        }
        else if (lines.Length >= 1)
        {
            List<string> origWords = new List<string>();
            List<string> translatedWords = new List<string>();
            foreach (string line in lines)
            {
                string[] words = line.Split(" - ");
                origWords.Add(words[0]);
                translatedWords.Add(words[1]);
            }

            Console.Clear();
            Console.WriteLine("To stop enter 'stop' or 'exit'");
            while (true)
            {
                Console.WriteLine("----------------------");
                int randomChooser = rnd.Next(0, origWords.Count);
                Console.WriteLine($"{origWords[randomChooser]}".Pastel(ConsoleColor.Green));
                Console.Write("Your guess: ".Pastel(Color.Crimson));
                string userGuess = Console.ReadLine()!.ToLower();
                if (userGuess.Equals("stop") || userGuess.Equals("exit"))
                {
                    Console.WriteLine("----------------------");
                    break;
                }

                Console.WriteLine("Translation: " + translatedWords[randomChooser].Pastel(ConsoleColor.Green));
            }
        }
    }
}
    