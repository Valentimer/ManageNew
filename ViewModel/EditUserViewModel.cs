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
    public class EditUserViewModel: ObservableObject
    {
        public List<Position> AllPositions { get; set; }

        public string UserName { get; set; }
        public string UserSurName { get; set; }
        public string UserPhone { get; set; }
        public Position UserPosition { get; set; }

        public RelayCommand<Window> EditUserCommand { get; }

        public EditUserViewModel(User selectedUser)
        {
            EditUserCommand = new RelayCommand<Window>(EditUser);
            AllPositions = DataWorker.GetAllPosition();
            UserName = selectedUser.Name;
            UserSurName = selectedUser.SurName;
            UserPhone = selectedUser.Phone;
            UserPosition = selectedUser.Position;
        }

        private void EditUser(Window window)
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
