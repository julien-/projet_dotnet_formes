using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    public abstract class DessinFormeSimple
    {
        protected DessinFormeSimple successor;

        public void SetSuccessor(DessinFormeSimple successor)
        {
            this.successor = successor;
        }

        public abstract void dessiner(Forme_simple entry, Graphics g);

        public abstract void contourSelection(Forme_simple forme, Graphics g);
    }

}
