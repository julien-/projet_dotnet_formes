﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Segment : Forme_simple
    {
        public Segment(int id, string nom, string couleur, List<Point> listpoint) : base(id, nom, couleur, listpoint) { }
    }
}
