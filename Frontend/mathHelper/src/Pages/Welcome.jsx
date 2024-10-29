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
          `Welcome ${userReduxName} be vagy jelentkezve`
        ) : (
          <button onClick={() => navigate("/login")}>Login</button>
        )}
      </p>
      Welcome
    </div>
  );
}
