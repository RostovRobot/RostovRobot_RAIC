using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    class PaintedPoint: PaintedObject
    {
        public double X;
        public double Y;
        
        public PaintedPoint(double x, double y)
        {
            X = x;
            Y = y;
        }

        public PaintedPoint(double x, double y, PaintColor color)
        {
            X = x;
            Y = y;
            setColor(color);
        }
    }
}
