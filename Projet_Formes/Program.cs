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
            DAOFormeSimple Fs1 = new DAORectangle();
            DAOFormeSimple Fs2 = new DAOSegment();
            DAOFormeSimple Fs3 = new DAOEllipse();
            DAOFormeSimple Fs4 = new DAOTriangle();
            DAOFormeSimple Fs5 = new DAOPolygone();

            Fs1.SetSuccessor(Fs2);
            Fs2.SetSuccessor(Fs3);
            Fs3.SetSuccessor(Fs4);
            Fs4.SetSuccessor(Fs5);

            //test ellipse

            string nom_ellipse = "elipse1";

            int couleur_ellipse = 55555;

            Point point1 = new Point(12, 13);
            int longueur_ell = 12;
            int largeur_ell = 10;


            Forme_simple ellipse1 = new Ellipse(0, nom_ellipse, couleur_ellipse, point1, longueur_ell, largeur_ell, -1);

            //test polygone

            string nom_polygone = "polygone1";

            int couleur_polygone = 15464;

            Point point3 = new Point(16, 17);
            Point point4 = new Point(18, 19);
            Point point5 = new Point(20, 21);

            Point []tableau_points = new Point[3]{point3, point4, point5};

            Polygone polygone1 = new Polygone(1, nom_polygone, couleur_polygone, tableau_points, -1);

            //test triangle

            string nom_triangle = "triangle1";

            int couleur_triangle = 15464;

            Point point7 = new Point(16, 17);
            Point point8 = new Point(18, 19);
            Point point9 = new Point(20, 21);

            Point[] tableau_points2 = new Point[3] { point3, point4, point5 };

            Triangle triangle1 = new Triangle(3, nom_triangle, couleur_triangle, tableau_points2, -1);

            //test rectangle

            string nom_rectangle = "rectangle1";

            int couleur_rectangle = 4646;

            Point point6 = new Point(22, 23);
            int longueur_rect = 12;
            int largeur_rect = 10;

            Rectangle rectangle1 = new Rectangle(2, nom_rectangle, couleur_rectangle, point6, longueur_rect, largeur_rect, -1);

            //test segment

            string nom_segment = "segment1";

            int couleur_segment = 4646;

            Point point10 = new Point(22, 23);
            Point point11 = new Point(24, 25);

            Segment segment1 = new Segment(4, nom_segment, couleur_segment, point10, point11, -1);

            //test groupe

            string nom_groupe = "groupe1";

            List<Forme_simple> liste_formes2 = new List<Forme_simple>();

            List<Forme> liste_formes = new List<Forme>();
            liste_formes.Add(ellipse1);
            liste_formes.Add(polygone1);
            liste_formes.Add(rectangle1);
            liste_formes.Add(segment1);
            liste_formes.Add(triangle1);

            Forme_composee groupe1 = new Forme_composee(5, nom_groupe, -1, liste_formes);


            foreach (Forme_simple list1 in liste_formes)
            {
                Fs1.create(list1);
            }

            //foreach(Forme_simple list in liste_formes)
            //{
            //       forme = Fs1.find();
            //       forme.Write();
            //       //liste_formes2.Add(forme);
            //}

            /*foreach (Forme_simple list2 in liste_formes2)
            {
                Console.WriteLine(list2.Id);
            }*/

            Console.ReadLine();
        }
    }
}
