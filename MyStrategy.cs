using System;
using System.Collections.Generic;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk {
    public sealed class MyStrategy : IStrategy {
        Tracer tracer = new Tracer();
        iPoint ipoint = new iPoint();
        Regulator regul = new Regulator();
        CrashProtect crPotect = new CrashProtect();
        RepeaterPainter painter = new RepeaterPainter();


        public void Move(Car self, World world, Game game, Move move) {
            if (world.Tick > game.InitialFreezeDurationTicks) //проверяем, что прошло уже 300 первых тиков бездействия
            { 
                List<Tile> traceTiles = tracer.getTrace(world, game, self);

                double[] nextPointCoordinate = ipoint.getNextPointXY(traceTiles, world, game, self);

                int[] nextPointIntCoordinate = new int[nextPointCoordinate.Length];
                for(int i=0;i<nextPointCoordinate.Length;i++)
                {
                    nextPointIntCoordinate[i] = Convert.ToInt32(nextPointCoordinate[i]);
                }

                move.WheelTurn = regul.getU(nextPointIntCoordinate[0], nextPointIntCoordinate[1], self);
                move.EnginePower = 0.5D;
            }
              
        }
    }
}