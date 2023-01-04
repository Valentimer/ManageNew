using ManagerFamily.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace ManagerFamily.Model
{
    public class DataWorker
    {

        //получить все категории трат

        public static List<SpendingCategory> GetAllSpendingCategories()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var spendingCategories = db.SpendingCategories.ToList();
                return spendingCategories;
            }
        }

        //получить все конкретные траты

        public static List<Position> GetAllPosition()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var position = db.Positions.ToList();
                return position;
            }
        }

        //получить все траты

        public static List<User> GetAllUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = db.Users.ToList();
                return user;
            }
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="name"></param>
        /// <returns>if succed return unique ID, otherwise MinValue</returns>
        public static int CreateSpendingCategory(string name)
        {
            int result = int.MinValue;

            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.SpendingCategories.Any(x => x.Name == name);
                if (!checkIsExist)
                {
                    SpendingCategory newSpendingCategory = new SpendingCategory { Name = name };
                    db.SpendingCategories.Add(newSpendingCategory);
                    db.SaveChanges();
                    result = newSpendingCategory.Id;
                }
                return result;
            }
        }

        //создать конкретную трату

        public static int CreatePosition(string name, decimal price, int maxNumber, SpendingCategory department)
        {
            int result = -1;

            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Positions.Any(x => x.Name == name && x.Price == price);
                if (!checkIsExist)
                {
                    Position newPositions = new Position { Name = name, Price = price, MaxNumber = maxNumber, SpendingCategoryId = department.Id };
                    db.Positions.Add(newPositions);
                    db.SaveChanges();
                    result = newPositions.Id;
                }
                return result;
            }
        }

        //создать трату

        public static int CreateUser(string name, string surName, string phone, Position position)
        {
            int result = -1;

            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Users.Any(x => x.Name == name && x.SurName == surName && x.Phone == phone);
                if (!checkIsExist)
                {
                    User newUsers = new User { Name = name, SurName = surName, Phone = phone, PositionId = position.Id };
                    db.Users.Add(newUsers);
                    db.SaveChanges();
                    result = newUsers.Id;
                }
                return result;
            }
        }

        //удалить категорию трат

        public static string DeleteSpendingCategory(SpendingCategory department)
        {
            string result = "Не существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                db.SpendingCategories.Remove(department);
                db.SaveChanges();
                result = "Категория " + department.Name + " удалена!";
            }
            return result;
        }

        //удалить конкретную трату

        public static string DeletePosition(Position position)
        {
            string result = "Не существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Positions.Remove(position);
                db.SaveChanges();
                result = "Вариант с затратой " + position.Name + " удалена!";
            }
            return result;
        }

        //удалить трату

        public static string DeleteUser(User user)
        {
            string result = "Не существует";

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
                result = "Ваша затрата " + user.Name + " удалена!";
            }
            return result;
        }

        //редактировать категорию трат

        public static int EditSpendingCategory(SpendingCategory oldDepartment, string newName)
        {
            int result = int.MinValue;

            using (ApplicationContext db = new ApplicationContext())
            {
                SpendingCategory spendingCategory = db.SpendingCategories.FirstOrDefault(x => x.Id == oldDepartment.Id);
                spendingCategory.Name = newName;
                db.SaveChanges();
                result = spendingCategory.Id;
                return result;
            }
        }

        //редактировать конкретную трату

        public static int EditPosition(Position oldPosition, string newName, decimal newPrice, int newNumber, SpendingCategory newDepartment)
        {
            int result = int.MinValue;

            using (ApplicationContext db = new ApplicationContext())
            {
                Position position = db.Positions.FirstOrDefault(x => x.Id == oldPosition.Id);
                position.Name = newName;
                position.Price = newPrice;
                position.MaxNumber = newNumber;
                position.SpendingCategoryId = newDepartment.Id;
                db.SaveChanges();
                result = position.Id;
                return result;
            }
        }

        //редактировать трату

        public static int EditUser(User oldUser, string newName, string newSurName, string newPhone, Position newPosition)
        {
            int result = int.MinValue;

            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(x => x.Id == oldUser.Id);
                user.Name = newName;
                user.SurName = newSurName;
                user.Phone = newPhone;
                user.PositionId = newPosition.Id;
                db.SaveChanges();
                result = user.Id;
                return result;
            }
        }

        //получение позиции по id позиции
        public static Position GetPositionById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Position pos = db.Positions.FirstOrDefault(x => x.Id == id);
                return pos;
            }
        }

        //получение категории по id категории
        public static SpendingCategory GetCategoryById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                SpendingCategory pos = db.SpendingCategories.FirstOrDefault(x => x.Id == id);
                return pos;
            }
        }

        public static User GetUserById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User pos = db.Users.FirstOrDefault(x => x.Id == id);
                return pos;
            }
        }

        //получение всех пользователей по id позиции
        public static List<User> GetAllUsersByPositionId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<User> users = (from user in GetAllUsers() where user.PositionId == id select user).ToList();
                return users;
            }
        }
        //получение всех позиций по id отдела
        public static List<Position> GetAllPositionBySpendingCategoryId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Position> positions = (from position in GetAllPosition() where position.SpendingCategoryId == id select position).ToList();
                return positions;
            }
        }
    }
}
