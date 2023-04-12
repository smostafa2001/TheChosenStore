using Framework.Application;
using ShopManagement.Application.Contracts.SlideAggregate;
using ShopManagement.Domain.SlideAggregate;
using System.Collections.Generic;

namespace ShopManagement.Application.Implementations
{
    public class SlideApplication : ISlideApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly ISlideRepository _repository;

        public SlideApplication(ISlideRepository repository, IFileUploader fileUploader)
        {
            _repository = repository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operation = new OperationResult();
            var pictureName = _fileUploader.Upload(command.Picture, "slides");
            var slide = new Slide
            (
                pictureName,
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
            var pictureName = _fileUploader.Upload(command.Picture, "slides");
            slide.Edit
            (
                pictureName,
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