package menu;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import sun.util.resources.cldr.ne.CurrencyNames_ne;

import java.io.IOException;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.*;
import static testUtils.Utils.assertNoThrows;

class MenuTest {

    private Menu _menu;
    @Mock
    private IActionSource _actions;
    @Mock
    private IErrorHandler _errors;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.initMocks(this);

        _menu = new Menu(_errors);
    }

    @Test
    void runWithError() throws IOException {
        when(_actions.HasMoreActions()).thenReturn(true).thenReturn(false);
        final Throwable err = new IllegalArgumentException("failed");
        when(_actions.GetNextAction()).thenThrow(err);
        assertNoThrows(()->_menu.Run(_actions));
        verify(_errors).OnError(err);
    }

    @Test
    void run() throws Exception {
        when(_actions.HasMoreActions()).thenReturn(true).thenReturn(false);
        final IAction action = mock(IAction.class);
        when(_actions.GetNextAction()).thenReturn(action);

        assertNoThrows(()->_menu.Run(_actions));

        verify(action).Perform();
        verifyZeroInteractions(_errors);
    }
}