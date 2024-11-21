using System;
using System.Data.SqlClient;
using System.Data;

class MainClass {

    static string connectionString = "Server=localhost;Database=SportsCatalog;Trusted_Connection=True;";
    public static void Main (string[] args) {
        try {
            // Initialize the database
            Init.InitializeDatabase();
            Console.WriteLine("La base de données a été initialisée avec succès.");
        }
        catch (Exception e) {
            Console.WriteLine($"Erreur lors de l'initialisation de la base de données : {e.Message}");
            return;
        }
    }
}
