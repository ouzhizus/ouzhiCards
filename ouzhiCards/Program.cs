using System.Drawing;
using System.Text;
using Pastel;

Console.OutputEncoding = Encoding.UTF8; //so we can see special symbols
Random rnd = new Random();
string userName = Environment.UserName;
string dirPath = "C:\\Users\\"+userName+"\\OneDrive\\Документы\\cardsLibrary";
if (!Directory.Exists(dirPath))
{
    Directory.CreateDirectory(dirPath); //creating a folder if it doesn't exist
    Console.WriteLine("----------------------");
    Console.WriteLine(" Folder 'cards library was created at: \n".Pastel(ConsoleColor.White) +
                      $"'{dirPath.Replace("\\cardsLibrary","")}'".Pastel(ConsoleColor.DarkCyan));
    Console.WriteLine(" Folder is empty.\n You should go to created folder and create new '.txt' file(s)");
    Console.WriteLine(" Tip: import words in format \n Word - Word");
    Thread.Sleep(1000);
    Console.WriteLine(" Press any key to exit");
    Console.ReadKey();
}
else
{
    string[] allDirFiles = Directory.GetFiles(dirPath); //getting all content of folder
    if (allDirFiles.Length == 0) //if the folder is empty ->
    {
        Console.WriteLine("----------------------");
        Console.WriteLine(" Folder is empty.\nYou should go to created folder and create new '.txt' file(s)");
        Console.WriteLine(" Tip: import words in format Word - Word".Pastel(ConsoleColor.Cyan));
        Console.WriteLine("----------------------");
        Thread.Sleep(1000);
        Console.WriteLine(" Press any key to exit".Pastel(ConsoleColor.Red));
        Console.ReadKey();
    }
    else
    {
        int counter = 0;
        foreach (var file in allDirFiles) //Displaying all the files that Folder contains
        {
            Console.WriteLine($" {counter + 1}) ".Pastel(ConsoleColor.Red) +
                              file.Replace("C:\\Users\\" + userName + "\\OneDrive\\Документы\\cardsLibrary\\", ""));
            counter++;
        }

        Console.WriteLine("----------------------");
        Console.Write(" Choose a file: ");
        string userChoice = Console.ReadLine()!;
        int userIntChoice = 0;
        while (!Int32.TryParse(userChoice, out userIntChoice)) //taking user's input as number to move to the folder
        {
            Console.Write(" Please,input a number: ");
            userChoice = Console.ReadLine()!;
        }

        string? folderFile = allDirFiles[userIntChoice - 1] //we are showing user list starting from 1,so -1 to int
            .Replace("C:\\Users\\" + userName + "\\OneDrive\\Документы\\cardsLibrary\\", "");
        //upgrading path to the file according to user choice
        string? path = "C:\\Users\\" + userName + "\\OneDrive\\Документы\\cardsLibrary\\" + folderFile;

        string[] lines = File.ReadAllLines(path,Encoding.UTF8); //writing all the lines to single array
        if (lines.Length == 0) //if file is empty
        {
            Console.WriteLine(" File is empty.");
            Console.WriteLine(" Tip: import words in format 'Word - Word'".Pastel(ConsoleColor.Green));
            Console.WriteLine("----------------------");
            Thread.Sleep(1000);
            Console.WriteLine(" Press any key to exit".Pastel(ConsoleColor.Red));
            Console.ReadKey();
        }
        else if (lines.Length >= 1) //if file is not empty
        {
            List<string> origWords = new List<string>();
            List<string> translatedWords = new List<string>();
            foreach (string line in lines) //splitting our array by "Word" - "Word" logic
            {
                string[] words = line.Split(" - ");
                origWords.Add(words[0]);
                translatedWords.Add(words[1]);
            }

            Console.Clear();
            Console.WriteLine(" To stop enter 'stop' or 'exit'");
            while (true) //here starts the gameplay
            {
                Console.WriteLine("----------------------");
                int randomChooser = rnd.Next(0, origWords.Count);
                Console.WriteLine($" {origWords[randomChooser]}".Pastel(ConsoleColor.Green));
                Console.Write(" Your guess: ".Pastel(Color.Crimson));
                string userGuess = Console.ReadLine()!.ToLower();
                if (userGuess.Equals("stop") || userGuess.Equals("exit"))
                {
                    Console.WriteLine("----------------------");
                    Console.WriteLine(" Press any key to exit".Pastel(ConsoleColor.Red));
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine(" Translation: " + translatedWords[randomChooser].Pastel(ConsoleColor.Green));
            }
        }
    }
}
    