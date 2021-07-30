using System;
using System.Threading.Tasks;
using task.Helper;
using task.Model;
using task.Service.InterFace;

namespace task.Service.Classes
{
    public class ArticleServices: IArticleServices
    {

        public async Task<Tuple<ArticlesModel, bool, string>> GetArticles(string source)
        {
            var response = await HttpManager.GetAsync<ArticlesModel>($"https://newsapi.org/v1/articles?source={source}&apiKey=33d48e11ae934d1da44b75f767eb89e0").ConfigureAwait(false);
            return response;
        }
    }
}
