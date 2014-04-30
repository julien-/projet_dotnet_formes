using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class DessinFactory : AbstractDessinFactory
    {
        public override Dessiner<Rectangle> getDessinRectangle()
        {
            return new DessinRectangle();
        }
        public override Dessiner<Segment> getDessinSegment()
        {
            return new DessinSegment();
        }
        public override Dessiner<Ellipse> getDessinEllipse()
        {
            return new DessinEllipse();
        }
        public override Dessiner<Triangle> getDessinTriangle()
        {
            return new DessinTriangle();
        }
        public override Dessiner<Polygone> getDessinPolygone()
        {
            return new DessinPolygone();
        }
    }
}
