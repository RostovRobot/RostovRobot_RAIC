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
                if (crPotect.isCrash(self, move))
                {
                    //вызов действий при залипании
                } else {
                    List<Tile> traceTiles = tracer.getTrace(world, game, self);
                    //вот тут мы отрисовываем полученную коллекцию тайлов
                    painter.PaintTile(traceTiles);

                    double[] nextPointCoordinate = ipoint.getPointXY(traceTiles, world, game, self);
                    //вот тут мы отрисовываем полученный массив точек
                    if (nextPointCoordinate.Length > 3) //если в массиве больше 3 значений (две и больше точек в массиве)
                    { //то
                        painter.PaintLineSeria(nextPointCoordinate); //отрисовываем последовательность линий
                    }else
                    { //иначе
                        if (nextPointCoordinate.Length > 1) painter.PaintLine(self.X, self.Y, nextPointCoordinate[0], nextPointCoordinate[1]); //отрисовываем линию от машинки до точки
                    }

                    int[] nextPointIntCoordinate = new int[nextPointCoordinate.Length];
                    for (int i = 0; i < nextPointCoordinate.Length; i++)
                    {
                        nextPointIntCoordinate[i] = Convert.ToInt32(nextPointCoordinate[i]);
                    }

                    move.WheelTurn = regul.getU(nextPointIntCoordinate[0], nextPointIntCoordinate[1], self);
                    move.EnginePower = 0.5D;
                }
            }
              
        }
    }
}