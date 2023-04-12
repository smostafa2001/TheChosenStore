using BlogManagement.Application.Contracts.ArticleCategoryAggregate;
using BlogManagement.Domain.ArticleCategoryAggregate;
using Framework.Application;
using Framework.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public class ArticleCategoryRepository : BaseRepository<long, ArticleCategory>, IArticleCategoryRepository
    {
        private readonly BlogDbContext _blogContext;

        public ArticleCategoryRepository(BlogDbContext blogContext) : base(blogContext) => _blogContext = blogContext;

        public EditArticleCategory GetDetails(long id) => _blogContext.ArticleCategories.Select(ac => new EditArticleCategory
        {
            Id = ac.Id,
            Name = ac.Name,
            CanonicalAddress = ac.CanonicalAddress,
            Description = ac.Description,
            Keywords = ac.Keywords,
            MetaDescription = ac.MetaDescription,
            ShowOrder = ac.ShowOrder,
            Slug = ac.Slug,
            PictureAlt = ac.PictureAlt,
            PictureTitle = ac.PictureTitle
        }).FirstOrDefault(ac => ac.Id == id);
        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            var query = _blogContext.ArticleCategories.Select(ac => new ArticleCategoryViewModel
            {
                Id = ac.Id,
                Name = ac.Name,
                Picture = ac.Picture,
                Description = ac.Description,
                ShowOrder = ac.ShowOrder,
                CreationDate = ac.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(ac => ac.Name.Contains(searchModel.Name));

            return query.OrderByDescending(ac => ac.Id).ToList();
        }
    }
}
