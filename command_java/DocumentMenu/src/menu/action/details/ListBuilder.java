package menu.action.details;

import document.documentItem.IDocumentItemVisitor;
import document.documentItem.ItemsCollection.IDocumentItem;
import document.documentItem.image.IImage;
import document.documentItem.paragraph.IParagraph;
import document.documentItem.title.IDocumentTitle;
import menu.ILogger;

class ListBuilder implements IDocumentStateBuilder {

    public ListBuilder(ILogger logger) {
        _visitor = new ListBuilderVisitor(logger);
    }

    @Override
    public void BuildTitle(IDocumentTitle title) {
        title.Accept(_visitor);
    }

    @Override
    public void BuildDocumentItem(IDocumentItem item) {
        item.Accept(_visitor);
    }

    @Override
    public void BeforeBuild() {
    }

    @Override
    public void AfterBuild() {
    }

    private final ListBuilderVisitor _visitor;


    private class ListBuilderVisitor implements IDocumentItemVisitor {

        private ListBuilderVisitor(ILogger logger) {
            _logger = logger;
        }

        @Override
        public void Visit(IDocumentTitle title) {
            _logger.Log("Title: " + title.GetTitle());
        }

        @Override
        public void Visit(IParagraph paragraph) {
            _logger.Log(new StringBuilder().append("#").append(_count++).append(" paragraph: ").append(paragraph.GetText()).toString());
        }

        @Override
        public void Visit(IImage image) {
            _logger.Log(new StringBuilder().append("#").append(_count++).append(" image: ")
                    .append("w=").append(image.GetSize().Width()).append(", h=").append(image.GetSize().Height())
                    .append("name=").append(image.GetResource().Name()).toString());
        }

        private int _count = 0;
        private final ILogger _logger;
    }
}
