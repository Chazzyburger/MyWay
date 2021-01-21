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
                if(checker == "l" && userInput.Input.Length > 1)
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

                    //TODO basic add case
                    case "a":
                    case "A":
                       
                        break;


                    // TODO insert case
                    case "I":
                     case "i":

                         break;

                        //TODO delete case
                    case"d":
                    case "D":
                       
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
