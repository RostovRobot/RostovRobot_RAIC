using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    class iPoint
    {
        double k = 0.3;//Коэффицент смещения от центра тайла Мах=0.5, Min=0;
        public double[] getNextPointXY(List<Tile> tiles, World world, Game game, Car self)
        {
            //возвращать в массиве можно координаты ВСЕХ точек всего маршрута,
            //а учитываться в регуляторе будут только первые две
            //формат такого массива: [100,100,200,100,300,100]
            TileType t = tiles[0].type;
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

            //если вычисляется что-то странное или возникла ошибка,
            //то возвращаем координаты центра след вейпоинта.
        }
    }
}
