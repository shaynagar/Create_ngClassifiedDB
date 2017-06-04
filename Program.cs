using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Create_ngClassifiedDB
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ngClassifiedsDBContext())
            {
                var user = new User();
                user.UserName = "shaynagar";
                user.Password = "Password";
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }

    public class User
    {
        public User()
        {
            this.Items = new List<Item>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Username Required", AllowEmptyStrings = false)]
        [RegularExpression("[a-zA-Z0-9]{6,16}", ErrorMessage = "Username must have 6 to 16 characters")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password Required", AllowEmptyStrings = false)]
        [RegularExpression("(?=.*[A-Z])[a-zA-Z0-9]{6,16}", ErrorMessage = "Password must have 6 to 16 characters and at least 1 capital letter")]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage ="Invalid Email adress")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public virtual List<Item> Items { get; set; }
    }

    public class Item
    {
        public Item()
        {
            this.Categories = new List<Category>();
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public DateTime Posted { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public virtual List<Category> Categories { get; set; }
        public string Image { get; set; }
        public int Views { get; set; }

    }

    public class Category
    {
        public Category()
        {
            this.Items = new List<Item>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Item> Items { get; set; }
    }

    public class ngClassifiedsDBContext : DbContext
    {
        public ngClassifiedsDBContext()
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
