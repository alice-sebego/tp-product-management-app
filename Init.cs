using System;
using System.Data.SqlClient;

class Init
{
    public static void InitializeDatabase()
    {
        string connectionString = "Server=localhost;Database=master;Trusted_Connection=True;";

        // Les instructions SQL pour créer la base de données et la table
        string createDatabaseQuery = @"
            IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'SportsCatalog')
            BEGIN
                CREATE DATABASE SportsCatalog;
            END";

        string createTableQuery = @"
            USE SportsCatalog;

            IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Products]') AND type in (N'U'))
            BEGIN
                CREATE TABLE Products (
                    pk_product INT IDENTITY(1,1) PRIMARY KEY,
                    name_product NVARCHAR(100) NOT NULL,
                    price DECIMAL(10, 2) NOT NULL,
                    quantity INT NOT NULL,
                    description NVARCHAR(255) NULL
                );

                INSERT INTO Products (name_product, price, quantity, description) 
                VALUES 
                ('Ballon de football', 25.99, 100, 'Ballon de football taille standard'),
                ('Raquette de tennis', 89.99, 50, 'Raquette légère et maniable'),
                ('Chaussures de running', 69.99, 200, 'Chaussures de course confortables'),
                ('Haltères 10kg', 39.99, 75, 'Haltères pour musculation'),
                ('Tapis de yoga', 19.99, 150, 'Tapis antidérapant pour yoga'),
                ('Casque de vélo', 49.99, 120, 'Casque de vélo de sécurité'),
                ('Maillot de bain', 29.99, 80, 'Maillot une pièce pour piscine'),
                ('Sac de sport', 34.99, 100, 'Sac résistant pour équipement sportif'),
                ('Short de football', 19.99, 200, 'Short léger pour entraînements'),
                ('Gants de boxe', 59.99, 60, 'Gants en cuir pour boxe amateur'),
                ('Lunettes de natation', 14.99, 180, 'Lunettes anti-buée pour natation'),
                ('Balles de tennis (set de 3)', 9.99, 300, 'Balles de tennis haute performance'),
                ('Vélo tout-terrain', 399.99, 20, 'VTT robuste pour terrains difficiles'),
                ('Protège-tibias', 12.99, 150, 'Protection légère pour football'),
                ('Kit de badminton', 29.99, 100, 'Ensemble avec raquettes et volant'),
                ('Montre de sport', 79.99, 50, 'Montre avec GPS et suivi d''activités'),
                ('Bande élastique de fitness', 14.99, 200, 'Bande pour exercices musculaires'),
                ('Gourde sportive', 9.99, 250, 'Gourde légère et réutilisable'),
                ('Chapeau de randonnée', 24.99, 90, 'Chapeau anti-UV pour randonnée'),
                ('Kettlebell 12kg', 54.99, 40, 'Kettlebell pour exercices fonctionnels'),
                ('Élastique de résistance', 19.99, 150, 'Élastique multi-niveaux pour fitness'),
                ('Genouillère', 14.99, 100, 'Genouillère de soutien pour sports'),
                ('Ballon de basket', 29.99, 80, 'Ballon en cuir synthétique'),
                ('Chaussures de randonnée', 89.99, 70, 'Chaussures imperméables pour randonnée'),
                ('Haltères 5kg', 24.99, 120, 'Haltères légers pour fitness'),
                ('Sac à dos de sport', 49.99, 60, 'Sac pratique avec plusieurs compartiments'),
                ('Tennis de table (set complet)', 99.99, 20, 'Table, raquettes et balles'),
                ('Pagaie de kayak', 69.99, 50, 'Pagaie en aluminium légère'),
                ('Tuba de plongée', 19.99, 150, 'Tuba avec embout silicone confortable'),
                ('Gilet de sauvetage', 39.99, 40, 'Gilet pour sécurité nautique'),
                ('Vélo de route', 499.99, 15, 'Vélo rapide pour routes bitumées'),
                ('Ceinture d''haltérophilie', 34.99, 80, 'Ceinture de maintien pour musculation'),
                ('Ballon de rugby', 24.99, 100, 'Ballon en cuir synthétique pour rugby'),
                ('Kit de skateboard', 89.99, 30, 'Planche, protections et casque'),
                ('Équipement de hockey', 199.99, 10, 'Set complet pour hockey sur glace'),
                ('Chaussures de football', 79.99, 110, 'Chaussures à crampons pour football'),
                ('Bandeau de sport', 9.99, 200, 'Bandeau absorbant pour sueur'),
                ('Gants de ski', 29.99, 75, 'Gants imperméables pour ski'),
                ('Pantalon de ski', 89.99, 50, 'Pantalon isolant pour sport d''hiver'),
                ('Skis alpins', 399.99, 10, 'Paire de skis avec fixations'),
                ('Pantalon de sport', 29.99, 150, 'Pantalon léger pour running'),
                ('Paddleboard gonflable', 349.99, 5, 'Paddleboard stable avec pompe'),
                ('Vélo elliptique', 799.99, 10, 'Machine cardio pour entraînement à domicile'),
                ('Gilet lesté', 59.99, 40, 'Gilet ajustable pour exercices intensifs'),
                ('Trampoline', 199.99, 15, 'Trampoline pour loisirs et fitness'),
                ('Bâton de marche', 24.99, 100, 'Bâton télescopique pour randonnée'),
                ('Matelas de camping', 39.99, 60, 'Matelas gonflable léger'),
                ('Balles de golf (lot de 12)', 19.99, 150, 'Balles pour golfeurs débutants'),
                ('Club de golf', 129.99, 40, 'Club de golf en acier inoxydable');
            END";

        // Exécution des instructions SQL
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();

                // Créer la base de données
                using (SqlCommand command = new SqlCommand(createDatabaseQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                // Créer la table et insérer les données
                using (SqlCommand command = new SqlCommand(createTableQuery, connection))
                {
                    command.ExecuteNonQuery();
                }

                Console.WriteLine("Base de données initialisée avec succès !");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Erreur lors de l'initialisation de la base de données : {e.Message}" );
            }
        }
    }
}
