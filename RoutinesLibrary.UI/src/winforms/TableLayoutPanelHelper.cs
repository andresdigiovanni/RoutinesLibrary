using System;
using System.Windows.Forms;

namespace RoutinesLibrary.UI.WinForms
{
    /// <summary>
    /// This class provides utils to use TableLayoutPanel
    /// </summary>
    /// <remarks></remarks>
    public class TableLayoutPanelHelper
    {
        /// <summary>
        /// Remove a row from a TableLayoutPanel
        /// </summary>
        /// <param name="panel">TableLayoutPanel to remove row</param>
        /// <param name="rowIndex">Row index to remove</param>
        public static void RemoveRow(TableLayoutPanel panel, int rowIndex)
        {
            int columnIndex = 0;

            for (columnIndex = 0; columnIndex <= panel.ColumnCount - 1; columnIndex++)
            {
                Control Control = panel.GetControlFromPosition(columnIndex, rowIndex);
                panel.Controls.Remove(Control);
            }

            int i = 0;
            for (i = rowIndex + 1; i <= panel.RowCount - 1; i++)
            {
                columnIndex = 0;
                for (columnIndex = 0; columnIndex <= panel.ColumnCount - 1; columnIndex++)
                {
                    Control control = panel.GetControlFromPosition(columnIndex, i);
                    panel.SetRow(control, i - 1);
                }
            }

            panel.RowCount--;
        }
    }
}