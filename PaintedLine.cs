using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    class PaintedLine:PaintedObject
    {
        public double startX;
        public double startY;
        public double endX;
        public double endY;

        public PaintedLine(double startx, double starty, double endx, double endy)
        {
            startX = startx;
            startY = starty;
            endX = endx;
            endY = endy;
        }

        public PaintedLine(double startx, double starty, double endx, double endy, PaintColor color)
        {
            startX = startx;
            startY = starty;
            endX = endx;
            endY = endy;
            setColor(color);
        }
    }
}
