using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using task.Model;
using task.Service.InterFace;

namespace task.ViewModel
{
    public class ArticlesViewModel:ViewModelBase
    {

        private Article selectedArticle;
        public Article SelectedArticle
        {
            get => selectedArticle;
            set => SetProperty(ref selectedArticle, value);
        }
        public DelegateCommand NavigateToArticleDetailPageCommand { get; set; }
        ObservableCollection<Article> _ArticlesLst = new ObservableCollection<Article>();
        public ObservableCollection<Article> ArticlesLst

        {
            set
            {
                _ArticlesLst = value;
                RaisePropertyChanged();
            }
            get
            {
                return _ArticlesLst;
            }
        }

        IArticleServices _articleServices;
        public ArticlesViewModel(INavigationService navigationService, IPageDialogService dialogService,IArticleServices articleServices) : base(navigationService, dialogService)
        {
           
            _articleServices = articleServices;
            ArticlesLst = new ObservableCollection<Article>();
            NavigateToArticleDetailPageCommand = new DelegateCommand(NavigateToArticleDetailPage);
            GetArticles();
        }
        private void NavigateToArticleDetailPage()
        {
            if (SelectedArticle != null)
            {
                var navParameters = new NavigationParameters();
                navParameters.Add("Article", SelectedArticle);
                var page = NavigationService.GetNavigationUriPath();

                if (page.EndsWith("/Home"))
                {
                    NavigationService.NavigateAsync("NavigationPage/Home/ArticleDetails", navParameters);
                }
                else
                {
                    NavigationService.NavigateAsync("ArticleDetails", navParameters);
                }
                SelectedArticle = null;

            }
            //
        }
        async Task GetArticles()
        {
            List<Article> articles = new List<Article>();
            IsLoading = true;
            try
            {
                ArticlesLst = new ObservableCollection<Article>();
                var paramLst = new List<string>() { "associated-press", "the-next-web" };
                for (int i = 0; i < 2; i++)
                {
                    var response = await _articleServices.GetArticles(paramLst[i]);
                    if (response.Item2)
                    {
                        if (response.Item1.status == "ok")
                        {
                            articles.AddRange(response.Item1.articles);
                        }
                        else
                        {
                            await DialogService.DisplayAlertAsync("", response.Item1.status, "OK");
                        }

                    }
                    else
                    {
                        articles = new List<Article>()
{
new Article()
{
     author="sdsd",
     title="sasas", description="sasas", urlToImage="asas"
}
};
                        await DialogService.DisplayAlertAsync("", response.Item3, "Ok"); }
                }
                ArticlesLst =new ObservableCollection<Article>(articles);
               
            }
            catch (Exception ex)
            {

            }
            finally { IsLoading = false; }
    
        }

    }
}
