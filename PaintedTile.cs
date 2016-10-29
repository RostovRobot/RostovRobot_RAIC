using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    class PaintedTile: PaintedObject
    {
        public double X;
        public double Y;

        public PaintedTile(double tileX, double tileY)
        {
            X = tileX;
            Y = tileY;
        }

        public PaintedTile(double tileX, double tileY, PaintColor color)
        {
            X = tileX;
            Y = tileY;
            setColor(color);
        }
    }
}
