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
        <button>Szorzás</button>
      </Link>
      <Link to={"/text"}>
        <button>Szöveges</button>
      </Link>
    </nav>
  );
}
