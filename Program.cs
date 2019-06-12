using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Project_Changebot{
    class Program{

        static Random Rnd = new Random();

        static void Main(string[] args){
            /* PSUEDOCODE
             * Make a struct called Kiosk to store and read the remaining coins and bills
             * Make a struct called UserBank to store user amoount entered.
             * Make a loop for user input of item costs, break when user inputs to end loop
             * Display total of items entered
             * Make a loop that breaks when total is <= 0
             * Ask user to insert money of any bills or coin asking how much of each
             * When Total <= 0 loop breaks
             * Calculate change from difference of last amount entered with the difference of the remaining
             * Check to see if Kiosk has enough change
             * IF TRUE Print Change and give it to the user

             * IF FALSE Cancel transaction and refund user and display another form of making payment.

             * Identify a bank card's credit company by looking at the first number.
             * Cards that start with a "3" are American Express. 
             * Those that start with "4" are Visa credit and debit cards, 
             * those that start with "5" are MasterCard credit and debit cards, 
             * and those that start with "6" are Discover credit cards. 
             * The service fee that is charged to merchants varies between card companies. 

             * Count the digits in the credit card number.
             * Most credit cards should contain either 15 or 16 digits. 
             * American Express credit cards contain 15.
             * The other three major credit companies -- Visa, Mastercard and Discover -- have a 16-digit sequence on their cards.*/

            int Hundreds = 10;
            int Fifties = 20;
            int Twenties = 45;
            int Tens = 0;
            int Fives = 0;
            int Twos = 23;
            int OnesB = 150;
            int OnesCoin = 324;
            int FiftyCent = 10;
            int Quarters = 145;
            int Dimes = 200;
            int Nickels = 250;
            int Pennies = 500;

            string ScannedItems = "A";
            decimal ScannedDouble = 0.00m;
            int NumCount = 1; //Numerical count for items
            decimal Total = 0;
            decimal Choice = 0;
            decimal CashSpent = 0;
            int Multi = 0;

            bool Kiosk = Initalize_Till(ref Hundreds, ref Fifties, ref Twenties, ref Tens, ref Fives, ref Twos, ref OnesB, ref OnesCoin, ref FiftyCent, ref Quarters, ref Dimes, ref Nickels, ref Pennies);

            string PaySelect = "";
            string CardNumber = "";
            string Cashback = "";
            decimal Charge = 0.00m;
            decimal CashAmount = 0.00m;

            string [] Temp = new string [5];

            string CashChange = "";
            string CreditSpent = "";
            string Scashchange = "";
            string Screditspent = "";
            string Scashspent = "";
            string Vendor = "";

            Console.Clear();


                #region Scan Prices

                Console.WriteLine("Please scan your items and enter the price (Enter nothing if done)\n\n");

                  while (ScannedItems != ""){//continue allowing items to be entered until done

                      ScannedItems = (prompt("Enter the price for Item " + NumCount));//store item price
                      Console.WriteLine();
                      Console.WriteLine();

                      if (ScannedItems == ""){//check if blank

                      }else{
                        if(double.Parse(ScannedItems) > 0){//make sure item value is not negative then add to total and increment number of items

                            ScannedDouble = decimal.Parse(ScannedItems);
                            ScannedDouble = (Math.Round(ScannedDouble, 2));
                            Total = ScannedDouble + Total;
                            NumCount++;
                        }
                      }//end if
                  }//end while

                #endregion

             Console.Clear();

            //Display total
                Console.WriteLine("The total cost is $" + Total + "\n\n");

            //Display payment options
                Console.WriteLine("1)       Cash\n2)       Credit/Debit\n\n");

            PaySelect = prompt("Which payment type would you like to use?");

            while(Kiosk){

            //If pay is cash
               if (PaySelect == "1"){


                   #region UserPay


                   while (Total > 0){
                         Console.WriteLine("\n\nThe total cost is $" + Total);

                         Console.WriteLine();

                         Console.WriteLine("Please enter your money below.");
                         Choice = decimal.Parse(Console.ReadLine());
                         Multi = 1;
                         Total = Total - (PriceCal(Choice,Multi));
                         Total = Math.Round(Total,2);
                         CashSpent = Choice + CashSpent;
                         CashSpent = Math.Round(CashSpent,2);

                     }//end while

                   #endregion


                   if(Total != 0){

                        string DisplayT = (-Total).ToString("F");
                        CashChange = DisplayT;
                        Console.WriteLine();

                        Console.WriteLine("$ " + DisplayT + " is your change.");

                        Change(Total, ref Hundreds, ref Fifties, ref Twenties, ref Tens, ref Fives, ref Twos, ref OnesB, ref OnesCoin, ref FiftyCent, ref Quarters, ref Dimes, ref Nickels, ref Pennies);
                        Kiosk = false;
                   }else if (Total == 0){
                       Console.WriteLine();

                       Console.WriteLine("No change for you.");
                        Kiosk = false;
                   }//end if


            //if pay is Credit/Debit Cards
               }else if (PaySelect == "2") {

                   CardNumber = prompt("Please enter your card number (no dashes)");

                //check if card is valid
                   if (CardNumber[0] == '3'){// American Express

                       #region AExpress

                        if (CardNumber.Length == 15){

                            Vendor = "American Express";

                              Cashback = prompt("Do you want cashback? Type y for and n for no");
                              if (Cashback.Contains('y')){

                                  Console.WriteLine();

                                  CashAmount = decimal.Parse(prompt("How much cash would you like: "));

                                  Charge = Total + CashAmount; 
                                  Temp = MoneyRequest(CardNumber, Charge);

                                  if(Temp[1] == "declined"){
                                    
                                      Console.WriteLine("Your card has been declined.");

                                      PaySelect = prompt("Would you like to pay in cash(y/n): ");

                                      if (PaySelect.Contains('y')){
                                          PaySelect = "1";
                                      }else{
                                          Console.WriteLine("Have a nice day.");
                                          Kiosk = false;
                                      }

                                  }else if (decimal.Parse(Temp[1]) < Charge){
                                      Console.WriteLine("You have paid: " + (Temp[1]));
                                      CreditSpent = Temp[1];

                                      Console.WriteLine("Your remaining balance is:");
                                      Console.WriteLine("$" + (Charge - decimal.Parse(Temp[1])).ToString("F"));
                                      Total = Charge - decimal.Parse(Temp[1]);

                                      PaySelect = prompt("Would you like to pay in cash(y/n/cancel): ");

                                      if (PaySelect.Contains('y')){
                                          PaySelect = "1";
                                      }else if(PaySelect.First() == 'n'){
                                          Console.WriteLine("Please enter another card. ");
                                          PaySelect = "2";

                                      }else{
                                        Console.WriteLine("Your money will be returned.");
                                        Console.WriteLine("Have a nice day.");
                                           Kiosk = false;
                                      }
                                      
                                      

                                  }else{
                                      CreditSpent = Total.ToString();
                                      Console.WriteLine("Payment Recieved, and your cash is being deposited below.\n\n");
                                      Console.WriteLine("Thank you for shopping with us. :)");
                                      Kiosk = false;


                                  }//end if

                              }else{

                                  Console.WriteLine();

                                  Charge = Total;

                                  Temp = MoneyRequest(CardNumber, Charge);

                                  if (Temp [1] == "declined"){

                                      Console.WriteLine("Your card has been declined.");

                                      PaySelect = prompt("Would you like to pay in cash(y/n): ");

                                      if (PaySelect.Contains('y')){
                                          PaySelect = "1";
                                      }else{
                                          Console.WriteLine("Have a nice day.");
                                          Console.WriteLine();
                                          Console.WriteLine("Your money will be refunded.");
                                          Kiosk = false;
                                      }

                                  }else if(decimal.Parse(Temp[1]) < Charge){

                                      Console.WriteLine("You have paid: " + (Temp[1]));
                                      CreditSpent = Temp[1];

                                      Console.WriteLine("Your remaining balance is:");
                                      Console.WriteLine("$" + (Charge - decimal.Parse(Temp[1])));
                                      Total = Charge - decimal.Parse(Temp[1]);

                                      PaySelect = prompt("Would you like to pay in cash(y/n/cancel): ");

                                      if (PaySelect.Contains('y')){
                                          PaySelect = "1";
                                          Kiosk = true;
                                      }else if(PaySelect.First() == 'n'){
                                          Console.WriteLine("Please enter another card. ");
                                          PaySelect = "2";

                                      }else{
                                        Console.WriteLine("Your money will be returned.");
                                        Console.WriteLine("Have a nice day.");
                                           Kiosk = false;
                                      }

                                  }else{

                                      Console.WriteLine("Thank you for shopping with us. :)");
                                      Kiosk = false;
                                      
                                  }//end if
                              }//end card if

                      }else{
                        Console.WriteLine("Not a valid card");
                      }//end AExpress Validation

                   #endregion

                   }else if (CardNumber[0] == '4'){//Visa

                       #region Visa

                    if (CardNumber.Length == 16){

                            Vendor = "Visa";

                            Cashback = prompt("Do you want cashback? Type y for and n for no");
                              if (Cashback.Contains('y')){

                                  Console.WriteLine();

                                  CashAmount = decimal.Parse(prompt("How much cash would you like: "));

                                  Charge = Total + CashAmount; 
                                  Temp = MoneyRequest(CardNumber, Charge);

                                  if(Temp[1] == "declined"){
                                    
                                      Console.WriteLine("Your card has been declined.");

                                      PaySelect = prompt("Would you like to pay in cash(y/n): ");

                                      if (PaySelect.Contains('y')){
                                          PaySelect = "1";
                                      }else{
                                          Console.WriteLine("Have a nice day.");
                                          Kiosk = false;
                                      }

                                  }else if (decimal.Parse(Temp[1]) < Charge){
                                      Console.WriteLine("You have paid: " + (Temp[1]));
                                      CreditSpent = Temp[1];

                                      Console.WriteLine("Your remaining balance is:");
                                      Console.WriteLine("$" + (Charge - decimal.Parse(Temp[1])).ToString("F"));
                                      Total = Charge - decimal.Parse(Temp[1]);

                                      PaySelect = prompt("Would you like to pay in cash(y/n/cancel): ");

                                      if (PaySelect.Contains('y')){
                                          PaySelect = "1";
                                      }else if(PaySelect.First() == 'n'){
                                          Console.WriteLine("Please enter another card. ");
                                          PaySelect = "2";

                                      }else{
                                        Console.WriteLine("Your money will be returned.");
                                        Console.WriteLine("Have a nice day.");
                                           Kiosk = false;
                                      }
                                      
                                      

                                  }else{
                                      CreditSpent = Total.ToString();
                                      Console.WriteLine("Payment Recieved, and your cash is being deposited below.\n\n");
                                      Console.WriteLine("Thank you for shopping with us. :)");
                                      Kiosk = false;


                                  }//end if

                              }else{

                                  Console.WriteLine();

                                  Charge = Total;

                                  Temp = MoneyRequest(CardNumber, Charge);

                                  if (Temp [1] == "declined"){

                                      Console.WriteLine("Your card has been declined.");

                                      PaySelect = prompt("Would you like to pay in cash(y/n): ");

                                      if (PaySelect.Contains('y')){
                                          PaySelect = "1";
                                      }else{
                                          Console.WriteLine("Have a nice day.");
                                          Console.WriteLine();
                                          Console.WriteLine("Your money will be refunded.");
                                          Kiosk = false;
                                      }

                                  }else if(decimal.Parse(Temp[1]) < Charge){

                                      Console.WriteLine("You have paid: " + (Temp[1]));
                                      CreditSpent = Temp[1];

                                      Console.WriteLine("Your remaining balance is:");
                                      Console.WriteLine("$" + (Charge - decimal.Parse(Temp[1])));
                                      Total = Charge - decimal.Parse(Temp[1]);

                                      PaySelect = prompt("Would you like to pay in cash(y/n/cancel): ");

                                      if (PaySelect.Contains('y')){
                                          PaySelect = "1";
                                          Kiosk = true;
                                      }else if(PaySelect.First() == 'n'){
                                          Console.WriteLine("Please enter another card. ");
                                          PaySelect = "2";

                                      }else{
                                        Console.WriteLine("Your money will be returned.");
                                        Console.WriteLine("Have a nice day.");
                                           Kiosk = false;
                                      }

                                  }else{

                                      Console.WriteLine("Thank you for shopping with us. :)");
                                      Kiosk = false;
                                      
                                  }//end if
                              }//end card if

                    }else{
                        Console.WriteLine("Not a valid card");
                    }//end Visa Validation

                        #endregion

                   }else if (CardNumber[0] == '5') {//MasterCard

                       #region MasterCard

                         if (CardNumber.Length == 16){

                            Vendor = "MasterCard";

                                Cashback = prompt("Do you want cashback? Type y for and n for no");
                              if (Cashback.Contains('y')){

                                  Console.WriteLine();

                                  CashAmount = decimal.Parse(prompt("How much cash would you like: "));

                                  Charge = Total + CashAmount; 
                                  Temp = MoneyRequest(CardNumber, Charge);

                                  if(Temp[1] == "declined"){
                                    
                                      Console.WriteLine("Your card has been declined.");

                                      PaySelect = prompt("Would you like to pay in cash(y/n): ");

                                      if (PaySelect.Contains('y')){
                                          PaySelect = "1";
                                      }else{
                                          Console.WriteLine("Have a nice day.");
                                          Kiosk = false;
                                      }

                                  }else if (decimal.Parse(Temp[1]) < Charge){
                                      Console.WriteLine("You have paid: " + (Temp[1]));
                                      CreditSpent = Temp[1];

                                      Console.WriteLine("Your remaining balance is:");
                                      Console.WriteLine("$" + (Charge - decimal.Parse(Temp[1])).ToString("F"));
                                      Total = Charge - decimal.Parse(Temp[1]);

                                      PaySelect = prompt("Would you like to pay in cash(y/n/cancel): ");

                                      if (PaySelect.Contains('y')){
                                          PaySelect = "1";
                                      }else if(PaySelect.First() == 'n'){
                                          Console.WriteLine("Please enter another card. ");
                                          PaySelect = "2";

                                      }else{
                                        Console.WriteLine("Your money will be returned.");
                                        Console.WriteLine("Have a nice day.");
                                           Kiosk = false;
                                      }
                                      

                                  }else{
                                      CreditSpent = Total.ToString();
                                      Console.WriteLine("Payment Recieved, and your cash is being deposited below.\n\n");
                                      Console.WriteLine("Thank you for shopping with us. :)");
                                      Kiosk = false;


                                  }//end if

                              }else{

                                  Console.WriteLine();

                                  Charge = Total;

                                  Temp = MoneyRequest(CardNumber, Charge);

                                  if (Temp [1] == "declined"){

                                      Console.WriteLine("Your card has been declined.");

                                      PaySelect = prompt("Would you like to pay in cash(y/n): ");

                                      if (PaySelect.Contains('y')){
                                          PaySelect = "1";
                                      }else{
                                          Console.WriteLine("Have a nice day.");
                                          Console.WriteLine();
                                          Console.WriteLine("Your money will be refunded.");
                                          Kiosk = false;
                                      }

                                  }else if(decimal.Parse(Temp[1]) < Charge){

                                      Console.WriteLine("You have paid: " + (Temp[1]));
                                      CreditSpent = Temp[1];

                                      Console.WriteLine("Your remaining balance is:");
                                      Console.WriteLine("$" + (Charge - decimal.Parse(Temp[1])));
                                      Total = Charge - decimal.Parse(Temp[1]);

                                      PaySelect = prompt("Would you like to pay in cash(y/n/cancel): ");

                                      if (PaySelect.Contains('y')){
                                          PaySelect = "1";
                                          Kiosk = true;
                                      }else if(PaySelect.First() == 'n'){
                                          Console.WriteLine("Please enter another card. ");
                                          PaySelect = "2";

                                      }else{
                                        Console.WriteLine("Your money will be returned.");
                                        Console.WriteLine("Have a nice day.");
                                           Kiosk = false;
                                      }

                                  }else{

                                      Console.WriteLine("Thank you for shopping with us. :)");
                                      Kiosk = false;
                                      
                                  }//end if
                              }//end card if

                         }else{
                             Console.WriteLine("Not a valid card");
                         }//end Master Validation

                    #endregion

                   }else if (CardNumber[0] == '6') {//Discover

                       #region Discover

                         if (CardNumber.Length == 16){

                            Vendor = "Discover";

                                 Cashback = prompt("Do you want cashback? Type y for and n for no");
                               if (Cashback.Contains('y')){

                                   Console.WriteLine();

                                   CashAmount = decimal.Parse(prompt("How much cash would you like: "));

                                   Charge = Total + CashAmount; 
                                   Temp = MoneyRequest(CardNumber, Charge);

                                   if(Temp[1] == "declined"){
                                     
                                       Console.WriteLine("Your card has been declined.");

                                       PaySelect = prompt("Would you like to pay in cash(y/n): ");

                                       if (PaySelect.Contains('y')){
                                           PaySelect = "1";
                                       }else{
                                           Console.WriteLine("Have a nice day.");
                                           Kiosk = false;
                                       }

                                   }else if (decimal.Parse(Temp[1]) < Charge){
                                       Console.WriteLine("You have paid: " + (Temp[1]));
                                       CreditSpent = Temp[1];

                                       Console.WriteLine("Your remaining balance is:");
                                       Console.WriteLine("$" + (Charge - decimal.Parse(Temp[1])).ToString("F"));
                                       Total = Charge - decimal.Parse(Temp[1]);

                                       PaySelect = prompt("Would you like to pay in cash(y/n/cancel): ");

                                      if (PaySelect.Contains('y')){
                                           PaySelect = "1";
                                      }else if(PaySelect.First() == 'n'){
                                          Console.WriteLine("Please enter another card. ");
                                          PaySelect = "2";

                                      }else{
                                        Console.WriteLine("Your money will be returned.");
                                        Console.WriteLine("Have a nice day.");
                                           Kiosk = false;
                                      }
                                       
                                       

                                   }else{
                                       CreditSpent = Total.ToString();
                                       Console.WriteLine("Payment Recieved, and your cash is being deposited below.\n\n");
                                       Console.WriteLine("Thank you for shopping with us. :)");
                                       Kiosk = false;


                                   }//end if

                               }else{

                                   Console.WriteLine();

                                   Charge = Total;

                                   Temp = MoneyRequest(CardNumber, Charge);

                                   if (Temp [1] == "declined"){

                                       Console.WriteLine("Your card has been declined.");

                                       PaySelect = prompt("Would you like to pay in cash(y/n): ");

                                       if (PaySelect.Contains('y')){
                                           PaySelect = "1";
                                       }else{
                                           Console.WriteLine("Have a nice day.");
                                           Console.WriteLine();
                                           Console.WriteLine("Your money will be refunded.");
                                           Kiosk = false;
                                       }

                                   }else if(decimal.Parse(Temp[1]) < Charge){

                                       Console.WriteLine("You have paid: " + (Temp[1]));
                                       CreditSpent = Temp[1];

                                       Console.WriteLine("Your remaining balance is:");
                                       Console.WriteLine("$" + (Charge - decimal.Parse(Temp[1])));
                                       Total = Charge - decimal.Parse(Temp[1]);

                                       PaySelect = prompt("Would you like to pay in cash(y/n/cancel): ");

                                      if (PaySelect.Contains('y')){
                                           PaySelect = "1";
                                           Kiosk = true;
                                      }else if(PaySelect.First() == 'n'){
                                          Console.WriteLine("Please enter another card. ");
                                          PaySelect = "2";

                                      }else{
                                        Console.WriteLine("Your money will be returned.");
                                        Console.WriteLine("Have a nice day.");
                                           Kiosk = false;
                                      }

                                   }else{

                                       Console.WriteLine("Thank you for shopping with us. :)");
                                       Kiosk = false;
                                       
                                   }//end if
                               }//end card if

                         }else{
                              Console.WriteLine("Not a valid card");
                         }//end discover validation

                      #endregion

                    }//end card selection if


                }else{

                    //do nothing if input is invalid

                }//end pay select if
            }//end while

            Scashspent = CashSpent.ToString();
            Scashchange = CashChange.ToString();
            Screditspent = CreditSpent.ToString();


            /*ProcessStartInfo startInfo =    new ProcessStartInfo();
            startInfo.FileName = @"C:\Users\CCA010\source\repos\Project_Changebot_TransactionLog\bin\Debug\Project_Changebot_TransactionLog.exe";
            startInfo.Arguments = Scashspent + "\t" + Vendor + "\t" + Screditspent + "\t" + Scashchange;
            Process.Start(startInfo);*/

            Console.WriteLine("Cash Spent:  $" + CashSpent + "\n\n" + "Card Vendor:   " + Vendor + "\n\nCard Money Spent:   $" + (CreditSpent) + "\n\n" + "Change Given:   $" + CashChange);

            Console.ReadKey();

        }//end main


        static string prompt(string msg){
            Console.WriteLine(msg + " ");
            return Console.ReadLine();
        }

        #region Price
        static decimal PriceCal (decimal Choice, int Multiple){

                 decimal Result = 0;
            do{

                 if (Choice >= 100.00m && !((Choice - Result) <= 100.00m)){
                     Result = Result +100;
                 }else if (Choice >= 50.00m && !((Choice - Result) <= 50.00m)){
                     Result = Result + 50;
                 }else if (Choice >= 20.00m  &&  !((Choice - Result) <= 20.00m)){
                     Result = Result + 20;
                 }else if (Choice >= 10.00m && !((Choice - Result) <= 10.00m)){
                     Result = Result + 10;
                 }else if (Choice >= 5.00m && !((Choice - Result) <= 5.00m)){
                     Result = Result + 5;
                 }else if (Choice >= 2.00m && !((Choice - Result) <= 2.00m)){
                     Result = Result + 2;
                 }else if (Choice >= 1.00m && !((Choice - Result) <= 1.00m)){
                     Result = Result + 1;
                 }else if (Choice >= 0.50m && !((Choice - Result) <= 0.50m)){
                     Result = Result + 0.50m;
                 }else if (Choice >= 0.25m && !((Choice - Result) <= 0.25m)){
                     Result = Result + 0.25m;
                 }else if (Choice >= 0.10m && !((Choice - Result) <= 0.10m)){
                     Result = Result + 0.10m;
                 }else if (Choice >= 0.05m && !((Choice - Result) <= 0.05m)){
                     Result = Result + 0.05m;
                 }else if (Choice >= 0.01m && !((Choice - Result) <= 0.01m) || (Choice - Result) < .04m){
                     Result = Result + 0.01m;
                 }//end if

                 Result = Math.Round(Result, 2);

            }while(Result < Choice);

            return Result;
            
        }//end price cal function
        #endregion

        #region Initialize

        static bool Initalize_Till(ref int Hundreds, ref  int Fifties, ref int Twenties, ref int Tens, ref int Fives, ref int Twos, ref int OnesB, ref int OnesCoin, ref int FiftyCent, ref int Quarters, ref int Dimes, ref int Nickels, ref int Pennies){
            //add code to initialize the till
                
                   Hundreds = 100;
                   Fifties = 100;
                   Twenties = 100;
                   Tens = 100;
                   Twos = 100;
                   OnesB = 1000;
                   OnesCoin = 1000;
                   FiftyCent = 1000;
                   Quarters = 1000;
                   Dimes = 10000;
                   Nickels = 10000;
                   Pennies = 10000;
            return true;
        }//end initialize till funct

        #endregion

        #region Change
        static void Change (decimal Change, ref int Hundreds, ref  int Fifties, ref int Twenties, ref int Tens, ref int Fives, ref int Twos, ref int OnesB, ref int OnesCoin, ref int FiftyCent, ref int Quarters, ref int Dimes, ref int Nickels, ref int Pennies){

            int HundredsCount = 0;
            int FiftiesCount = 0;
            int TwentiesCount = 0;
            int TensCount = 0;
            int FivesCount = 0;
            int TwosCount = 0;
            int OnesBCount = 0;
            int OnesCoinCount = 0;
            int FiftyCentCount = 0;
            int QuartersCount = 0;
            int DimesCount = 0;
            int NickelsCount = 0;
            int PenniesCount = 0;

            if (Change == 0){

                Console.WriteLine("Thank you for paying with exact change! :)");

            }//end if

            while((Change != 0) && !(Change > 0)){
            
                    if((Change <= -100) && (Hundreds != 0)){
                        
                        Change = Change += 100;
                        HundredsCount++;
                        
                    }else if ((Change <= -50) && (Fifties != 0)){

                        Change = Change += 50;
                        FiftiesCount++;

                    }else if ((Change <= -20) && (Twenties != 0)){

                        Change = Change += 20;
                        TwentiesCount++;

                    }else if ((Change <= -10) && (Tens != 0)){

                        Change = Change += 10;
                        TensCount++;

                    }else if ((Change <= -5) && (Fives != 0)){

                        Change = Change += 5;
                        FivesCount++;

                    }else if ((Change <= -2) && (Twos != 0)){

                        Change = Change += 2;
                        TwosCount++;

                    }else if ((Change <= -1) && (OnesB != 0) || (Change <= -1) && (OnesCoin != 0)){

                          if(OnesB >= 500){
                          
                              Change = Change += 1;
                              OnesCoinCount++;
                          
                          }else{ 

                              Change = Change += 1;
                              OnesBCount++;

                          }//end nested if

                    }else if (Change <= -.5m && FiftyCent != 0){

                        Change = Change += .5m;
                        FiftyCentCount++;

                    }else if (Change <= -.25m && Quarters != 0){

                        Change = Change += .25m;
                        QuartersCount++;

                    }else if (Change <= -.1m && Dimes != 0){

                        Change = Change += .1m;
                        DimesCount++;

                    }else if (Change <= -.05m && Nickels != 0){

                        Change = Change += .05m;
                        NickelsCount++;

                    }else if (Change <= -.01m && Pennies != 0){

                        Change = Change += .01m;
                        PenniesCount++;

                    }//end count if

                    Change = Math.Round(Change, 2);
            }//end while

            Console.WriteLine("Your change is: ");

            while (HundredsCount != 0 || FiftiesCount != 0 || TwentiesCount != 0 || TensCount != 0 || FivesCount != 0 || TwosCount != 0 || OnesBCount != 0 || OnesCoinCount != 0 || FiftyCentCount != 0 || QuartersCount != 0 || DimesCount != 0 || NickelsCount != 0 || PenniesCount != 0){
                if (HundredsCount > 0){
                    Console.WriteLine(HundredsCount + " Hundreds");
                        HundredsCount = 0;
                }else if (FiftiesCount > 0){
                    Console.WriteLine(FiftiesCount + " Fifties");
                        FiftiesCount = 0;
                }else if (TwentiesCount > 0){
                    Console.WriteLine(TwentiesCount + " Twenties");
                        TwentiesCount = 0;
                }else if (TensCount > 0){
                    Console.WriteLine(TensCount + " Tens");
                        TensCount = 0;
                }else if (FivesCount > 0){
                    Console.WriteLine(FivesCount + " Fives");
                        FivesCount = 0;
                }else if (TwosCount > 0){
                    Console.WriteLine(TwosCount + " Twos");
                        TwosCount = 0;
                }else if (OnesBCount > 0){
                    Console.WriteLine(OnesBCount + " One dollar bills");
                        OnesBCount = 0;
                }else if (OnesCoinCount > 0){
                    Console.WriteLine(OnesCoinCount + " One dollar coins");
                        OnesCoinCount = 0;
                }else if (FiftyCentCount > 0){
                    Console.WriteLine(FiftyCentCount + " Half dollars");
                        FiftyCentCount = 0;
                }else if (QuartersCount > 0){
                    Console.WriteLine(QuartersCount + " Quarters");
                        QuartersCount = 0;
                }else if (DimesCount > 0){
                    Console.WriteLine(DimesCount + " Dimes");
                        DimesCount = 0;
                }else if (NickelsCount > 0){
                    Console.WriteLine(NickelsCount + " Nickels");
                        NickelsCount = 0;
                }else if (PenniesCount > 0){
                    Console.WriteLine(PenniesCount + " Pennies");
                        PenniesCount = 0;
                }//end if

            }//end output while


        }//end change function

        #endregion

        static string[] MoneyRequest (string account_number, decimal amount){
            Random rnd = new Random();
            //50% to pass IF PASS
            bool Pass = rnd.Next(100) < 50;
            //50% to fail
            bool Declined = rnd.Next(100) < 50;

            if (Pass){
                return new string[] {account_number, amount.ToString()}; //Have money to pay
            }else{
                if (!Declined){
                    return new string[] { account_number, (amount / rnd.Next(2,6)).ToString("F")}; //Can pay part of it
                }else{
                    return new string[] {account_number, "declined"}; //No money at all
                }//end if
            }//end if
        }//end MoneyRequest

    }//end class
}//end main