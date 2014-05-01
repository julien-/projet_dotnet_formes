﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Polygone : Forme_simple
    {
        public Polygone(int id, string nom, List<Point> liste_points, string couleur) : base(id, nom, liste_points, couleur){}

        public int Nombre_points
        {
            get
            {
                return this._nombre_points;
            }
            set
            {
                this._nombre_points = value;
            }
        }
    }
}
