using System;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk {
    public sealed class MyStrategy : IStrategy {
        double kp = 2.0D;
        double kd = 0.5D;
        double anOld = 0;

        public void Move(Car self, World world, Game game, Move move) {

            //1. Прокладка маршрута по тайлам. В какой следующий тайл нам двигаться. 
            //Принимает объект world, объект self, объект game.
            //Должен возвращать колекцию адресов тайлов до вейпоинта. Коллекция начинается с текущего тайла.
            //Варианты решения: волновой алгоритм, алгоритм Дейкстры и т.д.
            //Николаев Александр
            //
            //2. Выбор точки, в которую стремится машинка (на которую наводится).
            //Принимает на вход коллекцию из задачи 1, объект world, объект game, объект self.
            //Возвращает координаты точки, в которую стремимся (может быть, создадим объект Point).
            //Ткаченко Сергей
            //
            //3. Регулятор на выбранную точку.
            //Принимает на вход координаты точки из 2, объект self.
            //Возвращает управляющее воздействие, возможно - мощность мотора.
            //Литвинов Михаил
            //
            //4. Управление в методе move по данным из 3.
            //
            //
            //5. Защита от залипания.
            //Принимает на вход: ???
            //Возвращает: ???
            //Николаев Константин
            //
            //6. Связь с сервером отрисовки
            //Принимает на вход данные из пунктов 1,2,4,5.
            //Ничего не возвращает (возможно false при ошибке).
            //Овсянников Алексей Юрьевич
            //
            //7. Скрипты для запуска LocalRunnera и нашего проекта.
            //
            //
            //Ткаченко Сергей

            
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