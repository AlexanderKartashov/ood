package menu.action;

import document.IMutableDocument;
import document.commands.ICommandsFactory;
import document.documentItem.ItemsCollection.IDocumentItemFactory;
import document.documentItem.ItemsCollection.IMutableDocumentItemCollection;
import document.documentItem.image.IMutableImage;
import document.documentItem.image.Size;
import document.documentItem.paragraph.IMutableParagraph;
import document.documentItem.title.IMutableDocumentTitle;
import environment.ITextEncoder;
import environment.fileSystem.IFileSystem;
import hsitory.ICommand;
import hsitory.IHistory;
import menu.IAction;
import menu.IActionsParser;
import menu.ILogger;
import menu.ISupportedActions;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import java.util.Optional;

import static org.junit.jupiter.api.Assertions.assertNotNull;
import static org.mockito.Mockito.*;
import static org.mockito.Mockito.when;
import static testUtils.Utils.assertNoThrows;

class MenuCommandsFactoryTest {

    @Mock
    private IDocumentItemFactory _itemFactory;
    @Mock
    private ILogger _logger;
    @Mock
    private ICommandsFactory _commands;
    @Mock
    private IMutableDocumentItemCollection _items;
    @Mock
    private IMutableDocument _document;
    @Mock
    private IMutableDocumentTitle _title;
    @Mock
    private IMutableImage _image;
    @Mock
    private IMutableParagraph _paragraph;
    @Mock
    private ICommand _command;
    @Mock
    private IHistory _history;
    @Mock
    private IFileSystem _fs;
    @Mock
    private ISupportedActions _supportedActions;
    @Mock
    private ITextEncoder _encoder;
    private MenuCommandsFactory _factory;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.initMocks(this);
        when(_document.MutableTitle()).thenReturn(_title);
        when(_image.GetImage()).thenReturn(_image);
        when(_paragraph.GetParagraph()).thenReturn(_paragraph);
        when(_document.MutableItems()).thenReturn(_items);
        _factory = new MenuCommandsFactory(_encoder, _fs, _itemFactory, _logger, _commands, _document, _history, _supportedActions);
    }

    @Test
    void redo() {
        final IAction action = _factory.Redo();
        assertNotNull(action);
        assertNoThrows(action::Perform);

        verify(_history).Redo();
        verifyNoMoreInteractions(_history);
        verifyZeroInteractions(_itemFactory);
        verifyZeroInteractions(_logger);
        verifyZeroInteractions(_supportedActions);
        verifyZeroInteractions(_document);
        verifyZeroInteractions(_commands);
        verifyZeroInteractions(_items);
        verifyZeroInteractions(_title);
        verifyZeroInteractions(_image);
        verifyZeroInteractions(_paragraph);
        verifyZeroInteractions(_command);
    }

    @Test
    void undo() {
        final IAction action = _factory.Undo();
        assertNotNull(action);
        assertNoThrows(action::Perform);

        verify(_history).Undo();
        verifyNoMoreInteractions(_history);
        verifyZeroInteractions(_itemFactory);
        verifyZeroInteractions(_logger);
        verifyZeroInteractions(_supportedActions);
        verifyZeroInteractions(_document);
        verifyZeroInteractions(_commands);
        verifyZeroInteractions(_items);
        verifyZeroInteractions(_title);
        verifyZeroInteractions(_image);
        verifyZeroInteractions(_paragraph);
        verifyZeroInteractions(_command);
    }

    /*@Test
    void list() {
        final IAction action = _factory.List();
        assertNotNull(action);
        assertNoThrows(action::Perform);

        verifyZeroInteractions(_history);
        verifyZeroInteractions(_itemFactory);
        verifyZeroInteractions(_logger);
        verifyZeroInteractions(_supportedActions);
        verifyZeroInteractions(_document);
        verifyZeroInteractions(_commands);
        verifyZeroInteractions(_items);
        verifyZeroInteractions(_title);
        verifyZeroInteractions(_image);
        verifyZeroInteractions(_paragraph);
        verifyZeroInteractions(_command);
    }*/

    @Test
    void changeText() {
        final String text = "text";
        when(_items.GetItem(1)).thenReturn(_paragraph);
        when(_commands.EditText(_paragraph, text)).thenReturn(_command);
        final IAction action = _factory.ChangeText(1, text);
        assertNotNull(action);
        assertNoThrows(action::Perform);

        assertNoThrows(() -> verify(_history).AddCommand(_command));
        verifyNoMoreInteractions(_history);
        verifyZeroInteractions(_itemFactory);
        verifyZeroInteractions(_logger);
        verifyZeroInteractions(_supportedActions);
        verify(_document).MutableItems();
        verifyNoMoreInteractions(_document);
        verify(_commands).EditText(_paragraph, text);
        verifyNoMoreInteractions(_commands);
        verify(_items).GetItem(1);
        verifyNoMoreInteractions(_items);
        verifyZeroInteractions(_title);
        verifyZeroInteractions(_image);
        verify(_paragraph).GetParagraph();
        verifyNoMoreInteractions(_paragraph);
        verify(_command).Execute();
        verifyNoMoreInteractions(_command);
    }

    @Test
    void delete() {
        final IAction action = _factory.Delete(1);
        when(_items.GetItem(1)).thenReturn(_paragraph);
        when(_commands.DeleteItem(_items, 1)).thenReturn(_command);
        assertNotNull(action);
        assertNoThrows(action::Perform);

        assertNoThrows(() -> verify(_history).AddCommand(_command));
        verifyNoMoreInteractions(_history);
        verifyZeroInteractions(_itemFactory);
        verifyZeroInteractions(_logger);
        verifyZeroInteractions(_supportedActions);
        verify(_document).MutableItems();
        verifyNoMoreInteractions(_document);
        verify(_commands).DeleteItem(_items, 1);
        verifyNoMoreInteractions(_commands);
        verifyZeroInteractions(_items);
        verifyZeroInteractions(_title);
        verifyZeroInteractions(_image);
        verifyZeroInteractions(_paragraph);
        verify(_command).Execute();
        verifyNoMoreInteractions(_command);
    }

    @Test
    void insertImage() {
        final Size size = new Size(1, 2);
        final String path = "path";
        assertNoThrows(()->when(_itemFactory.CreateImage(path, size)).thenReturn(_image));
        when(_commands.InsertItem(_items, _image, Optional.empty())).thenReturn(_command);
        final IAction action = _factory.InsertImage(Optional.empty(), size, path);
        assertNotNull(action);
        assertNoThrows(action::Perform);

        assertNoThrows(() -> verify(_history).AddCommand(_command));
        verifyNoMoreInteractions(_history);
        assertNoThrows(()->verify(_itemFactory).CreateImage(path, size));
        verifyNoMoreInteractions(_itemFactory);
        verifyZeroInteractions(_logger);
        verifyZeroInteractions(_supportedActions);
        verify(_document).MutableItems();
        verifyNoMoreInteractions(_document);
        verify(_commands).InsertItem(_items, _image, Optional.empty());
        verifyNoMoreInteractions(_commands);
        verifyZeroInteractions(_items);
        verifyZeroInteractions(_title);
        verifyZeroInteractions(_image);
        verifyZeroInteractions(_paragraph);
        verify(_command).Execute();
        verifyNoMoreInteractions(_command);
    }

    @Test
    void insertParagraph() {
        final String text = "text";
        when(_itemFactory.CreateParagraph(text)).thenReturn(_paragraph);
        when(_commands.InsertItem(_items, _paragraph, Optional.empty())).thenReturn(_command);
        final IAction action = _factory.InsertParagraph(Optional.empty(), text);
        assertNotNull(action);
        assertNoThrows(action::Perform);

        assertNoThrows(() -> verify(_history).AddCommand(_command));
        verifyNoMoreInteractions(_history);
        verify(_itemFactory).CreateParagraph(text);
        verifyNoMoreInteractions(_itemFactory);
        verifyZeroInteractions(_logger);
        verifyZeroInteractions(_supportedActions);
        verify(_document).MutableItems();
        verifyNoMoreInteractions(_document);
        verify(_commands).InsertItem(_items, _paragraph, Optional.empty());
        verifyNoMoreInteractions(_commands);
        verifyZeroInteractions(_items);
        verifyZeroInteractions(_title);
        verifyZeroInteractions(_image);
        verifyZeroInteractions(_paragraph);
        verify(_command).Execute();
        verifyNoMoreInteractions(_command);
    }

    @Test
    void resizeImage() {
        final Size size = new Size(1, 2);
        when(_items.GetItem(1)).thenReturn(_image);
        when(_commands.ResizeImage(_image, size)).thenReturn(_command);
        final IAction action = _factory.ResizeImage(1, size);
        assertNotNull(action);
        assertNoThrows(action::Perform);

        assertNoThrows(() -> verify(_history).AddCommand(_command));
        verifyNoMoreInteractions(_history);
        verifyZeroInteractions(_itemFactory);
        verifyZeroInteractions(_logger);
        verifyZeroInteractions(_supportedActions);
        verify(_items).GetItem(1);
        verifyNoMoreInteractions(_items);
        verifyZeroInteractions(_title);
        verify(_image).GetImage();
        verifyNoMoreInteractions(_image);
        verifyZeroInteractions(_paragraph);
        verify(_document).MutableItems();
        verifyNoMoreInteractions(_document);
        verify(_commands).ResizeImage(_image, size);
        verifyNoMoreInteractions(_commands);
        verify(_command).Execute();
        verifyNoMoreInteractions(_command);
    }

    /*@Test
    void save() {
        final String path = "path";
        final IAction action = _factory.Save(path);
        assertNotNull(action);
        assertNoThrows(action::Perform);

        verifyZeroInteractions(_history);
        verifyZeroInteractions(_itemFactory);
        verifyZeroInteractions(_logger);
        verifyZeroInteractions(_supportedActions);
        verifyZeroInteractions(_document);
        verifyZeroInteractions(_commands);
        verifyZeroInteractions(_items);
        verifyZeroInteractions(_title);
        verifyZeroInteractions(_image);
        verifyZeroInteractions(_paragraph);
        verifyZeroInteractions(_command);
    }*/

    @Test
    void setTitle() {
        final String title = "title";
        final IAction action = _factory.SetTitle(title);
        when(_commands.SetTitle(_title, title)).thenReturn(_command);
        assertNotNull(action);
        assertNoThrows(action::Perform);

        assertNoThrows(() -> verify(_history).AddCommand(_command));
        verifyNoMoreInteractions(_history);
        verifyZeroInteractions(_itemFactory);
        verifyZeroInteractions(_logger);
        verifyZeroInteractions(_supportedActions);
        verifyZeroInteractions(_items);
        verifyZeroInteractions(_title);
        verifyZeroInteractions(_image);
        verifyZeroInteractions(_paragraph);
        verify(_document).MutableTitle();
        verifyNoMoreInteractions(_document);
        verify(_commands).SetTitle(_title, title);
        verifyNoMoreInteractions(_commands);
        verify(_command).Execute();
        verifyNoMoreInteractions(_command);
    }
}