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

        public bool TryCreatePosition (out Position position)
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

        public bool TryCreateUser (out User user)
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

        public bool TryEditSpendingCategory(SpendingCategory selectedCategory, out SpendingCategory newModel)
        {
            EditSpendingCategoryViewModel viewModel = new EditSpendingCategoryViewModel(selectedCategory);
            EditSpendingCategoryView newCategory = new EditSpendingCategoryView()
            {
                DataContext = viewModel
            };
            bool result = newCategory.ShowDialog() ?? false;

            if (result)
            {
                int resultId = DataWorker.EditSpendingCategory(selectedCategory, viewModel.CategoryName);
                newModel = DataWorker.GetCategoryById(resultId);
            }
            else
            {
                newModel = null;
            }

            return result;
        }

        public bool TryEditPosition(Position selectedPosition, out Position newModel)
        {
            var editPositionViewModel = new EditPositionViewModel(selectedPosition);
            var newPosition = new EditPositionView();
            newPosition.DataContext = editPositionViewModel;
            bool result = newPosition.ShowDialog() ?? false;

            if (result)
            {
                int resultId = DataWorker.EditPosition(selectedPosition, editPositionViewModel.PositionName, editPositionViewModel.PositionPrice, editPositionViewModel.PositionNumber, editPositionViewModel.PostionCategory);
                newModel = DataWorker.GetPositionById(resultId);
            }
            else
            {
                newModel = null;
            }

            return result;
        }

        public bool TryEditUser (User selectedUser, out User newModel)
        {
            var editUserViewModel = new EditUserViewModel(selectedUser);
            var newUser = new EditUsersView();
            newUser.DataContext = editUserViewModel;
            bool result = newUser.ShowDialog() ?? false;

            if (result)
            {
                int resultId = DataWorker.EditUser(selectedUser, editUserViewModel.UserName, editUserViewModel.UserSurName, editUserViewModel.UserPhone, editUserViewModel.UserPosition);
                newModel = DataWorker.GetUserById(resultId);
            }
            else
            {
                newModel = null;
            }

            return result;
        }
    }
}
