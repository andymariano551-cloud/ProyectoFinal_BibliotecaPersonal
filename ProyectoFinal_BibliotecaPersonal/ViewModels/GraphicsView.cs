using Microsoft.Maui.Graphics;

namespace ProyectoFinal_BibliotecaPersonal.Drawables
{
    public class StatisticsDrawable : IDrawable
    {
        public int ReadBooks { get; set; } = 0;
        public int UnreadBooks { get; set; } = 0;

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            float total = ReadBooks + UnreadBooks;
            if (total == 0)
            {
                canvas.FontColor = Colors.Gray;
                canvas.FontSize = 18;
                canvas.DrawString("No hay datos suficientes", dirtyRect, HorizontalAlignment.Center, VerticalAlignment.Center);
                return;
            }

            // Dimensiones del gráfico circular
            float width = 200;
            float height = 200;
            float x = (dirtyRect.Width - width) / 2;
            float y = 20;

            // Calcular ángulos
            float readAngle = (ReadBooks / total) * 360f;
            float unreadAngle = (UnreadBooks / total) * 360f;

            // Dibujar porción de Leídos (Verde)
            canvas.FillColor = Colors.MediumSeaGreen;
            canvas.FillArc(x, y, width, height, 0, readAngle, true);

            // Dibujar porción de Pendientes (Gris/Azul)
            canvas.FillColor = Colors.LightSlateGray;
            canvas.FillArc(x, y, width, height, readAngle, unreadAngle, true);

            // Textos descriptivos
            canvas.FontColor = Colors.Black;
            canvas.FontSize = 16;
            canvas.DrawString($"Leídos: {ReadBooks}", x, y + height + 30, HorizontalAlignment.Left);
            canvas.DrawString($"Pendientes: {UnreadBooks}", x + width - 80, y + height + 30, HorizontalAlignment.Left);
        }
    }
}   