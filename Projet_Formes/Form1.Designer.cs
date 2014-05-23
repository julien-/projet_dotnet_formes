namespace Projet_Formes
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fichierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nouveauToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sauvegarderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dessinerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ellipseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.triangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rectangleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.segmentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.polygoneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajouterUnGroupeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lierGroupeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripComboBoxGroupes = new System.Windows.Forms.ToolStripComboBox();
            this.supprimerUnGroupeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SupprimerGroupetoolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxNbPoints = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.labelCreationGroupe = new System.Windows.Forms.Label();
            this.textBoxCreationGroupe = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelNom = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_nom = new System.Windows.Forms.TextBox();
            this.panel_couleur = new System.Windows.Forms.Panel();
            this.labelGroupeActif = new System.Windows.Forms.Label();
            this.labelNomGroupeActif = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fichierToolStripMenuItem,
            this.dessinerToolStripMenuItem,
            this.groupeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1065, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fichierToolStripMenuItem
            // 
            this.fichierToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nouveauToolStripMenuItem,
            this.sauvegarderToolStripMenuItem,
            this.importerToolStripMenuItem});
            this.fichierToolStripMenuItem.Name = "fichierToolStripMenuItem";
            this.fichierToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.fichierToolStripMenuItem.Text = "Fichier";
            // 
            // nouveauToolStripMenuItem
            // 
            this.nouveauToolStripMenuItem.Name = "nouveauToolStripMenuItem";
            this.nouveauToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.nouveauToolStripMenuItem.Text = "Nouveau";
            this.nouveauToolStripMenuItem.Click += new System.EventHandler(this.nouveauToolStripMenuItem_Click);
            // 
            // sauvegarderToolStripMenuItem
            // 
            this.sauvegarderToolStripMenuItem.Name = "sauvegarderToolStripMenuItem";
            this.sauvegarderToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.sauvegarderToolStripMenuItem.Text = "Sauvegarder";
            this.sauvegarderToolStripMenuItem.Click += new System.EventHandler(this.sauvegarderToolStripMenuItem_Click);
            // 
            // importerToolStripMenuItem
            // 
            this.importerToolStripMenuItem.Name = "importerToolStripMenuItem";
            this.importerToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.importerToolStripMenuItem.Text = "Importer";
            this.importerToolStripMenuItem.Click += new System.EventHandler(this.importerToolStripMenuItem_Click);
            // 
            // dessinerToolStripMenuItem
            // 
            this.dessinerToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ellipseToolStripMenuItem,
            this.triangleToolStripMenuItem,
            this.rectangleToolStripMenuItem,
            this.segmentToolStripMenuItem,
            this.polygoneToolStripMenuItem});
            this.dessinerToolStripMenuItem.Image = global::Projet_Formes.Properties.Resources.crayon;
            this.dessinerToolStripMenuItem.Name = "dessinerToolStripMenuItem";
            this.dessinerToolStripMenuItem.Size = new System.Drawing.Size(79, 20);
            this.dessinerToolStripMenuItem.Text = "Dessiner";
            // 
            // ellipseToolStripMenuItem
            // 
            this.ellipseToolStripMenuItem.Image = global::Projet_Formes.Properties.Resources.cercle;
            this.ellipseToolStripMenuItem.Name = "ellipseToolStripMenuItem";
            this.ellipseToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.ellipseToolStripMenuItem.Text = "Ellipse";
            this.ellipseToolStripMenuItem.Click += new System.EventHandler(this.ellipseToolStripMenuItem_Click);
            // 
            // triangleToolStripMenuItem
            // 
            this.triangleToolStripMenuItem.Image = global::Projet_Formes.Properties.Resources.triangle;
            this.triangleToolStripMenuItem.Name = "triangleToolStripMenuItem";
            this.triangleToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.triangleToolStripMenuItem.Text = "Triangle";
            this.triangleToolStripMenuItem.Click += new System.EventHandler(this.triangleToolStripMenuItem_Click);
            // 
            // rectangleToolStripMenuItem
            // 
            this.rectangleToolStripMenuItem.Image = global::Projet_Formes.Properties.Resources.rectangle;
            this.rectangleToolStripMenuItem.Name = "rectangleToolStripMenuItem";
            this.rectangleToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.rectangleToolStripMenuItem.Text = "Rectangle";
            this.rectangleToolStripMenuItem.Click += new System.EventHandler(this.rectangleToolStripMenuItem_Click);
            // 
            // segmentToolStripMenuItem
            // 
            this.segmentToolStripMenuItem.Image = global::Projet_Formes.Properties.Resources.segment;
            this.segmentToolStripMenuItem.Name = "segmentToolStripMenuItem";
            this.segmentToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.segmentToolStripMenuItem.Text = "Segment";
            this.segmentToolStripMenuItem.Click += new System.EventHandler(this.segmentToolStripMenuItem_Click);
            // 
            // polygoneToolStripMenuItem
            // 
            this.polygoneToolStripMenuItem.Image = global::Projet_Formes.Properties.Resources.polygone;
            this.polygoneToolStripMenuItem.Name = "polygoneToolStripMenuItem";
            this.polygoneToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.polygoneToolStripMenuItem.Text = "Polygone";
            this.polygoneToolStripMenuItem.Click += new System.EventHandler(this.polygoneToolStripMenuItem_Click);
            // 
            // groupeToolStripMenuItem
            // 
            this.groupeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterUnGroupeToolStripMenuItem,
            this.lierGroupeToolStripMenuItem,
            this.supprimerUnGroupeToolStripMenuItem});
            this.groupeToolStripMenuItem.Image = global::Projet_Formes.Properties.Resources.groupe;
            this.groupeToolStripMenuItem.Name = "groupeToolStripMenuItem";
            this.groupeToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.groupeToolStripMenuItem.Text = "Groupe";
            // 
            // ajouterUnGroupeToolStripMenuItem
            // 
            this.ajouterUnGroupeToolStripMenuItem.Name = "ajouterUnGroupeToolStripMenuItem";
            this.ajouterUnGroupeToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.ajouterUnGroupeToolStripMenuItem.Text = "Créer un groupe";
            this.ajouterUnGroupeToolStripMenuItem.Click += new System.EventHandler(this.ajouterUnGroupeToolStripMenuItem_Click);
            // 
            // lierGroupeToolStripMenuItem
            // 
            this.lierGroupeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripComboBoxGroupes});
            this.lierGroupeToolStripMenuItem.Name = "lierGroupeToolStripMenuItem";
            this.lierGroupeToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.lierGroupeToolStripMenuItem.Text = "Lier la forme active à un groupe";
            // 
            // toolStripComboBoxGroupes
            // 
            this.toolStripComboBoxGroupes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBoxGroupes.Items.AddRange(new object[] {
            "Aucun"});
            this.toolStripComboBoxGroupes.Name = "toolStripComboBoxGroupes";
            this.toolStripComboBoxGroupes.Size = new System.Drawing.Size(121, 23);
            this.toolStripComboBoxGroupes.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBoxGroupes_SelectedIndexChanged);
            // 
            // supprimerUnGroupeToolStripMenuItem
            // 
            this.supprimerUnGroupeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SupprimerGroupetoolStripComboBox});
            this.supprimerUnGroupeToolStripMenuItem.Name = "supprimerUnGroupeToolStripMenuItem";
            this.supprimerUnGroupeToolStripMenuItem.Size = new System.Drawing.Size(241, 22);
            this.supprimerUnGroupeToolStripMenuItem.Text = "Supprimer un groupe";
            // 
            // SupprimerGroupetoolStripComboBox
            // 
            this.SupprimerGroupetoolStripComboBox.Name = "SupprimerGroupetoolStripComboBox";
            this.SupprimerGroupetoolStripComboBox.Size = new System.Drawing.Size(121, 23);
            this.SupprimerGroupetoolStripComboBox.SelectedIndexChanged += new System.EventHandler(this.SupprimerGroupetoolStripComboBox_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(230, 4);
            this.label10.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(90, 15);
            this.label10.TabIndex = 3;
            this.label10.Text = "Nombre points:";
            this.label10.Visible = false;
            // 
            // textBoxNbPoints
            // 
            this.textBoxNbPoints.Location = new System.Drawing.Point(319, 4);
            this.textBoxNbPoints.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxNbPoints.Name = "textBoxNbPoints";
            this.textBoxNbPoints.Size = new System.Drawing.Size(18, 20);
            this.textBoxNbPoints.TabIndex = 4;
            this.textBoxNbPoints.Visible = false;
            this.textBoxNbPoints.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox3_KeyDown);
            // 
            // labelCreationGroupe
            // 
            this.labelCreationGroupe.AutoSize = true;
            this.labelCreationGroupe.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelCreationGroupe.Location = new System.Drawing.Point(351, 4);
            this.labelCreationGroupe.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCreationGroupe.Name = "labelCreationGroupe";
            this.labelCreationGroupe.Size = new System.Drawing.Size(95, 15);
            this.labelCreationGroupe.TabIndex = 6;
            this.labelCreationGroupe.Text = "Nom du groupe:";
            this.labelCreationGroupe.Visible = false;
            // 
            // textBoxCreationGroupe
            // 
            this.textBoxCreationGroupe.Location = new System.Drawing.Point(450, 4);
            this.textBoxCreationGroupe.Margin = new System.Windows.Forms.Padding(2);
            this.textBoxCreationGroupe.Name = "textBoxCreationGroupe";
            this.textBoxCreationGroupe.Size = new System.Drawing.Size(124, 20);
            this.textBoxCreationGroupe.TabIndex = 7;
            this.textBoxCreationGroupe.Visible = false;
            this.textBoxCreationGroupe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxCreationGroupe_KeyDown);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.Default;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(1065, 542);
            this.splitContainer1.SplitterDistance = 857;
            this.splitContainer1.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Cursor = System.Windows.Forms.Cursors.Default;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(853, 538);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseUp);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tableLayoutPanel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 538);
            this.panel2.TabIndex = 9;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 34.92696F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 65.07304F));
            this.tableLayoutPanel1.Controls.Add(this.labelNom, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.textBox_nom, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel_couleur, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelGroupeActif, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelNomGroupeActif, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(4, 2);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(198, 538);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // labelNom
            // 
            this.labelNom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNom.AutoSize = true;
            this.labelNom.Location = new System.Drawing.Point(2, 0);
            this.labelNom.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelNom.Name = "labelNom";
            this.labelNom.Size = new System.Drawing.Size(65, 179);
            this.labelNom.TabIndex = 0;
            this.labelNom.Text = "Nom";
            this.labelNom.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(2, 179);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 179);
            this.label2.TabIndex = 1;
            this.label2.Text = "Couleur";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_nom
            // 
            this.textBox_nom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_nom.Location = new System.Drawing.Point(71, 79);
            this.textBox_nom.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_nom.Name = "textBox_nom";
            this.textBox_nom.Size = new System.Drawing.Size(125, 20);
            this.textBox_nom.TabIndex = 6;
            this.textBox_nom.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox_nom_KeyUp);
            // 
            // panel_couleur
            // 
            this.panel_couleur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_couleur.BackColor = System.Drawing.Color.Black;
            this.panel_couleur.Location = new System.Drawing.Point(72, 244);
            this.panel_couleur.Name = "panel_couleur";
            this.panel_couleur.Size = new System.Drawing.Size(123, 49);
            this.panel_couleur.TabIndex = 11;
            this.panel_couleur.Click += new System.EventHandler(this.panel_couleur_Click);
            // 
            // labelGroupeActif
            // 
            this.labelGroupeActif.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelGroupeActif.AutoSize = true;
            this.labelGroupeActif.Location = new System.Drawing.Point(71, 358);
            this.labelGroupeActif.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelGroupeActif.Name = "labelGroupeActif";
            this.labelGroupeActif.Size = new System.Drawing.Size(125, 180);
            this.labelGroupeActif.TabIndex = 5;
            this.labelGroupeActif.Text = "Groupe Inactif";
            this.labelGroupeActif.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelNomGroupeActif
            // 
            this.labelNomGroupeActif.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelNomGroupeActif.AutoSize = true;
            this.labelNomGroupeActif.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNomGroupeActif.Location = new System.Drawing.Point(2, 358);
            this.labelNomGroupeActif.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelNomGroupeActif.Name = "labelNomGroupeActif";
            this.labelNomGroupeActif.Size = new System.Drawing.Size(65, 180);
            this.labelNomGroupeActif.TabIndex = 8;
            this.labelNomGroupeActif.Text = "Aucun";
            this.labelNomGroupeActif.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1065, 571);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.textBoxCreationGroupe);
            this.Controls.Add(this.labelCreationGroupe);
            this.Controls.Add(this.textBoxNbPoints);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.menuStrip1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dessinerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ellipseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem triangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rectangleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem segmentToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem polygoneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem groupeToolStripMenuItem;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxNbPoints;
        private System.Windows.Forms.ToolStripMenuItem fichierToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nouveauToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sauvegarderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importerToolStripMenuItem;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.ToolStripMenuItem ajouterUnGroupeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem lierGroupeToolStripMenuItem;
        private System.Windows.Forms.Label labelCreationGroupe;
        private System.Windows.Forms.TextBox textBoxCreationGroupe;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBoxGroupes;
        private System.Windows.Forms.ToolStripMenuItem supprimerUnGroupeToolStripMenuItem;
        private System.Windows.Forms.ToolStripComboBox SupprimerGroupetoolStripComboBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelNom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_nom;
        private System.Windows.Forms.Panel panel_couleur;
        private System.Windows.Forms.Label labelGroupeActif;
        private System.Windows.Forms.Label labelNomGroupeActif;

    }
}

