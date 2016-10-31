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
            if (world.Tick > game.InitialFreezeDurationTicks) //���������, ��� ������ ��� 300 ������ ����� �����������
            {
                if (crPotect.isCrash(self, move))
                {
                    //����� �������� ��� ���������
                } else {
                    List<Tile> traceTiles = tracer.getTrace(world, game, self);
                    //��� ��� �� ������������ ���������� ��������� ������
                    painter.PaintTile(traceTiles);

                    double[] nextPointCoordinate = ipoint.getPointXY(traceTiles, world, game, self);
                    //��� ��� �� ������������ ���������� ������ �����
                    if (nextPointCoordinate.Length > 3) //���� � ������� ������ 3 �������� (��� � ������ ����� � �������)
                    { //��
                        painter.PaintLineSeria(nextPointCoordinate); //������������ ������������������ �����
                    }else
                    { //�����
                        if (nextPointCoordinate.Length > 1) painter.PaintLine(self.X, self.Y, nextPointCoordinate[0], nextPointCoordinate[1]); //������������ ����� �� ������� �� �����
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