using System;

class Program
{
    static void Main(string[] args)
    {
        HealthSystemApp app = new HealthSystemApp();

        app.SeedData();
        app.BuildPrescriptionMap();

        app.PrintAllPatients();

        Console.Write("\nEnter Patient ID to view prescriptions: ");
        if (int.TryParse(Console.ReadLine(), out int patientId))
        {
            app.PrintPrescriptionsForPatient(patientId);
        }
        else
        {
            Console.WriteLine("Invalid Patient ID.");
        }
    }
}

