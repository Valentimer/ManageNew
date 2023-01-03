using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ManagerFamily.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagerFamily.ViewModel
{
    public class NewPositionViewModel: ObservableObject
    {
        public List<SpendingCategory> AllCategories { get; set; }

        public string PositionName { get; set; }
        public decimal PositionPrice { get; set; }
        public int PositionNumber { get; set; }
        public SpendingCategory PostionCategory { get; set; }

        public RelayCommand<Window> AddNewPositionCommand { get; }

        public NewPositionViewModel()
        {
            AddNewPositionCommand = new RelayCommand<Window>(AddNewPosition);
            AllCategories = DataWorker.GetAllSpendingCategories();
        }

        private void AddNewPosition(Window window)
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
