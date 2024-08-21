using System;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Model;
using Hl7.Fhir.Serialization;

namespace FHIR_Prac
{
    public static class Program
    {
        private const string fhirServer = "https://server.fire.ly";

        [Obsolete]
        static void Main(string[] args)
        {
            FhirClient fhirClient = new FhirClient(fhirServer);

            Bundle patientsBundle =  fhirClient.Search<Patient>(new string[] {"name=test"});




            int patientNumber = 1;

            while (patientsBundle != null)
            {
            Console.WriteLine($"Total: {patientsBundle.Total} Entry Count: {patientsBundle.Entry.Count}");
                //list each patients in bundle
            foreach(Bundle.EntryComponent entry in patientsBundle.Entry)
            {
                Console.WriteLine($"- Entry: {patientNumber,3} : {entry.FullUrl}");
                if (entry.Resource != null)
                {
                    Patient patient = (Patient)entry.Resource;
                    Console.WriteLine($"- ID: {patient.Id}");

                    if (patient.Name.Count > 0)
                    {
                        Console.WriteLine($"- Name: {patient.Name[0].ToString()}");
                    }
                }
                patientNumber++;
            }

                //get more result
                fhirClient.Continue(patientsBundle);
            }
            Console.ReadLine();
        }
    }
}
