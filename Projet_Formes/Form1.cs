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

        Point point1 = new Point(0, 0);
        Point point2 = new Point(0, 0);
        int id = 1;
        string couleur = "white";
        Point []tabcoord = new Point[] { };
        int nb_points_poly;
        int clicG_actif = 0;    //si le clic gauche est appuyé ou pas
        int clicD_actif = 0;    //si le clic droit est appuyé ou pas
        int id_groupe_actif = -1;   //-1 si non actif sinon valeur de l'id du groupe
        int zoom = 0;   //-1 si on retrecie, 0 si rien, 1 si on agrandit

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
            //Ellipse nouvelle_forme = new Ellipse(id, "Ellipse", point1, point2, couleur);
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Triangle nouvelle_forme = new Triangle(id, "Triangle", point1, point2, couleur, tabcoord);
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Rectangle nouvelle_forme = new Rectangle(id, "Rectangle", point1, point2, couleur);
        }

        private void segmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Segment nouvelle_forme = new Segment(id, "Segment", point1, point2, couleur);
            //Console.WriteLine("test" + nb_points_poly);
        }

        private void polygoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Polygone nouvelle_forme = new Polygone(id, "Polygone", point1, point2, couleur, tabcoord);
            label10.Visible = true;
            textBox3.Visible = true;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //point1 = e.Location;
            
            //Clic gauche
            if (e.Button == MouseButtons.Left)
            {
                clicG_actif = 1;
                //Valeur fictive du nom de la forme
                this.textBox_nom.Text = "blabla"; ;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            clicG_actif = 0;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (clicG_actif == 1)
            {
                Pen p = new Pen(Color.Black, 10);
                //point2 = e.Location;
                g = this.panel1.CreateGraphics();
                //g.FillRectangle(p, point1.X, point1.Y, point2.X, point2.Y);
                
            }
            point1 = point2;
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
