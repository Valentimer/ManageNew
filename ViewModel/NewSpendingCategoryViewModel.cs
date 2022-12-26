using ManagerFamily.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerFamily.ViewModel
{
    public class NewSpendingCategoryViewModel : ObservableObject
    {
        public int ResultId { get; set; } = -1;

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
            ResultId = DataWorker.CreateSpendingCategory(CategoryName);
            window.Close();
        }
    }
}
