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

            int couleur_ellipse = 55555;

            Point point1 = new Point(12, 13);
            int longueur_ell = 12;
            int largeur_ell = 10;
            

            Forme_simple ellipse1 = new Ellipse(0, nom_ellipse, couleur_ellipse, point1, longueur_ell, largeur_ell);

            //test polygone

            string nom_polygone = "polygone1";

            int couleur_polygone = 15464;

            Point point3 = new Point(16, 17);
            Point point4 = new Point(18, 19);
            Point point5 = new Point(20, 21);

            Point []tableau_points = new Point[3]{point3, point4, point5};

            Polygone polygone1 = new Polygone(1, nom_polygone, couleur_polygone, tableau_points);

            //test triangle

            string nom_triangle = "triangle1";

            int couleur_triangle = 15464;

            Point point7 = new Point(16, 17);
            Point point8 = new Point(18, 19);
            Point point9 = new Point(20, 21);

            Point[] tableau_points2 = new Point[3] { point3, point4, point5 };

            Triangle triangle1 = new Triangle(3, nom_triangle, couleur_triangle, tableau_points2);

            //test rectangle

            string nom_rectangle = "rectangle1";

            int couleur_rectangle = 4646;

            Point point6 = new Point(22, 23);
            int longueur_rect = 12;
            int largeur_rect = 10;

            Rectangle rectangle1 = new Rectangle(2, nom_rectangle, couleur_rectangle, point6, longueur_rect, largeur_rect);

            //test segment

            string nom_segment = "segment1";

            int couleur_segment = 4646;

            Point point10 = new Point(22, 23);
            Point point11 = new Point(24, 25);

            Segment segment1 = new Segment(4, nom_segment, couleur_segment, point10, point11);

            //test groupe

            string nom_groupe = "groupe1";



            List<Forme_simple> liste_formes = new List<Forme_simple>();
            liste_formes.Add(ellipse1);
            liste_formes.Add(polygone1);
            liste_formes.Add(rectangle1);
            liste_formes.Add(segment1);
            liste_formes.Add(triangle1);

            Forme_composee groupe1 = new Forme_composee(5, nom_groupe);
            groupe1.Liste_formes = liste_formes;


            

            //Test Base de donnée

            Forme_simple test;
            Forme_composee test2;
            
            DAOFormeSimple ellipsedao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAOEllipse();
            ellipsedao.create(ellipse1);

            ellipse1.Nom = "ellipse2";

            ellipsedao.update(ellipse1);

            test = ellipsedao.find(ellipse1.Id);

            test.Write();



            /*DAOFormeSimple rectangledao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAORectangle();
            rectangledao.create(rectangle1);

            rectangle1.Nom = "rectangle2";

            rectangledao.update(rectangle1);

            test = rectangledao.find(rectangle1.Id);

            test.Write();*/

            /*DAOFormeSimple polygonedao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAOPolygone();
            polygonedao.create(polygone1);

            polygone1.Nom = "polygone2";

            polygonedao.update(polygone1);

            test = polygonedao.find(polygone1.Id);

            test.Write();*/

            /*DAOFormeSimple triangledao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAOTriangle();
            triangledao.create(triangle1);

            triangle1.Nom = "triangle2";

            triangledao.update(triangle1);

            test = triangledao.find(triangle1.Id);

            test.Write();*/

            /*DAOFormeSimple segmentdao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAOSegment();
            segmentdao.create(segment1);

            segment1.Nom = "triangle2";

            segmentdao.update(segment1);

            test = segmentdao.find(segment1.Id);

            test.Write();*/

            /*DAOFormeComposee groupedao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAOFormeComposee();
            groupedao.create(groupe1);

            groupe1.Nom = "groupe2";

            groupedao.update(groupe1);

            test2 = groupedao.find(groupe1.Id);

            Console.WriteLine(test2.Id);
            Console.WriteLine(test2.Nom);*/


            Console.ReadLine();
        }
    }
}
