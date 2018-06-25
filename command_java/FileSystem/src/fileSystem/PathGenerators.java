package fileSystem;

import environment.fileSystem.IPathGenerators;

import java.io.File;
import java.io.IOException;

class PathGenerators implements IPathGenerators {
    @Override
    public String GetUniqueFileNameInDirectory(String folder) throws IOException {
        final String fileName = "resource";
        return File.createTempFile(fileName, null, new File(folder)).getPath();
    }

    @Override
    public String GetUniqueTempFolderPath() {
        return System.getProperty("java.io.tmpdir");
    }

    @Override
    public String RelativeToAbsolute(String root, String name) throws IOException {
        return new File(new StringBuilder(root).append("\\").append(name).toString()).getCanonicalPath();
    }

    @Override
    public String AbsoluteToRelative(String root, String path) {
        return new File(root).toURI().relativize(new File(path).toURI()).getPath();
    }

    @Override
    public String CurrentModulePath() {
        //return new File(MyClass.class.getProtectionDomain().getCodeSource().getLocation().toURI().getPath()).getParentFile();
        return System.getProperty("java.class.path");
    }

    @Override
    public String GetFileName(String path) {
        final File file = new File(path);
        return file.getName();
    }

    @Override
    public String ReplaceExtension(String fileName, String newExtension) {
        final int index = fileName.lastIndexOf(".");
        final String ext = fileName.substring(index);
        final String stem = fileName.substring(0, index - 1);
        return stem + ext;
    }
}
