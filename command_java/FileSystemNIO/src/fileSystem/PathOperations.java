package fileSystem;

import environment.fileSystem.IPathChecks;
import environment.fileSystem.IPathGenerators;
import environment.fileSystem.IPathOperations;

class PathOperations implements IPathOperations {
    @Override
    public IPathChecks Checks() {
        return new PathCheck();
    }

    @Override
    public IPathGenerators Generators() {
        return new PathGenerators();
    }
}
