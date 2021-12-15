# Project Title

Name: Adam Nolan	

Student Number: C18363931	

Class Group: TU856-4

# Description of the project
The initial goal for my project was to procedurally generated Landmass/Island apropriately coloured with mountians and tree's with stuff in the sky and some audio. maybe some visuals in the distance, like a sound bar that moves to the music(stretch goal) that the player could walk around in. 

goals:
implementing perlin noise to procedurally generate the island terrain
allow a person to use Unity Editor to generate new and unique islands
feature personally created audio
implementing procedural tree creation

# Instructions for use
to use you just have to click play. once you do it will run with the currently configured settings and just work as is.
if you wish to moddify the way in which everything is created and run you can use the different options to change them.

![MapGenControlls](Images/MapGeneratorControlls.PNG)
![SkyGenControlls](Images/SkyGeneratorControlls.PNG)
![SoundHeightControlls](Images/SoundHeightController.PNG)
![TreeCreatorControlls](Images/TreeCreator.PNG)
# How it works

# List of classes/assets in the project and whether made yourself or modified or if its from a source, please give the reference

| Class/asset | Source |
|-----------|-----------|
| Tree.prefab | Self Created |
| AllDisplays.cs | Modified from [reference] Modified from [Sebastian Lague's Terrain Generation Tutorial Series EP 1-11 (https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3) |
| FallOffMap.cs | From [Sebastian Lague's Terrain Generation Tutorial Series EP 1-11 (https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3) |
| MapGen.cs | Modified from [Sebastian Lague's Terrain Generation Tutorial Series EP 1-11 (https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3) |
| FallOffMap.cs | Modified from [Sebastian Lague's Terrain Generation Tutorial Series EP 1-11 (https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3) |
| MeshGen.cs | Modified from [Sebastian Lague's Terrain Generation Tutorial Series EP 1-11 (https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3) |
| Noise.cs | Modified from [Sebastian Lague's Terrain Generation Tutorial Series EP 1-11 (https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3) |
| SkyGen.cs | Modified from [Sebastian Lague's Terrain Generation Tutorial Series EP 1-11 (https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3) |
| TextureGen.cs | Modified from [Sebastian Lague's Terrain Generation Tutorial Series EP 1-11 (https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3) |
| AudioPeer.cs | Modified From [Peer Play- Audio Visualization playlist](https://www.youtube.com/watch?v=5pmoP1ZOoNs&list=PL3POsQzaCw53p2tA6AWf7_AWgplskR0Vo&ab_channel=PeerPlay)|
| InstantiateCubes.cs | Modified From [Peer Play- Audio Visualization playlist](https://www.youtube.com/watch?v=5pmoP1ZOoNs&list=PL3POsQzaCw53p2tA6AWf7_AWgplskR0Vo&ab_channel=PeerPlay) |
| PoissonDiscSampling.cs | From [reference](https://www.youtube.com/watch?v=7WcmyxyFO7o&ab_channel=SebastianLague) |
| TreeCreator.cs | Modified from [reference](https://www.youtube.com/watch?v=7WcmyxyFO7o&ab_channel=SebastianLague) |
| RigidbodyFirstPersonController.cs | From [reference](https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2018-4-32351) |
| MouseLook.cs | From [reference](https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2018-4-32351) |
| handcreatedleaf.png | Self Created |
| Mix No1.wav | Self Created |

# References
 [Sebastian Lague's Terrain Generation Tutorial Series EP 1-11](https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3)
[Sebastian Lague's poisson disc sampling] https://www.youtube.com/watch?v=7WcmyxyFO7o&ab_channel=SebastianLague
[Peer Play's Audio Visualisation] https://www.youtube.com/playlist?list=PL3POsQzaCw53p2tA6AWf7_AWgplskR0Vo
[unities legacy character movement scripts] https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2018-4-32351
# What I am most proud of in the assignment
That I finsished it.
# Proposal submitted earlier can go here:

## This is how to markdown text:

This is *emphasis*

This is a bulleted list

- Item
- Item

This is a numbered list

1. Item
1. Item

This is a [hyperlink](http://bryanduggan.org)

# Headings
## Headings
#### Headings
##### Headings

This is code:

```Java
public void render()
{
	ui.noFill();
	ui.stroke(255);
	ui.rect(x, y, width, height);
	ui.textAlign(PApplet.CENTER, PApplet.CENTER);
	ui.text(text, x + width * 0.5f, y + height * 0.5f);
}
```

So is this without specifying the language:

```
public void render()
{
	ui.noFill();
	ui.stroke(255);
	ui.rect(x, y, width, height);
	ui.textAlign(PApplet.CENTER, PApplet.CENTER);
	ui.text(text, x + width * 0.5f, y + height * 0.5f);
}
```

This is an image using a relative URL:

![An image](images/p8.png)

This is an image using an absolute URL:

![A different image](https://bryanduggandotorg.files.wordpress.com/2019/02/infinite-forms-00045.png?w=595&h=&zoom=2)

This is a youtube video:

[![YouTube](http://img.youtube.com/vi/J2kHSSFA4NU/0.jpg)](https://www.youtube.com/watch?v=J2kHSSFA4NU)

This is a table:

| Heading 1 | Heading 2 |
|-----------|-----------|
|Some stuff | Some more stuff in this column |
|Some stuff | Some more stuff in this column |
|Some stuff | Some more stuff in this column |
|Some stuff | Some more stuff in this column |

