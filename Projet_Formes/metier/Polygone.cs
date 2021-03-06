﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    public class Polygone : Forme_simple
    {
        protected Point []_tab_points;

        public Polygone(int id, string nom, int couleur, Point []tab_points, int idgroupe) : base(id, nom, couleur, idgroupe)
        {
            this._tab_points = tab_points;
        }

        public Point []Tableau_points
        {
            get
            {
                return this._tab_points;
            }
            set
            {
                this._tab_points = value;
            }
        }

        public override void Write()
        {
            base.Write();
            for(int i = 0; i < _tab_points.Length ; i++)
            {
                int Nombre = i + 1;
                Console.WriteLine("Point " + Nombre + " : (" + this._tab_points[i].X + "," + this._tab_points[i].Y + ")");
            }
        }

        public override void translation(Point vecteur)
        {
            for (int i = 0; i < this._tab_points.Length; i++)
            {
                this._tab_points[i].X += vecteur.X;
                this._tab_points[i].Y += vecteur.Y;
            }
        }

        public override void homothetie(int zoom)
        {
            //recherche du centre du triangle
            Point centre = new Point();
            centre = this.centre();

            for (int i = 0; i < this._tab_points.Length; i++)
            {
                //Gestion des X
                if (this._tab_points[i].X < centre.X)
                {//partie gauche
                    this._tab_points[i].X -= zoom;
                }
                else
                {//partie droite
                    this._tab_points[i].X += zoom;
                }

                //Gestion des Y
                if (this._tab_points[i].Y < centre.Y)
                {//partie haute
                    this._tab_points[i].Y -= zoom;
                }
                else
                {//partie basse
                    this._tab_points[i].Y += zoom;
                }
            }
        }

        private Point centre()
        {//recherche du centre du triangle
            Point centre = new Point(0, 0);

            for (int i = 0; i < this._tab_points.Length; i++)
            {
                centre.X += this._tab_points[i].X;
                centre.Y += this._tab_points[i].Y;
            }
            centre.X /= this._tab_points.Length;
            centre.Y /= this._tab_points.Length;

            return centre;
        }

        public override Boolean recuperer(int x, int y)
        {
            Boolean trouve = false;
            for (int i = 0; i < this._tab_points.Length; i++)
            {
                if ((x >= this._tab_points[i].X - 4 && x <= this._tab_points[i].X + 4) && (y >= this._tab_points[i].Y - 4 && y <= this._tab_points[i].Y + 4))
                {
                    trouve = true;
                    break;
                }
            }
            return trouve;
        }

        public override void maj(Point point1, Point point2)
        {
        }

        public override void maj(Point[] tabpoints)
        {
            this._tab_points = tabpoints;
        }

    }
}
