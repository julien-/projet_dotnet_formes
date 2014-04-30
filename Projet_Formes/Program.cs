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
            Point[] tabcoord = new Point[] { point5, point6 };


            //test elipse

            Ellipse elipse1 = new Ellipse(0, nomelipse, point1, point2, couleur);

            elipse1.Write();

            elipse1.translation(2, 2);

            elipse1.Write();

            //test polygone

            Polygone pol1 = new Polygone(1, nompoly, point3, point4, couleur, tabcoord);

            pol1.Write();

            pol1.translation(3, 3);

            pol1.Write();

            //test groupe
            Forme_simple[] tabformes = new Forme_simple[] { pol1, elipse1 };
            Forme_composee groupe1 = new Forme_composee(2, "groupe1", tabformes);

            groupe1.Write();

            groupe1.translation(3, 3);

            groupe1.Write();

            Console.ReadLine();

            Rectangle rect1 = new Rectangle(3, nomrectangle, point3, point4, couleur);

            //DAO<Rectangle> rectdao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAORectangle();
            //rectdao.create(rect1);

            DAO<Ellipse> elipsedao = AbstractDAOFactory.getFactory(FactoryType.DAO_FACTORY).getDAOEllipse();
            elipsedao.create(elipse1); //requete dans la BDD
        }
    }
}
