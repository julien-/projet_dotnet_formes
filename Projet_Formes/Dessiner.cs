using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    public abstract class Dessiner<T>
    {
        public abstract void dessiner(T entry, Graphics g);
    }
}
