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
using System.Diagnostics;

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
                    var cboBox = FindName("Skill" + i) as ComboBox;
                    var txtBox = FindName("SkillLevel" + i) as TextBox;
                    if (i < _hero.NumOfSkills)
                    {
                        cboBox.SelectedItem = _hero.Skills[i];
                        txtBox.Text = _hero.SkillLevels[i].ToString();
                    }
                    else if (i > _hero.NumOfSkills)
                    {
                        cboBox.IsEnabled = false;
                        txtBox.IsEnabled = false;
                    }
                    else
                    {
                        txtBox.IsEnabled = false;
                    }
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

                var artifact = _hero.Weapon;
                var artifactCboBox = FindName("CurrentWeapon") as ComboBox;
                artifactCboBox.SelectedItem = artifact;

                artifact = _hero.Helm;
                artifactCboBox = FindName("CurrentHelm") as ComboBox;
                artifactCboBox.SelectedItem = artifact;

                artifact = _hero.Shield;
                artifactCboBox = FindName("CurrentShield") as ComboBox;
                artifactCboBox.SelectedItem = artifact;

                artifact = _hero.LeftRing;
                artifactCboBox = FindName("CurrentLeftRing") as ComboBox;
                artifactCboBox.SelectedItem = artifact;

                artifact = _hero.Neck;
                artifactCboBox = FindName("CurrentNeck") as ComboBox;
                artifactCboBox.SelectedItem = artifact;

                artifact = _hero.RightRing;
                artifactCboBox = FindName("CurrentRightRing") as ComboBox;
                artifactCboBox.SelectedItem = artifact;

                artifact = _hero.Boots;
                artifactCboBox = FindName("CurrentBoots") as ComboBox;
                artifactCboBox.SelectedItem = artifact;

                artifact = _hero.Armor;
                artifactCboBox = FindName("CurrentArmor") as ComboBox;
                artifactCboBox.SelectedItem = artifact;

                artifact = _hero.Cloak;
                artifactCboBox = FindName("CurrentCloak") as ComboBox;
                artifactCboBox.SelectedItem = artifact;

                artifact = _hero.Slot1;
                artifactCboBox = FindName("CurrentSlot1") as ComboBox;
                artifactCboBox.SelectedItem = artifact;

                artifact = _hero.Slot2;
                artifactCboBox = FindName("CurrentSlot2") as ComboBox;
                artifactCboBox.SelectedItem = artifact;

                artifact = _hero.Slot3;
                artifactCboBox = FindName("CurrentSlot3") as ComboBox;
                artifactCboBox.SelectedItem = artifact;

                artifact = _hero.Slot4;
                artifactCboBox = FindName("CurrentSlot4") as ComboBox;
                artifactCboBox.SelectedItem = artifact;

                artifact = _hero.Ballista;
                var artifactChkBox = FindName("Ballista") as CheckBox;
                artifactChkBox.IsChecked = (artifact.Length > 7) ? true : false;

                artifact = _hero.FirstAidTent;
                artifactChkBox = FindName("FirstAidTent") as CheckBox;
                artifactChkBox.IsChecked = (artifact.Length > 7) ? true : false;

                artifact = _hero.AmmoCart;
                artifactChkBox = FindName("AmmoCart") as CheckBox;
                artifactChkBox.IsChecked = (artifact.Length > 7) ? true : false;
            }

            // Checkboxes for these
            // Spell Book - In the Spell Section 
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
            var slot = int.Parse(cboBox.Name.Substring("Skill".Length));
            var skill = cboBox.SelectedItem as string;

            var oldNumOfSkills = _hero.NumOfSkills;
            _hero.UpdateSkill(slot, skill);

            if (_hero.NumOfSkills > oldNumOfSkills)
            {
                var txtBox = FindName("SkillLevel" + slot) as TextBox;
                txtBox.Text = _hero.SkillLevels[slot].ToString();
                txtBox.IsEnabled = true;

                if (_hero.NumOfSkills < 8)
                {
                    var nextCboBox = FindName("Skill" + _hero.NumOfSkills) as ComboBox;
                    nextCboBox.IsEnabled = true;
                }
            }
        }

        private void UpdateSkillLevel(object sender, RoutedEventArgs e)
        {
            var txtBox = e.Source as TextBox;
            var slot = int.Parse(txtBox.Name.Substring("SkillLevel".Length));

            byte level;
            bool isNumber = byte.TryParse(txtBox.Text, out level);
            if (!isNumber || level < 0 || level > 3) return;

            _hero.UpdateSkillLevel(slot, level);
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

        private void UpdateWeapon(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var weapon = cboBox.SelectedItem as string;

            _hero.UpdateWeapon(weapon);
        }

        private void UpdateRightRing(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var ring = cboBox.SelectedItem as string;

            _hero.UpdateRightRing(ring);
        }

        private void UpdateLeftRing(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var ring = cboBox.SelectedItem as string;

            _hero.UpdateLeftRing(ring);
        }

        private void UpdateHelm(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var helm = cboBox.SelectedItem as string;

            _hero.UpdateHelm(helm);
        }

        private void UpdateSlot1(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var item = cboBox.SelectedItem as string;

            _hero.UpdateSlot1(item);
        }

        private void UpdateSlot2(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var item = cboBox.SelectedItem as string;

            _hero.UpdateSlot2(item);
        }

        private void UpdateSlot3(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var item = cboBox.SelectedItem as string;

            _hero.UpdateSlot3(item);
        }

        private void UpdateSlot4(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var item = cboBox.SelectedItem as string;

            _hero.UpdateSlot4(item);
        }

        private void UpdateNeck(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var neck = cboBox.SelectedItem as string;

            _hero.UpdateNeck(neck);
        }

        private void UpdateCloak(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var cloak = cboBox.SelectedItem as string;

            _hero.UpdateCloak(cloak);
        }

        private void UpdateArmor(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var armor = cboBox.SelectedItem as string;

            _hero.UpdateArmor(armor);
        }

        private void UpdateShield(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var shield = cboBox.SelectedItem as string;

            _hero.UpdateShield(shield);
        }

        private void UpdateBoots(object sender, RoutedEventArgs e)
        {
            var cboBox = e.Source as ComboBox;
            var boots = cboBox.SelectedItem as string;

            _hero.UpdateBoots(boots);
        }

        private void AddBallista(object sender, RoutedEventArgs e)
        {
            var chkBox = e.Source as CheckBox;
            _hero.AddBallista("Ballista");
        }

        private void RemoveBallista(object sender, RoutedEventArgs e)
        {
            var chkBox = e.Source as CheckBox;
            _hero.RemoveBallista("Ballista");
        }

        private void AddFirstAidTent(object sender, RoutedEventArgs e)
        {
            var chkBox = e.Source as CheckBox;
            _hero.AddFirstAidTent("First Aid Tent");
        }

        private void RemoveFirstAidTent(object sender, RoutedEventArgs e)
        {
            var chkBox = e.Source as CheckBox;
            _hero.RemoveFirstAidTent("First Aid Tent");
        }

        private void AddAmmoCart(object sender, RoutedEventArgs e)
        {
            var chkBox = e.Source as CheckBox;
            _hero.AddAmmoCart("Ammo Cart");
        }

        private void RemoveAmmoCart(object sender, RoutedEventArgs e)
        {
            var chkBox = e.Source as CheckBox;
            _hero.RemoveAmmoCart("Ammo Cart");
        }
    }
}
