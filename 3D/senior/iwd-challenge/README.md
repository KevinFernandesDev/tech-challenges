# IWD Senior 3D developer challenge

## Content

The project contains one scene in *Assets/Content/Scenes* called main.unity.
This scene is already setup for easy inspection of the challenge.

The scripts are under the folder *Assets/Scripts/* and are named :
- AbstractCameraFocus.cs
- CameraFocus.cs
- DebugCameraFocus.cs
- *Extensions*/BoundsExtensions.cs

## Guidelines

The scene contains a camera with 2 scripts :
*CameraFocus* & *DebugCameraFocus*. These scripts use the same base behaviour as expressend in the *AbstractCameraFocus* script.

### AbstractCameraFocus
This is the base class of the other two camera scripts. It is set as an *abstract* class and and every method are set as *virtual* so as to obey the SOLID Open/Close principle.

### DebugCameraFocus
When the scene is opened, the script *DebugCameraFocus* is totally active as seen by its properties **IsEditorDebugEnabled** & **IsEditorDebugGizmosEnabled**.

The *DebugCameraFocus* is designed to help see, in the editor viewport, the behaviour of the underlying camera as defined in the *AbstractCameraFocus* from which it is derived. This script is automatically disabled while in play mode for performance reasons.

While **IsEditorDebugCameraActive** is enabled, any change to the camera position, rotation, fov, and game view aspect ratio will automatically recalculate the best position.

**IsEditorDebugGizmosEnabled** helps see the Bounds of the group of objects and some helper lines in the editor view to potentially debug future changes (and it helped for creating the behaviour in the first place).

### CameraFocus

The *CameraFocus*  is the script run while in play mode. It gets its behaviour from the *AbstractCameraFocus* from which it is derived but can later override this behaviour.

This script only calculates the best distance to the objects **once** in its initialization for performance reasons but this can be easily modified.

To try this script in play mode, you should disable at least the **IsEditorDebugGizmosEnabled** property of the *DebugCameraFocus*. This way you can place the camera anywhere in the scene, add/remove objects and then press play to check the behaviour.

### BoundsExtensions

This script contains a set of functions related to the Bounds class which are helpful for this challenge, but also probably for other uses.