import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useCookies } from "react-cookie";

// Don't forget to
// // download the CSS file too OR
// // remove the following line if you're already using Tailwind
import "../styles.css";
import { useDispatch, useSelector } from "react-redux";
import { addName } from "../Reduce/userInformation";

export const Login = () => {
  const [cookies, setCookie, removeCookie] = useCookies(["token"]);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");
  const navigate = useNavigate();
  //Reduce
  const dispatch = useDispatch();
  const username = useSelector((state) => state.userData.value);

  const handleLogin = async (e) => {
    e.preventDefault();
    console.log(password, email);

    try {
      const response = await fetch("api/authentication/Login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ email, password }),
      });
      if (response.ok) {
        const data = await response.json();

        //data storage in localstorage
        localStorage.setItem(`user`, data.userName);
        console.log(localStorage.getItem`user`);

        //data storage with Redux , add username to Reduce
        dispatch(addName(data.userName));

        //cookieProvider - don't use it xxs attack
        // setCookie("token", data.token, { path: "/", maxAge: 30 });

        navigate("/");
      } else {
        setError("Invalid credentials");
      }
    } catch (error) {
      setError("An error occurred");
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
      setError("");
    }
  }, [error]);
  return (
    <div id="webcrumbs">
      <div className="w-[400px] bg-neutral-50 min-h-[500px] p-6 rounded-lg shadow-lg flex flex-col items-center justify-between">
        <h1 className="text-2xl font-title mb-6">Login</h1>

        <form className="w-full flex flex-col gap-4" onSubmit={handleLogin}>
          <div className="flex flex-col gap-1">
            <label htmlFor="email" className="font-semibold">
              Email
            </label>
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

          <div className="flex flex-col gap-1">
            <label htmlFor="password" className="font-semibold">
              Password
            </label>
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

          <button
            type="submit"
            className="w-full bg-primary text-white py-3 rounded-md mt-4"
          >
            Log In
          </button>
        </form>

        <p className="mt-6 text-neutral-700">
          Don't have an account?
          <a href="/register" className="text-primary ml-1">
            Register
          </a>
        </p>
      </div>
    </div>
  );
};
