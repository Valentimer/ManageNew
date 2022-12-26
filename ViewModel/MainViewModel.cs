using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ManagerFamily.Extensions;
using ManagerFamily.Model;
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
        public MainViewModel()
        {
            List<SpendingCategory> categories = DataWorker.GetAllSpendingCategories();
            List<User> users = DataWorker.GetAllUsers();
            List<Position> positions = DataWorker.GetAllPosition();

            allCategory = new ObservableCollection<SpendingCategory>(categories);
            allPosition = new ObservableCollection<Position>(positions);
            allUser = new ObservableCollection<User>(users);

            //AddNewCategoryCommand = new RelayCommand<string>(AddNewCategory);
            AddClickCountCommand = new RelayCommand(AddClickCount);
            //AddNewPositionCommand = new RelayCommand<string>(AddNewPosition);
        }

        //все категории трат
        private ObservableCollection<SpendingCategory> allCategory; // = DataWorker.GetAllSpendingCategories();
        public ObservableCollection<SpendingCategory> AllCategories
        {
            get => allCategory;
            set => SetProperty(ref allCategory, value);
        }

        //все конткретные траты
        private ObservableCollection<Position> allPosition; //= DataWorker.GetAllPosition();
        public ObservableCollection<Position> AllPositions
        {
            get => allPosition;
            set => SetProperty(ref allPosition, value);
        }

        //все траты
        private ObservableCollection<User> allUser; //= DataWorker.GetAllUsers();
        public ObservableCollection<User> AllUsers
        {
            get => allUser;
            set => SetProperty(ref allUser, value);
        }

        #region СВОЙСТВА ДЛЯ ПОЛЕЙ
        //свойства для категорий
        public string CategoryName { get; set; }

        //свойства для позиций
        public string PositionName { get; set; }
        public decimal PositionPrice { get; set; }
        public int PositionNumber { get; set; }
        public SpendingCategory PostionCategory { get; set; }

        //свойства для юзеров
        public string UserName { get; set; }
        public string UserSurName { get; set; }
        public string UserPhone { get; set; }
        public Position UserPosition { get; set; }

        //свойства для выделенных элементов
        public TabItem SelectedTabTtem { get; set; }
        public User SelectedUser { get; set; }
        public Position SelectedPosition { get; set; }
        public SpendingCategory SelectedCategory { get; set; }

        #endregion

        #region COMMANDS TO ADD
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

        //команды для добавляения
        //public RelayCommand<string> AddNewCategoryCommand { get; }
        //private void AddNewCategory(string categoryName)
        //{
        //    if (categoryName == null || categoryName.Replace(" ", "").Length == 0)
        //    {
        //        //SetRedBlockControl(wnd, "NameBlock");
        //        return;
        //    }
        //    int id = DataWorker.CreateSpendingCategory(categoryName);
        //    SpendingCategory newCategory = DataWorker.GetCategoryById(id);
        //    AllCategories.Add(newCategory);
        //    ClickCount++;
        //}

        //public RelayCommand<string> AddNewPositionCommand { get; }
        //private void AddNewPosition(string positionName)
        //{
        //    if (positionName == null || positionName.Replace(" ", "").Length == 0)
        //    {
        //        //SetRedBlockControl(wnd, "NameBlock");
        //        return;
        //    }
        //    int id = DataWorker.CreatePosition(positionName);
        //    Position newPosition = DataWorker.GetPositionById(id);
        //    AllPositions.Add(newPosition);
        //    ClickCount++;
        //}

        private RelayCommand<object> addNewUser;
        public RelayCommand<object> AddNewUser
        {
            get
            {
                return addNewUser ?? (addNewUser = new RelayCommand<object>(obj =>
                {
                    Window wnd = obj as Window;
                    string resultStr = "";
                    int id;
                    if (UserName == null || UserName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "NameBlock");
                    }
                    if (UserSurName == null || UserSurName.Replace(" ", "").Length == 0)
                    {
                        SetRedBlockControl(wnd, "SurNameBlock");
                    }
                    if (UserPosition == null)
                    {
                        MessageBox.Show("Укажите пизицию трат!");
                    }
                    else
                    {
                        //resultStr = DataWorker.CreateUser(UserName, UserSurName, UserPhone, UserPosition);
                        id = DataWorker.CreateUser(UserName, UserSurName, UserPhone, UserPosition);

                        User newUser = DataWorker.GetUserById(id);
                        AllUsers.Add(newUser);

                        var t = allUser;
                        AllUsers = null;
                        AllUsers = t;
                        //ShowMessageToUser(resultStr);
                        //UpdateAllDataView();
                        SetNullValuesToProperties();
                        wnd.Close();
                    }
                }));
            }
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
        private RelayCommand<object> openAddNewPositionWnd;
        public RelayCommand<object> OpenAddNewPositionWnd
        {
            get
            {
                return openAddNewPositionWnd ?? (openAddNewPositionWnd = new RelayCommand<object>(obj =>
            {
                OpenAddPositionWindowMethod();
                ClickCount++;
            }
            ));
            }
        }
        private RelayCommand<object> openAddNewWriteWnd;
        public RelayCommand<object> OpenAddNewWriteWnd
        {
            get
            {
                return openAddNewWriteWnd ?? (openAddNewWriteWnd = new RelayCommand<object>(obj =>
                {
                    OpenAddWriteWindowMethod();
                }
            ));
            }
        }
        private RelayCommand<object> openAddNewUser;
        public RelayCommand<object> OpenAddNewUserWnd
        {
            get
            {
                return openAddNewUser ?? (openAddNewUser = new RelayCommand<object>(obj =>
                {
                    OpenAddUserWindowMethod();
                }
            ));
            }
        }
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

        private RelayCommand<object> editUser;
        public RelayCommand<object> EditUser
        {
            get
            {
                return editUser ?? (editUser = new RelayCommand<object>(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбран пользователь";
                    string noPosition = "Не выбрана новая категория";
                    if (SelectedUser != null)
                    {
                        if (UserPosition != null)
                        {
                            resultStr = DataWorker.EditUser(SelectedUser, UserName, UserSurName, UserPhone, UserPosition);
                            User updatedItem = DataWorker.GetUserById(SelectedUser.Id);
                            AllUsers.Replace(SelectedUser, updatedItem);

                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noPosition);
                    }
                }));
            }
        }

        private RelayCommand<object> editPosition;
        public RelayCommand<object> EditPosition
        {
            get
            {
                return editPosition ?? (editPosition = new RelayCommand<object>(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана категория";
                    string noCategory = "Не выбрана новая категория трат";
                    if (SelectedPosition != null)
                    {
                        if (PostionCategory != null)
                        {
                            resultStr = DataWorker.EditPosition(SelectedPosition, PositionName, PositionPrice, PositionNumber, PostionCategory);
                            SetNullValuesToProperties();
                            ShowMessageToUser(resultStr);
                            window.Close();
                        }
                        else ShowMessageToUser(noCategory);
                    }
                }));
            }
        }

        private RelayCommand<object> editCategory;
        public RelayCommand<object> EditCategory
        {
            get
            {
                return editCategory ?? (editCategory = new RelayCommand<object>(obj =>
                {
                    Window window = obj as Window;
                    string resultStr = "Не выбрана категория трат";
                    if (SelectedCategory != null)
                    {
                        resultStr = DataWorker.EditSpendingCategory(SelectedCategory, CategoryName);
                        SetNullValuesToProperties();
                        ShowMessageToUser(resultStr);
                        window.Close();
                    }
                }));
            }
        }
        #endregion

        #region METHODS TO OPEN WINDOW
        //методы открытия окон
        private void OpenAddPositionWindowMethod()
        {
            var viewModel = new NewSpendingCategoryViewModel();
            var newPositionView = new AddNewSpendingCategory(viewModel);
            newPositionView.ShowDialog();

            if (viewModel.ResultId != -1)
            {
                SpendingCategory newCategory = DataWorker.GetCategoryById(viewModel.ResultId);
                AllCategories.Add(newCategory);
            }
        }
        private void OpenAddWriteWindowMethod()
        {
            var viewModel = new NewPositionViewModel();
            var newPositionView = new AddNewWrite(viewModel);
            newPositionView.ShowDialog();

            if (viewModel.ResultId != -1)
            {
                Position newPosition = DataWorker.GetPositionById(viewModel.ResultId);
                AllPositions.Add(newPosition);
            }
        }
        private void OpenAddUserWindowMethod()
        {
            AddNewUser newUser = new AddNewUser();
            SetCenterPositionAndOpen(newUser);
        }

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


        private void SetRedBlockControl(Window wnd, string blockName)
        {
            Control block = wnd.FindName(blockName) as Control;
            block.BorderBrush = Brushes.Red;
        }

        #region UPDATE VIEWS

        private void SetNullValuesToProperties()
        {
            //для юзера
            UserName = null;
            UserSurName = null;
            UserPhone = null;
            UserPosition = null;
            //для позиций
            PositionName = null;
            PositionPrice = 0;
            PositionNumber = 0;
            PostionCategory = null;
            //для категорий
            CategoryName = null;
        }

        #endregion

        private void ShowMessageToUser(string message)
        {
            MessageView messageView = new MessageView(message);
            SetCenterPositionAndOpen(messageView);
        }
    }
}
//addNewCategory = new RelayCommand(() =>
//         {
//             //Window wnd = obj as Window;
//             int id;
//             string resultStr = "";
//             if (CategoryName == null || CategoryName.Replace(" ", "").Length == 0)
//             {
//                 //SetRedBlockControl(wnd, "NameBlock");
//             }
//             else
//             {
//                 id = DataWorker.CreateSpendingCategory(CategoryName);


//                 SpendingCategory newCategory = DataWorker.GetCategoryById(id);
//                 AllCategories.Add(newCategory);
//                 var t = allCategory;
//                 AllCategories = null;
//                 //AllCategories= t;
//                 //ShowMessageToUser(resultSrt);
//                 //UpdateAllDataView();
//                 //SetNullValuesToProperties();
//                 //wnd.Close();
//             }
//         }));
//     }
// }