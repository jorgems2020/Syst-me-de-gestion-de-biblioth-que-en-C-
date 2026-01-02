using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliothequeApp
{
    // Classe qui représente un Livre
    public class Livre
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public string Auteur { get; set; }
        public int AnneePublication { get; set; }
        public bool Emprunte { get; set; }

        public Livre(int id, string titre, string auteur, int annee)
        {
            Id = id;
            Titre = titre;
            Auteur = auteur;
            AnneePublication = annee;
            Emprunte = false;
        }

        public void AfficherInfo()
        {
            string statut = Emprunte ? "Emprunté" : "Disponible";
            Console.WriteLine($"ID: {Id} | {Titre} - {Auteur} ({AnneePublication}) - {statut}");
        }
    }

    // Classe qui gère la bibliothèque
    public class Bibliotheque
    {
        private List<Livre> livres;
        private int prochainId;

        public Bibliotheque()
        {
            livres = new List<Livre>();
            prochainId = 1;
            ChargerLivresInitiaux();
        }

        private void ChargerLivresInitiaux()
        {
            AjouterLivre("1984", "George Orwell", 1949);
            AjouterLivre("Dom Casmurro", "Machado de Assis", 1899);
            AjouterLivre("Le Seigneur des Anneaux", "J.R.R. Tolkien", 1954);
        }

        public void AjouterLivre(string titre, string auteur, int annee)
        {
            Livre nouveauLivre = new Livre(prochainId++, titre, auteur, annee);
            livres.Add(nouveauLivre);
            Console.WriteLine($"\n✓ Livre '{titre}' ajouté avec succès!");
        }

        public void ListerLivres()
        {
            if (livres.Count == 0)
            {
                Console.WriteLine("\nAucun livre enregistré.");
                return;
            }

            Console.WriteLine("\n=== LIVRES DANS LA BIBLIOTHÈQUE ===");
            foreach (var livre in livres)
            {
                livre.AfficherInfo();
            }
        }

        public void RechercherParTitre(string terme)
        {
            var resultat = livres.Where(l => l.Titre.ToLower().Contains(terme.ToLower())).ToList();

            if (resultat.Count == 0)
            {
                Console.WriteLine("\nAucun livre trouvé.");
                return;
            }

            Console.WriteLine("\n=== RÉSULTATS DE LA RECHERCHE ===");
            foreach (var livre in resultat)
            {
                livre.AfficherInfo();
            }
        }

        public void EmprunterLivre(int id)
        {
            var livre = livres.FirstOrDefault(l => l.Id == id);

            if (livre == null)
            {
                Console.WriteLine("\nLivre non trouvé.");
                return;
            }

            if (livre.Emprunte)
            {
                Console.WriteLine($"\nLe livre '{livre.Titre}' est déjà emprunté.");
                return;
            }

            livre.Emprunte = true;
            Console.WriteLine($"\n✓ Livre '{livre.Titre}' emprunté avec succès!");
        }

        public void RetournerLivre(int id)
        {
            var livre = livres.FirstOrDefault(l => l.Id == id);

            if (livre == null)
            {
                Console.WriteLine("\nLivre non trouvé.");
                return;
            }

            if (!livre.Emprunte)
            {
                Console.WriteLine($"\nLe livre '{livre.Titre}' n'est pas emprunté.");
                return;
            }

            livre.Emprunte = false;
            Console.WriteLine($"\n✓ Livre '{livre.Titre}' retourné avec succès!");
        }
    }

    // Classe principale
    class Programme
    {
        static void Main(string[] args)
        {
            Bibliotheque bibliotheque = new Bibliotheque();
            bool continuer = true;

            Console.WriteLine("╔════════════════════════════════════╗");
            Console.WriteLine("║  SYSTÈME DE GESTION DE             ║");
            Console.WriteLine("║       BIBLIOTHÈQUE                 ║");
            Console.WriteLine("╚════════════════════════════════════╝");

            while (continuer)
            {
                AfficherMenu();
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        bibliotheque.ListerLivres();
                        break;

                    case "2":
                        Console.Write("\nTitre: ");
                        string titre = Console.ReadLine();
                        Console.Write("Auteur: ");
                        string auteur = Console.ReadLine();
                        Console.Write("Année de Publication: ");
                        if (int.TryParse(Console.ReadLine(), out int annee))
                        {
                            bibliotheque.AjouterLivre(titre, auteur, annee);
                        }
                        else
                        {
                            Console.WriteLine("\nAnnée invalide.");
                        }
                        break;

                    case "3":
                        Console.Write("\nEntrez le titre ou une partie: ");
                        string terme = Console.ReadLine();
                        bibliotheque.RechercherParTitre(terme);
                        break;

                    case "4":
                        Console.Write("\nEntrez l'ID du livre: ");
                        if (int.TryParse(Console.ReadLine(), out int idEmprunter))
                        {
                            bibliotheque.EmprunterLivre(idEmprunter);
                        }
                        else
                        {
                            Console.WriteLine("\nID invalide.");
                        }
                        break;

                    case "5":
                        Console.Write("\nEntrez l'ID du livre: ");
                        if (int.TryParse(Console.ReadLine(), out int idRetourner))
                        {
                            bibliotheque.RetournerLivre(idRetourner);
                        }
                        else
                        {
                            Console.WriteLine("\nID invalide.");
                        }
                        break;

                    case "6":
                        continuer = false;
                        Console.WriteLine("\nMerci d'avoir utilisé le système!");
                        break;

                    default:
                        Console.WriteLine("\nOption invalide. Veuillez réessayer.");
                        break;
                }

                if (continuer)
                {
                    Console.WriteLine("\nAppuyez sur une touche pour continuer...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void AfficherMenu()
        {
            Console.WriteLine("\n┌──────────────────────────────────┐");
            Console.WriteLine("│           MENU                   │");
            Console.WriteLine("├──────────────────────────────────┤");
            Console.WriteLine("│ 1. Lister tous les livres        │");
            Console.WriteLine("│ 2. Ajouter un nouveau livre      │");
            Console.WriteLine("│ 3. Rechercher livre par titre    │");
            Console.WriteLine("│ 4. Emprunter un livre            │");
            Console.WriteLine("│ 5. Retourner un livre            │");
            Console.WriteLine("│ 6. Quitter                       │");
            Console.WriteLine("└──────────────────────────────────┘");
            Console.Write("\nChoisissez une option: ");
        }
    }
}
