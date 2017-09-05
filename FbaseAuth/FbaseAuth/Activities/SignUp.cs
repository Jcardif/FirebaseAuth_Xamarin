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
    [Activity(Label = "SignUp", Theme = "@style/Firebase")]
    public class SignUp : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        Button btnsignUp;
        TextView txtlogin, txtforgotpass;
        EditText email_input, password_input;
        RelativeLayout relativeSignUp;


        FirebaseAuth auth;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.SignUp);

            //init firebase
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            //view
            btnsignUp = FindViewById<Button>(Resource.Id.btnSignUp);
            txtlogin = FindViewById<TextView>(Resource.Id.txtlogin);
            txtforgotpass = FindViewById<TextView>(Resource.Id.txtforgotpass);
            email_input = FindViewById<EditText>(Resource.Id.SignUpEmail);
            password_input = FindViewById<EditText>(Resource.Id.signUpPassword);
            relativeSignUp = FindViewById<RelativeLayout>(Resource.Id.relativeSignUp);

            btnsignUp.SetOnClickListener(this);
            txtlogin.SetOnClickListener(this);
            txtforgotpass.SetOnClickListener(this);
        }
        public void OnClick(View v)
        {
            if (v.Id == Resource.Id.txtlogin)
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
                Finish();
            }
            else if (v.Id == Resource.Id.txtforgotpass) 
            {
                StartActivity(new Intent(this, typeof(ForgotPass)));
                Finish();
            }
            else if (v.Id == Resource.Id.btnSignUp)
            {
                SignUpUser(email_input.Text, password_input.Text);

            }
        }

        private void SignUpUser(string email, string password)
        {
            auth.CreateUserWithEmailAndPassword(email, password)
                .AddOnCompleteListener(this, this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful == true)
            {
                Snackbar sbarMain = Snackbar.Make(relativeSignUp, "Registry Successful", Snackbar.LengthShort);
                sbarMain.Show();
            }
            else
            {
                Snackbar sbarMain = Snackbar.Make(relativeSignUp, "Registry In Failed", Snackbar.LengthShort);
                sbarMain.Show();
            }
        }
    }
}