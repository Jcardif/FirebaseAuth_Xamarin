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
    [Activity(Label = "ForgotPass", Theme ="@style/Firebase")]
    public class ForgotPass : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        private EditText email_input;
        private Button resetPass;
        private TextView txtViewback;
        private RelativeLayout relativeForgot;

        FirebaseAuth auth;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ForgotPass);

            //init firebase
            auth = FirebaseAuth.GetInstance(MainActivity.app);

            //view
            email_input = FindViewById<EditText>(Resource.Id.forgotpassEdtTxt);
            resetPass = FindViewById<Button>(Resource.Id.btnForgotPassReset);
            txtViewback = FindViewById<TextView>(Resource.Id.forgotPass_back);
            relativeForgot = FindViewById<RelativeLayout>(Resource.Id.relativeForgot);

            email_input.SetOnClickListener(this);
            resetPass.SetOnClickListener(this);
            txtViewback.SetOnClickListener(this);
        }
        public void OnClick(View v)
        {
           if (v.Id == Resource.Id.forgotpassEdtTxt)
            {

            }
           else if (v.Id == Resource.Id.btnForgotPassReset)
            {
                ResetPassword(email_input.Text);
            }
           else if (v.Id == Resource.Id.forgotPass_back)
            {
                StartActivity(new Intent(this, typeof(MainActivity)));
                Finish();
            }
        }

        private void ResetPassword(string email)
        {
            auth.SendPasswordResetEmail(email)
                .AddOnCompleteListener(this, this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful==false)
            {
                Snackbar sbarForgot = Snackbar.Make(relativeForgot, "Reset Password Failed", Snackbar.LengthShort);
                sbarForgot.Show();
            }
            else
            {
                Snackbar sbarForgot = Snackbar.Make(relativeForgot, "Reset Passwor link sent to email:"+email_input.Text, Snackbar.LengthShort);
                sbarForgot.Show();
            }
        }
    }
}