package document.commands;

import document.documentItem.ItemsCollection.IDocumentItem;
import document.documentItem.ItemsCollection.IMutableDocumentItemCollection;
import document.documentItem.image.IMutableImage;
import document.documentItem.image.Size;
import document.documentItem.paragraph.IMutableParagraph;
import document.documentItem.title.IMutableDocumentTitle;
import hsitory.ICommand;

import java.util.Optional;

public interface ICommandsFactory {
    ICommand EditText(IMutableParagraph paragraph, String newText);

    ICommand DeleteItem(IMutableDocumentItemCollection items, int pos) ;

    ICommand ResizeImage(IMutableImage image, Size newSize);

    ICommand SetTitle(IMutableDocumentTitle title, String newTitle);

    ICommand InsertItem(IMutableDocumentItemCollection items, IDocumentItem newItem, Optional<Integer> position);
}
