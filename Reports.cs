using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mis_221_pa_5_ttwhiting
{
    public class Reports
    {
        public void ReportsMenu()
        {
            string input = "invalid";
            while (input != "valid"){
                Console.Clear();
                Console.WriteLine("        Reports       ");
                Console.WriteLine("Please select an option");
                Console.WriteLine("-----------------------");
                Console.WriteLine("Enter 1 for Individual Customer Sessions");
                Console.WriteLine("Enter 2 for Historical Customer Sessions");
                Console.WriteLine("Enter 3 for Historical Revenue Report");
                Console.WriteLine("Enter 4 for Individual Trainer Sessions");
                Console.WriteLine("Enter 5 for Session cost");
                Console.WriteLine("Enter 6 for Exit Application");
                string answer = Console.ReadLine();
                string lowerAnswer = answer.ToLower();
                if (lowerAnswer == "1"){
                    input = "valid";
                    IndividualCustomer();
                }
                else if (lowerAnswer =="2"){
                    input = "valid";
                    date();
                }
                else if (lowerAnswer =="3"){
                    input = "valid";
                   Revenue();
                }
                else if (lowerAnswer =="4"){
                    input = "valid";
                    IndividualTrainer();                    
                }
                else if (lowerAnswer =="5"){
                    input = "valid";
                    MoneySort();
                }
                else if (lowerAnswer =="6"){
                    input = "valid";
                
                }
                else{

                }
            }
        }

        static void IndividualCustomer()
        {
            Console.Clear();
            Console.WriteLine("Enter Customer E-Mail Address.");
            string input = Console.ReadLine();
            Console.Clear();
            StreamReader LL = new StreamReader("transactions.txt");
            string temp = LL.ReadLine();
            Console.WriteLine($"  Transaction ID\t   Customer Name\t\tCustomer Email\tTraining Date\tTrainer ID\tTrainer Name\tStatus");
            while (temp != null)
            {
                string [] tempArray = temp.Split('#');
                if (tempArray[2] == input){
                Console.WriteLine($"\t{tempArray[0]}\t\t{tempArray[1]}\t\t{tempArray[2]}\t\t\t{tempArray[3]}\t\t{tempArray[4]}\t{tempArray[5]}\t{tempArray[6]}"); 
                }
                temp = LL.ReadLine();
            }
            LL.Close();
            Console.WriteLine("Would you like to save this report?");
            Console.WriteLine("Yes");
            Console.WriteLine("No");
            string answer = Console.ReadLine();
            string lowerAnswer = answer.ToLower();
            if (lowerAnswer == "yes");
            {
                Console.WriteLine("Enter report name.");
                string saveName = Console.ReadLine();
                StreamWriter myReport = new StreamWriter($"{saveName}.txt");
                myReport.Close();
                
                int count =0;
                Console.Clear();
                StreamReader trainers = new StreamReader("transactions.txt");
                StreamWriter LoadWrite = new StreamWriter("Load.txt");
                string fileInput = trainers.ReadLine();
                while (fileInput != null)
                {
                    string[] tempArray = fileInput.Split('#');
                    if (input == tempArray[2]){
                        LoadWrite.Write($"{tempArray[0]} {tempArray[1]} {tempArray[2]} {tempArray[3]} {tempArray[4]} {tempArray[5]} {tempArray[6]}");
                        LoadWrite.Write('^');
                        count++;
                    }
                    else{
                        count++;
                    }
                    fileInput = trainers.ReadLine();
                }
                trainers.Close();
                LoadWrite.Close();
                StreamReader LoadRead = new StreamReader("Load.txt");
                StreamWriter trainersWrite = new StreamWriter($"{saveName}.txt");
                string fileLoad = LoadRead.ReadLine();
                while (fileLoad != null)
                {
                    string[] tempArray = fileLoad.Split('^');
                    for (int i =0;i<count;i++){
                        trainersWrite.Write(tempArray[i]);
                        trainersWrite.WriteLine();
                    }    
                    fileLoad = LoadRead.ReadLine();
                }
                trainersWrite.Close();
                LoadRead.Close();
            }
            
            else
            {

            }
        }

        static void date(){
            Console.Clear();
            int count = 1;
            StreamReader SL = new StreamReader("transactions.txt");
            List<string> dates = new List<string>();
            List<DateTime> dateTime = new List<DateTime>();
            string temp = SL.ReadLine();
            while (temp != null)
            {
                string[] tempArray = temp.Split('#');
                string text = tempArray[3];
                dateTime.Add(DateTime.ParseExact(text, "00/00/0000", null));
                temp = SL.ReadLine();
                count++;
            }          
            SL.Close();
            dateTime.Sort();
            
            for(int i = 0; i < count - 1; i++)      
            {
                StreamReader LL = new StreamReader("transactions.txt");
                string temp2 = LL.ReadLine();
                while (temp2 != null)
                {
                    string [] tempArray = temp2.Split('#');
                    string text = tempArray[3];                    
                    if (dateTime[i] == DateTime.ParseExact(text, "00/00/0000", null))
                    {
                        Console.WriteLine($"\t{tempArray[0]} {tempArray[1]} {tempArray[2]} {tempArray[3]} {tempArray[4]} {tempArray[5]} {tempArray[6]}"); 
                    }                        
                    temp2 = LL.ReadLine();
                } 
                LL.Close();
            }
        }

        static void Revenue()
        {
            int money = 0;
            Console.Clear();
            StreamReader LL = new StreamReader("listings.txt");
            string temp = LL.ReadLine();
            while (temp != null)
            {
                string [] tempArray = temp.Split('#');
                if (tempArray[5] == "taken"){
                Console.WriteLine($"{tempArray[0]} {tempArray[1]} {tempArray[2]} {tempArray[3]} {tempArray[4]} {tempArray[5]}"); 
                money += int.Parse(tempArray[4]);
                }
                temp = LL.ReadLine();
            }
            LL.Close();
            Console.WriteLine("Total revenue earned is: $" + money);

            Console.WriteLine("Would you like to save this report?");
            Console.WriteLine("Yes");
            Console.WriteLine("No");
            string answer = Console.ReadLine();
            string lowerAnswer = answer.ToLower();
            if (lowerAnswer == "yes")
            {
                Console.WriteLine("Enter report name.");
                string saveName = Console.ReadLine();
                StreamWriter myReport = new StreamWriter($"{saveName}.txt");
                myReport.Close();
                
                int count =0;
                Console.Clear();
                StreamReader trainers = new StreamReader("listings.txt");
                StreamWriter LoadWrite = new StreamWriter("Load.txt");
                string fileInput = trainers.ReadLine();
                while (fileInput != null)
                {
                    string[] tempArray = fileInput.Split('#');
                    if ("taken" == tempArray[4]){
                        LoadWrite.Write($"{tempArray[0]} {tempArray[1]} {tempArray[2]} {tempArray[3]} {tempArray[4]} {tempArray[5]}");
                        LoadWrite.Write('^');
                        count++;
                    }
                    else{
                        LoadWrite.Write(fileInput);
                        LoadWrite.Write('^');
                        count++;
                    }
                    fileInput = trainers.ReadLine();
                }

                LoadWrite.Write("Total revenue earned is: $" + money);
                
                count++;
                trainers.Close();
                LoadWrite.Close();
                StreamReader LoadRead = new StreamReader("Load.txt");
                StreamWriter trainersWrite = new StreamWriter($"{saveName}.txt");
                string fileLoad = LoadRead.ReadLine();
                while (fileLoad != null)
                {
                    string[] tempArray = fileLoad.Split('^');
                    for (int i =0;i<count;i++){
                        trainersWrite.Write(tempArray[i]);
                        trainersWrite.WriteLine();
                    }    
                    fileLoad = LoadRead.ReadLine();
                }
                trainersWrite.Close();
                LoadRead.Close(); 
            }
            else
            {

            }
        }

        static void IndividualTrainer()
        {
            Console.Clear();
            Console.WriteLine("Enter Trainer ID.");
            string input = Console.ReadLine();
            Console.Clear();
            StreamReader LL = new StreamReader("transactions.txt");
            string temp = LL.ReadLine();
            Console.WriteLine($"  Transaction ID\t   Customer Name\t\tCustomer Email\tTraining Date\tTrainer ID\tTrainer Name\tStatus");
            Console.WriteLine("-------------------------------------------------------------------------------------------------------------------");
            while (temp != null)
            {
                string [] tempArray = temp.Split('#');
                if (tempArray[4] == input){
                Console.WriteLine($"\t{tempArray[0]}\t\t{tempArray[1]}\t\t{tempArray[2]}\t\t\t{tempArray[3]}\t\t{tempArray[4]}\t{tempArray[5]}\t{tempArray[6]}"); 
                }
                temp = LL.ReadLine();
            }
            LL.Close();
            Console.WriteLine("Would you like to save this report?");
            Console.WriteLine("Yes");
            Console.WriteLine("No");
            string answer = Console.ReadLine();
            string lowerAnswer = answer.ToLower();
            if (lowerAnswer == "yes")
            {
                Console.WriteLine("Enter the report name.");
                string saveName = Console.ReadLine();
                StreamWriter myReport = new StreamWriter($"{saveName}.txt");
                myReport.Close();
                
                int count =0;
                Console.Clear();
                StreamReader trainers = new StreamReader("transactions.txt");
                StreamWriter LoadWrite = new StreamWriter("Load.txt");
                string fileInput = trainers.ReadLine();
                while (fileInput != null)
                {
                    string[] tempArray = fileInput.Split('#');
                    if (input == tempArray[4]){
                        LoadWrite.Write($"{tempArray[0]} {tempArray[1]} {tempArray[2]} {tempArray[3]} {tempArray[4]} {tempArray[5]} {tempArray[6]}");
                        LoadWrite.Write('^');
                        count++;
                    }
                    else{
                        count++;
                    }
                    fileInput = trainers.ReadLine();
                }
                trainers.Close();
                LoadWrite.Close();
                StreamReader LoadRead = new StreamReader("Load.txt");
                StreamWriter trainersWrite = new StreamWriter($"{saveName}.txt");
                string fileLoad = LoadRead.ReadLine();
                while (fileLoad != null)
                {
                    string[] tempArray = fileLoad.Split('^');
                    for (int i =0;i<count;i++){
                        trainersWrite.Write(tempArray[i]);
                        trainersWrite.WriteLine();
                    }    
                    fileLoad = LoadRead.ReadLine();
                }
                trainersWrite.Close();
                LoadRead.Close(); 
            }
            else
            {

            }
        }

        static void MoneySort()
        {
            int money = 0;
            Console.Clear();
            Console.WriteLine("Enter minimum cost of a session.");
            int input = int.Parse(Console.ReadLine());
            Console.Clear();
            StreamReader LL = new StreamReader("listings.txt");
            string temp = LL.ReadLine();
            while (temp != null)
            {
                string [] tempArray = temp.Split('#');
                if (int.Parse(tempArray[4]) > input){
                Console.WriteLine($"\t{tempArray[0]}\t\t{tempArray[1]}\t\t{tempArray[2]}\t\t\t{tempArray[3]}\t\t{tempArray[4]}\t{tempArray[5]}"); 
                money += int.Parse(tempArray[4]);
                }
                temp = LL.ReadLine();
            }
            LL.Close();
            Console.WriteLine("Total revenue earned is: $" + money);
            Console.WriteLine("Would you like to save this report?");
            Console.WriteLine("Yes");
            Console.WriteLine("No");
            string answer = Console.ReadLine();
            string lowerAnswer = answer.ToLower();
            if (lowerAnswer == "yes")
            {
                Console.WriteLine("Enter report name.");
                string saveName = Console.ReadLine();
                StreamWriter myReport = new StreamWriter($"{saveName}.txt");
                myReport.Close();
                
                int count =0;
                Console.Clear();
                StreamReader trainers = new StreamReader("listings.txt");
                StreamWriter LoadWrite = new StreamWriter("Load.txt");
                string fileInput = trainers.ReadLine();
                while (fileInput != null)
                {
                    string[] tempArray = fileInput.Split('#');
                    if (int.Parse(tempArray[4]) > input){
                        LoadWrite.Write($"{tempArray[0]} {tempArray[1]} {tempArray[2]} {tempArray[3]} {tempArray[4]} {tempArray[5]}");
                        LoadWrite.Write('^');
                        count++;
                    }
                    else{
                        count++;
                    }
                    fileInput = trainers.ReadLine();
                }
                LoadWrite.Write("==================================");
                LoadWrite.Write('^');
                LoadWrite.Write("Total revenue earned is: $" + money);
                LoadWrite.Write('^');
                count++;
                trainers.Close();
                LoadWrite.Close();
                StreamReader LoadRead = new StreamReader("Load.txt");
                StreamWriter trainersWrite = new StreamWriter($"{saveName}.txt");
                string fileLoad = LoadRead.ReadLine();
                while (fileLoad != null)
                {
                    string[] tempArray = fileLoad.Split('^');
                    for (int i =0;i<count;i++){
                        trainersWrite.Write(tempArray[i]);
                        trainersWrite.WriteLine();
                    }    
                    fileLoad = LoadRead.ReadLine();
                }
                trainersWrite.Close();
                LoadRead.Close(); 
            }
            else
            {

            }
        }

    }
}
