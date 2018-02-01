# Input# #
A C# abstraction on top of Godot's input events that makes life just a little bit easier (especially for local multiplayer games with multiple active controllers).  This is still in its early stages.

Disclaimers:
* It's just a copy paste from my current project: it isn't a standalone library
* It's relatively untested.  Right now I can't / won't vouch for its ability to not destroy everything you love and care about
* It currently uses IConvertible to let you use your own enums without using generics.  I'm not sure I like this design but it works.