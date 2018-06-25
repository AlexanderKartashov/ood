package document.documentItem.paragraph;

import hsitory.IMemento;

public class ParagraphMemento implements IMemento {

    public ParagraphMemento(String oldText, IMutableParagraph paragraph) {
        _paragraph = paragraph;
        _oldText = oldText;
    }

    @Override
    public void Restore() {
        _paragraph.SetText(_oldText);
    }

    private final IMutableParagraph _paragraph;
    private final String _oldText;
}
