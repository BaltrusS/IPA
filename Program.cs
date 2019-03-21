using System;
using System.Collections.Generic;

namespace IPA
{
	class Program
	{
		public static void Main(string[] args)
		{
			List<double> ndResults = new List<double>();
			double examResult;
			string vardas="Name", pavarde="Name";
			
			Console.Write("Iveskite varda: ");
			vardas = Console.ReadLine();
			Console.Write("Iveskite pavarde: ");
			pavarde = Console.ReadLine();
			
			Console.Write("Iveskite kiek turite nd rezultatu: ");
			int n = int.Parse(Console.ReadLine());
			for (int i=0; i<n; i++) {
				Console.Write("Iveskite namu darbu rezultata nr "+(i+1)+": ");
				ndResults.Add(Double.Parse(Console.ReadLine()));
			}
			Console.WriteLine(avg(ndResults)*0.3);
			Console.Write("Iveskite egzamino rezultata: ");
			examResult = Double.Parse(Console.ReadLine());
			
			double finalGrade = ((avg(ndResults)*0.3) + (examResult*0.7));
			
			Console.WriteLine("-------------------------------------------------------");
			Console.WriteLine("First Name        | Last Name           |   Final grade");
			Console.WriteLine("-------------------------------------------------------");
			Console.WriteLine(String.Format("{0,-20}{1,-20}{2,15:0.00}", vardas, pavarde, finalGrade));
			
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
		
		public static double avg(List<double> list) {
			double sum=0;
			foreach (double grade in list) {
				sum=sum+grade;
			}
			return sum/list.Count;
		}
	}
}