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

namespace Xamarin.SQlite.Domain
{
    public class User
    {
        [AutoIncrement, PrimaryKey]
        public int ID { get; set; }

        public string Name { get; set; }

        public override string ToString()
        {
            return "ID: " + ID + "-" + " Name: " + Name;
        }
    }
}