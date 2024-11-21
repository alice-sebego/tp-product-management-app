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

        // Test operations
        program.AddProduct("Raquette de tennis", 79.99, 10, "Raquette légère et résistante");
        program.UpdateProduct(1, "Ballon de foot Pro", 39.99, 20, "Ballon en cuir synthétique");
        program.DeleteProduct(3);

        // Save changes to database
        program.SaveChanges();

        // Display products after changes
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

    /// <summary>
    /// Add a product to the DataSet
    /// </summary>
    public void AddProduct(string name, double price, int quantity, string description)
    {
        if (productDataSet.Tables.Contains("Products"))
        {
            DataTable productsTable = productDataSet.Tables["Products"];
            DataRow newRow = productsTable.NewRow();

            newRow["name_product"] = name;
            newRow["price"] = price;
            newRow["quantity"] = quantity;
            newRow["description"] = description;

            productsTable.Rows.Add(newRow);
            Console.WriteLine("Produit ajouté avec succès.");
        }
    }

    /// <summary>
    /// Update a product in the DataSet
    /// </summary>
    public void UpdateProduct(int productId, string name, double price, int quantity, string description)
    {
        if (productDataSet.Tables.Contains("Products"))
        {
            DataTable productsTable = productDataSet.Tables["Products"];

            var productRow = productsTable.AsEnumerable()
                .FirstOrDefault(row => row.Field<int>("pk_product") == productId);

            if (productRow != null)
            {
                productRow["name_product"] = name;
                productRow["price"] = price;
                productRow["quantity"] = quantity;
                productRow["description"] = description;
                Console.WriteLine("Produit mis à jour avec succès.");
            }
            else
            {
                Console.WriteLine("Produit introuvable.");
            }
        }
    }

    /// <summary>
    /// Delete a product from the DataSet
    /// </summary>
    public void DeleteProduct(int productId)
    {
        if (productDataSet.Tables.Contains("Products"))
        {
            DataTable productsTable = productDataSet.Tables["Products"];

            var productRow = productsTable.AsEnumerable()
                .FirstOrDefault(row => row.Field<int>("pk_product") == productId);

            if (productRow != null)
            {
                productRow.Delete();
                Console.WriteLine("Produit supprimé avec succès.");
            }
            else
            {
                Console.WriteLine("Produit introuvable.");
            }
        }
    }

    /// <summary>
    /// Save changes from DataSet to the database
    /// </summary>
    public void SaveChanges()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter("SELECT pk_product, name_product, price, quantity, description FROM Products", connection);
                        adapter.SelectCommand.Transaction = transaction;

                        SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

                        adapter.Update(productDataSet, "Products");
                        transaction.Commit();

                        Console.WriteLine("Modifications enregistrées dans la base de données avec succès.");
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Erreur lors de l'enregistrement des modifications : {e.Message}");
                    }
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erreur de connexion à la base de données : {e.Message}");
        }
    }
}
