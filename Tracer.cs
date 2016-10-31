using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;
using System.IO;
namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    class Tracer
    {
        
        

        public List<Tile> getTrace(World world, Game game, Car self)
        {
            
           
            //Check Position
            int[] cPos = new int[2];
            cPos[0] = (int)(self.X/800);
            cPos[1] = (int)(self.Y/800);

            //Заполнение массива тайлов, и массива с количеством шагов до тайла.
            Tile[,] masOfTiles= new Tile[world.Width,world.Height];
            int[,] mapI = new int[world.Width, world.Height];
            for (int i = 0; i < world.Width; i++)
            {
                for (int j=0;j< world.Height; j++)
                {
                    mapI[i, j] = -1;
                    masOfTiles[i, j] = new Tile(i,j,world.TilesXY[i][j]);

                }

            }

            //Получение двумерного массива с шириной и длиной world'a(в тайлах), в каждой ячейке которого содержится количество шагов от машинки, до этого тайла.
            mapI = stepTile(masOfTiles, cPos, mapI,self);

            

            for(int i =0;i< world.Width;i++)
            {
                for (int j = 0; j < world.Height;j++)
            {
                //Вывод двумерного массива mapI для проверки(Не знаю куда выводить. Возможно стоит сразу отрисовывать в LocalRunner'е).   
            }
                
            }


            //Создание коллекции с маршрутом из моего местоположения в следующий вейпоинт, и коллекции с обратным маршрутом.
            List<Tile> wayOfTilesFromFinish = new List<Tile>();
            List<Tile> wayOfTilesFromMyPosition = new List<Tile>();
            
            
            
            
            int setX = self.NextWaypointX;
            int setY = self.NextWaypointY;
            wayOfTilesFromFinish = getNextTrace(setX, setY, mapI, masOfTiles);
            for (int i = wayOfTilesFromFinish.Count - 1; i >= 0; i--)
            {
                wayOfTilesFromMyPosition.Add(wayOfTilesFromFinish[i]);
            }


            /*//NextWayP
            int[] nextTPos = new int[2];
            nextTPos[0] = self.NextWaypointX;
            nextTPos[1] = self.NextWaypointY;

            List<Tile> wayBetweenNextWaypoints = new List<Tile>();
            List<Tile> wayBetweenNextWaypoints2 = new List<Tile>();
            setX = world.Waypoints[self.NextWaypointIndex+1][0];
            setY = world.Waypoints[self.NextWaypointIndex+1][1];

            for (int i = 0; i < world.Width; i++)
            {
                for (int j = 0; j < world.Height; j++)
                {
                    mapI[i, j] = -1;
                }
            }
            mapI = stepTile(masOfTiles, nextTPos, mapI);
            

            wayBetweenNextWaypoints = getNextTrace(setX, setY, mapI, masOfTiles);
            for (int i = wayBetweenNextWaypoints.Count - 1; i >= 0; i--)
            {
                wayBetweenNextWaypoints2.Add(wayBetweenNextWaypoints[i]);
            }*/


            List<Tile> resultTrace = new List<Tile>();
            foreach(Tile tile in wayOfTilesFromMyPosition)
            {
                resultTrace.Add(tile);
            }
           /* foreach (Tile tile in wayBetweenNextWaypoints2)
            {
                
                    resultTrace.Add(tile);
                
            }*/

            return resultTrace;
        }


        public List<Tile> getNextTrace(int setX,int setY,int[,] mapI,Tile[,] masOfTiles)
        {
            List<Tile> wayOfTilesFromFinish = new List<Tile>();
            //Задаем первый тайл для прокладки маршрута - вейпоинт(его координаты)
            int distance = mapI[setX,setY];
            int[] tileOfThisStep = new int[2];
            tileOfThisStep[0] = setX;
            tileOfThisStep[1] = setY;

            for (int i = distance; i >= 0; i--)
            {
                
                //Добавляем текущий тайл в коллекцию с маршрутом.
                wayOfTilesFromFinish.Add(masOfTiles[tileOfThisStep[0], tileOfThisStep[1]]);
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
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] - 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0] + 1, cPos[1]]);
                    masOfSoseds.Add(masOfTiles[cPos[0] - 1, cPos[1]]);
                    break;

                case TileType.LeftHeadedT:
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] - 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] + 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0] + 1, cPos[1]]);
                    break;

                case TileType.RightHeadedT:
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] - 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] + 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0] - 1, cPos[1]]);
                    break;

                case TileType.TopHeadedT:                  
                    masOfSoseds.Add(masOfTiles[cPos[0], cPos[1] + 1]);
                    masOfSoseds.Add(masOfTiles[cPos[0] + 1, cPos[1]]);
                    masOfSoseds.Add(masOfTiles[cPos[0] - 1, cPos[1]]);
                    break;

                default: break;
            }
            return masOfSoseds;
        }

        public int[,] stepTile(Tile[,] masOfTiles, int[] cPos, int[,] mapI,Car self)
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
                    for(int k=0;k<masOfTiles.GetLength(0);k++)
                    {
                        for (int j = 0; j < masOfTiles.GetLength(1); j++)
                        {
                            if ((masOfTiles[k, j].type != TileType.Empty)&&(mapI[k,j]==-1))
                            {
                                schet++;
                            }
                        }
                    }
                    /*if(schet==0)
                    {
                        isWaypoint = false;
                    }*/
                    if ((thisSoseds[i].X == self.NextWaypointX) && (thisSoseds[i].Y == self.NextWaypointY))//Является ли проверяемый тайл следующим вейпоинтом.
                    {
                        isWaypoint = false;
                    }
                }
            }


            return mapI;
        }


    }
}
