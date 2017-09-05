using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Firebase;
using Firebase.Auth;
using System;
using static Android.Views.View;
using Android.Views;
using Android.Gms.Tasks;
using Android.Support.Design.Widget;

namespace FbaseAuth
{
    [Activity(MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Firebase")]
    public class MainActivity : AppCompatActivity, IOnClickListener, IOnCompleteListener
    {
        Button btnLogin;
        EditText email_input, pass_input;
        TextView txt_signUp, txt_forgotPass;

        RelativeLayout relativeMain;

        public static FirebaseApp app;
        FirebaseAuth auth;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            //init firebase
            InitFirebaseAuth();

            //view
            btnLogin = FindViewById<Button>(Resource.Id.btnLogin);
            email_input = FindViewById<EditText>(Resource.Id.loginEmail);
            pass_input = FindViewById<EditText>(Resource.Id.loginPassword);
            txt_signUp = FindViewById<TextView>(Resource.Id.signUp);
            txt_forgotPass = FindViewById<TextView>(Resource.Id.forgotpass);
            relativeMain = FindViewById<RelativeLayout>(Resource.Id.relativeMain);

            txt_signUp.SetOnClickListener(this);
            txt_forgotPass.SetOnClickListener(this);
            btnLogin.SetOnClickListener(this);
        }

        private void InitFirebaseAuth()
        {
            var options = new FirebaseOptions.Builder()
                .SetApplicationId("1:249232069396:android:cbe0f415e0307455")
                .SetApiKey("AIzaSyBoPHjd6w92ZDXFJ4YiVlZEFt0N4_tsXDQ")
                .Build();

            if (app == null)
                app = FirebaseApp.InitializeApp(this, options);
                    auth = FirebaseAuth .GetInstance(app);
        }

        public void OnClick(View v)
        {
           if (v.Id == Resource.Id.forgotpass)
            {
                StartActivity(new Android.Content.Intent(this, (typeof(ForgotPass))));
                Finish();
            }
           else if (v.Id == Resource.Id.signUp)
            {
                StartActivity(new Android.Content.Intent(this, (typeof(SignUp))));
                Finish();
            }
            else if (v.Id == Resource.Id.btnLogin)
            {
                LoginUser(email_input.Text, pass_input.Text);
            }
        }

        private void LoginUser(string email, string password)
        {
            auth.SignInWithEmailAndPassword(email, password)
                .AddOnCompleteListener(this);
        }

        public void OnComplete(Task task)
        {
            if (task.IsSuccessful)
            {
                StartActivity(new Android.Content.Intent(this, (typeof(Dashboard))));
                Finish();
            }
            else
            {
                Snackbar sbarMain = Snackbar.Make(relativeMain, "Log In Failed", Snackbar.LengthShort);
                sbarMain.Show();
            }
        }
    }
}

