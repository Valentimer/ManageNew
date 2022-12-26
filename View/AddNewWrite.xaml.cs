using CommunityToolkit.Mvvm.ComponentModel;
using ManagerFamily.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ManagerFamily.View
{
    /// <summary>
    /// Interaction logic for AddNewWrite.xaml
    /// </summary>
    public partial class AddNewWrite : Window
    {
        public AddNewWrite(ObservableObject context)
        {
            InitializeComponent();
            DataContext = context;
        }
    }
}
