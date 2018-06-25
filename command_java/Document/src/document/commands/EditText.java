package document.commands;

import document.documentItem.paragraph.IMutableParagraph;
import hsitory.AbstractCommand;
import hsitory.AbstractCommandWithMemento;
import hsitory.IMemento;

class EditText extends AbstractCommandWithMemento {

    public EditText(IMutableParagraph paragraph, String newText)
    {
        super(paragraph);

        _newText = newText;
        _paragraph = paragraph;
    }

    @Override
    protected void DestroyImpl() {
        // do nothing
    }

    @Override
    protected void ExecuteImpl() { _paragraph.SetText(_newText); }

    private final String _newText;
    private final IMutableParagraph _paragraph;
    private  IMemento _memento;
}
