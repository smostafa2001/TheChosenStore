using Common.Application;
using ShopManagement.Application.Contracts.SlideAggregate;
using ShopManagement.Domain.SlideAggregate;
using System.Collections.Generic;

namespace ShopManagement.Application.Implementations;

public class SlideApplication(ISlideRepository repository, IFileUploader fileUploader) : ISlideApplication
{
    public OperationResult Create(CreateSlide command)
    {
        var operation = new OperationResult();
        var pictureName = fileUploader.Upload(command.Picture, "slides");
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
        repository.Create(slide);
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Edit(EditSlide command)
    {
        var operation = new OperationResult();
        var slide = repository.Get(command.Id);
        if (slide is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        var pictureName = fileUploader.Upload(command.Picture, "slides");
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
        repository.Save();
        return operation.Succeeded();
    }

    public EditSlide GetDetails(long id) => repository.GetDetails(id);

    public List<SlideViewModel> GetSlides() => repository.GetSlides();

    public OperationResult Remove(long id)
    {
        var operation = new OperationResult();
        var slide = repository.Get(id);
        if (slide is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        slide.Remove();
        repository.Save();
        return operation.Succeeded();
    }

    public OperationResult Restore(long id)
    {
        var operation = new OperationResult();
        var slide = repository.Get(id);
        if (slide is null)
            return operation.Failed(ApplicationMessages.RecordNotFound);

        slide.Restore();
        repository.Save();
        return operation.Succeeded();
    }
}