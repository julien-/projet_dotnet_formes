using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;



namespace Projet_Formes
{
    class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        
        static void Main()
        {
            //test();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());            
        }

        static void test()
        {
     
            //test ellipse

            string nom_ellipse = "elipse1";

            string couleur_ellipse = "white";

            Point point1 = new Point(12, 13);
            int longueur_ell = 12;
            int largeur_ell = 10;
            

            Ellipse ellipse1 = new Ellipse(0, nom_ellipse, couleur_ellipse, point1, longueur_ell, largeur_ell);

            //test polygone

            string nom_polygone = "polygone1";

            string couleur_polygone = "white";

            Point point3 = new Point(16, 17);
            Point point4 = new Point(18, 19);
            Point point5 = new Point(20, 21);

            Point []tableau_points = new Point[3]{point3, point4, point5};

            Polygone polygone1 = new Polygone(1, nom_polygone, couleur_polygone, tableau_points);

            //test rectangle

            string nom_rectangle = "rectangle1";

            string couleur_rectangle = "white";

            Point point6 = new Point(22, 23);
            int longueur_rect = 12;
            int largeur_rect = 10;

            Rectangle rectangle1 = new Rectangle(2, nom_rectangle, couleur_rectangle, point6, longueur_rect, largeur_rect);

            //test groupe

            string nom_groupe = "groupe1";

            List<Forme_simple> liste_formes = new List<Forme_simple>();
            liste_formes.Add(ellipse1);
            liste_formes.Add(polygone1);
            liste_formes.Add(rectangle1);

            Forme_composee groupe1 = new Forme_composee(1, nom_groupe, liste_formes);

            groupe1.Write();

            groupe1.translation(3, 3);

            groupe1.Write();

            Console.ReadLine();

            //Test Base de donnée

            /*DAO<Ellipse> ellipsedao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAOEllipse();
            ellipsedao.create(ellipse1);

            ellipse1.Nom = "ellipse2";

            ellipsedao.update(ellipse1);

            DAO<Rectangle> rectangledao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAORectangle();
            rectangledao.create(rectangle1);

            rectangle1.Nom = "rectangle2";

            rectangledao.update(rectangle1);

            DAO<Polygone> polygonedao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAOPolygone();
            polygonedao.create(polygone1);

            polygone1.Nom = "polygone2";*/

            //Dessiner<Polygone> polygonedessin = AbstractDessinFactory.getFactory(FactoryDessin.DESSIN_FACTORY).getDessinPolygone();
            //polygonedessin.dessiner(polygone1);


            //polygonedao.update(polygone1);
        }
    }
}
