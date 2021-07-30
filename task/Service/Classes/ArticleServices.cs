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
            var response = await HttpManager.GetAsync<ArticlesModel>($"{App.baseUrl}articles?source={source}&apiKey={App.apiKey}").ConfigureAwait(false);
            return response;
        }
    }
}
