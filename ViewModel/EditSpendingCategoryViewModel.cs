using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ManagerFamily.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagerFamily.ViewModel
{
    public class EditSpendingCategoryViewModel : ObservableObject
    {
        public string CategoryName { get; set; }
        public RelayCommand<Window> EditCategoryCommand { get; }

        public EditSpendingCategoryViewModel(SpendingCategory selectedItem)
        {
            EditCategoryCommand = new RelayCommand<Window>(EditNewCategory);
            CategoryName = selectedItem.Name;
        }

        private void EditNewCategory(Window window)
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
