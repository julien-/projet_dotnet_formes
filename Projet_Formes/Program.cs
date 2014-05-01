using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Text;
using System.Threading.Tasks;



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

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            
        }

        static void test()
        {
            string nomelipse = "elipse1";
            string nompoly = "polygone1";
            string nomrectangle = "rect1";
            Point point1 = new Point(12, 13);
            Point point2 = new Point(14, 15);
            Point point3 = new Point(16, 17);
            Point point4 = new Point(18, 19);
            Point point5 = new Point(20, 21);
            Point point6 = new Point(22, 23);
            string couleur = "white";
            //2 points
            List<Point> listcoord1 = new List<Point>();
            listcoord1.Add(point1);
            listcoord1.Add(point2);
            //3 points
            List<Point> listcoord2 = new List<Point>();
            listcoord2.Add(point1);
            listcoord2.Add(point2);
            listcoord2.Add(point3);
            //6 points
            List<Point> listcoord3 = new List<Point>();
            listcoord3.Add(point1);
            listcoord3.Add(point2);
            listcoord3.Add(point3);
            listcoord3.Add(point4);
            listcoord3.Add(point5);
            listcoord3.Add(point6);
            //test elipse

            Ellipse elipse1 = new Ellipse(0, nomelipse, couleur, listcoord1);

            elipse1.Write();

            elipse1.translation(2, 2);

            elipse1.Write();





            Ellipse elipse2 = new Ellipse(5, nomelipse, couleur, listcoord1);

            elipse2.Write();

            elipse2.translation(2, 2);

            elipse2.Write();

            //test polygone

            Polygone pol1 = new Polygone(1, nompoly, couleur, listcoord3);

            pol1.Write();

            pol1.translation(3, 3);

            pol1.Write();

            //test groupe
            Forme_simple[] tabformes = new Forme_simple[] { pol1, elipse1, elipse2 };
            Forme_composee groupe1 = new Forme_composee(2, "groupe1", tabformes);

            groupe1.Write();

            groupe1.translation(3, 3);

            groupe1.Write();

            Console.ReadLine();

            Rectangle rect1 = new Rectangle(3, nomrectangle, couleur, listcoord1);

            //DAO<Rectangle> rectdao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAORectangle();
            //rectdao.create(rect1);

            DAO<Ellipse> elipsedao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAOEllipse();
            //REQUETES DANS LA BDD

            //test INSERT
            elipsedao.create(elipse1);
            //test UPDATE
            elipse1.Nom = "aaaaaaa";
            elipsedao.update(elipse1);
            //test INSERT après UPDATE
            elipsedao.create(elipse2);
            //test DELETE
            //elipsedao.delete(elipse1);
            //elipsedao.delete(elipse2);
            //test SELECT
            Ellipse f = elipsedao.find(0);
            f.Id = 9;
            f.Nom = "modif";
            elipsedao.create(f);

            DAO<Polygone> polygonedao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAOPolygone();
            //POLYGONE
            Polygone polygone1 = new Polygone(22, "polygone1", "blue", listcoord3);
            polygonedao.create(polygone1);


        }
    }
}
