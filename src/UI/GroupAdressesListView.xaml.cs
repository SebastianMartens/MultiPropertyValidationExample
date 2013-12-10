namespace MultiPropertyValidationExample.UI
{
    /// <summary>
    /// Interaction logic for GroupAdressListView.xaml
    /// </summary>
    public partial class GroupAdressListView
    {
        private readonly GroupAdressesViewModel _viewModel;

        public GroupAdressListView()
        {
            _viewModel = new GroupAdressesViewModel();
            DataContext = _viewModel;

            InitializeComponent();
        }
    }
}
