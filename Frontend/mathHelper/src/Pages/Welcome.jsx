import React, { useEffect, useState } from "react";
import { useSelector } from "react-redux";

export default function Welcome() {
  const [userName, setUserName] = useState(null);
  const userReduxName = useSelector((state) => state.userData.value);
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
      <p>{userName ? `Welcome ${userName}` : "Nincs token"}</p>
      <p>
        {userReduxName
          ? `Welcome ${userReduxName} be vagy jelentkezve`
          : "nem vagy bejelentkezve"}
      </p>
      Welcome
    </div>
  );
}
