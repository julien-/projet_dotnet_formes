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
            test();

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());            
        }

        static void test()
        {
     
            //test ellipse

            string nom_ellipse = "elipse1";

            string couleur_ellipse = "white";

            Point point1 = new Point(12, 13);
            Point point2 = new Point(14, 15);

            List<Point> liste_points = new List<Point>();
            liste_points.Add(point1);
            liste_points.Add(point2);

            Ellipse ellipse1 = new Ellipse(0, nom_ellipse, liste_points, couleur_ellipse);

            //test polygone

            string nom_polygone = "polygone1";

            string couleur_polygone = "white";

            Point point3 = new Point(16, 17);
            Point point4 = new Point(18, 19);
            Point point5 = new Point(20, 21);

            List<Point> liste_points2 = new List<Point>();
            liste_points2.Add(point3);
            liste_points2.Add(point4);
            liste_points2.Add(point5);

            Polygone polygone1 = new Polygone(1, nom_polygone, liste_points2, couleur_polygone);

            //test rectangle

            string nom_rectangle = "rectangle1";

            string couleur_rectangle = "white";

            Point point6 = new Point(22, 23);
            Point point7 = new Point(24, 25);

            List<Point> liste_points3 = new List<Point>();
            liste_points3.Add(point6);
            liste_points3.Add(point7);

            Rectangle rectangle1 = new Rectangle(2, nom_rectangle, liste_points3, couleur_rectangle);

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

            DAO<Ellipse> ellipsedao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAOEllipse();
            ellipsedao.create(ellipse1);

            ellipse1.Nom = "ellipse2";

            //ellipsedao.update(ellipse1);

            DAO<Rectangle> rectangledao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAORectangle();
            rectangledao.create(rectangle1);

            rectangle1.Nom = "rectangle2";

            rectangledao.update(rectangle1);

            DAO<Polygone> polygonedao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAOPolygone();
            polygonedao.create(polygone1);

            polygone1.Nom = "polygone2";

            polygonedao.update(polygone1);
        }
    }
}
