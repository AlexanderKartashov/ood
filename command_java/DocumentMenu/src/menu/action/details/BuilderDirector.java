package menu.action.details;

import document.IDocument;
import document.documentItem.ItemsCollection.IDocumentItem;

import java.io.IOException;
import java.util.Iterator;

class BuilderDirector {

    public void Build(IDocument document, IDocumentStateBuilder builder) throws Exception {
        builder.BeforeBuild();
        builder.BuildTitle(document.Title());
        for (Iterator<IDocumentItem> it = document.Items().GetIterator(); it.hasNext(); ) {
            IDocumentItem item = it.next();
            builder.BuildDocumentItem(item);
        }
        builder.AfterBuild();
    }
}
