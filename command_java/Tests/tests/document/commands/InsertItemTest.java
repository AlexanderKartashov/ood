package document.commands;

import document.documentItem.ItemsCollection.IDocumentItem;
import document.documentItem.ItemsCollection.IMutableDocumentItemCollection;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import java.util.Optional;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.never;
import static org.mockito.Mockito.verify;
import static testUtils.Utils.assertNoThrows;

class InsertItemTest {

    private InsertItem _command;
    @Mock
    private IMutableDocumentItemCollection _collection;
    @Mock
    private IDocumentItem _newItem;
    private int _position;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.initMocks(this);
        _position = 1;
        _command = new InsertItem(_collection, _newItem, Optional.ofNullable(_position));
    }

    @Test
    void destroyExecuted() {
        assertNoThrows(_command::Execute);
        assertNoThrows(_command::Destroy);
        verify(_collection, never()).GetSize();
        assertNoThrows(() -> verify(_newItem, never()).close());
    }

    @Test
    void destroyUnExecuted() {
        assertNoThrows(_command::Execute);
        assertNoThrows(_command::UnExecute);
        assertNoThrows(_command::Destroy);
        verify(_collection, never()).GetSize();
        assertNoThrows(() -> verify(_newItem).close());
    }

    @Test
    void destroyNotExecuted() {
        assertNoThrows(_command::Destroy);
        verify(_collection, never()).GetSize();
        assertNoThrows(() -> verify(_newItem).close());
    }

    @Test
    void unExecute() {
        assertNoThrows(_command::Execute);
        assertNoThrows(_command::UnExecute);
        verify(_collection, never()).GetSize();
        verify(_collection).DeleteItem(_position);
    }

    @Test
    void execute() {
        assertNoThrows(_command::Execute);
        assertNoThrows(_command::UnExecute);
        verify(_collection, never()).GetSize();
        verify(_collection).InsertItem(_newItem, _position);
    }
}