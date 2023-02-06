using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void OpenCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var openDlg = new OpenFileDialog { Filter = "HoMM3 Savegames |*.CGM;*.GM*" };
            if (openDlg.ShowDialog() == true)
            {
                Game = new Game(openDlg.FileName);

                heroTabs.Items.Clear();
                heroTabs.Visibility = Visibility.Hidden;
                heroCboBox.IsEnabled = true;
                heroSearchBtn.IsEnabled = true;

                status.Text = openDlg.FileName;
            }
        }

        private void SaveCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = Game != null;
        }

        private void SaveCmdExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var saveDlg = new SaveFileDialog { Filter = "HoMM3 Savegames |*.*GM;*.GM*" };
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

        private void SearchHero(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(heroCboBox.Text)) return;

            var added = Game.SearchHero(heroCboBox.Text);
            if (added)
            {
                var hero = Game.Heroes.Last();
                var heroTab = new TabItem()
                {
                    Header = hero.Name
                };
                heroTab.Content = new HeroPanel()
                {
                    Hero = hero
                };
                heroTabs.Items.Add(heroTab);
                heroTabs.Visibility = Visibility.Visible;
                heroTab.IsSelected = true;
            }
        }
    }
}
