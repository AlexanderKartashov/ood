package environment.fileSystem;

import java.io.IOException;

public class TempDirectory implements AutoCloseable {

    public TempDirectory(IFileSystem fs, String path) throws IOException {
        _fs = fs;
        _path = path;

        if (!_fs.PathOperations().Checks().PathExists(path)) {
            _fs.DirectoryOperations().Create(path);
        }
        else {
            throw new IllegalArgumentException("can't create new temp folder");
        }
    }

    public String GetPath(){
        return _path;
    }

    @Override
    public void close() throws Exception {
        if (_fs.PathOperations().Checks().PathExists(_path))
        {
            _fs.DirectoryOperations().Delete(_path);
        }
    }

    private final IFileSystem _fs;
    private final String _path;
}
