﻿using System;
using System.IO;
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
    class RegexInfo
    {
        private int regexNumberOutput;
        private int regexIndexOutput;


        public int RegexNumberOutput
        {
            get { return regexNumberOutput; }
            set { regexNumberOutput = value; }
        }
        public int RegexIndexOutput
        {
            get { return regexIndexOutput; }
            set { regexIndexOutput = value; }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MyWay Interview Exercise.");
            Console.WriteLine("Created by Charles Cowan. \n");
            Console.WriteLine("Greetings user! Please enter any commands to continue. \nEnter '?' for a list of commands.");

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

            //setup regex information
            RegexInfo regexInfo = new RegexInfo();
            string[] task = { "", "", "" };




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
                if(checker == "l" && userInput.Input.Length > 1 || checker == "L" && userInput.Input.Length > 1)
                {
                    checker = "load";
                }

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

                    //basic add case
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

                        //skip over the initial command, remove all proceeding empty spaces
                        string lineTrimmed = input.Substring(1);
                        lineTrimmed = lineTrimmed.TrimStart(' ');

                       /*Legacy Code
                        *
                        * List<string> changeList = new List<string> { };
                        changeList = userList.CurrentList;
                        changeList.Add(lineTrimmed); 
                        
                         */

                        userList.CurrentList.Add(lineTrimmed);
                        Console.WriteLine("Adding " + lineTrimmed + " to the end of the list!");
                        break;


                    //insert case
                    case "I":
                    case "i":
                        //call the regex operation
                        task = regexOperation(userInput.Input, userList.CurrentList);

                        //if there were any errors then break
                        if (task[0] == "false")
                        {
                            break;
                        }
                        //remove the line digit and any other user input from before their text
                        string toTrim = userInput.Input.Substring(Int32.Parse(task[2]));
                        //remove any blank space before the user input for display reasons (Can be removed if this is a requirement in the specification)
                        lineTrimmed = toTrim.TrimStart(' ');

                        //insert the user input text at the requested line number
                        //This will allow user to insert blank lines of text. Unsure if this should be allowed or not. Checking length then prompting y/n answer from user could be better.
                        userList.CurrentList.Insert(Int32.Parse(task[1]) - 1, lineTrimmed);
                        //acknowledgement of user input
                        Console.WriteLine("You have added the following text to line " + (Int32.Parse(task[1])) + ": " + lineTrimmed);
                        break;

                    //delete case
                    case "d":
                    case "D":
                        //call the regex operation
                        task = regexOperation(userInput.Input, userList.CurrentList);

                        //if there were any errors then break
                        if (task[0] == "false")
                        {
                            break;
                        }
                        else
                        {
                            userList.CurrentList.RemoveAt(Int32.Parse(task[1]) - 1);
                            break;
                        }


                    //replace case
                    case "r":
                    case "R":
                        //call the regex operation
                        task = regexOperation(userInput.Input, userList.CurrentList);

                        //if there were any errors then break
                        if (task[0] == "false")
                        {
                            break;
                        }
                        else
                        {
                            // get the first number and value to save
                            int firstLine = Int32.Parse(task[1]) - 1;
                            string firstText = userList.CurrentList[firstLine];
                            userInput.Input = userInput.Input.Substring(Int32.Parse(task[2]));
                            //ensure there is a second value to switch
                            task = regexOperation(userInput.Input, userList.CurrentList);
                            //if not break
                            if (task[0] == "false")
                            {
                                break;
                            }
                            //save this second value
                            int secondLine = Int32.Parse(task[1]) - 1;
                            string secondText = userList.CurrentList[secondLine];

                            //begin the replacement process

                            //two if statements to determine which process to do first, given that the remove at function could accidently remove a incorrect value if done out of sequeence.
                            if (secondLine > firstLine)
                            {
                                userList.CurrentList.Insert(secondLine, firstText);
                                userList.CurrentList.RemoveAt(firstLine);

                                userList.CurrentList.RemoveAt(secondLine);
                                userList.CurrentList.Insert(firstLine, secondText);
                            }
                            else if (firstLine > secondLine)
                            {
                                userList.CurrentList.Insert(firstLine, secondText);
                                userList.CurrentList.RemoveAt(secondLine);

                                userList.CurrentList.RemoveAt(firstLine);
                                userList.CurrentList.Insert(secondLine, firstText);
                            }
                            break;
                        }
                    //modify case
                    case "e":
                    case "E":
                        //call the regex operation
                        task = regexOperation(userInput.Input, userList.CurrentList);

                        //if there were any errors then break
                        if (task[0] == "false")
                        {
                            break;
                        }

                        //get the user input and then trim any proceeding empty white spaces
                        userInput.Input = userInput.Input.Substring(Int32.Parse(task[2]));
                        string userTrim = userInput.Input.TrimStart(' ');
                        //get the index to replace
                        int replaceText = Int32.Parse(task[1]) - 1;

                        //remove the old value and insert the new one
                        userList.CurrentList.RemoveAt(replaceText);
                        userList.CurrentList.Insert(replaceText, userTrim);
                        break;

                    //assistance case to assist user
                    case "?":
                        Console.WriteLine("'l' - Display all lines of text currently held by the program.");
                        Console.WriteLine("'a' - Add a new line of text to the end of those currently held by the program.");
                        Console.WriteLine("'i' - Insert to the specified line number the following text");
                        Console.WriteLine("'d' - Delete the specified line number of text");
                        Console.WriteLine("'r' - Swap the text at the specified line number with the text at the following line number");
                        Console.WriteLine("'e' - Change the text at the specified line number with the succeeding text");
                        Console.WriteLine("'L' - Load a new batch of text from the specified filename");
                        Console.WriteLine("'S' - Save the current lines of text to a specified filename");
                        break;

                    // load case
                    case "load":
                        //check to see if the user gave a filename, if not break
                        if (userInput.Input.Length == 1)
                        {
                            Console.WriteLine("You need to give a filename as well to load!");
                            break;
                        }
                        //skip the input and trim off all preceeding spaces
                        userInput.Input = userInput.Input.Substring(1);
                        userInput.Input = userInput.Input.TrimStart(' ');
                        //check to see if there is still a filename after this
                        //if not then break
                        if (userInput.Input.Length == 0)
                        {
                            Console.WriteLine("You need to have a valid filename!");
                            break;
                        }
                        if (File.Exists(userInput.Input)) { 
                        userList.CurrentList = new List<string>(File.ReadAllLines(userInput.Input));
                        break;
                        }
                        else
                        {
                            Console.WriteLine("This file does not exist.");
                            break;
                        }

                    // save case
                    case "S":
                        //check to see if the user gave a filename, if not break
                        if(userInput.Input.Length == 1)
                        {
                            Console.WriteLine("You need to give a filename as well to save!");
                            break;
                        }
                        //skip the input and trim off all preceeding spaces
                        userInput.Input = userInput.Input.Substring(1);
                        userInput.Input = userInput.Input.TrimStart(' ');
                        //check to see if there is still a filename after this
                        //if not then break
                        if(userInput.Input.Length == 0)
                        {
                            Console.WriteLine("You need to have a valid filename!");
                            break;
                        }
                        //additionally could add in another regex use where it would check for invalid operating system filenames, however for the scope of this seems excessive

                        //Otherwise, write all files to textfile of user specification. Overwrites existing files

                        File.WriteAllLines(userInput.Input, userList.CurrentList);
                        /* Text Writer first use, try File.Write
                        TextWriter tw = new StreamWriter(userInput.Input);

                        foreach (String s in userList.CurrentList)
                            tw.WriteLine(s); */
                        break;

                }
                //check after break if user has used the quit case if not then request further input
                if (endApp == false) Console.Write("\nPress 'q' and enter in order to close the application, or perform another operation.\n");
                //if they have used the quit case then thank them for using this program
                else Console.Write("Thank you for using this program!");
            }
        }
        public static string[] regexOperation(string inputRegex, List<string> currentList)
        {
            Match match = Regex.Match(inputRegex, @"-?\d+");
            string lineString = "";
            int index = match.Index;
            string[] output = { "false", "", "" };
            //parse what was found value 

            //TODO Make sure to catch user exception where they do not give a line to input
            if (match.Success == false)
            {
                Console.WriteLine("You must enter a line number.");
                return output;
            }
            int value = Int32.Parse(match.Value);
            int search = 0;

            //if the match was a success then do the following
            if (match.Success && value > 0)
            {
                //get the linestring for output to user and also work out where the digit ends on the main user input
                lineString = match.Value;
                search = index + lineString.Length;
                // parse the value of linestring and if it is beyond the number of elements in the list then display to the user that's the case and break
                int number = Int32.Parse(lineString) - 1;
                if (number > currentList.Count)
                {
                    Console.WriteLine("You are trying to access a line number that does not exist!");
                    return output;
                }
                else
                {
                    Console.WriteLine("You have input as your number: " + lineString);
                    output[0] = "true";
                    output[1] = "" + match.Value;
                    output[2] = "" + search;
                    return output;
                }
            }
            //any issues display to user
            else
            {
                Console.WriteLine("You must give a valid line number that is not negative or 0, please try again.");
                return output;
            }
        }
    }
}
