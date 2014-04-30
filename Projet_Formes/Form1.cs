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
        int k = 0;

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
            Ellipse nouvelle_forme = new Ellipse(id, "Ellipse", point1, point2, couleur);
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Triangle nouvelle_forme = new Triangle(id, "Triangle", point1, point2, couleur, tabcoord);
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rectangle nouvelle_forme = new Rectangle(id, "Rectangle", point1, point2, couleur);
        }

        private void segmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Segment nouvelle_forme = new Segment(id, "Segment", point1, point2, couleur);
            Console.WriteLine("test" + nb_points_poly);
        }

        private void polygoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Polygone nouvelle_forme = new Polygone(id, "Polygone", point1, point2, couleur, tabcoord);
            label10.Visible = true;
            textBox3.Visible = true;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            point1 = e.Location;

            if (e.Button == MouseButtons.Left)
                k = 1;
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            k = 0;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (k == 1)
            {
                Pen p = new Pen(Color.Black, 10);
                point2 = e.Location;
                g = this.panel1.CreateGraphics();
                g.DrawLine(p, point1, point2);
                
            }
            point1 = point2;
        }

    }
}
