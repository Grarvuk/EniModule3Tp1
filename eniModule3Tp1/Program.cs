
using ProjetLinq.BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace eniModule3Tp1
{
    class Program
    {
        private static List<Auteur> ListeAuteurs = new List<Auteur>();
        private static List<Livre> ListeLivres = new List<Livre>();

        static void Main(string[] args)
        {
            InitialiserDatas();

            var lesAuteursAvecG = ListeAuteurs.Where(Auteur => Auteur.Nom.StartsWith("G"));
            AfficherList(lesAuteursAvecG, "Les auteurs commencons avec un G : ");

            /*
            //Premiere etape, je compte les auteurs par livres ecris
            var auteurParLivreEcris =
            from Livre in ListeLivres
            group Livre by Livre.Auteur into resultats
            select new
            {
                Auteur = resultats.Key,
                Nombre = resultats.Count(),
            };

            var auteurPlusDelivres = auteurParLivreEcris.OrderByDescending(Auteur => Auteur.Nombre).First();
            //On recycle cette liste pour obtenir le moins d'auteur
            var auteurMoinsDelivres = auteurParLivreEcris.OrderBy(Auteur => Auteur.Nombre).First();

            //Deuxieme etape, je choisis celui qui en a ecris le plus
            Console.WriteLine("Le plus de livres "+auteurPlusDelivres.Auteur.Nom +" " + auteurPlusDelivres.Auteur.Prenom + " nombre de livres : " + auteurPlusDelivres.Nombre);
            Console.WriteLine("Le moins de livres " + auteurMoinsDelivres.Auteur.Nom + " " + auteurMoinsDelivres.Auteur.Prenom + " nombre de livres : " + auteurMoinsDelivres.Nombre);
            */

            IGrouping<Auteur, Livre> lePlusDeLivre = ListeLivres.GroupBy(x => x.Auteur).OrderByDescending(x => x.Count()).FirstOrDefault();
            Console.WriteLine("Le plus de livre " + lePlusDeLivre.Key.Nom + " "+lePlusDeLivre.Key.Prenom);

            IGrouping<Auteur, Livre> leMoinsDeLivre = ListeLivres.GroupBy(x => x.Auteur).OrderBy(x => x.Count()).FirstOrDefault();
            Console.WriteLine("Le moins de livres " + leMoinsDeLivre.Key.Nom + " " + leMoinsDeLivre.Key.Prenom);

            ListeLivres.GroupBy(x => x.Auteur).ToList().ForEach(x =>
            {
                Console.WriteLine(x.Key.Nom + " "+ x.Key.Prenom);
                Console.WriteLine(x.Average(y => y.NbPages));
            }
            );

            //Pour le plus de page
            var lePlusDePage = ListeLivres.OrderByDescending(x => x.NbPages).FirstOrDefault();
            Console.WriteLine("Le livre avec le plus de page " + lePlusDePage.Titre + " " + lePlusDePage.NbPages);

            //Les sous sous gagnés en moyenne
            //ListeAuteurs.Where(x => x.Factures.Count > 0).Average(x.Factures.Average(y => y.Montant));

            //Afficher les auteurs avec leurs livres
            Object qqch;
            
            ListeLivres.OrderBy(x => x.Auteur).ToList().ForEach(x => {
                Console.WriteLine(x.Auteur.Nom + " " + x.Auteur.Prenom + " " + x.Titre);
            });
        }


        private static void InitialiserDatas()
        {
            ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
            ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
            ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
            ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
            ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
            ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
            ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
            ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
            ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
            ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
            ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
            ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(0).addFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
            ListeAuteurs.ElementAt(1).addFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
            ListeAuteurs.ElementAt(2).addFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
            ListeAuteurs.ElementAt(3).addFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
        }

        private static void AfficherList(IEnumerable uneList, String msgDebut)
        {
            Console.WriteLine(msgDebut);
            foreach (Object unElement in uneList)
            {
                Console.WriteLine(unElement);
            }
        }

    }
}
