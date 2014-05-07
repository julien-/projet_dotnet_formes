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
        Graphics g2; 
        Bitmap bm;

        Point point_depart = new Point(0, 0);
        Point point_arrivee = new Point(0, 0);

        bool bouton_Mouse_left;
        bool bouton_Mouse_middle;

        Forme_simple forme_active;
        int id = 1;

        //Gestion Polygones
        int i = 0;
        int nb_points_poly;
        Point[] tabcoord = new Point[] { };

        List<Forme_composee> ListGroupes = new List<Forme_composee>();
        List<Forme_simple> ListFormes = new List<Forme_simple>();

        bool id_groupe_actif = false;   //-1 si non actif sinon valeur de l'id du groupe
        DessinFormeSimple dessinateur;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            g = this.panel1.CreateGraphics();
            bm = new Bitmap(this.panel1.Width, this.panel1.Height);
            g2 = Graphics.FromImage(bm);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
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
                dessin.dessiner(forme, g2);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            g.DrawImage(bm, 0, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            point_depart = e.Location;
            labelNom.Focus();   //enleve le focus à textBoxNom

            if (e.Button == MouseButtons.Left)
                bouton_Mouse_left = true;
            else if (e.Button == MouseButtons.Right)
            {
                if (i == this.nb_points_poly - 1)
                {
                    tabcoord[i] = point_depart;
                    forme_active.maj(tabcoord);
                    representer(forme_active, dessinateur);
                    panel1.Invalidate();
                    this.i = 0;
                }
                else
                {
                    tabcoord[i] = point_depart;
                    this.i++;
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                bouton_Mouse_middle = true;
                foreach (Forme_simple forme in ListFormes)
                {
                    if (forme.recuperer(point_depart.X, point_depart.Y))
                    {
                        forme_active = forme;
                        break;
                    }
                }
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            point_arrivee = e.Location;
            if (bouton_Mouse_left)
            {
                bouton_Mouse_left = false;

                g = this.panel1.CreateGraphics();


                if (forme_active != null)
                {
                    ListFormes.Add(forme_active);
                    forme_active.maj(point_depart, point_arrivee);
                    representer(forme_active, dessinateur);
                    this.id_groupe_actif = false;
                    this.labelNomGroupeActif.Text = "Aucun";
                    majPropriete(this.forme_active);
                    panel1.Invalidate();
                }
            }
            else if (bouton_Mouse_middle)
            {
                bouton_Mouse_middle = false;
                forme_active.translation(point_depart, point_arrivee);
                representer(forme_active, dessinateur);
                panel1.Invalidate();
            }
        }

        private void panel_couleur_Click(object sender, EventArgs e)
        {//CHOIX COULEUR
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                //DONNEES
                //forme active
                if (forme_active != null)
                    forme_active.Couleur = colorDialog1.Color.ToArgb();

                //VISUEL
                //fond du panel de choix de couleur
                panel_couleur.BackColor = colorDialog1.Color;
                //redessine la forme
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ////Une touche est enfoncée
            if (e.KeyCode == Keys.G && e.Modifiers == Keys.Control)
            {
                if (!id_groupe_actif && (this.forme_active != null)) //inactif
                {   //si inactif, on active
                    labelGroupeActif.Text = "Groupe Actif";
                    id_groupe_actif = true;
                    lierGroupeToolStripMenuItem.Text = "Lier le groupe";
                    labelNomGroupeActif.Text = this.ListGroupes.Find(item => item.Id == forme_active.IdGroupe).Nom;
                }
                else
                {   //sinon on desactive
                    labelGroupeActif.Text = "Groupe Inactif";
                    labelNomGroupeActif.Text = "Aucun";
                    id_groupe_actif = false;
                    lierGroupeToolStripMenuItem.Text = "Lier la forme";
                }
            }
            //Zoom
            if (e.KeyData == Keys.Up)
            {
                if (this.forme_active != null)
                {
                    this.forme_active.homothetie(1);
                    representer(forme_active, dessinateur);
                    panel1.Invalidate();
                }
            }
            //Dezoom
            if (e.KeyData == Keys.Down)
            {
                if (this.forme_active != null)
                {
                    g2.Clear(Color.White);
                    this.forme_active.homothetie(-1);
                    representer(forme_active, dessinateur);
                    panel1.Invalidate();
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

        private void toolStripComboBoxGroupes_Click(object sender, EventArgs e)
        {

        }

        private void ajouterUnGroupeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            labelCreationGroupe.Visible = true;
            textBoxCreationGroupe.Visible = true;
            textBoxCreationGroupe.Focus();
        }

        private void textBoxCreationGroupe_KeyDown(object sender, KeyEventArgs e)
        {//Creation de Groupe
            if (e.KeyCode == Keys.Enter)
            {
                this.ListGroupes.Add(new Forme_composee(this.id, textBoxCreationGroupe.Text));
                labelCreationGroupe.Visible = false;
                textBoxCreationGroupe.Visible = false;
                //Ajouts
                this.toolStripComboBoxGroupes.Items.Add(textBoxCreationGroupe.Text);
                this.SupprimerGroupetoolStripComboBox.Items.Add(textBoxCreationGroupe.Text);
                if (this.forme_active != null)
                {
                    this.forme_active.IdGroupe = this.id;
                }
                Console.WriteLine("CREATION GROUPE: "+this.id+" : "+this.textBoxCreationGroupe.Text);
                //Prepare pour les suivants
                textBoxCreationGroupe.Clear();
                this.id++;
            }
        }

        private void toolStripComboBoxGroupes_SelectedIndexChanged(object sender, EventArgs e)
        {//Selection d'un Groupe Actif différent que celui actif
            if (toolStripComboBoxGroupes.SelectedIndex != 0 && this.forme_active!= null)
            {   //LISTE DE TOUS LES GROUPES DE  LA FORMEACTIVE
                //label_groupe_actif.Text = toolStripComboBoxGroupes.SelectedItem.ToString();

                //Selection autre que Aucun
                if (this.id_groupe_actif)
                {//Affectation d'un GROUPE à un SOUS-GROUPE
                    //Liaison du SOUS-GROUPE(groupe de la forme active) au GROUPE
                    this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).IdGroupe =  this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Id; //ID dans ListeGroupes du groupe séléctionné dans la liste déroulante
                    Console.WriteLine("Affectation SOUS-GROUPE(" + this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Id + ", " + this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Nom + ") au GROUPEMAITRE(" + this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Id + ", " + this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Nom + ")");
                }
                else
                {//Affectation d'un GROUPE à une FORME
                    //Liaison de la FORMEACTIVE au GROUPE
                    this.forme_active.IdGroupe = this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Id; //ID dans ListeGroupes du groupe séléctionné dans la liste déroulante
                    Console.WriteLine("Affectation FORMEACTIVE(" + this.forme_active.Id + ") au GROUPE(" + this.ListGroupes[toolStripComboBoxGroupes.SelectedIndex - 1].Id + ", " + this.ListGroupes[toolStripComboBoxGroupes.SelectedIndex - 1].Nom + ")");
                }
            }
            else
            {//AUCUN: forme_active n'appartient plus à un groupe
                this.id_groupe_actif = false;
                this.labelNomGroupeActif.Text = "Aucun";
                if (this.forme_active != null)
                {
                    //Liaison de la FORMEACTIVE au GROUPE -1
                    this.forme_active.IdGroupe = -1;
                    Console.WriteLine("Desaffectaction: FORMEACTIVE("+this.forme_active.Id + ") au GROUPE -1");
                }
            }
        }

        private void SupprimerGroupetoolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {//Suppression de groupe
            int IDdugroupe = this.ListGroupes.Find(item => item.Nom == SupprimerGroupetoolStripComboBox.SelectedItem.ToString()).Id;
            String NOMdugroupe = this.ListGroupes.Find(item => item.Nom == SupprimerGroupetoolStripComboBox.SelectedItem.ToString()).Nom;
            Console.WriteLine("Suppression GROUPE(" + IDdugroupe + ", " + NOMdugroupe + ")");
            //Suppression dans la liste des groupes
            this.ListGroupes.Remove(this.ListGroupes.Find(item => item.Id == IDdugroupe));
            //Modifie tous les sous groupes où le groupe était lié
            this.ListGroupes.Where(item => item.IdGroupe == IDdugroupe).ToList().ForEach(g => g.IdGroupe = -1);
            //Modifie toutes les formes liées au groupe
            this.ListFormes.Where(item => item.IdGroupe == IDdugroupe).ToList().ForEach(g => g.IdGroupe = -1);
            //Modifie forme active
            if(this.forme_active != null)
            {
                if (this.forme_active.IdGroupe == IDdugroupe)
                    this.forme_active.IdGroupe = -1;
            }


            //VISUEL
            //Supprime de la liste déroulante de Liaison de Groupe
            this.toolStripComboBoxGroupes.Items.Remove(NOMdugroupe);
            //Label du groupe actif de la forme active (au cas ou)
            if (labelNomGroupeActif.Text == NOMdugroupe)
            {
                this.labelNomGroupeActif.Text = "Aucun";
                this.id_groupe_actif = false;
            }
            //Supprime de la liste déroulante des groupes à supprimer
            this.SupprimerGroupetoolStripComboBox.Items.Remove(NOMdugroupe);
        }
    }
}
