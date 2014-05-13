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
        bool selected;        
        bool PeutDessiner = false;

        Forme_simple forme_active;
        int id = 1;
        int GroupeActif = -1;   //-1 si non actif sinon valeur de l'id du groupe

        //Gestion Polygones
        int i = 0;
        int nb_points_poly;
        Point[] tabcoord = new Point[] { };

        List<Forme_composee> ListGroupes = new List<Forme_composee>();
        List<Forme_simple> ListFormes = new List<Forme_simple>();

        //DAO
        DAOFormeSimple Fs1 = new DAORectangle();
        DAOFormeSimple Fs2 = new DAOSegment();
        DAOFormeSimple Fs3 = new DAOEllipse();
        DAOFormeSimple Fs4 = new DAOTriangle();
        DAOFormeSimple Fs5 = new DAOPolygone();

        DAOFormeComposee Fc = new DAOFormeComposee();

        //Dessin
        DessinFormeSimple D1 = new DessinRectangle();
        DessinFormeSimple D2 = new DessinSegment();
        DessinFormeSimple D3 = new DessinEllipse();
        DessinFormeSimple D4 = new DessinTriangle();
        DessinFormeSimple D5 = new DessinPolygone();

        public Form1()
        {
            InitializeComponent();

            //Successeurs DAO
            Fs1.SetSuccessor(Fs2);
            Fs2.SetSuccessor(Fs3);
            Fs3.SetSuccessor(Fs4);
            Fs4.SetSuccessor(Fs5);

            //Successeurs Dessin
            D1.SetSuccessor(D2);
            D2.SetSuccessor(D3);
            D3.SetSuccessor(D4);
            D4.SetSuccessor(D5);
        }

        private void Form1_Load(object sender, EventArgs e) //attaché au Formulaire
        {
            g = this.panel1.CreateGraphics();
            bm = new Bitmap(this.panel1.Width, this.panel1.Height);
            g2 = Graphics.FromImage(bm);
        }

        private void ellipseToolStripMenuItem_Click(object sender, EventArgs e) //attaché à l'item Dessin Ellipse
        {
            textBox_nom.Clear();
            this.forme_active = new Ellipse(id, "Ellipse " + id, Color.Black.ToArgb(), new Point(0, 0), 0, 0);
            this.id++;
            activer_dessin();
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)    //attaché à l'item Dessin Triangle
        {
            this.nb_points_poly = 3;
            this.tabcoord = new Point[this.nb_points_poly];
            this.forme_active = new Triangle(id, "Triangle " + id, Color.Black.ToArgb(), tabcoord);
            this.id++;
            activer_dessin();
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)   //attaché à l'item Dessin Rectangle
        {
            this.forme_active = new Rectangle(id, "Rectangle " + id, Color.Black.ToArgb(), new Point(0, 0), 0, 0);
            this.id++;
            activer_dessin();
        }

        private void segmentToolStripMenuItem_Click(object sender, EventArgs e) //attaché à l'item Dessin Segment
        {
            this.forme_active = new Segment(id, "Segment " + id, Color.Black.ToArgb(), new Point(0, 0), new Point(0, 0));
            this.id++;
            activer_dessin();
        }

        private void polygoneToolStripMenuItem_Click(object sender, EventArgs e)    //attaché à l'item Dessin Polygone
        {
            this.forme_active = new Polygone(id, "Polygone " + id, Color.Black.ToArgb(), tabcoord);
            this.id++;
            label10.Visible = true;
            textBoxNbPoints.Visible = true;
            textBoxNbPoints.Focus();
        }

        private void activer_dessin()
        {
            panel1.Cursor = System.Windows.Forms.Cursors.Cross;   //PASSE EN MODE DESSIN
            this.PeutDessiner = true;
            if (this.GroupeActif != -1)
            {
                labelGroupeActif.Text = "Groupe Inactif";
                this.GroupeActif = -1;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)  //attaché au panel de dessin
        {
            g.DrawImage(bm, 0, 0);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)  //attaché à panel de dessin
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
                    ListFormes.Add(this.forme_active);
                    D1.dessiner(forme_active, g2);
                    panel1.Invalidate();
                    this.i = 0;
                    this.PeutDessiner = false;
                    panel1.Cursor = System.Windows.Forms.Cursors.Default;
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
                
                //Selectionne une forme
                foreach (Forme_simple forme in ListFormes)
                {
                    if (forme.recuperer(point_depart.X, point_depart.Y))
                    {
                        if (this.GroupeActif != -1)
                        {
                            if(forme.IdGroupe == this.GroupeActif)
                            {
                                selected = true;
                                forme_active = forme;
                                break;
                            }
                        }
                        else
                        {
                            selected = true;
                            forme_active = forme;
                            break;
                        }
                    }
                }
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)    //attaché à panel de dessin
        {
            point_arrivee = e.Location;
            if (bouton_Mouse_left && PeutDessiner)
            {
                bouton_Mouse_left = false;

                if (forme_active != null)
                {   //Dessin de Segments, Ellipse, Rectangle
                    forme_active.maj(point_depart, point_arrivee);
                    ListFormes.Add(this.forme_active);
                    D1.dessiner(forme_active, g2);
                    this.GroupeActif = -1;
                    this.labelNomGroupeActif.Text = "Aucun";
                    this.toolStripComboBoxGroupes.SelectedItem = "Aucun";
                    majPropriete(this.forme_active);
                    panel1.Invalidate();
                }
                this.PeutDessiner = false;
                panel1.Cursor = System.Windows.Forms.Cursors.Default;
            }
            else if (bouton_Mouse_middle && selected)
            {//TRANSLATION
                bouton_Mouse_middle = false;
                if (forme_active != null)
                {
                    if (GroupeActif != -1)
                    {   //Bouge une forme composée (tout le groupe actif)
                        this.ListGroupes.Find(item => item.Id == this.GroupeActif).translation(point_depart, point_arrivee);

                        //Bouge tous les groupes liés à cette forme composée (le groupe actif)
                        foreach (Forme_composee f in ListGroupes)
                        {
                            if (f.IdGroupe == this.GroupeActif)//this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Id)
                            {//Si le groupe parcouru est lié au groupe actif
                                f.translation(point_depart, point_arrivee);
                            }
                        }
                    }
                    else
                    {   //Bouge une forme simple (la forme active)
                        forme_active.translation(point_depart, point_arrivee);
                        majPropriete(forme_active);
                    }
                }

                this.refreshPanel();
                
                selected = false;
            }
            majListeAjoutGroupes();
        }

        private void panel_couleur_Click(object sender, EventArgs e)    //attaché au panel de selection de couleur
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
                D1.dessiner(forme_active, g2);
                panel1.Invalidate();

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)   //attaché au formulaire
        {
            ///Ctrl+G (Controler le groupe de la forme active)
            if (e.KeyCode == Keys.G && e.Modifiers == Keys.Control)
            {
                if ((this.GroupeActif == -1) && (this.forme_active != null))
                {   //si inactif, on active
                    labelGroupeActif.Text = "Groupe Actif";
                    this.GroupeActif = this.forme_active.IdGroupe;
                    lierGroupeToolStripMenuItem.Text = "Lier le groupe actif à un groupe";
                    textBox_nom.Text = this.ListGroupes.Find(item => item.Id == this.GroupeActif).Nom;
                }
                else
                {   //sinon on desactive
                    labelGroupeActif.Text = "Groupe Inactif";
                    this.GroupeActif = -1;
                    lierGroupeToolStripMenuItem.Text = "Lier la forme active à un groupe";
                }
                majListeAjoutGroupes();
            }  

            ///Ctrl+HAUT (Controler le groupe du groupe de la forme active (son SUPERGROUPE))
            if (e.KeyCode == Keys.Up && e.Modifiers == Keys.Control)
            {
                if((this.GroupeActif != -1))
                {//groupe actif, on controle le SUPERGROUPE
                    labelGroupeActif.Text = "Groupe Actif";
                    this.GroupeActif = this.ListGroupes.Find(item => item.Id == this.GroupeActif).IdGroupe;
                    Console.WriteLine("Controle du SUPER GROUPE: (" + this.GroupeActif);
                    majPropriete(this.ListGroupes.Find(item => item.Id == this.GroupeActif));
                    majListeAjoutGroupes();
                }
            }

            //Zoom
            if (e.KeyData == Keys.Up)
            {
                if (this.forme_active != null)
                {
                    if (GroupeActif != -1)
                    {   //Homothetie sur une forme composée (tout le groupe actif)
                        this.ListGroupes.Find(item => item.Id == this.GroupeActif).homothetie(1);

                        //Homothetie sur tous les groupes liés à cette forme composée (le groupe actif)
                        foreach (Forme_composee f in ListGroupes)
                        {
                            if (f.IdGroupe == this.GroupeActif)
                            {//Si le groupe parcouru est lié au groupe actif
                                f.homothetie(1);
                            }
                        }
                    }
                    else
                    {
                        this.forme_active.homothetie(1);
                    }
                    this.refreshPanel();
                }
            }
            //Dezoom
            if (e.KeyData == Keys.Down)
            {
                if (this.forme_active != null)
                {
                    if (GroupeActif != -1)
                    {
                        //Homothetie sur une forme composée (tout le groupe actif)
                        this.ListGroupes.Find(item => item.Id == this.GroupeActif).homothetie(-1);

                        //Homothetie sur tous les groupes liés à cette forme composée (le groupe actif)
                        foreach (Forme_composee f in ListGroupes)
                        {
                            if (f.IdGroupe == this.GroupeActif)
                            {//Si le groupe parcouru est lié au groupe actif
                                f.homothetie(-1);
                            }
                        }
                    }
                    else
                    {
                        //g2.Clear(Color.White);
                        this.forme_active.homothetie(-1);
                    }

                    this.refreshPanel();
                }
            }
            //Suppression d'une forme ou d'un groupe
            if (e.KeyData == Keys.Delete && this.forme_active != null && GroupeActif == -1)
            {
                Console.WriteLine("Suppression d'une forme simple");
                this.ListFormes.Remove(this.forme_active);
                if (this.forme_active.IdGroupe != -1)
                {//Liée à un groupe
                    int MASTERgroupe = this.forme_active.IdGroupe;
                    while (MASTERgroupe != -1)
                    {//Si il y a un MASTERgroupe
                        this.ListGroupes.Find(item => item.Id == MASTERgroupe).Liste_formes.Remove(this.forme_active);   //suppresion de la liste du MASTERgroupe
                        MASTERgroupe = this.ListGroupes.Find(item => item.Id == MASTERgroupe).IdGroupe;//si le groupe visité est lié
                    }
                }
                //VISUEL
                textBox_nom.Clear();//Nom
                this.labelNomGroupeActif.Text = "Aucun";
                this.forme_active = null;
                this.refreshPanel();
            }

        }


        private void majPropriete(Forme_simple forme)
        {//Affiche les propriétes de la forme dans la partie DROITE de l'application
            if (forme != null)
            {
                textBox_nom.Text = forme.Nom;//Nom
                panel_couleur.BackColor = Color.FromArgb(forme.Couleur);//Couleur
                if (forme.IdGroupe == -1)//Groupe
                    labelNomGroupeActif.Text = "Aucun";
                else
                    labelNomGroupeActif.Text = this.ListGroupes.Find(item => item.Id == forme.IdGroupe).Nom;
            }
        }
        private void majPropriete(Forme_composee forme)
        {//Affiche les propriétes de la forme dans la partie DROITE de l'application
            textBox_nom.Text = "GROUPE "+forme.Nom;
            labelNomGroupeActif.Text = forme.Nom;
        }



        private void textBox3_KeyDown(object sender, KeyEventArgs e)    //attaché à l'ajout de point de polygone
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.nb_points_poly = Convert.ToInt32(textBoxNbPoints.Text);
                textBoxNbPoints.Text = "";
                this.tabcoord = new Point[this.nb_points_poly];
                label10.Visible = false;
                textBoxNbPoints.Visible = false;
                activer_dessin();
            }
        }

        private void ajouterUnGroupeToolStripMenuItem_Click(object sender, EventArgs e) //attaché au menu ajouter un groupe
        {
            labelCreationGroupe.Visible = true;
            textBoxCreationGroupe.Visible = true;
            textBoxCreationGroupe.Focus();
        }

        private void textBoxCreationGroupe_KeyDown(object sender, KeyEventArgs e)   //attaché à la saisie d'un nouveau groupe
        {//Creation de Groupe
            if (e.KeyCode == Keys.Enter)
            {
                this.ListGroupes.Add(new Forme_composee(this.id, textBoxCreationGroupe.Text));
                labelCreationGroupe.Visible = false;
                textBoxCreationGroupe.Visible = false;
                //Ajouts
                this.toolStripComboBoxGroupes.Items.Add(textBoxCreationGroupe.Text);
                this.SupprimerGroupetoolStripComboBox.Items.Add(textBoxCreationGroupe.Text);
                Console.WriteLine("CREATION GROUPE: "+this.id+" : "+this.textBoxCreationGroupe.Text);
                //Prepare pour les suivants
                textBoxCreationGroupe.Clear();
                this.id++;
            }
        }

        private void toolStripComboBoxGroupes_SelectedIndexChanged(object sender, EventArgs e)  //attaché au choix d'un groupe
        {//Selection d'un Groupe Actif différent que celui actif
            if (toolStripComboBoxGroupes.SelectedIndex != 0)
            {   //Selection autre que Aucun : Affectation
                if (this.forme_active != null)
                {
                    if (this.GroupeActif != -1)
                    {//Affectation d'un GROUPE à un SOUS-GROUPE
                        //Liaison du SOUS-GROUPE(groupe de la forme active) au GROUPE
                        this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).IdGroupe = this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Id; //ID dans ListeGroupes du groupe séléctionné dans la liste déroulante
                        
                        //Ajoute toutes les formes du sous groupe à la liste des formes du SUPERgroupe
                        foreach (Forme_simple f in this.ListGroupes.Find(item => item.Id == this.GroupeActif).Liste_formes)
                        {//on ajoute chaque forme du groupe actif dans le SUPERGROUPE
                            this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Liste_formes.Add(f);    
                        }

                        Console.WriteLine("Affectation SOUS-GROUPE(" + this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Id + ", " + this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Nom + ") au GROUPEMAITRE(" + this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Id + ", " + this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Nom + ")");

                    }
                    else
                    {//Affectation d'un GROUPE à une FORME
                        //Liaison de la FORMEACTIVE au GROUPE
                        this.forme_active.IdGroupe = this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Id; //ID dans ListeGroupes du groupe séléctionné dans la liste déroulante
                        if (this.ListGroupes[toolStripComboBoxGroupes.SelectedIndex - 1].Liste_formes == null)
                        {
                            this.ListGroupes[toolStripComboBoxGroupes.SelectedIndex - 1].Liste_formes = new List<Forme_simple>();
                        }
                        this.ListGroupes[toolStripComboBoxGroupes.SelectedIndex - 1].Liste_formes.Add(this.forme_active);
                        Console.WriteLine("Affectation FORMEACTIVE(" + this.forme_active.Id + ") au GROUPE(" + this.ListGroupes[toolStripComboBoxGroupes.SelectedIndex - 1].Id + ", " + this.ListGroupes[toolStripComboBoxGroupes.SelectedIndex - 1].Nom + ")");
                    }
                    majPropriete(this.forme_active);
                }
            }
            else
            {//Desaffectation
                if (this.forme_active != null)
                {
                    if (this.GroupeActif != -1)
                    {
                        //Liaison du Groupe de la FORMEACTIVE au GROUPE -1
                        this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).IdGroupe = -1; //ID dans ListeGroupes du groupe séléctionné dans la liste déroulante
                        Console.WriteLine("Desaffectaction: SOUS-GROUPE(" + this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Id + ", "+ this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Nom +") au GROUPE -1");

                        //Supprime toutes les formes du sous groupe de la liste des formes de l'ancien SUPERgroupe
                        foreach (Forme_simple f in this.ListGroupes.Find(item => item.Id == this.GroupeActif).Liste_formes)
                        {//on supprime chaque forme du groupe actif de la liste de l'ancien SUPERGROUPE
                            this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Liste_formes.Remove(f);
                        }
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

        private void SupprimerGroupetoolStripComboBox_SelectedIndexChanged(object sender, EventArgs e)  //attaché à la suppression d'un groupe
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
                this.GroupeActif = -1;
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
                if (this.forme_active != null && this.forme_active.IdGroupe == g.Id && this.GroupeActif != -1)
                {//Un groupe ne peut pas se lier à soi-même
                    //On ne fait donc rien
                   // this.toolStripComboBoxGroupes.Items.Remove(this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Nom);
                }
                else
                {
                    this.toolStripComboBoxGroupes.Items.Add(g.Nom);
                }
            }
            if (this.forme_active != null && this.forme_active.IdGroupe != -1)
                this.toolStripComboBoxGroupes.SelectedText =  this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Nom;
            if (this.forme_active != null && this.forme_active.IdGroupe == -1)
                this.toolStripComboBoxGroupes.SelectedText = "Aucun";
        }

        public void majListeSupprGroupes()
        {
            SupprimerGroupetoolStripComboBox.Items.Clear();
            foreach(Forme_composee g in ListGroupes)
            {
                SupprimerGroupetoolStripComboBox.Items.Add(g.Nom);
            }
            
        }

        public void refreshPanel()
        {
            g2.Clear(Color.White);
            panel1.Invalidate();

            foreach (Forme_simple forme in ListFormes)
            {
                D1.dessiner(forme, g2);
            }
            panel1.Invalidate();
        }

        private void textBox_nom_KeyUp(object sender, KeyEventArgs e)   //attaché au nom de la forme
        {//Renommer une forme
            //FORME SIMPLE
            if (this.forme_active != null && this.GroupeActif == -1)
            {
                Console.WriteLine("Changement du nom d'une forme simple");
                this.forme_active.Nom = textBox_nom.Text;
                //modif dans listFormes
                this.ListFormes.Find(item => item.Id == this.forme_active.Id).Nom = textBox_nom.Text;
                //modif dans listGroupes
                foreach(Forme_composee g in this.ListGroupes)
                {
                    if (g.Liste_formes != null && g.Liste_formes.Find(item => item.Id == this.forme_active.Id) != null)
                            g.Liste_formes.Find(item => item.Id == this.forme_active.Id).Nom = textBox_nom.Text;
                }
            }

            //FORME COMPOSEE
            if (this.GroupeActif != -1)
            {
                Console.WriteLine("Changement du nom d'une forme composée");
                //list forme composee
                this.ListGroupes.Find(item => item.Id == this.GroupeActif).Nom = textBox_nom.Text;
                labelNomGroupeActif.Text = textBox_nom.Text;
                this.majListeAjoutGroupes();
                this.majListeSupprGroupes();
            }
        }

        private void sauvegarderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Forme_simple list in ListFormes)
            {
                if (!Fs1.presente(list))
                {
                    Fs1.create(list);
                    Console.WriteLine("test");
                }
                else
                    Fs1.update(list);
            }
        }

        private void importerToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

    }
}
