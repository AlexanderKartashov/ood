package document.commands;

import document.documentItem.title.IDocumentTitle;
import document.documentItem.title.IMutableDocumentTitle;
import hsitory.AbstractCommand;
import hsitory.AbstractCommandWithMemento;
import hsitory.IMemento;

class SetDocumentTitle extends AbstractCommandWithMemento {

    public SetDocumentTitle(IMutableDocumentTitle title, String newTitle) {
        super(title);

        _title = title;
        _newTitle = newTitle;
    }

    @Override
    protected void DestroyImpl() {}

    @Override
    protected void ExecuteImpl() {
        _title.SetTitle(_newTitle);
    }

    private final IMutableDocumentTitle _title;
    private final String _newTitle;
}
