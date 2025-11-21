namespace TechStore.WinForms
{
    public static class ListBoxExtensions
    {
        public static void EnableColorfulItems(this ListBox listBox)
        {
            // Подписываемся на событие отрисовки элементов
            listBox.DrawMode = DrawMode.OwnerDrawFixed;
            listBox.DrawItem += ListBox_DrawItem;
        }

        private static void ListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            // Это стандартный код для красивого отображения
            if (sender is not ListBox listBox) return;
            if (e.Index < 0) return;

            e.DrawBackground(); // Рисуем стандартный фон

            // Получаем элемент для отрисовки
            var item = listBox.Items[e.Index];
            string text = item.ToString(); // Текст, который будет отображаться
            Color color = listBox.ForeColor; // Цвет по умолчанию

            // Если элемент - товар и его нет в наличии, красим в серый
            if (item is TechStore.Models.Product product && product.StockQuantity == 0)
            {
                color = Color.Gray; // Можешь заменить на Color.LightCoral для красного
            }

            // Если это заголовок категории (начинается с "---"), делаем его жирным
            if (item is string str && str.StartsWith("---"))
            {
                color = Color.DarkBlue;
                using (var boldFont = new Font(listBox.Font, FontStyle.Bold))
                using (var brush = new SolidBrush(color))
                {
                    e.Graphics.DrawString(text, boldFont, brush, e.Bounds);
                }
            }
            else
            {
                // Рисуем текст выбранным цветом
                using (var brush = new SolidBrush(color))
                {
                    e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
                }
            }

            e.DrawFocusRectangle(); // Рисуем рамку фокуса
        }
    }
}