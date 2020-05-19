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
using Heroes3Editor.Models;
using Microsoft.Win32;

namespace Heroes3Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Game Game { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            heroTabs.Visibility = Visibility.Hidden;
        }

        private void OpenCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var openDlg = new OpenFileDialog { Filter = "HoMM3 Savegames |*.CGM" };
            if (openDlg.ShowDialog() == true)
            {
                Game = new Game(openDlg.FileName);

                if (Game.Heroes.Count > 0)
                {
                    heroTabs.Items.Clear();
                    foreach (var hero in Game.Heroes)
                    {
                        var heroTab = new TabItem()
                        {
                            Header = hero.Name
                        };
                        heroTab.Content = new HeroPanel()
                        {
                            Hero = hero
                        };
                        heroTabs.Items.Add(heroTab);
                    }
                    heroTabs.Visibility = Visibility.Visible;
                }

                status.Text = openDlg.FileName;
            }
        }

        private void SaveCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Game != null;
        }

        private void SaveCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var saveDlg = new SaveFileDialog { Filter = "HoMM3 Savegames |*.CGM" };
            if (saveDlg.ShowDialog() == true)
            {
                Game.Save(saveDlg.FileName);
                status.Text = saveDlg.FileName;
            }
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
