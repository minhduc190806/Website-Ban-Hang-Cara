document.addEventListener("DOMContentLoaded", () => {
  const TOTAL = 25;

  const container = document.createElement("div");
  container.style.cssText = `
    position:fixed;
    top:0;left:0;
    width:100%;height:100%;
    pointer-events:none;
    overflow:hidden;
    z-index:9999;
  `;
  document.body.appendChild(container);

  const flakes = [];

  for (let i = 0; i < TOTAL; i++) {
    const f = document.createElement("div");
    f.innerHTML = "✨";
    f.style.position = "absolute";
    f.style.fontSize = Math.random() * 15 + 15 + "px";
    container.appendChild(f);

    flakes.push({
      el: f,
      x: Math.random() * window.innerWidth,
      y: Math.random() * window.innerHeight,
      speed: Math.random() * 1 + 0.5,
      swing: Math.random() * 2,
    });
  }

  function animate() {
    flakes.forEach(f => {
      f.y += f.speed;
      f.x += Math.sin(f.y * 0.01) * f.swing;

      if (f.y > window.innerHeight) {
        f.y = -20;
        f.x = Math.random() * window.innerWidth;
      }

      f.el.style.transform = `translate(${f.x}px, ${f.y}px)`;
    });

    requestAnimationFrame(animate);
  }

  animate();
});