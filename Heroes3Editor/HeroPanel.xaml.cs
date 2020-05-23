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

                for (int i = 0; i < 4; ++i)
                {
                    var txtBox = FindName("Attribute" + i) as TextBox;
                    txtBox.Text = _hero.Attributes[i].ToString();
                }

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

                for (int i = 0; i < 7; ++i)
                {
                    var cboBox = FindName("Creature" + i) as ComboBox;
                    var txtBox = FindName("CreatureAmount" + i) as TextBox;
                    if (_hero.Creatures[i] != null)
                    {
                        cboBox.SelectedItem = _hero.Creatures[i];
                        txtBox.Text = _hero.CreatureAmounts[i].ToString();
                    }
                    else
                    {
                        txtBox.IsEnabled = false;
                    }
                }
            }
        }

        public HeroPanel()
        {
            InitializeComponent();
        }

        private void UpdateAttribute(object sender, RoutedEventArgs e)
        {
            var txtBox = e.Source as TextBox;

            byte value;
            bool isNumber = byte.TryParse(txtBox.Text, out value);
            if (!isNumber || value < 0 || value > 99) return;

            var i = int.Parse(txtBox.Name.Substring("Attribute".Length));
            _hero.UpdateAttribute(i, value);
        }

        private void UpdateSkill(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var slot = int.Parse(cboBox.Name.Substring("SkillSlot".Length));
            var skill = cboBox.SelectedItem as string;

            var oldNumOfSkills = _hero.NumOfSkills;
            _hero.UpdateSkill(slot, skill);
            if (_hero.NumOfSkills > oldNumOfSkills && _hero.NumOfSkills < 8)
            {
                var nextCboBox = FindName("SkillSlot" + _hero.NumOfSkills) as ComboBox;
                nextCboBox.IsEnabled = true;
            }
        }

        private void AddSpell(object sender, RoutedEventArgs e)
        {
            var chkBox = e.Source as CheckBox;
            _hero.AddSpell(chkBox.Name);
        }

        private void RemoveSpell(object sender, RoutedEventArgs e)
        {
            var chkBox = e.Source as CheckBox;
            _hero.RemoveSpell(chkBox.Name);
        }

        private void UpdateCreature(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var i = int.Parse(cboBox.Name.Substring("Creature".Length));
            var creature = cboBox.SelectedItem as string;

            _hero.UpdateCreature(i, creature);
            var txtBox = FindName("CreatureAmount" + i) as TextBox;
            if (!txtBox.IsEnabled)
            {
                txtBox.Text = _hero.CreatureAmounts[i].ToString();
                txtBox.IsEnabled = true;
            }
        }

        private void UpdateCreatureAmount(object sender, RoutedEventArgs e)
        {
            var txtBox = e.Source as TextBox;
            var i = int.Parse(txtBox.Name.Substring("CreatureAmount".Length));

            int amount;
            bool isNumber = int.TryParse(txtBox.Text, out amount);
            if (!isNumber || amount < 0 || amount > 9999) return;

            _hero.UpdateCreatureAmount(i, amount);
        }
    }
}
