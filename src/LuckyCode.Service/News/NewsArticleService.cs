using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using LuckyCode.Core;
using LuckyCode.Core.Data;
using LuckyCode.Core.Data.DapperExtensions;
using LuckyCode.Core.Utility.Sequence;
using LuckyCode.Data;
using LuckyCode.Entity.News;
using LuckyCode.IService.News;
using LuckyCode.ViewModels.Mapper;
using LuckyCode.ViewModels.News;
using Microsoft.Extensions.Logging;

namespace LuckyCode.Service.News
{
    public class NewsArticleService : INewsArticleService
    {
       
        private ILiteCodeContext _context;
        private IRepository<NewsArticle> _repository;
        private IRepository<NewsArticleText> _textRepository;
        private ILogger _logger;
        private IDapperContext _dapperContext;
        public NewsArticleService(IDapperContext dapperContext, IRepository<NewsArticle> repository, IRepository<NewsArticleText> textRepository, ILogger<NewsArticleService> logger, ILiteCodeContext context)
        {
            _repository = repository;
            _textRepository = textRepository;
            _context = context;
            _logger = logger;
            _dapperContext = dapperContext;
        }
        

        public void DeleteNewsArticles(Guid id)
        {
            _repository.Delete(a=>a.ArticleID==id);
            _textRepository.Delete(a=>a.ArticleID==id);
            _context.SaveChanges();
        }

        public List<ArticleFromCategoryId> GetArticleFromCategoryViewModel(long categoryid,int num)
        {
            string sql = @" select c.ArticleID,c.Title,d.ArticleText,c.CategoryID,c.ImgUrl from NewsArticles as c join [NewsArticleText] as d on c.ArticleID=d.ArticleID where c.CategoryID=@categoryid order by c.ArticleID desc offset 0 row fetch next @num rows only";
            return _dapperContext.Query<ArticleFromCategoryId>(sql, new { categoryid = categoryid,num=num }).ToList();
        }

        public List<ArticleTopOneViewModel> GetArticleTopOneViewModel(long categoryid)
        {
            string sql = @"select c.ArticleID,c.Title,d.ArticleText,c.CategoryID from NewsArticles as c join [NewsArticleText] as d on c.ArticleID=d.ArticleID where c.ArticleID in(
select  top 1 a.ArticleID from NewsArticles as a join [NewsArticleText] as b on a.ArticleID=b.ArticleID
 where a.CategoryID in (select CategoryID from Category where ParentID=@categoryid) and c.CategoryID=a.CategoryID order by a.ArticleID desc)
 order by c.CategoryID";
            return _dapperContext.Query<ArticleTopOneViewModel>(sql,new {categoryid=categoryid}).ToList();
        }

        public async Task<ArticleViewModel> GetNewsArticlesViewModel(Guid id)
        {
            var entity = _repository.Query().Single(a => a.ArticleID == id);
            var model = entity.ToModel();
            model.ArticleText =(await _textRepository.SingleAsync(a => a.ArticleID == id)).ArticleText;
            return model;
        }

        public PagedList<ArticleViewModel> GetPagedList(int pageIndex, int pageSize)
        {
            return _repository.Query().ProjectTo<ArticleViewModel>(AutoMapperConfiguration.MapperConfiguration).OrderByDescending(a => a.CreateDate).ToPagedList(pageIndex, pageSize);
        }

        public ArticleViewModel SaveNewsArticles(ArticleViewModel model)
        {
            var entity = model.ToEntity();
            entity.ArticleID = SequenceQueue.NewIdGuid();
            entity.CreateDate=DateTime.Now;
            entity.NewsArticleTexts.Add(new NewsArticleText() { ArticleID = entity.ArticleID, ArticleText = model.ArticleText,ArticleTextID = SequenceQueue.NewIdLong()});
            _repository.AddAsync(entity);
            _context.SaveChanges();
            return model;
        }

        public async Task<ArticleViewModel> UpdateNewsArticles(ArticleViewModel model)
        {
            var entity = model.ToEntity();
            _repository.Update(entity);
            var textEntity =await _textRepository.SingleAsync(a => a.ArticleID == entity.ArticleID);
            textEntity.ArticleText = model.ArticleText;
            _textRepository.Update(textEntity);
            _context.SaveChanges();
            return model;
        }
    }
}
