package document.commands;

import document.documentItem.paragraph.IMutableParagraph;
import hsitory.IMemento;
import org.junit.jupiter.api.*;
import org.mockito.*;
import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.mockito.Mockito.*;
import static testUtils.Utils.assertNoThrows;


class EditTextTest {

    private EditText _command;
    @Mock
    private IMutableParagraph _paragraph;
    @Mock
    private IMemento _memento;
    private String _newText;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.initMocks(this);
        _newText = "newText";
        when(_paragraph.GetState()).thenReturn(_memento);
        _command = new EditText(_paragraph, _newText);
    }

    @Test
    void unExecute() {
        assertThrows(IllegalStateException.class, _command::UnExecute);
        assertNoThrows(_command::Execute);

        Mockito.reset(_paragraph);
        Mockito.reset(_memento);

        assertNoThrows(_command::UnExecute);
        verify(_paragraph, never()).SetText(anyString());
        verify(_memento).Restore();
    }

    @Test
    void destroy() {
        assertNoThrows(_command::Destroy);
        verify(_paragraph, never()).SetText(anyString());
        verify(_memento, never()).Restore();
    }

    @Test
    void execute() {
        assertNoThrows(_command::Execute);
        verify(_paragraph).SetText(_newText);
        verify(_memento, never()).Restore();

        assertThrows(IllegalStateException.class, _command::Execute);
    }
}