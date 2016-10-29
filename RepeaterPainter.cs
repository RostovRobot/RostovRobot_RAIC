using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.CodeGame.CodeRacing2015.DevKit.CSharpCgdk
{
    class RepeaterPainter
    {
        private VisualClient vc;
        
        /// <summary>
        /// Создает клиент для отрисовки со стандартными параметрами (порт 13579)
        /// </summary>
        public RepeaterPainter()
        {
            vc = new VisualClient("localhost", 13579);
            vc.BeginPre();
        }

        /// <summary>
        /// !!!НЕ РАБОТЕТ!!! Рисует объекты из коллекции
        /// </summary>
        /// <param name="objects">Коллекция объектов для отрисовки</param>
        public void Paint(List<PaintedObject> objects)
        {
            try //запускаем проверку на ошибки
            {//либо коллекция может быть пустой, либо vc может быть не создан или еще что
                foreach (PaintedObject po in objects) //перебираем все объекты в коллекции
                {// на каждом шаге каждый новый объект будет доступен под именем po
                    switch (po.getType()) //узнаем тип отбъекта для отрисовки
                    {
                        case PaintedObjectType.Point:
                            break;
                        case PaintedObjectType.Line:
                            break;
                        case PaintedObjectType.Tile:
                            break;
                        case PaintedObjectType.Text:
                            break;
                        case PaintedObjectType.TileText:
                            break;
                        default:
                            break;
                    }
                }
            }catch(Exception e)
            {

            }
        }

        /// <summary>
        /// Закрашивает тайл серым цветом
        /// </summary>
        /// <param name="tile">Объект-тайл для отрисовки</param>
        public void PaintTile(Tile tile)
        {
            if(vc !=null)
            {
                double x1 = tile.X * 800 - 400;
                double y1 = tile.Y * 800 - 400;
                double x2 = tile.X * 800 + 400;
                double y2 = tile.Y * 800 + 400;
                vc.FillRect(x1, y1, x2, y2, 0.1f, 0.1f, 0.1f);
            }
        }

        /// <summary>
        /// Закрашивает тайл заданным цветом (!!!цвет не работает!!!)
        /// </summary>
        /// <param name="tile">Объект-тайл для отрисовки</param>
        /// <param name="color">Цвет отрисовки из доступных в PaintColor</param>
        public void PaintTile(Tile tile, PaintColor color)
        {
            if (vc != null)
            {
                double x1 = tile.X * 800 - 400;
                double y1 = tile.Y * 800 - 400;
                double x2 = tile.X * 800 + 400;
                double y2 = tile.Y * 800 + 400;
                float r = 0.1f;
                float g = 0.1f;
                float b = 0.1f;
                vc.FillRect(x1, y1, x2, y2, r, g, b);
            }
        }

        /// <summary>
        /// Закрашивает тайл серым цветом
        /// </summary>
        /// <param name="tileX">Координата X тайла</param>
        /// <param name="tileY">Координата Y тайла</param>
        public void PaintTile(int tileX, int tileY)
        {
            if (vc != null)
            {
                double x1 = tileX * 800 - 400;
                double y1 = tileY * 800 - 400;
                double x2 = tileX * 800 + 400;
                double y2 = tileY * 800 + 400;
                vc.FillRect(x1, y1, x2, y2, 0.1f, 0.1f, 0.1f);
            }
        }

        /// <summary>
        /// Закрашивает тайл заданным цветом (!!!цвет не работает!!!)
        /// </summary>
        /// <param name="tileX">Координата X тайла</param>
        /// <param name="tileY">Координата Y тайла</param>
        /// <param name="color">Цвет отрисовки из доступных в PaintColor</param>
        public void PaintTile(int tileX, int tileY, PaintColor color)
        {
            if (vc != null)
            {
                double x1 = tileX * 800 - 400;
                double y1 = tileY * 800 - 400;
                double x2 = tileX * 800 + 400;
                double y2 = tileY * 800 + 400;
                float r = 0.1f;
                float g = 0.1f;
                float b = 0.1f;
                vc.FillRect(x1, y1, x2, y2, r, g, b);
            }
        }

        /// <summary>
        /// Закрашивает несколько тайлов из коллекции серым цветом
        /// </summary>
        /// <param name="tiles">Коллекция объектов-тайлов для орисовки</param>
        public void PaintTile(List<Tile> tiles)
        {
            if (vc != null)
            {
                foreach (Tile tile in tiles)
                {
                    double x1 = tile.X * 800 - 400;
                    double y1 = tile.Y * 800 - 400;
                    double x2 = tile.X * 800 + 400;
                    double y2 = tile.Y * 800 + 400;
                    vc.FillRect(x1, y1, x2, y2, 0.1f, 0.1f, 0.1f);
                }
            }
        }

        /// <summary>
        /// Закрашивает несколько тайлов из коллекции заданным цветом (!!!цвет не работает!!!)
        /// </summary>
        /// <param name="tiles">Коллекция объектов-тайлов для отрисовки</param>
        /// <param name="color">Цвет отрисовки из доступных в PaintColor</param>
        public void PaintTile(List<Tile> tiles, PaintColor color)
        {
            if (vc != null)
            {
                foreach (Tile tile in tiles)
                {
                    double x1 = tile.X * 800 - 400;
                    double y1 = tile.Y * 800 - 400;
                    double x2 = tile.X * 800 + 400;
                    double y2 = tile.Y * 800 + 400;
                    float r = 0.1f;
                    float g = 0.1f;
                    float b = 0.1f;
                    vc.FillRect(x1, y1, x2, y2, 0.1f, 0.1f, 0.1f);
                }
            }
        }

        /// <summary>
        /// !!!ПОКА ПУСТОЙ МЕТОД!!! Рисует точку зеленым цветом
        /// </summary>
        /// <param name="X">Координата X точки</param>
        /// <param name="Y">Координата Y точки</param>
        public void PaintPoint(double X, double Y)
        {
            if (vc != null)
            {
                vc.Circle(X, Y, 3, 0.0f, 1.0f, 0.0f);
            }
        }

        /// <summary>
        /// Рисует точку заданным цветом (!!!цвет не работает!!!)
        /// </summary>
        /// <param name="X">Координата X точки</param>
        /// <param name="Y">Координата Y точки</param>
        /// <param name="color">Цвет отрисовки из доступных в PaintColor</param>
        public void PaintPoint(double X, double Y, PaintColor color)
        {
            if (vc != null)
            {
                float r = 0.0f;
                float g = 1.0f;
                float b = 0.0f;
                vc.Circle(X, Y, 3, r, g, b);
            }
        }

        /// <summary>
        /// Рисует последовательность линий красным цветом
        /// </summary>
        /// <param name="seria">Массив координат точек, задающих линии</param>
        public void PaintLineSeria(double[] seria)
        {
            if (vc != null && seria.Length>3)
            {
                for(int i=1; i<seria.Length/2;i++)
                {
                    double x1 = seria[(i-1) * 2];
                    double y1 = seria[(i-1) * 2 + 1];
                    double x2 = seria[i * 2];
                    double y2 = seria[i * 2 + 1];
                    vc.Line(x1, y1, x2, y2, 1.0f, 0.0f, 0.0f);
                }
            }
        }

        /// <summary>
        /// Рисует последовательность линий заданным цветом (!!!цвет не рабоает!!!)
        /// </summary>
        /// <param name="seria">Массив координат точек, задающих линии</param>
        /// <param name="color">Цвет отрисовки из доступных в PaintColor</param>
        public void PaintLineSeria(double[] seria, PaintColor color)
        {
            if (vc != null && seria.Length > 3)
            {
                for (int i = 1; i < seria.Length / 2; i++)
                {
                    double x1 = seria[(i - 1) * 2];
                    double y1 = seria[(i - 1) * 2 + 1];
                    double x2 = seria[i * 2];
                    double y2 = seria[i * 2 + 1];
                    float r = 1.0f;
                    float g = 0.0f;
                    float b = 0.0f;
                    vc.Line(x1, y1, x2, y2, r, g, b);
                }
            }
        }
    }
}
