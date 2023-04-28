using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mis_221_pa_5_ttwhiting
{
    public class Listing
    {   
        public void Sessions()
        {
            string input = "invalid";
            while (input != "valid"){
                Console.WriteLine("Choose one of the following options");
                Console.WriteLine("-----------------------");
                Console.WriteLine("Select 1 to Add");
                Console.WriteLine("Select 2 to Edit");
                Console.WriteLine("Select 3 to Delete");
                Console.WriteLine("Select 4 to View List");
                Console.WriteLine("Select 5 to Exit Application");
                string answer = Console.ReadLine();
                string lowerAnswer = answer.ToLower();
                if (lowerAnswer == "1"){
                    input = "valid";
                    SessionsAdd();
                }
                else if (lowerAnswer ==  "2"){
                    input = "valid";
                    SessionsEdit();
                }
                else if (lowerAnswer == "3"){
                    input = "valid";
                    SessionsDelete();
                }
                else if (lowerAnswer ==  "4"){
                    input = "valid";
                    SessionsList();
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    Sessions();
                }
                else if (lowerAnswer == "5"){
                    input = "valid";
                }
                else{

                }
            }
        }

        static void SessionsAdd()
        {
            int lID =1;
            string name ="";
            string[] tempArray = new string[100];
            StreamReader myfile = new StreamReader("listings.txt");

            string fileInput = myfile.ReadLine();
            while (fileInput != null)
            {
                tempArray = fileInput.Split('#');
                lID = int.Parse(tempArray[0]) + 1;
                fileInput = myfile.ReadLine();
            }   
            myfile.Close();
            string input = "invalid";
            Console.Clear();
            Console.WriteLine("Enter Trainers ID");
            string tID = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter the session date (00/00/00)");
            string date = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter the time of the session");
            string time = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter the session's cost");
            string cost = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Enter the session's availability");
            string reserved = Console.ReadLine();
            StreamReader trainers = new StreamReader("trainers.txt");

            string fileInput2 = trainers.ReadLine();
            while (fileInput2 != null)
            {
                string[] tempName = fileInput2.Split('#');
                if (tID == tempName[3]){
                    name = tempName[0];
                }
                fileInput2 = trainers.ReadLine();
            }   
            trainers.Close();
            while (input != "valid")
            {
                Console.Clear();
                Console.WriteLine("is the Session info correct");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine($"Listing ID:\t{lID}");
                Console.WriteLine($"Trainers Name:\t{name}");
                Console.WriteLine($"Session Date:\t{date}");
                Console.WriteLine($"Session Time:\t{time}");
                Console.WriteLine($"Session Cost:\t{cost}");
                Console.WriteLine($"Session Availability:\t{reserved}");
                Console.WriteLine("Yes");
                Console.WriteLine("No");
                string answer = Console.ReadLine();
                string lowerAnswer = answer.ToLower();
                if (lowerAnswer == "yes"){
                    string trainerInput = (lID+"#"+tID+"#"+date+"#"+time+"#"+cost+"#"+reserved);
                    StreamWriter sessionInfo = new StreamWriter("listings.txt", true);
                    sessionInfo.Write(trainerInput);
                    sessionInfo.WriteLine();
                    sessionInfo.Close();
                    input = "valid";
                }
                else if (lowerAnswer == "no"){
                    input = "valid";
                    SessionsAdd();
                }
                else{

                }
            }
            Listing classObj2 = new Listing();
                    classObj2.Sessions();
        }

        static void SessionsEdit()
        {
            string tID = "";
            string date = "";
            string time = "";
            string cost = "";
            string reserved = "";
            Console.Clear();
            Console.WriteLine("Please enter the Listing ID");
            string lID = Console.ReadLine();

            StreamReader myfile = new StreamReader("listings.txt");

            string fileInput = myfile.ReadLine();
            while (fileInput != null)
            {
                string[] tempArray = fileInput.Split('#');
                    if (lID == tempArray[0])
                    {
                        tID = tempArray[1];
                        date = tempArray[2];
                        time = tempArray[3];
                        cost = tempArray[4];
                        reserved = tempArray[5];
                    }
                fileInput = myfile.ReadLine();
            }
                    myfile.Close();
                    Console.Clear();
                    Console.WriteLine($"Trainer ID: {tID}");
                    Console.WriteLine($"Session Date {date}");
                    Console.WriteLine($"Session Time {time}");
                    Console.WriteLine($"Session Cost {cost}");
                    Console.WriteLine($"Session Availability {reserved}");
                    Console.WriteLine("What field would you like to edit?");
                    Console.WriteLine("Trainer");
                    Console.WriteLine("Date");
                    Console.WriteLine("Time");
                    Console.WriteLine("cost");
                    Console.WriteLine("Availability");
                    string input = Console.ReadLine();
                    string lowerInput = input.ToLower();
                    if (lowerInput == "id"||lowerInput == "trainer"||lowerInput == "trainer id"){
                        Console.WriteLine("Edit Trainer");
                        tID = Console.ReadLine();
                        LoadPush(lID,tID,date,time,cost,reserved);
                    }
                    else if (lowerInput == "date"){
                        Console.WriteLine("Edit session date");
                        date = Console.ReadLine();
                        LoadPush(lID,tID,date,time,cost,reserved);
                    }
                    else if (lowerInput == "time"){
                        Console.WriteLine("Edit session time");
                        time = Console.ReadLine();
                        LoadPush(lID,tID,date,time,cost,reserved);
                    }
                    else if (lowerInput == "cost"){
                        Console.WriteLine("Edit session cost");
                        cost = Console.ReadLine();
                        LoadPush(lID,tID,date,time,cost,reserved);
                    }
                    else if (lowerInput == "availability"){
                        Console.WriteLine("Edit session abailability");
                        date = Console.ReadLine();
                        LoadPush(lID,tID,date,time,cost,reserved);
                    }
            myfile.Close();
            static void LoadPush(string lID, string tID, string date, string time, string cost, string reserved)
            {
                int count =0;
                Console.Clear();
                StreamReader sessions = new StreamReader("listings.txt");
                StreamWriter LoadWrite = new StreamWriter("Load.txt");
                Trainer classObj = new Trainer();
                classObj.TrainerList();
                string fileInput = sessions.ReadLine();
                while (fileInput != null)
                {
                    string[] tempArray = fileInput.Split('#');
                    if (lID == tempArray[0]){
                        LoadWrite.Write(lID+"#"+tID+"#"+date+"#"+time+"#"+cost+"#"+reserved);
                        LoadWrite.Write('^');
                        count++;
                    }
                    else{
                        LoadWrite.Write(fileInput);
                        LoadWrite.Write('^');
                        count++;
                    }
                    fileInput = sessions.ReadLine();
                }
                sessions.Close();
                LoadWrite.Close();
                StreamReader LoadRead = new StreamReader("Load.txt");
                StreamWriter sessionssWrite = new StreamWriter("listings.txt");
                string fileLoad = LoadRead.ReadLine();
                while (fileLoad != null)
                {
                    string[] tempArray = fileLoad.Split('^');
                    for (int i =0;i<count;i++){
                        sessionssWrite.Write(tempArray[i]);
                        sessionssWrite.WriteLine();
                    }    
                    fileLoad = LoadRead.ReadLine();
                }
                sessionssWrite.Close();
                LoadRead.Close();
            }    
            Listing classObj2 = new Listing();
                    classObj2.Sessions();
        }

        static void SessionsDelete()
        {
            int count =0;
            Console.Clear();
            SessionsList();
            StreamReader listings = new StreamReader("listings.txt");
            StreamWriter LoadWrite = new StreamWriter("Load.txt");
            Console.WriteLine("input Listing ID to DELETE");
            string delete = Console.ReadLine();
            string fileInput = listings.ReadLine();
            while (fileInput != null)
            {
                string[] tempArray = fileInput.Split('#');
                if (delete == tempArray[0]){
                }
                else{
                    LoadWrite.Write(fileInput);
                    LoadWrite.Write('^');
                    count++;
                }
                fileInput = listings.ReadLine();
            }
            listings.Close();
            LoadWrite.Close();
            StreamReader LoadRead = new StreamReader("Load.txt");
            StreamWriter listingsWrite = new StreamWriter("listings.txt");
            string fileLoad = LoadRead.ReadLine();
            while (fileLoad != null)
            {
                string[] tempArray = fileLoad.Split('^');
                for (int i =0;i<count;i++){
                    listingsWrite.Write(tempArray[i]);
                    listingsWrite.WriteLine();
                }    
                fileLoad = LoadRead.ReadLine();
            }
            listingsWrite.Close();
            LoadRead.Close();
            Listing classObj2 = new Listing();
                    classObj2.Sessions();
        }

        static void SessionsList()
        {
            Console.Clear();
            StreamReader SL = new StreamReader("listings.txt");
            string temp = SL.ReadLine();
            Console.WriteLine($"  Listing ID\t   Trainer ID\t\tSession Date\tSession Time\tSession Cost\tAvailability");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            while (temp != null)
            {
                string [] tempArray = temp.Split('#');
                Console.WriteLine($"\t{tempArray[0]}\t\t{tempArray[1]}\t\t{tempArray[2]}\t\t\t{tempArray[3]}\t\t{tempArray[4]}\t{tempArray[5]}");
                temp = SL.ReadLine();
            }
            SL.Close();
        }
    }
}