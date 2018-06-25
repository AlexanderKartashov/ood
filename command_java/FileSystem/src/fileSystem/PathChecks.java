package fileSystem;

import environment.fileSystem.IPathChecks;

import java.io.File;

class PathChecks implements IPathChecks {
    @Override
    public boolean PathExists(String path) {
        return new File(path).exists();
    }

    @Override
    public boolean IsFile(String path) {
         return new File(path).isFile();
    }

    @Override
    public boolean IsDirectory(String path) {
        return new File(path).isDirectory();
    }

    @Override
    public boolean IsAbsolute(String path) {
        return new File(path).isAbsolute();
    }
}
