using Monopoly.Model;
using Monopoly.Model.Tiles;
using System.Windows;
using System.Windows.Input;

namespace Monopoly.ViewModel {
    public class ManageStreetsViewModel {
        public Game Game { get; set; }
        public TileProperty SelectedTile { get; set; }

        public ManageStreetsViewModel(Game game) {
            Game = game;
        }

        #region Commands

        private ICommand _upgradeCommand;
        public ICommand UpgradeCommand {
            get {
                return _upgradeCommand ??
                       (_upgradeCommand =
                        new RelayCommand(p => DoUpgradeCommand()));
            }
        }

        private ICommand _downgradeCommand;
        public ICommand DowngradeCommand {
            get {
                return _downgradeCommand ??
                       (_downgradeCommand =
                        new RelayCommand(p => DoDowngradeCommand()));
            }
        }

        #endregion

        #region Command Methods

        private void DoUpgradeCommand() {
            if (SelectedTile != null && (SelectedTile != null && SelectedTile.City.OwnsAllProperties(Game.CurrentPlayer) && SelectedTile.CanBeUpgraded() || SelectedTile.OnMortage)) {
                SelectedTile.Upgrade();
            } else if (SelectedTile == null) {
                MessageBox.Show("Select an upgradable item in the list.");
            } else if (SelectedTile != null && !SelectedTile.City.OwnsAllProperties(Game.CurrentPlayer)) {
                MessageBox.Show("You must first have all the streets before you can start building houses");
            } else if (!SelectedTile.CanBeUpgraded()) {
                MessageBox.Show("You must first have all the streets at the same number of houses before you can upgrade this one further!");
            }
        }

        private void DoDowngradeCommand() {
            if (SelectedTile != null && SelectedTile.CanBeDowngraded()) {
                SelectedTile.Downgrade();
            } else if (SelectedTile == null) {
                MessageBox.Show("Select a street you want to downgrade please");
            } else if (SelectedTile.OnMortage) {
                MessageBox.Show("This building cannot be downgraded any further!");
            } else if (!SelectedTile.CanBeDowngraded()) {
                MessageBox.Show("You must first have all the streets at the same number of houses before you can downgrade this one further!");
            }

        }

        #endregion
    }
}
