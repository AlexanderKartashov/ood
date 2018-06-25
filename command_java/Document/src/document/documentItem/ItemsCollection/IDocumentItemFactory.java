package document.documentItem.ItemsCollection;

import document.documentItem.image.IImage;
import document.documentItem.image.Size;
import document.documentItem.paragraph.IParagraph;
import storage.IResource;

import java.io.IOException;

public interface IDocumentItemFactory {

    IImage CreateImage(String path, Size size) throws IOException;

    IParagraph CreateParagraph(String text);
}
