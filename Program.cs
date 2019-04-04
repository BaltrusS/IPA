using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IPA
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var Input = "";

            Console.WriteLine("Iveskite duomenu ivedimo tipa:");
            Console.WriteLine("1 - duomenu ivedimas is failo");
            Console.WriteLine("2 - duomenu ivedimas konsoleje");
            Console.WriteLine("3 - efektyvumas");

            while (true)
            {
                Input = Console.ReadLine();

                if (Input.Equals("1"))
                {
                    InputByFile();
                    break;
                }

                if (Input.Equals("2"))
                {
                    InputByConsole();
                    break;
                }
                
                if (Input.Equals("3"))
                {
                    //setupFiles();
                    groupToFiles();
                    break;
                }

                Console.WriteLine("Bad input, karotkite!");
            }
        }

        public static void setupFiles()
        {
            string fileLoc = @"C:\Users\Baltrus\Documents\IPA\IPA\";
            int amountPrefix = 1;
            Random rnd = new Random();
                
            try    
            {
                for (int i = 0; i < 5; i++)
                {
                    amountPrefix = amountPrefix * 10;
                    string fileName = fileLoc + amountPrefix + "students.txt";
                    Console.WriteLine(fileName);
                    // Check if file already exists. If yes, delete it.     
                    if (File.Exists(fileName))
                    {
                        File.Delete(fileName);
                    }
                    
                    StringBuilder studentsBuilder = new StringBuilder();
                    for (int j = 1; j <= amountPrefix; j++)
                    {
                        studentsBuilder.Append("Vardas"+j+" Pavarde"+j+" "
                                               +rnd.Next(1,11)+" "
                                               +rnd.Next(1,11)+" "
                                               +rnd.Next(1,11)+" "
                                               +rnd.Next(1,11)+" "
                                               +rnd.Next(1,11)+" "
                                               +rnd.Next(1,11)+"\n"
                                               );
                    }
                    
                    // Create a new file     
                    using (FileStream fs = File.Create(fileName))
                    {
                        // Add some text to file    
                        Byte[] title = new UTF8Encoding(true).GetBytes(studentsBuilder.ToString());
                        fs.Write(title, 0, title.Length);
                    }
                }
            }    
            catch (Exception Ex)    
            {    
                Console.WriteLine("Unable to create files");
            }
        }
        
        private static void groupToFiles()
        {
            string[] fileInput;
            var vargsiukai = new List<Student>();
            var galvoti = new List<Student>();
            string vargsiukaiFile = @"C:\Users\Baltrus\Documents\IPA\IPA\vargsiukai.txt";
            string galvotiFile = @"C:\Users\Baltrus\Documents\IPA\IPA\galvoti.txt";

            try
            {
                fileInput = File.ReadAllLines(
                    @"C:\Users\Baltrus\Documents\IPA\IPA\100000students.txt");
                foreach (var line in fileInput)
                {
                    var student = GetStudentData(true, line);
                    if (student.AvgResult>=5)
                    {
                        galvoti.Add(student);
                    }
                    else
                    {
                        vargsiukai.Add(student);
                    }
                }
            }
            catch
            {
                Console.WriteLine("-----------------!!!!!!!!!-----------------");
                Console.WriteLine("Nepavyko nuskaityti failo. Baigiamas darbas");
            }
            
            try    
            {    
                // Check if file already exists. If yes, delete it.     
                if (File.Exists(vargsiukaiFile) || File.Exists(galvotiFile))    
                {    
                    File.Delete(vargsiukaiFile);
                    File.Delete(galvotiFile);
                }    
    
                StringBuilder stringToFile = new StringBuilder();
                foreach (var student in vargsiukai)
                {
                    stringToFile.Append(student.ToString());
                }
                
                using (FileStream fs = File.Create(vargsiukaiFile))     
                {    
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes(stringToFile.ToString());
                    fs.Write(title, 0, title.Length);       
                }

                stringToFile.Clear();
                foreach (var student in galvoti)
                {
                    stringToFile.Append(student.ToString());
                }
                
                using (FileStream fs = File.Create(galvotiFile))
                {    
                    // Add some text to file    
                    Byte[] title = new UTF8Encoding(true).GetBytes(stringToFile.ToString());
                    fs.Write(title, 0, title.Length);
                }
            }    
            catch (Exception Ex)    
            {    
                Console.WriteLine("Nepavyko irasyti failu... baigiamas darbas");    
            }
            
            Console.WriteLine("Vargsiuku: "+vargsiukai.Count);
            Console.WriteLine("Galvotu: "+galvoti.Count);
        }

        public static void InputByConsole()
        {
            var continueInput = true;
            var students = new List<Student>();

            while (continueInput)
            {
                Console.Write("Prideti studenta? Y/N : ");
                continueInput = Console.ReadLine().ToLower().Equals("y");

                if (continueInput)
                {
                    var student = GetStudentData(false, null);
                    students.Add(student);
                }
            }

            StudentSort(students);
            Console.ReadKey();
        }

        public static void InputByFile()
        {
            string[] fileInput;
            var students = new List<Student>();

            try
            {
                fileInput = File.ReadAllLines(
                    @"C:\Users\Baltrus\Documents\IPA\IPA\students.txt");
                foreach (var line in fileInput)
                {
                    var student = GetStudentData(true, line);
                    students.Add(student);
                }

                StudentSort(students);
                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("----------------------!!!!!!!!!------------------------");
                Console.WriteLine("Nepavyko nuskaityti failo. Duomenis veskite per konsole");
                InputByConsole();
            }
        }

        public static void StudentSort(List<Student> students)
        {
            var sortedStudents = new List<Student>();
            if (students.Any())
            {
                sortedStudents = students.OrderBy(o => o.Name).ToList();
                StudentsTable(sortedStudents);
            }
        }

        public static Student GetStudentData(bool isInputFromFile, string line)
        {
            string name;
            string surname;
            string input;
            string[] inputLine = {""};

            var testResult = 0;

            double medianResult = 0, avgResult = 0;

            var isAvgSelected = true;
            var generateNumbers = false;

            var homeWorkResults = new List<int>();

            if (isInputFromFile && line != null) inputLine = line.Split(' ');

            /* Vardo ivedimas */
            name = isInputFromFile ? inputLine[0] : GetStudentName();


            /* Pavardes ivedimas */
            surname = isInputFromFile ? inputLine[1] : GetStudentSurname();

            /* Namu darbu rezultatu ivedimas */
            if (isInputFromFile)
            {
                for (var i = 2; i < inputLine.Length - 1; i++)
                    try
                    {
                        homeWorkResults.Add(int.Parse(inputLine[i]));
                    }
                    catch
                    {
                        Console.WriteLine("{0} {1} namu darbo {2} rezultatas ivestas netinkamai!", name, surname,
                            i - 1);
                        Console.WriteLine("Exiting...");
                        Environment.Exit(1);
                    }
            }
            else
            {
                Console.Write("Ar sugeneruoti balus? Y/N: ");

                input = Console.ReadLine();
                generateNumbers = input.ToLower().Equals("y");

                homeWorkResults = GetStudentHomeWorkSum(generateNumbers);
            }


            /* Egzamino rezultato ivedimas */
            if (isInputFromFile)
                try
                {
                    testResult = int.Parse(inputLine[inputLine.Length - 1]);
                }
                catch
                {
                    Console.WriteLine("{0} {1} egzamino rezultatas netinkamai ivestas!", name, surname);
                    Console.WriteLine("Programa baigia darba.");
                    Environment.Exit(1);
                }
            else
                testResult = GetStudentTestResult(generateNumbers);

            /* Vidurkio skaiciavimas */
            if (isInputFromFile)
            {
                avgResult = 0.3 * GetStudentAvgHwResult(homeWorkResults) + 0.7 * testResult;
                medianResult = 0.3 * GetStudentMedianHwResult(homeWorkResults) + 0.7 * testResult;
            }
            else
            {
                isAvgSelected = GetStudentChoiceOfAvg();
                if (isAvgSelected)
                    avgResult = 0.3 * GetStudentAvgHwResult(homeWorkResults) + 0.7 * testResult;
                else
                    medianResult = 0.3 * GetStudentMedianHwResult(homeWorkResults) + 0.7 * testResult;
            }

            var stud = new Student(name, surname, avgResult, medianResult, isInputFromFile, isAvgSelected);
            return stud;
        }

        public static string GetStudentName()
        {
            string name;
            while (true)
            {
                Console.Write("Iveskite studento varda: ");
                name = Console.ReadLine();
                if (name.Length != 0) break;
            }

            return name;
        }

        public static string GetStudentSurname()
        {
            string surname;
            while (true)
            {
                Console.Write("Iveskite studento pavarde: ");
                surname = Console.ReadLine();
                if (surname.Length != 0) break;
            }

            return surname;
        }

        public static List<int> GetStudentHomeWorkSum(bool generateNumbers)
        {
            var continueInput = true;

            var homeWorkResults = new List<int>();
            var random = new Random();


            if (!generateNumbers) Console.WriteLine("Iveskite namu darbu rezultatus (1-10): ");

            while (continueInput)
            {
                while (true)
                {
                    int homeWorkVal;

                    if (generateNumbers)
                    {
                        homeWorkResults.Add(random.Next(0, 11));
                        Console.Write("Sugeneruotas rezultatas: {0}\n", homeWorkResults[homeWorkResults.Count - 1]);
                        break;
                    }

                    Console.Write("Iveskite {0} namu darbo pazymi: ", homeWorkResults.Count() + 1);

                    if (!int.TryParse(Console.ReadLine(), out homeWorkVal))
                    {
                        Console.WriteLine("Turite ivesti skaiciu!");
                    }
                    else if (homeWorkVal < 0 || homeWorkVal > 10)
                    {
                        Console.WriteLine("Galimi reziai 1-10, pakartokite!");
                    }
                    else
                    {
                        homeWorkResults.Add(homeWorkVal);
                        break;
                    }
                }

                Console.Write("Ar norite testi namu darbu ivedima? Y/N : ");
                continueInput = Console.ReadLine().ToLower().Equals("y");
            }

            return homeWorkResults;
        }

        public static int GetStudentTestResult(bool generateNumbers)
        {
            int testResult;
            var random = new Random();

            Console.WriteLine();
            if (!generateNumbers)
            {
                Console.Write("Egzamino pazymis: ");

                if (!int.TryParse(Console.ReadLine(), out testResult))
                    Console.WriteLine("Turite ivesti skaiciu!");
                else if (testResult < 0 || testResult > 10) Console.WriteLine("Galimi reziai 1-10, pakartokite!");
            }
            else
            {
                testResult = random.Next(0, 11);
                Console.Write("Sugeneruotas egzamino rezultatas: {0}\n", testResult);
            }

            return testResult;
        }

        public static bool GetStudentChoiceOfAvg()
        {
            string input;
            bool isAvgSelected;

            while (true)
            {
                Console.Write("Skaiciavimui naudoti vidurki ar mediana? V/M : ");
                input = Console.ReadLine();
                if (input.ToLower().Equals("v"))
                {
                    isAvgSelected = true;
                    break;
                }

                if (input.ToLower().Equals("m"))
                {
                    isAvgSelected = false;
                    break;
                }

                Console.WriteLine("Negalimas simbolis, pakartokite!");
            }

            return isAvgSelected;
        }

        public static double GetStudentAvgHwResult(List<int> homeWorkResults)
        {
            return homeWorkResults.Average();
        }

        public static double GetStudentMedianHwResult(List<int> homeWorkResults)
        {
            double medianHWResult;

            var ys = homeWorkResults.OrderBy(x => x).ToList();
            var mid = (ys.Count() - 1) / 2.0;
            medianHWResult = (ys[(int) mid] + ys[(int) (mid + 0.5)]) / 2;

            return medianHWResult;
        }

        public static void StudentsTable(List<Student> students)
        {
            var tableName = "Vardas";
            var tableSurname = "Pavarde";
            var tableAvg = "Galutinis (Vid.)";
            var tableMed = "Galutinis (Med.)";
            var tempS = "/";
            var defaultOffset = 6;
            var columnVardasLenght = 6; // For longest name
            var columnPavardeLength = 7; // For longest surname

            foreach (var student in students)
            {
                if (student.Name.Length > columnVardasLenght) columnVardasLenght = student.Name.Length;
                if (student.Surname.Length > columnPavardeLength) columnPavardeLength = student.Surname.Length;
            }

            /* Column names */
            Console.WriteLine("{0}{1}{2}{3}{4}",
                FormatSpaces(tableName, ' ', Math.Abs(columnVardasLenght - tableName.Length) + defaultOffset),
                FormatSpaces(tableSurname, ' ', Math.Abs(columnPavardeLength - tableSurname.Length) + defaultOffset),
                FormatSpaces(tableAvg, ' ', defaultOffset / 2),
                FormatSpaces(tempS, ' ', defaultOffset / 2),
                tableMed);

            Console.WriteLine(FormatSpaces("", '-',
                columnVardasLenght + columnPavardeLength + 3 * defaultOffset + tableAvg.Length + tableMed.Length +
                tempS.Length));

            /* Results */
            foreach (var student in students)
            {
                var columnNameOffset = columnVardasLenght - student.Name.Length + defaultOffset;
                var columnSurnameOffset = columnPavardeLength - student.Surname.Length + defaultOffset +
                                          (tableAvg.Length - student.AvgResult.ToString().Length - 3) + 2;
                Console.Write("{0}{1}",
                    FormatSpaces(student.Name, ' ', columnNameOffset),
                    FormatSpaces(student.Surname, ' ', columnSurnameOffset));

                if (student.IsInputFromFile)
                {
                    var columnAvgResultOffset = "   " + student.AvgResult;
                    Console.WriteLine("{0:F2} {1} {2:F2}", student.AvgResult, FormatSpaces("", ' ',
                            defaultOffset + tempS.Length + tableMed.Length - columnAvgResultOffset.Length),
                        student.MedianResult);
                }
                else
                {
                    Console.WriteLine("{0} {1}", student.IsAvgSelected
                            ? $"{student.AvgResult:F2}"
                            : FormatSpaces("", ' ', defaultOffset + tempS.Length + tableMed.Length),
                        !student.IsAvgSelected ? $"{student.AvgResult:F2}" : FormatSpaces("", ' ', tableMed.Length));
                }
            }
        }

        private static string FormatSpaces(string w, char c, int n) // Counting needed spaces
        {
            return w + new string(c, n);
        }
    }
}
//Comments for extra commits