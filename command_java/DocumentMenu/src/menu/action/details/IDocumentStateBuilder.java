package menu.action.details;

import document.documentItem.ItemsCollection.IDocumentItem;
import document.documentItem.image.IImage;
import document.documentItem.paragraph.IParagraph;
import document.documentItem.title.IDocumentTitle;

import java.io.IOException;

public interface IDocumentStateBuilder {

    void BuildTitle(IDocumentTitle title) throws IOException;

    void BuildDocumentItem(IDocumentItem item) throws IOException;

    void BeforeBuild() throws IOException;

    void AfterBuild() throws Exception;
}
