namespace MultiPropertyValidationExample.UI
{
    /// <summary>
    /// Interaction logic for GroupAdressCompositesListView.xaml
    /// 
    /// Naming: 
    /// 
    /// {0} Main class in view: GroupAdressComposite
    /// {1} Pluralization if multiple items are shown: "s"
    /// {2} "ListView": name of the view (to distinguish them if more than one view is able to show the class).
    /// </summary>
    public partial class GroupAdressCompositesListView
    {
        private readonly GroupAdressCompositesListViewModel _viewModel;

        public GroupAdressCompositesListView()
        {
            _viewModel = new GroupAdressCompositesListViewModel();
            DataContext = _viewModel;

            InitializeComponent();
        }
    }
}
