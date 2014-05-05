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
        
        Forme_simple forme_active;
        int id = 1;
        
        int nb_points_poly;
        bool clicG_actif = false;    //si le clic gauche est appuyé ou pas
        bool clicD_actif = false;    //si le clic droit est appuyé ou pas
        bool PeutDessiner;    //creation ou selection de forme
        int id_groupe_actif = -1;   //-1 si non actif sinon valeur de l'id du groupe
        int zoom = 0;   //-1 si on retrecie, 0 si rien, 1 si on agrandit
        DessinFormeSimple dessinateur;
        
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
            textBox_nom.Clear();
            this.forme_active = new Ellipse(id, "Ellipse " + id, panel_couleur.BackColor.ToArgb(), new Point(40, 40), 40, 20);
            this.dessinateur = new DessinEllipse();
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point[] tabcoord = new Point[3] { new Point(3, 4), new Point(3, 200), new Point(140, 200) };
            this.forme_active = new Triangle(id, "Triangle " + id, panel_couleur.BackColor.ToArgb(), tabcoord);
            this.dessinateur =new DessinTriangle();
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.forme_active = new Rectangle(id, "Rectangle " + id, panel_couleur.BackColor.ToArgb(), new Point(40, 40), 40, 20);
            this.dessinateur = new DessinRectangle();
        }

        private void segmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.forme_active = new Segment(id, "Segment " + id, panel_couleur.BackColor.ToArgb(), new Point(1, 2), new Point(3, 4));
            this.dessinateur = new DessinSegment();
        }

        private void polygoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Point[] tabcoord = new Point[5] { new Point(3, 4), new Point(3, 200), new Point(140, 200), new Point(400, 200), new Point(180, 50) };
            this.forme_active = new Polygone(id, "Polygone " + id, panel_couleur.BackColor.ToArgb(), tabcoord);
            this.dessinateur = new DessinPolygone();
            label10.Visible = true;
            textBox3.Visible = true;
        }

        private void representer(Forme_simple forme, DessinFormeSimple dessin)
        {
            if (forme != null)
            {
                dessin.dessiner(forme, this.panel1.CreateGraphics());
                Console.WriteLine(forme.GetType());
                forme.Nom = "test";
                forme.Write();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {//Clic sur le panel de dessin
            point_depart = e.Location;

            if (e.Button == MouseButtons.Left)
            {//Clic Gauche
                clicG_actif = true;
                clicD_actif = false;
                Console.WriteLine("DEPART GAUCHE: (" + point_depart.X + "," + point_depart.Y + ")");
                //if(PeutDessiner)
                majPropriete(forme_active);
                //creerAvecValeurParDefault(forme_active);
                representer(forme_active, dessinateur);
                //else //PEUT PAS DESSINER
            }
            if (e.Button == MouseButtons.Right)
            {//Clic Droit
                clicG_actif = false;
                clicD_actif = true;
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {//Clic appuyé + Mouvement
            point_arrivee = e.Location;

            if (e.Button == MouseButtons.Left)
            {//Clic Gauche
                clicG_actif = true;
                clicD_actif = false;


                Console.WriteLine("MOUVEMENT GAUCHE: de (" + point_depart.X + "," + point_depart.Y + ") à (" + point_arrivee.X + "," + point_arrivee.Y + ")");
                //if(PeutDessiner)


                point_depart = point_arrivee;
            }
            if (e.Button == MouseButtons.Right)
            {//Clic Droit
                clicG_actif = false;
                clicD_actif = true;


                point_depart = point_arrivee;    
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            clicG_actif = false;
            clicD_actif = false;

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

        private void panel_couleur_Click(object sender, EventArgs e)
        {//CHOIX COULEUR
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                //DONNEES
                //forme active
                if(forme_active != null)
                    forme_active.Couleur = colorDialog1.Color.ToArgb();
                
                //VISUEL
                //fond du panel de choix de couleur
                panel_couleur.BackColor = colorDialog1.Color;
                //redessine la forme
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


        private void majPropriete(Forme_simple forme)
        {//Affiche les propriétes de la forme dans la partie DROITE de l'application
            if (forme != null)
            {
                textBox_nom.Text = forme.Nom;
                panel_couleur.BackColor = Color.FromArgb(forme.Couleur);
            }

        }

    }
}
