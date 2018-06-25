package menu.action.details;

import document.documentItem.ItemsCollection.IDocumentItem;
import document.documentItem.image.IImage;
import document.documentItem.paragraph.IParagraph;
import document.documentItem.title.IDocumentTitle;

public interface IDocumentStateBuilder {

    void BuildTitle(IDocumentTitle title);

    void BuildDocumentItem(IDocumentItem item);

    void BeforeBuild();

    void AfterBuild();
}
