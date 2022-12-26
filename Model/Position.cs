using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ManagerFamily.Model
{
    public class Position
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public int MaxNumber { get; set; }

        public List<User> Users { get; set; }

        public int SpendingCategoryId { get; set; }

        public virtual SpendingCategory SpendingCategory { get; set; }
        [NotMapped]
        public SpendingCategory PositionSpendingCategory
        {
            get
            {
                return DataWorker.GetCategoryById(SpendingCategoryId);
            }
        }
        [NotMapped]
        public List<User> PositionUsers
        {
            get
            {
                return DataWorker.GetAllUsersByPositionId(Id);
            }
        } 
    }
}
