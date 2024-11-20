import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useCookies } from "react-cookie";

// Don't forget to
// // download the CSS file too OR
// // remove the following line if you're already using Tailwind
import "../styles.css";
import { useDispatch, useSelector } from "react-redux";
import { addName } from "../Reduce/userInformation";
import { setTokenExpiration } from "../Reduce/authSlice";

export const Login = ({ setIsLoggedIn }) => {
  const [cookies, setCookie, removeCookie] = useCookies(["token"]);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();
  //Reduce
  const dispatch = useDispatch();
  const username = useSelector((state) => state.userData.value);
  const tokenExpiration = useSelector(
    (state) => state.authData.tokenExpiration
  );

  const handleLogin = async (e) => {
    e.preventDefault();
    console.log(password, email);

    try {
      console.log(password);
      const response = await fetch("api/authentication/Login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        credentials: "include",
        body: JSON.stringify({ email, password }),
      });
      if (response.ok) {
        const data = await response.json();

        //data storage in localstorage
        // localStorage.setItem(`user`, data.userName);
        // console.log(localStorage.getItem`user`);
        console.log(data.email);

        //data storage with Redux , add username to Reduce
        dispatch(addName(data.userName));
        dispatch(setTokenExpiration(data.minutesTokenValid));
        setIsLoggedIn(true);
        //console.log(data.minutesTokenValid);

        //cookieProvider - don't use it xxs attack
        // setCookie("token", data.token, { path: "/", maxAge: 30 });

        navigate("/");
      }
      if (response.status === 400) {
        const dataError = await response.json();
        const badCredentialsKey = "Bad credentials";
        if (Array.isArray(dataError[badCredentialsKey])) {
          const errorMessages = dataError[badCredentialsKey].join(", ");
          setError(errorMessages);
        }
      } else {
        setError("Invalid credentials");
      }
    } catch (error) {
      // setError("An error occurred");
      console.error(error);
    }
  };

  useEffect(() => {
    // Figyeljük a Redux állapot változását
    if (username) {
      console.log("Felhasználónév a Redux-ból:", username);
    }
  }, [username]);

  const logOut = () => {
    document.cookie = "token=; path=/; max-age=0";
    removeCookie("token");
    navigate("/");
  };

  useEffect(() => {
    if (error) {
      alert(error);
      setError(null);
    }
  }, [error]);

  return (
    <div
      id="webcrumbs"
      className="flex justify-center items-center min-h-screen"
    >
      <div>
        <h1>Login</h1>

        <form onSubmit={handleLogin}>
          {/* Email mező */}
          <div className="flex flex-col gap-1">
            <label htmlFor="email" className="font-semibold"></label>
            <input
              type="email"
              id="email"
              name="email"
              className="p-2 border rounded-md w-full"
              placeholder="Enter your email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>

          {/* Password mező */}
          <div>
            <label htmlFor="password" className="font-semibold"></label>
            <input
              type="password"
              id="password"
              name="password"
              className="p-2 border rounded-md w-full"
              placeholder="Enter your password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
            />
          </div>

          {/* Login gomb */}
          <button type="submit">Log In</button>
        </form>

        <p>
          Don't have an account?
          <a href="/register">Register</a>
        </p>
      </div>
    </div>
  );
};
