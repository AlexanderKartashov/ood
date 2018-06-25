package fileSystem;

import environment.fileSystem.IPathChecks;

import java.nio.file.Paths;

class PathCheck implements IPathChecks {
    @Override
    public boolean PathExists(String path) {
        return Paths.get(path).toFile().exists();
    }

    @Override
    public boolean IsFile(String path) {
        return Paths.get(path).toFile().isFile();
    }

    @Override
    public boolean IsDirectory(String path) {
        return Paths.get(path).toFile().isDirectory();
    }

    @Override
    public boolean IsAbsolute(String path) {
        return Paths.get(path).isAbsolute();
    }
}
