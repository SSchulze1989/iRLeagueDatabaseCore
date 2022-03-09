// See https://aka.ms/new-console-template for more information
using System;
using iRLeagueDatabaseCore.Models;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var dbContext = new LeagueDbContext();
        dbContext.Database.EnsureCreated();

        Console.WriteLine("Test");
    }
}