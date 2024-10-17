import React from "react";
import { Link } from "react-router-dom";

export default function Navbar() {
  return (
    <nav>
      <Link to={"/"}>
        <button>Home</button>
      </Link>
      <Link to={"/algebra"}>
        <button>Összeadás</button>
      </Link>
      <Link to={"/multiplication"}>
        <button>Szorzás</button>
      </Link>
      <Link to={"/division"}>
        <button>Osztás</button>
      </Link>
      <Link to={"/text"}>
        <button>Szöveges</button>
      </Link>
      <Link to={"/maradek"}>
        <button>MaradékOsztás</button>
      </Link>
      <Link to={"/login"}>
        <button>Login</button>
      </Link>
    </nav>
  );
}
