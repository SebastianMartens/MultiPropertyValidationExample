using System.Collections.ObjectModel;
using System.Windows.Data;
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
        }                  
    }
    
}
