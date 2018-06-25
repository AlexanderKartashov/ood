package menu.parser;

import menu.action.IMenuCommandsFactory;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.ValueSource;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.argThat;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.verifyNoMoreInteractions;
import static org.mockito.Mockito.verifyZeroInteractions;
import static testUtils.Utils.assertNoThrows;

class InsertParagraphTest {

    private InsertParagraph _parser;
    @Mock
    private IMenuCommandsFactory _factory;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.initMocks(this);
        _parser = new InsertParagraph(_factory);
    }

    @ParameterizedTest
    @ValueSource(strings = { "", "test", "a", "1a", " 2 ", "1 ", "InsertParagraph a", "InsertParagraph 1", " InsertParagraph end t"})
    void invalidCommandNoThrow(String text) {
        assertNoThrows(() -> assertNull(_parser.Parse(text)));
        verifyZeroInteractions(_factory);
    }

    @ParameterizedTest
    @ValueSource(strings = { "InsertParagraph 1 text" })
    void validCommandInsert(String text) {
        assertNoThrows(() -> _parser.Parse(text));
        verify(_factory).InsertParagraph(
                argThat((pos) -> pos.isPresent() && pos.get() == 1),
                argThat((path)->path.equals("text")));
        verifyNoMoreInteractions(_factory);
    }

    @ParameterizedTest
    @ValueSource(strings = { "InsertParagraph end text" })
    void validCommandPush(String text) {
        assertNoThrows(() -> _parser.Parse(text));
        verify(_factory).InsertParagraph(
                argThat((pos) -> !pos.isPresent()),
                argThat((path)->path.equals("text")));
        verifyNoMoreInteractions(_factory);
    }
}