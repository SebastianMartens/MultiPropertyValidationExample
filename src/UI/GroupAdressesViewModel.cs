using System.Collections.ObjectModel;
using System.Windows.Data;
using Microsoft.Practices.Prism.ViewModel;
using MultiPropertyValidationExample.Model;

namespace MultiPropertyValidationExample.UI
{
    public class GroupAdressesViewModel: NotificationObject
    {
        // public "interface" to the view:
        //public ICommand LoadDataIntoViewModelCommand { get; set; }                
        public CollectionViewSource GroupAdressCollectionView { get; private set; }
      
        /// <summary>
        /// 
        /// </summary>
        public GroupAdressesViewModel()
        {
            // wire up commands:
            //LoadDataIntoViewModelCommand = new DelegateCommand(LoadDataIntoViewModel);

            // init members:                        
            // we have multiple "rows", each with two group adresses:
            GroupAdressCollectionView = new CollectionViewSource();
            
            var gaList = new ObservableCollection<GroupAdressInfoRow>();
            gaList.Add(new GroupAdressInfoRow {
                Name = "Adresse 1",
                ReadAdress = GroupAdress.Parse("9"),
                WriteAdress = GroupAdress.Parse("42")
                });

            gaList.Add(new GroupAdressInfoRow
            {
                Name = "Adresse 2",
                ReadAdress = GroupAdress.Parse("25"),
                WriteAdress = GroupAdress.Parse("2")
            });

            GroupAdressCollectionView.Source = gaList;
        }            

        /// <summary>
        /// In this snippet we use two seperate tasks to load data async. in the background.
        /// Data is NOT "stored" in any centralized domain model, instead it's only retrieved to be shown in the UI.
        ///
        /// => data synchronization with the main thread is done in the view model instance!
        /// </summary>
        private void LoadDataIntoViewModel()
        {                        
          
            // The View and ViewModel run in the UI thread. But no one ever accesses the viewmodel or view from elsewhere so calls to the dispatcher should not be needed here...
            // If you use shared objects (e.g. ObservableCollections that comes from the model) then we would have to marshall?? 
            // => make example of ObservableConcurrentColletion usage!
            //Dispatcher.Invoke(() => ...);
        }
      
    }
    
}
