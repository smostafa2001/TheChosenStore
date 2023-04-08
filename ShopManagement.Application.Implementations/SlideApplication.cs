using ShopManagement.Application.Contracts.Shared;
using ShopManagement.Application.Contracts.SlideAggregate;
using ShopManagement.Domain.SlideAggregate;
using System.Collections.Generic;

namespace ShopManagement.Application.Implementations
{
    public class SlideApplication : ISlideApplication
    {
        private readonly ISlideRepository _repository;

        public SlideApplication(ISlideRepository repository) => _repository = repository;

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();
            var slide = new Slide
            (
                command.Picture,
                command.PictureAlt,
                command.PictureTitle,
                command.Heading,
                command.Title,
                command.Text,
                command.BtnText,
                command.Link
            );
            _repository.Create(slide);
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operation = new OperationResult();
            var slide = _repository.Get(command.Id);
            if (slide is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slide.Edit
            (
                command.Picture,
                command.PictureAlt,
                command.PictureTitle,
                command.Heading,
                command.Title,
                command.Text,
                command.BtnText,
                command.Link
            );
            _repository.Save();
            return operation.Succeeded();
        }

        public EditSlide GetDetails(long id) => _repository.GetDetails(id);

        public List<SlideViewModel> GetSlides() => _repository.GetSlides();

        public OperationResult Remove(long id)
        {
            var operation = new OperationResult();
            var slide = _repository.Get(id);
            if (slide is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slide.Remove();
            _repository.Save();
            return operation.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operation = new OperationResult();
            var slide = _repository.Get(id);
            if (slide is null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            slide.Restore();
            _repository.Save();
            return operation.Succeeded();
        }
    }
}