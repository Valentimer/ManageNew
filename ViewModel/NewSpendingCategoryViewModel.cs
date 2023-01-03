using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ManagerFamily.Model;
using ManagerFamily.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagerFamily.ViewModel
{
    public class NewSpendingCategoryViewModel : ObservableObject
    {
        public string CategoryName { get; set; }
        public RelayCommand<Window> AddNewCategoryCommand { get; }

        public NewSpendingCategoryViewModel()
        {
            AddNewCategoryCommand = new RelayCommand<Window>(AddNewCategory);
        }

        private void AddNewCategory(Window window)
        {
            if (CategoryName == null || CategoryName.Replace(" ", "").Length == 0)
            {
                return;
            }
            window.DialogResult = true;
            window.Close();
        }
    }
}
