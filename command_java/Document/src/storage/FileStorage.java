package storage;

import environment.fileSystem.IFileSystem;
import environment.fileSystem.TempDirectory;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

public class FileStorage implements IFileStorage {

    public FileStorage(IFileSystem fs, IResourcesFactory resFactory) throws IOException {
        _fs = fs;
        _resFactory = resFactory;
        _tempDir = new TempDirectory(_fs, _fs.PathOperations().Generators().GetUniqueTempFolderPath());
    }

    @Override
    public void Delete(String name) throws IOException {
        if (_map.containsKey(name))
        {
            _fs.FileOperations().Delete(_map.get(name).Path());
            _map.remove(name);
        }
    }

    @Override
    public void close() throws Exception {
        for (IResource res : _map.values()){
            _fs.FileOperations().Delete(res.Path());
        }
        _map.clear();
        _tempDir.close();
    }

    @Override
    public IResource Add(String path) throws IOException {
        final String name = _fs.PathOperations().Generators().GetUniqueFileNameInDirectory(_tempDir.GetPath());
        final String newPath = _fs.PathOperations().Generators().RelativeToAbsolute(_tempDir.GetPath(), name);

        String absPath = path;
        if (!_fs.PathOperations().Checks().IsAbsolute(path))
        {
            absPath = _fs.PathOperations().Generators().RelativeToAbsolute(
                       _fs.PathOperations().Generators().CurrentModulePath(),
                        path
                );
        }
        _fs.FileOperations().Copy(absPath, newPath);

        final IResource res = _resFactory.CreateResource(name, newPath, this);
        _map.put(name, res);

        return res;
    }

    private final TempDirectory _tempDir;
    private final Map<String, IResource> _map = new HashMap<>();
    private final IFileSystem _fs;
    private final IResourcesFactory _resFactory;
}
