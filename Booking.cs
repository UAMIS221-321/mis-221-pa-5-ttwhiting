using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mis_221_pa_5_ttwhiting
{
    public class Booking
    {
        public void BookingsMenu()
        {
            string input = "invalid";
            while (input != "valid"){
                Console.Clear();
                Console.WriteLine("        Bookings      ");
                Console.WriteLine("-------------------------");
                Console.WriteLine("Please select an option");
                Console.WriteLine("Select 1 to View Session availability");
                Console.WriteLine("Select 2 to Book a Session");
                Console.WriteLine("Select 3 to Edit Booking");
                Console.WriteLine("Select 4 to Exit Application");
                string answer = Console.ReadLine();
                string lowerAnswer = answer.ToLower();
                if (lowerAnswer == "1"){
                    input = "valid";
                    BookingsOpenList();
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    BookingsMenu();
                }
                else if (lowerAnswer == "2"){
                    input = "valid";
                    BookingsBookSession();
                }
                else if (lowerAnswer == "3"){
                    input = "valid";
                    BookingsEdit();
                }
                else if (lowerAnswer == "4"){
                    input = "valid";
                }
                else{

                }
            }
        }

        static void BookingsOpenList()
        {
            Console.Clear();
            StreamReader LL = new StreamReader("listings.txt");
            string temp = LL.ReadLine();
            Console.WriteLine($"  Listing ID\t   Trainer ID\t\tSession Date\tSession Time\tSession Cost\tAvailability");
            while (temp != null)
            {
                string [] tempArray = temp.Split('#');
                if (tempArray[5] == "open" || tempArray[5] == "Open" || tempArray[5] == "available" || tempArray[5] == "Available"){
                Console.WriteLine($"\t{tempArray[0]}\t\t{tempArray[1]}\t\t{tempArray[2]}\t\t\t{tempArray[3]}\t\t{tempArray[4]}\t{tempArray[5]}"); 
                }
                temp = LL.ReadLine();
            }
            LL.Close();
        }

        static void BookingsBookSession()
        {
            BookingsOpenList();
            string lID = "";
            string trainingDate = "";
            string trainerID = "";
            string trainerName = "";
            Console.WriteLine("listing ID");
            string listingID = Console.ReadLine();
            StreamReader listing = new StreamReader("listings.txt");
            string listingInput = listing.ReadLine();
            while (listingInput != null)
            {
                string []tempList = listingInput.Split('#');
                if (listingID == tempList[0]){
                    lID = tempList[0];
                    trainingDate = tempList[2];
                    trainerID = tempList[1];
                    listingInput = listing.ReadLine();
                }
                else{
                    listingInput = listing.ReadLine();
                }
            }
            if (listingID != lID){
                Console.WriteLine("Oops, invalid listing ID");
                BookingsBookSession();
            }
            listing.Close();
            StreamReader trainer = new StreamReader("trainers.txt");
            string trainerInput = trainer.ReadLine();
            while (trainerInput != null)
            {
                string [] tempList = trainerInput.Split('#');
                if (trainerID == tempList[3]){
                    trainerName = tempList[0];
                    trainerInput = trainer.ReadLine();
                }
                else{
                    trainerInput = trainer.ReadLine();
                }
            }   
            trainer.Close();
            string input = "invalid";
            int ID =0;
            string[] tempArray = new string[100];
            StreamReader myfile = new StreamReader("transactions.txt");

            string fileInput = myfile.ReadLine();
            while (fileInput != null)
            {
                tempArray = fileInput.Split('#');
                ID = int.Parse(tempArray[0]) + 1;
                fileInput = myfile.ReadLine();
            }   
            myfile.Close();
            Console.Clear();
            Console.WriteLine("Please enter The customer's Name");
            string name = Console.ReadLine();
            Console.Clear();
            Console.WriteLine("Please enter the customer's email");
            string eMail = Console.ReadLine();
            while (input != "valid")
            {
                Console.Clear();
                Console.WriteLine("Is the following information correct?");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine($"Session ID:                {ID}");
                Console.WriteLine($"Customers Name:            { name }");
                Console.WriteLine($"Customers E-Mail Address:  {eMail}");
                Console.WriteLine($"Session Date:              {trainingDate}");
                Console.WriteLine($"Trainer ID:                {trainerID}");
                Console.WriteLine($"Trainer Name:              {trainerName}");
                Console.WriteLine("---------------------------------------------------");
                Console.WriteLine("Yes");
                Console.WriteLine("No");
                string answer = Console.ReadLine();
                string lowerAnswer = answer.ToLower();
                if (lowerAnswer == "yes"){
                    string bookingInput = (ID+"#"+name+"#"+eMail+"#"+trainingDate+"#"+trainerID+"#"+trainerName+"#"+"booked");
                    StreamWriter trainerInfo = new StreamWriter("transactions.txt", true);
                    trainerInfo.Write(bookingInput);
                    trainerInfo.WriteLine();
                    trainerInfo.Close();
                    input = "valid";
                }
                else if (lowerAnswer == "no"){
                    input = "valid";
                    BookingsBookSession();
                }
                else{}
            }

            string tID = "";
            string date = "";
            string time = "";
            string cost = "";
            string reserved = "";
            StreamReader myfile2 = new StreamReader("listings.txt");

            string fileInput2 = myfile2.ReadLine();
            while (fileInput2 != null)
            {
                string[] tempArray2 = fileInput2.Split('#');
                    if (lID == tempArray2[0])
                    {
                        tID = tempArray2[1];
                        date = tempArray2[2];
                        time = tempArray2[3];
                        cost = tempArray2[4];
                        reserved = tempArray2[5];
                    }
                fileInput2 = myfile2.ReadLine();
            }
                    myfile2.Close();
                    reserved = "reserved";
                    loadPush(lID,tID,date,time,cost,reserved);
                    
            myfile2.Close();
            static void loadPush(string lID, string tID, string date, string time, string cost, string reserved)
            {
                int count =0;
                Console.Clear();
                StreamReader sessions = new StreamReader("listings.txt");
                StreamWriter loadWrite = new StreamWriter("Load.txt");
                Trainer classObj = new Trainer();
                classObj.TrainerList();
                string fileInput = sessions.ReadLine();
                while (fileInput != null)
                {
                    string[] tempArray = fileInput.Split('#');
                    if (lID == tempArray[0]){
                        loadWrite.Write(lID+"#"+tID+"#"+date+"#"+time+"#"+cost+"#"+reserved);
                        loadWrite.Write('^');
                        count++;
                    }
                    else{
                        loadWrite.Write(fileInput);
                        loadWrite.Write('^');
                        count++;
                    }
                    fileInput = sessions.ReadLine();
                }
                sessions.Close();
                loadWrite.Close();
                StreamReader loadRead = new StreamReader("Load.txt");
                StreamWriter sessionssWrite = new StreamWriter("listings.txt");
                string fileload = loadRead.ReadLine();
                while (fileload != null)
                {
                    string[] tempArray = fileload.Split('^');
                    for (int i =0;i<count;i++){
                        sessionssWrite.Write(tempArray[i]);
                        sessionssWrite.WriteLine();
                    }    
                    fileload = loadRead.ReadLine();
                }
                sessionssWrite.Close();
                loadRead.Close();
               
        }
            Booking classObj = new Booking();
            classObj.BookingsMenu();
        }

        static void BookingsEdit()
        {
            string name = "";
            string eMail = "";
            string trainingDate = "";
            string trainerID = "";
            string trainerName = "";
            string availability = "";
            Console.Clear();
            Console.WriteLine("Please enter Session ID");
            string ID = Console.ReadLine();

            StreamReader myfile = new StreamReader("transactions.txt");

            string fileInput = myfile.ReadLine();
            while (fileInput != null)
            {
                string[] tempArray = fileInput.Split('#');
                    if (ID == tempArray[0])
                    {
                        name = tempArray[1];
                        eMail = tempArray[2];
                        trainingDate = tempArray[3];
                        trainerID = tempArray[4];
                        trainerName = tempArray[5];
                        availability = tempArray[6];
                    }
                fileInput = myfile.ReadLine();
            }
                myfile.Close();
                Console.Clear();
                Console.WriteLine("Session info: ");
                Console.WriteLine($"Session ID:                {ID}");
                Console.WriteLine($"Customer's Name:            { name }");
                Console.WriteLine($"Customer's E-Mail Address:  {eMail}");
                Console.WriteLine($"Session Date:              {trainingDate}");
                Console.WriteLine($"Trainer ID:                {trainerID}");
                Console.WriteLine($"Trainer Name:              {trainerName}");
                Console.WriteLine($"Session Availability:      {availability}");
                Console.WriteLine("Was the session canceled, completed, or no show?");
                availability = Console.ReadLine();
                Push(ID, name, eMail, trainingDate, trainerID, trainerName, availability);

            static void Push(string ID, string name, string eMail, string trainingDate, string trainerID, string trainerName, string availability)
            {
                int count =0;
                Console.Clear();
                StreamReader transactions = new StreamReader("transactions.txt");
                StreamWriter loadWrite = new StreamWriter("Load.txt");
                Trainer classObj2 = new Trainer();
                classObj2.TrainerList();
                string fileInput = transactions.ReadLine();
                while (fileInput != null)
                {
                    string[] tempArray = fileInput.Split('#');
                    if (ID == tempArray[0]){
                        loadWrite.Write(ID+"#"+name+"#"+eMail+"#"+trainingDate+"#"+trainerID+"#"+trainerName+"#"+availability);
                        loadWrite.Write('^');
                        count++;
                    }
                    else{
                        loadWrite.Write(fileInput);
                        loadWrite.Write('^');
                        count++;
                    }
                    fileInput = transactions.ReadLine();
                }
                transactions.Close();
                loadWrite.Close();
                StreamReader loadRead = new StreamReader("Load.txt");
                StreamWriter transactionsWrite = new StreamWriter("transactions.txt");
                string fileload = loadRead.ReadLine();
                while (fileload != null)
                {
                    string[] tempArray = fileload.Split('^');
                    for (int i =0;i<count;i++){
                        transactionsWrite.Write(tempArray[i]);
                        transactionsWrite.WriteLine();
                    }    
                    fileload = loadRead.ReadLine();
                }
                transactionsWrite.Close();
                loadRead.Close();
                Booking classObj = new Booking();
                classObj.BookingsMenu();
            }    
        }
    }
}