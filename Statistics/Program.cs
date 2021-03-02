using System;
using System.Collections.Generic;

namespace Statistics
{
	class Program
	{
		static void Main(string[] args)
		{
			//Fields
			string input = "";
			string index = "";
			Dictionary<string, double[]> setDict = new Dictionary<string, double[]>();

			//A loop that runs until the user quits the program
			while (input != "0")
			{
				//Asks the user to select an option
				do
				{
					Console.WriteLine("Choose an option using its respective number: ");
					Console.WriteLine(" 0) Quit             1) Add New Data Set          2) Find Mean       3) Find Standard Deviaton    4) Find Z-Score");
					Console.WriteLine(" 5) Find Quartile    6) Find IQR                  7) Find Fences     8) Find Outliers             9) Print Data Set");
					Console.WriteLine(" 10) Find Coefficient of Correlation              11) Sort Data Set\n");
					input = Console.ReadLine();
				}
				while (!int.TryParse(input, out int choice) || choice > 11);

				if (setDict.Count == 0 && input != "0" && input != "1")
				{
					Console.WriteLine("\nThere are no sets currently! Please create a set first.\n");
					continue;
				}
				//Performs the function that the user specified
				switch (input)
				{
					case "1":
						// Prompts the user to enter the size of their data set
						string numElements;
						do
						{
							Console.Write("\nHow many elements are in this data set: ");
							numElements = Console.ReadLine();
						}
						while (!int.TryParse(numElements, out int choice));

						double[] newSet = new double[int.Parse(numElements)];

						// Allows the user to name their set for future reference
						Console.Write("What would you like this set to be named: ");
						string setName = Console.ReadLine();

						// Has the user enter their data
						for (int i = 0; i < newSet.Length; i++)
						{
							string element;
							do
							{
								Console.Write("Enter element " + (i + 1) + ": ");
								element = Console.ReadLine();
							}
							while (!double.TryParse(element, out double choice));

							newSet[i] = double.Parse(element);
						}

						setDict.Add(setName, newSet);
						Console.WriteLine("\nThe set " + setName + " has been added!\n");
						break;

					case "2":
						// Has the user select which set to calculate the mean for
						Console.WriteLine("\nWhich data set would you like to find the mean of: ");
						foreach (string key in setDict.Keys)
						{
							Console.WriteLine(key);
						}

						index = Console.ReadLine();

						Console.WriteLine("\nThe mean of the set " + index + " is " + FindMean(setDict[index]) + "\n");
						break;

					case "3":
						// Has the user select which set to calculate the standard deviation for
						Console.WriteLine("\nWhich data set would you like to find the standard deviation of: ");
						foreach (string key in setDict.Keys)
						{
							Console.WriteLine(key);
						}

						index = Console.ReadLine();

						Console.WriteLine("\nThe standard deviation of the set " + index + " is " + FindStandardDeviation(setDict[index]) + "\n");
						break;

					case "4":
						// Has the user select which set to calculate the z-score for
						Console.WriteLine("Which data set would you like to select an element from: ");
						foreach (string key in setDict.Keys)
						{
							Console.WriteLine(key);
						}

						index = Console.ReadLine();

						Console.WriteLine("\nThe elements in set " + index + " are:\n");
						Print(setDict[index]);

						// Has the user select which element in the set to calculate the z-score for
						Console.Write("\nWhich element would you like to find the z-score of: ");
						string selection = Console.ReadLine();
						Console.WriteLine("\nThe z-score of " + selection + " in the set " + index + " is " + FindZScore(double.Parse(selection), setDict[index]) + "\n");
						break;

					case "5":
						// Has the user select which set to calculate the quartile for
						Console.WriteLine("Which data set would you like to find a quartile of: ");
						foreach (string key in setDict.Keys)
						{
							Console.WriteLine(key);
						}

						index = Console.ReadLine();

						// Has the user select which quartile to find
						Console.Write("\nWhich quartile would you like to find: ");
						string quarter = Console.ReadLine();
						Console.WriteLine("\nQ" + quarter + " for the set " + index + " is " + FindQuartile(int.Parse(quarter), setDict[index]) + "\n");
						break;

					case "6":
						// Has the user select which set to calculate the interquartile ratio for
						Console.WriteLine("Which data set would you like to find the IQR for: ");
						foreach (string key in setDict.Keys)
						{
							Console.WriteLine(key);
						}

						index = Console.ReadLine();

						Console.WriteLine("\nThe IQR for the set " + index + " is " + FindIQR(setDict[index]));
						break;

					case "7":
						// Has the user select which set to calculate the upper and lower fences for
						Console.WriteLine("Which data set would you like to find the fences of: ");
						foreach (string key in setDict.Keys)
						{
							Console.WriteLine(key);
						}

						index = Console.ReadLine();
						double[] fences = FindFences(setDict[index]);

						Console.WriteLine("\nThe lower fence of the set " + index + " is " + fences[0]);
						Console.WriteLine("The upper fence of the set " + index + " is " + fences[1] + "\n");
						break;

					case "8":
						// Has the user select which set to determine the outliers for
						Console.WriteLine("Which data set would you like to find the outliers of: ");
						foreach (string key in setDict.Keys)
						{
							Console.WriteLine(key);
						}

						index = Console.ReadLine();
						List<double> outliers = FindOutliers(setDict[index]);

						Console.WriteLine("\nThe outliers of the set " + index + " are:");
						foreach (double outlier in outliers)
						{
							Console.WriteLine(outlier);
						}
						Console.WriteLine();

						break;

					case "9":
						// Has the user select which set to print
						Console.WriteLine("\nWhich data set would you like to print: ");
						foreach (string key in setDict.Keys)
						{
							Console.WriteLine(key);
						}

						index = Console.ReadLine();
						Console.WriteLine("\nThe elements in set " + index + " are:\n");
						Print(setDict[index]);
						break;

					case "10":
						// Has the user select which sets to use to find the coefficient of correlation
						double[] xValues;
						double[] yValues;
						Console.WriteLine("Which data set contains the x-values: ");
						foreach (string key in setDict.Keys)
						{
							Console.WriteLine(key);
						}

						xValues = setDict[Console.ReadLine()];

						Console.WriteLine("Which data set contains the y-values: ");
						foreach (string key in setDict.Keys)
						{
							Console.WriteLine(key);
						}

						yValues = setDict[Console.ReadLine()];

						Console.WriteLine("\nThe coefficient of correlation for this pair of data sets is " + FindCoefficientOfCorrelation(xValues, yValues) + "\n");
						break;

					case "11":
						// Has the user select which set to sort
						Console.WriteLine("Which data set would you like to sort: ");
						foreach (string key in setDict.Keys)
						{
							Console.WriteLine(key);
						}

						string setToSort = Console.ReadLine();
						double[] sorted = new double[setDict[setToSort].Length];
						for(int i = 0; i < setToSort.Length; i++)
						{
							sorted[i] = setDict[setToSort][i];
						}
						Array.Sort(sorted);
						setDict.Add(setToSort + " (sorted)", sorted);

						Console.WriteLine(setToSort + " has been sorted!");
						break;

					default:
						break;
				}
			}
		}

		/// <summary>
		/// Prints the given data set
		/// </summary>
		/// <param name="data">The data to print</param>
		private static void Print(double[] data)
		{
			for (int i = 0; i < data.Length; i++)
			{
				if (i != data.Length - 1)
				{
					Console.Write(data[i] + ", ");
				}
				else
				{
					Console.Write(data[i]);
				}

				if (i % 4 == 0 && i != 0)
				{
					Console.Write("\n");
				}
			}
			Console.WriteLine("\n");
		}
		/// <summary>
		/// Finds the mean of a given data set
		/// </summary>
		private static double FindMean(double[] data)
		{
			double average = 0;

			foreach (double element in data)
			{
				average += element;
			}

			return average / data.Length;
		}

		/// <summary>
		/// Finds the standard deviation for a given data set
		/// </summary>
		private static double FindStandardDeviation(double[] data)
		{
			double mean = FindMean(data);
			double standardDeviation = 0;

			for (int i = 0; i < data.Length; i++)
			{
				standardDeviation += Math.Pow(data[i] - mean, 2);
			}

			standardDeviation /= data.Length;
			standardDeviation = Math.Sqrt(standardDeviation);
			return standardDeviation;
		}

		/// <summary>
		/// Finds the z-score of a specific piece of data from a data set
		/// </summary>
		private static double FindZScore(double data, double[] dataSet)
		{
			return (data - FindMean(dataSet)) / FindStandardDeviation(dataSet);
		}

		/// <summary>
		/// Finds the value of a given quartile in a given data set
		/// </summary>
		/// <param name="quarter">The quarter of the data set to evaluate</param>
		private static double FindQuartile(int quarter, double[] data)
		{
			double index = (quarter * 0.25 * data.Length) - 1;
			double result = 0;
			if (index == (int)index)
			{
				result = (data[(int)index] + data[(int)index + 1]) / 2;
			}
			else
			{
				index += 1;
				result = data[(int)index];
			}

			return result;
		}

		/// <summary>
		/// Finds the interquartile range of a given set of data
		/// </summary>
		private static double FindIQR(double[] data)
		{
			return FindQuartile(3, data) - FindQuartile(1, data);
		}

		/// <summary>
		/// Finds the lower and upper fences of a given data set
		/// </summary>
		/// <returns>An array containing the lower fence in index 0 and the upper fence in index 1</returns>
		private static double[] FindFences(double[] data)
		{
			double[] fences =
			{
				FindQuartile(1, data) - (1.5 * FindIQR(data)),
				FindQuartile(3, data) + (1.5 * FindIQR(data))
			};

			return fences;
		}

		/// <summary>
		/// Finds the outliers of a set of data assuming the set is in ascending order
		/// </summary>
		private static List<double> FindOutliers(double[] data)
		{
			double[] fences = FindFences(data);
			List<double> outliers = new List<double>();

			for (int i = 0; i < data.Length; i++)
			{
				if (data[i] < fences[0])
				{
					outliers.Add(data[i]);
				}
				else
				{
					break;
				}
			}

			for (int i = data.Length - 1; i > 0; i--)
			{
				if (data[i] > fences[1])
				{
					outliers.Add(data[i]);
				}
				else
				{
					break;
				}
			}

			return outliers;
		}

		/// <summary>
		/// Calculates the coefficient of correlation of 2 sets of data
		/// </summary>
		private static double FindCoefficientOfCorrelation(double[] xValues, double[] yValues)
		{
			double xSum = 0;
			double ySum = 0;
			double xySum = 0;
			double xSquareSum = 0;
			double ySquareSum = 0;
			double sampleSize = xValues.Length;

			for (int i = 0; i < sampleSize; i++)
			{
				xSum += xValues[i];
				xSquareSum += xValues[i] * xValues[i];
				ySum += yValues[i];
				ySquareSum += yValues[i] * yValues[i];
				xySum += xValues[i] * yValues[i];
			}

			double numerator = (sampleSize * xySum) - (xSum * ySum);
			double denominator = Math.Sqrt(((sampleSize * xSquareSum) - (xSum * xSum)) * ((sampleSize * ySquareSum) - (ySum * ySum)));

			return numerator / denominator;
		}
	}
}
