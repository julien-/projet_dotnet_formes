using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projet_Formes
{
    public partial class Form1 : Form
    {
        Graphics g; //Bibliothèque pour dessiner des formes

        Point point_depart = new Point(0, 0);
        Point point_arrivee = new Point(0, 0);

        Forme_simple nouvelle_forme;
        int id = 1;
        string couleur = "white";
        
        int nb_points_poly;
        int clicG_actif = 0;    //si le clic gauche est appuyé ou pas
        int clicD_actif = 0;    //si le clic droit est appuyé ou pas
        int id_groupe_actif = -1;   //-1 si non actif sinon valeur de l'id du groupe
        int zoom = 0;   //-1 si on retrecie, 0 si rien, 1 si on agrandit
        bool bouton_Mouse_down;

        public Form1()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ellipse nouvelle_forme = new Ellipse(id, "Ellipse", couleur, new Point(40, 40), 40, 20);
            Dessiner<Ellipse> ellipsedessin = AbstractDessinFactory.getFactory(FactoryDessin.DESSIN_FACTORY).getDessinEllipse();
            g = this.panel1.CreateGraphics();
            ellipsedessin.dessiner(nouvelle_forme, g);
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point[] tabcoord = new Point[3] { new Point(3, 4), new Point(3, 200), new Point(140, 200) };
            Triangle nouvelle_forme = new Triangle(id, "Triangle", couleur, tabcoord);
            Dessiner<Triangle> triangledessin = AbstractDessinFactory.getFactory(FactoryDessin.DESSIN_FACTORY).getDessinTriangle();
            g = this.panel1.CreateGraphics();
            triangledessin.dessiner(nouvelle_forme, g);
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rectangle nouvelle_forme = new Rectangle(id, "Rectangle", couleur, new Point(40,40), 40, 20);
            Dessiner<Rectangle> rectangledessin = AbstractDessinFactory.getFactory(FactoryDessin.DESSIN_FACTORY).getDessinRectangle();
            g = this.panel1.CreateGraphics();
            rectangledessin.dessiner(nouvelle_forme, g);
        }

        private void segmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Segment nouvelle_forme = new Segment(id, "Segment", couleur, new Point(1, 2), new Point(3, 4));
            Dessiner<Segment> segmentdessin = AbstractDessinFactory.getFactory(FactoryDessin.DESSIN_FACTORY).getDessinSegment();
            g = this.panel1.CreateGraphics();
            segmentdessin.dessiner(nouvelle_forme, g);
            creer(nouvelle_forme);
        }

        private void polygoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point[] tabcoord = new Point[5] { new Point(3, 4), new Point(3, 200), new Point(140, 200), new Point(400, 200), new Point(180, 50) };
            Polygone nouvelle_forme = new Polygone(id, "Polygone", couleur, tabcoord);
            Dessiner<Polygone> polygonedessin = AbstractDessinFactory.getFactory(FactoryDessin.DESSIN_FACTORY).getDessinPolygone();
            g = this.panel1.CreateGraphics();
            polygonedessin.dessiner(nouvelle_forme, g);
            label10.Visible = true;
            textBox3.Visible = true;
            creer(nouvelle_forme);
        }

        private void creer(Forme_simple forme)
        {
            Console.WriteLine(forme.GetType());
            forme.Nom = "test";
            forme.Write();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            point_depart.X = e.Location.X;
            point_depart.Y = e.Location.Y;

            if (e.Button == MouseButtons.Left)
                bouton_Mouse_down = true;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (bouton_Mouse_down)
            {
                bouton_Mouse_down = false;

                Pen pen = new Pen(Color.Black, 10);
                SolidBrush blueBrush = new SolidBrush(Color.Blue);
                point_arrivee.X = e.Location.X;
                point_arrivee.Y = e.Location.Y;
                g = this.panel1.CreateGraphics();
                int longueur = point_arrivee.X - point_depart.X;
                int largeur = point_arrivee.Y - point_depart.Y;

                //g.FillRectangle(blueBrush, point_depart.X , point_depart.Y , longueur, largeur);
                //g.FillEllipse(blueBrush, point_depart.X, point_depart.Y, longueur, largeur);
                //g.DrawLine(pen, point_depart.X, point_depart.Y, point_arrivee.X, point_arrivee.Y);

                //panel1.Invalidate(); 

            }
        }

        private void panel_couleur_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                panel_couleur.BackColor = colorDialog1.Color;
                textBox_nom.Text = colorDialog1.Color.Name;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Une touche est enfoncée
            if (e.KeyCode == Keys.G && e.Modifiers == Keys.Control)
            {
                if (id_groupe_actif < 0) //inactif
                {   //si inactif, on active
                    label_groupe_actif.Text = "Actif";
                    id_groupe_actif = 2;
                }
                else
                {   //sinon on desactive
                    label_groupe_actif.Text = "Inactif";
                    id_groupe_actif = -1;
                }
            }
        }

    }
}
