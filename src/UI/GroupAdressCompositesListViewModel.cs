using System.Collections.ObjectModel;
using System.Windows.Data;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.ViewModel;
using MultiPropertyValidationExample.Model;

namespace MultiPropertyValidationExample.UI
{
    /// <summary>
    /// Naming: ViewModel for the "GroupAdressCompositesListView" !!
    /// </summary>
    public class GroupAdressCompositesListViewModel: NotificationObject
    {
        
        public CollectionViewSource GroupAdressCollectionView { get; private set; }
        public DelegateCommand SetFormatSlashedCommand { get; set; }
        public DelegateCommand SetFormatSpacedCommand { get; set; }
        private const string SettingNameCurrentGaStyle = "CurrentGaStyle";      

        /// <summary>
        /// 
        /// </summary>
        public GroupAdressCompositesListViewModel()
        {            
            // init members:                        
            // we have multiple "rows" in the list. Here we init 4 rows with dummy data:
            GroupAdressCollectionView = new CollectionViewSource();            
            var gaList = new ObservableCollection<GroupAdressComposite>();
            for (var i = 0; i < 4; i++)
            {
                gaList.Add(GroupAdressComposite.GetDummyItem(i));
            }            
            GroupAdressCollectionView.Source = gaList;


            // wire-up commands            
            SetFormatSlashedCommand = new DelegateCommand(SetFormatSlashed, SetFormatSlashedCanExecute);
            SetFormatSpacedCommand = new DelegateCommand(SetFormatSpaced, SetFormatSpacedCanExecute);
            CurrentGaStyle = GaStylesEnum.Slashed;
        }

        /// <summary>
        /// here I wrap the interesting setting. You could also expose the SettingService..
        /// The change events then differ slightly...
        /// </summary>
        public GaStylesEnum CurrentGaStyle
        {
            get
            {
                return (GaStylesEnum)SettingsService.GetSetting(SettingNameCurrentGaStyle);
            }
            set
            {
                SettingsService.SetSetting(SettingNameCurrentGaStyle, value);
                RaisePropertyChanged(() => CurrentGaStyle);
                SetFormatSlashedCommand.RaiseCanExecuteChanged();
                SetFormatSpacedCommand.RaiseCanExecuteChanged();

                GroupAdressCollectionView.View.Refresh(); // update strings in UI; as an alternative you could subscribe to a global "onSettingChanged" event...
            }
        }

        #region commanding

        private bool SetFormatSlashedCanExecute()
        {
            return CurrentGaStyle != GaStylesEnum.Slashed;
        }

        private void SetFormatSlashed()
        {
            CurrentGaStyle = GaStylesEnum.Slashed;
        }

        private bool SetFormatSpacedCanExecute()
        {
            return CurrentGaStyle != GaStylesEnum.Spaced;
        }

        private void SetFormatSpaced()
        {
            CurrentGaStyle = GaStylesEnum.Spaced;
        }

        #endregion

    }
    
}
