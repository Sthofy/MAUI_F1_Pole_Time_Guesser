using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using AndroidX.Browser.Trusted;
using ScreenOrientation = Android.Content.PM.ScreenOrientation;

namespace PoleTimeGuesser;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
}
