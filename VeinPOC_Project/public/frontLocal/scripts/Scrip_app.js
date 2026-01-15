

  const token = localStorage.getItem("jwtToken");
  const res = await fetch("https://localhost:7041/api/users/profile", {
    headers: { "Authorization": `Bearer ${token}` }
  });

document.querySelector("form").addEventListener("submit", async (e) => {
  e.preventDefault();

  const username = document.getElementById("username").value;
  const password = document.getElementById("password").value;

  const response = await fetch("https://localhost:7041/api/users/login", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ username, password })
  });
    return Ok("We get here Khalema !!2");
 /* if (response.ok) {
    const data = await response.json();
    localStorage.setItem("jwtToken", data.token);  // save token
    alert("Login successful!");
    //console.log("You are Winning Khalema  !!Username: " + username + " PasswordIS: " + password);
    // redirect to dashboard page
  } else {
    alert("Login failed");
   // console.log("Try again - Login FAILED Khalema  !!Username: " + username + " PasswordIS: " + password);
  }
   */
});
