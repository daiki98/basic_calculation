using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace basic_calculation
{
    public class BaseLinkButton : Label
    {
        public BaseLinkButton() : base()
        {
            //初期値
            this.IsUnderline = true;
            this.IsDisabled = false;
            base.BackgroundColor = Color.Transparent;

            //リンクをタップした際の動作を設定
            var tgr = new TapGestureRecognizer();
            tgr.Tapped += this.OnClicked;
            this.GestureRecognizers.Add(tgr);
        }

        public static BindableProperty UrlProperty = BindableProperty.Create(
            propertyName: "Url",
            returnType: typeof(string),
            declaringType: typeof(BaseLinkButton),
            defaultValue: "",
            defaultBindingMode: BindingMode.TwoWay);

        public static BindableProperty IsUnderlineProperty = BindableProperty.Create(
            propertyName: "IsUnderline",
            returnType: typeof(bool),
            declaringType: typeof(BaseLinkButton),
            defaultValue: true,
            defaultBindingMode: BindingMode.TwoWay);

        //URLを設定するプロパティ
        public string Url
        {
            get
            {
                return (string)GetValue(UrlProperty); ;
            }
            set
            {
                SetValue(UrlProperty, value);
            }
        }
        //下線を表示するかどうか
        public bool IsUnderline
        {
            get
            {
                return (bool)GetValue(IsUnderlineProperty); ;
            }
            set
            {
                SetValue(IsUnderlineProperty, value);
            }
        }
        //有効無効の設定（IsEnabledがoverrideできないため）
        public bool IsDisabled
        {
            get
            {
                return !base.IsEnabled;
            }
            set
            {
                base.IsEnabled = !value;
                OnEnabledChange(null, null);
            }
        }
        //無効の場合に文字色を変更する
        void OnEnabledChange(object sender, EventArgs e)
        {
            if (!base.IsEnabled)
            {
                this.TextColor = Color.Gray;
            }
            else
            {
                this.TextColor = Color.Blue;
            }
        }
        //タップ時の動作
        void OnClicked(object sender, EventArgs e)
        {
            //無効の場合はタップ時の動作をしない
            if (!base.IsEnabled)
            {
                return;
            }

            //URL未設定の場合は処理を終了する
            if (String.IsNullOrEmpty(this.Url))
            {
                return;
            }

            string err = String.Empty;
            //外部ブラウザを起動しURLのサイトを表示する
            DependencyService.Get<IWebBrowserService>().Open(new Uri(this.Url));
        }
    }
}
