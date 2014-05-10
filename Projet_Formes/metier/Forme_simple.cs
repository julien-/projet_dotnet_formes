using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    public abstract class Forme_simple : Forme
    {
        protected int _couleur;
        protected DessinFormeSimple _dessinateur;

        public Forme_simple(int id, string nom, int couleur) : base (id, nom)
        {
            this._couleur = couleur;
        }

        public DessinFormeSimple Dessinateur
        {
            get
            {
                return this._dessinateur;
            }
            set
            {
                this._dessinateur = value;
            }
        }

        public int Couleur
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

        public override void translation(Point point1, Point point2) { }

        public override void homothetie(int zoom){}

        public abstract Boolean recuperer(int x, int y);

        public abstract void translation_comp(Point point1, Point point2);

        public abstract void maj(Point point1, Point point2);

        public abstract void maj(Point[] tabpoints);
    }
}
