package document.commands;

import document.documentItem.image.IMutableImage;
import document.documentItem.image.Size;
import hsitory.IMemento;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.MockitoAnnotations;

import static org.junit.jupiter.api.Assertions.assertThrows;
import static org.mockito.ArgumentMatchers.any;
import static org.mockito.ArgumentMatchers.anyString;
import static org.mockito.ArgumentMatchers.argThat;
import static org.mockito.Mockito.never;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.when;
import static testUtils.Utils.assertNoThrows;

class ResizeImageTest {

    private ResizeImage _command;
    @Mock
    private IMutableImage _image;
    @Mock
    private IMemento _memento;
    private Size _newSize;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.initMocks(this);
        _newSize = new Size(100, 100);
        when(_image.GetState()).thenReturn(_memento);
        _command = new ResizeImage(_image, _newSize);
    }

    @Test
    void destroy() {
        assertNoThrows(_command::Destroy);
        verify(_image, never()).SetSize(any());
        verify(_memento, never()).Restore();
    }

    @Test
    void unExecute(){
        assertThrows(IllegalStateException.class, _command::UnExecute);
        assertNoThrows(_command::Execute);

        Mockito.reset(_image);
        Mockito.reset(_memento);

        assertNoThrows(_command::UnExecute);
        verify(_image, never()).SetSize(any());
        verify(_memento).Restore();
    }

    @Test
    void execute() {
        assertNoThrows(_command::Execute);
        verify(_image).SetSize(_newSize);
        verify(_memento, never()).Restore();

        assertThrows(IllegalStateException.class, _command::Execute);
    }
}