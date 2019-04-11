using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace IPA
{
    internal class Program
    {
        public static string[] TIPAI = new[] {
            "LIST",
            "QUEUE",
            "LINKEDLIST"
        };
        
        public static string[] FILES = new[] {
            @"C:\Users\Baltrus\Documents\IPA\IPA\10studentu.txt",
            @"C:\Users\Baltrus\Documents\IPA\IPA\100studentu.txt",
            @"C:\Users\Baltrus\Documents\IPA\IPA\1000studentu.txt",
            @"C:\Users\Baltrus\Documents\IPA\IPA\10000studentu.txt",
            @"C:\Users\Baltrus\Documents\IPA\IPA\100000studentu.txt"
        };
        
        public static void Main(string[] args)
        {
            var select = "";

            Console.WriteLine("Iveskite duomenu ivedimo tipa:");
            Console.WriteLine("1 - duomenu ivedimas is failo");
            Console.WriteLine("2 - duomenu ivedimas konsoleje");
            Console.WriteLine("3 - generuoti failus");
            Console.WriteLine("4 - rusiuoti i failus");
            Console.WriteLine("5 - skaiciuoti laika");
            Console.WriteLine("6 - testutoi konteinerius");

            while (true)
            {
                select = Console.ReadLine();

                if (select.Equals("1"))
                {
                    ReadFromFile();
                    break;
                }

                if (select.Equals("2"))
                {
                    ReadFromConsole();
                    break;
                }
                
                if (select.Equals("3"))
                {
                    setupFiles();
                    break;
                }

                if (select.Equals("4"))
                {
                    groupToFiles(FILES[4]);
                    break;
                }
                
                if (select.Equals("5"))
                {
                    //SpeedAnalysis();
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
                    string fileName = fileLoc + amountPrefix + "studentu.txt";
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
        
        private static void groupToFiles(string filePath, bool noLogging = false, bool withOutput = true, string TYPE = "LIST")
        {
            IEnumerable<Student> vargsiukaiEnum;
            IEnumerable<Student> galvotiEnum;
            var vargsiukai = new List<Student>();
            var galvoti = new List<Student>();
            var vargsiukaiQueue = new Queue<Student>();
            var galvotiQueue = new Queue<Student>();
            var vargsiukaiLinkedList = new LinkedList<Student>();
            var galvotiLinkedList = new LinkedList<Student>();
            
            const string galvotiFile = @"C:\Users\Baltrus\Documents\IPA\IPA\galvoti.txt";
            const string vargsiukaiFile = @"C:\Users\Baltrus\Documents\IPA\IPA\vargsiukai.txt";
            
            try {
                var fileInput = System.IO.File.ReadAllLines(filePath);
                
                foreach (var line in fileInput) {
                    var student = CreationOfStudent(true, line);
                    
                    switch (TYPE) {
                        case "LIST":
                            if (student.AvgResult >= 5) {
                                galvoti.Add(student);
                            } else {
                                vargsiukai.Add(student);
                            }
                            break;
                        case "QUEUE":
                            if (student.AvgResult >= 5) {
                                galvotiQueue.Enqueue(student);
                            } else {
                                vargsiukaiQueue.Enqueue(student);
                            }
                            break;
                        case "LINKEDLIST":
                            if (student.AvgResult >= 5) {
                                galvotiLinkedList.AddLast(student);
                            } else {
                                vargsiukaiLinkedList.AddLast(student);
                            }
                            break;
                        default:
                            if (student.AvgResult >= 5) {
                                galvoti.Add(student);
                            } else {
                                vargsiukai.Add(student);
                            }
                            break;
                    }
                }
            } catch {
                Console.WriteLine("-----------------!!!!!!!!!-----------------");
                Console.WriteLine("Nepavyko nuskaityti failo. Baigiamas darbas");
            }
            
            switch (TYPE) {
                case "LIST":
                    galvotiEnum = galvoti;
                    vargsiukaiEnum = vargsiukai;
                    break;
                case "QUEUE":
                    galvotiEnum = galvotiQueue;
                    vargsiukaiEnum = vargsiukaiQueue;
                    break;
                case "LINKEDLIST":
                    galvotiEnum = galvotiLinkedList;
                    vargsiukaiEnum = vargsiukaiLinkedList;
                    break;
                default: // Assign something, to ignore IDE init errors...
                    galvotiEnum = galvoti;
                    vargsiukaiEnum = vargsiukai;
                    break;
            }

            if (withOutput) {
                try {
                    StudentToFile(galvotiEnum, galvotiFile);
                    StudentToFile(vargsiukaiEnum, vargsiukaiFile);
                } catch (Exception e) {
                    Console.WriteLine("Nepavyko irasyti failu. Programa uzsidaro.");
                    Environment.Exit(1);
                }
            }

            if (noLogging) return;
            Console.WriteLine("Vargsiukai: " + vargsiukaiEnum.Count());
            Console.WriteLine("Galvoti: " + galvotiEnum.Count());
        }
        
        private static void StudentToFile(IEnumerable<Student> list, string fileName) {
            if (File.Exists(fileName)) {    
                File.Delete(fileName);
            }
            
            /*
             foreach (var line in fileInput) {
                    var student = GetStudentData(true, line);
                    students.Enqueue(student);
                }
             */
            var studList = list.Select(student => student.Name + ' ' + student.Surname).ToList();

            File.WriteAllLines(fileName, studList, Encoding.UTF8);
        }

        public static void ReadFromConsole()
        {
            var continueInput = true;
            var students = new List<Student>();

            while (continueInput)
            {
                Console.Write("Prideti studenta? Y/N : ");
                continueInput = Console.ReadLine().ToLower().Equals("y");

                if (continueInput)
                {
                    var student = CreationOfStudent(false, null);
                    students.Add(student);
                }
            }

            StudentSort(students);
            Console.ReadKey();
        }

        public static void ReadFromFile()
        {
            string[] fileInput;
            var students = new List<Student>();

            try
            {
                fileInput = File.ReadAllLines(
                    @"C:\Users\Baltrus\Documents\IPA\IPA\students.txt");
                foreach (var line in fileInput)
                {
                    var student = CreationOfStudent(true, line);
                    students.Add(student);
                }

                StudentSort(students);
                Console.ReadKey();
            }
            catch
            {
                Console.WriteLine("----------------------!!!!!!!!!------------------------");
                Console.WriteLine("Nepavyko nuskaityti failo. Duomenis veskite per konsole");
                ReadFromConsole();
            }
        }

        public static void StudentSort(List<Student> students)
        {
            var sortedStudents = new List<Student>();
            if (students.Any())
            {
                sortedStudents = students.OrderBy(o => o.Name).ToList();
                StudentsResultConsoleOut(sortedStudents);
            }
        }

        public static Student CreationOfStudent(bool inputFromFile, string line)
        {
            string name;
            string surname;
            string input;
            string[] inputLine = {""};

            var testResult = 0;

            double medianResult = 0, avgResult = 0;

            var isAvgSelected = true;
            var generateGrade = false;

            var homeWorkResults = new List<int>();

            if (inputFromFile && line != null) inputLine = line.Split(' ');

            /* Vardo ivedimas */
            name = inputFromFile ? inputLine[0] : InputName();


            /* Pavardes ivedimas */
            surname = inputFromFile ? inputLine[1] : InputSurname();

            /* Namu darbu rezultatu ivedimas */
            if (inputFromFile)
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
                generateGrade = input.ToLower().Equals("y");

                homeWorkResults = InputHomeWorkSum(generateGrade);
            }


            /* Egzamino rezultato ivedimas */
            if (inputFromFile)
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
                testResult = InputExamResult(generateGrade);

            /* Vidurkio skaiciavimas */
            if (inputFromFile)
            {
                avgResult = 0.3 * GetAverageHWResult(homeWorkResults) + 0.7 * testResult;
                medianResult = 0.3 * GetMedianHWResult(homeWorkResults) + 0.7 * testResult;
            }
            else
            {
                isAvgSelected = InputTypeOfCalculation();
                if (isAvgSelected)
                    avgResult = 0.3 * GetAverageHWResult(homeWorkResults) + 0.7 * testResult;
                else
                    medianResult = 0.3 * GetMedianHWResult(homeWorkResults) + 0.7 * testResult;
            }

            var stud = new Student(name, surname, avgResult, medianResult, inputFromFile, isAvgSelected);
            return stud;
        }

        public static string InputName()
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

        public static string InputSurname()
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

        public static List<int> InputHomeWorkSum(bool generateGrades)
        {
            var continueInput = true;

            var homeWorkResults = new List<int>();
            var random = new Random();


            if (!generateGrades) Console.WriteLine("Iveskite namu darbu rezultatus (1-10): ");

            while (continueInput)
            {
                while (true)
                {
                    int homeWorkVal;

                    if (generateGrades)
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

        public static int InputExamResult(bool generateGrades)
        {
            int testResult;
            var random = new Random();

            Console.WriteLine();
            if (!generateGrades)
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

        public static bool InputTypeOfCalculation()
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

        public static double GetAverageHWResult(List<int> homeWorkResults)
        {
            return homeWorkResults.Average();
        }

        public static double GetMedianHWResult(List<int> homeWorkResults)
        {
            double hwResult;

            var ys = homeWorkResults.OrderBy(x => x).ToList();
            var mid = (ys.Count() - 1) / 2.0;
            hwResult = (ys[(int) mid] + ys[(int) (mid + 0.5)]) / 2;

            return hwResult;
        }

        public static void StudentsResultConsoleOut(List<Student> students)
        {
            var vardas = "Vardas";
            var pavarde = "Pavarde";
            var vidurkis = "Galutinis (Vid.)";
            var mediana = "Galutinis (Med.)";
            var temp = "/";
            var defaultOffset = 8;
            var columnVardasLenght = 8; // For longest name
            var columnPavardeLength = 9; // For longest surname

            foreach (var student in students)
            {
                if (student.Name.Length > columnVardasLenght) columnVardasLenght = student.Name.Length;
                if (student.Surname.Length > columnPavardeLength) columnPavardeLength = student.Surname.Length;
            }

            /* Column names */
            Console.WriteLine("{0}{1}{2}{3}{4}",
                FormatSpaces(vardas, ' ', Math.Abs(columnVardasLenght - vardas.Length) + defaultOffset),
                FormatSpaces(pavarde, ' ', Math.Abs(columnPavardeLength - pavarde.Length) + defaultOffset),
                FormatSpaces(vidurkis, ' ', defaultOffset / 2),
                FormatSpaces(temp, ' ', defaultOffset / 2),
                mediana);

            Console.WriteLine(FormatSpaces("", '-',
                columnVardasLenght + columnPavardeLength + 3 * defaultOffset + vidurkis.Length + mediana.Length +
                temp.Length));

            /* Results */
            foreach (var student in students)
            {
                var columnNameOffset = columnVardasLenght - student.Name.Length + defaultOffset;
                var columnSurnameOffset = columnPavardeLength - student.Surname.Length + defaultOffset +
                                          (vidurkis.Length - student.AvgResult.ToString().Length - 3) + 2;
                Console.Write("{0}{1}",
                    FormatSpaces(student.Name, ' ', columnNameOffset),
                    FormatSpaces(student.Surname, ' ', columnSurnameOffset));

                if (student.IsInputFromFile)
                {
                    var columnAvgResultOffset = "   " + student.AvgResult;
                    Console.WriteLine("{0:F2} {1} {2:F2}", student.AvgResult, FormatSpaces("", ' ',
                            defaultOffset + temp.Length + mediana.Length - columnAvgResultOffset.Length),
                        student.MedianResult);
                }
                else
                {
                    Console.WriteLine("{0} {1}", student.IsAvgSelected
                            ? $"{student.AvgResult:F2}"
                            : FormatSpaces("", ' ', defaultOffset + temp.Length + mediana.Length),
                        !student.IsAvgSelected ? $"{student.AvgResult:F2}" : FormatSpaces("", ' ', mediana.Length));
                }
            }
        }

        private static string FormatSpaces(string w, char c, int n) // Counting needed spaces
        {
            return w + new string(c, n);
        }
    }
}