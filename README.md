# 🐦 Flappy Bird

This project is a full re-creation of **Flappy Bird**, built from scratch using **MonoGame**.

Previously, I had partially recreated Flappy Bird in [this repository](https://github.com/leo-bloise/FlappyBird). However, due to limited game development experience at the time and some poor architectural decisions, the project did not behave as expected.

After gaining more experience and a deeper understanding of game development concepts, I decided to rebuild the game entirely, focusing on **clean architecture, performance, and correctness** 🚀

---

## ✨ Improvements & Evolutions

* 📐 **Improved physics model**
  Proper integration of velocity and acceleration, resulting in more consistent and predictable movement.

* ⚡ **Rendering performance optimizations**
  GPU context switches were eliminated by using a **single texture atlas**, with sprite regions defined directly or via XML.

* 🐛 **Collision debugging tools**
  Visual collision overlays can be enabled to speed up debugging and fine-tuning.

* 🔄 **Observer pattern for entity communication**
  Replaced thread-based communication and synchronization mechanisms with a clean observer-based approach.

* 🔊 **Sound effects implemented**
  Includes audio feedback for actions such as jumping and player death.

* 🏠 **Main menu screen**
  A dedicated entry screen allowing the player to start the game.

* 💀 **Game over screen**
  Displays the final score and best score, providing clear feedback at the end of each run.

* 🧠 **Pipe generation memory issue fixed**
  Resolved a bug where pipe instances were continuously allocated, causing memory usage to grow over time.

---

## 📚 Purpose

This project was built primarily as a **learning exercise** and a way to validate improved architectural and technical decisions compared to my earlier attempt.

Feel free to explore, fork, or build on top of it! 🎮
