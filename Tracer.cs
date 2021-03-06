﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;
using System.IO;
namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    class Tracer
    {

        public List<Tile> getTrace(World world, Game game, Car self)//Old
        {
            //Check Position
            int[] cPos = new int[2];
            cPos[0] = self.NextWaypointX;
            cPos[1] = self.NextWaypointY;

            int nextX = (int)(self.X / 800);
            int nextY = (int)(self.Y / 800);

            //Заполнение массива тайлов, и массива с количеством шагов до тайла.
            Tile[,] masOfTiles = new Tile[world.Width, world.Height];
            int[,] mapI = new int[world.Width, world.Height];
            for (int i = 0; i < world.Width; i++)
            {
                for (int j = 0; j < world.Height; j++)
                {
                    mapI[i, j] = -1;
                    masOfTiles[i, j] = new Tile(i, j, world.TilesXY[i][j]);

                }

            }


            mapI = stepTile(masOfTiles, cPos, mapI, nextX, nextY);


            for (int i = 0; i < world.Width; i++)
            {
                for (int j = 0; j < world.Height; j++)
                {
                    //Вывод двумерного массива mapI для проверки(Не знаю куда выводить. Возможно стоит сразу отрисовывать в LocalRunner'е).   
                }

            }

            //Создание коллекции с маршрутом из моего местоположения в следующий вейпоинт, и коллекции с обратным маршрутом.
            List<Tile> wayOfTilesFromFinish = new List<Tile>();
            List<Tile> wayOfTilesFromMyPosition = new List<Tile>();

            wayOfTilesFromFinish = getNextTrace(nextX, nextY, mapI, masOfTiles);
            for (int i = wayOfTilesFromFinish.Count - 1; i >= 0; i--)
            {
                wayOfTilesFromMyPosition.Add(wayOfTilesFromFinish[i]);
            }


            //NextWayP
            nextX = self.NextWaypointX;
            nextY = self.NextWaypointY;


            List<Tile> wayBetweenNextWaypoints = new List<Tile>();
            List<Tile> wayBetweenNextWaypoints2 = new List<Tile>();


            if ((self.NextWaypointX != world.Waypoints[(world.Waypoints.GetLength(0) - 1)][0]) || (self.NextWaypointY != world.Waypoints[(world.Waypoints.GetLength(0) - 1)][1]))
            {
                cPos[0] = world.Waypoints[self.NextWaypointIndex + 1][0];
                cPos[1] = world.Waypoints[self.NextWaypointIndex + 1][1];

            }
            else
            {
                cPos[0] = world.Waypoints[0][0];
                cPos[1] = world.Waypoints[0][1];
            }

            for (int i = 0; i < world.Width; i++)
            {
                for (int j = 0; j < world.Height; j++)
                {
                    mapI[i, j] = -1;
                    
                }
            }
            
            mapI = stepTile(masOfTiles, cPos, mapI, nextX, nextY);


            wayBetweenNextWaypoints = getNextTrace(nextX, nextY, mapI, masOfTiles);
            for (int i = wayBetweenNextWaypoints.Count - 1; i >= 0; i--)
            {
                wayBetweenNextWaypoints2.Add(wayBetweenNextWaypoints[i]);
            }


            List<Tile> resultTrace = new List<Tile>();
            

            foreach (Tile tile in wayBetweenNextWaypoints2)
            {

                resultTrace.Add(tile);

            }

            foreach (Tile tile in wayOfTilesFromMyPosition)
            {
                resultTrace.Add(tile);
            }

            resultTrace.Remove(resultTrace[0]);

            resultTrace.Add(masOfTiles[(int)(self.X / 800), (int)(self.Y / 800)]);

            resultTrace.Reverse();

            return resultTrace;

        }


        public List<Tile> getNextTrace(int nextX,int nextY,int[,] mapI,Tile[,] masOfTiles)
        {
            List<Tile> wayOfTilesFromFinish = new List<Tile>();
            //Задаем первый тайл для прокладки маршрута - вейпоинт(его координаты)
            int distance = mapI[nextX,nextY];
            int[] tileOfThisStep = new int[2];
            tileOfThisStep[0] = nextX;
            tileOfThisStep[1] = nextY;

            for (int i = distance; i > 0; i--)
            {
                
                //Добавляем текущий тайл в коллекцию с маршрутом.
                //if((tileOfThisStep[0]!=nextX)||(tileOfThisStep[1]!=nextY))
                
                List<Tile> sosedsOfTile = new List<Tile>();
                sosedsOfTile = getSosed(masOfTiles, tileOfThisStep);

                //Выбираем следующий тайл из соседей текущего
                int min = i;
                foreach (Tile tile in sosedsOfTile)
                {
                    if ((mapI[tile.X, tile.Y] < i) && (mapI[tile.X, tile.Y] != -1))
                    {
                        min = mapI[tile.X, tile.Y];
                        tileOfThisStep[0] = tile.X;
                        tileOfThisStep[1] = tile.Y;

                    }

                }
                wayOfTilesFromFinish.Add(masOfTiles[tileOfThisStep[0], tileOfThisStep[1]]);

            }
            return wayOfTilesFromFinish;
        }


        public List<Tile> getSosed(Tile[,] masOfTiles, int[] cPos)
        {
            List<Tile> masOfSoseds = new List<Tile>();
            switch (masOfTiles[cPos[0], cPos[1]].type)
            {
                case TileType.LeftTopCorner:
                    masOfSoseds.Add(masOfTiles[cPos[0] + 1, cPos[1]]);
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] + 1]);
                    break;

                case TileType.RightBottomCorner:
                    masOfSoseds.Add(masOfTiles[cPos[0] - 1, cPos[1]]);
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] - 1]);
                    break;

                case TileType.RightTopCorner:
                    masOfSoseds.Add(masOfTiles[cPos[0] - 1, cPos[1]]);
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] + 1]);
                    break;

                case TileType.LeftBottomCorner:
                    masOfSoseds.Add(masOfTiles[cPos[0] + 1, cPos[1]]);
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] - 1]);
                    break;

                case TileType.Horizontal:
                    masOfSoseds.Add(masOfTiles[cPos[0] + 1, cPos[1]]);
                    masOfSoseds.Add(masOfTiles[cPos[0] - 1, cPos[1]]);
                    break;
                case TileType.Vertical:
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] - 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] + 1]);
                    break;

                case TileType.Crossroads:
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] - 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] + 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0] + 1, cPos[1]]);
                    masOfSoseds.Add(masOfTiles[cPos[0] - 1, cPos[1]]);
                    break;

                case TileType.BottomHeadedT:
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] + 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0] + 1, cPos[1]]);
                    masOfSoseds.Add(masOfTiles[cPos[0] - 1, cPos[1]]);
                    break;

                case TileType.LeftHeadedT:
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] - 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] + 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0] - 1, cPos[1]]);
                    break;

                case TileType.RightHeadedT:
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] - 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] + 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0] + 1, cPos[1]]);
                    break;

                case TileType.TopHeadedT:                  
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] - 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0] + 1, cPos[1]]);
                    masOfSoseds.Add(masOfTiles[cPos[0] - 1, cPos[1]]);
                    break;

                default: break;
            }
            return masOfSoseds;
        }


        public int[,] stepTile(Tile[,] masOfTiles, int[] cPos, int[,] mapI, int nextX, int nextY)//OLD
        {
            
            mapI[cPos[0], cPos[1]] = 0;
            //Создаем коллекцию координат тайлов на данном шаге.
            List<int[]> tilesOfThisStep = new List<int[]>();
            tilesOfThisStep.Add(new int[2]);
            //Добавляем координаты стартового тайла в коллекцию.
            tilesOfThisStep[0][0] = cPos[0];
            tilesOfThisStep[0][1] = cPos[1];
            int indOfStep = 0;//Шаг цикла(количество шагов от тайла, проверяющегося в цикле, до машинки)
            bool isWaypoint = true;
            while (isWaypoint)
            {
                
                indOfStep++;
                //Создаем массив тайлов, соседствующих с тайлами из коллекции tilesOfThisStep
                List<Tile> thisSoseds = new List<Tile>();
                for (int i = 0; i < tilesOfThisStep.Count; i++)
                {
                    List<Tile> sosGet = new List<Tile>();
                    sosGet = getSosed(masOfTiles, tilesOfThisStep[i]);
                    
                    foreach (Tile tile in sosGet)
                    {
                        if((mapI[tile.X,tile.Y])==-1)
                        thisSoseds.Add(tile);
                    }
                }
                //Присваиваем номера(кол-во шагов) тайлам, в двумерном массиве.
                for (int i = 0; i < thisSoseds.Count; i++)
                {

                    if ((mapI[thisSoseds[i].X, thisSoseds[i].Y] == -1) || (mapI[thisSoseds[i].X, thisSoseds[i].Y] > indOfStep))
                        mapI[thisSoseds[i].X, thisSoseds[i].Y] = indOfStep;
                                      
                }
                tilesOfThisStep.Clear();
                for (int i = 0; i < thisSoseds.Count; i++)
                {

                    int schet = 0;
                    tilesOfThisStep.Add(new int[2]);
                    tilesOfThisStep[i][0] = thisSoseds[i].X;
                    tilesOfThisStep[i][1] = thisSoseds[i].Y;
                    
                    if (((thisSoseds[i].X == nextX) && (thisSoseds[i].Y == nextY)))//Является ли проверяемый тайл следующим вейпоинтом.
                    {
                        isWaypoint = false;
                    }
                }
            }

            return mapI;
        }


    }
}
