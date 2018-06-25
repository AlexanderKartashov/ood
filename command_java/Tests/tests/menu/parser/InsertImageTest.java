package menu.parser;

import document.documentItem.image.Size;
import menu.action.IMenuCommandsFactory;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.ValueSource;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.argThat;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.verifyNoMoreInteractions;
import static org.mockito.Mockito.verifyZeroInteractions;
import static testUtils.Utils.assertNoThrows;

class InsertImageTest {

    private InsertImage _parser;
    @Mock
    private IMenuCommandsFactory _factory;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.initMocks(this);
        _parser = new InsertImage(_factory);
    }

    @ParameterizedTest
    @ValueSource(strings = { "", "test", "a", "1a", " 2 ", "1 ", "InsertImage a", "InsertImage 1 w", "InsertImage 1 2 h path", "InsertImage 1 2 3", " InsertImage 1 2 3 path", " InsertImage end 1 2 path"})
    void invalidCommandNoThrow(String text) {
        assertNoThrows(() -> assertNull(_parser.Parse(text)));
        verifyZeroInteractions(_factory);
    }

    @ParameterizedTest
    @ValueSource(strings = { "InsertImage 1 2 3 path" })
    void validCommandInsert(String text) {
        assertNoThrows(() -> _parser.Parse(text));
        verify(_factory).InsertImage(
                argThat((pos) -> pos.isPresent() && pos.get() == 1),
                argThat((sz) -> sz.Height() == 3 && sz.Width() == 2),
                argThat((path)->path.equals("path")));
        verifyNoMoreInteractions(_factory);
    }

    @ParameterizedTest
    @ValueSource(strings = { "InsertImage end 1 2 path" })
    void validCommandPush(String text) {
        assertNoThrows(() -> _parser.Parse(text));
        verify(_factory).InsertImage(
                argThat((pos) -> !pos.isPresent()),
                argThat((sz) -> sz.Height() == 2 && sz.Width() == 1),
                argThat((path)->path.equals("path")));
        verifyNoMoreInteractions(_factory);
    }
}