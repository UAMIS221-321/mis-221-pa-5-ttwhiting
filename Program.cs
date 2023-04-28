string userInput = GetMenuChoice();
while (userInput != "3")
{
    Route(userInput);
    userInput = GetMenuChoice();
}



// End main

static string GetMenuChoice(){
    DisplayMenu();
    string userInput = Console.ReadLine();

    while (!ValidMenuChoice(userInput))
    {
        Console.WriteLine("Invalid menu choice.  Please Enter a Valid Menu Choice");
        Console.WriteLine("Press any key to continue....");
        Console.ReadKey();

        DisplayMenu();
        userInput = Console.ReadLine();
    }

    return userInput;
}

static void DisplayMenu(){
    Console.Clear();
    Console.WriteLine("1:   ");
    Console.WriteLine("2:  ");
    Console.WriteLine("3:  ");
}

static bool ValidMenuChoice(string userInput){
    if(userInput == "1" || userInput == "2" || userInput == "3"){
        return true;
    }
    else{
        return false;
    }

  


}

static void Route(){
    if(userInput == "1"){
       ();
    }
    else if(userInput == "2") {
        ();
    }

     

    


}