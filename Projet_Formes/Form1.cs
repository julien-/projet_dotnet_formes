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
        Graphics g3;

        Bitmap bm;
        Bitmap bm2;

        Point point_depart = new Point(0, 0);
        Point point_arrivee = new Point(0, 0);

        bool bouton_Mouse_left;
        bool bouton_Mouse_middle;
        bool selected;
        bool GroupeActif = false;   //-1 si non actif sinon valeur de l'id du groupe
        bool PeutDessiner = false;

        Forme_simple forme_active;
        int id = 1;

        //Gestion Polygones
        int i = 0;
        int nb_points_poly;
        Point[] tabcoord = new Point[] { };

        List<Forme_composee> ListGroupes = new List<Forme_composee>();
        List<Forme_simple> ListFormes = new List<Forme_simple>();

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
            bm2 = new Bitmap(this.panel1.Width, this.panel1.Height);
            g3 = Graphics.FromImage(bm2);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox_nom.Clear();
            this.forme_active = new Ellipse(id, "Ellipse " + id, panel_couleur.BackColor.ToArgb(), new Point(0, 0), 0, 0);
            forme_active.Dessinateur = new DessinEllipse();
            this.id++;
            ListFormes.Add(this.forme_active);
            panel1.Cursor = System.Windows.Forms.Cursors.Cross;   //PASSE EN MODE DESSIN
            this.PeutDessiner = true;
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.nb_points_poly = 3;
            this.tabcoord = new Point[this.nb_points_poly];
            this.forme_active = new Triangle(id, "Triangle " + id, panel_couleur.BackColor.ToArgb(), tabcoord);
            forme_active.Dessinateur = new DessinTriangle();
            this.id++;
            ListFormes.Add(this.forme_active);
            panel1.Cursor = System.Windows.Forms.Cursors.Cross;   //PASSE EN MODE DESSIN
            this.PeutDessiner = true;
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.forme_active = new Rectangle(id, "Rectangle " + id, panel_couleur.BackColor.ToArgb(), new Point(40, 40), 40, 20);
            forme_active.Dessinateur = new DessinRectangle();
            this.id++;
            ListFormes.Add(this.forme_active);
            panel1.Cursor = System.Windows.Forms.Cursors.Cross;   //PASSE EN MODE DESSIN
            this.PeutDessiner = true;
        }

        private void segmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.forme_active = new Segment(id, "Segment " + id, panel_couleur.BackColor.ToArgb(), new Point(1, 2), new Point(3, 4));
            forme_active.Dessinateur = new DessinSegment();
            this.id++;
            ListFormes.Add(this.forme_active);
            panel1.Cursor = System.Windows.Forms.Cursors.Cross;   //PASSE EN MODE DESSIN
            this.PeutDessiner = true;
        }

        private void polygoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.forme_active = new Polygone(id, "Polygone " + id, panel_couleur.BackColor.ToArgb(), tabcoord);
            forme_active.Dessinateur = new DessinPolygone();
            this.id++;
            ListFormes.Add(this.forme_active);
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

            if (e.Button == MouseButtons.Left && this.PeutDessiner)
                bouton_Mouse_left = true;
            else if (e.Button == MouseButtons.Right && this.PeutDessiner)
            {//Dessin de Triangle/Polygone
                if (i == this.nb_points_poly - 1)
                {
                    tabcoord[i] = point_depart;
                    forme_active.maj(tabcoord);
                    representer(forme_active, forme_active.Dessinateur);
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
                selected = true;
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
            if (bouton_Mouse_left && PeutDessiner)
            {
                bouton_Mouse_left = false;

                if (forme_active != null)
                {   //Dessin de Segments, Ellipse, Rectangle
                    forme_active.maj(point_depart, point_arrivee);
                    representer(forme_active, forme_active.Dessinateur);
                    this.GroupeActif = false;
                    this.labelNomGroupeActif.Text = "Aucun";
                    this.toolStripComboBoxGroupes.SelectedItem = "Aucun";
                    majPropriete(this.forme_active);
                    panel1.Invalidate();
                }
            }
            else if (bouton_Mouse_middle)
            {
                bouton_Mouse_middle = false;
                if (forme_active != null)
                {
                    forme_active.translation(point_depart, point_arrivee);
                    majPropriete(forme_active);
                }
                g2.Clear(Color.White);
                panel1.Invalidate();

                foreach (Forme_simple forme in ListFormes)
                {
                    forme.Dessinateur.dessiner(forme, g2);   
                }
                panel1.Invalidate();
            }
            this.PeutDessiner = false;
            panel1.Cursor = System.Windows.Forms.Cursors.Default;   //QUITTE LE MODE DESSIN
        }

        private void panel_couleur_Click(object sender, EventArgs e)
        {//CHOIX COULEUR
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                //DONNEES
                //forme active
                if (forme_active != null)
                {
                    forme_active.Couleur = colorDialog1.Color.ToArgb();
                }
                //VISUEL
                //fond du panel de choix de couleur
                panel_couleur.BackColor = colorDialog1.Color;
                //redessine la forme
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            ///Ctrl+G (Controler le groupe de la forme active)
            if (e.KeyCode == Keys.G && e.Modifiers == Keys.Control)
            {
                if (!GroupeActif && (this.forme_active != null))
                {   //si inactif, on active
                    labelGroupeActif.Text = "Groupe Actif";
                    GroupeActif = true;
                    lierGroupeToolStripMenuItem.Text = "Lier le groupe";
                    //aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa
                }
                else
                {   //sinon on desactive
                    labelGroupeActif.Text = "Groupe Inactif";
                    GroupeActif = false;
                    lierGroupeToolStripMenuItem.Text = "Lier la forme";
                }
                majListeAjoutGroupes();
            }
            
            if (selected)
            {
                //Zoom
                if (e.KeyData == Keys.Up)
                {
                    if (this.forme_active != null)
                    {
                        this.forme_active.homothetie(1);
                        g2.Clear(Color.White);
                        panel1.Invalidate();

                        foreach (Forme_simple forme in ListFormes)
                        {
                            forme.Dessinateur.dessiner(forme, g2);
                        }
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
                        g2.Clear(Color.White);
                        panel1.Invalidate();

                        foreach (Forme_simple forme in ListFormes)
                        {
                            forme.Dessinateur.dessiner(forme, g2);
                        }
                        panel1.Invalidate();
                    }
                }
            }
        }


        private void majPropriete(Forme_simple forme)
        {//Affiche les propriétes de la forme dans la partie DROITE de l'application
            if (forme != null)
            {
                textBox_nom.Text = forme.Nom;
                panel_couleur.BackColor = Color.FromArgb(forme.Couleur);
                if (forme.IdGroupe == -1)
                    labelNomGroupeActif.Text = "Aucun";
                else
                    labelNomGroupeActif.Text = this.ListGroupes.Find(item => item.Id == forme.IdGroupe).Nom;
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
                panel1.Cursor = System.Windows.Forms.Cursors.Cross;   //PASSE EN MODE DESSIN
                this.PeutDessiner = true;
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
            if (toolStripComboBoxGroupes.SelectedIndex != 0)
            {   //Selection autre que Aucun : Affectation
                if (this.forme_active != null)
                {
                    if (this.GroupeActif)
                    {//Affectation d'un GROUPE à un SOUS-GROUPE
                        //Liaison du SOUS-GROUPE(groupe de la forme active) au GROUPE
                        this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).IdGroupe = this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Id; //ID dans ListeGroupes du groupe séléctionné dans la liste déroulante
                        Console.WriteLine("Affectation SOUS-GROUPE(" + this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Id + ", " + this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Nom + ") au GROUPEMAITRE(" + this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Id + ", " + this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Nom + ")");
                    }
                    else
                    {//Affectation d'un GROUPE à une FORME
                        //Liaison de la FORMEACTIVE au GROUPE
                        this.forme_active.IdGroupe = this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Id; //ID dans ListeGroupes du groupe séléctionné dans la liste déroulante
                        Console.WriteLine("Affectation FORMEACTIVE(" + this.forme_active.Id + ") au GROUPE(" + this.ListGroupes[toolStripComboBoxGroupes.SelectedIndex - 1].Id + ", " + this.ListGroupes[toolStripComboBoxGroupes.SelectedIndex - 1].Nom + ")");
                    }
                    majPropriete(this.forme_active);
                }
            }
            else
            {//Desaffectation
                if (this.forme_active != null)
                {
                    if (this.GroupeActif)
                    {
                        //Liaison du Groupe de la FORMEACTIVE au GROUPE -1
                        this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).IdGroupe = -1; //ID dans ListeGroupes du groupe séléctionné dans la liste déroulante
                        Console.WriteLine("Desaffectaction: SOUS-GROUPE(" + this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Id + ", "+ this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Nom +") au GROUPE -1");
                    }
                    else
                    {
                        //Liaison de la FORMEACTIVE au GROUPE -1
                        this.forme_active.IdGroupe = -1;
                        Console.WriteLine("Desaffectaction: FORMEACTIVE("+this.forme_active.Id + ") au GROUPE -1");
                        this.labelNomGroupeActif.Text = "Aucun";

                    }
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
                this.GroupeActif = false;
            }
            //Supprime de la liste déroulante des groupes à supprimer
            this.SupprimerGroupetoolStripComboBox.Items.Remove(NOMdugroupe);
        }

        public void majListeAjoutGroupes()
        {
            this.toolStripComboBoxGroupes.Items.Clear();
            this.toolStripComboBoxGroupes.Items.Add("Aucun");
            foreach (Forme_composee g in ListGroupes)
            {
                if (this.forme_active.IdGroupe == g.Id && GroupeActif)
                {//Un groupe ne peut pas se lier à soi-même
                    //On ne fait donc rien
                   // this.toolStripComboBoxGroupes.Items.Remove(this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Nom);
                }
                else
                {
                    this.toolStripComboBoxGroupes.Items.Add(g.Nom);
                }
            }
        }
    }
}
