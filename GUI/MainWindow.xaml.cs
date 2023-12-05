using BIZ;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ClassBIZ BIZ;
        public MainWindow()
        {
            InitializeComponent();
            BIZ = new ClassBIZ();
            MainGrid.DataContext = BIZ;
        }

        private void RbBox_Checked(object sender, RoutedEventArgs e)
        {
            if (GridBox != null && GridCircle != null)
            {
                GridBox.Visibility = Visibility.Visible;
                GridCircle.Visibility = Visibility.Collapsed;
            }
        }

        private void RbCircle_Checked(object sender, RoutedEventArgs e)
        {
            if (GridBox != null && GridCircle != null)
            {
                GridBox.Visibility = Visibility.Collapsed;
                GridCircle.Visibility = Visibility.Visible;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BIZ.PrintData();
        }
    }
}
