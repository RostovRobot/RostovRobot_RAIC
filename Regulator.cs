using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    class Regulator
    {
        private double errOld=0.0D;
        private double I = 0.0D;
        //пусть коэффициенты стоят вне метода
        //при необходимости можно будет добавить методы, изменяющие коэффициенты
        //(например, для разных карт или типов машин)
        private double kp = 1.0D; //необходимо подобрать
        private double kd = 0.1D; //необходимо подобрать
        private double ki = 0.1D; //необходимо подобрать
        private double dt = 0.001;
        public double getU(int X, int Y, Car self)
        {
            double err = self.GetAngleTo(X, Y);
            double P = err * kp;
            double D = kd * (err - errOld)/dt;
            I = I + ki * err*dt;

            double U = P + I + D;

            errOld = err;

            return U;
        }
    }
}
