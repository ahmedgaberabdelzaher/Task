using System;
using Prism;
using Prism.Ioc;
using task.Service.Classes;
using task.Service.InterFace;
using task.View;
using task.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace task
{
    public partial class App 
    {
      
        protected override async void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("/Home/NavigationPage/Articles");

        }
          public static string apiKey = "63bc7237ad8748d4afbd7ab5fc5274ff";
        public static string baseUrl = "https://newsapi.org/v1/";


        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<Home, FlyoutViewModel>();
            containerRegistry.RegisterForNavigation<Articles, ArticlesViewModel>();
            containerRegistry.RegisterForNavigation<LiveChat, FlyoutViewModel>();
                      containerRegistry.RegisterForNavigation<Gallery>();
  containerRegistry.RegisterForNavigation<FlyoutMenuPage>();
            containerRegistry.Register<IArticleServices, ArticleServices>();
            containerRegistry.RegisterForNavigation<OnlineNews>();
            containerRegistry.RegisterForNavigation<WishList>();
            containerRegistry.RegisterForNavigation<ArticleDetails,ArticleDetailViewModel>();

            //   containerRegistry.RegisterForNavigation<MyPage,FlyoutViewModel>();

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
