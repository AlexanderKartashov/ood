package document.commands;

import document.documentItem.title.IMutableDocumentTitle;
import hsitory.IMemento;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.MockitoAnnotations;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.anyString;
import static org.mockito.Mockito.never;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;
import static testUtils.Utils.assertNoThrows;

class SetDocumentTitleTest {

    @Mock
    private IMutableDocumentTitle _title;
    @Mock
    private IMemento _memento;
    private SetDocumentTitle _command;
    private String _newText;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.initMocks(this);
        _newText = "newText";
        when(_title.GetState()).thenReturn(_memento);
        _command = new SetDocumentTitle(_title, _newText);
    }

    @Test
    void destroy() {
        assertNoThrows(_command::Destroy);
        verify(_title, never()).SetTitle(anyString());
        verify(_memento, never()).Restore();
    }

    @Test
    void execute() {
        assertNoThrows(_command::Execute);
        verify(_title).SetTitle(_newText);
        verify(_memento, never()).Restore();

        assertThrows(IllegalStateException.class, _command::Execute);
    }

    @Test
    void unExecute(){
        assertThrows(IllegalStateException.class, _command::UnExecute);
        assertNoThrows(_command::Execute);

        Mockito.reset(_title);
        Mockito.reset(_memento);

        assertNoThrows(_command::UnExecute);
        verify(_title, never()).SetTitle(anyString());
        verify(_memento).Restore();
    }
}