# This script will only run on Blender because only Blender has bpy thing
import bpy
import json
import math
from mathutils import Vector, Euler

def load_json(file_path):
    """Loads JSON data from a file."""
    with open(file_path, 'r') as f:
        data = json.load(f)
    return data

def create_object_from_json(data):
    """Creates a 3D object in Blender using geometry and transform data from JSON."""
    # Build a unique object name using SemanticClassification and UUID.
    semantic = data.get("SemanticClassifications", ["Object"])[0]
    uuid = data.get("UUID", "")
    object_name = f"{semantic}_{uuid}"
    
    # Try to create geometry from VolumeBounds if available.
    volume_bounds = data.get("VolumeBounds", None)
    if volume_bounds:
        vmin = volume_bounds.get("Min", None)
        vmax = volume_bounds.get("Max", None)
        if not (vmin and vmax):
            print(f"{object_name}: Incomplete VolumeBounds.")
            return
        
        # Calculate 8 vertices for a cuboid based on the volume bounds.
        min_x, min_y, min_z = vmin
        max_x, max_y, max_z = vmax
        vertices = [
            [min_x, min_y, min_z],
            [max_x, min_y, min_z],
            [max_x, max_y, min_z],
            [min_x, max_y, min_z],
            [min_x, min_y, max_z],
            [max_x, min_y, max_z],
            [max_x, max_y, max_z],
            [min_x, max_y, max_z]
        ]
        # Standard faces for a cuboid.
        faces = [
            [0, 1, 2, 3],  # bottom
            [4, 5, 6, 7],  # top
            [0, 1, 5, 4],  # front
            [2, 3, 7, 6],  # back
            [0, 3, 7, 4],  # left
            [1, 2, 6, 5]   # right
        ]
    else:
        # Fallback to PlaneBoundary2D for 2D geometry.
        boundary = data.get("PlaneBoundary2D", None)
        if boundary:
            # Use the 2D points as vertices (with z=0).
            vertices = [[p[0], p[1], 0.0] for p in boundary]
            faces = [list(range(len(vertices)))]
        else:
            print(f"{object_name}: No valid geometry bounds found in JSON.")
            return

    # Create a new mesh from the vertices and faces.
    mesh = bpy.data.meshes.new(object_name + "_Mesh")
    mesh.from_pydata(vertices, [], faces)
    mesh.update()

    # Create an object from the mesh.
    obj = bpy.data.objects.new(object_name, mesh)
    
    # Apply Transform properties.
    transform = data.get("Transform", {})
    translation = transform.get("Translation", [0.0, 0.0, 0.0])
    rotation = transform.get("Rotation", [0.0, 0.0, 0.0])  # in degrees
    scale = transform.get("Scale", [1.0, 1.0, 1.0])
    
    # Set location using Translation (converted to a Vector).
    try:
        if len(translation) != 3:
            raise ValueError("Translation must have exactly 3 values.")
        obj.location = Vector(translation)
    except Exception as e:
        print(f"Error setting location for {object_name}: {e}")
    
    # Convert rotation from degrees to radians and assign as Euler rotation.
    try:
        rot_rad = [math.radians(angle) for angle in rotation]
        obj.rotation_euler = Euler(rot_rad, 'XYZ')
    except Exception as e:
        print(f"Error setting rotation for {object_name}: {e}")
    
    # Set object scale.
    obj.scale = Vector(scale)
    
    # Link the object to the active collection so it appears in the scene.
    bpy.context.collection.objects.link(obj)
    print(f"Created object: {object_name}")

def main():
    json_file_path = "realpath of scene.json" # Update with the actual path #r"D:\My 3d Objects Jaimin\SceneData.json"  
    data = load_json(json_file_path)
    

    if "Rooms" in data:
        for room in data["Rooms"]:
            anchors = room.get("Anchors", [])
            for anchor in anchors:
                create_object_from_json(anchor)
    elif "Anchors" in data:
        for anchor in data["Anchors"]:
            create_object_from_json(anchor)
    else:
        # Treat the dict as a single object.
        create_object_from_json(data)

    
    print("All 3D objects created successfully!")

if __name__ == "__main__":
    main()
