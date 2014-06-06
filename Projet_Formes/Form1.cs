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
        bool selected;
        bool PeutDessiner = false;

        Forme_simple forme_active;
        int id = 1;
       

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
            this.forme_active = new Ellipse(id, "Ellipse " + id, panel_couleur.BackColor.ToArgb(), new Point(0, 0), 0, 0, -1);
            this.id++;
            activer_dessin();
        }

        private void triangleToolStripMenuItem_Click(object sender, EventArgs e)    //attaché à l'item Dessin Triangle
        {
            this.nb_points_poly = 3;
            this.tabcoord = new Point[this.nb_points_poly];
            this.forme_active = new Triangle(id, "Triangle " + id, panel_couleur.BackColor.ToArgb(), tabcoord, -1);
            this.id++;
            activer_dessin();
        }

        private void rectangleToolStripMenuItem_Click(object sender, EventArgs e)   //attaché à l'item Dessin Rectangle
        {
            this.forme_active = new Rectangle(id, "Rectangle " + id, panel_couleur.BackColor.ToArgb(), new Point(0, 0), 0, 0, -1);
            this.id++;
            activer_dessin();
        }

        private void segmentToolStripMenuItem_Click(object sender, EventArgs e) //attaché à l'item Dessin Segment
        {
            this.forme_active = new Segment(id, "Segment " + id, panel_couleur.BackColor.ToArgb(), new Point(0, 0), new Point(0, 0), -1);
            this.id++;
            activer_dessin();
        }

        private void polygoneToolStripMenuItem_Click(object sender, EventArgs e)    //attaché à l'item Dessin Polygone
        {
            this.forme_active = new Polygone(id, "Polygone " + id, panel_couleur.BackColor.ToArgb(), tabcoord, -1);
            this.id++;
            label10.Visible = true;
            textBoxNbPoints.Visible = true;
            textBoxNbPoints.Focus();
        }

        private void activer_dessin()
        {
            panel1.Cursor = System.Windows.Forms.Cursors.Cross;   //PASSE EN MODE DESSIN
            this.PeutDessiner = true;
            if (this.forme_active.IdGroupe != -1)
            {
                //labelGroupeActif.Text = "Groupe Inactif";
                this.forme_active.IdGroupe = -1;
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
            {
                //######
                //DESSIN
                //######
                bouton_Mouse_left = true;
                listBoxGroupesLiés.Items.Clear();
                listBoxGroupesLiés.Items.Add("Aucun");
                listBoxGroupesLiés.SetSelected(listBoxGroupesLiés.Items.IndexOf("Aucun"), true);

                if (forme_active != null)
                {
                    if ( !(this.forme_active is Triangle) && !(this.forme_active is Polygone) )
                    {
                        //Dessin de Segments, Ellipse, Rectangle
                        this.forme_active.maj(point_depart, point_depart);
                        ListFormes.Add(this.forme_active);
                        majPropriete(this.forme_active);
                    }
                    else
                    {   //Dessin de Triangle/Polygone
                        if (i == this.nb_points_poly - 1)
                        {
                            tabcoord[i] = point_depart;
                            forme_active.maj(tabcoord);
                            ListFormes.Add(this.forme_active);
                            this.i = 0;
                            this.PeutDessiner = false;
                            panel1.Cursor = System.Windows.Forms.Cursors.Default;

                            this.refreshPanel();
                            majListeAjoutGroupes();
                        }
                        else
                        {
                            tabcoord[i] = point_depart;
                            this.i++;
                        }
                    }
                }
            }
            else if (e.Button == MouseButtons.Left && !this.PeutDessiner)
            {
                //#########
                //SELECTION
                //#########
                bouton_Mouse_left = true;
                //Selectionne une forme
                foreach (Forme_simple forme in ListFormes)
                {
                    if (forme.recuperer(point_depart.X, point_depart.Y))
                    {
                        this.selected = true;
                        this.forme_active = forme;
                        break;
                    }
                }
                if(this.forme_active != null)
                    majPropriete(forme_active);

            }

        }

        private void chercheSousGroupes(Forme_composee GroupeBase)
        {
          
            //Les groupes liés directement
            List<Forme_composee> ListeSousGroupes = this.ListGroupes.FindAll(g => g.IdGroupe == GroupeBase.Id);
            foreach (Forme_composee groupe in ListeSousGroupes)
            {
                chercheSousGroupes(groupe);
            }
            listBoxGroupesLiés.Items.Add(GroupeBase.Nom);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)    //attaché à panel de dessin
        {
            this.point_arrivee = e.Location;
            //######
            //DESSIN
            //######
            if (bouton_Mouse_left && PeutDessiner)
            {
                bouton_Mouse_left = false;
                if ( !(this.forme_active is Triangle) && !(this.forme_active is Polygone) )
                {
                    if (forme_active != null)
                    {   //Dessin de Segments, Ellipse, Rectangle
                        forme_active.maj(point_depart, point_arrivee);
                        panel1.Invalidate();
                    }
                    this.PeutDessiner = false;
                    panel1.Cursor = System.Windows.Forms.Cursors.Default;

                    this.refreshPanel();
                    majListeAjoutGroupes();
                }
            }
            else if (bouton_Mouse_left && this.selected)
            {   //#########
                //SELECTION
                //#########
                bouton_Mouse_left = false;
                if (forme_active != null)
                {
                    if (forme_active.IdGroupe != -1)
                    {   //Bouge une forme composée (tout le groupe actif)
                        this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).translation(new Point(this.point_arrivee.X - this.point_depart.X, this.point_arrivee.Y - this.point_depart.Y) );
                    }
                    else
                    {   //Bouge une forme simple (la forme active)
                        forme_active.translation(new Point(this.point_arrivee.X - this.point_depart.X, this.point_arrivee.Y - this.point_depart.Y));
                    }
                }

                selected = false;
                this.refreshPanel();
                majListeAjoutGroupes();
            }
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
                D1.contourSelection(forme_active, g2, Color.Red);
                D1.dessiner(forme_active, g2);
                panel1.Invalidate();

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)   //attaché au formulaire
        {
            /////Ctrl+HAUT (Controler le groupe du groupe de la forme active (son SUPERGROUPE))
            //if (e.KeyCode == Keys.Up && e.Modifiers == Keys.Control)
            //{
            //    if ((this.forme_active.IdGroupe != -1))
            //    {//groupe actif, on controle le SUPERGROUPE
            //        //labelGroupeActif.Text = "Groupe Actif";
            //        this.forme_active.IdGroupe = this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).IdGroupe;
            //        Console.WriteLine("Controle du SUPER GROUPE: (" + this.forme_active.IdGroupe);
            //        majPropriete(this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe));
            //        majListeAjoutGroupes();
            //    }
            //}

            //Zoom
            if (e.KeyData == Keys.Up)
            {
                if (this.forme_active != null)
                {
                    if (forme_active.IdGroupe != -1)
                    {   //Homothetie sur une forme composée (tout le groupe actif)
                        this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).homothetie(1);

                        //Homothetie sur tous les groupes liés à cette forme composée (le groupe actif)
                        foreach (Forme_composee f in ListGroupes)
                        {
                            if (f.IdGroupe == this.forme_active.IdGroupe)
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
                    if (forme_active.IdGroupe != -1)
                    {
                        //Homothetie sur une forme composée (tout le groupe actif)
                        this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).homothetie(-1);

                        //Homothetie sur tous les groupes liés à cette forme composée (le groupe actif)
                        foreach (Forme_composee f in ListGroupes)
                        {
                            if (f.IdGroupe == this.forme_active.IdGroupe)
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
            if (e.KeyData == Keys.Delete && this.forme_active != null && this.forme_active.IdGroupe == -1)
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
                listBoxGroupesLiés.Items.Clear();
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
                //Groupe
                listBoxGroupesLiés.Items.Clear();
                listBoxGroupesLiés.Items.Add("Aucun");
                listBoxGroupesLiés.SetSelected(listBoxGroupesLiés.Items.IndexOf("Aucun"), true);
                //Groupe lié à la forme_active
                Forme_composee GroupeLiéDirectement = this.ListGroupes.Find(groupe => groupe.Id == this.forme_active.IdGroupe);
                if (GroupeLiéDirectement != null)
                {   //Recherche de tous les sous groupes liés directement ou indirectement au groupe forme_active.IdGroupe et les affiche dans listBoxGroupesLiés
                    this.chercheSousGroupes(GroupeLiéDirectement);
                    
                    //Selectionne le groupe lié directement
                    listBoxGroupesLiés.SetSelected(listBoxGroupesLiés.Items.IndexOf(GroupeLiéDirectement.Nom), true);
                }
                
                
                
                
                
                //if (forme.IdGroupe == -1)//Groupe
                //{
                //    listBoxGroupesLiés.Items.Clear();
                //    listBoxGroupesLiés.Items.Add("Aucun");
                //    listBoxGroupesLiés.SetSelected(listBoxGroupesLiés.Items.IndexOf("Aucun"), true);
                //}
                //else
                //{
                //    listBoxGroupesLiés.Items.Add(this.ListGroupes.Find(item => item.Id == forme.IdGroupe).Nom);
                //    listBoxGroupesLiés.SetSelected(listBoxGroupesLiés.Items.IndexOf(this.ListGroupes.Find(item => item.Id == forme.IdGroupe).Nom), true);
                //}
            }
        }


        private void majPropriete(Forme_composee forme)
        {//Affiche les propriétes du groupe dans la partie DROITE de l'application
            textBox_nom.Text = "GROUPE " + forme.Nom;

            //labelNomGroupeActif.Text = forme.Nom;
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
                this.ListGroupes.Add(new Forme_composee(this.id, textBoxCreationGroupe.Text, -1, new List<Forme>()));
                labelCreationGroupe.Visible = false;
                textBoxCreationGroupe.Visible = false;
                //Ajouts
                this.toolStripComboBoxGroupes.Items.Add(textBoxCreationGroupe.Text);
                this.SupprimerGroupetoolStripComboBox.Items.Add(textBoxCreationGroupe.Text);
                Console.WriteLine("CREATION GROUPE: " + this.id + " : " + this.textBoxCreationGroupe.Text);
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
                    if (this.forme_active.IdGroupe != -1)
                    {//Affectation d'un sous GROUPE à GROUPE
                        int IdSuperGroupe = this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Id;
                        //Liaison du SOUS-GROUPE(groupe de la forme active) au GROUPE
                        this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).IdGroupe = IdSuperGroupe; //ID dans ListeGroupes du groupe séléctionné dans la liste déroulante

                        int IdSousGroupe = this.forme_active.IdGroupe;
                        //Ajoute toutes les formes du sous groupe à la liste des formes du SUPERgroupe
                        foreach (Forme_simple f in this.ListGroupes.Find(item => item.Id == IdSousGroupe).Liste_formes)
                        {
                            //on ajoute la forme dans le SUPERGROUPE (simple ajout de pointeur, pas de copie)
                            this.ListGroupes.Find(item => item.Id == IdSuperGroupe).Liste_formes.Add(f);
                        }
                        Console.WriteLine("Affectation SOUS-GROUPE(" + this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Id + ", " + this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Nom + ") au GROUPEMAITRE(" + this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Id + ", " + this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Nom + ")");

                        //########################
                        //Controle du SUPER Groupe
                        //########################
                        Console.WriteLine("Controle du SUPER GROUPE: (" + this.forme_active.IdGroupe + ")");
                        majPropriete(this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe));
                    }
                    else
                    {//Affectation d'une FORME à un GROUPE
                        //Liaison de la FORMEACTIVE au GROUPE
                        this.forme_active.IdGroupe = this.ListGroupes.Find(item => item.Nom == toolStripComboBoxGroupes.SelectedItem.ToString()).Id; //ID dans ListeGroupes du groupe séléctionné dans la liste déroulante
                        if (this.ListGroupes[toolStripComboBoxGroupes.SelectedIndex - 1].Liste_formes == null)
                        {
                            this.ListGroupes[toolStripComboBoxGroupes.SelectedIndex - 1].Liste_formes = new List<Forme>();
                        }
                        this.ListGroupes[toolStripComboBoxGroupes.SelectedIndex - 1].Liste_formes.Add(this.forme_active);
                        Console.WriteLine("Affectation FORMEACTIVE(" + this.forme_active.Id + ") au GROUPE(" + this.ListGroupes[toolStripComboBoxGroupes.SelectedIndex - 1].Id + ", " + this.ListGroupes[toolStripComboBoxGroupes.SelectedIndex - 1].Nom + ")");

                        //##################
                        //Controle du groupe
                        //##################
                        this.forme_active.IdGroupe = this.forme_active.IdGroupe;
                    }

                    majListeAjoutGroupes();
                    majPropriete(this.forme_active);
                    refreshPanel();
                }
            }
            else
            {//Desaffectation
                if (this.forme_active != null)
                {
                    if (this.forme_active.IdGroupe != -1)
                    {
                        //Liaison du Groupe de la FORMEACTIVE au GROUPE -1
                        this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).IdGroupe = -1; //ID dans ListeGroupes du groupe séléctionné dans la liste déroulante
                        Console.WriteLine("Desaffectaction: SOUS-GROUPE(" + this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Id + ", " + this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Nom + ") au GROUPE -1");

                        //Supprime toutes les formes du sous groupe de la liste des formes de l'ancien SUPERgroupe
                        this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Liste_formes.RemoveAll(forme => forme.IdGroupe == this.forme_active.IdGroupe);
                        
                        //Modifie l'id du groupe dans la ListeFormes
                        foreach(Forme_simple f in this.ListFormes)
                        {//on supprime chaque forme du groupe actif de la liste de l'ancien SUPERGROUPE
                            if(f.IdGroupe == this.forme_active.IdGroupe)
                                f.IdGroupe = -1;
                        }
                      
                    }
                    else
                    {
                        //Liaison de la FORMEACTIVE au GROUPE -1
                        this.forme_active.IdGroupe = -1;
                        Console.WriteLine("Desaffectaction: FORMEACTIVE(" + this.forme_active.Id + ") au GROUPE -1");
                        listBoxGroupesLiés.SetSelected(listBoxGroupesLiés.Items.IndexOf("Aucun"), true);

                    }
                    majListeAjoutGroupes();
                    majPropriete(this.forme_active);
                    refreshPanel();
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
            if (this.forme_active != null)
            {
                if (this.forme_active.IdGroupe == IDdugroupe)
                    this.forme_active.IdGroupe = -1;
            }


            //VISUEL
            //Supprime de la liste déroulante de Liaison de Groupe
            this.toolStripComboBoxGroupes.Items.Remove(NOMdugroupe);
            //Label du groupe actif de la forme active (au cas ou)
            //if (labelNomGroupeActif.Text == NOMdugroupe)
            //{
            //    this.labelNomGroupeActif.Text = "Aucun";
            //    this.forme_active.IdGroupe = -1;
            //}
            //Supprime de la liste déroulante des groupes à supprimer
            this.SupprimerGroupetoolStripComboBox.Items.Remove(NOMdugroupe);
        }

        public void majListeAjoutGroupes()
        {
            this.toolStripComboBoxGroupes.Items.Clear();
            this.toolStripComboBoxGroupes.Items.Add("Aucun");
            foreach (Forme_composee g in ListGroupes)
            {
                if (this.forme_active != null && this.forme_active.IdGroupe == g.Id && this.forme_active.IdGroupe != -1)
                {//Un groupe ne peut pas se lier à soi-même
                    //On ne fait donc rien
                    // this.toolStripComboBoxGroupes.Items.Remove(this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Nom);
                }
                else
                {
                    this.toolStripComboBoxGroupes.Items.Add(g.Nom);
                }
            }


            //VISUEL: Liaison à un groupe
            if (this.forme_active == null)
                lierGroupeToolStripMenuItem.Enabled = false;
            else
                lierGroupeToolStripMenuItem.Enabled = true;

            if ((this.forme_active != null) && (this.forme_active.IdGroupe != -1) && this.ListGroupes.Any(item => item.Id == this.forme_active.IdGroupe))
            {   //si on choisit un groupe existant, on le controle
                String nomGroupe = this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Nom;
                lierGroupeToolStripMenuItem.Text = "Lier le groupe  '" + nomGroupe + "'  au groupe";
                textBox_nom.Text = nomGroupe;
                this.toolStripComboBoxGroupes.SelectedText = nomGroupe;
            }
            else
            {   //sinon aucun controle
                lierGroupeToolStripMenuItem.Text = "Lier la forme active au groupe";
                this.toolStripComboBoxGroupes.SelectedText = "Aucun";
            }
        }

        public void majListeSupprGroupes()
        {
            SupprimerGroupetoolStripComboBox.Items.Clear();
            foreach (Forme_composee g in ListGroupes)
            {
                SupprimerGroupetoolStripComboBox.Items.Add(g.Nom);
            }

        }

        public void refreshPanel()
        {
            g2.Clear(Color.White);
            panel1.Invalidate();



            //#################
            //DESSIN DU CONTOUR
            //#################
            
            //SOUS GROUPES
            if (this.forme_active != null)
            {
                //Contour des sous groupes du groupe séléctionné
                List<Forme_composee> ListSousGroupesLiés = this.ListGroupes.FindAll(groupe => groupe.IdGroupe != -1 && groupe.IdGroupe == this.forme_active.IdGroupe);
                foreach (Forme_composee groupe_lié in ListSousGroupesLiés)
                {
                    foreach (Forme f in groupe_lié.Liste_formes)
                    {
                        Forme_simple copie = (Forme_simple)f;
                        D1.contourSelection(copie, g2, Color.FromArgb(255, 0, 255, 0));
                    }
                }

                //Contour des SUPER groupe du groupe séléctionné
                List<Forme_composee> ListSuperGroupesLiés = this.ListGroupes.FindAll(groupe => groupe.IdGroupe != -1 && groupe.Id == this.forme_active.IdGroupe);
                foreach (Forme_composee groupe_lié in ListSuperGroupesLiés)
                {
                    foreach (Forme f in groupe_lié.Liste_formes)
                    {
                        Forme_simple copie = (Forme_simple)f;
                        D1.contourSelection(copie, g2, Color.FromArgb(255, 0, 255, 0));
                    }
                }
            }
                    
            //GROUPE et FORME ACTIVE
            foreach (Forme_simple forme in ListFormes)
            {
                if (this.forme_active != null)
                {
                    if (forme_active.IdGroupe == forme.IdGroupe && forme.IdGroupe != -1)
                    {
                        //cas des formes du meme groupe (autre que -1)
                        D1.contourSelection(forme, g2, Color.FromArgb(255, 0, 255, 0));
                    }

                    if (forme_active == forme)
                        //cas forme active (nécéssaire d'etre déssiné après le groupe)
                        D1.contourSelection(forme, g2, Color.Red);
                }
                //dessine toutes les formes
                D1.dessiner(forme, g2);
            }
            panel1.Invalidate();
        }

        private void textBox_nom_KeyUp(object sender, KeyEventArgs e)   //attaché au nom de la forme
        {//Renommer une forme
            //FORME SIMPLE
            if (this.forme_active != null && this.forme_active.IdGroupe == -1)
            {
                Console.WriteLine("Changement du nom d'une forme simple");
                this.forme_active.Nom = textBox_nom.Text;
                //modif dans listFormes
                this.ListFormes.Find(item => item.Id == this.forme_active.Id).Nom = textBox_nom.Text;
                //modif dans listGroupes
                foreach (Forme_composee g in this.ListGroupes)
                {
                    if (g.Liste_formes != null && g.Liste_formes.Find(item => item.Id == this.forme_active.Id) != null)
                        g.Liste_formes.Find(item => item.Id == this.forme_active.Id).Nom = textBox_nom.Text;
                }
            }

            //FORME COMPOSEE
            if (this.forme_active != null && this.forme_active.IdGroupe != -1)
            {
                Console.WriteLine("Changement du nom d'une forme composée");
                //list forme composee
                this.ListGroupes.Find(item => item.Id == this.forme_active.IdGroupe).Nom = textBox_nom.Text;
                //labelNomGroupeActif.Text = textBox_nom.Text;
                this.majListeAjoutGroupes();
                this.majListeSupprGroupes();
            }
        }

        private void sauvegarderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //On enleve tous les enregistrements existants  DELETE FROM forme
                Fs1.deleteAll();
                

                foreach (Forme_simple forme in ListFormes)
                {
                    forme.Write();
                    Fs1.createorupdate(forme);
                }

                foreach (Forme_composee formecomp in ListGroupes)
                {
                    Fc.createorupdate(formecomp);
                }
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("\nPour sauvegarder: La connexion doit etre valide et ouverte\n");
            }

        }

        private void importerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.ListFormes.Clear();
                this.ListGroupes.Clear();

                this.clearTouteLappli();

                //PARTIE DONNEES
                //Formes Composees
                if (Fc.find() != null)   //gère cas où la BDD n'est pas disponible
                    this.ListGroupes.AddRange(Fc.find());

                //Formes Simples
                if (Fs1.find() != null)   //gère cas où la BDD n'est pas disponible
                    this.ListFormes.AddRange(Fs1.find());   //Rectangle
                if (Fs2.find() != null)   //gère cas où la BDD n'est pas disponible
                    this.ListFormes.AddRange(Fs2.find());   //Segment
                if (Fs3.find() != null)   //gère cas où la BDD n'est pas disponible
                    this.ListFormes.AddRange(Fs3.find());   //Ellipse
                if (Fs4.find() != null)   //gère cas où la BDD n'est pas disponible
                    this.ListFormes.AddRange(Fs4.find());   //Triangle
                if (Fs5.find() != null)   //gère cas où la BDD n'est pas disponible
                    this.ListFormes.AddRange(Fs5.find());   //Polygone

                foreach (Forme_simple f in ListFormes)
                {
                    //si le groupe dont appartient à la fomre existe 
                    if (this.ListGroupes.Any(item => item.Id == f.IdGroupe))
                        //alors on l'ajoute dans ListForme du groupe
                        this.ListGroupes.Find(item => item.Id == f.IdGroupe).Liste_formes.Add(f);
                }


                //Groupe de Groupe
                foreach (Forme_composee g in ListGroupes)
                {
                    //idGroupe de notre groupe
                    int IDG = g.IdGroupe;

                    //tant qu'un groupe est lié à un autre groupe existant
                    while (this.ListGroupes.Any(item => item.Id == IDG))
                    {
                        Forme_composee supergroupe = this.ListGroupes.Find(item => item.Id == IDG);

                        foreach (Forme_simple f in g.Liste_formes)
                        {//On ajoute toutes les formes non encore existant 

                            //si la forme simple n'existe pas dans la liste_formes du super groupe, on l'ajoute
                            if (!supergroupe.Liste_formes.Any(item => item.Id == f.Id))
                            {
                                supergroupe.Liste_formes.Add(f);
                            }

                        }

                        //récupère l'idGroupe pour la prochaine boucle: le groupe père est il lié?
                        IDG = this.ListGroupes.Find(item => item.Id == IDG).IdGroupe;
                    }
                }




                //PARTIE VISUELLE
                majListeSupprGroupes();
                majListeAjoutGroupes();
                refreshPanel();

                this.id = this.ListGroupes.Count + this.ListFormes.Count + 1;
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("\nPour importer, la connexion doit être ouverte, ce n'est pas le cas.\n");
            }

        }

        private void clearTouteLappli()
        {
            this.ListFormes.Clear();
            this.ListGroupes.Clear();

            refreshPanel();
            
            if (this.forme_active != null)
            {
                this.forme_active.IdGroupe = -1;
                this.forme_active = null;
            }

            //Visuel partie droite
            textBox_nom.Clear();
            panel_couleur.BackColor = Color.Black;
            //labelNomGroupeActif.Text = "Aucun";
            //labelGroupeActif.Text = "Groupe Inactif";

            //Visuel Listes de groupe
            majListeAjoutGroupes();
            majListeSupprGroupes();
        }

        private void nouveauToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.clearTouteLappli();

        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            splitContainer1.Height = control.Size.Height - 60;
            splitContainer1.Width = control.Size.Width - 16;

            panel1.Height = control.Size.Height - 60;
            panel1.Width = control.Size.Width - 60;

            g = this.panel1.CreateGraphics();
            bm = new Bitmap(this.panel1.Width, this.panel1.Height);
            g2 = Graphics.FromImage(bm);

            //redessine toutes les formes
            this.refreshPanel();

        }

        private void quitterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void délierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Suppression de groupe
            int IDdugroupe = forme_active.IdGroupe;
            String NOMdugroupe = this.ListGroupes.Find(item => item.Id == IDdugroupe).Nom;
            Console.WriteLine("DELIER du groupe (" + IDdugroupe + ", " + NOMdugroupe + ")");
            //Suppression dans la liste des groupes
            this.ListGroupes.Remove(this.ListGroupes.Find(item => item.Id == IDdugroupe));
            
            //Modifie toutes les formes de tous les sous groupes liés au groupe supprimé
            foreach(Forme_composee g in this.ListGroupes.FindAll(g => g.IdGroupe == IDdugroupe))
            {
                foreach(Forme f in g.Liste_formes)
                {
                    f.IdGroupe = g.Id;
                }
            }

            //Modifie l'IdGroupe de tous les sous groupes liés au groupe supprimé
            this.ListGroupes.Where(item => item.IdGroupe == IDdugroupe).ToList().ForEach(g => g.IdGroupe = -1);
            
            //Modifie forme active
            if (this.forme_active != null)
            {
                if (this.forme_active.IdGroupe == IDdugroupe)
                    this.forme_active.IdGroupe = -1;
            }


            //VISUEL
            //Supprime de la liste déroulante de Liaison de Groupe
            this.toolStripComboBoxGroupes.Items.Remove(NOMdugroupe);
            //Label du groupe actif de la forme active (au cas ou)
            //if (labelNomGroupeActif.Text == NOMdugroupe)
            //{
            //    this.labelNomGroupeActif.Text = "Aucun";
            //    this.forme_active.IdGroupe = -1;
            //}
            //Supprime de la liste déroulante des groupes à supprimer
            this.SupprimerGroupetoolStripComboBox.Items.Remove(NOMdugroupe);
        }

    }
}
