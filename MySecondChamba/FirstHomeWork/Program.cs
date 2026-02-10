
using System;
using System.Collections.Generic;
using MySecondChamba.Entities;

class Program
{
    static void Main()
    {
        var members = new List<CommunityMember>
        {
            new Student { Id=1, Name="Marcos", career="Ing. Sistemas", Semester=4 },
            new ExStudent { Id=2, Name="Ana", title="Lic.", CareerStudied="Adm.", GraduationYear=2023 },
            new Employee { Id=3, Name="Juan", profession="Soporte", CompanyName="Uni", salary=30000m },
            new Teacher { Id=4, Name="Laura", profession="Docente", CompanyName="Uni", subject="Programación" },
            new Administrator { Id=5, Name="Luis", profession="Docente", CompanyName="Uni",
                                department="Facultad Ing.", adminMode="Académico" }
        };

        foreach (var member in members)
        {
            Console.WriteLine($"{member.Name} - {member.Id}");

        }
            Console.ReadLine();
    }
}
