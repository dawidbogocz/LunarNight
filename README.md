# LunarNight

*A Minecraft-inspired voxel sandbox game built in Unity*

![Unity](https://img.shields.io/badge/Unity-2021.3+-000000?style=for-the-badge&logo=unity&logoColor=white)
![C#](https://img.shields.io/badge/C%23-8.0+-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green.svg?style=for-the-badge)

## Overview

LunarNight is a voxel-based sandbox game that explores infinite procedural world generation using Unity. Experience a unique blend of creativity, exploration, and survival in dynamically generated landscapes that ensure no two playthroughs are the same.

### Key Features

- **Infinite World Generation** - Explore limitless landscapes powered by advanced procedural algorithms
- **Voxel-Based Building** - Create and destroy blocks to build anything you imagine  
- **Dynamic Terrain** - Realistic biomes with rivers, forests, mountains, and caves
- **Crafting System** - Gather resources and craft tools, weapons, and building materials
- **Performance Optimized** - Efficient chunk loading and LOD system for smooth gameplay

## Technical Highlights

### Procedural Generation Engine
The game leverages **Perlin Noise algorithms** for creating natural, coherent landscapes:

- **Terrain Generation**: Smooth height maps creating realistic mountain ranges and valleys
- **Biome Distribution**: Natural placement of forests, deserts, plains, and water bodies
- **Resource Spawning**: Intelligent ore and material distribution throughout the world
- **Cave Systems**: Complex underground networks for exploration and mining

### Performance Optimization
- **Chunk-based Loading**: Efficient memory management for infinite worlds
- **LOD (Level of Detail)**: Dynamic quality adjustment based on distance
- **Multithreaded Generation**: Background world generation without gameplay interruption
- **Occlusion Culling**: Optimized rendering for complex voxel environments

## Built With

- **Unity Engine 2021.3+** - Game engine and rendering
- **C# 8.0+** - Primary programming language
- **Perlin Noise** - Procedural generation algorithms
- **Unity Job System** - Multithreaded performance optimization
- **Unity Burst Compiler** - High-performance math operations

## Getting Started

### Prerequisites
- Unity 2021.3 LTS or newer
- Visual Studio 2019+ or VS Code
- Git for version control

### Installation
```bash
# Clone the repository
git clone https://github.com/dawidbogocz/LunarNight.git

# Open in Unity Hub
# File -> Open -> Select the LunarNight folder
```

### Controls
- **WASD** - Movement
- **Mouse** - Look around
- **Left Click** - Destroy block
- **Right Click** - Place block  
- **E** - Open inventory
- **Space** - Jump
- **Shift** - Sprint

## Development Journey

This project showcases advanced Unity development techniques and serves as a deep dive into:

- **Procedural Content Generation** - Creating infinite, varied game worlds
- **Performance Engineering** - Optimizing complex voxel rendering
- **Game Architecture** - Modular, maintainable codebase design
- **Algorithm Implementation** - Custom noise functions and spatial algorithms

## Future Enhancements

- Multiplayer networking support
- Advanced lighting and shadow systems  
- Weather and day/night cycles
- Animal AI and ecosystem simulation
- Advanced crafting and technology trees
- Mod support and scripting API

## Contributing

While this is primarily a learning project, suggestions and feedback are welcome! Feel free to:

- Report bugs via issues
- Suggest features or improvements  
- Share your creations and screenshots

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

<div align="center">

**Built by [Dawid Bogocz](https://github.com/dawidbogocz)**

*Part of my game development portfolio - exploring the intersection of algorithms, performance, and player experience*

</div>