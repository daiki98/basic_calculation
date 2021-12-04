using System;
using System.Collections.Generic;
using System.Text;
using Android.Content;
using Xamarin.Forms;
using basic_calculation;

[assembly: Dependency(typeof(WebBrowserService))]
public class WebBrowserService : IWebBrowserService
{
    public void Open(Uri uri)
    {
        Forms.Context.StartActivity(
            new Intent(Intent.ActionView,
                       global::Android.Net.Uri.Parse(uri.AbsoluteUri))
        );
    }
}
