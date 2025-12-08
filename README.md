# Virtual Escape Room

A VR escape room experience with Alchemist vibe, built with Unity and XR Interaction Toolkit, designed for Meta Quest 3. I developed this project as a final project for my class CSCI 379 - Design and Development for XR at Bucknell University.

## Prerequisites

- Unity Editor (with Universal Render Pipeline support)
- Meta Quest 3 headset
- Approximately 20MB of storage space
- Required Unity packages:
  - OpenXR
  - XR Interaction Toolkit
  - XR Simulation

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/ctmphuongg/Virtual-Escape-Room.git
```

### 2. Open in Unity

1. Launch Unity Hub
2. Click "Add" and select the cloned project folder
3. Open the project with a compatible Unity version

### 3. Install Required Packages

Ensure the following packages are installed via the Package Manager (Window > Package Manager):

- OpenXR Plugin
- XR Interaction Toolkit
- XR Device Simulator (for testing without a headset)

### 4. Open the Main Scene

Navigate to `Assets/Scenes/SampleScene` and open it.

### 5. Fix Material Issues (If Needed)

If materials appear magenta after opening the project:

1. Select all affected materials in the Project window
2. Go to **Window > Rendering > Render Pipeline Converter**
3. Choose **Material Upgrade**
4. Click **Initialize and Convert**

This converts materials to Universal Render Pipeline (URP) format.

## Building for Meta Quest 3

1. Go to **File > Build Settings**
2. Select **Android** as the platform
3. Click **Switch Platform**
4. Go to **File > Build Profiles**
5. Select **Meta Quest** profile
6. Set **Run Device** to **Quest 3**
7. Click **Build and Run**

The application will build and deploy directly to your connected Quest 3 headset.

## Demo

Watch the gameplay demo here: [Video Demo](https://drive.google.com/file/d/1En3eHnA_4_LmAi3f5sS8XOFWbWqvjlWA/view?usp=sharing)

## Assets & Resources

This project uses the following third-party assets and resources:

- **Environment Assets**: [Alchemist House](https://assetstore.unity.com/packages/3d/environments/alchemist-house-112442) - Prefabs for room scene and thematic elements
- **Interactive Props**: [Keypad Free](https://assetstore.unity.com/packages/3d/props/electronics/keypad-free-262151) - Keypad prefabs for puzzle mechanics
- **VR Configuration**: Settings and fonts adapted from Unity Learn's [VR Development Pathway](https://learn.unity.com/pathway/vr-development) VR Room assignment

## Troubleshooting

**Issue**: Materials appear pink/magenta  
**Solution**: Follow the material conversion steps in section 5 of Setup Instructions

**Issue**: XR packages not found  
**Solution**: Install required packages through Window > Package Manager

**Issue**: Build fails for Quest 3  
**Solution**: Ensure Android Build Support is installed in Unity Hub and your Quest 3 is connected via USB with developer mode enabled

## License

Please refer to individual asset licenses from the Unity Asset Store for usage rights.
