using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ManagerFamily.Extensions;
using ManagerFamily.Model;
using ManagerFamily.Service;
using ManagerFamily.View;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ManagerFamily.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private DataFlowManager DataManager;

        private const int USER_TAB_INDEX = 0;
        private const int POSITION_TAB_INDEX = 1;
        private const int CATEGORY_TAB_INDEX = 2;

        public MainViewModel()
        {
            List<SpendingCategory> categories = DataWorker.GetAllSpendingCategories();
            List<User> users = DataWorker.GetAllUsers();
            List<Position> positions = DataWorker.GetAllPosition();

            allCategory = new ObservableCollection<SpendingCategory>(categories);
            allPosition = new ObservableCollection<Position>(positions);
            allUser = new ObservableCollection<User>(users);
            DataManager = new DataFlowManager();
        }

        public List<string> Data { get; }

        //все категории трат
        private ObservableCollection<SpendingCategory> allCategory;
        public ObservableCollection<SpendingCategory> AllCategories
        {
            get => allCategory;
            set => SetProperty(ref allCategory, value);
        }

        //все конткретные траты
        private ObservableCollection<Position> allPosition;
        public ObservableCollection<Position> AllPositions
        {
            get => allPosition;
            set => SetProperty(ref allPosition, value);
        }

        //все траты
        private ObservableCollection<User> allUser;
        public ObservableCollection<User> AllUsers
        {
            get => allUser;
            set => SetProperty(ref allUser, value);
        }

        #region СВОЙСТВА ДЛЯ ПОЛЕЙ

        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set => SetProperty(ref _selectedTabIndex, value);
        }
        public User SelectedUser { get; set; }
        public Position SelectedPosition { get; set; }
        public SpendingCategory SelectedCategory { get; set; }

        #endregion

        #region УДАЛЕНИЕ

        private RelayCommand<object> deleteItem;
        public RelayCommand<object> DeleteItem
        {
            get
            {
                return deleteItem ?? (deleteItem = new RelayCommand<object>(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если юзер
                    if (SelectedTabIndex == USER_TAB_INDEX && SelectedUser != null)
                    {
                        resultStr = DataWorker.DeleteUser(SelectedUser);
                        AllUsers.Remove(SelectedUser);
                    }
                    //если позиция
                    if (SelectedTabIndex == POSITION_TAB_INDEX && SelectedPosition != null)
                    {
                        resultStr = DataWorker.DeletePosition(SelectedPosition);
                        AllPositions.Remove(SelectedPosition);
                    }
                    //если категория
                    if (SelectedTabIndex == CATEGORY_TAB_INDEX && SelectedCategory != null)
                    {
                        resultStr = DataWorker.DeleteSpendingCategory(SelectedCategory);
                        AllCategories.Remove(SelectedCategory);
                    }
                    //обновление
                    SetNullValuesToProperties();
                    ShowMessageToUser(resultStr);
                }));
            }
        }

        #endregion

        #region COMMANDS TO OPEN WINDOW
        //команды для открытия
        private RelayCommand openAddNewCategoryWnd;
        public RelayCommand OpenAddNewCategoryWnd => openAddNewCategoryWnd ??= new RelayCommand(AddNewCategory);


        private RelayCommand openAddNewPositionNewWnd;
        public RelayCommand OpenAddNewPositionNewWnd => openAddNewPositionNewWnd ??= new RelayCommand(AddNewPosition);


        private RelayCommand openAddNewUser;
        public RelayCommand OpenAddNewUserWnd => openAddNewUser ??= new RelayCommand(AddNewUser);


        private RelayCommand<object> openEditItemWnd;
        public RelayCommand<object> OpenEditItemWnd
        {
            get
            {
                return openEditItemWnd ??= new RelayCommand<object>(obj =>
                {
                    if (SelectedTabIndex == USER_TAB_INDEX && SelectedUser != null)
                    {
                        EditUser();
                    }

                    if (SelectedTabIndex == POSITION_TAB_INDEX && SelectedPosition != null)
                    {
                        EditPosition();
                    }

                    if (SelectedTabIndex == CATEGORY_TAB_INDEX && SelectedCategory != null)
                    {
                        EditCategory();
                    }
                });
            }
        }

        #endregion

        #region METHODS TO OPEN WINDOW

        private void SetCenterPositionAndOpen(Window window)
        {
            window.Owner = Application.Current.MainWindow;
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.DataContext = this;
            window.ShowDialog();
        }

        #endregion

        #region UPDATE VIEWS

        private void SetNullValuesToProperties()
        {
        }

        #endregion

        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }

        #region METHOD TO ADD
        public void AddNewCategory()
        {
            bool result = DataManager.TryCreateSpendingCategory(out SpendingCategory newCategory);
            if (result)
            {
                SelectedTabIndex = CATEGORY_TAB_INDEX;
                AllCategories.Add(newCategory);
            }
        }

        public void AddNewPosition()
        {
            bool result = DataManager.TryCreatePosition(out Position position);
            if (result)
            {
                SelectedTabIndex = POSITION_TAB_INDEX;
                AllPositions.Add(position);
            }
        }

        public void AddNewUser()
        {
            bool result = DataManager.TryCreateUser(out User user);
            if (result)
            {
                SelectedTabIndex = USER_TAB_INDEX;
                AllUsers.Add(user);
            }
        }

        public void EditCategory()
        {
            bool result = DataManager.TryEditSpendingCategory(SelectedCategory, out SpendingCategory category);

            if (result)
            {
                AllCategories.Replace(SelectedCategory, category);
            }
        }

        public void EditPosition()
        {
            bool result = DataManager.TryEditPosition(SelectedPosition, out Position position);

            if (result)
            {
                AllPositions.Replace(SelectedPosition, position);
            }
        }

        public void EditUser()
        {
            bool result = DataManager.TryEditUser(SelectedUser, out User user);

            if (result)
            {
                AllUsers.Replace(SelectedUser, user);
            }
        }
        #endregion
    }
}