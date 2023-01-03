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
    public class NewUserViewModel: ObservableObject
    {
        public List<Position> AllPositions { get; set; }

        public string UserName { get; set; }
        public string UserSurName { get; set; }
        public string UserPhone { get; set; }
        public Position UserPosition { get; set; }

        public RelayCommand<Window> AddNewUserCommand { get; }

        public NewUserViewModel()
        {
            AddNewUserCommand = new RelayCommand<Window>(AddNewUser);
            AllPositions = DataWorker.GetAllPosition();
        }

        private void AddNewUser(Window window)
        {
            if (UserName == null || UserName.Replace(" ", "").Length == 0)
            {
                return;
            }
            window.DialogResult = true;
            window.Close();
        }
    }
}
