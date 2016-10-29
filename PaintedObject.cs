using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    //от этого класса унаследуются все возможные объекты для отрисовки:
    //точка
    //линия
    //тайл
    //надпись
    //надпись на тайле
    class PaintedObject
    {
        public float red=0f;
        public float green=0f;
        public float blue=0f;
        private PaintedObjectType type = PaintedObjectType.Unknoun;
        public void setColor(PaintColor color)
        {
            switch (color)
            {
                case PaintColor.Red:
                    red = 0xFF;
                    green = 0x00;
                    blue = 0x00;
                    break;
                case PaintColor.Green:
                    red = 0x00;
                    green = 0xFF;
                    blue = 0x00;
                    break;
                case PaintColor.Blue:
                    red = 0x00;
                    green = 0x00;
                    blue = 0xFF;
                    break;
                case PaintColor.Yellow:
                    red = 0xFF;
                    green = 0xFF;
                    blue = 0x00;
                    break;
                case PaintColor.Black:
                    red = 0x00;
                    green = 0x00;
                    blue = 0x00;
                    break;
                case PaintColor.Gray:
                    red = 0x30;
                    green = 0x30;
                    blue = 0x30;
                    break;
                case PaintColor.White:
                    red = 0xFF;
                    green = 0xFF;
                    blue = 0xFF;
                    break;
                default:
                    break;
            }
        }

        public PaintedObjectType getType()
        {
            return type;
        }
        
    }
}
