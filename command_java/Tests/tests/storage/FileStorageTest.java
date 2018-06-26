package storage;

import environment.fileSystem.*;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;

import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.ArgumentMatchers.anyString;
import static org.mockito.Mockito.*;
import static testUtils.Utils.assertNoThrows;

class FileStorageTest {

    @Mock
    private IFileSystem _fs;
    @Mock
    private IPathOperations _path;
    @Mock
    private IPathGenerators _pathGenerators;
    @Mock
    private IPathChecks _pathChecks;
    @Mock
    private IFileOperations _files;
    @Mock
    private IDirectoryOperations _directory;
    @Mock
    private IResourcesFactory _resources;
    private String _tempFolder;
    private FileStorage _storage;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.initMocks(this);
        when(_fs.PathOperations()).thenReturn(_path);
        when(_fs.FileOperations()).thenReturn(_files);
        when(_fs.DirectoryOperations()).thenReturn(_directory);
        when(_path.Checks()).thenReturn(_pathChecks);
        when(_path.Generators()).thenReturn(_pathGenerators);
        when(_pathChecks.PathExists(anyString())).thenReturn(false);
        _tempFolder = "TempFolder";
        assertNoThrows(()->when(_pathGenerators.GetUniqueTempFolderPath()).thenReturn(_tempFolder));

        assertNoThrows(() -> { _storage = new FileStorage(_fs, _resources); });
    }

    @Test
    void constructor() {
        assertNoThrows(()->verify(_pathGenerators).GetUniqueTempFolderPath());
        verify(_pathChecks).PathExists(_tempFolder);
        assertNoThrows(()-> verify(_directory).Create(_tempFolder));
    }

    @Test
    void constructorThrowsException() {
        when(_pathChecks.PathExists(_tempFolder)).thenReturn(true);
        assertThrows(IllegalArgumentException.class, () -> { _storage = new FileStorage(_fs, _resources); });
    }

    @Test
    void delete() {
        final String name = "resource";
        final String path = "resourcePath";
        final String absPath = "absResourcePath";
        final String newPath = "newResourcePath";
        final IResource res = mock(IResource.class);
        when(res.Path()).thenReturn(newPath);

        when(_resources.CreateResource(name, newPath, _storage)).thenReturn(res);
        assertNoThrows(()->when(_pathGenerators.RelativeToAbsolute(_tempFolder, name)).thenReturn(newPath));
        when(_pathChecks.IsAbsolute(path)).thenReturn(true);
        assertNoThrows(()->when(_pathGenerators.GetUniqueFileNameInDirectory(_tempFolder)).thenReturn(name));

        assertNoThrows(()-> _storage.Add(path) );
        assertNoThrows(() -> _storage.Delete(name));

        assertNoThrows(()->verify(_files).Delete(newPath));
    }

    @Test
    void close() {
        final IResource res1 = mock(IResource.class);
        final String name1 = "resource1";
        final String path1 = "resourcePath1";
        final String newPath1 = "newResourcePath1";
        when(res1.Path()).thenReturn(newPath1);
        when(_resources.CreateResource(name1, newPath1, _storage)).thenReturn(res1);
        assertNoThrows(()->when(_pathGenerators.RelativeToAbsolute(_tempFolder, name1)).thenReturn(newPath1));
        when(_pathChecks.IsAbsolute(path1)).thenReturn(true);
        final IResource res2 = mock(IResource.class);
        final String name2 = "resource2";
        final String path2 = "resourcePath2";
        final String newPath2 = "newResourcePath2";
        when(res2.Path()).thenReturn(newPath2);
        when(_resources.CreateResource(name2, newPath2, _storage)).thenReturn(res2);
        assertNoThrows(()->when(_pathGenerators.RelativeToAbsolute(_tempFolder, name2)).thenReturn(newPath2));
        when(_pathChecks.IsAbsolute(path2)).thenReturn(true);
        when(_pathChecks.PathExists(_tempFolder)).thenReturn(true);
        assertNoThrows(()->when(_pathGenerators.GetUniqueFileNameInDirectory(_tempFolder)).thenReturn(name1).thenReturn(name2));

        assertNoThrows(()-> _storage.Add(path1) );
        assertNoThrows(()-> _storage.Add(path2) );
        assertNoThrows(()-> _storage.close() );

        assertNoThrows(()->verify(_files).Copy(path1, newPath1));
        assertNoThrows(()->verify(_files).Copy(path2, newPath2));
        assertNoThrows(()->verify(_files).Delete(newPath1));
        assertNoThrows(()->verify(_files).Delete(newPath2));
        assertNoThrows(()->verify(_directory).Delete(_tempFolder));
    }

    @Test
    void addAbsolute() {
        final String name = "resource";
        final String absPath = "absResourcePath";
        final String newPath = "newResourcePath";
        final IResource res = mock(IResource.class);

        when(_resources.CreateResource(name, newPath, _storage)).thenReturn(res);
        assertNoThrows(()->when(_pathGenerators.RelativeToAbsolute(_tempFolder, name)).thenReturn(newPath));
        when(_pathChecks.IsAbsolute(absPath)).thenReturn(true);
        assertNoThrows(()->when(_pathGenerators.GetUniqueFileNameInDirectory(_tempFolder)).thenReturn(name));

        assertNoThrows(()-> _storage.Add(absPath) );

        assertNoThrows(()->verify(_files).Copy(absPath, newPath));
        assertNoThrows(()->verify(_pathGenerators).RelativeToAbsolute(_tempFolder, name));
        assertNoThrows(()->verify(_resources).CreateResource(name, newPath, _storage));
        verify(_pathChecks).IsAbsolute(absPath);
        assertNoThrows(()->verify(_pathGenerators).GetUniqueFileNameInDirectory(_tempFolder));
    }

    @Test
    void addRelative() {
        final String name = "resource";
        final String path = "resourcePath";
        final String absPath = "absResourcePath";
        final String modulePath = "modulePath";
        final String newPath = "newResourcePath";
        final IResource res = mock(IResource.class);

        when(_resources.CreateResource(name, newPath, _storage)).thenReturn(res);
        assertNoThrows(()->when(_pathGenerators.RelativeToAbsolute(modulePath, path)).thenReturn(absPath));
        assertNoThrows(()->when(_pathGenerators.RelativeToAbsolute(_tempFolder, name)).thenReturn(newPath));
        when(_pathGenerators.CurrentModulePath()).thenReturn(modulePath);
        when(_pathChecks.IsAbsolute(path)).thenReturn(false);
        assertNoThrows(()->when(_pathGenerators.GetUniqueFileNameInDirectory(_tempFolder)).thenReturn(name));

        assertNoThrows(()-> _storage.Add(path) );

        assertNoThrows(()->verify(_files).Copy(absPath, newPath));
        assertNoThrows(()->verify(_pathGenerators).RelativeToAbsolute(modulePath, path));
        assertNoThrows(()->verify(_pathGenerators).RelativeToAbsolute(_tempFolder, name));
        verify(_resources).CreateResource(name, newPath, _storage);
        verify(_pathGenerators).CurrentModulePath();
        verify(_pathChecks).IsAbsolute(path);
        assertNoThrows(()->verify(_pathGenerators).GetUniqueFileNameInDirectory(_tempFolder));
    }
}