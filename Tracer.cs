using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk.Model;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    class Tracer
    {
        //Tile[,] masOfTiles;
        


        public List<Tile> getTrace(World world, Game game, Car self)
        {
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
            int n = 6;//Размер поля n x n

            Tile[,] masOfTiles= new Tile[n,n];
            int[,] mapI = new int[n,n];
            for (int i = 0; i < n; i++)
            {
                for (int j=0;j<n;j++)
                {
                    mapI[i, j] = -1;
                    masOfTiles[i, j] = new Tile(i,j,world.TilesXY[i][j]);

                }

            }


            int counter = 0;
            int tHeaded = 0;



            return null;
        }

        public void getSosed(Tile[,] masOfTiles, int[] cPos, int n)
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
        }

        public void stepTile(Tile[,] masOfTiles, int[] cPos, int counter, int tHeaded, int[,] mapI, int[] cOld, Car self)
        {
            int x=0;
            int y=0;
            mapI[cPos[0], cPos[1]] = 0;

            while ((cPos[0] != self.NextWaypointX) && (cPos[1] != self.NextWaypointY))
            {




            }




            cOld[0] = cPos[0];
            cOld[1] = cPos[1];
            cPos[0] += x;
            cPos[1] += y;
            
        }



    }
}
