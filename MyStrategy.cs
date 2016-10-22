using System;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk {
    public sealed class MyStrategy : IStrategy {
        double kp = 2.0D;
        double kd = 0.5D;
        double anOld = 0;

        public void Move(Car self, World world, Game game, Move move) {

            //1. ��������� �������� �� ������. � ����� ��������� ���� ��� ���������. 
            //��������� ������ world, ������ self, ������ game.
            //������ ���������� �������� ������� ������ �� ���������. ��������� ���������� � �������� �����.
            //�������� �������: �������� ��������, �������� �������� � �.�.
            //�������� ���������
            //
            //2. ����� �����, � ������� ��������� ������� (�� ������� ���������).
            //��������� �� ���� ��������� �� ������ 1, ������ world, ������ game, ������ self.
            //���������� ���������� �����, � ������� ��������� (����� ����, �������� ������ Point).
            //�������� ������
            //
            //3. ��������� �� ��������� �����.
            //��������� �� ���� ���������� ����� �� 2, ������ self.
            //���������� ����������� �����������, �������� - �������� ������.
            //�������� ������
            //
            //4. ���������� � ������ move �� ������ �� 3.
            //
            //
            //5. ������ �� ���������.
            //��������� �� ����: ???
            //����������: ???
            //�������� ����������
            //
            //6. ����� � �������� ���������
            //��������� �� ���� ������ �� ������� 1,2,4,5.
            //������ �� ���������� (�������� false ��� ������).
            //���������� ������� �������
            //
            //7. ������� ��� ������� LocalRunnera � ������ �������.
            //
            //
            //�������� ������

            
            double an = self.GetAngleTo(self.NextWaypointX*800+400, self.NextWaypointY*800+400);
            double P = kp * an;
            double D = kd * (an - anOld);
            double U = P + D;
            anOld = an;

            move.EnginePower = 0.5D;

            int[][] myMass;
            myMass = new int[3][];
            myMass[0] = new int[4];
            myMass[2] = new int[4];
            myMass[3] = new int[4];


            world.TilesXY[0][0].GetType();
            //move.IsThrowProjectile = true;
            //move.IsSpillOil = true;

            /*if (world.Tick > game.InitialFreezeDurationTicks) {
                move.IsUseNitro = true;
            }*/
            move.WheelTurn = U;
        }
    }
}