let map = L.map('map').setView([20, 0], 2);
L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(map);

async function runTrace() {
    const target = document.getElementById('target').value;
    const res = await fetch(`/trace?target=${target}`);
    const data = await res.json();

    console.log("Trace result:", data);

    document.getElementById('results').innerHTML = `
        <tr><th>Hop</th><th>IP</th><th>RTT (ms)</th><th>Location</th><th>ISP</th></tr>
        ${data.map((hop, i) => `
            <tr>
                <td>${i + 1}</td>
                <td>${hop.ip}</td>
                <td>${hop.rtt.join(', ')}</td>
                <td>${hop.city}, ${hop.country}</td>
                <td>${hop.isp}</td>
            </tr>
        `).join('')}
    `;

    const latlngs = data.filter(h => h.lat && h.lon).map(h => [h.lat, h.lon]);
    if (latlngs.length) {
        map.setView(latlngs[0], 4);
        L.polyline(latlngs, { color: 'blue' }).addTo(map);
        latlngs.forEach(([lat, lon], i) => {
            L.circleMarker([lat, lon], {
                radius: 5,
                color: 'red'
            }).bindPopup(`Hop ${i + 1}`).addTo(map);
        });
    }
}