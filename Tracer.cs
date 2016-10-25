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
            



            return null;
        }


        public void stepTile(Tile[,] masOfTiles, int[] cPos, int counter, int[,] mapI, int[] cOld)
        {
            int x=0;
            int y=0;
            mapI[cPos[0],cPos[1]] = counter;
            switch(masOfTiles[cPos[0],cPos[1]].type)
            {
                case TileType.LeftTopCorner:
                    if (cOld[0] > cPos[0])
                    {
                        y = 1;
                    }
                    else
                    { x = 1; } break;
                case TileType.RightBottomCorner:
                    if(cOld[0]<cPos[0])
                    {
                        y = -1;
                    }
                    else
                    { x = -1; } break;
                case TileType.RightTopCorner:
                    if(cOld[0]<cPos[0])
                    {
                        y = 1;
                    }
                    else
                    { x = -1; } break;
                case TileType.LeftBottomCorner:
                    if(cOld[0]>cPos[0])
                    {
                        y = -1;
                    }
                    else
                    { x = 1; } break;

                case TileType.Horizontal:
                    if(cOld[0]>cPos[0])
                    {
                        x = -1;
                    }
                    else
                    { x = 1; } break;
                case TileType.Vertical:
                    if(cOld[1]>cPos[1])
                    {
                        y = -1;
                    }
                    else
                    { y = 1; } break;
                    

                default: break;
            }



            cOld[0] = cPos[0];
            cOld[1] = cPos[1];
            cPos[0] += x;
            cPos[1] += y;
            
        }



    }
}
