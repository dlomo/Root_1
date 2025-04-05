function updateClock() {
    const now = new Date();
    const clockElement = document.getElementById('clock');
    const hours = now.getHours().toString().padStart(2, '0');
    const minutes = now.getMinutes().toString().padStart(2, '0');
    const seconds = now.getSeconds().toString().padStart(2, '0');
    const date = now.toLocaleDateString();

    clockElement.textContent = `${date} ${hours}:${minutes}:${seconds}`;
}

updateClock();
setInterval(updateClock, 1000);
