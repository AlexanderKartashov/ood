package hsitory;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mockito;

import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.mockito.Mockito.*;
import static testUtils.Utils.assertNoThrows;

class HistoryTest {

    private History _history;

    @BeforeEach
    void setUp() {
        _history = new History(3);
    }

    @Test
    void addUnExecutedCommand() {
        ICommand command = mock(ICommand.class);
        assertThrows(IllegalArgumentException.class, ()-> _history.AddCommand(command));
    }

    @Test
    void addExecutedCommand() {
        ICommand command = mock(ICommand.class);
        when(command.IsExecuted()).thenReturn(true);
        assertNoThrows(()-> _history.AddCommand(command));
    }

    @Test
    void addExtraCommandsRemoveOld() {
        ICommand command1 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        ICommand command2 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        ICommand command3 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        assertNoThrows(()-> _history.AddCommand(command1));
        assertNoThrows(()-> _history.AddCommand(command2));
        assertNoThrows(()-> _history.AddCommand(command3));

        ICommand command4 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        assertNoThrows(()-> _history.AddCommand(command4));

        assertNoThrows(() -> verify(command1).Destroy());
    }

    @Test
    void addCommandAndRemoveUnExecuted() {
        ICommand command1 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        ICommand command2 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        ICommand command3 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        assertNoThrows(()-> _history.AddCommand(command1));
        assertNoThrows(()-> _history.AddCommand(command2));
        assertNoThrows(()-> _history.AddCommand(command3));

        assertNoThrows(_history::Undo);
        assertNoThrows(_history::Undo);

        ICommand command4 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        assertNoThrows(()-> _history.AddCommand(command4));

        assertNoThrows(() -> verify(command2).Destroy());
        assertNoThrows(() -> verify(command3).Destroy());
    }

    @Test
    void undo() {
        ICommand command1 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        assertNoThrows(()-> _history.AddCommand(command1));
        assertNoThrows(_history::Undo);
        verify(command1).UnExecute();
    }

    @Test
    void undoNothing() {
        assertThrows(IllegalStateException.class, _history::Undo);
    }

    @Test
    void undoAllCommandsAlreadyUndo() {
        ICommand command1 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        assertNoThrows(()-> _history.AddCommand(command1));
        assertNoThrows(_history::Undo);
        assertThrows(IllegalStateException.class, _history::Undo);
    }

    @Test
    void redo() {
        ICommand command1 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        assertNoThrows(()-> _history.AddCommand(command1));
        assertNoThrows(_history::Undo);
        assertNoThrows(_history::Redo);
        verify(command1).Execute();
    }

    @Test
    void redoNothing() {
        assertThrows(IllegalStateException.class, _history::Redo);
    }

    @Test
    void redoAllCommandsAlreadyRedo() {
        ICommand command1 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        assertThrows(IllegalStateException.class, _history::Redo);
    }

    @Test
    void close() {
        ICommand command1 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        ICommand command2 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        ICommand command3 = when(mock(ICommand.class).IsExecuted()).thenReturn(true).getMock();
        assertNoThrows(()-> _history.AddCommand(command1));
        assertNoThrows(()-> _history.AddCommand(command2));
        assertNoThrows(()-> _history.AddCommand(command3));

        assertNoThrows(_history::close);

        assertNoThrows(()-> verify(command1).Destroy());
        assertNoThrows(()-> verify(command2).Destroy());
        assertNoThrows(()-> verify(command3).Destroy());
    }
}