using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.SQlite.Repository;
using Xamarin.SQlite.Domain;
using System.Collections.Generic;

namespace Xamarin.SQlite
{
    [Activity(Label = "Xamarin.SQlite", MainLauncher = true, Icon = "@drawable/icon", Theme ="@android:style/Theme.Black.NoTitleBar")]
    public class MainActivity : Activity
    {
        private EditText mText;
        private ListView mList;
        private Button mLoadID, mLoadAll, mAdd, mRemove;
        private UserApp userApp = new UserApp();
        private User user;
        private List<User> usersList;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            mText = FindViewById<EditText>(Resource.Id.etField);
            mList = FindViewById<ListView>(Resource.Id.list);
            mLoadAll = FindViewById<Button>(Resource.Id.btnLoadAll);
            mLoadID = FindViewById<Button>(Resource.Id.btnFindID);
            mAdd = FindViewById<Button>(Resource.Id.btbAdd);
            mRemove = FindViewById<Button>(Resource.Id.btnRemove);

            mAdd.Click += MAdd_Click;
            mLoadAll.Click += MLoadAll_Click;
            mLoadID.Click += MLoadID_Click;
            mRemove.Click += MRemove_Click;

        }

        private void MRemove_Click(object sender, EventArgs e)
        {
            int userID;

            try
            {
                userID = int.Parse(mText.Text);
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Invalid ID, please try again!", ToastLength.Short).Show();
                return;
            }

            var result = userApp.RemoveUser(userID);


            if (result)
            {
                Toast.MakeText(this, "User removed!", ToastLength.Long).Show();
                mText.Text = "";
                FillList();
            }
            else
            {
                Toast.MakeText(this, "Users not removed!", ToastLength.Short).Show();
            }
        }

        private void MLoadID_Click(object sender, EventArgs e)
        {
            int userID;

            try
            {
                userID = int.Parse(mText.Text);
            }
            catch (Exception)
            {
                Toast.MakeText(this, "Invalid ID, please try again!", ToastLength.Short).Show();
                return;
            }

            var user = userApp.GetbyID(userID);

            if(user != null)
            {
                Toast.MakeText(this, user.Name, ToastLength.Long).Show();
            }
            else
            {
                Toast.MakeText(this, "Users not found!", ToastLength.Short).Show();
            }
        }

        private void MLoadAll_Click(object sender, EventArgs e)
        {
            FillList();
        }

        private void MAdd_Click(object sender, EventArgs e)
        {
            user = new User();

            if(mText.Text != "")
            {
                user.Name = mText.Text;
                var result = userApp.Insert(user);

                if(result)
                {
                    Toast.MakeText(this, "User added!", ToastLength.Short).Show();
                    mText.Text = "";
                    FillList();
                }
                else
                {
                    Toast.MakeText(this, "Error, Please try again!", ToastLength.Short).Show();
                }
            }
            else
            {
                Toast.MakeText(this, "Field can´t be empty!", ToastLength.Short).Show();
                mText.RequestFocus();
            }
        }

        private void FillList()
        {
            usersList = new List<User>();

            usersList = userApp.GetAllUsers();

            if(usersList!= null)
            {
                var adapter = new ArrayAdapter(this, global::Android.Resource.Layout.SimpleListItem1, usersList);
                mList.Adapter = adapter;
            }
            else
            {
                Toast.MakeText(this, "No users found!", ToastLength.Short).Show();
            }
        }
    }
}

