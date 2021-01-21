using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace MyWayTest
{
    class List
    {
        //get set for the list we'll be using
        private List<string> currentList;

        public List<string> CurrentList
        {
            get { return currentList; }
            set { currentList = value; }
        }
    }

    class UserInput
    {
        //same for user input
        private string input;

        public string Input
        {
            get { return input; }
            set { input = value; }
        }
    }

    class Program
    {
       
        static void Main(string[] args)
        {
            //create the default list so that the program has it ready to replace the current one if that functionality is needed
            List<string> defaultList = new List<string> {
                "Welcome to the timewarp of programs!",
                "Applications like this were used in the 1980s.",
                "I can't wait for User Interfaces to be invented.",
                "Then I can do much more complicated things."
            };
            //instantiate the user list and set it to the same value as the default
            List userList = new List();
            userList.CurrentList = defaultList;
            //instantiate the user input and make sure it is nothing at the moment.
            UserInput userInput = new UserInput();
            userInput.Input = "";


            //simple bool to check if the user wants to quit or not
            bool endApp = false;
            //while that bool is false the program will look for user input.
            while (!endApp)
            {
                //input into the console is set as the user input.
                userInput.Input = Console.ReadLine();

                //input recording for bugfixing
                Console.WriteLine("Your input is " + userInput.Input);

                //checker to if the user is trying to display the text or load from a file
                string checker = new string("");
                checker = userInput.Input.Substring(0, 1);

                //if statement to check if the user wants to load, can be redone later to improve, potential issues with user mistakes
                if (checker == "l" && userInput.Input.Length > 1)
                {
                    checker = "load";
                }

                //switch for user input
                //switch for user input
                switch (checker)
                {
                    //quit cases
                    case "q":
                    case "Q":
                        //set bool to true to stop while loop
                        endApp = true;
                        break;

                    //display list cases
                    case "L":
                    case "l":
                        //for each value in the user list write it onto the console
                        userList.CurrentList.ForEach(Console.WriteLine);
                        break;

                    // basic add case
                    case "a":
                    case "A":
                        //check to see if the user has any input if not break
                        if (userInput.Input.Length < 2)
                        {
                            Console.WriteLine("You need to have an input with this operation! Please try again.");
                            break;
                        }

                        //take the input and add it to the end of the current user list.
                        string input = userInput.Input;
                        Console.WriteLine("You inputted " + input);
                        string lineTrimmed = input.Substring(2);
                        List<string> changeList = new List<string> { };
                        changeList = userList.CurrentList;
                        changeList.Add(lineTrimmed);
                        userList.CurrentList = changeList;
                        Console.WriteLine("Adding " + lineTrimmed + " to the end of the list!");
                        break;


                    // insert case
                    case "I":
                    case "i":
                        //regex to get the value the user inputted. only takes digit, could have issues if the user did not put a space after their line number
                        Match match = Regex.Match(userInput.Input, @"-?\d+");
                        string lineString = "";
                        int index = match.Index;
                        //parse what was found value 

                        //TODO Make sure to catch user exception where they do not give a line to input
                        if (match.Success == false)
                        {
                            Console.WriteLine("You must enter a line number for your message to be inserted into.");
                            break;
                        }
                        int value = Int32.Parse(match.Value);
                        int search = 0;

                        //if the match was a success then do the following
                        if (match.Success && value > 0)
                        {
                            //get the linestring for output to user and also work out where the digit ends on the main user input
                            lineString = match.Value;
                            search = index + lineString.Length;
                            Console.WriteLine("You have input as your number: " + lineString);
                        }
                        //any issues display to user
                        else
                        {
                            Console.WriteLine("You must give a valid line number that is not negative or 0, please try again.");
                            break;
                        }

                        // parse the value of linestring and if it is beyond the number of elements in the list then display to the user that's the case and break
                        int number = Int32.Parse(lineString) - 1;
                        if (number > userList.CurrentList.Count)
                        {
                            Console.WriteLine("You are trying to insert to a line number that does not exist! Please use the a function instead.");
                            break;
                        }

                        //remove the line digit and any other user input from before their text
                        string toTrim = userInput.Input.Substring(search);
                        //remove any blank space before the user input for display reasons (Can be removed if this is a requirement in the specification)
                        lineTrimmed = toTrim.TrimStart(' ');

                        //insert the user input text at the requested line number
                        //This will allow user to insert blank lines of text. Unsure if this should be allowed or not. Checking length then prompting y/n answer from user could be better.
                        userList.CurrentList.Insert(number, lineTrimmed);
                        //acknowledgement of user input
                        Console.WriteLine("You have added the following text to line " + (number + 1) + ": " + lineTrimmed);
                        break;

                    // delete case
                    case "d":
                    case "D":
                        //regex to get the value the user inputted. only takes digit, could have issues if the user did not put a space after their line number
                        Match deleteMatch = Regex.Match(userInput.Input, @"-?\d+");

                        int deleteIndex = deleteMatch.Index;
                        //parse what was found value 

                        //TODO Make sure to catch user exception where they do not give a line to input
                        if (deleteMatch.Success == false)
                        {
                            Console.WriteLine("You must enter a line number to delete.");
                            break;
                        }
                        int deleteValue = Int32.Parse(deleteMatch.Value);

                        //if the match was a success then do the following
                        if (deleteMatch.Success && deleteValue > 0)
                        {
                            //get the linestring for output to user and also work out where the digit ends on the main user input
                            lineString = deleteMatch.Value;

                            Console.WriteLine("You have input as your number: " + lineString);
                            if (deleteValue - 1 > userList.CurrentList.Count)
                            {
                                Console.WriteLine("You are trying to delete a line number that does not exist!");
                                break;
                            }
                        }
                        else
                        {
                            //any issues display to user

                            Console.WriteLine("You must give a valid line number that is not negative or 0, please try again.");
                            break;
                        }
                        userList.CurrentList.RemoveAt(deleteValue - 1);
                        break;


                    //TODO replace case

                    //TODO modify case

                    //TODO load case
                    case "load":
                        break;

                        //TODO save case
                }
                //check after break if user has used the quit case if not then request further input
                if (endApp == false) Console.Write("\nPress 'q' and enter in order to close the application, or perform another operation.\n");
                //if they have used the quit case then thank them for using this program
                else Console.Write("Thank you for using this program!");
            }
            }
    }
}
