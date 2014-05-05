using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    public abstract class Forme
    {
        protected string _nom;
        protected int _groupe = -1;
        protected int _id = 0;

        public Forme(int id, string nom)
        {
            this._id = id;
            this._nom = nom;
        }

        public string Nom
        {
            get
            {
                return this._nom;
            }
            set
            {
                this._nom = value;
            }
        }

        public int Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }


        public virtual void Write()
        {
            Console.Out.WriteLine("Nom : " + this._nom);
        }

        public abstract void maj(int x, int y); //en cours de dessin
        public abstract void maj(Point p); //definition du premier point
        public abstract void translation(int x, int y);
        public abstract void homothetie(int coeff);
    }
}
