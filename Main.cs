using System;
using System.Data.SqlClient;
using System.Data;

class MainClass {

    private readonly string connectionString = "Server=localhost;Database=SportsCatalog;Trusted_Connection=True;";
    private readonly DataSet productDataSet = new DataSet();

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

        MainClass program = new MainClass();
        program.LoadDataFromDatabase();

        // display products in console
        program.DisplayProducts();
    }

    /// <summary>
    /// Load table Products data in the DataSet
    /// </summary>
    public void LoadDataFromDatabase()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                string selectQuery = "SELECT pk_product, name_product, price, quantity, description FROM Products";

               
                SqlDataAdapter adapter = new SqlDataAdapter(selectQuery, connection);

               
                adapter.Fill(productDataSet, "Products");

                Console.WriteLine("Données chargées avec succès dans le DataSet.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erreur lors du chargement des données : {e.Message}");
        }
    }

    /// <summary>
    /// Display loaded product in the DataSet.
    /// </summary>
    public void DisplayProducts()
    {
        if (productDataSet.Tables.Contains("Products"))
        {
            DataTable productsTable = productDataSet.Tables["Products"];

            Console.WriteLine("\nListe des produits :");
            foreach (DataRow row in productsTable.Rows)
            {
                Console.WriteLine($"ID: {row["pk_product"]}, Nom: {row["name_product"]}, Prix: {row["price"]}, Quantité: {row["quantity"]}, Description: {row["description"]}");
            }
        }
        else
        {
            Console.WriteLine("Aucune donnée à afficher.");
        }
    }
}
