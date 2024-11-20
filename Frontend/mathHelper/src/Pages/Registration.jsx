import React, { useState } from "react";

export default function Registration() {
  const [email, setEmail] = useState("");
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [attempt, setAttempt] = useState(0);
  const [error, setError] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    if(attempt==3){
        alert("nincs több probálkozásod")
    }

    // Validációk
    if (!email || !username || !password || !confirmPassword) {
      setError("Kérjük, töltsd ki az összes mezőt.");
      return;
    }

    if (password !== confirmPassword) {
      setError("A jelszavak nem egyeznek.");
      return;
    }

    const response = await fetch("api/authentication/Register", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      credentials: "include",
      body: JSON.stringify({ email, username, password }),
    });
    if (!response.ok) {
      const data = await response.json();
      const registrationErrors = data;
      if (registrationErrors) {
        Object.entries(registrationErrors).forEach(([code, description]) => {
          console.log(`Error code: ${code}, Description: ${description}`);
          if (code == "DuplicateEmail") {
            setEmail("foglalt");
          }
          if (code == "DuplicateUserName") {
            setUsername("foglalt");
          }
        });
        setAttempt(prev => prev+1);
      } else {
        console.log("Váratlan hiba történt.");
        setError("Váratlan hiba történt.");
      }
    } else {
      console.log("Regisztráció sikeres:", { email, username, password });
      alert("sikeres regisztráció");
      setError(""); // sikeres regisztráció esetén töröljük az esetleges hibát

      // Form mezők törlése a sikeres regisztráció után
      setEmail("");
      setUsername("");
      setPassword("");
      setConfirmPassword("");
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Regisztráció</h2>

      {error && <p style={{ color: "red" }}>{error}</p>}

      <div>
        <label>Email cím:</label>
        <input
          type="email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          required
        />
      </div>

      <div>
        <label>Felhasználónév:</label>
        <input
          type="text"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
          required
        />
      </div>

      <div>
        <label>Jelszó:</label>
        <input
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          required
        />
      </div>

      <div>
        <label>Jelszó újra:</label>
        <input
          type="password"
          value={confirmPassword}
          onChange={(e) => setConfirmPassword(e.target.value)}
          required
        />
      </div>

      <button type="submit">Regisztráció</button>
    </form>
  );
}
