using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Views;
using Android.Content;
using System;
using ShoppingListApp.Helper;
using System.Collections.Generic;

namespace ShoppingListApp
{
    [Activity(Label = "@string/app_name"/*, Theme = "@style/AppTheme"*/, MainLauncher = true, /*Icon = "@drawable/icon",*/ Theme = "@style/Theme.AppCompat.Light")]
    public class MainActivity : AppCompatActivity
    {
        EditText taskEditText;
        DBHelper DBHelper;
        CustomAdapter adapter;
        ListView lstTask;

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_item, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch(item.ItemId)
            {
                case Resource.Id.action_add:
                    taskEditText = new EditText(this);
                    Android.Support.V7.App.AlertDialog dialog = new Android.Support.V7.App.AlertDialog.Builder(this)
                        .SetTitle("Add New Item")
                        .SetMessage("What item would you like to add to your shopping list?")
                        .SetView(taskEditText)
                        .SetPositiveButton("Add", OkAction)
                        .SetNegativeButton("Cancel", CancelAction)
                        .Create();
                    dialog.Show();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }

        private void CancelAction(object sender, DialogClickEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OkAction(object sender, DialogClickEventArgs e)
        {
            string task = taskEditText.Text;
            DBHelper.InsertNewTask(task);
            LoadTaskList();
        }

        public void LoadTaskList()
        {
            List<string> taskList = DBHelper.getTaskList();
            adapter = new CustomAdapter(this, taskList,DBHelper);
            lstTask.Adapter = adapter;

        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            DBHelper = new DBHelper(this);
           lstTask = FindViewById<ListView>(Resource.Id.lstTask);

            //Load Data
            LoadTaskList();
        }
    }
}