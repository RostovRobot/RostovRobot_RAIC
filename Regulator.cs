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
        public double getU(int X, int Y, Car self)
        {
            double kp = 1.0D; //необходимо подобрать
            double kd = 0.1D; //необходимо подобрать
            double ki = 0.1D; //необходимо подобрать
            double dt = 0.001;

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
