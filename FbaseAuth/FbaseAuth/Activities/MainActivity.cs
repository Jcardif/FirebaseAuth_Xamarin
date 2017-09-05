using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Firebase;
using Firebase.Auth;

namespace FbaseAuth
{
    [Activity(Label = "FbaseAuth", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
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
            // SetContentView (Resource.Layout.Main);
        }
    }
}

