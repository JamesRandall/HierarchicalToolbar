# README

The Hierarchical Toolbar can be used to display toolbar hierarchies for example a toolbar that has an export button which when tapped changes the toolbar buttons to another set, in Xamarin.iOS / MonoTouch. You can position the toolbar on any side of the device and it will animate show and hiding and the pushing and popping of toolbars.

An example of it can be found in [Fluent Mind Map](https://itunes.apple.com/us/app/fluent-mind-map/id645539191?ls=1&mt=8) on the App Store.

This is early code that I've generalised out of that application. Ultimately this should be cross platform and I will eventually update to include an Android UI assembly.

To use in your iOS project add AccidentalFish.HierarchicalToolbar and AccidentalFish.HierarchicalToolbar.iOS to your application and create a new instance of the Toolbar class passing in the parent view and the toolbar definition. The repository shows an example of this. The code also makes use of [FluentAnimate](https://github.com/JamesRandall/FluentAnimate) and it is included in the iOS project.

Hopefully I'll get some more complete documentation up soon, in the meantime if it looks useful to you and you're stuck drop me an email and I'll try and help out.


## License

Copyright (C) 2013 Accidental Fish Ltd.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.