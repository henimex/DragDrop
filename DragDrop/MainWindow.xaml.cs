using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DragDrop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Point ListBoxOneMousePos;
        Point ListBoxTwoMousePos;

        private void ListBoxOne_OnMouseMove(object sender, MouseEventArgs e)
        {
            MouseMove(e,ListBoxOne);
        }

        private void ListBoxTwo_OnMouseMove(object sender, MouseEventArgs e)
        {
            MouseMove(e, ListBoxTwo);
        }

        private void ListBoxOne_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                ListBoxOne.Items.Add(listItem);
            }
        }

        private void ListBoxTwo_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(DataFormats.FileDrop) is ListBoxItem listItem)
            {
                ListBoxTwo.Items.Add(listItem);
            }
        }

        private void ListBoxOne_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxOneMousePos = e.GetPosition(null);
        }

        private void ListBoxTwo_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxTwoMousePos = e.GetPosition(null);
        }
        
        private void MouseMove(MouseEventArgs e, ListBox listItem)
        {
            Point mPos = e.GetPosition(null);

            if (e.LeftButton == MouseButtonState.Pressed &&
                Math.Abs(mPos.X) > SystemParameters.MinimumHorizontalDragDistance &&
                Math.Abs(mPos.Y) > SystemParameters.MinimumVerticalDragDistance)
            {
                try
                {
                    ListBoxItem selectedItem = (ListBoxItem)listItem.SelectedItem;
                    listItem.Items.Remove(selectedItem);
                    System.Windows.DragDrop.DoDragDrop(this, new DataObject(DataFormats.FileDrop, selectedItem),
                        DragDropEffects.Copy);
                    if (selectedItem.Parent == null)
                    {
                        listItem.Items.Add(selectedItem);
                    }
                }
                catch
                {
                }
            }
        }
    }
}
