using System;
using System.Collections.Generic;
using System.IO;

namespace Q4_GhanaianStudentGrades
{
    // -------------------------------
    // a. Student Class
    // -------------------------------
    public class Student
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Score { get; set; }

        public Student(int id, string fullName, int score)
        {
            Id = id;
            FullName = fullName;
            Score = score;
        }

        public string GetGrade()
        {
            if (Score >= 80 && Score <= 100) return "A";
            else if (Score >= 70 && Score <= 79) return "B";
            else if (Score >= 60 && Score <= 69) return "C";
            else if (Score >= 50 && Score <= 59) return "D";
            else return "F";
        }
    }

    // -------------------------------
    // b. Custom Exceptions
    // -------------------------------
    public class InvalidScoreFormatException : Exception
    {
        public InvalidScoreFormatException(string message) : base(message) { }
    }

    public class MissingFieldException : Exception
    {
        public MissingFieldException(string message) : base(message) { }
    }

    // -------------------------------
    // d. StudentResultProcessor Class
    // -------------------------------
    public class StudentResultProcessor
    {
        public List<Student> ReadStudentsFromFile(string inputFilePath)
        {
            List<Student> students = new List<Student>();

            using (StreamReader reader = new StreamReader(inputFilePath))
            {
                string line;
                int lineNumber = 0;

                while ((line = reader.ReadLine()) != null)
                {
                    lineNumber++;
                    string[] parts = line.Split(',');

                    if (parts.Length != 3)
                        throw new MissingFieldException($"Line {lineNumber}: Missing field(s).");

                    // Parse ID
                    if (!int.TryParse(parts[0].Trim(), out int id))
                        throw new Exception($"Line {lineNumber}: Invalid ID format.");

                    string fullName = parts[1].Trim();

                    if (!int.TryParse(parts[2].Trim(), out int score))
                        throw new InvalidScoreFormatException($"Line {lineNumber}: Invalid score format.");

                    students.Add(new Student(id, fullName, score));
                }
            }

            return students;
        }

        public void WriteReportToFile(List<Student> students, string outputFilePath)
        {
            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (Student s in students)
                {
                    writer.WriteLine($"{s.FullName} (ID: {s.Id}): Score = {s.Score}, Grade = {s.GetGrade()}");
                }
            }
        }
    }

    // -------------------------------
    // e. Main Method
    // -------------------------------
    class Program
    {
        static void Main(string[] args)
        {
            string inputFilePath = "students.txt";  // Input file
            string outputFilePath = "report.txt";   // Output file

            try
            {
                StudentResultProcessor processor = new StudentResultProcessor();

                // Read students from input file
                List<Student> students = processor.ReadStudentsFromFile(inputFilePath);

                // Write summary report
                processor.WriteReportToFile(students, outputFilePath);

                Console.WriteLine("Report successfully generated! Check 'report.txt'.");
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Error: Input file not found. " + ex.Message);
            }
            catch (InvalidScoreFormatException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (MissingFieldException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }
        }
    }
}

