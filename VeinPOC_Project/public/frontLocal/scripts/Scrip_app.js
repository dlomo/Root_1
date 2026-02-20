document.querySelector("form").addEventListener("submit", async (e) => {
  e.preventDefault();

  const username = document.getElementById("username").value.trim();
  const password = document.getElementById("password").value.trim();

  try {
    const response = await fetch("/api/login", {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ username, password })
    });

    if (!response.ok) {
      const err = await response.json();
      alert(err.message || "Login failed");
      return;
    }

    const data = await response.json();
    localStorage.setItem("jwtToken", data.token);

    alert("Login successful!");
    //load new window or content here
  } catch (err) {
    console.error(err);
    alert("Unexpected error");
  }
});