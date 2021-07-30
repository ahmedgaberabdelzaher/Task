using System;
using Prism.Navigation;
using Prism.Services;
using task.Model;

namespace task.ViewModel
{
    public class ArticleDetailViewModel:ViewModelBase
    {
        private Article currentArticle;
        public Article CurrentArticle
        {
            get => currentArticle;
            set => SetProperty(ref currentArticle, value);
        }

        public ArticleDetailViewModel(INavigationService navigationService, IPageDialogService dialogService) : base(navigationService, dialogService)
        {

        }
       
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters!=null&&parameters.Count>0)
            {
                CurrentArticle = parameters["Article"] as Article;
            }
        }

    }
}
