using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    class CrashProtect
    {
        private int steps = 0;
        private double oldX = 0;
        private double oldY = 0;
        private int deltaPorog = 150;  //необходимо подобрать
        private int stepsPorog = 30;  //необходимо подобрать
        private int Timedown = 30;  //необходимо подобрать
        public bool isCrash(Car self, Move move)
        {
            //НЕ НАДО МЕНЯТЬ ВХОДНЫЕ ПАРАМЕТРЫ!!!!
            //объекта move во входных параметрах не было, теперь он откуда-то появился
            //зачем он нужен в этом методе? Этот метод только говорит, застряла ли машинка
            //он не должен ничем управлять, это задача других методов

            //исправьте, пожалуйста, так, чтобы здесь метод move не было

            double deltaX = self.X - oldX;
            double deltaY = self.Y - oldY;
            double delta = Math.Sqrt(deltaX * deltaX + deltaY * deltaY);

            if (delta < deltaPorog)
            {
                steps++;
            }
            else
            {
                steps = 0;
                oldX = self.X;
                oldY = self.Y;
            }
            if(Timedown == 0)
            {
                Timedown = 30;
            }
            if (steps > stepsPorog)
            {
                

                move.EnginePower = 0.0;
                steps--;
                return true;
            }
            return false;
        }

        public void CrashedMove(Move move)
        {
            
            if (Timedown != 0)
            {
                Timedown--;
                move.EnginePower = -1.0;
            }
        }
    }
}
