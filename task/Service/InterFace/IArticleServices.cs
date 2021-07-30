using System;
using System.Threading.Tasks;
using task.Model;

namespace task.Service.InterFace
{
    public interface IArticleServices
    {
        Task<Tuple<ArticlesModel, bool, string>> GetArticles(string source);

    }
}
