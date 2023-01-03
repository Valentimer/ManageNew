using ManagerFamily.Model;
using ManagerFamily.View;
using ManagerFamily.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerFamily.Service
{
    public class DataFlowManager
    {
        public bool TryCreateSpendingCategory(out SpendingCategory newModel)
        {
            NewSpendingCategoryViewModel viewModel = new NewSpendingCategoryViewModel();
            AddNewSpendingCategoryView newCategory = new AddNewSpendingCategoryView()
            {
                DataContext = viewModel
            };
            bool result = newCategory.ShowDialog() ?? false;
            if (result)
            {
                int resultId = DataWorker.CreateSpendingCategory(viewModel.CategoryName);
                newModel = DataWorker.GetCategoryById(resultId);
            }
            else
            {
                newModel = null;
            }
            return result;
        }

        public bool TryCreatPosition (out Position position)
        {
            NewPositionViewModel viewModel = new NewPositionViewModel();
            AddNewPositionView newPosition = new AddNewPositionView();
            newPosition.DataContext = viewModel;
            bool result = newPosition.ShowDialog() ?? false;
            if (result)
            {
                int resultId = DataWorker.CreatePosition(viewModel.PositionName, viewModel.PositionPrice, viewModel.PositionNumber, viewModel.PostionCategory);
                position = DataWorker.GetPositionById(resultId);
            }
            else { position = null; }
            return result;
        }

        public bool TryCreatUser (out User user)
        {
            NewUserViewModel viewModel = new NewUserViewModel();
            AddNewUserView newUserView = new AddNewUserView();
            newUserView.DataContext = viewModel;
            bool result = newUserView.ShowDialog() ?? false;
            if (result)
            {
                int resultId = DataWorker.CreateUser(viewModel.UserName, viewModel.UserSurName, viewModel.UserPhone, viewModel.UserPosition);
                user = DataWorker.GetUserById(resultId);
            }
            else { user = null; }
            return result;
        }
    }
}
