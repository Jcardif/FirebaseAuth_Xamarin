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
using Android.Support.V7.App;
using Firebase.Auth;
using static Android.Views.View;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;

namespace FbaseAuth
{
    [Activity(Label = "Dashboard", Theme = "@style/Firebase")]
    public class Dashboard : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        TextView txtwelcome;
        EditText newPass_input;
        Button btnChangepass, btnLogout;
        RelativeLayout relativeashboard;

        FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.Dashboard);

            //init firebase
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            //view
            txtwelcome = FindViewById<TextView>(Resource.Id.dashboardWelcome);
            newPass_input = FindViewById<EditText>(Resource.Id.newpas);
            btnChangepass = FindViewById<Button>(Resource.Id.changepass);
            btnLogout = FindViewById<Button>(Resource.Id.dbLogout);
            relativeashboard = FindViewById<RelativeLayout>(Resource.Id.relativeDashboard);

            btnChangepass.SetOnClickListener(this);
            btnLogout.SetOnClickListener(this);

        }
        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.changepass)
            {
                Changepassword(newPass_input.Text);
            }
            else if (v.Id == Resource.Id.dbLogout)
            {
                LogoutUser();
            }
        }

        private void LogoutUser()
        {
            auth.SignOut();
            if (auth.CurrentUser == null)
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
                Finish();
            }
        }

        private void Changepassword(string newpassword)
        {
            FirebaseUser user = auth.CurrentUser;
            user.UpdatePassword(newpassword)
                .AddOnCompleteListener(this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful == true)
            {
                Snackbar sbardb = Snackbar.Make(relativeashboard, "Password has been changed", Snackbar.LengthShort);
                sbardb.Show();
            }
        }
    }
}