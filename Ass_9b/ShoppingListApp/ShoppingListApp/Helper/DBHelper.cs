using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ShoppingListApp.Helper
{
    public class DBHelper : SQLiteOpenHelper
    {

        private static string DB_NAME = "EDMTDev";
        private static int DB_VER = 1;
        public static string DB_TABLE = "Task";
        public static string DB_COLUMN = "TaskName";


        public DBHelper(Context context):base(context, DB_NAME,null,DB_VER) { }

        public override void OnCreate(SQLiteDatabase db)
        {
            string query = $"CREATE TABLE {DBHelper.DB_TABLE} (ID INTEGER PRIMARY KEY AUTOINCREMENT,{DBHelper.DB_COLUMN} TEXT NOT NULL) ;";
            db.ExecSQL(query);

        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
            string query = $"DELETE TABLE IF EXISTS {DB_TABLE}";
            db.ExecSQL(query);
            OnCreate(db);
        }

        public void InsertNewTask(String task)
        {
            SQLiteDatabase db = this.WritableDatabase;
            ContentValues values = new ContentValues();
            values.Put(DB_COLUMN, task);
            db.InsertWithOnConflict(DB_TABLE, null, values, Android.Database.Sqlite.Conflict.Replace);
            db.Close();

        }

        public void deleteTask(String task)
        {
            SQLiteDatabase db = this.WritableDatabase;
            db.Delete(DB_TABLE, DB_COLUMN + "= ?", new string[] { task });
            db.Close();
        }

        public List<string> getTaskList()
        {
            List<String> tasklist = new List<string>();
            SQLiteDatabase db = this.ReadableDatabase;
            ICursor cursor = db.Query(DB_TABLE, new string[] { DB_COLUMN }, null, null, null, null, null);
            while (cursor.MoveToNext())
            {
                int index = cursor.GetColumnIndex(DB_COLUMN);
                tasklist.Add(cursor.GetString(index));
            }
            return tasklist;
        }

    }
}