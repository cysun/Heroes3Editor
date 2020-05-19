using System;
using System.Collections.Generic;
using System.Text;
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

namespace Heroes3Editor
{
    /// <summary>
    /// Interaction logic for HeroPanel.xaml
    /// </summary>
    public partial class HeroPanel : UserControl
    {
        private Hero _hero;

        public Hero Hero
        {
            set
            {
                _hero = value;
                for (int i = 0; i < 8; ++i)
                {
                    var cboBox = FindName("SkillSlot" + i) as ComboBox;
                    if (i < _hero.NumOfSkills)
                        cboBox.SelectedItem = _hero.Skills[i];
                    else if (i > _hero.NumOfSkills)
                        cboBox.IsEnabled = false;
                }

                foreach (var spell in _hero.Spells)
                {
                    var chkBox = FindName(spell) as CheckBox;
                    chkBox.IsChecked = true;
                }
            }
        }

        public HeroPanel()
        {
            InitializeComponent();
        }
    }
}
