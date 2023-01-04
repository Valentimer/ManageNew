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
    public class EditPositionViewModel: ObservableObject
    {
        public List<SpendingCategory> AllCategories { get; set; }

        public string PositionName { get; set; }
        public decimal PositionPrice { get; set; }
        public int PositionNumber { get; set; }
        public SpendingCategory PostionCategory { get; set; }

        public RelayCommand<Window> EditPositionCommand { get; }

        public EditPositionViewModel(Position selectedPosition)
        {
            EditPositionCommand = new RelayCommand<Window>(EditNewPosition);
            AllCategories = DataWorker.GetAllSpendingCategories();
            PositionName = selectedPosition.Name;
            PositionPrice = selectedPosition.Price;
            PositionNumber = selectedPosition.MaxNumber;
            PostionCategory = selectedPosition.PositionSpendingCategory;
        }
        private void EditNewPosition(Window window)
        {
            if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
            {
                return;
            }
            window.DialogResult = true;
            window.Close();
        }
    }
}
