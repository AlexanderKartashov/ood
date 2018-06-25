package document.documentItem.title;

import hsitory.IMemento;

public class DocumentTitleMemento implements IMemento {

    public DocumentTitleMemento(String oldTitle, IMutableDocumentTitle documentTitle) {
        _oldTitle = oldTitle;
        _documentTitle = documentTitle;
    }

    @Override
    public void Restore() {
        _documentTitle.SetTitle(_oldTitle);
    }

    private final String _oldTitle;
    private final IMutableDocumentTitle _documentTitle;
}
