using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace SeekOFix
{
    public static class UIUtils
    {
        public static T CreateControl<T>(params object[] args)
            where T : Control
        {
            T result = (T) typeof(T).GetConstructor(args.Select(x => x.GetType()).ToArray()).Invoke(args);
            var defaultsFunc = typeof(UIUtils).GetMethod(
                "ApplyControlDefaults",
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                new Type[] { typeof(T) },
                null
            );
            defaultsFunc?.Invoke(null, new object[] { result });
            return result;
        }

        public static T CreateChild<T>(Control parent, params object[] args)
            where T : Control
        {
            T result = CreateControl<T>(args);
            parent.Controls.Add(result);
            return result;
        }

        public static T CreateInLayout<T>(TableLayoutPanel layout, int column, int row, params object[] args)
            where T : Control
        {
            T result = CreateControl<T>(args);
            layout.Controls.Add(result, column, row);
            return result;
        }

        public static void AddColumn(TableLayoutPanel layout, SizeType sizeType, float size = 0.0f)
        {
            layout.ColumnCount += 1;
            layout.ColumnStyles.Add(new ColumnStyle(sizeType, size));
        }

        public static void AddRow(TableLayoutPanel layout, SizeType sizeType, float size = 0.0f)
        {
            layout.RowCount += 1;
            layout.RowStyles.Add(new RowStyle(sizeType, size));
        }

        public static TableLayoutPanel CreateLayout(Control parent)
        {
            var layout = CreateControl<TableLayoutPanel>();
            layout.Dock = DockStyle.Fill;
            layout.Margin = new Padding(0);
            parent.Controls.Add(layout);
            return layout;
        }

        public static TableLayoutPanel CreateSublayout(TableLayoutPanel layout, int column, int row)
        {
            var sublayout = CreateControl<TableLayoutPanel>();
            sublayout.Dock = DockStyle.Fill;
            sublayout.Margin = new Padding(0);
            layout.Controls.Add(sublayout, column, row);
            return sublayout;
        }

        public static void SetColumnSpan(Control control, int count)
        {
            var layout = control?.Parent as TableLayoutPanel;
            layout?.SetColumnSpan(control, count);
        }

        public static void SetRowSpan(Control control, int count)
        {
            var layout = control?.Parent as TableLayoutPanel;
            layout?.SetColumnSpan(control, count);
        }

        public static void SetSpan(Control control, int columns, int rows)
        {
            var layout = control?.Parent as TableLayoutPanel;
            layout?.SetColumnSpan(control, columns);
            layout?.SetRowSpan(control, rows);
        }

        public static void SetToolTip(Control control, string text)
        {
            var toolTip = new ToolTip();
            toolTip.SetToolTip(control, text);
        }

        private static void ApplyControlDefaults(Button button)
        {
            button.UseVisualStyleBackColor = true;
        }

        private static void ApplyControlDefaults(CheckBox check)
        {
            check.AutoSize = true;
        }

        private static void ApplyControlDefaults(Label label)
        {
            label.AutoSize = true;
        }

        private static void ApplyControlDefaults(RadioButton radio)
        {
            radio.AutoSize = true;
        }

        private static void ApplyControlDefaults(TabPage page)
        {
            page.UseVisualStyleBackColor = true;
        }
    }
}
