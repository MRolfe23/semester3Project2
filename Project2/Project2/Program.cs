using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Project2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<SuperBowlGames> data = new List<SuperBowlGames>();
            string path = @"C:\Users\rolmicw\Documents\Visual Studio 2017\Projects\Project2\write2File\";
            string pathFile;
            Console.WriteLine("Please name the file you want to create: ");
            pathFile = Console.ReadLine();
            pathFile = pathFile + ".txt";
            path = path + pathFile;
            using (File.Create(path))
            {
                Console.Write("File Created");
            }

            data = readInAllData(path);
            Console.WriteLine("*****TOP FIVE ATTENDENDED SUPER BOWLS*****");
            getTopFiveAttendance(data, path);
            Console.WriteLine("All winning teams of Super Bowl's");
            Console.WriteLine("=================================");
            getAllSuperBowlWinners(data, path);
            Console.WriteLine("State to host the most Super Bowl's");
            Console.WriteLine("===================================");
            getMostHosts(data, path);
            Console.WriteLine("MVP");
            Console.WriteLine("===================================");
            getBestMVPs(data, path);
            Console.WriteLine("Questions to answer");
            Console.WriteLine("===================");
            questions(data, path);
        }

        // Method that reads in all  data and displays all
        public static List<SuperBowlGames> readInAllData(string write)
        {
            List<SuperBowlGames> SuperBowlGame = new List<SuperBowlGames>();
            SuperBowlGames aGame;
            string path = write;
            const char DELIMITER = ',';
            string[] arrayOfData;
            const string FILEPATH = @"C:\Users\rolmicw\Documents\Visual Studio 2017\Projects\Project2\Super_Bowl_Project.csv";
            try
            {
                FileStream file = new FileStream(FILEPATH, FileMode.Open, FileAccess.Read);
                StreamReader read = new StreamReader(file);
                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine("*****ALL SUPER BOWL DATA*****");
                while (!read.EndOfStream)
                {
                    arrayOfData = read.ReadLine().Split(DELIMITER);
                    aGame = new SuperBowlGames(arrayOfData[0], arrayOfData[1], Convert.ToInt32(arrayOfData[2]), arrayOfData[3], arrayOfData[4], arrayOfData[5], Convert.ToInt32(arrayOfData[6]), arrayOfData[7], arrayOfData[8], arrayOfData[9], Convert.ToInt32(arrayOfData[10]), arrayOfData[11], arrayOfData[12], arrayOfData[13], arrayOfData[14]);

                    aGame.Date = arrayOfData[0];
                    aGame.SB = arrayOfData[1];
                    aGame.Attendance = Convert.ToInt32(arrayOfData[2]);
                    aGame.QBWin = arrayOfData[3];
                    aGame.CoachWin = arrayOfData[4];
                    aGame.WinTeam = arrayOfData[5];
                    aGame.WinPoints = Convert.ToInt32(arrayOfData[6]);
                    aGame.QBLose = arrayOfData[7];
                    aGame.CoachLose = arrayOfData[8];
                    aGame.LoseTeam = arrayOfData[9];
                    aGame.LosePoints = Convert.ToInt32(arrayOfData[10]);
                    aGame.MVP = arrayOfData[11];
                    aGame.Stadium = arrayOfData[12];
                    aGame.City = arrayOfData[13];
                    aGame.State = arrayOfData[14];


                    Console.WriteLine(aGame);
                    sw.WriteLine("");
                    sw.WriteLine(aGame);
                    sw.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________");

                }
                sw.Close();
                read.Close();
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
            return (SuperBowlGame);
        }

        public static void getTopFiveAttendance(List<SuperBowlGames> list, string write)
        {
            List<string> date = new List<string>();
            List<string> winner = new List<string>();
            List<string> loser = new List<string>();
            List<string> city = new List<string>();
            List<string> state = new List<string>();
            List<string> stadium = new List<string>();
            List<int> attendance = new List<int>();
            string path = write;
            int x = 0;
            const char DELIMITER = ',';
            string[] arrayOfData;
            const string FILEPATH = @"C:\Users\rolmicw\Documents\Visual Studio 2017\Projects\Project2\Super_Bowl_Project.csv";

            try
            {
                FileStream file = new FileStream(FILEPATH, FileMode.Open, FileAccess.Read);
                StreamReader read = new StreamReader(file);
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine();
                    sw.WriteLine("*****TOP FIVE ATTENDENDED SUPER BOWLS*****");
                }

                while (!read.EndOfStream)
                {
                    arrayOfData = read.ReadLine().Split(DELIMITER);

                    date.Add(arrayOfData[0]);
                    winner.Add(arrayOfData[5]);
                    loser.Add(arrayOfData[9]);
                    city.Add(arrayOfData[13]);
                    state.Add(arrayOfData[14]);
                    attendance.Add(Convert.ToInt32(arrayOfData[2]));

                }
                read.Close();
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var q = attendance.GroupBy(i => i).Select(g => new { Value = g.Key, Count = g.Count() }).OrderByDescending(i => i.Value).Take(5);

            while (x <= 49)
            {

                if (attendance[x] >= 101063)
                {

                    Console.WriteLine("");
                    Console.WriteLine($"Date: " + date[x] + "\n Winner: " + winner[x] + "\n Loser: " + loser[x] + "\n City: " + city[x] + "\n State: " + state[x] + "\n Attendance: " + attendance[x] + "\n\n");
                    
                }
                x++;
            }
            using (StreamWriter sw = File.AppendText(path))
            {
                x = 0;
                while (x <= 49)
                {

                    if (attendance[x] >= 101063)
                    {

                        sw.WriteLine("");
                        sw.WriteLine($"Date: " + date[x] + "\n Winner: " + winner[x] + "\n Loser: " + loser[x] + "\n City: " + city[x] + "\n State: " + state[x] + "\n Attendance: " + attendance[x] + "\n\n");
                        sw.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________");

                    }
                    x++;
                }
            }

                Console.ReadKey();
        }

        // Method to get all winning teams of superbowls
        public static void getAllSuperBowlWinners(List<SuperBowlGames> list, string write)
        {
            List<string> team = new List<string>();
            List<string> year = new List<string>();
            List<string> qb = new List<string>();
            List<string> coach = new List<string>();
            List<string> mvp = new List<string>();
            List<int> ptsDiff = new List<int>();
            string path = write;
            int x = 0;
            const char DELIMITER = ',';
            string[] arrayOfData;
            const string FILEPATH = @"C:\Users\rolmicw\Documents\Visual Studio 2017\Projects\Project2\Super_Bowl_Project.csv";

            try
            {
                FileStream file = new FileStream(FILEPATH, FileMode.Open, FileAccess.Read);
                StreamReader read = new StreamReader(file);
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine("*****ALL SUPER BOWL WINNERS*****");
                }
                
                while (!read.EndOfStream)
                {
                    arrayOfData = read.ReadLine().Split(DELIMITER);

                    team.Add(arrayOfData[5]);
                    year.Add(arrayOfData[0]);
                    qb.Add(arrayOfData[3]);
                    coach.Add(arrayOfData[4]);
                    mvp.Add(arrayOfData[11]);
                    ptsDiff.Add(Convert.ToInt32(arrayOfData[6]) - Convert.ToInt32(arrayOfData[10]));
                    
                    Console.WriteLine($"Team: "+ team[x] +"\nYear: " + year[x] + "\nQuarterback: " + qb[x] + "\nCoach: " + coach[x] + "\nMVP: " + mvp[x] + "\nWon by: " + ptsDiff[x] + "\n\n");
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine("");
                        sw.WriteLine($"Team: " + team[x] + "\n Year: " + year[x] + "\n Quarterback: " + qb[x] + "\n Coach: " + coach[x] + "\n MVP: " + mvp[x] + "\n Won by: " + ptsDiff[x] + "\n\n");
                        sw.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________");

                    }
                    
                    x++;
                }
                read.Close();
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }

        // Method to get states that host the most superbowls
        public static void getMostHosts(List<SuperBowlGames> aGame, string write)
        {
            List<SuperBowlGames> SuperBowlGame = new List<SuperBowlGames>();
            SuperBowlGames aHost;
            List<string> state = new List<string>();
            List<string> city = new List<string>();
            List<string> stadium = new List<string>();
            List<string> date = new List<string>();
            string path = write;
            int x = 0;
            const char DELIMITER = ',';
            string[] arrayOfData;
            const string FILEPATH = @"C:\Users\rolmicw\Documents\Visual Studio 2017\Projects\Project2\Super_Bowl_Project.csv";

            try
            {
                FileStream file = new FileStream(FILEPATH, FileMode.Open, FileAccess.Read);
                StreamReader read = new StreamReader(file);
                
                while (!read.EndOfStream)
                {
                    arrayOfData = read.ReadLine().Split(DELIMITER);
                    aHost = new SuperBowlGames(arrayOfData[0], arrayOfData[1], Convert.ToInt32(arrayOfData[2]), arrayOfData[3], arrayOfData[4], arrayOfData[5], Convert.ToInt32(arrayOfData[6]), arrayOfData[7], arrayOfData[8], arrayOfData[9], Convert.ToInt32(arrayOfData[10]), arrayOfData[11], arrayOfData[12], arrayOfData[13], arrayOfData[14]);

                    state.Add(arrayOfData[14]);
                    city.Add(arrayOfData[13]);
                    stadium.Add(arrayOfData[12]);
                    date.Add(arrayOfData[0]);

                }
                read.Close();
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var q = state.GroupBy(i => i).Select(g => new { Value = g.Key, Count = g.Count() }).OrderByDescending(i => i.Count);
            
            foreach (var i in q)
            {
                Console.WriteLine("Value: " + i.Value + " Count: " + i.Count);
            }
            Console.WriteLine();
            Console.WriteLine("List of the cities in most hosted state");
            Console.WriteLine("=======================================");
            try
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine();
                    sw.WriteLine("*****STATE THAT HAS HOSTED THE MOST SUPER BOWLS*****");
                }
                while (x < 49)
                {
                    if (state[x].Contains("Florida"))
                    {
                        Console.WriteLine("\nCity: " + city[x] + "\n State: " + state[x] + "\n Stadium: " + stadium[x] + "\n Date: " + date[x]);
                        
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine("");
                            sw.WriteLine("\nCity: " + city[x] + "\n State: " + state[x] + "\n Stadium: " + stadium[x] + "\n Date: " + date[x]);
                            sw.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________");
                        }
                    }
                    x++;
                }
            
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            Console.ReadKey();
        }

        // Method for players with most MVPs
        public static void getBestMVPs(List<SuperBowlGames> aGame, string write)
        {
            List<SuperBowlGames> SuperBowlGame = new List<SuperBowlGames>();
            SuperBowlGames aMvp;
            List<string> mvp = new List<string>();
            List<string> winner = new List<string>();
            List<string> loser = new List<string>();
            List<string> date = new List<string>();
            string path = write;
            int x = 0;
            const char DELIMITER = ',';
            string[] arrayOfData;
            const string FILEPATH = @"C:\Users\rolmicw\Documents\Visual Studio 2017\Projects\Project2\Super_Bowl_Project.csv";

            try
            {
                FileStream file = new FileStream(FILEPATH, FileMode.Open, FileAccess.Read);
                StreamReader read = new StreamReader(file);
                
                while (!read.EndOfStream)
                {
                    arrayOfData = read.ReadLine().Split(DELIMITER);
                    aMvp = new SuperBowlGames(arrayOfData[0], arrayOfData[1], Convert.ToInt32(arrayOfData[2]), arrayOfData[3], arrayOfData[4], arrayOfData[5], Convert.ToInt32(arrayOfData[6]), arrayOfData[7], arrayOfData[8], arrayOfData[9], Convert.ToInt32(arrayOfData[10]), arrayOfData[11], arrayOfData[12], arrayOfData[13], arrayOfData[14]);

                    mvp.Add(arrayOfData[11]);
                    winner.Add(arrayOfData[5]);
                    loser.Add(arrayOfData[9]);
                    date.Add(arrayOfData[0]);

                }
                read.Close();
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            var q = mvp.GroupBy(i => i).Select(g => new { Value = g.Key, Count = g.Count() }).OrderByDescending(i => i.Count);

            foreach (var i in q)
            {
                if (i.Count > 2)
                {
                    Console.WriteLine("Value: " + i.Value + " Count: " + i.Count);
                }
            }
            try
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine();
                    sw.WriteLine("*****MVPS THAT HAVE WON MVP MORE THAN TWICE*****");
                }
                
                while (x <= 49)
                {
                    if (mvp[x].Contains("Tom Brady") || mvp[x].Contains("Joe Montana"))
                    {
                        Console.WriteLine("MVP: " + mvp[x] + "\n Winning Team: " + winner[x] + "\n Losing Team: " + loser[x] + "\n Date: " + date[x]);
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine("");
                            sw.WriteLine("MVP: " + mvp[x] + "\n Winning Team: " + winner[x] + "\n Losing Team: " + loser[x] + "\n Date: " + date[x]);
                            sw.WriteLine("______________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________________");
                        }

                    }
                    x++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ReadKey();
        }

        // Method to get coach that lost the most superbowl games
        public static void questions(List<SuperBowlGames> aGame, string write)
        {
            List<SuperBowlGames> SuperBowlGame = new List<SuperBowlGames>();
            SuperBowlGames aHost;
            List<string> loserCoach = new List<string>();
            List<string> winnerCoach = new List<string>();
            List<string> winnerTeam = new List<string>();
            List<string> loserTeam = new List<string>();
            List<int> greatestPtsDiff = new List<int>();
            List<float> avgAttendance = new List<float>();
            List<string> sb = new List<string>();
            string path = write;
            int x = 0;
            const char DELIMITER = ',';
            string[] arrayOfData;
            const string FILEPATH = @"C:\Users\rolmicw\Documents\Visual Studio 2017\Projects\Project2\Super_Bowl_Project.csv";

            try
            {
                FileStream file = new FileStream(FILEPATH, FileMode.Open, FileAccess.Read);
                StreamReader read = new StreamReader(file);
                
                while (!read.EndOfStream)
                {
                    arrayOfData = read.ReadLine().Split(DELIMITER);
                    aHost = new SuperBowlGames(arrayOfData[0], arrayOfData[1], Convert.ToInt32(arrayOfData[2]), arrayOfData[3], arrayOfData[4], arrayOfData[5], Convert.ToInt32(arrayOfData[6]), arrayOfData[7], arrayOfData[8], arrayOfData[9], Convert.ToInt32(arrayOfData[10]), arrayOfData[11], arrayOfData[12], arrayOfData[13], arrayOfData[14]);

                    sb.Add(arrayOfData[1]);
                    avgAttendance.Add(Convert.ToInt32(arrayOfData[2]));
                    winnerCoach.Add(arrayOfData[4]);
                    winnerTeam.Add(arrayOfData[5]);
                    loserCoach.Add(arrayOfData[8]);
                    loserTeam.Add(arrayOfData[9]);
                    greatestPtsDiff.Add(Convert.ToInt32(arrayOfData[6]) - Convert.ToInt32(arrayOfData[10]));

                }
                read.Close();
                file.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            
            try
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine();
                    sw.WriteLine("*****QUESTIONS*****");
                }
                

                var q = loserCoach.GroupBy(i => i).Select(g => new { Value = g.Key, Count = g.Count() }).OrderByDescending(i => i.Count);

                Console.WriteLine();
                Console.WriteLine("Which coach lost the most super bowls?");
                Console.WriteLine("=======================================");
                Console.WriteLine("{0}", q.First());
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine();
                    sw.WriteLine("Which coach lost the most super bowls?");
                    sw.WriteLine("=======================================");
                    sw.WriteLine("{0}", q.First());
                }

                var q2 = winnerCoach.GroupBy(i => i).Select(g => new { Value = g.Key, Count = g.Count() }).OrderByDescending(i => i.Count);

                Console.WriteLine();
                Console.WriteLine("Which coach won the most super bowls?");
                Console.WriteLine("=======================================");
                Console.WriteLine("{0}", q2.First());
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine();
                    sw.WriteLine("Which coach won the most super bowls?");
                    sw.WriteLine("=======================================");
                    sw.WriteLine("{0}", q2.First());
                }

                var q3 = winnerTeam.GroupBy(i => i).Select(g => new { Value = g.Key, Count = g.Count() }).OrderByDescending(i => i.Count);

                Console.WriteLine();
                Console.WriteLine("Which team(s) won the most super bowls?");
                Console.WriteLine("=======================================");
                Console.WriteLine("{0}", q3.First());
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine();
                    sw.WriteLine("Which team(s) won the most super bowls?");
                    sw.WriteLine("=======================================");
                    sw.WriteLine("{0}", q3.First());
                }

                var q4 = loserTeam.GroupBy(i => i).Select(g => new { Value = g.Key, Count = g.Count() }).OrderByDescending(i => i.Count);

                Console.WriteLine();
                Console.WriteLine("Which team(s) lost the most super bowls?");
                Console.WriteLine("========================================");
                Console.WriteLine("{0}", q4.First());
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine();
                    sw.WriteLine("Which team(s) lost the most super bowls?");
                    sw.WriteLine("========================================");
                    sw.WriteLine("{0}", q4.First());
                }

                var q5 = greatestPtsDiff.GroupBy(i => i).Select(g => new { Value = g.Key, Count = g.Max() }).OrderByDescending(i => i.Count);

                Console.WriteLine();
                Console.WriteLine("Which Super bowl had the greatest point difference?");
                Console.WriteLine("===================================================");
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine();
                    sw.WriteLine("Which Super bowl had the greatest point difference?");
                    sw.WriteLine("===================================================");
                }
                
                while (x < 49)
                {
                    if (greatestPtsDiff[x] == 45)
                    {
                        Console.WriteLine("Super Bowl: {1} Point Difference: {0}", q5.First(), sb[x]);
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            sw.WriteLine();
                            sw.WriteLine("Super Bowl: {1} Point Difference: {0}", q5.First(), sb[x]);
                        }
                        
                    }
                    x++;
                }

                Console.WriteLine();
                Console.WriteLine("What is the average attendance of all super bowls?");
                Console.WriteLine("==================================================");
                Console.WriteLine("{0}", avgAttendance.Average());
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine();
                    sw.WriteLine("What is the average attendance of all super bowls?");
                    sw.WriteLine("==================================================");
                    sw.WriteLine("{0}", avgAttendance.Average());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }

    class SuperBowlGames
    {
        public string Date { get; set; }
        public string SB { get; set; }
        public int Attendance { get; set; }
        public string QBWin { get; set; }
        public string CoachWin { get; set; }
        public string WinTeam { get; set; }
        public int WinPoints { get; set; }
        public string QBLose { get; set; }
        public string CoachLose { get; set; }
        public string LoseTeam { get; set; }
        public int LosePoints { get; set; }
        public string MVP { get; set; }
        public string Stadium { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public SuperBowlGames()
        {
            
        }

        public SuperBowlGames(string Date, string SB, int Attendance, string QBWin, string CoachWin, string WinTeam, int WinPoints, string QBLose, string CoachLose, string LoseTeam, int LosePoints, string MVP, string Stadium, string City, string State)
        {
            this.Date = Date;
            this.SB = SB;
            this.Attendance = Attendance;
            this.QBWin = QBWin;
            this.CoachWin = CoachWin;
            this.WinTeam = WinTeam;
            this.WinPoints = WinPoints;
            this.QBLose = QBLose;
            this.CoachLose = CoachLose;
            this.LoseTeam = LoseTeam;
            this.LosePoints = LosePoints;
            this.MVP = MVP;
            this.Stadium = Stadium;
            this.City = City;
            this.State = State;
        }
        public override string ToString()
        {
            return string.Format($"Date: {Date}\n Super Bowl: {SB}\n Attendance: {Attendance}\n Winning Quarterback: {QBWin}\n Winning Couch: {CoachWin}\n Winning Team: {WinTeam}\n Winning Points: {WinPoints}\n Losing Quarterback: {QBLose}\n Losing Couch: {CoachLose}\n Losing Team: {LoseTeam}\n Losing Points: {LosePoints}\n MVP: {MVP}\n Stadium: {Stadium}\n City: {City}\n State: {State}\n\n");
        }
    }
}
