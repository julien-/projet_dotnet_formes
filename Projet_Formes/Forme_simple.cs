using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Forme_simple : Forme
    {
        protected string _couleur;

        public Forme_simple(int id, string nom, string couleur) : base (id, nom)
        {
            this._couleur = couleur;
        }

        public string Couleur
        {
            get
            {
                return this._couleur;
            }
            set
            {
                this._couleur = value;
            }
        }

        public override void Write()
        {
            base.Write();
            Console.Out.WriteLine("Id : " + this._id);
            Console.Out.WriteLine("Couleur : " + this._couleur);    
        }

        public override void translation(int x, int y){}

        public override void homothetie(int coeff){}
    }
}
