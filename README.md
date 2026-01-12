# 🐦 Flappy Bird

Previously, I partially re-created Flappy Bird [in this repository](https://github.com/leo-bloise/FlappyBird). However, it didn’t work as expected due to my limited knowledge of game development at the time and some poor architectural decisions.

After learning more and gaining better experience, I decided to recreate the project from scratch, applying the improvements listed below 🚀

## ✨ Improvements & Evolutions

- 📐 Deeper understanding of physics, including proper integration of velocity and acceleration values.

- ⚡ Performance improvements by avoiding GPU context switches between multiple textures. The game now uses a single texture with different regions, which can also be defined via an XML file.

- 🐛 Collision debugging support, with visual feedback to make debugging faster and more intuitive.

- 🔄 Observer pattern implementation for communication between entities, replacing thread-based communication using sync mechanisms.

## 🛠️ Tasks

- [ ] 🔊 Implement sound effects for jumping and dying.

- [ ] 🏠 Create a main menu screen.

- [ ] 💀 Create a game over screen.

- [ ] 🧠 Investigate and fix the pipe generation issue where memory usage continuously increases.