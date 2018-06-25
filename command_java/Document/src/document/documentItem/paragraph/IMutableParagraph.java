package document.documentItem.paragraph;

import hsitory.IMementoOriginator;

public interface IMutableParagraph extends IParagraph, IMementoOriginator {
    void SetText(String text);
}
