package menu.parser;

import menu.action.IMenuCommandsFactory;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.ValueSource;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.verify;
import static org.mockito.Mockito.verifyNoMoreInteractions;
import static org.mockito.Mockito.verifyZeroInteractions;
import static testUtils.Utils.assertNoThrows;

class SaveTest {

    private Save _parser;
    @Mock
    private IMenuCommandsFactory _factory;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.initMocks(this);
        _parser = new Save(_factory);
    }

    @ParameterizedTest
    @ValueSource(strings = { "", "test", "a", "1a", " 2 ", "1 ", " Save 1 text", " Save end ", "Save"})
    void invalidCommandNoThrow(String text) {
        assertNoThrows(() -> assertNull(_parser.Parse(text)));
        verifyZeroInteractions(_factory);
    }

    @ParameterizedTest
    @ValueSource(strings = { "Save path" })
    void validCommandNoThrow(String text) {
        assertNoThrows(() -> _parser.Parse(text));
        verify(_factory).Save("path");
        verifyNoMoreInteractions(_factory);
    }
}