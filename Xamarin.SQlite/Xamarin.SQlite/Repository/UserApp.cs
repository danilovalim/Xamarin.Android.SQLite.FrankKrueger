using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System.IO;
using Xamarin.SQlite.Domain;

namespace Xamarin.SQlite.Repository
{
    public class UserApp
    {
        private string dbPath = "";
        private SQLiteConnection db;

        public UserApp()
        {
            dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "dbUsers.db3");

            db = new SQLiteConnection(dbPath);

            var tableInfo = db.GetTableInfo("User");

            if (tableInfo.Count == 0)
            {
                db.CreateTable<User>();
            }
        }

        public bool Insert(User user)
        {
            try
            {
                db.Insert(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public User GetbyID(int id)
        {
            try
            {
                var user = db.Get<User>(id);

                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                var usersList = db.Table<User>();

                if (usersList.Count() > 0)
                {
                    return usersList.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool RemoveUser(int id)
        {
            try
            {
                var user = db.Get<User>(id);

                if (user != null)
                {
                    db.Delete<User>(id);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}