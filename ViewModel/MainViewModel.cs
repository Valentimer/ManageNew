using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ManagerFamily.Extensions;
using ManagerFamily.Model;
using ManagerFamily.Service;
using ManagerFamily.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManagerFamily.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private DataFlowManager DataManager;
        public MainViewModel()
        {
            List<SpendingCategory> categories = DataWorker.GetAllSpendingCategories();
            List<User> users = DataWorker.GetAllUsers();
            List<Position> positions = DataWorker.GetAllPosition();

            allCategory = new ObservableCollection<SpendingCategory>(categories);
            allPosition = new ObservableCollection<Position>(positions);
            allUser = new ObservableCollection<User>(users);

            AddClickCountCommand = new RelayCommand(AddClickCount);
            DataManager = new DataFlowManager();
        }

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
        //свойства для категорий
        //public string CategoryName { get; set; }

        ////свойства для позиций
        //public string PositionName { get; set; }
        //public decimal PositionPrice { get; set; }
        //public int PositionNumber { get; set; }
        //public SpendingCategory PostionCategory { get; set; }

        //свойства для юзеров
        //public string UserName { get; set; }
        //public string UserSurName { get; set; }
        //public string UserPhone { get; set; }
        //public Position UserPosition { get; set; }

        //свойства для выделенных элементов
        public TabItem SelectedTabTtem { get; set; }
        public User SelectedUser { get; set; }
        public Position SelectedPosition { get; set; }
        public SpendingCategory SelectedCategory { get; set; }

        #endregion

        #region COMMANDS TO ADD CLICK
        private int _clickCount;
        public int ClickCount
        {
            get => _clickCount;
            set => SetProperty(ref _clickCount, value);
        }
        public RelayCommand AddClickCountCommand { get; }
        private void AddClickCount()
        {
            ClickCount++;
        }

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
                    if (SelectedTabTtem.Name == "UsersTab" && SelectedUser != null)
                    {
                        resultStr = DataWorker.DeleteUser(SelectedUser);
                        AllUsers.Remove(SelectedUser);
                    }
                    //если позиция
                    if (SelectedTabTtem.Name == "PositionTab" && SelectedPosition != null)
                    {
                        resultStr = DataWorker.DeletePosition(SelectedPosition);
                        AllPositions.Remove(SelectedPosition);
                    }
                    //если категория
                    if (SelectedTabTtem.Name == "CategoryTab" && SelectedCategory != null)
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
                return openEditItemWnd ?? (openEditItemWnd = new RelayCommand<object>(obj =>
                {
                    string resultStr = "Ничего не выбрано";
                    //если юзер
                    if (SelectedTabTtem.Name == "UsersTab" && SelectedUser != null)
                    {
                        OpenEditAddUserWindowMethod(SelectedUser);
                    }
                    //если позиция
                    if (SelectedTabTtem.Name == "CategoryTab" && SelectedCategory != null)
                    {
                        OpenEditAddPositionWindowMethod(SelectedCategory);
                    }
                    //если категория
                    if (SelectedTabTtem.Name == "PositionTab" && SelectedPosition != null)
                    {
                        OpenEditAddWriteWindowMethod(SelectedPosition);
                    }
                }));
            }
        }
        #endregion

        #region EDIT COMMANDS

        //private RelayCommand<object> editUser;
        //public RelayCommand<object> EditUser
        //{
        //    get
        //    {
        //        return editUser ?? (editUser = new RelayCommand<object>(obj =>
        //        {
        //            Window window = obj as Window;
        //            string resultStr = "Не выбран пользователь";
        //            string noPosition = "Не выбрана новая категория";
        //            if (SelectedUser != null)
        //            {
        //                if (UserPosition != null)
        //                {
        //                    resultStr = DataWorker.EditUser(SelectedUser, UserName, UserSurName, UserPhone, UserPosition);
        //                    User updatedItem = DataWorker.GetUserById(SelectedUser.Id);
        //                    AllUsers.Replace(SelectedUser, updatedItem);

        //                    SetNullValuesToProperties();
        //                    ShowMessageToUser(resultStr);
        //                    window.Close();
        //                }
        //                else ShowMessageToUser(noPosition);
        //            }
        //        }));
        //    }
        //}

        //private RelayCommand<object> editPosition;
        //public RelayCommand<object> EditPosition
        //{
        //    get
        //    {
        //        return editPosition ?? (editPosition = new RelayCommand<object>(obj =>
        //        {
        //            Window window = obj as Window;
        //            string resultStr = "Не выбрана категория";
        //            string noCategory = "Не выбрана новая категория трат";
        //            if (SelectedPosition != null)
        //            {
        //                if (PostionCategory != null)
        //                {
        //                    resultStr = DataWorker.EditPosition(SelectedPosition, PositionName, PositionPrice, PositionNumber, PostionCategory);
        //                    SetNullValuesToProperties();
        //                    ShowMessageToUser(resultStr);
        //                    window.Close();
        //                }
        //                else ShowMessageToUser(noCategory);
        //            }
        //        }));
        //    }
        //}

        //private RelayCommand<object> editCategory;
        //public RelayCommand<object> EditCategory
        //{
        //    get
        //    {
        //        return editCategory ?? (editCategory = new RelayCommand<object>(obj =>
        //        {
        //            Window window = obj as Window;
        //            string resultStr = "Не выбрана категория трат";
        //            if (SelectedCategory != null)
        //            {
        //                resultStr = DataWorker.EditSpendingCategory(SelectedCategory, CategoryName);
        //                SetNullValuesToProperties();
        //                ShowMessageToUser(resultStr);
        //                window.Close();
        //            }
        //        }));
        //    }
        //}
        #endregion

        #region METHODS TO OPEN WINDOW

        //методы окна редактирования
        private void OpenEditAddPositionWindowMethod(SpendingCategory spendingCategory)
        {
            EditSpendingCategory editPosition = new EditSpendingCategory(spendingCategory);
            SetCenterPositionAndOpen(editPosition);
        }
        private void OpenEditAddWriteWindowMethod(Position position)
        {
            EditWrite editWrite = new EditWrite(position);
            SetCenterPositionAndOpen(editWrite);
        }
        private void OpenEditAddUserWindowMethod(User user)
        {
            EditUsers editUser = new EditUsers(user);
            SetCenterPositionAndOpen(editUser);
        }

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
            //для юзера
            //UserName = null;
            //UserSurName = null;
            //UserPhone = null;
            //UserPosition = null;
            //для позиций
            //PositionName = null;
            //PositionPrice = 0;
            //PositionNumber = 0;
            //PostionCategory = null;
            ////для категорий
            //CategoryName = null;
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
                AllCategories.Add(newCategory);
            }
        }

        public void AddNewPosition()
        {
            bool result = DataManager.TryCreatPosition(out Position position);
            if (result )
            {
                AllPositions.Add(position);
            }
        }

        public void AddNewUser()
        {
            bool result = DataManager.TryCreatUser(out User user);
            if (result)
            {
                AllUsers.Add(user);
            }
        }
        #endregion
    }
}