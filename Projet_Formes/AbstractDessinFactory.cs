using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    public enum FactoryDessin
    {
        DESSIN_FACTORY
    }

    abstract class AbstractDessinFactory
    {
        public abstract Dessiner<Rectangle> getDessinRectangle();
        public abstract Dessiner<Segment> getDessinSegment();
        public abstract Dessiner<Ellipse> getDessinEllipse();
        public abstract Dessiner<Triangle> getDessinTriangle();
        public abstract Dessiner<Polygone> getDessinPolygone();

        public static AbstractDessinFactory getFactory(FactoryType type)
        {

            if (type.Equals(FactoryDessin.DESSIN_FACTORY))
                return new DessinFactory();
            return null;
        }
    }
}
