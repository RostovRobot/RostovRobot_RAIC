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
        //Tile[,] masOfTiles;
        

        public List<Tile> getTrace(World world, Game game, Car self)
        {
            //НЕ НАДО ИЗМЕНЯТЬ ТИП ОЗВРАЩАЕМОГО ЗНАЧЕНИЯ!!!!!
            //это фундаментальная вещь для командной работы - остальные УЖЕ рассчитывают, что метод getTrace вернет коллекцию тайлов
            //если необходимо изменить тип, то обязательно обсуждаем это (на он-лайн встрече, в группе ВК или еще где-то)

            //My Position
            int[] myP= new int[2];
            myP[0] = (int)(self.X / 800);
            myP[1] = (int)(self.Y / 800);

            //Next Position
            int[] nextP = new int[2];
            nextP[0] = self.NextWaypointX;
            nextP[1] = self.NextWaypointY;

            //Check Position
            int[] cPos = new int[2];
            cPos[0] = 0;
            cPos[1] = 0;
            
            //Check Position(OLD)
            int[] cOlPos = new int[2];
            cOlPos[0] = (int)(self.X / 800);
            cOlPos[1] = (int)(self.Y / 800);

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


            mapI = stepTile(masOfTiles, cPos, mapI, self);
            
            for(int i =0;i< world.Width;i++)
            {
                for (int j = 0; j < world.Height;j++)
            {
                    Console.Write(mapI[i, j] + "  ");
            }
                Console.WriteLine();
            }



            return null;
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

        public int[,] stepTile(Tile[,] masOfTiles, int[] cPos, int[,] mapI,  Car self)
        {
            
            mapI[cPos[0], cPos[1]] = 0;
            //Создаем коллекцию координат тайлов на данном шаге.
            List<int[]> tilesOfThisStep = new List<int[]>();
            tilesOfThisStep.Add(new int[2]);
            //Добавляем стартовый тайл.
            tilesOfThisStep[0][0] = cPos[0];
            tilesOfThisStep[0][1] = cPos[1];
            int indOfStep = 0;
            bool isWaypoint = true;
            while (isWaypoint)
            {
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

                for (int i = 0; i < thisSoseds.Count; i++)
                {

                    if ((mapI[thisSoseds[i].X, thisSoseds[i].Y] == -1) || (mapI[thisSoseds[i].X, thisSoseds[i].Y] > indOfStep))
                        mapI[thisSoseds[i].X, thisSoseds[i].Y] = indOfStep;
                }
                indOfStep++;
                for (int i = 0; i < thisSoseds.Count; i++)
                {
                    tilesOfThisStep[i][0] = thisSoseds[i].X;
                    tilesOfThisStep[i][1] = thisSoseds[i].Y;
                    if((thisSoseds[i].X == self.NextWaypointX)&& (thisSoseds[i].Y == self.NextWaypointY))
                    {
                        isWaypoint = false;
                    }
                }
            }





            return mapI;
        }



    }
}
