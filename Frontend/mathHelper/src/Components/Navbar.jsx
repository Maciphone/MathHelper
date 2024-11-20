import React from "react";
import { Link } from "react-router-dom";

export default function Navbar() {
  return (
    <nav className="navbar">
      <Link to={"/"} className="nav-link">
        Home
      </Link>
      <Link to={"/algebra"} className="nav-link">
        Összeadás
      </Link>
      <Link to={"/multiplication"} className="nav-link">
        Szorzás
      </Link>
      <Link to={"/division"} className="nav-link">
        Osztás
      </Link>
      <Link to={"/text"} className="nav-link">
        Szöveges
      </Link>
      <Link to={"/maradek"} className="nav-link">
        MaradékOsztás
      </Link>
      <Link to={"/saját"} className="nav-link">
        Megoldásaim
      </Link>
      <Link to={"/todelete"} className="nav-link">
        Próbaoldal
      </Link>
      <Link to={"/login"} className="nav-link">
        Login
      </Link>
    </nav>
  );
}

// export default function Navbar() {
//   return (
//     <nav>
//       <Link to={"/"}>
//         <button>Home</button>
//       </Link>
//       <Link to={"/algebra"}>
//         <button>Összeadás</button>
//       </Link>
//       <Link to={"/multiplication"}>
//         <button>Szorzás</button>
//       </Link>
//       <Link to={"/division"}>
//         <button>Osztás</button>
//       </Link>
//       <Link to={"/text"}>
//         <button>Szöveges</button>
//       </Link>
//       <Link to={"/maradek"}>
//         <button>MaradékOsztás</button>
//       </Link>
//       <Link to={"/saját"}>
//         <button>Megoldásaim</button>
//       </Link>
//       <Link to={"/todelete"}>
//         <button>próbaoldal</button>
//       </Link>
//       <Link to={"/login"}>
//         <button>Login</button>
//       </Link>
//     </nav>
//   );
// }
