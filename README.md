# Project Title

Name: Adam Nolan	

Student Number: C18363931	

Class Group: TU856-4

Video To Watch: [link](https://www.youtube.com/watch?v=PX4MiwkWo-I)

# Description of the project
The initial goal for my project was to procedurally generated Landmass/Island apropriately coloured with mountians and tree's with stuff in the sky and some audio. maybe some visuals in the distance, like a sound bar that moves to the music(stretch goal) that the player could walk around in. im glad to say that i achieved all of the things i wanted to do even the stetch goal

goals:
implementing perlin noise to procedurally generate the island terrain
allow a person to use Unity Editor to generate new and unique islands
feature personally created audio
implementing procedural tree creation

# Instructions for use
to use you just have to click play. once you do it will run with the currently configured settings and just work as is.
if you wish to moddify the way in which everything is created and run you can use the different options to change them.
if you want the circle of sound to go around the whole island as shown in the video and not just the spawn you need to set the scale to 11, 20, 11

# How it works
This project uses perlin noise maps to create height maps which is used for Terrain Chunk generation that creates a mesh that the player can walk on. you enter the colours of each of the different regions and then by setting the amount of noise, number of octaves, persistance, lacunarity, speed, height and height curve you can modify the map to your liking and make a ton of different terrain.
I use a falloffmap as shown below to make the perimeter of the map water so that the landmass is now waterlocked.
![FallOffMap](Images/FallOffMap.PNG)

There are also bars that move along to the intensity of the music of a particular point, this usually ends up with the lower end of the spectrum way higher than the higher end as theres way more bases and mids than high highs.

The first set of tree's were created using poisson distribution, but as that was creating problems with the y always being 0, i instead have now swapped to it being a raycast from heaven system where a ray comes from heaven, hits the mesh and creates the tree at the point.

![MapGenControlls](Images/MapGeneratorControlls.PNG)Land Chunk Data Generator
The above image is used to configure the land generation characteristics. We have the following parametres:

Noise Scale : Used to control the initial division of offset coordinates into a Perlin noisemap, this provides zoom.
Number of Octaves: number of layers of perlin noise.

Persistance: What fraction of amplitude persists in each Octave. (0-1)

Lacunarity: this gives the perlin noise map a focus on overall terrain heights with each passing octave growing larger and larger focusing on finer details, improving your ability to emulate terrain

Seed: A seed is used to give pseudo random generation to the perlin noisemap

Offset: An offset can be applied to pick an initial coordinate the generation begins from on the noisemap.

Height Changer: this is used to decide how high you want the peak sections to go.

Mesh Height Map: this is used to decide the slopes you want to be modified by the height changer, if from 0-0.6 is set to a 0 on the height map there will be no change to the mesh between 0 and 0.6, and if you then change it all the way to 1 you will have block that goes straight up, and if you slope it up to 1 over time you will have cone like mountians.

Terain Layers: Used for texture generation, a serializable struct definition for a terrain layer is written into Land Chunk Data Generatorm, containing a name string, height string and Color parametre. Heights are 0-1 to parse colour values from the initial parametre accordingly if the vertice value is higher than the layer.

![SkyGenControlls](Images/SkyGeneratorControlls.PNG)
The above image is mostly the same as the map controlls except there is an option for how fast you want the sky to move, this is emulating windspeed.

![SoundHeightControlls](Images/SoundHeightController.PNG)
this is how high the sound meters that move along to the song that is played in the background move.

![SpawnTreeFromRaycast](Images/SpawnTreeFromRaycast.PNG)
Here the spawnsize is the area in which the tree's are allowed to spawn ranging from -spawnsize to +spawnsize on the x and z co-ordinates.
Treenum is the number of tree's to be spawned.
Tree is the asset of the tree to be spawned
Delay is the delay of the tree spawning from the game starting

# Legacy Content!
![TreeCreatorControlls](Images/TreeCreator.PNG)
Tree: this is the tree model that will be used in the world.
Distance Between Tree's: this is the distance between the trees used in the poisson disc sampling
Region Size: this is the size of area of tree's that will be used.
## This was phased our as it only used an Y co-ordinate of 0 which lead to major clipping

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
| InstantiateCubes.cs | From [Peer Play- Audio Visualization playlist](https://www.youtube.com/watch?v=5pmoP1ZOoNs&list=PL3POsQzaCw53p2tA6AWf7_AWgplskR0Vo&ab_channel=PeerPlay) |
| PoissonDiscSampling.cs | From [Sebastian League's poisson disk tutorial](https://www.youtube.com/watch?v=7WcmyxyFO7o&ab_channel=SebastianLague) |
| TreeCreator.cs | Modified from [Sebastian League's poisson disk tutorial](https://www.youtube.com/watch?v=7WcmyxyFO7o&ab_channel=SebastianLague) |
| RigidbodyFirstPersonController.cs | From [Unity Legacy Character Content](https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2018-4-32351) |
| MouseLook.cs | From [Unity Legacy Character Content](https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2018-4-32351) |
| handcreatedleaf.png | Self Created |
| Mix No1.wav | Self Created |
| SpawnTreeFromRaycast.cs | Self Created |

# References
 [Sebastian Lague's Terrain Generation Tutorial Series EP 1-11](https://www.youtube.com/watch?v=wbpMiKiSKm8&list=PLFt_AvWsXl0eBW2EiBtl_sxmDtSgZBxB3)
[Sebastian Lague's poisson disc sampling] https://www.youtube.com/watch?v=7WcmyxyFO7o&ab_channel=SebastianLague
[Peer Play's Audio Visualisation] https://www.youtube.com/playlist?list=PL3POsQzaCw53p2tA6AWf7_AWgplskR0Vo
[unities legacy character movement scripts] https://assetstore.unity.com/packages/essentials/asset-packs/standard-assets-for-unity-2018-4-32351
# What I am most proud of in the assignment
That I finsished it and actually got to 80 commits(said at commit 64, hope it ages well).
# Proposal submitted earlier can go here:
Procedurally generated Landmass/Island apropriately coloured with mountians and tree's with stuff in the sky and some audio ill get mixed. maybe some visuals in the distance, like a sound bar that moves to the music(stretch goal)

