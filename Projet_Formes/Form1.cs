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
        Point point1 = new Point(0, 0);
        Point point2 = new Point(0, 0);
        string couleur = "white";
        Point[] tabcoord;

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
            //Ellipse nouvelle_forme = new Ellipse("Ellipse", point1, point2, couleur);
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Triangle nouvelle_forme = new Triangle("Triangle", point1, point2, couleur, tabcoord);
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Rectangle nouvelle_forme = new Rectangle("Rectangle", point1, point2, couleur);
        }

        private void segmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Segment nouvelle_forme = new Segment("Segment", point1, point2, couleur);
        }

        private void polygoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Polygone nouvelle_forme = new Polygone("Triangle", point1, point2, couleur, tabcoord);
            //label10.Visible = true;
            //textBox3.Visible = true;
        }
    }
}
