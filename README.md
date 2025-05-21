# DescriptiveMapPoints Add-in for ArcGIS Pro

## Overview

DescriptiveMapPoints is a custom ArcGIS Pro add-in that allows users to annotate maps with descriptive points. Users can add, edit, delete, and zoom to points on the map, with each point having an associated description.

## Prerequisites

- ArcGIS Pro 3.0 or higher
- .NET 6.0 Runtime or higher
- ArcGIS Pro SDK for .NET 3.0 or higher


## Installation

### Method 1: Build and Run from Source

1. Clone the repository:
    ```bash
    git clone https://github.com/amr4tarek/ArcGIS-Task.git
    ```
2. Open the solution file `DescriptiveMapPoints.sln` in Visual Studio.
3. Ensure you have the required prerequisites installed:
    - ArcGIS Pro SDK for .NET
    - .NET 6.0 Runtime or higher
4. Build the solution in Visual Studio.
5. Run the solution, and it will automatically open in ArcGIS Pro if the build is successful.

### Method 1: Direct Installation

1. Download the DescriptiveMapPoints.esriAddinX file
2. Double-click the file to install it into ArcGIS Pro
3. If prompted, confirm the installation

### Method 2: Manual Installation

1. Copy the DescriptiveMapPoints.esriAddinX file to:
   ```
   C:\Users\{YourUsername}\Documents\ArcGIS\AddIns\ArcGISPro
   ```
2. Start or restart ArcGIS Pro

## Usage

### Accessing the Add-in

1. Open ArcGIS Pro and create or open a project
2. Ensure you have an active map view
3. Navigate to the "Add-In" tab in the ribbon
4. Click the "Show Point Addition" button in the "Group 1" section

### Adding Points

1. In the "Add Point Dockpane", enter a description in the text box
2. Click the "Add Point" button
3. Click on the map where you want to place the point
4. The point will appear on the map with its associated description in the list

### Editing Points

1. Select a point from the list in the dockpane
2. Click the "Edit" button
3. Click on the map to place the point in a new location
4. The point will be moved to the new location

### Deleting Points

1. Select a point from the list in the dockpane
2. Click the "Delete" button
3. Confirm the deletion when prompted

### Zooming to Points

1. Select any card from the list in the dockpane.
2. The map will zoom to the selected point.

## Features

- **Add Points**: Create points on the map with custom descriptions
- **Edit Points**: Move existing points to new locations
- **Delete Points**: Remove unwanted points from the map
- **Zoom To**: Quickly navigate to specific points on the map
- **Point Management**: All points are listed in a dockpane for easy access and management

## Troubleshooting

- **Add-in not appearing**: Ensure ArcGIS Pro is restarted after installation.
- **XAML errors**: If you encounter XAML-related errors, rebuild the solution in Visual Studio and run it again.

## Demo Video

For a quick demonstration of the DescriptiveMapPoints add-in, watch the video below:

<video width="640" height="360" controls>
  <source src="demo-video.mp4" type="video/mp4">
  Your browser does not support the video tag.
</video>
