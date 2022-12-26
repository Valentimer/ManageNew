using CommunityToolkit.Mvvm.ComponentModel;
using ManagerFamily.ViewModel;
using System.Windows;


namespace ManagerFamily.View
{
    /// <summary>
    /// Interaction logic for AddNewPosition.xaml
    /// </summary>
    public partial class AddNewSpendingCategory : Window
    {
        public AddNewSpendingCategory(ObservableObject context)
        {
            InitializeComponent();
            DataContext = context;
        }
    }
}
