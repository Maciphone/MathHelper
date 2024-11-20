import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";

export default function Welcome() {
  const [userName, setUserName] = useState(null);
  const userReduxName = useSelector((state) => state.userData.value);
  const navigate = useNavigate();

  //const [cookie] = useCookies(["token"]);
  //const token = cookie.token;
  //console.log(token);
  useEffect(() => {
    var name = localStorage.getItem("user");

    //var claims = TokenHandler(token);
    if (name) {
      // console.log(claims);
      // var name = claims.name;
      setUserName(name);
      console.log(name);
    }
  }, []);

  return (
    <div>
      <p>{userReduxName ? `Welcome ${userReduxName}` : "Nincs token"}</p>
      <p>
        {userReduxName ? (
          <div>
            Welcome ${userReduxName} be vagy jelentkezve <br /> Szia, kis
            matekzseni! 👋 Üdvözlünk a Matekvarázs világában, ahol izgalmas és
            játékos feladatokon keresztül fejlesztheted a számolási tudásodat!
            🎲💡 Legyen szó összeadásról, kivonásról vagy szorzásról, itt mindig
            találsz neked való kihívást. 🌟 Fedezd fel a rejtélyeket, gyűjts
            pontokat, és lépj szintet, miközben észre sem veszed, hogy tanulsz!
            Kalandra fel, mert a matek szórakozás is lehet! 🚀✨
          </div>
        ) : (
          <button onClick={() => navigate("/login")}>Login</button>
        )}
      </p>
      Matekra fel!
    </div>
  );
}
