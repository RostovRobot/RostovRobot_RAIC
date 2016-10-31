using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{

    class iPoint
    {
        public static double[] Step(double[,] points, double t)//Безье
        {
            if (points.GetLength(0) == 2)
            {
                return new double[] { points[0, 0] + t * (points[1, 0] - points[0, 0]), points[0, 1] + t * (points[1, 1] - points[0, 1]) };


            }
            else

            {
                double[,] nPts = new double[points.GetLength(0) - 1, 2];

                for (int i = 0; i < nPts.GetLength(0); i++)
                {
                    nPts[i, 0] = points[i, 0] + t * (points[i + 1, 0] - points[i, 0]);
                    nPts[i, 1] = points[i, 1] + t * (points[i + 1, 1] - points[i, 1]);
                }

                return Step(nPts, t);

            }


        }
        double k = 0.3;//Коэффицент смещения от центра тайла Мах=0.5, Min=0;


        public double[] getPointXY(List<Tile> tiles, World world, Game game, Car self,int i)//Возвращаем ипервичную, необработанную координату для данного , i-того тайла из массива.
        {
            //возвращать в массиве можно координаты ВСЕХ точек всего маршрута,
            //а учитываться в регуляторе будут только первые две
            //формат такого массива: [100,100,200,100,300,100]
            //TileType t = tiles[0].type;
            if ((tiles[i-1].X == tiles[i+1].X) || (tiles[i-1].Y == tiles[i+1].Y))
            {
                return new double[2] { (tiles[i].X - 0.5D) * game.TrackTileSize, (tiles[i].Y - 0.5D) * game.TrackTileSize };

            }
            else
            {
                double X = (tiles[i].X - 0.5D) * game.TrackTileSize + (tiles[i+1].X - tiles[i-1].X) * k * game.TrackTileSize;
                double Y = (tiles[i].Y - 0.5D) * game.TrackTileSize + (tiles[i+1].Y - tiles[i-1].Y) * k * game.TrackTileSize;
                return new double[2] { X, Y };//Try again
            }

            //комментарий Сергея из секции
            //если вычисляется что-то странное или возникла ошибка,
            //то возвращаем координаты центра след вейпоинта.
        }
        public double[] getPointXY(List<Tile> tiles, World world, Game game, Car self)//Первый вариант
        {
            //возвращать в массиве можно координаты ВСЕХ точек всего маршрута,
            //а учитываться в регуляторе будут только первые две
            //формат такого массива: [100,100,200,100,300,100]
            //TileType t = tiles[0].type;
            if ((tiles[0].X == tiles[2].X) || (tiles[0].Y == tiles[2].Y))
            {
                return new double[2] { (tiles[1].X - 0.5D) * game.TrackTileSize, (tiles[1].Y - 0.5D) * game.TrackTileSize };

            }
            else
            {
                double X = (tiles[1].X - 0.5D) * game.TrackTileSize + (tiles[2].X - tiles[0].X) * k * game.TrackTileSize;
                double Y = (tiles[1].Y - 0.5D) * game.TrackTileSize + (tiles[2].Y - tiles[0].Y) * k * game.TrackTileSize;
                return new double[2] { X, Y };//Try again
            }

            //комментарий Сергея из секции
            //если вычисляется что-то странное или возникла ошибка,
            //то возвращаем координаты центра след вейпоинта.
        }

        public double[,] getStandartPoints(List<Tile> tiles)
        {
            double[,] points = new double[tiles.Count,2];
            points[0, 0] = tiles[0].X*800 + 400;
            points[0, 1] = tiles[0].Y*800 + 400;
            points[tiles.Count-1,0] = tiles[tiles.Count-1].X * 800 + 400;
            points[tiles.Count - 1, 1] = tiles[tiles.Count - 1].Y * 800 + 400;
            for (int i = 1; i < points.GetLength(0)-1; i++)
            {
                double[] a;

                if ((tiles[i - 1].X == tiles[i + 1].X) || (tiles[i - 1].Y == tiles[i + 1].Y))
                {
                    a= new double[2] { (tiles[i].X - 0.5D) * 400, (tiles[i].Y - 0.5D) * 400 };

                }
                else
                {
                    double X = (tiles[i].X - 0.5D) * 400 + (tiles[i + 1].X - tiles[i - 1].X) * k * 400;
                    double Y = (tiles[i].Y - 0.5D) * 400 + (tiles[i + 1].Y - tiles[i - 1].Y) * k * 400;
                    a= new double[2] { X, Y };//Try again
                }
                points[i, 0] = a[0];
                points[i, 1] = a[1];
            }
            return points;


        }
        public double[,] SgladBez(double[,] points,List<Tile> tiles)//Bezie
        {
            double[,] nPts = new double[points.GetLength(0),2];
            for (int i = 0; i <= nPts.GetLength(0); i++)
            {
                var a = Step(points, i / nPts.GetLength(0));
                nPts[i, 0] = a[0];
                nPts[i, 1] = a[1];
            }
            bool flag = false;
            for (int i = 0; i <= nPts.GetLength(0); i++)
            {
                if((nPts[i,0]<tiles[i].X*800)||(nPts[i, 1] < tiles[i].Y * 800)||(nPts[i, 0] > tiles[i].X * 800+800) ||(nPts[i, 1] > tiles[i].Y * 800 + 800))
                {
                    points[i, 0] = points[i, 0] - 50 * (nPts[i, 0] - tiles[i].X * 800) / Math.Abs(nPts[i, 0] - tiles[i].X * 800); ;
                    points[i, 1] = points[i, 1] - 50 * (nPts[i, 1] - tiles[i].Y) / Math.Abs(nPts[i, 1] - tiles[i].Y * 800);
                    flag = true;
                }
                
            }
            if (flag)
            {
                return (SgladBez(points, tiles));

            }
            else
            {
                return nPts;
            }
        }


    }
}
