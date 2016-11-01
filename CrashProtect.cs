using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    class CrashProtect
    {
        private double turn = 0;
        private int steps = 0;
        private double oldX = 0;
        private double oldY = 0;
        private int deltaPorog = 60;  //подобрать
        private int stepsPorog = 40;  //подобрать
        private int turnAgo = 1;
        public bool isCrash(Car self)
        {
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
            if (steps > stepsPorog)
            {
                
                return true;

            }
            if (turnAgo == 0)
            turnAgo = 1;
            return false;
        }

        public void CrashedMove(Move move, Car self)
        {
            
            if (steps != 0)
            {
                if (turnAgo == 1)
                    turn = -self.WheelTurn;
                    move.WheelTurn = turn;
                    turnAgo = 0;

                move.EnginePower = -1.0;
            }
        }
    }
}
