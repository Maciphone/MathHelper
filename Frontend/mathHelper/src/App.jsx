import "./App.css";
import { useNavigate, useRoutes } from "react-router-dom";
import Layout from "./Components/Layout";
import Welcome from "./Pages/Welcome";
import Algbera from "./Pages/Algbera";
import Multiplication from "./Pages/Multiplication";
import Division from "./Pages/Division";
import AITextExercise from "./Pages/TesterPage";
import LevelButtons from "./Components/LevelButtons";
import { Login } from "./Pages/Login";
import { CookiesProvider } from "react-cookie";
import RemainDivision from "./Pages/RemainDivision";
import MySolutions from "./Pages/MySolutions";
import { useDispatch, useSelector } from "react-redux";
import { useEffect } from "react";
import { removeName } from "./Reduce/userInformation";

function App() {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const tokenExpiration = useSelector(
    (state) => state.authData.tokenExpiration
  );
  console.log(tokenExpiration);
  //backend sends in min tokens expire, this useEffect sets a timeout
  useEffect(() => {
    if (!tokenExpiration) return;
    console.log("Token expiration (minutes):", tokenExpiration);
    const timeUntillExpiration = tokenExpiration * 60 * 1000; //set to milisecs
    console.log("Time until expiration (ms):", timeUntillExpiration);

    const timeoutSet = setTimeout(() => {
      dispatch(removeName());
      alert("a munkamenet lejárt, jelntkezz be újra");
      //navigate("/login");
    }, timeUntillExpiration);
    return () => clearTimeout(timeoutSet);
  }, [tokenExpiration, dispatch, navigate]);

  const routes = useRoutes([
    {
      path: "/",
      element: <Layout />,
      children: [
        {
          path: "/",
          element: <Welcome />,
        },
        {
          path: "/algebra",
          element: <Algbera />,
        },
        {
          path: "/multiplication",
          element: <Multiplication />,
        },
        {
          path: "/division",
          element: <Division />,
        },
        {
          path: "/text",
          element: <AITextExercise />,
        },
        {
          path: "/tester",
          element: <AITextExercise />,
        },
        {
          path: "/maradek",
          element: <RemainDivision />,
        },
        {
          path: "/saját",
          element: <MySolutions />,
        },
        {
          path: "/login",
          element: <Login />,
        },
      ],
    },
  ]);
  return <CookiesProvider>{routes}</CookiesProvider>;
}

export default App;
