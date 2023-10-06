# iPAHeartBeat Core Observer-System

# Introduction
Introducing the Observer Design Pattern Utility

This project is all about providing a powerful tool for building systems based on the Observer Design Pattern, such as a Signal System. With this pattern, two modules can communicate and share data using a signal data structure, while remaining completely independent from each other.

In simpler terms, it's like giving different modules or features a way to talk to each other without getting tangled up. This decouples them, allowing them to work together seamlessly without causing any problems. Whether you're new to programming or a seasoned coder, this utility will help you create flexible and efficient systems.

## Quick Links
- [Change Log](CHANGELOG.md)
- [UPM Package License](LICENCE.md)

## Unity Package
Our Unity Package is built using .NET Framework 4.71 and C# 10.

**Key Information**:
We've streamlined the process of accessing this package by configuring Unity Packages with Cloud Smith IO. This means that even if you're new to Unity, you can easily incorporate this package into your projects.

### How to Use

Getting started is a walk in the park. Simply add this package to your Unity project's `manifest.json` file. You have two straightforward options to set up the repository in Unity:

#### Using Unity Editor (Recommended for Beginners)

1. Open your project in the Unity Editor.
2. Go to the Edit menu and select Project Settings.
3. In the Project Settings window, click on Package Manager in the left pane.
4. Scroll down to the Scoped Registry section on the right.
5. In the left section, click the plus icon to create a new registry entry.
6. In the right section, fill in the following details:

   - **Name**: "C# Helper package by iPAHeartBeat"
   - **URL**: "https://npm.cloudsmith.io/ipaheartbeat/core"
   - **Scope(s)**: Click the plus icon on the right and add `com.ipaheartbeat`"

7. Close the window and save your project from the File menu.
8. Congratulations! You're now all set to tap into the capabilities of the packages we've created.

#### Modifying manifest.json (Advanced Option)

1. Open your project in Finder (macOS) or Explorer (Windows).
2. Navigate to the Package folder and open `manifest.json` in your preferred code or text editor (it's a JSON file).
3. Look for the `scopedRegistries` entry within the JSON file. If it's empty or missing, don't worry.
4. Add the following JSON data to the `scopedRegistries` array:

```json
{
	"name": "C# Helper package by iPAHeartBeat",
	"scopes": ["com.ipaheartbeat"],
	"url": "https://npm.cloudsmith.io/ipaheartbeat/core/"
}
```

5. Save the file and close it.
6. Open or reopen your Unity project.
7. Voilà! You're now all set to make full use of the packages we've developed.

After you've set up the registry in your Unity project, locating the packages in the Unity Package Manager under the "My Registry" option is a breeze.

# Reporting Issues
Should you encounter any problems or have feature requests, please don't hesitate to report them in the "Issues" section of our GitHub repository. We're committed to addressing issues promptly.

# Feature Requests
We're always eager to hear what features the community would like to see. Your requests will be carefully considered for potential implementation in the future. While we can't provide specific timelines, if any request is particularly critical due to guidelines or other factors, please let us know in the request so we can prioritize accordingly.

Feel free to share your feedback, and we'll keep a close eye on it.
