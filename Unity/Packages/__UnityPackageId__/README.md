# <<Project-Name>>
<<Project-Description>>


* [Change Log](CHANGELOG.md)
* [UPM Package License](LICENCE.md)

## Unity Package
Unity Package is based on .Net Framework 4.71 and C# 10.

Note:
- Recently I have configure Unity Packages to Cloud Smith IO. Now any-one can use this package to their unity project
### How to Use
Add this information to you Unity manifest.json file. There is two way to setup repo in unity.
1. Using Unity Editor
2. Direct modify the manifest json in your code editor.

#### Using Unity Editor
1. Open your project in Unity Editor
2. Go To Edit menu and Select Project Settings.
3. Select Package manger from left pane of Project setting window.
4. Go To Scoped Registry section in Right Section.
5. Right section have sub section with Left and Right partition.
6. Click Plus icon in left partition. (It will add a new empty entry for registry setup.)
7. Now In Right Sub Section add detail as below mentioned.
  1. Add Name like "C# Helper package by iPAHeartBeat"
  2. In Url Section Added "https://npm.cloudsmith.io/ipaheartbeat/core"
  3. in  Scope(s) use Plus Icon from right side and Add "com.ipaheartbeat".
8. Close the window and Save project from File Menu.
9. Now you are ready to use the packages created by Me.

##### Modify manifest.json
1. Open You project in Finder/Explorer
2. Navigate to Package folder and Open manifest.json in your favorite Code/text editor. (it's JSON File.)
3. Find the entry `scopedRegistries` in json file in navigate.
4. It might be empty Json array or it might have few entries for other scoped registries.
5. Add below mention json date in the array
```
{
	"name": "C# Helper package by iPAHeartBeat",
	"scopes": [ "com.ipaheartbeat" ],
	"url": "https://npm.cloudsmith.io/ipaheartbeat/core/"
}
```
6. Save the file and close the File.
7. Open/reopen your unity project.
8. Now you are ready to use the packages created by Me.

Once you have setup the registry in unity project you can find the packages in Unity Package manger, from "My Registry" option.
