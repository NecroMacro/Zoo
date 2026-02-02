![2026-02-0217-05-36-ezgif com-video-to-gif-converter (1)](https://github.com/user-attachments/assets/7fe607ad-c1b3-4afa-80ff-e6bf45134cc1)

Техническое задание:<br>Every (1-2) seconds one animal appears and starts moving randomly. It can collide with other animals by physics. 
If animals move out of screen it changed movement direction to return back to screen.

Food chain:<br>
1. Prey - if it collided with another prey animal they would just fly apart by physics. If it’s collided with predator it become dead and disappear from screen.
2. Predators - if they collide with other animals, prey or predators they will eat them. If a predator collides with another predator, one of them will survive and the other will become dead (you canchoose any easiest way to implement it). Each time a predator eats another animal the label “Tasty!” should appear underpredators

Animals:<br>
Frog - is a prey animal. Its movement is jumps. Every x second make a jump for fixed distance.
Snake - are predators. It moving linear (fixed dispense by second).

Используемые технологии и паттерны:<br>

Data-Driven, Pooling, MVP, Fabric<br>
VContainer - dependency injection<br>
R3 - reactive framework<br>
UniTask - async lib for Unity
