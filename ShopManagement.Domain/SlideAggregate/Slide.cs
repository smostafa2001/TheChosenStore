using Common.Domain;

namespace ShopManagement.Domain.SlideAggregate;

public class Slide(string picture, string pictureAlt, string pictureTitle, string heading, string title, string text, string btnText, string link) : EntityBase
{
    public string Picture { get; private set; } = picture;
    public string PictureAlt { get; private set; } = pictureAlt;
    public string PictureTitle { get; private set; } = pictureTitle;
    public string Heading { get; private set; } = heading;
    public string Title { get; private set; } = title;
    public string Text { get; private set; } = text;
    public string BtnText { get; private set; } = btnText;
    public string Link { get; private set; } = link;
    public bool IsRemoved { get; private set; } = false;

    public void Edit(string picture, string pictureAlt, string pictureTitle, string heading, string title, string text, string btnText, string link)
    {
        if (!string.IsNullOrWhiteSpace(picture)) Picture = picture;

        PictureAlt = pictureAlt;
        PictureTitle = pictureTitle;
        Heading = heading;
        Title = title;
        Text = text;
        BtnText = btnText;
        Link = link;
    }

    public void Remove() => IsRemoved = true;

    public void Restore() => IsRemoved = false;
}