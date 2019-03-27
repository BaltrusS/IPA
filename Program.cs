﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace IPA
{
	class Program
	{
		public static void Main(string[] args)
		{
			string Input = "";

            Console.WriteLine("Iveskite duomenu ivedimo tipa:");
            Console.WriteLine("1 - duomenu ivedimas is failo");
            Console.WriteLine("2 - duomenu ivedimas konsoleje");

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

                Console.WriteLine("Bad input, karotkite!");
            }
        }

        public static void InputByConsole()
        {
            bool continueInput = true;
            List<Student> students = new List<Student>();

            while (continueInput)
            {
            	Console.Write("Prideti studenta? Y/N : ");
                continueInput = Console.ReadLine().ToLower().Equals("y");

                if (continueInput)
                {
                    Student student = GetStudentData(false, null);
                    students.Add(student);
                }
            }

            StudentSort(students);
            Console.ReadKey();
        }

        public static void InputByFile()
        {
            string[] fileInput;
            List<Student> students = new List<Student>();

            try
            {
                fileInput = System.IO.File.ReadAllLines(
                    @"C:\Users\Baltrus\Documents\IPA\IPA\students.txt");
                foreach (var line in fileInput)
                {
                    Student student = GetStudentData(true, line);
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
            List<Student> sortedStudents = new List<Student>();
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

            int testResult = 0;

            double avgHWResult = 0, medianResult = 0, avgResult = 0;

            //bool continueInput = true;
            bool isAvgSelected = true;
            bool generateNumbers = false;

            List<int> homeWorkResults = new List<int>();

            if (isInputFromFile && line != null)
            {
                inputLine = line.Split(' ');
            }

            /* Vardo ivedimas */
            name = isInputFromFile ? inputLine[0] : GetStudentName();


            /* Pavardes ivedimas */
            surname = isInputFromFile ? inputLine[1] : GetStudentSurname();

            /* Namu darbu rezultatu ivedimas */
            if (isInputFromFile)
            {
                for (int i = 2; i < inputLine.Length - 1; i++)
                {
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
            {
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
            }
            else
            {
                testResult = GetStudentTestResult(generateNumbers);
            }

            /* Vidurkio skaiciavimas */
            if (isInputFromFile)
            {
                avgResult = 0.3 * GetStudentAvgHWResult(homeWorkResults) + 0.7 * testResult;
                medianResult = 0.3 * GetStudentMedianHWResult(homeWorkResults) + 0.7 * testResult;
            }
            else
            {
                isAvgSelected = GetStudentChoiceOfAvg();
                if (isAvgSelected)
                {
                    avgResult = 0.3 * GetStudentAvgHWResult(homeWorkResults) + 0.7 * testResult;
                }
                else
                {
                    medianResult = 0.3 * GetStudentMedianHWResult(homeWorkResults) + 0.7 * testResult;
                }
            }

            Student stud = new Student(name, surname, avgResult, medianResult, isInputFromFile, isAvgSelected);
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
            bool continueInput = true;

            List<int> homeWorkResults = new List<int>();
            Random random = new Random();


            if (!generateNumbers)
            {
                Console.WriteLine("Iveskite namu darbu rezultatus (1-10): ");
            }

            while (continueInput)
            {
                while (true)
                {
                    int hWVal;
                    Console.WriteLine(".......");

                    if (generateNumbers)
                    {
                        homeWorkResults.Add(random.Next(0, 11));
                        Console.Write("Sugeneruotas rezultatas: {0}\n", homeWorkResults[homeWorkResults.Count - 1]);
                        break;
                    }

                    Console.Write("Iveskite {0} namu darbo pazymi: ", homeWorkResults.Count() + 1);

                    if (!int.TryParse(Console.ReadLine(), out hWVal))
                    {
                        Console.WriteLine("Turite ivesti skaiciu!");
                    }
                    else if (hWVal < 0 || hWVal > 10)
                    {
                        Console.WriteLine("Galimi reziai 1-10, pakartokite!");
                    }
                    else
                    {
                        homeWorkResults.Add(hWVal);
                        break;
                    }
                }

                Console.WriteLine("-------");
                Console.WriteLine("Ar norite testi namu darbu ivedima? Y/N");
                continueInput = Console.ReadLine().ToLower().Equals("y");
            }

            return homeWorkResults;
        }

        public static int GetStudentTestResult(bool generateNumbers)
        {
            int testResult;
            Random random = new Random();

            Console.WriteLine("*-*-*-*");
            if (!generateNumbers)
            {
                Console.Write("Egzamino pazymis: ");

                if (!int.TryParse(Console.ReadLine(), out testResult))
                {
                    Console.WriteLine("Turite ivesti skaiciu!");
                }
                else if (testResult < 0 || testResult > 10)
                {
                    Console.WriteLine("Galimi reziai 1-10, pakartokite!");
                }
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

        public static double GetStudentAvgHWResult(List<int> homeWorkResults)
        {
            return homeWorkResults.Average();
        }

        public static double GetStudentMedianHWResult(List<int> homeWorkResults)
        {
            double medianHWResult;

            var ys = homeWorkResults.OrderBy(x => x).ToList();
            double mid = (ys.Count() - 1) / 2.0;
            medianHWResult = (ys[(int) (mid)] + ys[(int) (mid + 0.5)]) / 2;

            return medianHWResult;
        }

        public static void StudentsTable(List<Student> students)
        {
            string tableName = "Vardas";
            string tableSurname = "Pavarde";
            string tableAvg = "Galutinis (Vid.)";
            string tableMed = "Galutinis (Med.)";
            string tempS = "/";
            int defaultOffset = 6;
            int columnVardasLenght = 6; // For longest name
            int columnPavardeLength = 7; // For longest surname

            foreach (Student student in students)
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
            foreach (Student student in students)
            {
                int columnNameOffset = columnVardasLenght - student.Name.Length + defaultOffset;
                int columnSurnameOffset = columnPavardeLength - student.Surname.Length + defaultOffset +
                                          (tableAvg.Length - student.AvgResult.ToString().Length - 3) + 2;
                Console.Write("{0}{1}",
                    FormatSpaces(student.Name, ' ', columnNameOffset),
                    FormatSpaces(student.Surname, ' ', columnSurnameOffset));

                if (student.IsInputFromFile)
                {
                    string columnAvgResultOffset = "   " + student.AvgResult;
                    Console.WriteLine("{0:F2} {1} {2:F2}", student.AvgResult, FormatSpaces("", ' ',
                            defaultOffset + tempS.Length + tableMed.Length - columnAvgResultOffset.Length),
                        student.MedianResult);
                }
                else
                {
                    Console.WriteLine("{0} {1}", student.IsAvgSelected
                            ? "{student.AvgResult:F2}"
                            : FormatSpaces("", ' ', defaultOffset + tempS.Length + tableMed.Length),
                            !student.IsAvgSelected ? "{student.AvgResult:F2}" : FormatSpaces("", ' ', tableMed.Length));
                }
            }
        }

        static string FormatSpaces(string w, char c, int n) // Counting needed spaces
        {
            return w + new String(c, n);
        }
    }
}