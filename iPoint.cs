﻿using System;
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
            TileType t = tiles[0].type;
            if ((tiles[0].X == tiles[2].X) || (tiles[0].Y == tiles[2].Y))
            {
                return new double[2] { (tiles[1].X - 0.5D) * game.TrackTileSize, (tiles[1].Y - 0.5D) * game.TrackTileSize };

            }
            else
            {
                double X; //= (tiles[1].X - 0.5D) * game.TrackTileSize;
                double Y; //= (tiles[1].Y - 0.5D) * game.TrackTileSize;
                X = (tiles[1].X - 0.5D) * game.TrackTileSize + (tiles[2].X - tiles[0].X) * k * game.TrackTileSize;
                Y = (tiles[1].Y - 0.5D) * game.TrackTileSize + (tiles[2].Y - tiles[0].Y) * k * game.TrackTileSize;
                return new double[2] { X, Y };
            }
        }
    }
}
