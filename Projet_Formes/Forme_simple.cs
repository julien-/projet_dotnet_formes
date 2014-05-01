using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Forme_simple : Forme
    {
        protected List<Point> _tabcoord;
        protected string _couleur;

       

        public Forme_simple(int id, string nom, string couleur, List<Point> tabcoord)
            : base(id, nom)
        {
            this._tabcoord = tabcoord;
            this._couleur = couleur;
        }

        //public Point Point1
        //{
        //    get
        //    {
        //        return this._tabcoord.ElementAt(0);
        //    }
        //    set
        //    {

        //        this._tabcoord[0] = value;
        //    }
        //}

        //public Point Point2
        //{
        //    get
        //    {
        //        return this._tabcoord.ElementAt(1);
        //    }
        //    set
        //    {
        //        this._tabcoord[1] = value;
        //    }
        //}

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


        public List<Point> TabCoord
        {
            get
            {
                return this._tabcoord;
            }
            set
            {

                this._tabcoord = value;
            }
        }

        public override void Write()
        {
            base.Write();
            Console.Out.WriteLine("Id : " + this._id);
            Console.Out.WriteLine("Couleur : " + this._couleur);
            Console.Out.WriteLine("Point1 (" + this._tabcoord[0].X + "," + this._tabcoord[0].Y + ")");
            Console.Out.WriteLine("Point2 (" + this._tabcoord[1].X + "," + this._tabcoord[1].Y + ")");
        }

        public override void translation(float x, float y)
        {
            this._tabcoord[0].X = this._tabcoord[0].X + x;
            this._tabcoord[0].Y = this._tabcoord[0].Y + y;

            this._tabcoord[1].X = this._tabcoord[1].X + x;
            this._tabcoord[1].Y = this._tabcoord[1].Y + y;
        }

        public override void homothetie(int coeff)
        {

        }
    }
}
