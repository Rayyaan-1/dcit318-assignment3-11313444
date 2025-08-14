using System;
using System.Collections.Generic;
using System.Linq;

public class HealthSystemApp
{
    private Repository<Patient> _patientRepo = new Repository<Patient>();
    private Repository<Prescription> _prescriptionRepo = new Repository<Prescription>();
    private Dictionary<int, List<Prescription>> _prescriptionMap = new Dictionary<int, List<Prescription>>();

    public void SeedData()
    {
        // Add Patients
        _patientRepo.Add(new Patient(1, "Alice Johnson", 28, "Female"));
        _patientRepo.Add(new Patient(2, "Bob Smith", 45, "Male"));
        _patientRepo.Add(new Patient(3, "Catherine Brown", 32, "Female"));

        // Add Prescriptions
        _prescriptionRepo.Add(new Prescription(1, 1, "Amoxicillin", DateTime.Now.AddDays(-10)));
        _prescriptionRepo.Add(new Prescription(2, 1, "Ibuprofen", DateTime.Now.AddDays(-5)));
        _prescriptionRepo.Add(new Prescription(3, 2, "Paracetamol", DateTime.Now.AddDays(-2)));
        _prescriptionRepo.Add(new Prescription(4, 3, "Azithromycin", DateTime.Now.AddDays(-15)));
        _prescriptionRepo.Add(new Prescription(5, 2, "Cetirizine", DateTime.Now.AddDays(-8)));
    }

    public void BuildPrescriptionMap()
    {
        _prescriptionMap = _prescriptionRepo
            .GetAll()
            .GroupBy(p => p.PatientId)
            .ToDictionary(g => g.Key, g => g.ToList());
    }

    public void PrintAllPatients()
    {
        Console.WriteLine("\nAll Patients:");
        foreach (var patient in _patientRepo.GetAll())
        {
            Console.WriteLine(patient);
        }
    }

    public List<Prescription> GetPrescriptionsByPatientId(int patientId)
    {
        if (_prescriptionMap.ContainsKey(patientId))
        {
            return _prescriptionMap[patientId];
        }
        return new List<Prescription>();
    }

    public void PrintPrescriptionsForPatient(int id)
    {
        Console.WriteLine($"\nPrescriptions for Patient ID {id}:");
        var prescriptions = GetPrescriptionsByPatientId(id);

        if (prescriptions.Any())
        {
            foreach (var p in prescriptions)
            {
                Console.WriteLine(p);
            }
        }
        else
        {
            Console.WriteLine("No prescriptions found for this patient.");
        }
    }
}
