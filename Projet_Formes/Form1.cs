﻿using System;
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

        bool bouton_Mouse_down;
        
        Forme_simple forme_active;
        int id = 1;

        //Gestion Polygones
        int i = 0;
        int nb_points_poly;
        Point[] tabcoord = new Point[] {};

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
            this.forme_active = new Ellipse(id, "Ellipse " + id, panel_couleur.BackColor.ToArgb(), new Point(0, 0), 0, 0);
            this.dessinateur = new DessinEllipse();
            this.id++;
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.nb_points_poly = 3;
            this.tabcoord = new Point[this.nb_points_poly];
            this.forme_active = new Triangle(id, "Triangle " + id, panel_couleur.BackColor.ToArgb(), tabcoord);
            this.dessinateur = new DessinTriangle();
            this.id++;   
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.forme_active = new Rectangle(id, "Rectangle " + id, panel_couleur.BackColor.ToArgb(), new Point(40, 40), 40, 20);
            this.dessinateur = new DessinRectangle();
            this.id++;
        }

        private void segmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.forme_active = new Segment(id, "Segment " + id, panel_couleur.BackColor.ToArgb(), new Point(1, 2), new Point(3, 4));
            this.dessinateur = new DessinSegment();
            this.id++;
        }

        private void polygoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.forme_active = new Polygone(id, "Polygone " + id, panel_couleur.BackColor.ToArgb(), tabcoord);
            this.dessinateur = new DessinPolygone();
            this.id++;
            label10.Visible = true;
            textBoxNbPoints.Visible = true;
            textBoxNbPoints.Focus();
        }

        private void representer(Forme_simple forme, DessinFormeSimple dessin)
        {
            if (forme != null)
            {
                dessin.dessiner(forme, this.panel1.CreateGraphics());
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            point_depart = e.Location;
            labelNom.Focus();   //enleve le focus à textBoxNom

            if (e.Button == MouseButtons.Left)
                bouton_Mouse_down = true;
            else if (e.Button == MouseButtons.Right)
            {
                if (i == this.nb_points_poly - 1)
                {
                    tabcoord[i] = point_depart;
                    forme_active.maj(tabcoord);
                    representer(forme_active, dessinateur);
                    this.i = 0;
                }
                else
                {
                    tabcoord[i] = point_depart;
                    this.i++;
                }
            }

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (bouton_Mouse_down)
            {
                point_arrivee = e.Location;

                bouton_Mouse_down = false;

                Pen pen = new Pen(Color.Black, 10);
                SolidBrush blueBrush = new SolidBrush(Color.Blue);

                g = this.panel1.CreateGraphics();
                

                if (forme_active != null)
                {
                    forme_active.maj(point_depart, point_arrivee);
                    representer(forme_active, dessinateur);
                }

                //panel1.Invalidate(); 

            }
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
            //+-: Zoom
            if (e.KeyData == Keys.Add)
            {
                if (this.forme_active != null)
                {
                    this.forme_active.homothetie(1);

                    representer(forme_active, dessinateur);
                }
            }
            if (e.KeyData == Keys.Subtract)
            {
                if (this.forme_active != null)
                {
                    this.forme_active.homothetie(-1);
                    representer(forme_active, dessinateur);
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

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.nb_points_poly = Convert.ToInt32(textBoxNbPoints.Text);
                textBoxNbPoints.Text = "";
                this.tabcoord = new Point[this.nb_points_poly];
                label10.Visible = false;
                textBoxNbPoints.Visible = false;
            }
        }

    }
}
